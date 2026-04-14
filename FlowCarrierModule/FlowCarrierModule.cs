using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTAMachine.Module;
using HTAMachine.Machine;
using Hta.MotionBase;
using System.Threading;
using ModuleTemplate.Services;
using System.Xml;
using HTAMachine.Machine.Services;
using System.Collections.Concurrent;
using ControlFlow.Executor;
using FlowCarrierModule.Properties;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using System.CodeDom.Compiler;
using HTA.SecsBase;
using System.Xml.Linq;
using DevExpress.DirectX.Common.DirectWrite;
using ModuleTemplate;
using System.IO;
using System.Xml.Serialization;
//using BigMapStatisticContract;
using HTA.Com.WCF.DuplexService;
using DevExpress.XtraPrinting.Export.Pdf;
using System.Diagnostics;

namespace TA2000Modules
{
    public partial class FlowCarrierModule : ModuleBase
    {
        bool bVirtual = false;
        //public  BigMapStatisticContract.BigMapServer _BigMapServerDB = null;
        //public  WcfDuplexServer<BigMapStatisticContract.IBigMapServer> wcfDuplexServer = null;
        //public BigMapClient client = null;

        #region SYS Data

        public List<VelocityData> InspectVelList; //流道縱移軸速度List (宣告速度List欄位)
        //public Velocity _InspectVelocity;

        public List<VelocityData> FlowVelList;  //流道傳送軸速度List (宣告速度List欄位)
        public BoatCarrier BoatCarrier1 = new BoatCarrier(); //實作BoatCarrier資料內容
        public bool IsVisionFinish = false;
        public BlockingCollection<bool> IsVisionFinishQueue = new BlockingCollection<bool>(); //宣告佇列欄位 (阻塞收集機制)
        public BlockingCollection<bool> AlarmQueue = new BlockingCollection<bool>();
        public BlockingCollection<bool> UpperPutQueue = new BlockingCollection<bool>();
        public BlockingCollection<bool> LowerReadyQueue = new BlockingCollection<bool>();
        [ModuleGlobalSetting][MotorOffsetClass] public FlowMotorOffset FlowMotorOffset = new FlowMotorOffset();
        public MapDataInfo MapData = new MapDataInfo();
        public IMachineSimpleController MachineSimpleController;
        public bool CanPutProduct = false;
        private IShowWorkLogService _showWorkLogService;
        private bool _visionIsByPass = false;
        private bool _isGolden = false;
        private bool _cvAndVisionTest =false; //CV And Vision Test
        public int IDNum_server;
        public int IDNum_client;
        public bool Rcv_Server_flag { get; set; } = false;
        public bool Rcv_Client_flag { get; set; } = false;
        //TCPIP ID的計數
        public int IndexCount { get; set; } = 0;
        bool _isInStart = false;

        /// <summary>
        /// 確認下位機已經回傳GotBoat
        /// </summary>
        public bool LowerGotBoat = false;
        public Stopwatch LoadProductTime = new Stopwatch();
        public Stopwatch OutProductTime = new Stopwatch();
        public UPHClass UPHClass1 = new UPHClass();
        public TimeSpan InspectSpendTime = new TimeSpan(0, 0, 0, 0, 0);
        #endregion



        //#region DefineHardware
        //[DefineHardware] public IAxis 視覺縱移軸;
        //[DefineHardware] public IAxis BX2_流道傳送軸;

        //[DefineHardware] public IInputIO X066000_傳送流道_入料銜接檢知_上下對照;
        //[DefineHardware] public IInputIO X066002_傳送流道_產品位置檢知;
        //[DefineHardware] public IInputIO X066003_傳送流道_產品減速檢知;
        //[DefineHardware] public IInputIO X066004_傳送流道_產品到位檢知;
        //[DefineHardware] public IInputIO X066005_傳送流道_到位氣缸_上升;
        //[DefineHardware] public IInputIO X066006_傳送流道_到位氣缸_下降;
        //[DefineHardware] public IInputIO X066007_傳送流道_出料銜接檢知_側邊反射;
        //[DefineHardware] public IInputIO X066007_治具頂升氣缸_上升;
        //[DefineHardware] public IInputIO X066008_治具頂升氣缸_下降;
        //[DefineHardware] public IInputIO X066014_靜電消除器_警報_Warning;
        //[DefineHardware] public IInputIO X066015_靜電消除器_異常_Alarm;

        //[DefineHardware] public IInputIO X064002_光閘門開啟檢知; //M01
        //[DefineHardware] public IInputIO X064003_光閘觸發檢知; //M01

        //[DefineHardware] public IInputIO X064012_SMEMA_上位1_通知已放料信號;
        //[DefineHardware] public IInputIO X064013_SMEMA_下位1_通知Ready信號;

        //[DefineHardware] public IAnalogInputIO AI128000_頂升治具真空流量計; //類比

        //[DefineHardware] public IOutputIO Y065012_SMEMA_上位1_通知上位_Ready信號;
        //[DefineHardware] public IOutputIO Y065013_SMEMA_下位1_通知已放料信號;
        //[DefineHardware] public IOutputIO Y065010_靜電消除器_放電停止;

        //[DefineHardware] public IOutputIO Y067000_傳送流道_靠邊氣缸電磁閥;
        //[DefineHardware] public IOutputIO Y067001_傳送流道_到位氣缸電磁閥;
        //[DefineHardware] public IOutputIO Y067002_治具頂升氣缸;
        //[DefineHardware] public IOutputIO Y067002_傳送流道_頂升真空電磁閥;
        //#endregion




        #region Module Setting

        [ModuleProductSetting] public FlowCarrierProductParam ProductParam = new FlowCarrierProductParam();
        [ModuleGlobalSetting] public FlowCarrierGlobalParam Param = new FlowCarrierGlobalParam();

        #endregion

        #region Define Broadcast / Event

        [DefineBroadcast] public event EventHandler<RedLightOnArgs> NotifyRedLight;
        [DefineBroadcast] public event EventHandler<OrangeLightOnArgs> NotifyOrangeLight;
        [DefineBroadcast] public event EventHandler<GreenLightOnArgs> NotifyGreenLight;
        [DefineBroadcast] public event EventHandler<VirtualArgs> NotifyVirtual;
        [DefineEvent] public event EventHandler<BoatCarrier> NotifyVision;
        [DefineEvent] public event EventHandler<BoatCarrier> GetTrayLayot;
        public event EventHandler<IModule> SaveGlobalParam;
        public event EventHandler<IModule> SaveProductParam;

        #endregion


        #region Define Broadcast/Event Receive

        [DefineEventReceive]
        public void GetVisionFinish(object sender, BoatCarrier e)
        {
            BoatCarrier1 = e;
            IsVisionFinishQueue.Add(true);
        }


        [DefineBroadcastReceive]
        public void GetVisionByPass(object sender, VisionByPassArgs e)
        {
            _visionIsByPass = e.IsByPass;
        }

        [DefineBroadcastReceive]
        public void GetGoldenModel(object sender, GoldenModelArgs e)
        {
            _isGolden = e.IsGolden;
        }

        [DefineBroadcastReceive]
        public void GetMachineState(object sender, MachineStateArgs args)
        {
            if (args._AxisState== AxisState.Virtual)
            {
                bVirtual = true;
            }
        }
        /// <summary>
        /// 接收廣播，入該軸定問位置之內容(For 軸干涉用 AxisContrl)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [DefineBroadcastReceive]
        public void GetFMotorOffset(object sender, FlowMotorOffset args)
        {
            args.InspectPosition = FlowMotorOffset.InspectPosition;
            args.LoadPosition = FlowMotorOffset.LoadPosition;
            args.UnloadPosition = FlowMotorOffset.UnloadPosition;

        }
        [DefineBroadcastReceive]
        public void GetVisionSpendTime(object sender, SpendTimeArgs e)
        {
            InspectSpendTime = e.Time;
            UPHClass1.SpendTime += InspectSpendTime;
            UPHClass1.InspectionSpendTime += InspectSpendTime;
            UPHClass1.TotalCount = e.TotalCount;
            UPHClass1.Update();
        }
        #endregion


        [ModuleInitialFunction(11, -1)]
        public void InitFunc()
        {
            var myIP = "192.168.70.115";
            // 1. 初始化環境
            var dbIp = "127.0.0.1";
            var dbPort = "27017";
            var dbName = "aoi_db";
            //_BigMapServerDB = new BigMapServer(dbIp);// (dbIp, dbPort, dbName);
            //// 2. 啟動服務 WcfDuplexServer
            //doServer(_BigMapServerDB);
            //doClient(_BigMapServerDB);

        }
        //public void doServer(BigMapServer _BigMapServerDB)
        //{
        //    wcfDuplexServer = new WcfDuplexServer<IBigMapServer>(_BigMapServerDB, 50005, 3000, 3000);
        //    wcfDuplexServer.ClientDisconnect += (s, ee) =>
        //    {
        //        Console.WriteLine($"[Disconnected] {"ClientDisconnect"}");
        //    };
        //    wcfDuplexServer.Open();
        //}
        //public void doClient(BigMapServer _BigMapServerDB)
        //{
        //    client = new BigMapClient(_BigMapServerDB);
        //    client = iniClien(client);
        //}
        //private static BigMapClient iniClien(BigMapClient client)
        //{
        //    client.NotifyReceived += msg =>
        //    {
        //        Console.WriteLine($"NotifyReceived: {msg}");
        //    };

        //    client.DataReceived += data =>
        //    {
        //        Console.WriteLine($"DataReceived: {data}");
        //    };

        //    client.RecheckRequestReceived += req =>
        //    {
        //        Console.WriteLine($"RecheckRequestReceived req: {req}");
        //    };

        //    client.RecheckSingleResultReceived += msg =>
        //    {
        //        Console.WriteLine($"RecheckSingleResultReceived => Lot={msg.LotName}, Map={msg.MapIndex}, Mag={msg.MagzIndex}, Tray={msg.TrayIndex}, Row={msg.Row}, Col={msg.Col}, Result={msg.Result}");
        //    };

        //    client.RecheckSingleFinalResultReceived += msg =>
        //    {
        //        Console.WriteLine($"RecheckSingleFinalResultReceived => Lot={msg.LotName}, Mag={msg.MagzIndex}, Tray={msg.TrayIndex}, Row={msg.Row}, Col={msg.Col}, Result={msg.Result}");
        //    };

        //    client.RelayReceived += msg =>
        //    {
        //        Console.WriteLine($"RelayReceived from {msg.FromClientId}: {msg.Text}");
        //    };

        //    client.ErrorOccurred += (source, ex) =>
        //    {
        //        Console.WriteLine($"ErrorOccurred [{source}] {ex}");
        //    };
        //    return client;
        //}
        #region  Form CDI2.0

        [ModuleProductSettingForm]
        public object GetProductSettingForm()
        {
            FlowCarrierProductSetting frm = new FlowCarrierProductSetting(ProductParam);
            return frm;
        }

        [ModuleGlobalSettingForm]
        public object GetGlobalSettingForm()
        {
            FlowCarrierGlobalSetting frm = new FlowCarrierGlobalSetting(Param);
            return frm;
        }


        //有按鈕可以直接拿料跑視覺
        [GroupDisplay("ForTest", "流道測試", "")]
        public void AddTray()
        {
            BoatCarrier1.IsVirtual = true;
            NotifyVirtual?.Invoke(this, new VirtualArgs() { IsVirtual = true });
            //this.Start();
        }



        [GroupDisplay(nameof(Resources.ClassifyTool), nameof(Resources.ConnectionForm), "")]
        public void ShowConnectionStateForm()
        {
            var service = this.GetHtaService<IDockPanelService>();
            service.Show(this, nameof(ConnectionStateForm));
        }

        private TCPIPComForm _tcpipForm;
        [GroupDisplay(nameof(Resources.ClassifyTool), nameof(Resources.TCPIPForm), "")]
        public void TCPIPForm()
        {
            if (_tcpipForm == null)
            {
                var mdiService = this.GetHtaService<IMdiService>();
                _tcpipForm = (TCPIPComForm)mdiService.ShowMdi(this, typeof(TCPIPComForm), new Point(0, 0), new Size(0, 0), new object[] { this });
                _tcpipForm.FormClosed += (ss, ee) =>
                {
                    _tcpipForm = null;
                };
            }
        }

        //private VacuumSettingForm _vacuumSettingForm;
        //[GroupDisplay(nameof(Resources.ClassifyTool), nameof(Resources.VacuumSettingForm), "")]
        //public void ShowVacuumSettingForm()
        //{
        //    if (_vacuumSettingForm == null)
        //    {
        //        var mdiService = this.GetHtaService<IMdiService>();
        //        _vacuumSettingForm = (VacuumSettingForm)mdiService.ShowMdi(this, typeof(VacuumSettingForm), new Point(0, 0), new Size(0, 0), new object[] { this });
        //        _vacuumSettingForm.FormClosed += (ss, ee) =>
        //        {
        //            _vacuumSettingForm = null;
        //        };
        //    }
        //}


        #endregion

        /// <summary>
        /// 建構子
        /// </summary>
        public FlowCarrierModule() : base()
        {
            InitializeComponent();
            this.ModuleStarted += FlowCarrierModule_ModuleStarted;
            this.ModuleStopped += FlowCarrierModule_ModuleStopped;       
        }

        private void FlowCarrierModule_ModuleStarted(object sender, object e)
        {
            _isInStart = true;
            //CDI執行 通知上下游
            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.TCPIP && Param.UseInterLock)
            {
                if (!TCPIPTool.TcpServer.IsOnline)
                {
                    TCPIPTool.TcpServer.OnLine();
                }

                if (!Rcv_Server_flag)
                {
                    byte[] bytes = new byte[8];

                    //server
                    if (IDNum_server == 255)
                        IDNum_server = 0;

                    IDNum_server++;
                    TCPIPTool.ServerSendMessage(CmdEm.cmdRun, TypeEm.cmdRequest, bytes, 0, (byte)IDNum_server, 0, 8);
                }
                Rcv_Server_flag = false;
            }

            if (Param.LowerDeliveryType == MachineDeliveryTypeEm.TCPIP && Param.UseInterLock)
            {
                if (!TCPIPTool.TcpClient.IsConnect)
                {
                    //TCPIPTool.TcpClient.Connect();
                    TCPIPTool.ClientConnect(Param.TCPIPClientIP, Param.TCPIPClientPort);

                    if (Param.EnableOnlyDataTCPIPClient)
                    {
                        TCPIPTool.OnlyDataClientConnect(Param.TCPIPClientIP, Param.OnlyDataTCPIPClientPort);
                        //TCPIPTool.OnlyDataClientSendData("BoatBarcode:1dwe54,"); //範例
                    }
                }

                if (!Rcv_Client_flag)
                {
                    byte[] bytes = new byte[8];

                    //client
                    if (IDNum_client == 255)
                        IDNum_client = 0;

                    IDNum_client++;
                    TCPIPTool.ClientSendMessage(CmdEm.cmdRun, TypeEm.cmdRequest, bytes, 0, (byte)IDNum_client, 0, 8);
                }
                Rcv_Client_flag = false;
            }
        }

        public void FlowCarrierModule_ModuleStopped(object sender, object args)
        {
            //CDI暫停 通知上下游
            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.TCPIP && Param.UseInterLock)
            {
                if (!Rcv_Server_flag)
                {
                    byte[] bytes = new byte[8];

                    //server
                    if (IDNum_server == 255)
                        IDNum_server = 0;

                    IDNum_server++;
                    TCPIPTool.ServerSendMessage(CmdEm.cmdStop, TypeEm.cmdRequest, bytes, 0, (byte)IDNum_server, 0, 8);
                }
                Rcv_Server_flag = false;
            }

            if (Param.LowerDeliveryType == MachineDeliveryTypeEm.TCPIP && Param.UseInterLock)
            {
                if (!Rcv_Client_flag)
                {
                    byte[] bytes = new byte[8];

                    //client
                    if (IDNum_client == 255)
                        IDNum_client = 0;

                    IDNum_client++;
                    TCPIPTool.ClientSendMessage(CmdEm.cmdStop, TypeEm.cmdRequest, bytes, 0, (byte)IDNum_client, 0, 8);
                }
                Rcv_Client_flag = false;
            }
        }



        #region SecsGem
        [SecsSV("SV_LoadTimeout", true, false)]
        public int LoadTimeout
        {
            get => ProductParam.LoadTimeout;
            set
            {
                if (ProductParam.LoadTimeout == value)
                    return;
                ProductParam.LoadTimeout = value;
            }
        }

        [SecsSV("SV_StopTimeout", true, false)]
        public int StopTimeout
        {
            get => ProductParam.StopTimeout;
            set
            {
                if (ProductParam.StopTimeout == value)
                    return;
                ProductParam.StopTimeout = value;
            }
        }

        [SecsSV("SV_DecTimeout", true, false)]
        public int DecTimeout
        {
            get => ProductParam.DecTimeout;
            set
            {
                if (ProductParam.DecTimeout == value)
                    return;
                ProductParam.DecTimeout = value;
            }
        }

        [SecsSV("SV_UnloadTimeout", true, false)]
        public int UnloadTimeout
        {
            get => ProductParam.UnloadTimeout;
            set
            {
                if (ProductParam.UnloadTimeout == value)
                    return;
                ProductParam.UnloadTimeout = value;
            }
        }

        [SecsSV("SV_ReturnTimeout", true, false)]
        public int ReturnTimeout
        {
            get => ProductParam.ReturnTimeout;
            set
            {
                if (ProductParam.ReturnTimeout == value)
                    return;
                ProductParam.ReturnTimeout = value;
            }
        }

        [SecsSV("SV_CylinderTimeout", true, false)]
        public int CylinderTimeout
        {
            get => ProductParam.CylinderTimeout;
            set
            {
                if (ProductParam.CylinderTimeout == value)
                    return;
                ProductParam.CylinderTimeout = value;
            }
        }

        [SecsSV("SV_FlowVel", true, false)]
        public int FlowVel
        {
            get => (int)ProductParam.FlowVel;
            set
            {
                if ((int)ProductParam.FlowVel == value)
                    return;
                ProductParam.FlowVel = (MoveVelEm)value;
            }
        }

        [SecsAlarmContainer("Alarm_FlowCarrier_LoadTimeout", "Alarm_FlowCarrier_DecTimeout",
            "Alarm_FlowCarrier_StopTimeout", "Alarm_FlowCarrier_OutProduct")]
        public event EventHandler AnyTypeAlram;

        [SecsEventContainer("Event_FlowCarrier_LoadProduct_Start", "Event_FlowCarrier_LoadProduct_End",
            "Event_FlowCarrier_OutProduct_Start", "Event_FlowCarrier_OutProduct_End")]
        public event EventHandler SendEvent;


        #endregion


        #region Funtion

        public override void MachineUiReady()
        {
            base.MachineUiReady();
            _showWorkLogService = this.GetHtaService<IShowWorkLogService>();
            _showWorkLogService.ShowWorkLog(this);
        }


        public void LogTrace(string msg)
        {
            OnAddTrace("FlowCarrierModule", $@"{msg}");
            string timeStr = DateTime.Now.ToString("HH:mm:ss") + " : ";
            _showWorkLogService?.AddWorkLog(this, timeStr + msg);
        }

        protected override void ModuleBase_Disposed(object sender, EventArgs e)
        {
            base.ModuleBase_Disposed(sender, e);
            TCPIPTool.ServerDispose();
            TCPIPTool.ClientDispose();
        }

        public override void MachineControllerReady()
        {
            base.MachineControllerReady();

            var velService = this.GetHtaService<IAxisConfigVelocityService>();

            velService.DataSourceChanged += VelService_DataSourceChanged;

            //獲取所需軸的所有速度值
     
            InspectVelList = velService.QueryVelData("AY1");
            //_InspectVelocity = (GetVelocity(this, 視覺縱移軸, TVelEnum.Slow).Velocity);// velService.QueryVelData(視覺縱移軸.Name);
            FlowVelList = velService.QueryVelData(BX2_流道傳送軸.Name);

            MachineSimpleController = this.GetHtaService<IMachineSimpleController>();

            TCPIPTool.UpdateModule(this);

            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.TCPIP)
            {
                TCPIPTool.ServerOnline(Param.TCPIPServerPort);
                if (Param.EnableOnlyDataTCPIPServer)
                    TCPIPTool.OnlyDataServerOnline(Param.OnlyDataTCPIPServerPort);
            }
            if (Param.LowerDeliveryType == MachineDeliveryTypeEm.TCPIP)
            {
                TCPIPTool.ClientConnect(Param.TCPIPClientIP, Param.TCPIPClientPort);
                if (Param.EnableOnlyDataTCPIPClient)
                    TCPIPTool.OnlyDataClientConnect(Param.TCPIPClientIP, Param.OnlyDataTCPIPClientPort);
            }

            

        }

        private void VelService_DataSourceChanged(object sender, EventArgs e)
        {
            var velService = sender as IAxisConfigVelocityService;

            InspectVelList = velService.QueryVelData("視覺縱移軸"); //InspectVelList = velService.QueryVelData(視覺縱移軸.Name);
            //_InspectVelocity = (GetVelocity(this, 視覺縱移軸, TVelEnum.Slow).Velocity);// velService.QueryVelData(視覺縱移軸.Name);

            FlowVelList = velService.QueryVelData(BX2_流道傳送軸.Name);
        }

        public void Init()
        {
            var visionQueueCount = IsVisionFinishQueue.Count;
            for (int i = 0; i < visionQueueCount; i++)
            {
                IsVisionFinishQueue.Take();
            }
            var alarmQueueCount = AlarmQueue.Count;
            for (int i = 0; i < alarmQueueCount; i++)
            {
                AlarmQueue.Take();
            }
            var upperPutQueueCount = UpperPutQueue.Count;
            for (int i = 0; i < upperPutQueueCount; i++)
            {
                UpperPutQueue.Take();
            }
            var lowerReadyQueueCount = LowerReadyQueue.Count;
            for (int i = 0; i < lowerReadyQueueCount; i++)
            {
                LowerReadyQueue.Take();
            }
            CanPutProduct = false;
        }

        public void OnSaveGlobalParam(object sender, IModule module)
        {
            SaveGlobalParam?.Invoke(sender, module);
        }


        private bool CheckVecuumState(bool _Virtual) 
        {
            if (_Virtual || ProductParam.VCCheck_Bypass_Pick || ProductParam.VC_Bypass_Pick) { return true; }
            else { return AI128000_頂升治具真空流量計.GetValue() < Param.CVBoatVacuum_EstablishedValue; } 
        }



        #endregion



        private void FlowCarrier_Start_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            TCPIPTool.ClearList();
        }

        private void CV狀態確認_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (_isGolden)
            {
                while (!e.Executor.CancelToken.IsCancellationRequested)
                {
                    SpinWait.SpinUntil(() => false, 100);
                }
                e.ProcessSuccess = false;
                return;
            }
        }

        private void CV判定有無Boat_Check_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace("--流道上是否有載盤_ConditionCheck--");
            Init();
            if (bVirtual)
            {
                X066000_傳送流道_入料銜接檢知_上下對照.SetIO(false);
                X066007_傳送流道_出料銜接檢知_側邊反射.SetIO(false);
                X066003_傳送流道_產品減速檢知.SetIO(false);
                X066004_傳送流道_產品到位檢知.SetIO(false);
                X066002_傳送流道_產品位置檢知.SetIO(false);
            }

            if (X066003_傳送流道_產品減速檢知.CheckIO() || X066004_傳送流道_產品到位檢知.CheckIO()
                || X066002_傳送流道_產品位置檢知.CheckIO())
            {
                LogTrace("--流道上有載盤");
                e.Result = true;
            }
            else
            {
                LogTrace("--流道上沒有載盤");
                e.Result = false;
                //e.Result = true; //測試用
            }
        }

        private void CVYA_移動_Load定位點_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--移動到入料位置_ProcessIn--");

            if (bVirtual)
            {
                X066000_傳送流道_入料銜接檢知_上下對照.SetIO(false);
                X066007_傳送流道_出料銜接檢知_側邊反射.SetIO(false);
            }
            if (_isGolden)  //判斷有無 Golden Mode
            {
                while (!e.Executor.CancelToken.IsCancellationRequested) //IsCancellationRequested 若CDI2.0 暫停觸發，跳出迴圈
                {
                    SpinWait.SpinUntil(() => false, 100);  //等待時間
                }
                e.ProcessSuccess = false;
                return;
            }
            //tcpip上線
            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.TCPIP)
            {
                TCPIPTool.ServerOnline(Param.TCPIPServerPort);
                if (Param.EnableOnlyDataTCPIPServer)
                    TCPIPTool.OnlyDataServerOnline(Param.OnlyDataTCPIPServerPort);
            }
            if (Param.LowerDeliveryType == MachineDeliveryTypeEm.TCPIP)
            {
                TCPIPTool.ClientConnect(Param.TCPIPClientIP, Param.TCPIPClientPort);
                if (Param.EnableOnlyDataTCPIPClient)
                    TCPIPTool.OnlyDataClientConnect(Param.TCPIPClientIP, Param.OnlyDataTCPIPClientPort);
            }


            var vel = FlowVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());

            bool bMoveSuccessY = BX1_流道橫移軸.ActualPos.Equals(FlowMotorOffset.LoadPosition) ? true : BX1_流道橫移軸.AbsoluteMove(FlowMotorOffset.LoadPosition, vel.Velocity, 10000);


            //var res = BX2_流道傳送軸.AbsoluteMove(FlowMotorOffset.LoadPosition, vel.Velocity,10000);
            var waitRes = bMoveSuccessY?true: BX1_流道橫移軸.WaitMotionDone();
            
            if (bMoveSuccessY && waitRes)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
                LogTrace($"移動到入料位置 失敗，移動:{bMoveSuccessY},等待:{waitRes}");
            }
        }

        private void 發送_上位Load_Ready_Flg_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

            LogTrace("--通知上位可以入料_ProcessIn--");
            if (Math.Abs(BX1_流道橫移軸.ActualPos - FlowMotorOffset.LoadPosition) > 0.1)
            {
                LogTrace($"有被移動過，所以就在移動一次到入料位置");
                var vel = InspectVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
                var res = BX1_流道橫移軸.AbsoluteMove(FlowMotorOffset.LoadPosition, vel.Velocity);
                var waitRes = BX1_流道橫移軸.WaitMotionDone();
                if (res && waitRes)
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                    LogTrace($"移動到入料位置 失敗，移動:{res},等待:{waitRes}");
                    return;
                }
            }

            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.SMEMA)
            {
                Y065012_SMEMA_上位1_通知上位_Ready信號.SetIO(true);
            }
            e.ProcessSuccess = true;
        }

        private void 取得_上位Load_OK_Flg_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待載盤入料與上位SMEMA訊號_ProcessIn--");
            if (bVirtual)
            {
                X064012_SMEMA_上位1_通知已放料信號.SetIO(true);
            }

            if (Math.Abs(視覺縱移軸.ActualPos - FlowMotorOffset.LoadPosition) > 0.1)
            {
                LogTrace($"目前不在入料位置，須移至入料位置");
                var vel = InspectVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
                var res = 視覺縱移軸.AbsoluteMove(FlowMotorOffset.LoadPosition, vel.Velocity);
                var waitRes = 視覺縱移軸.WaitMotionDone();
                if (res && waitRes)
                {
                    
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                    LogTrace($"移動到入料位置 失敗，移動:{res},等待:{waitRes}");
                    return;
                }
            }


            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.NoUse)
            {
                e.ProcessSuccess = true;
                return;
            }
            else
            {
                if (Param.UpperDeliveryType == MachineDeliveryTypeEm.SMEMA)
                {
                    SpinWait.SpinUntil(() => X064012_SMEMA_上位1_通知已放料信號.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, -1);
                }
                else
                {
                    CanPutProduct = true;
                    bool res = false;
                    while (!e.Executor.CancelToken.IsCancellationRequested)
                    {
                        if (UpperPutQueue.Count > 0)
                        {
                            UpperPutQueue.Take();
                            res = true;
                            break;
                        }
                        SpinWait.SpinUntil(() => false, 100);
                    }
                    if (res)
                    {
                        e.ProcessSuccess = true;
                        return;
                    }
                    if (e.Executor.CancelToken.IsCancellationRequested)
                    {
                        e.ProcessSuccess = false;
                        return;
                    }
                }
                e.ProcessSuccess = true;
            }


            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
                return;
            }

            e.ProcessSuccess = true;
        }

        private void 取得_上位SMEMA_Flg_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

        }

        private void CVYA_移動_Detection定位點_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

            LogTrace("--移動到檢測位置_ProcessIn--");

            if (bVirtual)
            {
                X066000_傳送流道_入料銜接檢知_上下對照.SetIO(false);
                X066007_傳送流道_出料銜接檢知_側邊反射.SetIO(false);
                e.ProcessSuccess = true;

            }
            else
            {

                var vel = InspectVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
                var res = 視覺縱移軸.AbsoluteMove(FlowMotorOffset.InspectPosition, vel.Velocity);
                var waitRes = 視覺縱移軸.WaitMotionDone();
                if (res && waitRes)
                {
                    e.ProcessSuccess = true;
                    //這裡才+1 BoatCarrier1.Count ，因為到這裡才真正要檢測
                    GetTrayLayot?.Invoke(this, BoatCarrier1);
                }
                else
                {
                    e.ProcessSuccess = false;
                    LogTrace("移動到檢測位置 失敗");
                }
            }
            

            //var vel = InspectVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
            //var res = 視覺縱移軸.AbsoluteMove(FlowMotorOffset.InspectPosition, vel.Velocity);
            //var waitRes = 視覺縱移軸.WaitMotionDone();
            //if (res && waitRes)
            //{
            //    e.ProcessSuccess = true;
            //}
            //else
            //{
            //    e.ProcessSuccess = false;
            //    LogTrace("移動到檢測位置 失敗");
            //}


            e.ProcessSuccess = true;

        }

        private void BoatUnload判定_Check_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace("--詢問是否要將產品出料_ConditionCheck--");
            var service = this.GetHtaService<IDialogService>();
            var result = service.ShowDialog(this, new ShowDialogArgs()
            {
                Button = MessageBoxButtons.YesNo,
                Message = "是否要將產品出料?",
            });

            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.TCPIP ||
                Param.LowerDeliveryType == MachineDeliveryTypeEm.TCPIP)
            {
                var result2 = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.YesNo,
                    Message = "是否使用舊資料?",
                });

                if (result2 == DialogResult.Yes)
                {
                    LogTrace("是否使用舊資料? 使用者選擇Yes");
                    var bytes = TCPIPTool.ReadByteData();
                    TCPIPTool.DeliveryBoats.Add(bytes);
                    LogTrace("加入一個舊資料");
                }
            }

            if (result == DialogResult.Yes)
            {
                LogTrace("--使用者選擇Yes--");
                e.Result = true;
            }
            else
            {
                LogTrace("--使用者選擇No--");
                e.Result = false;
            }
        }

        private void CV_含Boat_SubLoop_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--流道有料子流程_ProcessIn--");
            Y065012_SMEMA_上位1_通知上位_Ready信號.SetIO(false);
        }

        private void 發出_CVYADetection定位點_允許開始檢測_Ok_Flg_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--通知檢測模組入料完成_ProcessIn--");
            NotifyVision?.Invoke(this, BoatCarrier1);
            LoadProductTime.Stop();
            var timeSpan = LoadProductTime.Elapsed;
            UPHClass1.SpendTime += timeSpan;
            UPHClass1.Update();
            LoadProductTime.Reset();
        }

        private void 取得_CVYADetection_OK_Flg_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待檢測完成_ProcessIn--");
            
           // if (bVirtual)
           // {
           //     e.ProcessSuccess = true;
           //     return;
           //}


            bool res = false;
            while (!e.Executor.CancelToken.IsCancellationRequested)
            {
                if (IsVisionFinishQueue.Count > 0)
                {
                    IsVisionFinishQueue.Take();
                    res = true;
                    break;
                }
                SpinWait.SpinUntil(() => false, 100);
            }

            //e.Executor.TakeFromQueue(IsVisionFinishQueue, -1, out bool res);
            if (res == false)
            {
                e.ProcessSuccess = false;
                return;
            }
        }

        private void CVYA_移動_Unload定位點_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--移動到出料位置_ProcessIn--");
            OutProductTime.Start();
            if (bVirtual)
            {
                X066000_傳送流道_入料銜接檢知_上下對照.SetIO(false);
                X066007_傳送流道_出料銜接檢知_側邊反射.SetIO(false);
                e.ProcessSuccess = true;
            }
            else
            {
                if (BoatCarrier1.IsRecheckTakeout)
                {
                    LogTrace("確認產品已被取走，流程結束");
                    e.ProcessSuccess = true;
                    return;
                }


                var vel = FlowVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());

                bool res = BX2_流道傳送軸.ActualPos.Equals(FlowMotorOffset.UnloadPosition) ? true : BX2_流道傳送軸.AbsoluteMove(FlowMotorOffset.UnloadPosition, vel.Velocity);

                var waitRes = BX2_流道傳送軸.WaitMotionDone();



                if (res && waitRes)
                {
                    if (Math.Abs(BX2_流道傳送軸.ActualPos - FlowMotorOffset.UnloadPosition) > 0.5)
                    {
                        e.ProcessSuccess = false;
                        LogTrace($"移動到出料位置 未到{FlowMotorOffset.UnloadPosition}");
                        return;
                    }
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                    LogTrace("移動到出料位置 失敗");
                }
            }

            
        }

        private void CVRA_停止汽缸_Up_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--開啟檔料汽缸_ProcessIn--");
            
            SendEvent?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Event_FlowCarrier_LoadProduct_Start" });

            if (bVirtual)
            {
                X066006_傳送流道_到位氣缸_下降.SetIO(false);
            }

            if (ProductParam.StopSV_Bypass_Pick)
            {
                e.ProcessSuccess = true;
                return ;
            }


            Y067001_傳送流道_到位氣缸電磁閥.SetIO(true);
            if (SpinWait.SpinUntil(() =>
            !X066006_傳送流道_到位氣缸_下降.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout*1000))
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 等待_CVRA_Load檢知_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待通過入料Sensor_ProcessIn--");
            if (bVirtual)
            {
                X066000_傳送流道_入料銜接檢知_上下對照.SetIO(true);
                X066001_傳送流道_入料銜接檢知_側邊反射.SetIO(true);
            }

            bool isOK = SpinWait.SpinUntil(() =>( X066000_傳送流道_入料銜接檢知_上下對照.CheckIO() && X066001_傳送流道_入料銜接檢知_側邊反射.CheckIO()) || e.Executor.CancelToken.IsCancellationRequested, ProductParam.LoadTimeout * 1000);
            if (isOK)
            {
                CanPutProduct = false;
                e.ProcessSuccess = true;
            }
            else
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    AlarmQueue.Add(true);
                }
                else
                {
                    AlarmQueue.Add(false);
                }
                e.ProcessSuccess = false;
                return;
            }
        }

        private void CVRA_啟動_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--流道開始轉動_ProcessIn--");
            var vel = FlowVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
            BX2_流道傳送軸.ConstantMove(vel.Velocity, TMotionDir.Forward);
            if (bVirtual)
            {
                Y067012_靜電消除器_吹氣啟動.SetIO(true);
            }
            
           Y067012_靜電消除器_吹氣啟動.SetIO(true);
            
            e.ProcessSuccess = true;
        }

        private void 等待_CVRA_低速檢知_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待通過減速Sensor_ProcessIn--");
            if (bVirtual)
            {
                X066003_傳送流道_產品減速檢知.SetIO(true);
            }

            if (!BX2_流道傳送軸.IsRunning())
            {
                var vel = FlowVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
                BX2_流道傳送軸.ConstantMove(vel.Velocity, TMotionDir.Forward);
            }


            if (SpinWait.SpinUntil(() =>
             X066003_傳送流道_產品減速檢知.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.DecTimeout * 1000))
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    AlarmQueue.Add(true);
                    e.ProcessSuccess = false;
                    return;
                }

                //BX2_流道傳送軸.Stop();
                //SpinWait.SpinUntil(() => false, 500);
                
                if (!bVirtual)
                {
                    var vel2 = FlowVelList.FirstOrDefault(x => x.VelocityName == MoveVelEm.VerySlow.ToString());
                    BX2_流道傳送軸.ConstantMove(vel2.Velocity, TMotionDir.Forward);
                }

                e.ProcessSuccess = true;
            }
            else
            {
                AlarmQueue.Add(false);
                e.ProcessSuccess = false;
            }
        }

        private void 等待_CVRA_到位檢知_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待通過到位Sensor_ProcessIn--");
            if (bVirtual)
            {
                X066004_傳送流道_產品到位檢知.SetIO(true);
            }

            if (SpinWait.SpinUntil(() =>
            X066004_傳送流道_產品到位檢知.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.StopTimeout * 1000))
            {
                SpinWait.SpinUntil(() => false, 500);
                BX2_流道傳送軸.Stop();
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    AlarmQueue.Add(true);
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = true;
            }
            else
            {
                AlarmQueue.Add(false);
                e.ProcessSuccess = false;
            }
        }

        private void 流道傳送軸_Guide動作_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--流道傳送軸_Guide動作_STP_ProcessIn--");
            if (bVirtual)
            {
                Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
                Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
                // e.ProcessSuccess = true;
            }

            if (ProductParam.GuideSV_Bypass_Pick)
            {
                e.ProcessSuccess = true;
                return;
            }


            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
            SpinWait.SpinUntil(() => false, 500);
            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);
            SpinWait.SpinUntil(() => false, 100);
            Y067012_靜電消除器_吹氣啟動.SetIO(false);
            e.ProcessSuccess = true;
          
        }



        private void 入料_CVRA_真空開啟_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--開啟真空汽缸_ProcessIn--");
            if (bVirtual)
            {
                Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
            }
            if (ProductParam.VC_Bypass_Pick)
            {
                e.ProcessSuccess = true;
                return;
            }
            else
            {
                Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
            }

            if (SpinWait.SpinUntil(() => ProductParam.VCCheck_Bypass_Pick || CheckVecuumState(bVirtual) || e.Executor.CancelToken.IsCancellationRequested, ProductParam.VacuumTimeout * 1000))
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 入料_CVRA_頂升汽缸開啟_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--治具頂升氣缸_上升_ProcessIn--");
            if (bVirtual)
            {
               // X066007_治具頂升氣缸_上升.SetIO(false);
            }

            if (ProductParam.TopSV_Bypass_Pick)
            {
                e.ProcessSuccess = true;
                return;
            }

            //Y067002_治具頂升氣缸.SetIO(true);
            //if (SpinWait.SpinUntil(() => !X066006_傳送流道_到位氣缸_下降.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout))
            if(true)// (SpinWait.SpinUntil(() => !X066007_治具頂升氣缸_上升.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.TopSVTimeout * 1000))
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void CVRA_入料動作_OK_END_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--入料成功_ProcessIn--");
            //Y067000_靠邊汽缸打出.SetIO(true);
            if (Param.UpperDeliveryType == MachineDeliveryTypeEm.SMEMA)
            {
                Y065012_SMEMA_上位1_通知上位_Ready信號.SetIO(false);
            }
            else if (Param.UpperDeliveryType == MachineDeliveryTypeEm.TCPIP)
            {
                byte[] bytes = new byte[10];
                bytes[0] = (byte)TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count - 1][0];
                bytes[1] = (byte)TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count - 1][1];
                bytes[2] = (byte)CmdEm.cmdGotBoat;
                bytes[3] = (byte)TypeEm.cmdRequest;
                bytes[4] = 2;

                TCPIPTool.ServerSendMessage(CmdEm.cmdGotBoat, TypeEm.cmdRequest,
                    bytes,
                    2,
                    bytes[0],
                    bytes[1],
                    10);
            }
            else
            {
                LogTrace("--入料成功_:"+Param.UpperDeliveryType);
            }


            SendEvent?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Event_FlowCarrier_LoadProduct_End" });

        }

        private void 等待_CVRA_Load檢知逾時_Err_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待_CVRA_Load檢知逾時_Err_ProcessIn--");
            var executor = (IControlFlowExecutor)e.Executor;
            if (executor.TakeFromQueue<bool>(AlarmQueue, -1, out var res))
            {
                e.ProcessSuccess = res;
                if (res == true)
                {
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_FlowCarrier_LoadTimeout", AlarmActive = false });
                }
                //NotifyGreenLight?.Invoke(this, new GreenLightOnArdgs() { IsOn = true });
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 等待_CVRA_低速檢知逾時_Err_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待_CVRA_低速檢知逾時_Err_ProcessIn--");
            var executor = (IControlFlowExecutor)e.Executor;
            if (executor.TakeFromQueue<bool>(AlarmQueue, -1, out var res))
            {
                e.ProcessSuccess = res;

                if (res == true)
                {
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_FlowCarrier_DecTimeout", AlarmActive = false });
                }

                //NotifyGreenLight?.Invoke(this, new GreenLightOnArdgs() { IsOn = true });
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 等待_CVRA_到位檢知逾時_Err_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待_CVRA_到位檢知逾時_Err_ProcessIn--");
            var executor = (IControlFlowExecutor)e.Executor;
            if (executor.TakeFromQueue<bool>(AlarmQueue, -1, out var res))
            {
                e.ProcessSuccess = res;

                if (res == true)
                {
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_FlowCarrier_StopTimeout", AlarmActive = false });
                }

                //NotifyGreenLight?.Invoke(this, new GreenLightOnArdgs() { IsOn = true });
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 入料_CVRA_真空開啟逾時_Err_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--入料_CVRA_真空開啟逾時_Err_ProcessIn--");
            var executor = (IControlFlowExecutor)e.Executor;
            if (executor.TakeFromQueue<bool>(AlarmQueue, -1, out var res))
            {
                e.ProcessSuccess = res;


                if (res == true)
                {
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_FlowCarrier_VacuumTimeout", AlarmActive = false });
                }

                //NotifyGreenLight?.Invoke(this, new GreenLightOnArdgs() { IsOn = true });
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 入料_CVRA_頂升汽缸開啟逾時_Err_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--入料_CVRA_頂升汽缸開啟逾時_Err_ProcessIn--");
            var executor = (IControlFlowExecutor)e.Executor;
            if (executor.TakeFromQueue<bool>(AlarmQueue, -1, out var res))
            {
                e.ProcessSuccess = res;

                if (res == true)
                {
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_FlowCarrier_TopSVTimeout", AlarmActive = false });
                }

                //NotifyGreenLight?.Invoke(this, new GreenLightOnArdgs() { IsOn = true });
            }
            else
            {
                e.ProcessSuccess = false;
            } 
        }

        private void 入料_CVRA_定位汽缸開啟逾時_Err_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--入料_CVRA_定位汽缸開啟逾時_Err_ProcessIn--");
            var executor = (IControlFlowExecutor)e.Executor;
            if (executor.TakeFromQueue<bool>(AlarmQueue, -1, out var res))
            {
                e.ProcessSuccess = res;
                //NotifyGreenLight?.Invoke(this, new GreenLightOnArdgs() { IsOn = true });
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 發送_下位SMEMA_Flg_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--發送SMEMA訊號給下位_ProcessIn--");
            if (Param.LowerDeliveryType == MachineDeliveryTypeEm.SMEMA)
            {
                Y065013_SMEMA_下位1_通知已放料信號.SetIO(true);
            }
            else if (Param.LowerDeliveryType == MachineDeliveryTypeEm.TCPIP)
            {
                if (IDNum_server == 255)
                    IDNum_server = 0;

                IDNum_server++;
                //紀錄ID
                IndexCount = IDNum_server;

                int length = 0;
                IndexCount = TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1][0];//把最新的Deliveryboat ID記錄下來
                if (Param.TCPIPUseXml)
                {
                    if (TCPIPTool.DeliveryBoats.Count() > 0)
                        //XML解析長度
                        length = (TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1][7] << 24) + (TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1][6] << 16) + (TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1][5] << 8) + TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1][4];
                }
                else
                {
                    if (TCPIPTool.DeliveryBoats.Count() > 0)
                        length = TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1][4];
                }

                while (!e.Executor.CancelToken.IsCancellationRequested)
                {
                    if (!TCPIPTool.TcpClient.IsConnect)
                    {
                        //TCPIPTool.TcpClient.Connect();
                        TCPIPTool.ClientConnect(Param.TCPIPClientIP, Param.TCPIPClientPort);

                        if (Param.EnableOnlyDataTCPIPClient)
                        {
                            TCPIPTool.OnlyDataClientConnect(Param.TCPIPClientIP, Param.OnlyDataTCPIPClientPort);
                            TCPIPTool.OnlyDataClientSendData(TATool.BoatBarcode);//發送Barcode給下位
                        }
                    }

                    if (length == 0)
                    {
                        byte[] bytes = new byte[145];
                        bytes[0] = (byte)TCPIPTool.CurrentID1;
                        bytes[1] = (byte)TCPIPTool.CurrentID2;
                        bytes[2] = (byte)CmdEm.cmdDeliverBoat;
                        bytes[3] = (byte)TypeEm.cmdRequest;
                        bytes[4] = 137;
                        TCPIPTool.ClientSendMessage(CmdEm.cmdDeliverBoat, TypeEm.cmdRequest, bytes, 137, bytes[0], bytes[1], 137 + 8);
                    }
                    else
                    {
                        byte[] bytes = new byte[TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1].Length];
                        Array.Copy(TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1], bytes, TCPIPTool.DeliveryBoats[TCPIPTool.DeliveryBoats.Count-1].Length);

                        TCPIPTool.ClientSendMessage(CmdEm.cmdDeliverBoat,
                            TypeEm.cmdRequest,
                            bytes,
                            length,
                            bytes[0],
                            bytes[1],
                            length+8);
                    }

                    SpinWait.SpinUntil(() => false, 2000);
                    if (LowerReadyQueue.Count > 0)
                    {
                        break;//有收到下位的Ready信號
                    }
                }
                if (LowerReadyQueue.Count > 0)
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    //如果被按暫停會到這裡
                    e.ProcessSuccess = false;
                }

            }
        }

        private void 等待_下位SMEMA_Flg_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待下位SMEMA訊號_ProcessIn--");
             if (bVirtual)
            {
                X064013_SMEMA_下位1_通知Ready信號.SetIO(true);
            }

            if (Param.LowerDeliveryType == MachineDeliveryTypeEm.NoUse)
            {
                e.ProcessSuccess = true;
                return;
            }
            else
            {
                if (Param.LowerDeliveryType == MachineDeliveryTypeEm.SMEMA)
                {
                    SpinWait.SpinUntil(() => X064013_SMEMA_下位1_通知Ready信號.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, -1);
                }
                else
                {
                    bool res = false;
                    while (!e.Executor.CancelToken.IsCancellationRequested)
                    {
                        if (LowerReadyQueue.Count > 0)
                        {
                            LowerReadyQueue.Take();
                            res = true;
                            break;
                        }
                        SpinWait.SpinUntil(() => false, 100);
                    }
                    if (res)
                    {
                        e.ProcessSuccess = true;
                        return;
                    }
                }
            }

            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
                return;
            }
            e.ProcessSuccess = true;
        }

        private void 定位汽缸_關閉_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--定位汽缸_關閉_ProcessIn--");
            if (bVirtual)
            {
                X066006_傳送流道_到位氣缸_下降.SetIO(true);
            }

            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);
            //067001_流道到位氣缸.SetIO(false);
            //SpinWait.SpinUntil(() => X066006_傳送流道_到位氣缸_下降.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout);
            SpinWait.SpinUntil(() => X066006_傳送流道_到位氣缸_下降.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);
            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
            }
            else
            {
                if (X066006_傳送流道_到位氣缸_下降.CheckIO())
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                }
            }
        }

        private void 停止汽缸_下降_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--停止汽缸_下降_ProcessIn--");
            if (bVirtual)
            {
                X066005_傳送流道_到位氣缸_上升.SetIO(false);
                X066006_傳送流道_到位氣缸_下降.SetIO(true);
            }

            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);
            Y067001_傳送流道_到位氣缸電磁閥.SetIO(false);
            SpinWait.SpinUntil(() =>
            (!X066005_傳送流道_到位氣缸_上升.CheckIO() && X066006_傳送流道_到位氣缸_下降.CheckIO()) || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);

            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
            }
            else
            {
                bool bX066005 = X066005_傳送流道_到位氣缸_上升.CheckIO();
                bool bX066006 = X066006_傳送流道_到位氣缸_下降.CheckIO();

                if (!bX066005 && bX066006)
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    LogTrace($"X066005:{bX066005},X066006:{bX066006}");
                    e.ProcessSuccess = false;
                }
            }
        }

        private void 真空汽缸_關閉_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--真空汽缸_關閉_ProcessIn--");

             if (bVirtual)
            {
                e.ProcessSuccess = true; 
                
            }

            Y067002_傳送流道_頂升真空電磁閥.SetIO(false);

            if (SpinWait.SpinUntil(() => CheckVecuumState(bVirtual) ||  e.Executor.CancelToken.IsCancellationRequested, ProductParam.VacuumTimeout * 1000))
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void CV啟動出料_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--流道開始轉動出料_ProcessIn--");
             if (bVirtual)
            {
                X066007_傳送流道_出料銜接檢知_側邊反射.SetIO(true);
            }

            var vel = FlowVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
            BX2_流道傳送軸.ConstantMove(vel.Velocity, TMotionDir.Forward);

            if (SpinWait.SpinUntil(() => X066007_傳送流道_出料銜接檢知_側邊反射.CheckIO(), ProductParam.UnloadTimeout * 1000))
            {
                int noProductCount = 0;

                while (true)
                {
                    if (bVirtual)
                    {
                        X066007_傳送流道_出料銜接檢知_側邊反射.SetIO(false);
                        X066008_傳送流道_出料銜接檢知_上下對照.SetIO(false);
                    }

                    if (SpinWait.SpinUntil(() => (!X066007_傳送流道_出料銜接檢知_側邊反射.CheckIO()&& !X066008_傳送流道_出料銜接檢知_上下對照.CheckIO()),
                        ProductParam.UnloadTimeout * 1000))
                    {
                        noProductCount++;
                        SpinWait.SpinUntil(() => false, 100);
                    }
                    else
                    {
                        noProductCount = -1;
                        LogTrace("noProductCount = -1");
                        break;
                    }

                    if (noProductCount >= 15)
                    {
                        LogTrace("noProductCount >= 15");
                        break;
                    }
                }
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    LogTrace("被按暫停");
                    if (noProductCount >= 15)
                    {
                        LogTrace("出料成功");
                        BX2_流道傳送軸.Stop();
                        e.ProcessSuccess = true;
                        return;
                    }
                    else
                    {
                        AlarmQueue.Add(true);
                        e.ProcessSuccess = false;
                        return;
                    }
                }
                if (noProductCount == -1)
                {
                    //卡料
                    LogTrace("卡料 noProductCount == -1");
                    BX2_流道傳送軸.Stop();
                    AlarmQueue.Add(false);
                    e.ProcessSuccess = false;
                    return;
                }

                LogTrace("出料成功");
                BX2_流道傳送軸.Stop();
                e.ProcessSuccess = true;
                return;
            }
        }

        private void CVRA_出料動作_Err_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--出料失敗發出Alarm_ProcessIn--");
            var executor = (IControlFlowExecutor)e.Executor;
            if (executor.TakeFromQueue<bool>(AlarmQueue, -1, out var res))
            {
                e.ProcessSuccess = res;

                if (res == true)
                {
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_FlowCarrier_OutProduct", AlarmActive = false });
                }

                //NotifyGreenLight?.Invoke(this, new GreenLightOnArdgs() { IsOn = true });
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void CVRA_出料動作_OK_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--出料成功_ProcessIn--");
            Y065013_SMEMA_下位1_通知已放料信號.SetIO(false);
            BX2_流道傳送軸.Stop();
            SendEvent?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Event_FlowCarrier_OutProduct_End" });

        }

        private void CVRA_停止汽缸_下降_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--CVRA_停止汽缸_下降_ProcessIn--");
            if (bVirtual)
            {
                X066006_傳送流道_到位氣缸_下降.SetIO(true);
                Y067001_傳送流道_到位氣缸電磁閥.SetIO(true);
            }

            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);
            Y067001_傳送流道_到位氣缸電磁閥.SetIO(false);
            SpinWait.SpinUntil(() => X066006_傳送流道_到位氣缸_下降.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);

            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
            }
            else
            {
                if (X066006_傳送流道_到位氣缸_下降.CheckIO())
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                }
            }
        }

        private void CVRA_到位檢知_Check_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace("--CVRA_到位檢知_Check_ConditionCheck--");
            if (bVirtual)
            {
                X066004_傳送流道_產品到位檢知.SetIO(true) ;
            }

            if (X066004_傳送流道_產品到位檢知.CheckIO())
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void CVRA_反轉脫離動作_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--CVRA_反轉脫離動作_ProcessIn--");
            if (bVirtual)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                var velInspect = FlowVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
                BX2_流道傳送軸.ConstantMove(velInspect.Velocity, TMotionDir.Backward);
                SpinWait.SpinUntil(() => !X066004_傳送流道_產品到位檢知.CheckIO(), ProductParam.LoadTimeout * 1000);
                BX2_流道傳送軸.Stop();
            }
            
        }

        private void CVRA_正轉靠位動作_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--正轉到到位檢知_ProcessIn--");
            if (bVirtual)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                Y067001_傳送流道_到位氣缸電磁閥.SetIO(true);
                SpinWait.SpinUntil(() => false, 500);
                var velInspect = FlowVelList.FirstOrDefault(x => x.VelocityName == ProductParam.FlowVel.ToString());
                BX2_流道傳送軸.ConstantMove(velInspect.Velocity, TMotionDir.Forward);
                 //模擬測試用
                SpinWait.SpinUntil(() => X066004_傳送流道_產品到位檢知.CheckIO(), ProductParam.LoadTimeout * 1000); 
                SpinWait.SpinUntil(() => false, 1000);
                BX2_流道傳送軸.Stop();
            }

            
        }

        private void CV_含Boat_OK_END_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--CV_含Boat_OK_END_ProcessIn--");
        }

        private void CVRA_到位檢知脫離_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--CVRA_到位檢知脫離_ProcessIn--");
            if (bVirtual)
            {
                X066004_傳送流道_產品到位檢知.SetIO(false);
            }

            SpinWait.SpinUntil(() => !X066004_傳送流道_產品到位檢知.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);

            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
            }
            else
            {
                if (!X066004_傳送流道_產品到位檢知.CheckIO())
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                }
            }
        }

        private void CVRA_停止汽缸_上升_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--CVRA_停止汽缸_下降_ProcessIn--");
            if (bVirtual)
            {
                X066005_傳送流道_到位氣缸_上升.SetIO(true);
            }

            Y067001_傳送流道_到位氣缸電磁閥.SetIO(true);
            SpinWait.SpinUntil(() => X066005_傳送流道_到位氣缸_上升.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);

            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
            }
            else
            {
                if (X066005_傳送流道_到位氣缸_上升.CheckIO())
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                }
            }
        }

        private void 有料_CVRA_真空開啟_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--有料_CVRA_真空開啟_ProcessIn--");
            
           // Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
            if (ProductParam.VC_Bypass_Pick)
            {
                e.ProcessSuccess = true;
                return;
            }
            else
            {
                Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
            }


            if (SpinWait.SpinUntil(() => ProductParam.VCCheck_Bypass_Pick || CheckVecuumState(BoatCarrier1.IsVirtual) || 
            e.Executor.CancelToken.IsCancellationRequested, ProductParam.VacuumTimeout * 1000))
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 有料_CVRA_頂升汽缸開啟_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--有料_CVRA_頂升汽缸開啟_ProcessIn--");
            if (bVirtual)
            {
                e.ProcessSuccess = true;
            }

            if (ProductParam.TopSV_Bypass_Pick)
            {
                e.ProcessSuccess = true;
                return;
            }

            //Y067002_治具頂升氣缸.SetIO(true);
            //SpinWait.SpinUntil(() => X066007_治具頂升氣缸_上升.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);

            //if (e.Executor.CancelToken.IsCancellationRequested)
            //{
            //    e.ProcessSuccess = false;
            //}
            //else
            //{
            //    if (X066007_治具頂升氣缸_上升.CheckIO())
            //    {
            //        e.ProcessSuccess = true;
            //    }
            //    else
            //    {
            //        e.ProcessSuccess = false;
            //    }
            //}
          
        }

        private void 有料_CVRA_定位汽缸開啟_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

            LogTrace("--有料_CVRA_定位汽缸開啟_ProcessIn--");
             if (bVirtual)
            {
               X066005_傳送流道_到位氣缸_上升.SetIO(true);
            }

            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
            SpinWait.SpinUntil(() =>  e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);

            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                e.ProcessSuccess = false;
            }
            else
            {
                e.ProcessSuccess = true;
                /*
                if (X066007_治具頂升氣缸_上升.CheckIO())
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                }
                */
            }
        }

        private void 有料_CVRA_頂升汽缸關閉_STP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            SendEvent?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Event_FlowCarrier_OutProduct_Start" });
            LogTrace("--有料_CVRA_頂升汽缸開啟_ProcessIn--");
            if (bVirtual)
            {
                //X066008_治具頂升氣缸_下降.SetIO(true);
            }

            //Y067002_治具頂升氣缸.SetIO(false);
            //SpinWait.SpinUntil(() => 
            // X066008_治具頂升氣缸_下降.CheckIO() || e.Executor.CancelToken.IsCancellationRequested, ProductParam.CylinderTimeout * 1000);

            //if (e.Executor.CancelToken.IsCancellationRequested)
            //{
            //    e.ProcessSuccess = false;
            //}
            //else
            //{
            //    if (X066008_治具頂升氣缸_下降.CheckIO())
            //    {
            //        e.ProcessSuccess = true;
            //    }
            //    else
            //    {
            //        e.ProcessSuccess = false;
            //    }
            //}
            e.ProcessSuccess = true;
        }


        private void Alarm操作_ProcessError(object sender, ExecuteFailArgs e)
        {
            var process = (ControlFlow.Controls.ProcessItem)sender;
            string errorMessage = "";
            switch (process.Name)
            {
                case "等待_流道傳送軸_Load檢知逾時_Err":
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() 
                        { NotifyName = "Alarm_FlowCarrier_LoadTimeout", AlarmActive = true });
                    errorMessage = Resources.LoadTimeoutAlarm;
                    break;
                case "等待_流道傳送軸_減速檢知逾時_Err":
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() 
                        { NotifyName = "Alarm_FlowCarrier_DecTimeout", AlarmActive = true });
                    errorMessage = Resources.DecTimeoutAlarm;
                    break;
                case "等待_流道傳送軸_到位檢知逾時_Err":
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() 
                        { NotifyName = "Alarm_FlowCarrier_StopTimeout", AlarmActive = true });
                    errorMessage = Resources.StopTimeoutAlarm;
                    break;
                case "入料_流道傳送軸_真空開啟逾時_Err":
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs()
                        { NotifyName = "Alarm_FlowCarrier_VacuumTimeout", AlarmActive = true });
                    errorMessage = Resources.VacuumTimeAlarm;
                    break;
                case "入料_流道傳送軸_頂升汽缸開啟逾時_Err":
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs()
                        { NotifyName = "Alarm_FlowCarrier_TopSVTimeout", AlarmActive = true });
                    errorMessage = Resources.TopSVTimeAlarm;
                    break;
                case "流道傳送軸_出料動作_Err":
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() 
                        { NotifyName = "Alarm_FlowCarrier_OutProduct", AlarmActive = true });
                    errorMessage = Resources.TopSVTimeAlarm;
                    break;
                    
            }
            BX2_流道傳送軸.Stop();

            var moduleError = (ModuleNotifyArgs)e;
            moduleError.ErrorMessage = errorMessage;
            moduleError.StopAction = StopActionEm.StopAll;
            moduleError.ShowAction = ShowActionEm.NotifyYesNo;
            NotifyRedLight?.Invoke(this, new RedLightOnArgs() { IsOn = true });
        }


        private void Alarm操作_ProcessErrorDone(object sender, ExecuteFailArgs e)
        {
            var process = (ControlFlow.Controls.ProcessItem)sender;
            switch (process.Name)
            {
                case "等待_流道傳送軸_Load檢知逾時_Err":
                case "等待_流道傳送軸_減速檢知逾時_Err":
                case "等待_流道傳送軸_到位檢知逾時_Err":
                case "入料_流道傳送軸_真空開啟逾時_Err":
                case "入料_流道傳送軸_頂升汽缸開啟逾時_Err":
                case "流道傳送軸_出料動作_Err":

                    AlarmQueue.Add(true);
                    break;
            }
        }

        public static VelocityData GetVelocity(ModuleBase module, IAxis axis, TVelEnum vel)
        {
            var velService = module.GetHtaService<IAxisConfigVelocityService>();
            if (velService == null || axis == null)
            {


                var _currentVel = new VelocityData()
                {
                    //OwnerAxisName = axis.Name,
                    Velocity = new Velocity()
                    {
                        ACCTime = 0.1,
                        DECTime = 0.1,
                        MaxVel = 20,
                        StartVel = 1,
                        VSACC = 0.1,
                        VSDEC = 0.1
                    },
                    VelocityName = "NotExist"
                };
                return _currentVel;
            }

            else
            {
                var velList = velService.QueryVelData(axis.Name);
                var currentVel = velList.Find(x => x.VelocityName == vel.ToString());
                return currentVel;
            }
        }

       
        private void 是否為Golden模式_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            

            if (_isGolden)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void Golden模式不做事_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            while (!e.Executor.CancelToken.IsCancellationRequested)
            {
                SpinWait.SpinUntil(() => false, 100);
            }
            e.ProcessSuccess = false;
            return;
        }
    }
}
