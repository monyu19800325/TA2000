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
using ModuleTemplate;
using HTA.MainController;
using Hta.Container;
using VisionController2.Routines2.WCF.Contract;
using VisionController2.Routines2.WCF;
using HTA.Com.WCF;
using HTA.Utility.Common;
using HTA.Utility.Structure;
using System.Collections.Concurrent;
using Hta.MotionBase;
using ModuleTemplate.Services;
using System.Threading;
using ControlFlow.Executor;
using HTAMachine.Machine.Services;
using HTA.MotionBase.Utility;
using HTA.Motion3.Virtual;
using HTA.InspectionFlow;
using static Hta.Container.VisionInspectInfo;
using HTA.IFramer;
using HyperInspection;
using VisionController2.MosaicController;
using Hta.ModuleImplement;
using System.CodeDom.Compiler;
using HTA.Utility;
using HTA.Motion3.BaseClass;
using VisionController2.VisionController.Common;
using TA2000Modules;
using DevExpress.Mvvm.POCO;
using TA2000Modules.ToolForm;
using HTA.SecsBase;
using HTAMachine.Module.AxisConfigModule;
using HTA.Motion3.Sensor.Serial;
using DevExpress.Utils.DragDrop;
using ObjectDraw;
//using CamBarcodeH20;
using LVDATA;
using ImageExtendInterFace;
using HTA.Utility.Vision;
using VisionController2.Remote.Framer.Client;
using System.IO;
using System.Text.RegularExpressions;
using HalconDotNet;
//using System.IO.Packaging;
using DevExpress.Mvvm.ModuleInjection.Native;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskBand;
using DevExpress.XtraEditors;
using HTA.Com.TCPIP;
using HTA.Utility.Halcon;
using InspectionModule.Properties;
using CamBarcodeH20;
using ControlFlow.Controls;



namespace TA2000Modules
{
    public partial class InspectionModule : ModuleBase, IInternalVision, ITrayViewService, ICanTeaching, INotifyPropertyChanged, ICanSaveParam, ICanUseSecs
    {
        bool bVirtual = false;
        //全域變數在InspectModuleExtend.cs
        Calibration.Halcon.MosaicTool.MotionPoseGrid MotionGrid = new Calibration.Halcon.MosaicTool.MotionPoseGrid();
        /// <summary>
        /// 相機重新連線確認
        /// </summary>
        public bool reconnectTrue = false;
        WizardForm WizardForm;
        //視覺站Setting
        [DefineVisionModule("TA2000Vision")] public IMainController VisionController; //檢測視覺站
        [DefineVisionModule("TA2000Vision_Barcode")] public IMainController BarcodeController; //Barcode視覺站
        [DefineVisionModule("TA2000Vision_Mosaic")] public IMainController VisionController_Mosaic;

        #region defineHardware (Start)


        #endregion defineHardware (End)

        #region InspectionModule設定參數 (Start)
        [ModuleGlobalSetting] public InspectionModuleParam Param = new InspectionModuleParam();
        [ModuleProductSetting] public InspectionProductParam ProductParam = new InspectionProductParam();
        //[ModuleProductSetting] public InspectFailCode FailCodeParam = new InspectFailCode();
        [ModuleGlobalSetting][MotorOffsetClass] public MotorOffsetParam MotorOffset = new MotorOffsetParam();
        #endregion InspectionModule設定參數 (End)

        #region 廣播 DefineBroadcast (Start)
        [DefineBroadcast]
        public event EventHandler<RedLightOnArgs> NotifyRedLight;
        [DefineBroadcast]
        public event EventHandler<OrangeLightOnArgs> NotifyOrangeLight;
        [DefineBroadcast]
        public event EventHandler<GreenLightOnArgs> NotifyGreenLight;
        [DefineBroadcast]
        public event EventHandler<QueryTrayLayoutArgs> QueryTrayLayout;
        [DefineBroadcast]
        public event EventHandler<VisionControllerArgs> SetVisionController;
        [DefineBroadcast]
        public event EventHandler<LotChangeArgs> NotifyLotChange;
        [DefineBroadcast]
        public event EventHandler<VisionByPassArgs> NotifyVisionByPass;
        [DefineBroadcast]
        public event EventHandler<GoldenModelArgs> NotifyGoldenModel;
        [DefineBroadcast]
        public event EventHandler<MotorOffsetParam> NotifyInspectMotorOffsetParam;

        [DefineBroadcast]
        public event EventHandler<BoatCarrier> GetTrayLayout;

        [DefineBroadcast]
        public event EventHandler<InitalArgs> NotifyInital;
         [DefineBroadcast]
        public event EventHandler<SpendTimeArgs> NotifySpendTime;

        public void PreGetTray(BoatCarrier carrier, string RecipeName, bool IsCalBig)
        {
            carrier.IsPreUse = true;
            carrier.IsCalBig = IsCalBig;
            carrier.RecipeName = RecipeName;
            GetTrayLayout?.Invoke(this, carrier);
        }
        #endregion 廣播 DefineBroadcast (End)

        #region 廣播 BroadcastReceive       

        [DefineBroadcastReceive]
        public void ReceiveLotChange(object sender, LotChangeArgs lot)
        {
            _curLotName = lot.LotName;
            //_statisticHandler.ReceiveLotChange(lot.LotGuid, lot.LotName);
            if (StatisticView != null && CurrentTrayCarrier.Count != -1)
            {
                _endRunTime = DateTime.Now;
                SummaryReport summaryReport = new SummaryReport();
                summaryReport.MachineName = "TA2000";
                summaryReport.LotName = PreviousLotName;
                summaryReport.StartTime = _startRunTime;
                summaryReport.EndTime = _endRunTime;
                summaryReport.ProductName = ProductName;
                summaryReport.PassCount = StatisticView.AllCounts.ViewFormDatas[0].Pass;
                summaryReport.FailCount = StatisticView.AllCounts.ViewFormDatas[0].Fail;
                summaryReport.InvalidCount = StatisticView.AllCounts.ViewFormDatas[0].Invalid;
                summaryReport.TrayCount = CurrentTrayCarrier.Count;
                summaryReport.Path = Param.SummaryReportPath;
                summaryReport.StatisticViewModel = StatisticView;
                summaryReport.GenSummaryReport(this);
            }
            if (ProductParam.LotRejectAlarmCount > 0)
            {
                if (StatisticView != null)
                {
                    if (StatisticView.AllCounts.ViewFormDatas[0].Fail + StatisticView.AllCounts.ViewFormDatas[0].Invalid
                    >= ProductParam.LotRejectAlarmCount)
                    {
                        NotifyRedLight?.Invoke(this, new RedLightOnArgs() { IsOn = true });
                        DialogService.ShowDialog(this, new ShowDialogArgs()
                        {
                            Button = MessageBoxButtons.OK,
                            Caption = "Alarm",
                            Message = Resources.TheLot + $"({PreviousLotName})" + Resources.RejectNumberReach + $"{ProductParam.LotRejectAlarmCount}，" + Resources.CheckProductStatus
                        });
                        NotifyGreenLight?.Invoke(this, new GreenLightOnArgs() { IsOn = true });
                    }
                }
            }
            PreviousLotName = lot.LotName;
            _startRunTime = DateTime.Now;

            if (_statisticForm != null)
            {
                StatisticView.InitialCounts();
            }
        }

        [DefineBroadcastReceive]
        public void ReceiveProductChanged(object sender, ProductChangeArgs product)
        {
            ProductName = product.ProductName;
            if (_statisticForm != null)
            {
                _mdiService.CloseMdiForm(this, _statisticForm);
            }
            //如果切換產品檔，就強制Abort
            _machineSimpleController.Abort();
            //_statisticHandler.ReceiveProductChange(ProductName);
        }

        //[DefineBroadcastReceive]
        //public void GetVersion(object sender, VersionController versionController)
        //{
        //    _versionController = versionController;
        //}
        [DefineBroadcastReceive]
        public void GetIMotorOffset(object sender, MotorOffsetParam args)
        {

            args.Start_Z1 = MotorOffset.Start_Z1;
            //args.Param = MotorOffset;
            //目前有些offset改到suckerModule，所以這邊看需要哪些
        }
        [DefineBroadcastReceive]
        public void ReconnectTrigger(object sender, ReConnectTriggerArgs e)
        {
            ((MainController)VisionController).ReConnectWcfTrigger();
        }
        [DefineBroadcastReceive]
        public void GetMachineState(object sender, MachineStateArgs args)
        {
            if (args._AxisState == AxisState.Virtual)
            {
                bVirtual = true;
            }
        }
        //[DefineBroadcastReceive]
        //public void GetCalibOffset(object sender,CalibOffsetArgs e)
        //{
        //    SuckerModuleThis.MotorOffset.InspectPosition_AZ1 = e.HalconCalibAZ1Offset;
        //    //儲存參數
        //    SaveGlobalParam?.Invoke(this, SuckerModuleThis);
        //}
        #endregion

        #region 事件 DefineEvent (Start)

        [DefineEvent]
        public event EventHandler<BoatCarrier> NotifyFlow;

        #endregion 事件 DefineEvent (End)


        #region 事件接收 DefineEventReceive (Start)

        [DefineEventReceive]
        public void GetFlowBoat(object sender, BoatCarrier e)
        {
            _trayInQueue.Add(e.Tray);
            CurrentTrayCarrier = e;
            OnTrayIn(e.Tray);
        }


        [DefineEventReceive]
        public void GetInspMotorOffset(object sender, InspectionParamArgs e)
        {
            //e = MotorOffset;
            e.Param = MotorOffset;
            //NotifyCalibration?.Invoke(this, MotorOffset);
        }

        #endregion 事件接收 DefineEventReceive (End)

        List<List<double>> MapRegionMeasurePoints = new List<List<double>>();
        List<Point2d> MapRegionMaxMinPoints = new List<Point2d>();
        Thread _laserMeasureThread;
        public int LaserMapIndex = 0;
        public double LaserBestZ = 0;
        public IMachineCommon MachineCommonService;
        #region 建構子 (Start)
        /// <summary>
        /// 建構子
        /// </summary>
        public InspectionModule() : base()
        {
            InitializeComponent();
            this.Disposed += ModuleBase_Disposed;
            this.BeforeAbort += InspectionModule_BeforeAbort;
            this.BeforeStop += InspectionModule_BeforeStop;
            this.CheckCanStart += CheckCanStartAction;
        }

        private void InspectionModule_BeforeAbort(object sender, ModuleDesignConfirmArgs e)
        {
            if (_currentImageView != null)
            {
                _mdiService.CloseMdiFormAsync(this, _currentImageView);
            }

            if (WizardForm != null)
            {
                _mdiService.CloseMdiFormAsync(this, WizardForm);
            }

            //Abort時，視覺下線，這樣才能更換產品檔
            _wcfClient?.Call(s =>
            {
                s?.Offline();
            });

            _wcfClientMosaic?.Call(s =>
            {
                s?.Offline();
            });


            IsGolden = false;
            Is3DGolden = false;
            NotifyGoldenModel?.Invoke(this, new GoldenModelArgs() { IsGolden = false });
            NotifyInital?.Invoke(this, new InitalArgs() { IsInitialAxis = true });
            //var service = this.GetHtaService<IMdiService>();
            //if (TeachingForm != null)
            //{
            //    service.CloseMdiForm(this, TeachingForm);
            //}
        }
        protected override void ModuleBase_Disposed(object sender, EventArgs e)
        {
            base.ModuleBase_Disposed(sender, e);
            if (_wcfClient != null)
            {
                _wcfClient.Dispose();
                _wcfClient = null;
            }
            BarcodeReader?.Dispose();
        }

        private void InspectionModule_BeforeStop(object sender, ModuleDesignConfirmArgs e)
        {
            var service = this.GetHtaService<IMdiService>();
            if (TeachingForm != null)
            {
                service.CloseMdiForm(this, TeachingForm);
            }
        }

        public void CheckCanStartAction(object sender, ModuleDesignConfirmArgs e)
        {
            //var isOk = CheckSuckerInstall();
            //e.CanExecute = isOk;
            //e.Message = "吸嘴安裝異常，請確認";
            //TODO 待測試
            //if (LITool.ProductMosaicListLeft.Any(x => x != "") && LITool.ProductMosaicListRight.Any(x => x != ""))
            //{
            //    if(!LITool.ProductMosaicListLeft.Any(x => x == ProductName) ||
            //        !LITool.ProductMosaicListRight.Any(x => x == ProductName))
            //    {
            //        e.CanExecute = false;
            //        e.Message = "此產品沒有做組圖校正，請做完組圖校正後，才可以執行流程";
            //    }
            //}

            if (BX1_流道橫移軸 is VirtualAxis)
            {
                bVirtual = true;
            }
            #region 檢查產品檔是否有對應的Mosaic點位 (Start)
            BoatCarrier _boarCarrier = new BoatCarrier();

            PreGetTray(_boarCarrier, ProductName, true);


            if (_boarCarrier.IsBigProduct)
            {
                //讀取Mosaic檔案
                var _loadsuccess = TATool.LoadMosaic(out MotionGrid);

                bool _check = false;
                if (_loadsuccess)
                {
                    //整理Mosaic資訊
                    List<MosaicInfo> _mosaicInfo_List = TATool.SortMosaicInfo(MotionGrid);

                    //確認是否有Mosaic點位
                    _check = CheckPosition(_boarCarrier, _mosaicInfo_List);
                }

                //if (!_check)
                //{
                //    e.CanExecute = false;
                //    e.Message = "此產品沒有做組圖校正，請做完組圖校正後，才可以執行流程";
                // }
            }
            #endregion 檢查產品檔是否有對應的Mosaic點位 (End)

            #region 檢查Focus數量是否與FlowSetting的Group數量相同 (Start)

            //ProductParam.FocusLocations = checkFocusList(ProductParam.FocusLocations);

            #endregion 檢查Focus數量是否與FlowSetting的Group數量相同 (End)
        }

        #endregion 建構子 (End)


        #region Initial & Setting (Start)

        public void InitialParam()
        {
            _sgGoCommands.Clear();
            _sgGoCommandsMosaic.Clear();

            //_sgBackCommands.Clear();
            _grabCommands.Clear();
            _grabCommandsMosaic.Clear();

            FinalResults.Clear();
            FinalResultsMosaic.Clear();

            TeachInspectTimes = 0;
           
            _programStartTime = DateTime.Now;
            CheckStatisticForm();
            _totalTriggerCount = 0;
            DataTimeoutIsRetry = false;
            IsPassVision = false;
            _isReinspectFlag = false;
            GoldenFinishFlag = false;
            AccumulateEachTrayFailAlarmCount = 0;
            
            UPHStopwatch.Stop();
            UPHStopwatch.Reset();

            IsFirstInspect = true;
            if (ProductParam.UseLaserMeasure) ProductParam.IsPick = true;//雷射測高只能用單顆抽檢進行檢測
            LaserBestZ = ProductParam.LaserSetting.BestZValue;

            //是否抽測，將抽測的位置加入到SingleInspectPos
            if (ProductParam.IsPick)
            {
                SingleInspectPos.Clear();
                for (int i = 0; i < ProductParam.PickPos.Count; i++)
                {
                    SingleInspectPos.Add(new Point2d(ProductParam.PickPos[i].x + 1, ProductParam.PickPos[i].y + 1));
                }

                //排序
                var _tempSingleInspectPos = SingleInspectPos.OrderBy(order1 => order1.y).ThenBy(order2 => order2.x).ToList();
                SingleInspectPos.Clear();
                SingleInspectPos = new List<Point2d>(_tempSingleInspectPos);
                _tempSingleInspectPos.Clear();
            }

            //開啟影像視窗顯示
            if (_currentImageView == null)
            {
                _currentImageView = (CurrentImageView)_mdiService.ShowMdi(this, typeof(CurrentImageView), new Point(0, 0), new Size(0, 0), new object[] { VisionController });
                _currentImageView.FormClosed += (ss, ee) =>
                {
                    _currentImageView = null;
                };
            }
            MapIndexOrder.Clear();
            for (int i = 0; i < 2; i++)//ProductParam.BigProductMapSetting.MapList.Count
            {
                MapIndexOrder.Add(i);
            }
            //if (Param.IsUseLotOldData)
            //{
            //    ReadRecordStatistic();
            //}

            //if(Directory.Exists($@"D:\Coordinator2.0\Products\{ProductName}\LaserData"))
            //    if (File.Exists($@"D:\Coordinator2.0\Products\{ProductName}\LaserData\LaserBlockModel.ncm"))
            //        HOperatorSet.ReadNccModel($@"D:\Coordinator2.0\Products\{ProductName}\LaserData\LaserBlockModel.ncm",out TargetModel);


            PixeltoMM = VisionController.GetHardware().CalibrationLists[0].Pix2MMX;
        }


        #region 教讀 (Start)
        private int _stationIndex = 0;
        public int StationIndex { get => _stationIndex; set { _stationIndex = value; } }
        private bool _teaching;
        public bool Teaching
        {
            get => _teaching;
            set
            {
                if (value == _teaching) return;
                _teaching = value;
                TeachingFlagChanged?.Invoke(this, _teaching);
            }

        }
        public event EventHandler<bool> TeachingFlagChanged;
        public event EventHandler<Type> TeachFormChanged;
        public event EventHandler<IModule> SaveGlobalParam;
        public event EventHandler<IModule> SaveProductParam;

        public void SaveParam(object sender, IModule module)
        {
            SaveGlobalParam?.Invoke(this, module);
        }
        public void SaveVisionProductParam(object sender, IModule module)
        {
            SaveProductParam?.Invoke(this, module);
        }

        #endregion 教讀 (End)


        #region Mosaic Function
        public void WcfMosaicSetting(ControlFlow.Controls.ProcessArgs e)
        {
            if (_wcfClientMosaic == null || !_wcfClientMosaic.IsServerOnline)
            {
                _clientMosaic = new VisionServerClient();
                _wcfClientMosaic = new WcfDuplexClient<IVisionServerWcf, IVisionServerCallback>(Param.PortMosaic, _clientMosaic, timeout: Param.TimeoutMosaic);
                _wcfClientMosaic.Open();

                //如果沒上線，就一直連上線為止
                if (!_wcfClientMosaic.IsServerOnline)
                {
                    while (!_wcfClientMosaic.IsServerOnline)
                    {
                        //取消，跳出迴圈機制
                        if (e.Executor.CancelToken.IsCancellationRequested)
                        {
                            e.ProcessSuccess = false;
                            return;
                        }
                        _wcfClientMosaic.Open();
                    }
                }

                //掛載事件，檢測完成
                _clientMosaic.OnInspectionDone += (ss, grabInfosResult) =>
                {
                    if (grabInfosResult.Success == false &&
                        _sgGoCommandsMosaic.FirstOrDefault(x => x.Id == grabInfosResult.Id) == null)
                    {
                        _sgGoCommandsMosaic.Enqueue(grabInfosResult);//如果檢測失敗，就重新檢測
                    }
                    else
                    {
                        FinalResultsMosaic.Add(grabInfosResult);
                        try
                        {
                            UpdateGridView(grabInfosResult, "Mosaic");

                            //StatisticView.SetStatisticCount(grabInfosResult);
                            //TODO 待修改統計
                            //StatisticTableViewModel.SetStatisticCount(grabInfosResult, "Not Mosaic");
                            StatisticTableViewModel2?.SetStatisticCount(grabInfosResult, "Mosaic");
                        }
                        catch (Exception exp)
                        {
                            OnAddFatal("InspectionModule", $"Result回來異常", exp);
                        }

                    }

                    if (_isReinspectFlag || ProductParam.IsPick)
                    {
                        if (FinalResultsMosaic.Count == SingleInspectPos.Count)
                        {
                            //收齊所有產品結果
                            _inspectionDoneMosaic.Add(true);

                        }
                    }
                    else
                    {
                        if (FinalResultsMosaic.Count == CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount)
                        {
                            //收齊所有產品結果
                            _inspectionDoneMosaic.Add(true);
                        }
                    }

                    if (_isReinspectFlag)
                    {
                        //如果是重測，就要刪除原本的統計
                        for (int i = 0; i < grabInfosResult.FOVInfos[0].ICs.Count; i++)
                        {
                            var icRow = grabInfosResult.FOVInfos[0].ICs[i].Row;
                            var icCol = grabInfosResult.FOVInfos[0].ICs[i].Col;
                            //TODO 待修改統計
                            //StatisticView.DeleteStatisticOriginResultCount(CurrentTrayCarrier.ProductInfos[icRow][icCol].InspectResult);
                        }
                    }
                };

                //掛載事件，取像完成(一Round拍完會進來)
                _clientMosaic.OnRoundDone += (ss, ee) =>
                {
                    _allCaptureDoneMosaic.Add(true);
                };
            }
        }

        public bool WcfMosaicIR_FN_NL(ControlFlow.Controls.ProcessArgs e)
        {
            bool isMosaicOnline = false;
            _wcfClientMosaic.Call(s =>
            {
                if (s.IsOnline() == false || IsPassVision == true)  //如果之前下線，就要上線
                {
                    if (IsPassVision)
                    {
                        s.Offline();
                        IsPassVision = false;
                    }

                    //Tips 
                    //Tips  是 By Row 檢測
                    s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //FN
                    s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //IR

                    //s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                    //s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //IR
                    s.Online(); //NL  
                }
                else
                {
                    if (((MainController)VisionController_Mosaic).MachineControlConfig.FlowRunnerNum == 1)
                    {
                        //如果已經上線了，但上次的FlowRunnerNum=1，代表上次重測，這次需要改回來
                        s.Offline();
                        s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //FN
                        s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //IR
                        s.Online(); //NL 
                    }
                }

                //偵測是否上線成功
                isMosaicOnline = SpinWait.SpinUntil(() => s.IsOnline() || e.Executor.CancelToken.IsCancellationRequested, Param.InspectOnlineTimeout * 1000);
            });
            return isMosaicOnline;
        }

        #endregion

        #region SecsGem (Start)

        [SecsSV("SV_IsPick", true, false)]
        public bool IsPick
        {
            get => ProductParam.IsPick;
            set
            {
                if (ProductParam.IsPick == value)
                    return;
                ProductParam.IsPick = value;
            }
        }

        [SecsSV("SV_InspectByPass", true, false)]
        public bool InspectByPass
        {
            get => ProductParam.InspectByPass;
            set
            {
                if (ProductParam.InspectByPass == value)
                    return;
                ProductParam.InspectByPass = value;
            }
        }

        [SecsSV("SV_IsReinspect", true, false)]
        public bool IsReinspect
        {
            get => ProductParam.IsReinspect;
            set
            {
                if (ProductParam.IsReinspect == value)
                    return;
                ProductParam.IsReinspect = value;
            }
        }

        [SecsSV("SV_ReinspectCount", true, false)]
        public int ReinspectCount
        {
            get => ProductParam.ReinspectCount;
            set
            {
                if (ProductParam.ReinspectCount == value)
                    return;
                ProductParam.ReinspectCount = value;
            }
        }

        [SecsSV("SV_FocusLocation", true, false)]
        public List<List<double>> FocusLocation
        {
            get => ProductParam.FocusLocations;
            set
            {
                if (ProductParam.FocusLocations == value)
                    return;
                ProductParam.FocusLocations = value;
            }
        }

        [SecsSV("SV_CheckCylinderTimeout", true, false)]
        public int CheckCylinderTimeout
        {
            get => ProductParam.CheckCylinderTimeout;
            set
            {
                if (ProductParam.CheckCylinderTimeout == value)
                    return;
                ProductParam.CheckCylinderTimeout = value;
            }
        }

        [SecsSV("SV_FlyPercent", true, false)]
        public double FlyPercent
        {
            get => ProductParam.FlyPercent;
            set
            {
                if (ProductParam.FlyPercent == value)
                    return;
                ProductParam.FlyPercent = value;
            }
        }

        [SecsSV("SV_FlyVel", true, false)]
        public int FlyVel
        {
            get => (int)ProductParam.FlyVel;
            set
            {
                if ((int)ProductParam.FlyVel == value)
                    return;
                ProductParam.FlyVel = (MoveVelEm)value;
            }
        }

        [SecsSV("SV_InspectVel", true, false)]
        public int InspectVel
        {
            get => (int)ProductParam.InspectVel;
            set
            {
                if ((int)ProductParam.InspectVel == value)
                    return;
                ProductParam.InspectVel = (MoveVelEm)value;
            }
        }

        [SecsSV("SV_IsSetFly", true, false)]
        public bool IsSetFly
        {
            get => ProductParam.IsSetFly;
            set
            {
                if (ProductParam.IsSetFly == value)
                    return;
                ProductParam.IsSetFly = value;
            }
        }
        [SecsSV("SV_UseBoatBarcodeReader", true, false)]
        public bool BarcodeReaderByPass
        {
            get => ProductParam.UseBoatBarcodeReader;
            set
            {
                if (ProductParam.UseBoatBarcodeReader == value)
                    return;
                ProductParam.UseBoatBarcodeReader = value;
            }
        }
        [SecsSV("SV_UseLaserMeasure", true, false)]
        public bool LaserByPass
        {
            get => ProductParam.UseLaserMeasure;
            set
            {
                if (ProductParam.UseLaserMeasure == value)
                    return;
                ProductParam.UseLaserMeasure = value;
            }
        }

        [SecsAlarmContainer("Alarm_Inspection_Sucker_Error", "Alarm_Inspection_SG_Fail",
           "Alarm_Inspection_FailAlarm",
           "Alarm_Inspection_CaptureImage_Not_Enough", "Alarm_Inspection_Inspect_Result_Timeout")]
        public event EventHandler AnyTypeAlram;

        [SecsEventContainer("Event_Inspection_Inspect_Start", "Event_Inspection_Inspect_End")]
        public event EventHandler SendEvent;



        #endregion SecsGem (End)


        #endregion Initial & Setting (End)

        #region 主流程 (Start)

        private void 視覺是否bypass_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            NotifyVisionByPass?.Invoke(this, new VisionByPassArgs() { IsByPass = ProductParam.InspectByPass });
            if (ProductParam.InspectByPass)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void 視覺bypass中_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--視覺bypass中_ProcessIn--");

            while (true)
            {
                if (e.Executor.CancelToken.IsCancellationRequested) //e.Executor.CancelToken.IsCancellationRequested0  (Stop 旗標)**
                {
                    e.ProcessSuccess = false;
                    return;
                }

                if (ProductParam.InspectByPass)
                {
                    SpinWait.SpinUntil(() => false, 200);
                }
                else
                {
                    e.ProcessSuccess = true;
                    return;
                }
            }
        }

        private void WCF通訊上線_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--WCF通訊上線_ProcessIn--");

            InitialParam();


            WcfMosaicSetting(e);

            //定義通訊方式
            if (_wcfClient == null || !_wcfClient.IsServerOnline)
            {
                //Client連線Server

                _client = new VisionServerClient();
                if (_wcfClient != null) _wcfClient.Dispose();
                _wcfClient = new WcfDuplexClient<IVisionServerWcf, IVisionServerCallback>(Param.Port, _client, timeout: Param.Timeout);
                _wcfClient.Open();

                //如果沒上線，就一直連上線為止
                    if (!_wcfClient.IsServerOnline)
                {
                    while (!_wcfClient.IsServerOnline)
                    {
                        //取消，跳出迴圈機制
                        if (e.Executor.CancelToken.IsCancellationRequested)
                        {
                            e.ProcessSuccess = false;
                            return;
                        }
                        Thread.SpinWait(500);
                        _wcfClient.Open();
                    }
                }

                //掛載事件，檢測完成
                _client.OnInspectionDone += (ss, grabInfosResult) =>
                {
                    //sgCommands.FirstOrDefault(x => x.Id == grabInfosResult.Id) =>為了避開有重複的GrabInfo
                    if (grabInfosResult.Success == false &&
                        _sgGoCommands.FirstOrDefault(x => x.Id == grabInfosResult.Id) == null)
                    {
                        _sgGoCommands.Enqueue(grabInfosResult);//如果檢測失敗，就重新檢測
                    }
                    else
                    {
                        FinalResults.Add(grabInfosResult);
                        try
                        {
                            //NormalizeGrabInfos(grabInfosResult); // grabInfosResult 正規化
                            UpdateGridView(grabInfosResult, "Not Mosaic");
                            //StatisticView.SetStatisticCount(grabInfosResult);
                            StatisticTableViewModel2?.SetStatisticCount(grabInfosResult, "Not Mosaic");
                        }
                        catch (Exception exp)
                        {
                            OnAddFatal("InspectionModule", $"Result回來異常", exp);
                        }

                    }

                    if (_isReinspectFlag || ProductParam.IsPick)
                    {
                        if (FinalResults.Count == SingleInspectPos.Count)
                        {
                            //收齊所有產品結果
                            _inspectionDone.Add(true);
                            InspectionDone = true;
                        }
                    }
                    else
                    {
                        if (FinalResults.Count == CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount)
                        {
                            //收齊所有產品結果
                            _inspectionDone.Add(true);
                            InspectionDone = true;
                        }
                    }

                    if (_isReinspectFlag)
                    {
                        //如果是重測，就要刪除原本的統計
                        for (int i = 0; i < grabInfosResult.FOVInfos[0].ICs.Count; i++)
                        {
                            var icRow = grabInfosResult.FOVInfos[0].ICs[i].Row;
                            var icCol = grabInfosResult.FOVInfos[0].ICs[i].Col;
                            // StatisticView.DeleteStatisticOriginResultCount(CurrentTrayCarrier.ProductInfos[icRow][icCol].InspectResult);
                        }
                    }

                    //UpdateSecsParam();
                };

                //掛載事件，取像完成(一Round拍完會進來)
                _client.OnRoundDone += (ss, ee) =>
                {
                    _allCaptureDone.Add(true);
                    AllCaptureDone = true;
                };

            }

            LogTrace($"連線:{_wcfClient.IsServerOnline}");

            e.ProcessSuccess = true;
        }

        /// <summary>
        /// 將除了流道軸之外，所有軸移動至Standby位置 
        ///(此時流道軸為FlowCarrierModule控制)
        /// </summary>
        private void 所有軸移動到Standby位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            //此時流道軸權限是FlowCarrierModule控制，因此不能控制移動

            LogTrace($"--移動到Standby位置_ProcessIn--");
            if (IsGolden)
            {
                e.ProcessSuccess = true;
                LogTrace($"--IsGolden--");
                return;
            }

            //有差異的地方 Mon
            _preBoatCarrier.IsPreUse = true;//提前拿，不要加計數
            GetTrayLayout.Invoke(this, _preBoatCarrier);//先提前拿取Boat資訊，只給下面先偷跑使用，實際執行使用的是CurrentTrayCarrier
                                                        //CurrentTrayCarrier = _preBoatCarrier;

            double distX = 0; //流道橫移軸
            double distY = 0; //視覺縱移軸

            if (_preBoatCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            {
                int n = _preBoatCarrier.InspectData.InspectionPostion._bigProductX.Length;
                distX = _preBoatCarrier.InspectData.InspectionPostion._bigProductX[n - 1][0];

                n = _preBoatCarrier.InspectData.InspectionPostion._bigProductY.Length;
                distY = _preBoatCarrier.InspectData.InspectionPostion._bigProductY[n - 1][0];
            }
            else
            {
                distX = MotorOffset.Start_X1;
                distY = MotorOffset.Start_AY1;
            }

            var velDefAX1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);
            var moveSuccessAX1 = MoveAxis(BX1_流道橫移軸, distX, velDefAX1);

            var velDefY1 = SelectVelDef(視覺縱移軸, AY1VelList);
            var moveSuccessY1 = MoveAxis(視覺縱移軸, distY, velDefY1);


            var velDefAZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
            var moveSuccessAZ1 = MoveAxis(BZ1_流道頂升升降軸, MotorOffset.Start_Z1, velDefAZ1);

            bool AX1_waitRes = BX1_流道橫移軸.WaitMotionDone();
            bool AY1_waitRes = 視覺縱移軸.WaitMotionDone();
            bool AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();

            SpinWait.SpinUntil(() => false, 100); //稍作延時判斷

            if (AX1_waitRes && AY1_waitRes && AZ1_waitRes) //AX1_waitRes
            {
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
            }
        }

        private void 讀取檢測序列資訊IR_FN_NL_ProcessIn_old(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--下IR_FN_NL_ProcessIn--");

            //_preBoatCarrier.IsPreUse = true;//提前拿，不要加計數
            //GetTrayLayout.Invoke(this, _preBoatCarrier);//先提前拿取Boat資訊，只給下面先偷跑使用，實際執行使用的是CurrentTrayCarrier

            bool isOnline = false;

            LaserResult_List = new List<LaserResult>(); //創建新的List

            _wcfClient.Call(s =>
            {
                if (s.IsOnline() == false || IsPassVision == true)  //如果之前下線，就要上線
                {
                    if (IsPassVision)
                    {
                        s.Offline();
                        IsPassVision = false;
                    }

                    //Tips  是 By Row 檢測
                    s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.MoreBlockYMaxStepCount); //FN
                    s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.MoreBlockXMaxStepCount); //IR
                    if (ProductParam.UseMosaic == false &&
                    _preBoatCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                    {
                        s.SetMosaicIsOpen(false);
                    }
                    s.Online(); //NL  
                }
                else
                {
                    if (((MainController)VisionController).MachineControlConfig.FlowRunnerNum == 1)
                    {
                        //如果已經上線了，但上次的FlowRunnerNum=1，代表上次重測，這次需要改回來
                        s.Offline();
                        s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.MoreBlockYMaxStepCount); //FN
                        s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.MoreBlockXMaxStepCount); //IR
                        if (ProductParam.UseMosaic == false &&
                    _preBoatCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                        {
                            s.SetMosaicIsOpen(false);
                        }
                        s.Online(); //NL 
                    }
                }

                //偵測是否上線成功
                isOnline = SpinWait.SpinUntil(() => s.IsOnline() || e.Executor.CancelToken.IsCancellationRequested, Param.InspectOnlineTimeout * 1000);
            });

            if (isOnline)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                LogTrace($"上線失敗，isOnline:{isOnline}");
                e.ProcessSuccess = false;
            }
        }

        private void 讀取檢測序列資訊IR_FN_NL_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--下IR_FN_NL_ProcessIn--");

            _preBoatCarrier.IsPreUse = true;//提前拿，不要加計數
            GetTrayLayout.Invoke(this, _preBoatCarrier);//先提前拿取Boat資訊，只給下面先偷跑使用，實際執行使用的是CurrentTrayCarrier
            bool isOnlineMosaic = WcfMosaicIR_FN_NL(e);

            bool isOnline = false;


            _wcfClient.Call(s =>
            {
                if (s.IsOnline() == false || IsPassVision == true)  //如果之前下線，就要上線
                {
                    if (IsPassVision)
                    {
                        s.Offline();
                        IsPassVision = false;
                    }

                    //Tips BA3000 是 By Col 檢測
                    s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                    s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //IR
                    s.SetMosaicIsOpen(false);
                    s.Online(); //NL                                           
                }
                else
                {
                    if (((MainController)VisionController).MachineControlConfig.FlowRunnerNum == 1)
                    {
                        //如果已經上線了，但上次的FlowRunnerNum=1，代表上次重測，這次需要改回來
                        s.Offline();
                        s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                        s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //IR
                        s.SetMosaicIsOpen(false);
                        s.Online(); //NL 
                    }
                }

                //偵測是否上線成功
                isOnline = SpinWait.SpinUntil(() => s.IsOnline() || e.Executor.CancelToken.IsCancellationRequested, Param.InspectOnlineTimeout * 1000);
            });




            if (isOnline && isOnlineMosaic)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                LogTrace($"上線失敗，isOnline:{isOnline},isOnlineMosaic:{isOnlineMosaic}");
                e.ProcessSuccess = false;
            }
        }
        private void 等待載盤進入_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待產品進入_ProcessIn--");

            if (TeachInspectTimes > 0)
            {
                //TeachInspectTimes>0 代表在教讀，且在跑檢測次數的檢測
                e.ProcessSuccess = true;
            }
            else
            {
                //WriteVisionProductCount(); //畫面呈現結果 --> 後續討論(12/30)

                //Fetch Executer
                //var executor = (IControlFlowExecutor)e.Executor;
                //等待tray in command產生

                bool res = false;
                TrayContainer tContainer = new TrayContainer();
                while (!e.Executor.CancelToken.IsCancellationRequested)
                {
                    if (_trayInQueue.Count > 0)
                    {
                        tContainer = _trayInQueue.Take();
                        res = true;
                        break;
                    }
                    SpinWait.SpinUntil(() => false, 100);
                }

                //if (executor.TakeFromQueue<TrayContainer>(_trayInQueue, -1, out var tContainer))
                if (res)
                {
                    LogTrace("第幾盤:" + CurrentTrayCarrier.Count);
                    _trayInCommands = tContainer;
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;
                    return;
                }

            }

            //清除所有Queue
            var trayQueueCount = _trayInQueue.Count;
            for (int i = 0; i < trayQueueCount; i++)
            {
                _trayInQueue.Take();
            }
            var allCaptureDoneCount = _allCaptureDone.Count;
            for (int i = 0; i < allCaptureDoneCount; i++)
            {
                _allCaptureDone.Take();
            }
            var allCaptureDoneMosaicCount = _allCaptureDoneMosaic.Count;
            for (int i = 0; i < allCaptureDoneMosaicCount; i++)
            {
                _allCaptureDoneMosaic.Take();
            }
            var inspectionDoneCount = _inspectionDone.Count;
            for (int i = 0; i < inspectionDoneCount; i++)
            {
                _inspectionDone.Take();
            }
            var inspectionDoneMosaicCount = _inspectionDoneMosaic.Count;
            for (int i = 0; i < inspectionDoneMosaicCount; i++)
            {
                _inspectionDoneMosaic.Take();
            }
            var goInsepctQueueCount = GoInsepctQueue.Count;
            for (int i = 0; i < goInsepctQueueCount; i++)
            {
                GoInsepctQueue.Take();
            }
            var alarmQueueCount = AlarmQueue.Count;
            for (int i = 0; i < alarmQueueCount; i++)
            {
                AlarmQueue.Take();
            }
            FinalResults.Clear();
            FinalResultsMosaic.Clear();
        }

        private void 是否Barcode教讀_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace($"是否Barcode教讀_ConditionCheck");
            if (IsGolden)
            {
                e.Result = false;
                //Sreturn;
            }

            if (Teaching == true && ProductParam.UseBoatBarcodeReader)
            {
                //Barcode教讀的Flag與教讀共用
                LogTrace($"--BarcodeTeaching:{Teaching}--");

                e.Result = true;
            }
            else
            {
                //Barcode教讀的Flag與教讀共用
                LogTrace($"--BarcodeTeaching:{Teaching}--");

                //GoInspect = false;
                e.Result = false;
            }
        }

        /// <summary>
        /// 將Vision Fov資訊傳送至TeachingForm
        /// </summary>
        /// <param name="frm"></param>
        private void SendVisionFovInfo(FlowForm frm)
        {

            VisionFovInfo visionFovInfo = null;

            if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
            {
                visionFovInfo = new VisionFovInfo()
                {
                    ProductSizeMm = new Point2d(CurrentTrayCarrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.X,
                                                CurrentTrayCarrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.Y),
                    Thickness = 0,
                    GridSize = new Point2d(CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount,
                                            CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount),
                    ProductPitchMm = new Point2d(CurrentTrayCarrier.InspectData.InspectionPostion.Container.BlockContainerDesc.Pitch.X,
                                                  CurrentTrayCarrier.InspectData.InspectionPostion.Container.BlockContainerDesc.Pitch.Y),
                    FreeFind = false,
                    ProductGap = new Point2d(1, 1),

                };
            }
            else if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            {
                visionFovInfo = new VisionFovInfo()
                {
                    ProductSizeMm = new Point2d(0.4, 0.4),
                    Thickness = 0,
                    GridSize = new Point2d(7, 3),
                    ProductPitchMm = new Point2d(1, 1),
                    FreeFind = false,
                    ProductGap = new Point2d(1, 1)
                };
            }

            frm.InjectVisionFovInfo(visionFovInfo);


        }

        private void Barcode教讀_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--Barcode教讀_ProcessIn--");

            if (flowForm != null)
            {
                this.GetHtaService<IMdiService>().CloseMdiFormAsync(this, flowForm);
            }

            var hardware = BarcodeController.GetHardware();

            flowForm = (FlowForm)this.GetHtaService<IMdiService>().ShowMdi(this, typeof(FlowForm), new Point(0, 0), new Size(0, 0),
                new object[] { BarcodeController, hardware, true }, true);

            flowForm.CurrentAccessLevel = 4;

            if (BarcodeTestForm != null)
            {
                this.GetHtaService<IMdiService>().CloseMdiFormAsync(this, BarcodeTestForm);
            }

            BarcodeTestForm = (BarcodeTestForm)this.GetHtaService<IMdiService>().ShowMdi(
                                    this, typeof(BarcodeTestForm),
                                    new Point(flowForm.Right, 0),
                                    new Size(0, 0),
                                    new object[] { this },
                                    true);//給建構子資料
            VisionController.ProductSetting.ProductInfo.UseProductInfo = false;



            while (!e.Executor.CancelToken.IsCancellationRequested)
            {
                if (Param.Barcode_TeachFinish)
                {
                    Param.Barcode_TeachFinish = false; //參數初始化
                    SaveParam(this, this);
                    break;
                }
                SpinWait.SpinUntil(() => false, 100);
            }

            if (flowForm != null)
                this.GetHtaService<IMdiService>().CloseMdiFormAsync(this, flowForm);

            if (BarcodeTestForm != null)
                this.GetHtaService<IMdiService>().CloseMdiFormAsync(this, BarcodeTestForm);//.CloseMdiForm(this, teachingForm);


            e.ProcessSuccess = true;
        }


        private void 是否拍攝Barcode_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            if (IsGolden)
            {
                e.Result = false;
                return;
            }

            LogTrace($"--BarcodeReaderByPass:{ProductParam.UseBoatBarcodeReader}--");

            if (ProductParam.UseBoatBarcodeReader)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void 是否教讀_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace($"是否教讀_ConditionCheck");
            LogTrace($"--Teaching:{Teaching}--");

            if (Teaching == true)
                e.Result = true;
            else
            {
                GoInspect = false;
                e.Result = false;
            }
        }


        private void 教讀_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--教讀_ProcessIn--");

            if (TeachInspectTimes == 0 || TeachInspectTimes >= InspectCounts || TeachImmediatelyStop == true)
            {
                //代表回到教讀介面，代表第一次教讀或是跑完教讀檢測次數
                CurrentTrayCarrier.IsErrorPassInspect = false;
                if (TeachImmediatelyStop)
                {
                    TeachBackQueue.Add(true);
                }

                //go offline，要先下線才能修改Flow的參數
                _wcfClient.Call(s =>
                {
                    s.Offline();
                });
                _wcfClientMosaic.Call(s =>
                {
                    s.Offline();
                });

                if (TeachInspectTimes >= InspectCounts && IsBurning)
                {
                    //燒機結束
                    //var triggerCount =  VisionController.Trigger1.GetTriggerCount();
                    //現在EachCameraCaptureCount被改成，SG的時候會被清0，所以這得到的是一個SG所拍攝的次數
                    //var cameraCaptureArray2 = ((StationFramer)mainController.Framer).EachCameraCaptureCount();
                    //CameraCaptureCount = cameraCaptureArray2[0] - CameraCaptureCount;
                    BurningReport.Data.Send_Trigger = _totalTriggerCount;
                    BurningReport.Data.Cam0Event = Camera0CaptureCount;
                    SetBurningParam();
                    BurningReport.GenBurningReport("BurningReport.txt", _curLotName);
                }

                if (TeachInspectTimes >= InspectCounts && IsGolden)
                {
                    //跑Golden結束
                    //TODO 確定檢測元件後打開測試

                    GoldenReport goldenReport = new GoldenReport();
                    goldenReport.CmpPath = VisionController.CommonInfo.ComponentSaveSetting.path;
                    goldenReport.WritePath = Param.GoldenReportPath;
                    goldenReport.InspectTimes = InspectCounts;
                    goldenReport.mCurrentLot = _curLotName; //DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_GoldenReport";
                    goldenReport.InitialParam(VisionController);


                    /*
                     * golden 部分,
                     * 2D Golden 要新增處理 1.Die 2.underfill   球半徑的偵測與 產品中心到中心的距離
                     * 新增2個按鈕,來個別處理
                     * */

                    if (Is3DGolden)
                    {
                        //goldenReport.ComponentHeightRawData("ComponentHeightRawData");
                        //goldenReport.ComponentHeightAccuracyReport("ComponentHeightAccuracy");
                        goldenReport.Export3DComponentHtml("ComponentHeightGoldenReport");
                    }
                    else
                    {
                        if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                        {
                            goldenReport.CmpPath = VisionController_Mosaic.CommonInfo.ComponentSaveSetting.path;
                        }
                        else
                        {
                            goldenReport.CmpPath = VisionController.CommonInfo.ComponentSaveSetting.path;
                        }
                        goldenReport.ExportAllInOneHtml("CircleGoldenReport");

                    }

                }
                if (IsGolden)
                {
                    if (WizardForm != null)
                    {
                        this.GetHtaService<IMdiService>().CloseMdiForm(this, WizardForm);
                    }
                }
                if (WizardForm == null)
                {
                    WizardForm = (WizardForm)this.GetHtaService<IMdiService>().ShowMdi(this, typeof(WizardForm), new Point(0, 0), new Size(1800, 800),
                        new object[] { this }, true);

                    WizardForm.FormClosed += (ss, ee) =>
                    {
                        WizardForm = null;
                    };
                }
                WizardForm?.ReturnTeach();

                bool isOnline = false;
                bool isOnlineMosaic = false;
                var executor = (IControlFlowExecutor)e.Executor;

                //這邊要等待使用者在教讀介面點下檢測或是停止才會繼續動作
                bool res = false;
                while (!e.Executor.CancelToken.IsCancellationRequested)
                {
                    if (GoInsepctQueue.Count > 0)
                    {
                        GoInspect = GoInsepctQueue.Take();
                        res = true;
                        break;
                    }
                    SpinWait.SpinUntil(() => false, 100);
                }

                if (IsGolden)
                {
                    if (GoldenFinishFlag)
                    {
                        //這邊要用Task，才不會沒辦法停止，不然會卡死
                        Task.Factory.StartNew(() =>
                        {
                            _machineSimpleController.Abort();
                        });
                        return;
                    }
                }

                //if (executor.TakeFromQueue<bool>(GoInsepctQueue, -1, out var GoInspect))
                if (res)
                {
                    LogTrace($"GoInspect={GoInspect}");
                    TeachImmediatelyStop = false;

                    LogTrace($"使用者教讀完按停止，關閉WizardForm");

                    if (_statisticForm != null)
                    {
                        _mdiService.CloseMdiForm(this, _statisticForm);
                    }
                    CheckStatisticForm();

                    //GoInspect = true代表開始教讀檢測，GoInspect = false代表教讀檢測停止(代表直接跑流程)
                    if (GoInspect == false)
                    {
                        TeachInspectTimes = 0;
                        e.ProcessSuccess = true;

                        //go online
                        _wcfClient.Call(s =>
                        {
                            s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                            s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //IR
                            s.SetMosaicIsOpen(false);
                            s.Online(); //NL  
                            isOnline = SpinWait.SpinUntil(s.IsOnline, Param.InspectOnlineTimeout * 1000);
                        });
                        _wcfClientMosaic.Call(s =>
                        {
                            s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                            s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //IR
                            s.Online(); //NL  
                            isOnlineMosaic = SpinWait.SpinUntil(s.IsOnline, Param.InspectOnlineTimeout * 1000);
                        });

                        ClearGridView();
                        LogTrace($"isOnline:{isOnline},isOnlineMosaic:{isOnlineMosaic}");
                        LogTrace($"使用者教讀完按停止，繼續流程");
                        return;
                    }
                    LogTrace($"使用者教讀完按檢測，依檢測次數完成，會再回到教讀");
                }
                else
                {
                    //只有暫停才可能進來這段
                    e.ProcessSuccess = false;
                    return;
                }

                if (IsBurning)
                {
                    //燒機開始
                    VisionController.Trigger1.ClearTriggerCount();
                    _totalTriggerCount = 0;
                    //var cameraCaptureArray = ((StationFramer)mainController.Framer).EachCameraCaptureCount();
                    //CameraCaptureCount = cameraCaptureArray[0];
                    Camera0CaptureCount = 0;
                    BurningReport.Data.ProductName = ProductName;
                    BurningReport.Data.StartTime = DateTime.Now;

                }
                TotalProductCount = 1 * CurrentTrayCarrier.Tray.BlockContainerDesc.SubDimSize.X *
                                        CurrentTrayCarrier.Tray.BlockContainerDesc.SubDimSize.Y;
                TeachInspectTimes = 0;
                TeachInspectTimes++;

                //go online 這邊是跑教讀檢測
                _wcfClient.Call(s =>
                {
                    s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                    s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //IR
                    s.SetMosaicIsOpen(false);
                    s.Online(); //NL  
                    isOnline = SpinWait.SpinUntil(s.IsOnline, Param.InspectOnlineTimeout * 1000);
                });
                _wcfClientMosaic.Call(s =>
                {
                    s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                    s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.YMaxStepCount); //IR
                    s.Online(); //NL  
                    isOnlineMosaic = SpinWait.SpinUntil(s.IsOnline, Param.InspectOnlineTimeout * 1000);
                });
                LogTrace($"isOnline:{isOnline},isOnlineMosaic:{isOnlineMosaic}");
            }
            else if (TeachInspectTimes < InspectCounts)
            {
                //正在跑教讀檢測
                TotalProductCount += 1 * CurrentTrayCarrier.Tray.BlockContainerDesc.SubDimSize.X *
                                         CurrentTrayCarrier.Tray.BlockContainerDesc.SubDimSize.Y;
                TeachInspectTimes++;
            }

            ClearGridView();
        }


        private void ChangeCollectImageMode(bool active)
        {
            IMainController controller = VisionController;
            foreach (var f in controller.GetHardware().Framer)
            {
                for (int i = 0; i < f.Grabbers.Count; i++)
                {
                    if (f.Grabbers[i] is GrabberClient cam)
                        cam.ReceiveImage(active);
                }
            }
        }

        private void 是否抽檢_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            // mon add 20260223
            UPHStopwatch.Start();
            //StatisticTableViewModel?.ResetType();

            SendEvent?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Event_Inspection_Inspect_Start" });

            if (ProductParam.IsPick)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }
        private void 建立SG_Quene_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--建立SG_Queue_ProcessIn--");

            //另外計算教讀跑檢測的次數 (因教讀檢測是同一個Tray，因此次數另外計算，不能將資訊蓋到下一盤貨)
            int otherTimes = 0;
            if (TeachInspectTimes > 0)
            {
                otherTimes = TeachInspectTimes - 1;
            }

            // SubDimSize : Tray X數量,  FOVProductXnum : 一個FOV數量  , 求餘數
            //int blockColRem = CurrentTrayCarrier.InspectData.InspectionPostion.Container.BlockContainerDesc.SubDimSize.X
            //    % CurrentTrayCarrier.InspectData.InspectionPostion.FOVProductXnum;
            //int blockRowRem =  CurrentTrayCarrier.InspectData.InspectionPostion.Container.BlockContainerDesc.SubDimSize.Y
            //    % CurrentTrayCarrier.InspectData.InspectionPostion.FOVProductYnum;


            //確認這盤的Row Col
            if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            {
                //大產品固定用單顆檢測
                for (int i = 0; i < CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount; i++)
                {
                    for (int j = 0; j < CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount; j++)
                    {
                        var grabInfo = CurrentTrayCarrier.Tray.GenSingleGrabInfo(
                                    VisionController.MachineControlConfig.SGTimeOut * 1000,
                                    CurrentTrayCarrier.Count + otherTimes,
                                    i + 1,//當下Row
                                    j + 1,//當下Col
                                    CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount * CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount, //一個FOV有幾顆
                                    CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount
                                    );

                        _sgGoCommands.Enqueue(grabInfo);
                        _sgGoCommandsMosaic.Enqueue(grabInfo);
                    }
                }
                //順便要下IR1??
                DoIR1(e);
            }
            else
            {
                //Tips 使用以Row為主產生GrabInfo
                for (int i = 0; i < CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount; i++)
                {

                    if (CurrentTrayCarrier.InspectData.InspectionPostion.SingleBlock) //單一Block
                    {
                        var grabInfo = CurrentTrayCarrier.Tray.GenGrabInfo(
                           VisionController.MachineControlConfig.SGTimeOut, //timeout
                           CurrentTrayCarrier.Count + otherTimes, //curTContainer.index,//第幾個Tray ，otherTimes如果有教讀會加TeachInspectTimes - 1，沒教讀就是0
                           i + 1, //Col,GenGrabInfo是從1開始
                           CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount,//一個Row要拍幾次
                           CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount * CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount,
                           CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount,
                           false, true, 0, 0,// bool inFOVRowInverse = false, bool inFOVColInverse = false, int rowJumpGap = 0, int currentJumpRow = 0,
                           1, 1);// int blockColNum = 1, int blockRowNum = 1)

                        if (i % 2 == 1) //偶數排做反向
                            grabInfo.FOVInfos.Reverse();

                        _sgGoCommands.Enqueue(grabInfo);

                    }
                    else //複數Block(stripe)
                    {
                        var grabInfo = CurrentTrayCarrier.Tray.GenGrabInfo(
                          VisionController.MachineControlConfig.SGTimeOut, //timeout
                          CurrentTrayCarrier.Count + otherTimes, //curTContainer.index,//第幾個Tray ，otherTimes如果有教讀會加TeachInspectTimes - 1，沒教讀就是0
                          i + 1, //Col,GenGrabInfo是從1開始
                          CurrentTrayCarrier.InspectData.InspectionPostion.MoreBlockXMaxStepCount,//一個Row要拍幾次
                          CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount * CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount,
                          CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount,
                          false, true, 0, 0,
                          CurrentTrayCarrier.InspectData.InspectionPostion.BlockNum_X,
                          CurrentTrayCarrier.InspectData.InspectionPostion.BlockNum_Y);

                        if (i % 2 == 1) //偶數排做反向
                            grabInfo.FOVInfos.Reverse();

                        _sgGoCommands.Enqueue(grabInfo);
                    }


                }
            }


            e.ProcessSuccess = true;
        }

        private void 建立Single_SG_Quene_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--建立Single_SG_Queue_ProcessIn--");

            //另外計算教讀跑檢測的次數 (因教讀檢測是同一個Tray，因此次數另外計算，不能將資訊蓋到下一盤貨)
            int otherTimes = 0;
            if (TeachInspectTimes > 0)
            {
                otherTimes = TeachInspectTimes - 1;
            }

            //確認這盤的Row Col
            for (int i = 0; i < SingleInspectPos.Count; i++)
            {
                var grabInfo = CurrentTrayCarrier.Tray.GenSingleGrabInfo(
                                  VisionController.MachineControlConfig.SGTimeOut * 1000,
                                CurrentTrayCarrier.Count + otherTimes,
                                (int)SingleInspectPos[i].x,//當下Row
                                (int)SingleInspectPos[i].y,//當下Col
                                CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount * CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount, //一個FOV有幾顆
                                CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount
                                );

                _sgGoCommands.Enqueue(grabInfo);
            }
            e.ProcessSuccess = true;
        }

        private void 讀取IR1資訊_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--單顆檢測流程_ProcessIn--");


            bool isOnline = false;
            DoIR1(e);
            /*
                        _wcfClient.Call((s) =>
                        {
                            if (s.IsOnline() == false)
                            {
                                //如果下線，就要上線
                                s.SetupFlowNumber(CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount); //FN
                                s.SetFlowRunnerNumber(1); //IR
                                if (ProductParam.UseMosaic == false &&
                                _preBoatCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                                {
                                    s.SetMosaicIsOpen(false);
                                }
                                s.Online(); //NL  
                            }
                            else
                            {
                                if (((MainController)VisionController).MachineControlConfig.FlowRunnerNum == 1)
                                {
                                    //如果上次FlowRunnerNum=1，代表上次重測，不需要重新設定
                                }
                                else
                                {
                                    //如果上次FlowRunnerNum!=1，代表上次是正常流程，需要下線在上線
                                    s.Offline();
                                    s.SetupFlowNumber(CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount); //FN
                                    s.SetFlowRunnerNumber(1); //IR
                                    if (ProductParam.UseMosaic == false &&
                                _preBoatCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                                    {
                                        s.SetMosaicIsOpen(false);
                                    }
                                    s.Online(); //NL  
                                }
                            }
                            isOnline = SpinWait.SpinUntil(() => s.IsOnline() || e.Executor.CancelToken.IsCancellationRequested, Param.InspectOnlineTimeout * 1000);

                            if (isOnline)
                            {
                                if (e.Executor.CancelToken.IsCancellationRequested)
                                {
                                    e.ProcessSuccess = false;
                                    return;
                                }
                                e.ProcessSuccess = isOnline;
                            }
                            else
                            {
                                LogTrace($"上線失敗，isOnline:{isOnline}");
                                e.ProcessSuccess = false;
                            }
                        });
                   */
        }
        public void DoIR1(ProcessArgs e)
        {
            LogTrace("DoIR1 start");
            bool isOnline = false;
            _wcfClient.Call((s) =>
            {
                if (s.IsOnline() == false)
                {
                    //如果下線，就要上線
                    s.SetupFlowNumber(CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                    s.SetFlowRunnerNumber(1); //IR
                    s.SetMosaicIsOpen(false);
                    s.Online(); //NL  
                }
                else
                {
                    if (((MainController)VisionController).MachineControlConfig.FlowRunnerNum == 1)
                    {
                        //如果上次FlowRunnerNum=1，代表上次重測，不需要重新設定
                    }
                    else
                    {
                        //如果上次FlowRunnerNum!=1，代表上次是正常流程，需要下線在上線
                        s.Offline();
                        s.SetupFlowNumber(CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                        s.SetFlowRunnerNumber(1); //IR
                        s.SetMosaicIsOpen(false);
                        s.Online(); //NL  
                    }
                }
                isOnline = SpinWait.SpinUntil(() => s.IsOnline() || e.Executor.CancelToken.IsCancellationRequested, Param.InspectOnlineTimeout * 1000);

            });

            if (isOnline)
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = isOnline;
            }
            else
            {
                LogTrace($"上線失敗，isOnline:{isOnline}");
                e.ProcessSuccess = false;
                return;
            }

            bool isOnlineMosaic = false;
            _wcfClientMosaic.Call((s) =>
            {
                if (s.IsOnline() == false)
                {
                    //如果下線，就要上線
                    s.SetupFlowNumber(CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                    s.SetFlowRunnerNumber(1); //IR
                    s.Online(); //NL  
                }
                else
                {
                    if (((MainController)VisionController_Mosaic).MachineControlConfig.FlowRunnerNum == 1)
                    {
                        //如果上次FlowRunnerNum=1，代表上次重測，不需要重新設定
                    }
                    else
                    {
                        //如果上次FlowRunnerNum!=1，代表上次是正常流程，需要下線在上線
                        s.Offline();
                        s.SetupFlowNumber(CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                        s.SetFlowRunnerNumber(1); //IR
                        s.Online(); //NL  
                    }
                }
                isOnlineMosaic = SpinWait.SpinUntil(() => s.IsOnline() || e.Executor.CancelToken.IsCancellationRequested, Param.InspectOnlineTimeout * 1000);

            });

            if (isOnlineMosaic)
            {
                if (e.Executor.CancelToken.IsCancellationRequested)
                {
                    e.ProcessSuccess = false;
                    return;
                }
                e.ProcessSuccess = isOnlineMosaic;
            }
            else
            {
                LogTrace($"上線失敗，isOnline:{isOnline},isOnlineMosaic:{isOnlineMosaic}");
                e.ProcessSuccess = false;
            }

            LogTrace("DoIR1 end");
        }

        private void 建立Grab_Quene_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            //todo check
            LogTrace($"--建立Grab_Quene_ProcessIn--");
            IsMosaicSG = false;
            IsPartSG = false;
            //看當下的sgCommand的Row ( 起始點(1,1)→(0,0) )
            CurrentRow = _sgGoCommands.Peek().FOVInfos[0].FovRow - 1;
            CurrentCol = _sgGoCommands.Peek().FOVInfos[0].FovCol - 1;

            _grabCommands.Clear();
            _grabCommandsMosaic.Clear();


            LogTrace($"_sgGoCommands CurrentRow:{CurrentRow} ,CurrentCol:{CurrentCol}");

            if (_isReinspectFlag || ProductParam.IsPick)  //重檢 或 抽檢
            {
                //一顆一顆放

                _grabCommands.Enqueue(new Point2d(CurrentRow, CurrentCol));  //去程

                //_grabBackCommands.Enqueue(new Point2d(CurrentRow, CurrentCol));  //回程

                //Mon add20260223
                //_grabLaserCommands.Enqueue(new Point2d(CurrentRow, CurrentCol));

                _grabCommandsMosaic.Enqueue(new Point2d(CurrentRow, CurrentCol));
            }
            else  //全檢
            {
                //將同一檢測Col的Row資訊放進Quene裡

                int grabCols = CurrentTrayCarrier.InspectData.InspectionPostion.MoreBlockXMaxStepCount;

                if (CurrentRow % 2 == 0)  //奇數排順向拍攝
                {
                    for (int i = 0; i < grabCols; i++)
                        _grabCommands.Enqueue(new Point2d(CurrentRow, i)); //Step模式把要做的IP先建出來
                }
                else if (CurrentRow % 2 == 1) //偶數排反向拍攝
                {
                    for (int i = grabCols - 1; i >= 0; i--)
                        _grabCommands.Enqueue(new Point2d(CurrentRow, i)); //Step模式把要做的IP先建出來
                }
            }
            StatisticTableViewModel2?.InitData();
            e.ProcessSuccess = true;
        }

        internal static class VisionSG_Queue
        {
            /// <summary>
            /// 建立檢測結果元件資訊
            /// </summary>
            /// <param name="controller"></param>
            /// <returns></returns>
            internal static List<CustomFetcherInfo> BulidSG(IMainController controller)
            {
                //紀錄需取得檢測結果元件資訊
                List<CustomFetcherInfo> customFetcherLis = new List<CustomFetcherInfo>();

                //拿取Flow元件名稱
                ((MainController)controller).FlowHandle.GetCmpNames();

                //拿取Flow元件數量
                int componentCount = ((MainController)controller).FlowHandle.Count;

                for (int i = 0; i < componentCount; i++)
                {
                    //拿取元件資訊
                    var component = ((MainController)controller).FlowHandle[i];
                    //元件GUID
                    var guid = component.GetGuid;
                    //有Active Spec提取
                    VisionComponent.SpecStructure specs = component.QuerySpecs(true);

                    if (specs.ComponentTypeName.ToLower().Contains("barcode"))
                    {
                        customFetcherLis.Add(new CustomFetcherInfo() { ComponentId = guid, DataName = "Info" });
                    }

                    foreach (var spec in specs.Specs)
                    {
                        //Spec名稱(在元件輸出結果Spec Name)
                        string specName = spec.Name;
                        //填入拿取元件資料
                        customFetcherLis.Add(new CustomFetcherInfo() { ComponentId = guid, DataName = specName });
                    }
                }

                return customFetcherLis;
            }

            /// <summary>
            /// Golden 模式下產生每一排的GrabInfos
            /// </summary>
            /// <param name="isSmallGolden"></param>
            /// <param name="trayIndex"></param>
            /// <param name="_controller"></param>
            /// <returns></returns>
            internal static List<GrabInfos> GenGoldenGrabInfos(bool isSmallGolden, int trayIndex, IMainController _controller)
            {
                var customFetcherLis = VisionSG_Queue.BulidSG(_controller);
                List<GrabInfos> rowGrabInfos = new List<GrabInfos>();//每排
                List<FOVInfo> infos = new List<FOVInfo>();//每排的每Fov

                if (isSmallGolden)
                {
                    FOVInfo info = new FOVInfo();
                    info = new FOVInfo() // 0.4x0.4 Golden FOV一次拍完全部(3x7顆，中間那排沒有Die還是有算進去)
                    {
                        FovRow = 1,
                        FovCol = 1,
                        ICs = new List<ICInfo>(),
                        TrayIndex = trayIndex
                    };

                    for (int y = 1; y <= 3; y++)
                    {
                        for (int x = 7; x >= 1; x--)
                        {
                            info.ICs.Add(new ICInfo()
                            {
                                Tray = trayIndex,
                                Row = y,
                                Col = x,
                                Inspect = true,
                                CustomFetcherInfo = customFetcherLis
                            });
                        }
                    }

                    infos.Add(info);
                    GrabInfos grabInfos = new GrabInfos()
                    {
                        Id = new Guid(),
                        TimeOut = 30000,
                        FOVInfos = infos
                    };

                    rowGrabInfos.Add(grabInfos);
                }
                else
                {
                    for (int y = 1; y <= 2; y++)
                    {
                        List<FOVInfo> infos_14x14 = new List<FOVInfo>();//每排的每Fov
                        for (int x = 1; x <= 7; x++)
                        {
                            FOVInfo info_14x14 = new FOVInfo() // 14x14 Golden FOV一次只拍一顆，7x2顆
                            {
                                FovRow = y,
                                FovCol = x,
                                ICs = new List<ICInfo>(),
                                TrayIndex = trayIndex
                            };

                            info_14x14.ICs.Add(new ICInfo()
                            {
                                Tray = trayIndex,
                                Row = y,
                                Col = x,
                                Inspect = true,
                                CustomFetcherInfo = customFetcherLis
                            });

                            infos_14x14.Add(info_14x14);
                        }

                        GrabInfos grabInfos = new GrabInfos()
                        {
                            Id = new Guid(),
                            TimeOut = 30000,
                            FOVInfos = infos_14x14
                        };

                        rowGrabInfos.Add(grabInfos);
                    }

                }


                return rowGrabInfos;
            }
        }

        bool _cvAndVisionTest = false;
        private void 讀取SG資訊_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--讀取SG資訊_ProcessIn--");
            if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            {
                return;
            }
            AllCaptureDone = false;
            if (_sgGoCommands.Count == 0)
            {
                LogTrace("sgCommands為空");
                //var service = this.GetHtaService<IDialogService>();
                //var result = service.ShowDialog(this, new ShowDialogArgs()
                //{
                //    Button = MessageBoxButtons.OK,
                //    Caption = Resources.Cap_Alarm,
                //    Message = Resources.Alarm_sgCommands為空
                //});
                //LogTrace($"result={result}");
                _errorMessage = Resources.Alarm_sgCommands為空;
                AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_SG_Fail", AlarmActive = true });
                e.ProcessSuccess = false;
                return;
            }

            GrabInfos curGrabInfo;
            curGrabInfo = _sgGoCommands.Peek();
            List<CustomFetcherInfo> CustomInfo = VisionSG_Queue.BulidSG(VisionController);
            foreach (var fov in curGrabInfo.FOVInfos)
            {
                foreach (var ic in fov.ICs)
                {
                    ic.CustomFetcherInfo = CustomInfo;
                }

            }

            LogTrace($"_sgGoCommands FovRow:{curGrabInfo.FOVInfos[0].FovRow} ,_sgGoCommands FovCol:{curGrabInfo.FOVInfos[0].FovCol}");

            bool ret = false;

            if (!_wcfClient.IsServerOnline)
            {
                _wcfClient.Dispose();
                _wcfClient =
                    new WcfDuplexClient<IVisionServerWcf, IVisionServerCallback>(Param.Port, _client,
                        timeout: Param.Timeout);
                _wcfClient.Open();
            }

            if (bVirtual)
            {
                if (!VisionController.Framer.HaveHardware)
                {
                    //沒有硬體的話，就是用離線匯圖的方法
                    int forwardCap = 1;
                    int backwardCap = 1;

                    forwardCap = VisionController.ProductSetting.RoundSettings2[0].GetAllCaptures().ToList().Count;
                    if (VisionController.ProductSetting.RoundSettings2.Count > 1)
                    {
                        backwardCap = VisionController.ProductSetting.RoundSettings2[1].GetAllCaptures().ToList().Count;
                    }


                    if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
                    {
                        if (Is3DGolden)
                        {
                            OfflineImagePath = @"D:\MyTest\TA2000\Golden3D";
                        }
                        else if (IsGolden)
                        {
                            OfflineImagePath = @"D:\MyTest\TABig_m1x2_p1x3";
                        }
                        else
                        {
                            OfflineImagePath = @"D:\MyTest\TestImageStripe";
                        }

                    }
                    else
                    {
                        OfflineImagePath = @"D:\MyTest\TABig_m1x2_p1x3";
                        //OfflineImagePath = @"D:\TA1000\LI110x110_MosaicBurn";
                    }

                    //新版離線測試
                    _wcfClient.Call(s =>
                    {
                        if (CurrentTrayCarrier.InspectData.InspectionPostion.ProductType == ProductTypeEm.SmallProduct)
                        {
                            //currentRow從1開始
                            if (VisionController.ProductSetting.RoundSettings2.Count > 1)
                            {
                                s.SetVirtualFramerLoadPath(OfflineImagePath, CurrentTrayCarrier.Count, CurrentRow + 1,
                                CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount, new int[] { forwardCap, backwardCap }, null, false); //離線測試用
                            }
                            else
                            {
                                s.SetVirtualFramerLoadPath(OfflineImagePath, CurrentTrayCarrier.Count, CurrentRow + 1,
                            CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount, new int[] { forwardCap }, null, false); //離線測試用
                            }
                        }
                        else
                        {
                            var mosaicCount = CurrentTrayCarrier.InspectData.InspectionPostion.MosaicYCount * CurrentTrayCarrier.InspectData.InspectionPostion.MosaicZCount;
                            if (VisionController.ProductSetting.RoundSettings2.Count > 1)
                            {
                                s.SetVirtualFramerLoadPathMosaic(OfflineImagePath, CurrentTrayCarrier.Count, CurrentRow + 1,
                                CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount, new int[] { forwardCap, backwardCap }, mosaicCount, false); //離線測試用
                            }
                            else
                            {
                                s.SetVirtualFramerLoadPathMosaic(OfflineImagePath, CurrentTrayCarrier.Count, CurrentRow + 1,
                                CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount, new int[] { forwardCap }, mosaicCount, false); //離線測試用
                            }
                        }

                    });
                }
            }


            //SG
            _wcfClient.Call(s =>
            {
                ret = s.StartGrab(curGrabInfo);
            });


            if (ret == true)
            {
                e.ProcessSuccess = true;

                _sgGoCommands.Dequeue();
            }
            else
            {
                //AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Vision_SG_Fail", AlarmActive = true });
                LogTrace("SG fail");

                _errorMessage = Resources.Alarm_SgFailed;
                AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_SG_Fail", AlarmActive = true });
                e.ProcessSuccess = false;

            }
        }

        private void MainFlow_Node1_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            e.ProcessSuccess = true;
        }

        private void 是否為大產品_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace("是否為大產品:" + CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType().ToString());

            if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                e.Result = true;  //大產品
            else
                e.Result = false;  //小產品
        }


        /// <summary>
        /// Camera拍照失敗時Retry重啟次數
        /// </summary>
        public int cameraCheckTimes = 0;


        private void 此回合是否拍攝完畢_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace("第幾個Row:" + CurrentRow);

            LogTrace($"當下觸發數量:{VisionController.Trigger1.GetTriggerCount()}，當下Framer取像張數:{VisionController.Framer.Count}");

            if (VisionController.Framer.Grabbers.Count > 0)
            {
                //LogTrace($"Grabber取像張數:{mainController.Framer.Grabbers[0].GetGrabCount()}");
                LogTrace($"Grabber取像張數:{VisionController.Framer.Grabbers[0].GrabCount}");//待測試，如果要用最新的Framer的話，再換成這段
            }

            LogTrace($"當下觸發數量:{VisionController_Mosaic.Trigger1.GetTriggerCount()}，當下Framer取像張數:{VisionController_Mosaic.Framer.Grabbers[0].GrabCount}");
            if (VisionController_Mosaic.Framer.Grabbers.Count > 0)
            {
                LogTrace($"Grabber取像張數:{VisionController_Mosaic.Framer.Grabbers[0].GrabCount}");
            }
            if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            {
                if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType == "Mosaic"))
                {
                    e.Result = _grabCommandsMosaic.Count == 0;
                    return;
                }

                if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType != "Mosaic"))
                {
                    e.Result = _grabCommands.Count == 0;
                    return;
                }

                e.Result = _grabCommands.Count == 0 && _grabCommandsMosaic.Count == 0;
            }
            else
            {
                e.Result = _grabCommands.Count == 0;
            }
           
        }

        private void 是否全部產品拍攝完成_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            if (_sgGoCommandsMosaic != null && _sgGoCommandsMosaic.Count > 0)
                _sgGoCommandsMosaic.Dequeue();
            if (_sgGoCommands != null && _sgGoCommands.Count > 0)
                _sgGoCommands.Dequeue();


            LogTrace("第幾個Row:" + CurrentRow + ",第幾個Col:" + CurrentCol);
            _totalTriggerCount += VisionController.Trigger1.GetTriggerCount();
            var camCapture = ((StationFramer)VisionController.Framer).EachCameraCaptureCount();
            Camera0CaptureCount += camCapture[0];

            _totalTriggerCountMosaic += VisionController_Mosaic.Trigger1.GetTriggerCount();
            var camCaptureMosaic = ((StationFramer)VisionController_Mosaic.Framer).EachCameraCaptureCount();
            Camera0CaptureCountMosaic += camCaptureMosaic[0];

            if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            {
                if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType == "Mosaic"))
                {
                    if (_sgGoCommandsMosaic.Count == 0)
                        e.Result = true;
                    else
                        e.Result = false;
                    return;
                }
                if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType != "Mosaic"))
                {
                    if (_sgGoCommands.Count == 0)
                        e.Result = true;
                    else
                        e.Result = false;
                    return;
                }

                if (_sgGoCommands.Count == 0 && _sgGoCommandsMosaic.Count == 0)
                    e.Result = true;
                else
                    e.Result = false;
            }
            else
            {
                if (_sgGoCommands.Count == 0)
                    e.Result = true;
                else
                    e.Result = false;
            }
        }

        private void 等待所有結果產出_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--等待所有結果產出_ProcessIn--");

            //檢測流程為Pass
            if (CurrentTrayCarrier.IsErrorPassInspect == true)
            {
                LogTrace($"流程被Pass，IsErrorPassInspect:{CurrentTrayCarrier.IsErrorPassInspect}");

                CurrentTrayCarrier.InspectData.InspectDone = true;

                IsPassVision = true;

                //將檢測Product之結果設定Fail
                if (_isReinspectFlag || ProductParam.IsPick)
                {
                    var fails = SingleInspectPos;
                    for (int i = 0; i < fails.Count; i++)
                    {
                        CurrentTrayCarrier.ProductInfos[(int)fails[i].y][(int)fails[i].x].InspectResult = new ICInfo()
                        {
                            Tray = CurrentTrayCarrier.Count,
                            Row = (int)fails[i].y,
                            Col = (int)fails[i].x,
                            Result = ProductResultEm.Fail
                        };
                    }
                }
                else
                {
                    for (int i = 0; i < CurrentTrayCarrier.ProductInfos.Count; i++)
                    {
                        for (int j = 0; j < CurrentTrayCarrier.ProductInfos[i].Count; j++)
                        {
                            CurrentTrayCarrier.ProductInfos[i][j].InspectResult = new ICInfo()
                            {
                                Tray = CurrentTrayCarrier.Count,
                                Row = i,
                                Col = j,
                                Result = ProductResultEm.Fail
                            };

                        }
                    }
                }

                e.ProcessSuccess = true;
                return;
            }

            //---------下面為正常檢測流程------
            while (true)
            {
                var executor = (IControlFlowExecutor)e.Executor;
                bool inspectDone = false;
                bool queuerResult = false;
                if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                {
                    if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType == "Mosaic"))
                    {
                        inspectDone = executor.TakeFromQueue(_inspectionDoneMosaic, Param.DataTimeout * 1000, out queuerResult);
                    }
                    else if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType != "Mosaic"))
                    {
                        inspectDone = executor.TakeFromQueue(_inspectionDone, Param.DataTimeout * 1000, out queuerResult);
                    }
                    else
                    {
                        var inspectDoneMosaic = executor.TakeFromQueue(_inspectionDoneMosaic, Param.DataTimeout * 1000, out queuerResult);
                        inspectDone = executor.TakeFromQueue(_inspectionDone, Param.DataTimeout * 1000, out bool res2) && inspectDoneMosaic;
                    }
                }
                else
                {
                    inspectDone = executor.TakeFromQueue(_inspectionDone, Param.DataTimeout * 1000, out queuerResult);
                }

                if (inspectDone)
                {
                    CurrentTrayCarrier.InspectData.InspectDone = InspectionDone;

                    if (_isReinspectFlag || ProductParam.IsPick) //重檢 或 抽檢
                    {

                        for (int i = 0; i < SingleInspectPos.Count; i++)
                        {
                            for (int k = 0; k < FinalResults[i].FOVInfos[0].ICs.Count; k++)
                            {
                                var icRow = FinalResults[i].FOVInfos[0].ICs[k].Row;
                                var icCol = FinalResults[i].FOVInfos[0].ICs[k].Col;
                                if (FinalResults[i].FOVInfos[0].ICs[k].Inspect == false)
                                {
                                    continue;
                                }
                                CurrentTrayCarrier.ProductInfos[(int)icRow][(int)icCol].InspectResult = FinalResults[i].FOVInfos[0].ICs[k];

                            }

                            for (int k = 0; k < FinalResultsMosaic[i].FOVInfos[0].ICs.Count; k++)
                            {
                                var icRow = FinalResultsMosaic[i].FOVInfos[0].ICs[k].Row;
                                var icCol = FinalResultsMosaic[i].FOVInfos[0].ICs[k].Col;
                                if (FinalResultsMosaic[i].FOVInfos[0].ICs[k].Inspect == false)
                                {
                                    continue;
                                }
                                CurrentTrayCarrier.ProductInfos[(int)icRow][(int)icCol].InspectResultMosaic = FinalResultsMosaic[i].FOVInfos[0].ICs[k];

                            }
                        }

                    }
                    else  //全檢
                    {
                        int blockWidth = CurrentTrayCarrier.InspectData.InspectionPostion.BlockICNumCol;
                        int fovColsPerBlock = CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount;
                        int fovWidth = CurrentTrayCarrier.InspectData.InspectionPostion.FOVProductXnum;
                        try
                        {

                            for (int i = 0; i < FinalResults.Count; i++)
                            {
                                for (int j = 0; j < FinalResults[i].FOVInfos.Count; j++)
                                {
                                    for (int k = 0; k < FinalResults[i].FOVInfos[j].ICs.Count; k++)
                                    {
                                        var icRow = FinalResults[i].FOVInfos[j].ICs[k].Row;
                                        var icCol = FinalResults[i].FOVInfos[j].ICs[k].Col;
                                        if (fovWidth != 1)
                                        {
                                            icCol = GrabInfoChangeTrayCol(blockWidth, fovWidth, CurrentTrayCarrier.InspectData.InspectionPostion.BlockNum_X, icCol, fovColsPerBlock * CurrentTrayCarrier.InspectData.InspectionPostion.BlockNum_X);
                                        }

                                        if (FinalResults[i].FOVInfos[j].ICs[k].Inspect == false)  //是否進行檢測
                                        {
                                            continue;
                                        }
                                        CurrentTrayCarrier.ProductInfos[icRow][icCol].InspectResult = FinalResults[i].FOVInfos[j].ICs[k];

                                    }
                                }

                            }

                            for (int i = 0; i < FinalResultsMosaic.Count; i++)
                            {
                                for (int j = 0; j < FinalResultsMosaic[i].FOVInfos.Count; j++)
                                {
                                    for (int k = 0; k < FinalResultsMosaic[i].FOVInfos[j].ICs.Count; k++)
                                    {
                                        var icRow = FinalResultsMosaic[i].FOVInfos[j].ICs[k].Row;
                                        var icCol = FinalResultsMosaic[i].FOVInfos[j].ICs[k].Col;
                                        if (FinalResultsMosaic[i].FOVInfos[j].ICs[k].Inspect == false)  //是否進行檢測
                                        {
                                            continue;
                                        }
                                        CurrentTrayCarrier.ProductInfos[icRow][icCol].InspectResultMosaic = FinalResultsMosaic[i].FOVInfos[j].ICs[k];
                                    }
                                }
                            }

                        }
                        catch (Exception ee)
                        {
                            string wwww = ee.ToString();
                        }
                    }
                    e.ProcessSuccess = true;

                    TrayReport report = new TrayReport();
                    report.OnLog = OnAddTrace;
                    MainController mainController;
                    mainController = (MainController)VisionController;

                    MainController mainControllerMosaic = (MainController)VisionController_Mosaic;

                    int otherTimes = 0;
                    if (TeachInspectTimes > 0)
                    {
                        otherTimes = TeachInspectTimes - 1;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        report.GenTrayReport(mainController.CommonInfo.ComponentSaveSetting.path,
                            _curLotName, CurrentTrayCarrier.Count + otherTimes,
                            CurrentTrayCarrier, 1,
                            CurrentTrayCarrier.BoatBarcode, Param.TrayReportPath,
                            _failCmpNames);
                    });
                    //Task.Factory.StartNew(() =>
                    //{
                    //    WriteRecordStatistic();
                    //});

                    break;
                }
                else
                {
                    if (executor.CancelToken.IsCancellationRequested)
                    {
                        e.ProcessSuccess = false;
                        break;//暫停才會出來while迴圈 
                    }
                    else
                    {
                        AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_Inspect_Result_Timeout", AlarmActive = true });
                        LogTrace("檢測資料產出逾時(選擇Yes:重新等待檢測資料；選擇No:這盤重新檢測；選擇Cancel:略過這盤的檢測結果)");
                        NotifyRedLight?.Invoke(this, new RedLightOnArgs() { IsOn = true });

                        var service = this.GetHtaService<IDialogService>();
                        var result = service.ShowDialog(this, new ShowDialogArgs()
                        {
                            Button = MessageBoxButtons.YesNoCancel,
                            Caption = Resources.Cap_Alarm,
                            Message = Resources.Alarm_DataTimeout
                        });
                        NotifyGreenLight?.Invoke(this, new GreenLightOnArgs() { IsOn = true });
                        if (result == DialogResult.Yes)
                        {
                            LogTrace("使用者選擇:Yes");
                            AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_Inspect_Result_Timeout", AlarmActive = false });
                            continue;
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            CurrentTrayCarrier.IsErrorPassInspect = true;
                            e.ProcessSuccess = true;
                            IsPassVision = true;
                            LogTrace("使用者選擇:Cancel");
                            AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_Inspect_Result_Timeout", AlarmActive = false });
                            break;
                        }
                        else
                        {
                            DataTimeoutIsRetry = true;
                            _trayInQueue.Add(CurrentTrayCarrier.Tray);
                            e.ProcessSuccess = true;
                            IsPassVision = true;
                            LogTrace("使用者選擇:No");
                            AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_Inspect_Result_Timeout", AlarmActive = false });
                            break;
                        }
                    }
                }
            }
        }

        private void 確認產品結果是否OK_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--確認產品結果是否OK_ProcessIn--");

            CurrentTrayCarrier.InspectData.FailIndex = new List<Point2d>();
            if (CurrentTrayCarrier.IsErrorPassInspect)
            {
                e.ProcessSuccess = true;
                return;
            }

            List<Point2d> realFailIndex = new List<Point2d>(); //紀錄實際Fail位置 → 呈現使用
            List<Point2d> realInvalidIndex = new List<Point2d>(); //紀錄實際Invalid位置 → 呈現使用
            List<Point2d> failIndex = new List<Point2d>();  //紀錄總共的Fail的FovIndex (包含Fail&lInvalid) → 重檢使用

            for (int i = 0; i < FinalResults.Count; i++)
            {
                for (int j = 0; j < FinalResults[i].FOVInfos.Count; j++)
                {
                    for (int k = 0; k < FinalResults[i].FOVInfos[j].ICs.Count; k++)
                    {

                        if (FinalResults[i].FOVInfos[j].ICs[k].Inspect)
                        {

                            if (FinalResults[i].FOVInfos[j].ICs[k].Invalid.Count > 0)
                            {
                                realInvalidIndex.Add(new Point2d(FinalResults[i].FOVInfos[j].ICs[k].Row + 1, FinalResults[i].FOVInfos[j].ICs[k].Col + 1));
                            }
                            if (FinalResults[i].FOVInfos[j].ICs[k].FailIndex.Count > 0)
                            {
                                realFailIndex.Add(new Point2d(FinalResults[i].FOVInfos[j].ICs[k].Row + 1, FinalResults[i].FOVInfos[j].ICs[k].Col + 1));
                            }

                            //排除掉重複的FovRow、Col
                            var repeat = failIndex.Where(s => s.x == FinalResults[i].FOVInfos[j].FovRow && s.y == FinalResults[i].FOVInfos[j].FovCol);
                            if (repeat.ToList().Count == 0)
                                failIndex.Add(new Point2d(FinalResults[i].FOVInfos[j].FovRow, FinalResults[i].FOVInfos[j].FovCol));
                        }
                    }
                }
            }
            for (int i = 0; i < FinalResultsMosaic.Count; i++)
            {
                for (int j = 0; j < FinalResultsMosaic[i].FOVInfos.Count; j++)
                {
                    for (int k = 0; k < FinalResultsMosaic[i].FOVInfos[j].ICs.Count; k++)
                    {
                        //檢測有Fail 或 無效 的產品時進入
                        if (FinalResultsMosaic[i].FOVInfos[j].ICs[k].FailIndex.Count != 0 ||
                            FinalResultsMosaic[i].FOVInfos[j].ICs[k].Invalid.Count != 0)
                        {

                            if (FinalResultsMosaic[i].FOVInfos[j].ICs[k].Invalid.Count > 0)
                            {
                                var repeat2 = realInvalidIndex.Where(s => s.x == FinalResultsMosaic[i].FOVInfos[j].ICs[k].Row + 1 && s.y == FinalResultsMosaic[i].FOVInfos[j].ICs[k].Col + 1);
                                if (repeat2.ToList().Count == 0)
                                    realInvalidIndex.Add(new Point2d(FinalResultsMosaic[i].FOVInfos[j].ICs[k].Row + 1, FinalResultsMosaic[i].FOVInfos[j].ICs[k].Col + 1));
                            }
                            if (FinalResultsMosaic[i].FOVInfos[j].ICs[k].FailIndex.Count > 0)
                            {
                                var repeat3 = realFailIndex.Where(s => s.x == FinalResultsMosaic[i].FOVInfos[j].ICs[k].Row + 1 && s.y == FinalResultsMosaic[i].FOVInfos[j].ICs[k].Col + 1);
                                if (repeat3.ToList().Count == 0)
                                    realFailIndex.Add(new Point2d(FinalResultsMosaic[i].FOVInfos[j].ICs[k].Row + 1, FinalResultsMosaic[i].FOVInfos[j].ICs[k].Col + 1));
                            }

                            //排除掉重複的FovRow、Col
                            var repeat = failIndex.Where(s => s.x == FinalResultsMosaic[i].FOVInfos[j].FovRow && s.y == FinalResultsMosaic[i].FOVInfos[j].FovCol);
                            if (repeat.ToList().Count == 0)
                                failIndex.Add(new Point2d(FinalResultsMosaic[i].FOVInfos[j].FovRow, FinalResultsMosaic[i].FOVInfos[j].FovCol));
                        }
                    }
                }
            }
            CurrentTrayCarrier.InspectData.FailIndex = failIndex;
            string mess = "";
            for (int i = 0; i < failIndex.Count; i++)
            {
                mess += ",Row:" + failIndex[i].x + "Col:" + failIndex[i].y;
            }
            LogTrace("FailIndex:" + mess);

            string realFailMess = "";
            for (int i = 0; i < realFailIndex.Count; i++)
            {
                realFailMess += $"Row:{realFailIndex[i].x} Col:{realFailIndex[i].y},";
            }
            LogTrace($"實際位置的FailIndex: " + realFailMess);

            string realInvalidMess = "";
            for (int i = 0; i < realInvalidIndex.Count; i++)
            {
                realInvalidMess += $"Row:{realInvalidIndex[i].x} Col:{realInvalidIndex[i].y},";
            }
            LogTrace($"實際位置的InvalidIndex: " + realInvalidMess);

            LogTrace($"FailIndex Count:{CurrentTrayCarrier.InspectData.FailIndex.Count}");
        }

        private void 是否FailAlarm確認_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            AccumulateEachTrayFailAlarmCount += CurrentTrayCarrier.InspectData.FailIndex.Count;

            if (IsGolden)
            {
                e.Result = false;
                return;
            }
            //當User設定數量為0時，代表不做FailAlarm機制
            if (ProductParam.FailAlarmCount == 0)
            {
                LogTrace($"FailAlarm確認 : 不進行確認 (參數設定)");

                e.Result = false;
                return;
            }
            else
            {
                //當Fail數量大於設定的Alarm數量時，執行FailAlarm機制
                if (CurrentTrayCarrier.InspectData.FailIndex.Count > ProductParam.FailAlarmCount)
                {
                    LogTrace($"FailAlarm確認 : 進行確認。 (FailCount : {CurrentTrayCarrier.InspectData.FailIndex.Count})");

                    e.Result = true;
                }
                else
                {
                    LogTrace($"FailAlarm確認 : 不進行確認。 (FailCount : {CurrentTrayCarrier.InspectData.FailIndex.Count})");

                    e.Result = false;
                }
            }
            //每盤累計FailAlarm的數量
            if (ProductParam.AccumulateEachTrayFailAlarmCount == 0)
            {
                e.Result = false;
            }
            else
            {
                if (AccumulateEachTrayFailAlarmCount > ProductParam.AccumulateEachTrayFailAlarmCount)
                {
                    LogTrace($"FailAlarm確認 : 進行確認。 (FailCount : {AccumulateEachTrayFailAlarmCount})");
                    AccumulateEachTrayFailAlarmCount = 0;
                    e.Result = true;
                    return;
                }
                else
                {
                    LogTrace($"FailAlarm確認 : 不進行確認。 (FailCount : {AccumulateEachTrayFailAlarmCount})");

                    e.Result = false;
                }
            }



        }

        #region FailAlarm子流程 (Start)

        #endregion FailAlarm子流程 (End)

        private void 是否重測_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            if (CurrentTrayCarrier.IsRecheckTakeout) //若產品被取走，則跳過流程
            {
                LogTrace("產品已被取走，不進行重測流程");

                return;
            }
            else  //正常流程
            {
                if (ProductParam.IsReinspect)
                {
                    LogTrace("--重測流程啟動--");
                    LogTrace($"第{_currentReinspectCount + 1}次重測");

                    if (ProductParam.ReinspectCount <= _currentReinspectCount)
                    {
                        //重測次數已達上限
                        e.Result = false;
                        _isReinspectFlag = false;
                        return;
                    }

                    SingleInspectPos.Clear();
                    for (int i = 0; i < CurrentTrayCarrier.InspectData.FailIndex.Count; i++)
                    {
                        SingleInspectPos.Add(new Point2d(CurrentTrayCarrier.InspectData.FailIndex[i].x, CurrentTrayCarrier.InspectData.FailIndex[i].y));
                    }
                    if (SingleInspectPos.Count > 0)
                    {
                        _currentReinspectCount++;
                        _isReinspectFlag = true;
                        FinalResults.Clear();
                        InspectionDone = false;
                        e.Result = true;
                    }
                    else
                    {
                        e.Result = false;
                        _isReinspectFlag = false;
                    }
                }
                else
                {
                    LogTrace("--此產品檔設定不進行重測--");

                    e.Result = false;
                    _isReinspectFlag = false;
                }
            }
        }

        private void 通知流道模組_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("--通知流道Module_ProcessIn--");
            LogTrace($"DataTimeoutIsRetry:{DataTimeoutIsRetry}");

            //if(ProductParam.UseLaserMeasure)
            //    LaserReport();

            if (GoInspect == false && DataTimeoutIsRetry == false)//教讀結束
            {
                LogTrace($"頂升下降 Start");
                var velDefBZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
                bool moveSuccess2BZ1 = MoveAxis(BZ1_流道頂升升降軸, 0, velDefBZ1);
                if (moveSuccess2BZ1 == false)
                {
                    e.ProcessSuccess = false;
                    LogTrace($"頂升下降 失敗");
                    return;
                }
                LogTrace($"頂升下降 End");

                NotifyFlow?.Invoke(this, CurrentTrayCarrier);
                UPHStopwatch.Stop();
                FinalResults.Clear();
                NotifySpendTime?.Invoke(this, new SpendTimeArgs() { Time = UPHStopwatch.Elapsed, TotalCount = StatisticView==null?0: StatisticView.AllCounts.ViewFormDatas[0].Total });
                UPHStopwatch.Reset();
                SendEvent?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Event_Inspection_Inspect_End" });
                OnFinishProcessJob?.Invoke(ControlJobId, ProcessJobId, ProductName);
            }
            else//正在跑教讀的檢測
            {
                FinalResults.Clear();
            }
            _currentReinspectCount = 0;
            DataTimeoutIsRetry = false;
            InspectionDone = false;
            e.ProcessSuccess = true;
        }

        #endregion 主流程 (End)

        #region 子流程(Start)

        #region 小產品拍攝子流程 (Start)
        private void 是否飛拍_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            if (ProductParam.IsSetFly && ProductParam.IsPick == false && _isReinspectFlag == false &&
             CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
            {
                LogTrace($"--流程使用飛拍--");

                e.Result = true;
            }
            else
            {
                LogTrace($"--流程使用點拍--");

                e.Result = false;
            }
        }

        private void 移動點拍起始位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--移動點拍起始位置_ProcessIn--");

            _grabIndex = _grabCommands.Peek();

            double distX = 0;
            double distY = 0;

            if (Is3DGolden)
            {
                var afterX = CurrentTrayCarrier.InspectData.InspectionPostion.SingleToGroupX((int)_grabIndex.y);
                var afterY = CurrentTrayCarrier.InspectData.InspectionPostion.SingleToGroupX((int)_grabIndex.x);
                var cornerX = MotorOffset.Golden3DCenterToCornerBX1;
                var cornerY = MotorOffset.Golden3DCenterToCornerAY1;
                distX = cornerX + CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[afterX];
                distY = cornerY + CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[afterY];
            }
            else
            {
                if (CurrentTrayCarrier.InspectData.InspectionPostion.SingleBlock)
                {
                    distX = MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[(int)_grabIndex.y];
                    distY = MotorOffset.InspStandBy_Y + CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[(int)_grabIndex.x];
                }
                else
                {
                    distX = MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._trayStepX[(int)_grabIndex.y];
                    distY = MotorOffset.InspStandBy_Y + CurrentTrayCarrier.InspectData.InspectionPostion._trayStepY[(int)_grabIndex.x];
                }

            }

            var fovCenter = new Point2d(distX, distY);


            var velDefY1 = AY1VelList.FirstOrDefault(x => x.VelocityName == ProductParam.InspectVel.ToString());
            var velDefX1 = BX1VelList.FirstOrDefault(x => x.VelocityName == ProductParam.InspectVel.ToString());
            var velDef_BZ = BZ1VelList.FirstOrDefault(x => x.VelocityName == ProductParam.InspectVel.ToString());
            //var velDefY1 = SelectVelDef(視覺縱移軸, AY1VelList);
            //var velDefX1= SelectVelDef(BX1_流道橫移軸, BX1VelList);

            bool moveSuccessX1 = MoveAxis(BX1_流道橫移軸, fovCenter.x, velDefX1.Velocity);
            bool moveSuccessY1 = MoveAxis(視覺縱移軸, fovCenter.y, velDefY1.Velocity);


            //對焦           
            var velDefAZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
            double AZ1Offset = MotorOffset.InspStandBy_Z + ProductParam.FocusLocation_Mon;
            bool moveSuccessAZ1 = MoveAxis(BZ1_流道頂升升降軸, AZ1Offset, velDefAZ1);


            var AX1_waitRes = BX1_流道橫移軸.WaitMotionDone();
            var BY1_waitRes = 視覺縱移軸.WaitMotionDone();
            var AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();

            if (AX1_waitRes && BY1_waitRes && AZ1_waitRes)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
                if (!AX1_waitRes) LogTrace($"移動點拍起始位置 失敗，移動:{moveSuccessX1},等待:{AX1_waitRes}");
                if (!BY1_waitRes) LogTrace($"移動點拍起始位置 失敗，移動:{moveSuccessY1},等待:{BY1_waitRes}");
                if (!AZ1_waitRes) LogTrace($"移動點拍起始位置 失敗，移動:{moveSuccessAZ1},等待:{AZ1_waitRes}");
            }


            //e.ProcessSuccess = moveSuccessAX1 && moveSuccessBY1 && moveSuccessAZ1;



            if (e.ProcessSuccess == true)
            {
                _grabIndex = _grabCommands.Dequeue();
            }
        }

        private void 下IP_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--小產品_下IP_ProcessIn--");

            for (int RoundIndex = 0; RoundIndex < VisionController.ProductSetting.RoundSettings2.Count; RoundIndex++)
            {
                int groupCount = VisionController.ProductSetting.RoundSettings2[RoundIndex].Groups.Count;

                for (int index = 0; index < groupCount; index++)
                {

                    var velDefAZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
                    //double AZ1Offset = MotorOffset.InspStandBy_Z + ProductParam.FocusLocations[RoundIndex][index];
                    //bool moveSuccessAZ1 = MoveAxis(BZ1_流道頂升升降軸, AZ1Offset, velDefAZ1);
                    //var AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();


                    _wcfClient.Call(s =>
                    {
                        s.Grab(0, 0);
                    });
                    SpinWait.SpinUntil(() => false, 150);
                }
            }

            e.ProcessSuccess = true;
        }

        private void 移至飛拍起始位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--移至飛拍起始位置_ProcessIn--");

            var commands = _grabCommands.First();
            var distX = MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[(int)commands.y];
            var distY = MotorOffset.InspStandBy_Y + CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[(int)commands.x];
            var fovCenter = new Point2d(distX,
                                        distY);

            //需要離起點前面一點距離            
            double offset = 0;

            if (CurrentRow % 2 == 0)
            {
                offset = 2;
            }
            else
            {
                offset = -2;
            }

            var velDefAX1 = SelectVelDef(BX1_流道橫移軸, AY1VelList);
            var velDefBY1 = SelectVelDef(視覺縱移軸, BX1VelList);

            bool moveSuccessAX1 = MoveAxis(BX1_流道橫移軸, distX + offset, velDefAX1);
            bool moveSuccessBY1 = MoveAxis(視覺縱移軸, distY, velDefBY1);

            //對焦           
            var velDefAZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
            double AZ1Offset = MotorOffset.InspStandBy_Z + ProductParam.FocusLocations[0][0];
            bool moveSuccessAZ1 = MoveAxis(BZ1_流道頂升升降軸, AZ1Offset, velDefAZ1);



            var AX1_waitRes = BX1_流道橫移軸.WaitMotionDone();
            var BY1_waitRes = 視覺縱移軸.WaitMotionDone();
            var AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();

            if (AX1_waitRes && BY1_waitRes && AZ1_waitRes)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
                if (!AX1_waitRes) LogTrace($"移至飛拍起始位置 失敗，移動:{moveSuccessAX1},等待:{AX1_waitRes}");
                if (!BY1_waitRes) LogTrace($"移至飛拍起始位置 失敗，移動:{moveSuccessBY1},等待:{BY1_waitRes}");
                if (!AZ1_waitRes) LogTrace($"移至飛拍起始位置 失敗，移動:{moveSuccessAZ1},等待:{AZ1_waitRes}");
            }



            //e.ProcessSuccess = moveSuccessAX1 && moveSuccessBY1 && moveSuccessAZ1;
        }

        private void 建立TriggerTable_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--建立TriggerTable_ProcessIn--");

            EncoderSettingContract triggerTable = new EncoderSettingContract();
            triggerTable.Settings = new EncoderSetting();
            triggerTable.Settings.Encoder = 0;//use trigger1
            double diffX = 0;

            if (視覺縱移軸 is AxisBase axis)  //飛拍軸，判斷Motion檔是否將此參數與實體連接
            {
                //橫移相機軸進行檢測影像拍攝
                if (CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount > 1)
                {
                    diffX = CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[1] - CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[0];
                }

                triggerTable.Settings.StartPos = (int)((MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX.Length - 1]) / (axis.OriginalMotorResolution + axis.MotorResolutionOffset));
                triggerTable.Settings.Pitch = (int)(diffX / (axis.OriginalMotorResolution + axis.MotorResolutionOffset));
            }

            triggerTable.Settings.Count = CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount;
            if (CurrentRow % 2 == 0)
            {
                triggerTable.Settings.Mode = 1;
            }
            else
            {
                triggerTable.Settings.Mode = 0;
            }

            var ret = false;
            _wcfClient.Call(s =>
            {
                ret = s.SetEncoder(triggerTable);
            });

            if (ret == false)
                LogTrace($"建立TriggerTable:{ret}");
        }

        private void 移至飛拍結束位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--移至飛拍結束位置_ProcessIn--");

            if (_grabCommands.Count == 0)
            {
                LogTrace($"移至飛拍結束位置 Failed - _grabCommands.Count == 0");

                e.ProcessSuccess = false;
                return;
            }

            var commands = _grabCommands.Last();
            var distX = MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[(int)commands.y]; //視覺橫移軸
            var distY = MotorOffset.InspStandBy_Y + CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[(int)commands.x]; //流道縱移軸

            var fovCenter = new Point2d(distX, distY);

            //需要離終點後面一點距離            

            double offset = -2;

            if (CurrentRow % 2 == 0)
            {
                offset = -2;
            }
            else
            {
                offset = 2;
            }

            //新速度寫法
            var velDefAX1 = SelectVelDef(視覺縱移軸, AY1VelList, true);
            var finalFlyVel = velDefAX1.GetClone();
            finalFlyVel.MaxVel = (finalFlyVel.MaxVel - finalFlyVel.StartVel) * ProductParam.FlyPercent + finalFlyVel.StartVel;
            bool moveSuccessX = MoveAxis(視覺縱移軸, distX + offset, finalFlyVel);
            var AX1_waitRes = 視覺縱移軸.WaitMotionDone();

            _grabCommands.Clear();
            e.ProcessSuccess = AX1_waitRes;
        }
        #endregion 小產品拍攝子流程 (End)

        #region 大產品拍攝子流程 (Start)

        private void 移至拍攝起始位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"-- 移至拍攝起始位置_ProcessIn--");

            _grabIndex = _grabCommands.Peek();

            double distX = 0; //相機橫移軸
            double distY = 0;  //流道縱移軸


            distX = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].MosaicPosition[MosaicImgCount].x;
            distY = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].MosaicPosition[MosaicImgCount].y;

            //var distZ = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductY[(int)_grabIndex.x][MosaicImgCount/CurrentTrayCarrier.InspectData.InspectionPostion.MosaicZCount];//測試有多Y的mosaic時用

            var fovCenter = new Point2d(distX, distY);

            var velDefAX1 = SelectVelDef(BX1_流道橫移軸, AY1VelList);
            var velDefBY1 = SelectVelDef(視覺縱移軸, BX1VelList);

            bool moveSuccessAX1 = MoveAxis(BX1_流道橫移軸, fovCenter.x, velDefAX1);
            bool moveSuccessBY1 = MoveAxis(視覺縱移軸, fovCenter.y, velDefBY1);

            //對焦
            var velDefAZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
            double AZ1Offset = MotorOffset.InspStandBy_Z + ProductParam.FocusLocation_Mon;
            bool moveSuccessAZ1 = MoveAxis(BZ1_流道頂升升降軸, AZ1Offset, velDefAZ1);



            var AX1_waitRes = BX1_流道橫移軸.WaitMotionDone();
            var BY1_waitRes = 視覺縱移軸.WaitMotionDone();
            var AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();

            if (AX1_waitRes && BY1_waitRes && AZ1_waitRes)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
                if (!AX1_waitRes) LogTrace($"移至拍攝起始位置 失敗，移動:{moveSuccessAX1},等待:{AX1_waitRes}");
                if (!BY1_waitRes) LogTrace($"移至拍攝起始位置 失敗，移動:{moveSuccessBY1},等待:{BY1_waitRes}");
                if (!AZ1_waitRes) LogTrace($"移至拍攝起始位置 失敗，移動:{moveSuccessAZ1},等待:{AZ1_waitRes}");
            }

            //var result = moveSuccessAX1 && moveSuccessBY1 && moveSuccessAZ1;
            //e.ProcessSuccess = result;

        }

        private void 下IP拍照_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--下IP拍照_ProcessIn--");

            //int groupCount = VisionController.ProductSetting.RoundSettings2[0].Groups.Count;           
            int groupCount = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].GroupIndexes.Count;

            for (int i = 0; i < groupCount; i++)
            {
                _wcfClient.Call(s =>
                {
                    s.Grab(0, 0);
                });
                SpinWait.SpinUntil(() => false, 150);
            }
            e.ProcessSuccess = true;
        }

        private void 是否產品影像拍攝完成_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            MosaicImgCount++;
            //if (CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount * CurrentTrayCarrier.InspectData.InspectionPostion.MosaicYCount <= MosaicImgCount)
            if (ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].MosaicPosition.Count <= MosaicImgCount)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void 大產品拍攝完畢_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            MosaicImgCount = 0;

            _grabIndex = _grabCommands.Dequeue();

            LogTrace($"--大產品拍攝完畢--");
        }

        private void BigProduct_Node1_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            e.ProcessSuccess = true;
        }

        #endregion 大產品拍攝子流程 (End)

        #region FailAlarm子流程 (Start)

        private void Boat盤限制解除_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--Boat盤限制解除_ProcessIn--");

            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);
            Y067002_傳送流道_頂升真空電磁閥.SetIO(false);

            Y067002_傳送流道_頂升真空電磁閥.SetIO(false);
            var rlt = SpinWait.SpinUntil(() => !X066005_傳送流道_到位氣缸_上升.CheckIO() && X066006_傳送流道_到位氣缸_下降.CheckIO(), 10000);

            e.ProcessSuccess = true;
        }

        private void 流道軸移動至人員確認位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--流道軸移動至人員確認位置_ProcessIn--");

            var velDefBY1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);
            bool moveSuccessBY1 = MoveAxis(BX1_流道橫移軸, MotorOffset.FailAlarm_Y, velDefBY1);
            var BY1_waitRes = BX1_流道橫移軸.WaitMotionDone();

            e.ProcessSuccess = BY1_waitRes;
        }

        private void 人員確認_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--人員確認_ProcessIn--");
            AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_FailAlarm", AlarmActive = true });
            if (SettingService.SystemSetting.SelectVersion == "M00")
            {
                Y065000_設備安全門電磁鎖.SetIO(false);
            }
            else if (SettingService.SystemSetting.SelectVersion == "M01")
            {
                //M01
            }

            var service = this.GetHtaService<IDialogService>();
            NotifyRedLight?.Invoke(this, new RedLightOnArgs() { IsOn = true });
            var Result = service.ShowDialog(this, new ShowDialogArgs()
            {
                Button = MessageBoxButtons.OK,
                Caption = "Info",
                Message = "Fail數量超出設定，請人員確認結果。\n點擊OK後可開門進行確認流程。"
            });

            MessageBox.Show("確認完成後，請將門確實關上，並點擊\"確定\"，流程將繼續執行\n\n提醒：" +
                "\n若確認過程中有將門打開，則按下\"確定\"按鈕之後，需要再按\"執行\"按鈕，機台將會繼續作業" +
                "\n若確認過程中無將門打開，則按下\"確定\"按鈕之後，機台將會繼續作業",
                                "Info",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);

            while (!X064006_設備安全門關閉.CheckIO())
            {
                service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "Info",
                    Message = "請關上門，流程將繼續"
                });
                Thread.Sleep(100);
            }

            if (SettingService.SystemSetting.SelectVersion == "M00")
            {
                Y065000_設備安全門電磁鎖.SetIO(true);
            }
            else if (SettingService.SystemSetting.SelectVersion == "M01")
            {
                //M01
            }


            NotifyGreenLight?.Invoke(this, new GreenLightOnArgs() { IsOn = true });

            e.ProcessSuccess = true;
            AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_FailAlarm", AlarmActive = false });

        }

        private void 流道偵測產品是否存在_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            //轉動流道馬達偵測產品
            LogTrace("流道開始轉動偵測產品");
            BX2_流道傳送軸.ConstantMove(TVelEnum.Slow, TMotionDir.Forward);
            var rlt = SpinWait.SpinUntil(() => X066003_傳送流道_產品減速檢知.CheckIO() && X066004_傳送流道_產品到位檢知.CheckIO(), 10000);

            //確認結果
            if (rlt)
            {
                LogTrace("流道偵測到產品");
                Task.Delay(100).Wait();//延遲停止，讓產品靠近Stopbar
                BX2_流道傳送軸.Stop();//軸動停止
                e.ProcessSuccess = true;
            }
            else
            {
                LogTrace("流道無偵測到產品");
                BX2_流道傳送軸.Stop();//軸動停止
                _recheckNoProduct = true;
                CurrentTrayCarrier.IsRecheckTakeout = true;
                e.ProcessSuccess = true;
            }
        }

        private void 流道軸移動回檢測位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (CurrentTrayCarrier.IsRecheckTakeout)
                return;

            LogTrace($"--流道軸移動回檢測位置_ProcessIn--");

            var velDefBY1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);
            bool moveSuccessBY1 = MoveAxis(BX1_流道橫移軸, MotorOffset.InspStandBy_Y, velDefBY1);
            var BY1_waitRes = BX1_流道橫移軸.WaitMotionDone();

            e.ProcessSuccess = BY1_waitRes;
        }


        private void Boat盤限制開啟_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (CurrentTrayCarrier.IsRecheckTakeout)
                return;

            LogTrace($"--Boat盤限制開啟_ProcessIn--");

            if (_recheckNoProduct == false)
            {
                Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
                var rlt = SpinWait.SpinUntil(() => X066005_傳送流道_到位氣缸_上升.CheckIO() && !X066006_傳送流道_到位氣缸_下降.CheckIO(), 10000);

                Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
                Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
            }

            //參數初始化
            _recheckNoProduct = false;

            e.ProcessSuccess = true;
        }

        #endregion FailAlarm子流程 (End)

        #region Barcode拍攝子流程 (Start)
        private void 移至Barcode位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--移至Barcode位置_ProcessIn--");
            var velDefx = SelectVelDef(BX1_流道橫移軸, BX1VelList);

            bool moveSuccessX = MoveAxis(視覺縱移軸, MotorOffset.TrayBarcode_BX1, velDefx, 30000, 20000);
            var velDefy = SelectVelDef(視覺縱移軸, AY1VelList);
            bool moveSuccessY = MoveAxis(BX1_流道橫移軸, MotorOffset.TrayBarcode_AY1, velDefy);

            bool moveSuccessZ = true;
            if (Param.BarcodeCatchMode == BarcodeCatchModeEm.相機模式) //相機模式需要Z軸對焦移動
            {
                var velDefz = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
                moveSuccessZ = MoveAxis(BZ1_流道頂升升降軸, MotorOffset.TrayBarcode_BZ1, velDefz);
            }



            var AX1_waitRes = BX1_流道橫移軸.WaitMotionDone();
            var BY1_waitRes = 視覺縱移軸.WaitMotionDone();
            var AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();

            if (AX1_waitRes && BY1_waitRes && AZ1_waitRes)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
                if (!AX1_waitRes) LogTrace($"移至Barcode位置 失敗，移動:{moveSuccessX},等待:{AX1_waitRes}");
                if (!BY1_waitRes) LogTrace($"移至Barcode位置 失敗，移動:{moveSuccessY},等待:{BY1_waitRes}");
                if (!AZ1_waitRes) LogTrace($"移至Barcode位置 失敗，移動:{moveSuccessZ},等待:{AZ1_waitRes}");
            }




            // e.ProcessSuccess = moveSuccessX && moveSuccessY && moveSuccessZ;
        }

        private void 是否使用相機拍攝_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            LogTrace($"--Barcode拍攝模式：{Param.BarcodeCatchMode}--");

            if (Param.BarcodeCatchMode == BarcodeCatchModeEm.相機模式)
            {
                e.Result = true;
            }
            else if (Param.BarcodeCatchMode == BarcodeCatchModeEm.Barcode機模式)
            {
                e.Result = false;
            }
        }

        public string TempBarcodeID = null;
        private void Barcode機拍攝Barcode_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            bool rtv = false;
            TempBarcodeID = "Null";

            if (BarcodeReader is IKeyenceBarcodeReader keyenceBarcode)
            {
                //Retry三次
                for (int index = 0; index < 3; index++)
                {
                    rtv = keyenceBarcode.ScanBarcode(1000);

                    if (rtv == true) break;

                    SpinWait.SpinUntil(() => false, 300);
                }

                keyenceBarcode.CloseScanBarcode(1000);

                if (rtv == true)   //掃描成功，紀錄Barcode碼
                {
                    TempBarcodeID = keyenceBarcode.Barcode;

                    LogTrace($"Barcode偵測成功：{TempBarcodeID}");
                }
                else
                {
                    TempBarcodeID = "Null";

                    LogTrace($"連線失敗");
                }
            }

            e.ProcessSuccess = true;
        }

        public CamBarcode2 CamBarcodeRead;
        private void 相機拍攝Barcode_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--相機拍攝Barcode_ProcessIn--");

            BarcodeCamera_Catch();

            e.ProcessSuccess = true;
        }

        public string BarcodeCamera_Catch()
        {
            //初始BarCode
            Lv3Data Func(string name)
            {
                LVDATA.LvDataGlobal.SystemMgrLv3.GetLv3Data(name, out var result);
                return result;
            }

            //BarCode Reader實體化
            CamBarcodeRead = new CamBarcode2((HTA.IFramer.StationFramer)BarcodeController.Framer, BarcodeController.Lighter, BarcodeController.Trigger1, Func);

            //BarCode Reader讀取參數位置，並進行讀取
            if (CamBarcodeRead.ReadConfigFile($"D:\\Coordinator2.0\\Products\\{ProductName}\\{BarcodeStation}"))
            {
                //BarCode連線
                CamBarcodeRead.Connect();

                //拍照讀取BarCode
                var ret = CamBarcodeRead.DoCaptureBarcode(10000, out List<string> barcode);

                if (ret.Result)
                {
                    LogTrace("BarCode :" + barcode[0]);

                    if (barcode[0] == "Barcode-Fail")
                    {
                        TempBarcodeID = "Null";
                    }
                    else
                    {
                        TempBarcodeID = barcode[0];
                    }
                }
                else
                {
                    LogTrace("DoCaptureBarcode失敗");

                    TempBarcodeID = "Null";
                }

                CamBarcodeRead.Disconnect();
            }

            CamBarcodeRead.Dispose();

            return TempBarcodeID;
        }

        private void 是否讀取成功_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            if (TempBarcodeID != "Null")   //Barcode掃描成功
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }
        private void 手動輸入Barcode_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace("開啟BarCode手動輸入視窗");

            //this.GetHtaService<IShowFormDialogService>().ShowDialog(this, typeof(XtraBarCodeKeyInForm), new object[] { this });
            string sPromptInfo = "Enter BarCode Info";
            string sTitle = "BarCode Info:";
            string sDefaultResponse = @"DefaultResponse";
            //紅燈亮起
            NotifyRedLight?.Invoke(this, new RedLightOnArgs() { IsOn = true });
            var result = XtraInputBox.Show(sPromptInfo, sTitle, sDefaultResponse);
            if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result) || result == "")
            {
                MessageBox.Show("BarCode Input Null ,", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CurrentTrayCarrier.BoatBarcode = "BarCode_Null";//

            }
            else
            {
                CurrentTrayCarrier.BoatBarcode = result;
            }
            LogTrace($"BarCode input Info:{TempBarcodeID}");
            NotifyGreenLight?.Invoke(this, new GreenLightOnArgs() { IsOn = true });
            e.ProcessSuccess = true;
        }

        private void 儲存Barcode資訊_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

            TATool.BoatBarcode = TempBarcodeID;
        }

        #endregion Barcode拍攝子流程 (End)

        #endregion 子流程(End)


        #region Function (Start)

        public void LogTrace(string msg)
        {
            OnAddTrace("InspectionModule", msg);
            string timeStr = DateTime.Now.ToString("HH:mm:ss") + " : ";
            _showWorkLogService?.AddWorkLog(this, timeStr + msg);
        }

        public Velocity SelectVelDef(IAxis axis, List<VelocityData> velocityDatas, bool isFly = false)
        {
            VelocityData velDef = null;
            if (isFly) //飛拍
            {
                velDef = velocityDatas.FirstOrDefault(v => v.VelocityName == ProductParam.FlyVel.ToString());
            }
            else //點拍
            {
                velDef = velocityDatas.FirstOrDefault(v => v.VelocityName == ProductParam.InspectVel.ToString());
            }

            if (velDef == null)
                return axis.MoveVelocity;

            return velDef.Velocity;
        }

        public bool MoveAxis(IAxis axis, double dist, Velocity vel = null, int moveTimeout = 10000, int waitTimeout = 10000)
        {
            Velocity axisVel = new Velocity();
            bool isMoveSuccess = false;
            if (vel == null)
                axisVel = axis.MoveVelocity;
            else
                axisVel = vel;

            if (Math.Abs(axis.ActualPos - dist) < 2)
            {
                isMoveSuccess = true;
            }
            else
            {
                bool moveResult = axis.AbsoluteMove(dist, axisVel, moveTimeout);
                bool waitResult = axis.WaitMotionDone(waitTimeout);
                isMoveSuccess = moveResult && waitResult;
                LogTrace($"MoveAxis - Axis:{axis.Name}, dist:{dist}, ActualPos:{axis.ActualPos}, moveTimeout:{moveTimeout}, waitTimeout:{waitTimeout}, IsMoveSuccess:{isMoveSuccess}");

                if (isMoveSuccess == false)
                {
                    LogTrace($"MoveAxisFailed - Axis:{axis.Name}, dist:{dist}, ActualPos:{axis.ActualPos}, ALM:{axis.CurrentStatus.ALM}, INP:{axis.CurrentStatus.INP}, SVON:{axis.CurrentStatus.SVON}, EMG:{axis.CurrentStatus.EMG}, NSTP:{axis.CurrentMoveStatus.NSTP}, CSTP:{axis.CurrentMoveStatus.CSTP}");
                    LogTrace($"MoveAxisFailed - ErrorCode:{axis.ErrorCode.ToString()}, Message{axis.ErrorString}");
                }
            }

            return isMoveSuccess;
        }



        public void UpdateGridView(GrabInfos grabInfosResult, string inspectType)
        {
            int blockWidth = CurrentTrayCarrier.InspectData.InspectionPostion.BlockICNumCol;
            int fovColsPerBlock = CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount;
            int fovWidth = CurrentTrayCarrier.InspectData.InspectionPostion.FOVProductXnum;

            FolderList folderList = new FolderList();
            if (inspectType == "Mosaic")
            {
                folderList = ((Flow2)((MainController)VisionController_Mosaic).FlowHandle).GetFolderInfo();
            }
            else
            {
                folderList = ((Flow2)((MainController)VisionController).FlowHandle).GetFolderInfo();
            }

            for (int i = 0; i < grabInfosResult.FOVInfos.Count; i++)
            {
                IcObject icObject = new IcObject();

                for (int j = 0; j < grabInfosResult.FOVInfos[i].ICs.Count; j++)
                {
                    int productRow = grabInfosResult.FOVInfos[i].ICs[j].Row;
                    int productCol = grabInfosResult.FOVInfos[i].ICs[j].Col;
                    if (fovWidth != 1)
                    {
                        productCol = GrabInfoChangeTrayCol(blockWidth, fovWidth, CurrentTrayCarrier.InspectData.InspectionPostion.BlockNum_X, productCol, fovColsPerBlock * CurrentTrayCarrier.InspectData.InspectionPostion.BlockNum_X);
                    }
                    //如果一個FOV中，有Empty的  Inspect會=false
                    if (grabInfosResult.FOVInfos[i].ICs[j].Inspect == false)
                    {
                        continue;
                    }

                    if (CurrentTrayCarrier.Tray[productRow, productCol].ObjectInSlot is IcObject ic)
                    {
                        SingleVisionResult singleVisionResult = new SingleVisionResult();

                        //FailIndex 編號從0開始，能夠Fail的才會記上去(順序是從MappedList來的)
                        var failResult = grabInfosResult.FOVInfos[i].ICs[j].FailIndex.Count != 0;
                        singleVisionResult.PassFail = failResult == false;

                        //Invalid編號也是從0開始，但0固定是Input，1開始就是FlowForm上元件的排序下來
                        var invalidResult = grabInfosResult.FOVInfos[i].ICs[j].Invalid.Count != 0;
                        singleVisionResult.Valid = invalidResult == false;

                        if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                        {
                            UpdateBigMapResult(productRow, productCol, folderList, grabInfosResult.FOVInfos[i].ICs[j]);
                        }


                        VisionInspectInfo visionInspectInfo = new VisionInspectInfo();
                        visionInspectInfo.Results = new List<SingleVisionResult>();
                        visionInspectInfo.Results.Add(singleVisionResult);

                        if (ic.StationResults[0] != null)
                        {
                            var results0 = ic.StationResults[0];
                            //代表已經填過
                            //要重新填就是要比較糟糕才要填(Invalid > Fail > Pass)
                            if (visionInspectInfo.Valid == false)
                            {
                                //假如invalid，就要填
                                ic.StationResults[0] = visionInspectInfo;
                                ic.StationResults[0].Results = visionInspectInfo.Results;
                                icObject = ic;
                                //設定結果
                                CurrentTrayCarrier.Tray.SetObject(productRow, productCol, icObject);
                            }
                            else if (visionInspectInfo.Pass == false && (results0.Valid == true && results0.Pass == true))
                            {
                                //如果第二個fail，第一個pass，要填
                                ic.StationResults[0] = visionInspectInfo;
                                ic.StationResults[0].Results = visionInspectInfo.Results;
                                icObject = ic;
                                //設定結果
                                CurrentTrayCarrier.Tray.SetObject(productRow, productCol, icObject);
                            }
                        }
                        else
                        {
                            //還沒填就直接填
                            ic.StationResults[0] = visionInspectInfo;
                            ic.StationResults[0].Results = visionInspectInfo.Results;
                            icObject = ic;
                            //設定結果
                            CurrentTrayCarrier.Tray.SetObject(productRow, productCol, icObject);
                        }


                        icObject = ic;
                    }
                    //設定結果
                    CurrentTrayCarrier.Tray.SetObject(productRow, productCol, icObject);
                }
            }
            //更新TrayView顯示
            OnTrayIn(CurrentTrayCarrier.Tray);
        }

        public void UpdateBigMapResult(int row, int col, FolderList folderList, ICInfo icInfo)
        {
            //failtable=>flowindex=>folder
            var failCmpNames = ((MainController)VisionController).GetFailTable();
            var allCmpNames = ((MainController)VisionController).GetCmpNames();

            var failIndexes = icInfo.FailIndex;
            var invalidIndexes = icInfo.Invalid;//從1開始

            List<int> failToFlowIndex = new List<int>();
            for (int i = 0; i < failIndexes.Count; i++)
            {
                var target = allCmpNames.FirstOrDefault(x => x == failCmpNames[failIndexes[i]]);
                if (target == null)
                {
                    continue;
                }
                var index = allCmpNames.IndexOf(target);
                failToFlowIndex.Add(index);
            }

            bool isFail = false;
            bool isInvalid = false;
            for (int i = 0; i < ProductParam.BigProductMapSetting.MapList.Count; i++)
            {
                isFail = false;
                isInvalid = false;
                if (CurrentTrayCarrier.ProductInfos[row][col].BigMapResults.Count <= i)
                {
                    CurrentTrayCarrier.ProductInfos[row][col].BigMapResults.Add(new BigMapResult() { MapIndex = i });
                }
                CurrentTrayCarrier.ProductInfos[row][col].BigMapResults[i].MapIndex = ProductParam.BigProductMapSetting.MapList[i].MapIndex;
                var useFolder = folderList.Folders.Where(x => x.FolderName.StartsWith($"MapIndex_{ProductParam.BigProductMapSetting.MapList[i].MapIndex}")).ToList();
                for (int j = 0; j < useFolder.Count; j++)
                {
                    for (int k = 0; k < invalidIndexes.Count; k++)
                    {
                        if (invalidIndexes[k] - 1 >= useFolder[j].Contain.First && invalidIndexes[k] - 1 <= useFolder[j].Contain.Second)
                        {
                            isInvalid = true;
                            break;
                        }
                    }
                    if (isInvalid)
                    {
                        break;
                    }
                    for (int k = 0; k < failToFlowIndex.Count; k++)
                    {
                        if (useFolder[j].Contain.First >= failToFlowIndex[k] && useFolder[j].Contain.Second <= failToFlowIndex[k])
                        {
                            isFail = true;
                        }
                    }
                }
                if (useFolder.Count > 0)
                {
                    if (isInvalid)
                    {
                        CurrentTrayCarrier.ProductInfos[row][col].BigMapResults[i].InspectResult = "Invalid";
                    }
                    else if (isInvalid = false && isFail == true)
                    {
                        CurrentTrayCarrier.ProductInfos[row][col].BigMapResults[i].InspectResult = "Fail";
                    }
                    else
                    {
                        CurrentTrayCarrier.ProductInfos[row][col].BigMapResults[i].InspectResult = "Pass";
                    }
                }

            }
        }


        public void NormalizeGrabInfos(GrabInfos grabInfosResult)
        {
            // TODO: 以下請依據實際資料填寫


            int blockWidth = CurrentTrayCarrier.InspectData.InspectionPostion.BlockICNumCol;       // Block 的欄數 (Col)，例如一個 Block 是 9x6
            int blockHeight = CurrentTrayCarrier.InspectData.InspectionPostion.BlockICNumRow;      // Block 的列數 (Row)

            int fovColsPerBlock = CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount;  // 每個 Block 有幾個 FOV (水平)
            int fovRowsPerBlock = CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount;  // 每個 Block 有幾個 FOV (垂直)

            int fovWidth = CurrentTrayCarrier.InspectData.InspectionPostion.FOVProductXnum;         // 每個 FOV 寬度 (Col)
            int fovHeight = CurrentTrayCarrier.InspectData.InspectionPostion.FOVProductYnum;        // 每個 FOV 高度 (Row)

            // SubDimSize : Tray X數量,  FOVProductXnum : 一個FOV數量  , 求餘數
            int blockColRem = fovColsPerBlock
                % fovWidth;
            int blockRowRem = fovRowsPerBlock
                % fovHeight;

            int totalFOVPerBlock = fovColsPerBlock * fovRowsPerBlock;

            int scale = 0;
            for (int fovIndex = fovColsPerBlock; fovIndex < grabInfosResult.FOVInfos.Count; fovIndex++)
            {
                if (fovIndex % fovColsPerBlock == 0)
                {
                    scale++;
                }

                var fov = grabInfosResult.FOVInfos[fovIndex];
                bool isEvenRow = (fov.FovRow % 2 == 0); //判斷是否鏡像

                for (int i = 0; i < fov.ICs.Count; i++)
                {
                    int oriCol = fov.ICs[i].Col;
                    fov.ICs[i].Col = oriCol - (blockColRem * scale);
                }

            }

            // ➤ 你也可以額外做 FOV 檢查報告、錯誤收集等
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inBlockColNum">block裡的Col產品數量</param>
        /// <param name="inFovCol">FOV裡Col產品數量</param>
        /// <param name="blockColNum">block的Col數量</param>
        /// <param name="currentCol">當下的Col編號</param>
        /// <param name="fovColCount">FOV的Col數量</param>
        /// <returns></returns>
        public int GrabInfoChangeTrayCol(int inBlockColNum, int inFovCol, int blockColNum, int currentCol, int fovColCount)
        {
            var notFullCol = inFovCol - (inBlockColNum % inFovCol);

            Dictionary<int, int> grabInfoToIcLayout = new Dictionary<int, int>();//grabInfo : icLayout ，grabInfo會比較多
            List<int> notIndex = new List<int>();//用來記錄不滿的col
            var oneBlockMaxCount = fovColCount * inFovCol / blockColNum;
            for (int i = 1; i <= blockColNum; i++)
            {
                for (int j = 0; j < notFullCol; j++)
                {
                    notIndex.Add(oneBlockMaxCount * blockColNum - j - 1);
                }
            }

            int scale = 0;
            for (int i = 1; i <= fovColCount * inFovCol; i++)
            {

                if (notIndex.Contains(i - 1))
                {
                    //不檢測的index
                    grabInfoToIcLayout.Add(i - 1, -1);//-1是因為IC的Index是從0開始
                }
                else if (i / oneBlockMaxCount >= 1)
                {
                    //第2個block開始的index
                    grabInfoToIcLayout.Add(i - 1, i - (notFullCol * scale) - 1);//-1是因為IC的Index是從0開始
                }
                else
                {
                    //第一個block的index
                    grabInfoToIcLayout.Add(i - 1, i - 1);//-1是因為IC的Index是從0開始
                }

                if (i % (oneBlockMaxCount) == 0)
                {
                    scale++;
                }
            }
            return grabInfoToIcLayout[currentCol];
        }


        //public void WriteVisionProductCount()
        //{
        //    //TODO 要修改
        //    //OnAddTrace("VisionModule_ProductCount", $"WriteVisionProductCount start");
        //    //OnAddTrace("VisionModule_ProductCount", $"Pin1 Pass Count:{Pin1PassCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"Pin1 Fail Count:{Pin1FailCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"Pin1 Invalid Count:{Pin1InvalidCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"Pin1 Total Count:{Pin1TotalCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"PVI Pass Count:{PVIPassCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"PVI Fail Count:{PVIFailCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"PVI Invalid Count:{PVIInvalidCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"PVI Total Count:{PVITotalCount}");
        //    //OnAddTrace("VisionModule_ProductCount", $"WriteVisionProductCount end");
        //}

        #region TrayView相關
        public TrayContainer CurrentTray { get; set; }

        public bool IsLocalTestMode => throw new NotImplementedException();

        //public event EventHandler<Point> IcStateUpdate;
        public event EventHandler<TrayContainer> TrayChange;
        public event EventHandler<Point> IcStateUpdate;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<int, int, string, string> OnTranSF;
        public event Action<int, int> OnUpdateProcessState;
        public event Func<string, string> OnGetSV;
        public event Func<int, string> OnIDGetSV;
        public event Func<string, string> OnGetEC;
        public event Action<string, string, string> OnFinishProcessJob;

        public void OnS10F3Message(string message)
        {

        }

        public void OnNotiSF_S14F2(string data)
        {

        }

        public void NotifyEqControlJob(string cjId, string pjId, string productName)
        {

        }
        public void OnTrayIn(TrayContainer newTray)
        {
            CurrentTray = newTray;
            TrayChange?.Invoke(this, CurrentTray);
        }

        public void ClearGridView()
        {
            //清空GridView檢測結果
            for (int i = 0; i < CurrentTrayCarrier.Tray.Layout.Y; i++)
            {
                for (int j = 0; j < CurrentTrayCarrier.Tray.Layout.X; j++)
                {
                    var icObject = CurrentTrayCarrier.Tray[i, j].ObjectInSlot;
                    IcObject oldic = new IcObject();
                    if (icObject is IcObject ic)
                    {
                        oldic = ic;
                        oldic.StationResults[0] = null;
                    }
                    CurrentTrayCarrier.Tray.SetObject(i, j, oldic);
                }
            }
            OnTrayIn(CurrentTrayCarrier.Tray);
        }

        BigMapShowView _bigMapShowView;
        public void OnClickGridItem(int row, int col)
        {
            var a = ((Flow2)((MainController)VisionController).FlowHandle).GetFolderInfo();
            _preBoatCarrier.IsPreUse = true;//提前拿，不要加計數
            GetTrayLayout.Invoke(this, _preBoatCarrier);//先提前拿取Boat資訊，只給下面先偷跑使用，實際執行使用的是CurrentTrayCarrier
            CurrentTrayCarrier = _preBoatCarrier;
            if (_bigMapShowView == null)
            {
                _bigMapShowView = (BigMapShowView)this.GetHtaService<IMdiService>().ShowMdi(this, typeof(BigMapShowView), new Point(0, 0), new Size(0, 0), new object[] { row, col });
                _bigMapShowView.FormClosed += (s, e) =>
                {
                    _bigMapShowView = null;
                };
            }
            /*
            int currColIndex = col / CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount;
            int currRowIndex = row / CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount;

            if (_componentReportForm == null)
            {
                _componentReportForm = (ComponentReportForm)_mdiService.ShowMdi(this, typeof(ComponentReportForm), new Point(0, 0), new Size(0, 0));
                _componentReportForm.FormClosed += (ss, ee) =>
                {
                    _componentReportForm = null;
                };
                _componentReportForm.TotalTrayInLot = CurrentTrayCarrier.Count;
                //需要自己求出index(影像中第幾顆產品)，row跟col可能都要重算
                _componentReportForm.ShowComponentReportValue(
                    VisionController.CommonInfo.ComponentSaveSetting.path,
                    currRowIndex + 1,
                    currColIndex + 1,
                    0,
                    VisionController.CommonInfo.ResultImageSaveSetting.path,
                    VisionController.CommonInfo.RowImageSaveSetting.path,
                    _curLotName);
            }
            else
            {
                _componentReportForm.TotalTrayInLot = CurrentTrayCarrier.Count;
                _componentReportForm.ShowComponentReportValue(
                    VisionController.CommonInfo.ComponentSaveSetting.path,
                    currRowIndex + 1,
                    currColIndex + 1,
                    0,
                    VisionController.CommonInfo.ResultImageSaveSetting.path,
                    VisionController.CommonInfo.RowImageSaveSetting.path,
                    _curLotName);
            }
            */
        }

        public object OnCreateObject(SlotObject slotObject)
        {
            return null;
        }

        public bool CheckPosition(BoatCarrier Carrier, List<MosaicInfo> MosaicInfo_List)
        {
            bool _checkExist = false;
            List<bool> _checkList = new List<bool>();

            var _productX_Count = Carrier.InspectData.InspectionPostion._bigProductX[0].Length;
            var _productY_Count = Carrier.InspectData.InspectionPostion._bigProductY[0].Length;
            var _mosaicPoint_Count = MotionGrid.Poses.Count;

            for (int x_index = 0; x_index < _productX_Count; x_index++)
            {
                for (int y_index = 0; y_index < _productY_Count; y_index++)
                {
                    _checkExist = false;
                    for (int index = 0; index < _mosaicPoint_Count; index++)
                    {
                        if (Carrier.InspectData.InspectionPostion._bigProductX[0][x_index] == MotionGrid.Poses[index].MotionX
                            && Carrier.InspectData.InspectionPostion._bigProductY[0][y_index] == MotionGrid.Poses[index].MotionY)
                        {
                            _checkExist = true;
                            break;
                        }
                    }
                    _checkList.Add(_checkExist);
                }
            }

            _checkExist = true;
            _checkExist = _checkList.Any(x => x != false);
            //for (int index = 0; index < _checkList.Count; index++)
            //{
            //    if (_checkList[index] == false)
            //    {
            //        _checkExist = false;
            //    }
            //}
            return _checkExist;
        }

        /// <summary>
        /// 根據光源設定數量更新FocusList
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<List<double>> checkFocusList(List<List<double>> param)
        {
            List<List<double>> tempList = new List<List<double>>();
            List<double> temp = null;

            for (int List_index = 0; List_index < VisionController.ProductSetting.RoundSettings2.Count; List_index++)
            {
                temp = new List<double>();
                if (List_index < param.Count)
                {
                    for (int index = 0; index < VisionController.ProductSetting.RoundSettings2[List_index].Groups.Count; index++)
                    {
                        if (index < param[List_index].Count)
                            temp.Add(param[List_index][index]);
                        else
                            temp.Add(0);
                    }
                }
                else
                {
                    for (int index = 0; index < VisionController.ProductSetting.RoundSettings2[List_index].Groups.Count; index++)
                    {
                        temp.Add(0);
                    }
                }
                tempList.Add(temp);
            }

            return tempList;
        }

        #endregion

        public void SetBurningParam()
        {
            BurningReport.Data.EndTime = DateTime.Now;
            double averageSpendTime = (BurningReport.Data.EndTime.Day - BurningReport.Data.StartTime.Day) * 24 * 3600 +
                                      (BurningReport.Data.EndTime.Hour - BurningReport.Data.StartTime.Hour) * 3600 +
                                      (BurningReport.Data.EndTime.Minute - BurningReport.Data.StartTime.Minute) * 60 +
                                      (BurningReport.Data.EndTime.Second - BurningReport.Data.StartTime.Second);
            TimeSpan workTime = BurningReport.Data.EndTime - BurningReport.Data.StartTime;

            averageSpendTime /= InspectCounts;

            BurningReport.Data.TestCycle = InspectCounts;
            BurningReport.Data.FailCycle = 0;
            BurningReport.Data.FailCycleRate = BurningReport.Data.FailCycle / BurningReport.Data.TestCycle;
            BurningReport.Data.Average_Cycle_Time = averageSpendTime;

            double subHr = workTime.TotalHours;
            double uph = BurningReport.Data.Expect_Product_Count / subHr;

            BurningReport.Data.Expect_Product_Count = InspectCounts * CurrentTrayCarrier.Tray.BlockContainerDesc.SubDimSize.X *
                                                                        CurrentTrayCarrier.Tray.BlockContainerDesc.SubDimSize.Y;
            BurningReport.Data.Total_TestProduct = TotalProductCount;
            BurningReport.Data.Trigger_Lose_Rate = 1 - BurningReport.Data.Total_TestProduct / BurningReport.Data.Expect_Product_Count;
            BurningReport.Data.UPH = uph;

            if (BurningReport.Data.FailCycle == 0 && BurningReport.Data.Trigger_Lose_Rate == 0.0)
            {
                BurningReport.Data.TestResult = true;
            }
            else
            {
                BurningReport.Data.TestResult = false;
            }

        }

        public void SetSummaryReport()
        {
            SummaryReport summaryReport = new SummaryReport();
            summaryReport.ProductName = ProductName;
            summaryReport.LotName = _curLotName;
            summaryReport.MachineName = "TA2000";
            summaryReport.TrayCount = CurrentTrayCarrier.Count;
            summaryReport.PassCount = StatisticView.AllCounts.ViewFormDatas[0].Pass;
            summaryReport.FailCount = StatisticView.AllCounts.ViewFormDatas[0].Fail;
            summaryReport.InvalidCount = StatisticView.AllCounts.ViewFormDatas[0].Invalid;
            summaryReport.StartTime = _startRunTime;
            summaryReport.EndTime = _endRunTime;
            summaryReport.Path = Param.SummaryReportPath + $"\\SummaryReport_{summaryReport.EndTime.ToString("yyyy_MM_dd_HH_mm_ss")}";
        }

        private void Alarm操作_ProcessError(object sender, ExecuteFailArgs e)
        {
            var process = (ControlFlow.Controls.ProcessItem)sender;
            string errorMessage = "";
            switch (process.Name)
            {
                case "檢查90度未到位Alarm":
                case "檢查0度未到位Alarm":
                case "下SG":
                    errorMessage = _errorMessage;
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_SG_Fail", AlarmActive = false });
                    break;
            }


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
                case "下SG":
                    {
                        SGErrorWork();
                        return;
                    }
            }
            AlarmQueue.Add(true);
        }

        private void SGErrorWork()
        {
            int otherTimes = 0;
            if (TeachInspectTimes > 0)
            {
                otherTimes = TeachInspectTimes - 1;
            }

            bool isOnline = false;
            if (ProductParam.IsReinspect || ProductParam.IsPick)
            {
                _sgGoCommands.Clear();
                //確認這盤的Row Col
                for (int i = 0; i < SingleInspectPos.Count; i++)
                {
                    var grabInfo = CurrentTrayCarrier.Tray.GenSingleGrabInfo(
                                  VisionController.MachineControlConfig.SGTimeOut * 1000,
                                    CurrentTrayCarrier.Count + otherTimes,
                                    (int)SingleInspectPos[i].x,//當下Row
                                    (int)SingleInspectPos[i].y,//當下Col
                                    CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount * CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount, //一個FOV有幾顆
                                    CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount
                                    );

                    _sgGoCommands.Enqueue(grabInfo);
                }

                _wcfClient.Call(s =>
                {
                    s.Offline();
                    s.SetupFlowNumber(CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount); //FN
                    s.SetFlowRunnerNumber(1); //IR
                    if (ProductParam.UseMosaic == false &&
                    _preBoatCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                    {
                        s.SetMosaicIsOpen(false);
                    }
                    s.Online(); //NL  
                    isOnline = SpinWait.SpinUntil(s.IsOnline, Param.InspectOnlineTimeout * 1000);
                });
            }
            else
            {
                //重建SG_queue
                _sgGoCommands.Clear();
                //確認這盤的Row Col
                //Tips 使用以Col為主產生GrabInfo
                for (int i = 0; i < CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount; i++)
                {
                    var grabInfo = CurrentTrayCarrier.Tray.GenColMainGrabInfo(
                            VisionController.MachineControlConfig.SGTimeOut * 1000, //timeout
                            CurrentTrayCarrier.Count + otherTimes, //curTContainer.,//第幾個Tray ，otherTimes如果有教讀會加TeachInspectTimes - 1，沒教讀就是0
                            i + 1, //Col,GenGrabInfo是從1開始
                            CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount,//一個Row要拍幾次
                            CurrentTrayCarrier.InspectData.InspectionPostion.XStepCount * CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount,
                            CurrentTrayCarrier.InspectData.InspectionPostion.YStepCount);

                    _sgGoCommands.Enqueue(grabInfo);
                }

                if (!_wcfClient.IsServerOnline)
                {
                    _wcfClient.Dispose();
                    _wcfClient = new WcfDuplexClient<IVisionServerWcf, IVisionServerCallback>(Param.Port, _client, timeout: Param.Timeout);
                    _wcfClient.Open();
                }

                _wcfClient.Call(s =>
                {
                    s.Offline();
                    s.SetupFlowNumber(_preBoatCarrier.InspectData.InspectionPostion.MoreBlockYMaxStepCount); //FN
                    s.SetFlowRunnerNumber(_preBoatCarrier.InspectData.InspectionPostion.MoreBlockXMaxStepCount); //IR
                    if (ProductParam.UseMosaic == false &&
                    _preBoatCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
                    {
                        s.SetMosaicIsOpen(false);
                    }
                    s.Online(); //NL 
                    isOnline = SpinWait.SpinUntil(s.IsOnline, Param.InspectOnlineTimeout * 1000);
                });

            }

            //看當下的sgCommand的Row
            CurrentRow = _sgGoCommands.Peek().FOVInfos[0].FovRow - 1;
            CurrentCol = _sgGoCommands.Peek().FOVInfos[0].FovCol - 1;


            LogTrace($"_sgGoCommands CurrentRow:{CurrentRow} ,CurrentCol:{CurrentCol}");

            if (_isReinspectFlag || ProductParam.IsPick)
            {
                _grabCommands.Enqueue(new Point2d(CurrentRow, CurrentCol));

                _grabLaserCommands.Enqueue(new Point2d(CurrentRow, CurrentCol));
            }
            else
            {
                int grabRows = CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount;

                for (int i = 0; i < grabRows; i++)
                    _grabCommands.Enqueue(new Point2d(i, CurrentCol)); //Step模式把要做的IP先建出來

            }
            //AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Vision_SG_Fail", AlarmActive = false });
        }

        public void ChangeLotName(string lotName)
        {
            MachineCommonService.LotName = lotName;
        }

        #endregion Function (End)

        #region Form (Start)
        List<string> _allCmpNames = new List<string>();
        private void CheckStatisticForm()
        {



            //if (_statisticTableForm2 == null)
            //{
            //    _statisticTableForm2 = (StatisticTableForm2)_mdiService.ShowMdi(this, typeof(StatisticTableForm2), new Point(0, 0), new Size(0, 0), new object[] { this });
            //    StatisticTableViewModel2 = _statisticTableForm2.ViewModel;
            //    _statisticTableForm2.FormClosed += (ss, ee) =>
            //    {
            //        _statisticTableForm2 = null;
            //    };
            //}
            //else
            //{
            //    StatisticTableViewModel2.InitialCounts();
            //}
            if (IsGolden)
            {
                //golden產品檔因為是自己加的，不是用教讀介面建立的，所以格式不符合新的統計介面
                //所以跑golden就不出現統計
                return;
            }

            try
            {
                if (_statisticTableForm2 == null)
                {
                    _statisticTableForm2 = (StatisticTableForm2)_mdiService.ShowMdi(this, typeof(StatisticTableForm2), new Point(0, 0), new Size(0, 0), new object[] { this });
                    StatisticTableViewModel2 = _statisticTableForm2.ViewModel;
                    _statisticTableForm2.FormClosed += (ss, ee) =>
                    {
                        _statisticTableForm2 = null;
                    };
                }
                else
                {
                    StatisticTableViewModel2.InitialCounts();
                }
            }
            catch (Exception ee)
            {

            }
            
        }


        #region CDI 按鈕顯示 (Start)
        [GroupDisplay("ForTest", "產品設定", "")]
        public void Test()
        {
            GetTrayLayout.Invoke(this, _preBoatCarrier);//先提前拿取Boat資訊，只給下面先偷跑使用，實際執行使用的是CurrentTrayCarrier
            CurrentTrayCarrier = _preBoatCarrier;

            var a = (WizardForm)this.GetHtaService<IMdiService>().ShowMdi(this, typeof(WizardForm), new Point(0, 0), new Size(1800, 800),
                    new object[] { this }, true);
        }


        TestMosaicForm _testMosaicForm;
        [GroupDisplay("ForTest", "Mosaic精度測試", "")]
        public void TestMosaic()
        {
            if (flowForm == null)//可以拿掉 換成3D的
            {
                var hardware = VisionController.GetHardware();

                flowForm = (FlowForm)this.GetHtaService<IMdiService>().ShowMdi(this, typeof(FlowForm), new Point(0, 0), new Size(0, 0),
                    new object[] { VisionController, hardware, true }, true);
                flowForm.CurrentAccessLevel = 4;

                ChangeCollectImageMode(true);

                flowForm.FormClosed += (ss, ee) =>
                {
                    flowForm = null;
                    _testMosaicForm?.Close();
                };
            }
            if (_testMosaicForm == null)
            {
                _testMosaicForm = (TestMosaicForm)_mdiService.ShowMdi(this, typeof(TestMosaicForm), new Point(1300, 200), new Size(0, 0), new object[] { this, flowForm });
                _testMosaicForm.FormClosed += (ss, ee) =>
                {
                    _testMosaicForm = null;
                };
            }
        }


        [GroupDisplay(nameof(Resources.gpTop_Statistic), nameof(Resources.gpBtn_StatisticCounts), nameof(Resources.data_analysis))]
        public void 秀統計介面()
        {

            //if (_statisticForm == null)
            //{
            //    _failCmpNames = ((MainController)VisionController).GetFailTable();
            //    var allCmpNames = ((MainController)VisionController).GetCmpNames();
            //    StatisticView = new StatisticViewModel(_failCmpNames, allCmpNames);
            //    _statisticForm = (StatisticForm)_mdiService.ShowMdi(this, typeof(StatisticForm), new Point(0, 0), new Size(0, 0));
            //    _statisticForm.FormClosed += (ss, ee) =>
            //    {
            //        _statisticForm = null;
            //    };
            //}
            if (_statisticTableForm2 == null)
            {

                _statisticTableForm2 = (StatisticTableForm2)_mdiService.ShowMdi(this, typeof(StatisticTableForm2), new Point(0, 0), new Size(0, 0), new object[] { this });
                StatisticTableViewModel2 = _statisticTableForm2.ViewModel;
                _statisticTableForm2.FormClosed += (ss, ee) =>
                {
                    _statisticTableForm2 = null;
                };
            }

        }

        [GroupDisplay(nameof(Resources.ClassifyTool), "Vision離線測試", "")]
        public void AddTray()
        {
            BoatCarrier args = new BoatCarrier();
            GetTrayLayout?.Invoke(this, args);
            args.IsVirtual = true;
            CurrentTrayCarrier = args;
            _trayInQueue.Add(args.Tray);
            ProductParam.FocusLocations = checkFocusList(ProductParam.FocusLocations); //檢查Focus數量是否與光源Group數量相同
            this.Start();
        }
        [GroupDisplay(nameof(Resources.ClassifyTool), "雷射測試", "")]
        public void OpenLaserMeasureTest()
        {
            if (_laserTestForm == null)
            {
                _laserTestForm = (LaserTestForm)_mdiService.ShowMdi(this, typeof(LaserTestForm), new Point(0, 0), new Size(0, 0), new object[] { this });
                _laserTestForm.FormClosed += (ss, ee) =>
                {
                    _laserTestForm = null;
                };
            }
        }
        [GroupDisplay(nameof(Resources.Icon_GoldenTeach), nameof(Resources.Icon_GoldenInspect), nameof(Resources._3d_printer))]
        public void GoldenTeach()
        {
            var service = this.GetHtaService<IDialogService>();

            //if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
            //{
            //    var noGolden = service.ShowDialog(this, new ShowDialogArgs()
            //    {
            //        Button = MessageBoxButtons.OK,
            //        Caption = "Info",
            //        Message = "請切換成大產品檔"
            //    });
            //    return;
            //}



            if (!ProductName.StartsWith("Golden"))
            {
                var noGolden = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "Info",
                    Message = "請切換成Golden產品檔"
                });
                return;
            }


            if (_machineSimpleController.State != HtaMachineController.MachineStateEm.Idle)
            {
                if (!IsGolden)
                {
                    var noReady2 = service.ShowDialog(this, new ShowDialogArgs()
                    {
                        Button = MessageBoxButtons.OK,
                        Caption = "Info",
                        Message = "請結束原本的流程，系統重製後，再執行Golden流程"
                    });
                }

                var noReady = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "Info",
                    Message = "請將機台狀態中止並系統重製，再執行Golden流程"
                });
                return;
            }

            var resultMove = service.ShowDialog(this, new ShowDialogArgs()
            {
                Button = MessageBoxButtons.OKCancel,
                Caption = "Info",
                Message = "請確認機台內部狀態，無問題即可按OK鍵，並開門放置Golden治具\n" +
                "\nOK：可開門放置Goldne治具" +
                "\nCancel：動作將停止"
            });

            if (resultMove == DialogResult.OK)
            {
                var velDefBY1 = SelectVelDef(視覺縱移軸, AY1VelList);
                bool moveSuccessY = MoveAxis(視覺縱移軸, 0, velDefBY1);
                //bool moveSuccessY = MoveAxis(BX1_流道橫移軸, MotorOffset.InspStandBy_BY1, velDefBY1);

                if (SettingService.SystemSetting.SelectVersion == "M00")
                    Y065000_設備安全門電磁鎖.SetIO(false);
                else if (SettingService.SystemSetting.SelectVersion == "M01")
                    Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
            }
            else
            {
                return;
            }

            MessageBox.Show("可開啟機台門，並將Golden治具放入機台。",
                         "Info",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Warning,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);


            while (true)
            {
                if (SettingService.SystemSetting.SelectVersion == "M00")
                {

                    if (!bVirtual)
                    {
                        service.ShowDialog(this, new ShowDialogArgs()
                        {
                            Button = MessageBoxButtons.OK,
                            Caption = "Info",
                            Message = "請確認機台門確實關上 " +
                       "\n\n(門關好後系統會自動上鎖)"
                        });

                        if (X064006_設備安全門關閉.CheckIO())
                        {
                            Y065000_設備安全門電磁鎖.SetIO(true);

                            break;
                        }
                    }
                    else
                    {
                        Y065000_設備安全門電磁鎖.SetIO(true);

                        break;
                    }

                }
                else if (SettingService.SystemSetting.SelectVersion == "M01")
                {
                    if (!bVirtual)
                    {
                        service.ShowDialog(this, new ShowDialogArgs()
                        {
                            Button = MessageBoxButtons.OK,
                            Caption = "Info",
                            Message = "光閘觸發，請確認"
                        });
                        if (X064006_設備安全門關閉.CheckIO() == false)
                        {
                            break;
                        }
                    }
                    else
                    {
                        X064006_設備安全門關閉.SetIO(false);

                        break;
                    }


                }

                Thread.Sleep(100);
            }


            Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
            var resultIO = service.ShowDialog(this, new ShowDialogArgs()
            {
                Button = MessageBoxButtons.OKCancel,
                Caption = "Info",
                Message = "治具已靠邊，請確認是否完成\n" +
              "\nOK：確認治具擺放完成，即將啟動Golden流程" +
              "\nCancel：治具擺放需再調整，動作將停止"
            });
            if (resultIO == DialogResult.OK)
            {
                //流程繼續
            }
            else
            {
                Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);

                var noGolden = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "Info",
                    Message = "請重新開始Golden流程"
                });

                return;
            }


            //幫忙換新的LotName，怕使用者忘記
            NotifyLotChange?.Invoke(this, new LotChangeArgs() { LotName = $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_Golden")}" });//TODO 好像改不了介面上的，待確認

            BoatCarrier args = new BoatCarrier();
            GetTrayLayout?.Invoke(this, args);

            //if (BX1_流道橫移軸 is VirtualAxis)
            //{
            //    args.IsVirtual = true;//有硬體的話，這個為false
            //}
            //else
            //{
            //    args.IsVirtual = false;
            //}

            args.IsVirtual = bVirtual;
            CurrentTrayCarrier = args;
            _trayInQueue.Add(args.Tray);
            Teaching = true;
            IsGolden = true;

            NotifyGoldenModel?.Invoke(this, new GoldenModelArgs() { IsGolden = true });//通知其他模組，進入Golden模式

            _machineSimpleController.Run();

        }


        [GroupDisplay(nameof(Resources.Icon_GoldenTeach), nameof(Resources.Icon_Golden3DInspect), nameof(Resources._3d_printer))]
        public void Golden3DTeach()
        {
            var service = this.GetHtaService<IDialogService>();
            //if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            //{
            //    var noGolden = service.ShowDialog(this, new ShowDialogArgs()
            //    {
            //        Button = MessageBoxButtons.OK,
            //        Caption = "Info",
            //        Message = "請切換成小產品檔"
            //    });
            //    return;
            //}

            if (!ProductName.Contains("Golden3D"))
            {
                var noGolden = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "Info",
                    Message = "請切換成Golden3D產品檔"
                });
                return;
            }


            if (_machineSimpleController.State != HtaMachineController.MachineStateEm.Idle)
            {
                if (!IsGolden)
                {
                    var noReady2 = service.ShowDialog(this, new ShowDialogArgs()
                    {
                        Button = MessageBoxButtons.OK,
                        Caption = "Info",
                        Message = "請結束原本的流程，系統重製後，再執行Golden流程"
                    });
                }

                var noReady = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "Info",
                    Message = "請將機台狀態中止並系統重製，再執行Golden流程"
                });
                return;
            }


            var resultMove = service.ShowDialog(this, new ShowDialogArgs()
            {
                Button = MessageBoxButtons.OKCancel,
                Caption = "Info",
                Message = "吸嘴縱移軸(AY1)將移動到放置Golden位置，請確認治具上無任何物品\n" +
                "\nOK：移動吸嘴縱移軸(AY1)並可開門放置Goldne治具" +
                "\nCancel：動作將停止"
            });

            if (resultMove == DialogResult.OK)
            {
                var velDefBY1 = SelectVelDef(視覺縱移軸, AY1VelList);
                bool moveSuccessY = MoveAxis(視覺縱移軸, 0, velDefBY1);
                //bool moveSuccessY = MoveAxis(BX1_流道橫移軸, MotorOffset.InspStandBy_BY1, velDefBY1);

                if (SettingService.SystemSetting.SelectVersion == "M00")
                    Y065000_設備安全門電磁鎖.SetIO(false);
                else if (SettingService.SystemSetting.SelectVersion == "M01")
                    Y067002_傳送流道_頂升真空電磁閥.SetIO(true);

            }
            else
            {
                return;
            }

            MessageBox.Show("可開啟機台門，並將Golden治具放入機台。",
                            "Info",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);


            while (true)
            {
                if (!bVirtual)
                {
                    service.ShowDialog(this, new ShowDialogArgs()
                    {
                        Button = MessageBoxButtons.OK,
                        Caption = "Info",
                        Message = "請確認機台門確實關上 " +
                   "\n\n(門關好後系統會自動上鎖)"
                    });

                    if (X064006_設備安全門關閉.CheckIO())
                    {
                        Y065000_設備安全門電磁鎖.SetIO(true);

                        break;
                    }
                }
                else
                {
                    Y065000_設備安全門電磁鎖.SetIO(true);

                    break;
                }

                Thread.Sleep(100);
            }

            //幫忙換新的LotName，怕使用者忘記
            //NotifyLotChange?.Invoke(this, new LotChangeArgs() { LotName = $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_Golden")}" });//TODO 好像改不了介面上的，待確認
            MachineCommonService.LotName = $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}_Golden3D";

            BoatCarrier args = new BoatCarrier();
            GetTrayLayout?.Invoke(this, args);

            if (bVirtual)
            {
                args.IsVirtual = true;//有硬體的話，這個為false
            }
            else
            {
                args.IsVirtual = false;
            }
            CurrentTrayCarrier = args;
            _trayInQueue.Add(args.Tray);
            Teaching = true;
            IsGolden = true;
            Is3DGolden = true;

            NotifyGoldenModel?.Invoke(this, new GoldenModelArgs() { IsGolden = true });//通知其他模組，進入Golden模式

            _machineSimpleController.Run();

        }

        #endregion CDI 按鈕顯示 (End)


        [ModuleProductSettingForm]
        public object GetProductSettingForm()
        {
            InspectProductSetting frm = new InspectProductSetting(this);
            return frm;
        }

        [ModuleGlobalSettingForm]
        public object GetGlobalSettingForm()
        {
            InspectGlobalSetting frm = new InspectGlobalSetting(Param);
            return frm;
        }

        private BarcodeTestForm _barcodeTestForm;
        //[GroupDisplay(nameof(Resources.gpBtn_BarcodeTool), nameof(Resources.gpBtn_BarcodeTestForm), null)]
        public void ShowBarcodeTestForm()
        {
            if (_barcodeTestForm == null)
            {
                var mdiService = this.GetHtaService<IMdiService>();
                _barcodeTestForm = (BarcodeTestForm)mdiService.ShowMdi(this, typeof(BarcodeTestForm), new Point(0, 0), new Size(0, 0), new object[] { this });

                _barcodeTestForm.FormClosed += (ss, ee) =>
                {
                    _barcodeTestForm = null;
                };
            }

        }

        //private APS_TestForm _aPS_TestForm;
        //[GroupDisplay(nameof(Resources.gpBtn_ASPTool), nameof(Resources.gpBtn_APS_TestForm), null)]
        public void ShowAPS_TestForm()
        {
            //if (_aPS_TestForm == null)
            //{
            //    var mdiService = this.GetHtaService<IMdiService>();
            //    _aPS_TestForm = (APS_TestForm)mdiService.ShowMdi(this, typeof(APS_TestForm), new Point(0, 0), new Size(0, 0), new object[] { this });

            //    _aPS_TestForm.FormClosed += (ss, ee) =>
            //    {
            //        _aPS_TestForm = null;
            //    };
            //}

        }


        public override void MachineControllerReady()
        {
            base.MachineControllerReady();

            var velService = this.GetHtaService<IAxisConfigVelocityService>();

            velService.DataSourceChanged += VelService_DataSourceChanged;
            if (BX1_流道橫移軸 is VirtualAxis || 視覺縱移軸 is VirtualAxis)
            {
                bVirtual = true;
            }
            //獲取所需軸的所有速度值
            BX1VelList = velService.QueryVelData(BX1_流道橫移軸.Name);
            AY1VelList = velService.QueryVelData("AY1");
            //_InspectVelocity = (GetVelocity(this, 視覺縱移軸, TVelEnum.Slow).Velocity);// velService.QueryVelData(視覺縱移軸.Name);

            BZ1VelList = velService.QueryVelData(BZ1_流道頂升升降軸.Name);

            SetVisionController?.Invoke(this, new VisionControllerArgs() { VisionController = VisionController });

            _machineSimpleController = this.GetHtaService<IMachineSimpleController>();
            _mdiService = this.GetHtaService<IMdiService>();
            SettingService = this.GetHtaService<IMachineSystemSettingService>();

            NotifyInspectMotorOffsetParam?.Invoke(this, MotorOffset);

            var service = ((HtaMachineController)ParentController).FindService<IMachineCommon>().ToArray();
            MachineCommonService = service.First();


            TATool.MainController = VisionController;
        }
        private void VelService_DataSourceChanged(object sender, EventArgs e)
        {
            var velService = sender as IAxisConfigVelocityService;
            BX1VelList = velService.QueryVelData(BX1_流道橫移軸.Name);
            AY1VelList = velService.QueryVelData("AY1");
            //_InspectVelocity = (GetVelocity(this, 視覺縱移軸, TVelEnum.Slow).Velocity);// velService.QueryVelData(視覺縱移軸.Name);


            BZ1VelList = velService.QueryVelData(BZ1_流道頂升升降軸.Name);

        }

        public override void MachineUiReady()
        {
            base.MachineUiReady();
            _showWorkLogService = this.GetHtaService<IShowWorkLogService>();
            _showWorkLogService.ShowWorkLog(this);
        }

        #endregion Form (End)


        [ModuleInitialFunction(11, -1)]
        public void InitFunc()
        {
            LogTrace("InitFunc start");
            try
            {
                //詢問tray layout
                QueryTrayLayoutArgs args = new QueryTrayLayoutArgs();
                QueryTrayLayout?.Invoke(this, args);


                if (args.Desc != null)
                {
                    //如果不是空的，先產生一個標準樣板
                    CurrentTray = args.Desc.GenerateTray();
                    var layout = CurrentTray.Layout;
                    for (int i = 0; i < layout.Y; i++)
                    {
                        for (int j = 0; j < layout.X; j++)
                        {
                            CurrentTray.SetObject(i, j, null);
                        }
                    }

                    OnTrayIn(CurrentTray);
                }

            }
            catch (Exception exp)
            {
                OnAddFatal("InspectionModule", "InitFunc異常", exp);
                //throw new Exception($@"InspectionModule InitFunc異常");
            }

            LogTrace("InitFunc end");
        }

        [ModuleInitialFunction(11, -1)]
        public void InitVisionOffline()
        {
            LogTrace("InitVisionOffline start");
            if (_wcfClient != null)
            {
                if (_wcfClient.IsServerOnline)
                {
                    _wcfClient.Call(s =>
                    {
                        if (s.IsOnline())
                        {
                            s.Offline();
                        }
                    });
                }
            }
            LogTrace("InitVisionOffline end");

            _encoderPosition = 0;
            var hardware = VisionController.GetHardware();
            var trigger = hardware.Trigger[0];
            if (!(BZ1_流道頂升升降軸 is VirtualAxis))
            {
                trigger.ZeroEncoder();
                _encoderPosition = trigger.EncoderPosition;
            }
        }

        private void 是否組圖_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            _partionIndex = 0;
            _partionGroupIndex = 0;
            MosaicImgCount = 0;

            //if (ProductParam.UseMosaic)
            //{
            //    e.Result = true;
            //}
            //else
            //{
            //    e.Result = false;
            //}

            if (ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].UseType == "Mosaic")
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void 移動指定位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"-- 移動指定位置_ProcessIn--");

            _grabIndex = _grabCommands.Peek();

            var n = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductX.Length;



            //var distX = MotorOffset.InspStandBy_AX1 - CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[n - 1 - (int)_grabIndex.y];
            //var distY = MotorOffset.InspStandBy_BY1 + CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[ (int)_grabIndex.x];

            //var distX = CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[n - 1-(int)_grabIndex.y];
            //var distY = CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[(int)_grabIndex.x];


            var distX = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].MosaicPosition[_partionIndex].x; //相機橫移軸 (X方向固定一顆產品)
            var distY = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].MosaicPosition[_partionIndex].y;  //吸嘴縱移軸


            //var distZ = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductY[(int)_grabIndex.x][MosaicImgCount/CurrentTrayCarrier.InspectData.InspectionPostion.MosaicZCount];//測試有多Y的mosaic時用

            var velDefAX1 = SelectVelDef(視覺縱移軸, AY1VelList);
            var velDefBY1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);

            bool moveSuccessAX1 = MoveAxis(視覺縱移軸, distX, velDefAX1, 0, 0);
            bool moveSuccessBY1 = MoveAxis(BX1_流道橫移軸, distY, velDefBY1, 0, 0);


            //對焦
            var velDefAZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);

            double AZ1Offset = ProductParam.PartionZPosList[_partionIndex];

            bool moveSuccessAZ1 = MoveAxis(BZ1_流道頂升升降軸, AZ1Offset, velDefAZ1);


            var AX1_waitRes = 視覺縱移軸.WaitMotionDone();
            var BY1_waitRes = BX1_流道橫移軸.WaitMotionDone();
            var AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();

            if (AX1_waitRes && BY1_waitRes && AZ1_waitRes)
            {
                e.ProcessSuccess = true;
            }
            else
            {
                e.ProcessSuccess = false;
                if (!AX1_waitRes) LogTrace($"移動指定位置 失敗，移動:{moveSuccessAX1},等待:{AX1_waitRes}");
                if (!BY1_waitRes) LogTrace($"移動指定位置 失敗，移動:{moveSuccessBY1},等待:{BY1_waitRes}");
                if (!AZ1_waitRes) LogTrace($"移動指定位置 失敗，移動:{moveSuccessAZ1},等待:{AZ1_waitRes}");
            }


            ////var result = moveSuccessAX1 && moveSuccessBY1 && moveSuccessAZ1;
            ////e.ProcessSuccess = result;
            //if (AX1_waitRes && BY1_waitRes && AZ1_waitRes)
            //{
            //    _partionIndex++;
            //}
        }

        private void 下IP拍照2_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LogTrace($"--下IP拍照2_ProcessIn--");

            // _wcfClient.Call(s =>
            // {
            //     s.Grab(0, 0);
            // });
            //// _partionGroupIndex++;
            // e.ProcessSuccess = true;
            int groupCount = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].GroupIndexes.Count;
            int posCount = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].MosaicPosition.Count;

            for (int i = 0; i < groupCount / posCount; i++)
            {
                //TODO 加入對焦移動


                _wcfClient.Call(s =>
                {
                    s.Grab(0, 0);
                });
            }


            e.ProcessSuccess = true;
        }

        private void 是否所有影像拍完_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            int posCount = ProductParam.BigProductMapSetting.MapList[CurrentMapIndexOrder].MosaicPosition.Count;
            _partionIndex++;
            if (_partionIndex >= posCount)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
            //CurrentMapIndexOrder++;
            //if (CurrentMapIndexOrder >= ProductParam.BigProductMapSetting.MapList.Count)//ProductParam.BigProductMapSetting.MapList.Count
            //{
            //    e.Result = true;
            //}
            //else
            //{
            //    e.Result = false;
            //}
        }

        public void OnClickGridItem(object sender, int row, int col)
        {
            throw new NotImplementedException();
        }

        private void 大產品拍攝子流程C_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            CurrentMapIndexOrder = 0;
            IsMosaicSG = false;
            IsPartSG = false;
        }

        public void AskCanChangeTeachingMode(AskCanTeachArgs args)
        {
            //throw new NotImplementedException();
        }

        public string GetVisionStationName()
        {
            throw new NotImplementedException();
        }

        public void SyncProductForVision(List<string> productNames)
        {
            throw new NotImplementedException();
        }

        public void WriteRecordStatistic()
        {
            string path = @"D:\Coordinator2.0\LIDatas\Statistic";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = $"Statistic_{_curLotName}.txt";
            string combinePath = Path.Combine(path, fileName);
            using (StreamWriter w = new StreamWriter(combinePath))
            {
                w.WriteLine("Inspection Statistic");
                w.WriteLine($"Pass Count: {StatisticView.AllCounts.ViewFormDatas[0].Pass}");
                w.WriteLine($"Fail Count: {StatisticView.AllCounts.ViewFormDatas[0].Fail}");
                w.WriteLine($"Invalid Count: {StatisticView.AllCounts.ViewFormDatas[0].Invalid}");
                w.WriteLine($"Total Count: {StatisticView.AllCounts.ViewFormDatas[0].Total}");

                for (int i = 1; i < StatisticView.AllCounts.ViewFormDatas.Count; i++)
                {
                    w.WriteLine("--------------------");
                    w.WriteLine($"Component Item: {StatisticView.AllCounts.ViewFormDatas[i].Label}");
                    w.WriteLine($"Pass Count: {StatisticView.AllCounts.ViewFormDatas[i].Pass}");
                    w.WriteLine($"Fail Count: {StatisticView.AllCounts.ViewFormDatas[i].Fail}");
                    w.WriteLine($"Invalid Count: {StatisticView.AllCounts.ViewFormDatas[i].Invalid}");
                    w.WriteLine($"Total Count: {StatisticView.AllCounts.ViewFormDatas[i].Total}");
                }

                w.Close();
            }
        }

        //public void ReadRecordStatistic()
        //{
        //    try
        //    {
        //        string path = @"D:\Coordinator2.0\TADatas\Statistic";
        //        if (!Directory.Exists(path))
        //            return;

        //        string fileName = $"Statistic_{_curLotName}.txt";
        //        string combinePath = Path.Combine(path, fileName);
        //        if (!File.Exists(combinePath))
        //            return;

        //        List<ViewFormData> viewFormDatas = new List<ViewFormData>();
        //        string[] lines = File.ReadAllLines(combinePath);
        //        ViewFormData current = new ViewFormData("OverAll", 0, 0, 0);
        //        foreach (var line in lines)
        //        {
        //            if (line.StartsWith("Component Item:"))
        //            {
        //                // 儲存上一個
        //                if (current != null)
        //                    viewFormDatas.Add(current);

        //                current = new ViewFormData(line.Replace("Component Item:", "").Trim(), 0, 0, 0);

        //            }
        //            else if (current != null)
        //            {
        //                if (line.StartsWith("Pass Count:"))
        //                    current.Pass = ExtractNumber(line);

        //                if (line.StartsWith("Fail Count:"))
        //                    current.Fail = ExtractNumber(line);

        //                if (line.StartsWith("Invalid Count:"))
        //                    current.Invalid = ExtractNumber(line);

        //            }
        //        }

        //        // 加入最後一個
        //        if (current != null)
        //            viewFormDatas.Add(current);

        //        for (int i = 0; i < viewFormDatas.Count; i++)
        //        {
        //            var target = StatisticView.AllCounts.ViewFormDatas.FirstOrDefault(x => x.Label == viewFormDatas[i].Label);
        //            target.Pass = viewFormDatas[i].Pass;
        //            target.Fail = viewFormDatas[i].Fail;
        //            target.Invalid = viewFormDatas[i].Invalid;
        //        }
        //        StatisticView.DataResetBindings();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogTrace("ReadRecordStatistic Error: " + ex.Message);
        //    }

        //}
       
        int ExtractNumber(string line)
        {
            var match = Regex.Match(line, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }



        private void 是否雷射測高_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            if (IsGolden)
            {
                e.Result = false;
                return;
            }

            LogTrace($"--LaseByPass:{ProductParam.UseLaserMeasure}--");

            if (ProductParam.UseLaserMeasure)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }



        private void 拍攝點位收集_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            _grabLaserCommands.Peek();

            if (LaserCaptureLocation != null) LaserCaptureLocation.Clear(); LaserCaptureLocation = null;
            LaserCaptureLocation = new List<Point2d>();
            double distX = 0, distY = 0;

            if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)//大產品 
            {
                if (ProductParam.UseMosaic) //組圖點位
                {
                    int mosaicCount = 0;

                    for (int index = 0; index < CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount * CurrentTrayCarrier.InspectData.InspectionPostion.MosaicYCount; index++)
                    {
                        if (CurrentTrayCarrier.InspectData.InspectionPostion.AlreadyProductMosaic)
                        {
                            var n = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductX.Length;

                            distX = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductX[n - 1 - (int)_grabIndex.y][mosaicCount % CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount];
                            distY = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductY[(int)_grabIndex.x][mosaicCount / CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount];

                            //distX = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductX[(int)(CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount -_grabIndex.y)][mosaicCount % CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount];
                            //distY =  CurrentTrayCarrier.InspectData.InspectionPostion._bigProductY[(int)_grabIndex.x][ mosaicCount / CurrentTrayCarrier.InspectData.InspectionPostion.MosaicYCount];

                        }
                        else
                        {
                            distX = MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._bigProductX[(int)_grabIndex.y][mosaicCount % CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount]; //相機橫移軸
                            distY = MotorOffset.InspStandBy_Y + CurrentTrayCarrier.InspectData.InspectionPostion._bigProductY[(int)_grabIndex.x][CurrentTrayCarrier.InspectData.InspectionPostion.MosaicYCount - 1 - mosaicCount / CurrentTrayCarrier.InspectData.InspectionPostion.MosaicXCount];  //流道縱移軸
                        }

                        Point2d _point = new Point2d(distX, distY);
                        LaserCaptureLocation.Add(_point);

                        mosaicCount++;
                    }
                }
                else //不組圖點位
                {

                    var n = CurrentTrayCarrier.InspectData.InspectionPostion._bigProductX.Length;


                    distX = CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[n - 1 - (int)_grabIndex.y];
                    distY = CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[(int)_grabIndex.x];

                    var distX1 = CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[2]; //第一個位置
                    var distY1 = CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[0];  //第一個位置

                    double partX = 0;
                    double partY = 0;
                    for (int index = 0; index < VisionController.ProductSetting.RoundSettings2[0].Groups.Count; index++)
                    {
                        partX = ProductParam.PartionXPosList[index]; //相機橫移軸 (X方向固定一顆產品)
                        partY = ProductParam.PartionYPosList[index];  //吸嘴縱移軸

                        Point2d _point = new Point2d(distX + (partX - distX1), distY + (partY - distY1));
                        LaserCaptureLocation.Add(_point);
                    }
                }
            }
            else //小產品
            {
                if (CurrentTrayCarrier.InspectData.InspectionPostion.SingleBlock)
                {
                    distX = MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._blockStepX[(int)_grabIndex.y];
                    distY = MotorOffset.InspStandBy_Y + CurrentTrayCarrier.InspectData.InspectionPostion._blockStepY[(int)_grabIndex.x];
                }
                else
                {
                    distX = MotorOffset.InspStandBy_X - CurrentTrayCarrier.InspectData.InspectionPostion._trayStepX[(int)_grabIndex.y];
                    distY = MotorOffset.InspStandBy_Y + CurrentTrayCarrier.InspectData.InspectionPostion._trayStepY[(int)_grabIndex.x];
                }

                Point2d _point = new Point2d(distX, distY);
                LaserCaptureLocation.Add(_point);
            }
        }


        private void 點位移動_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (LaserDetectImage_List != null) LaserDetectImage_List.Clear(); LaserDetectImage_List = null;
            LaserDetectImage_List = new List<CustomImage>();

            SpinWait.SpinUntil(() => false, 100);//延遲讓前段動作取像影像清空

            VisionController.Framer.OnGroupAllCaptured += GetCaptureImage;
            for (int index = 0; index < LaserCaptureLocation.Count; index++)
            {
                //移動
                var fovCenter = new Point2d(LaserCaptureLocation[index].x, LaserCaptureLocation[index].y);

                var velDefAX1 = SelectVelDef(視覺縱移軸, AY1VelList);
                var velDefBY1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);

                bool moveSuccessAX1 = MoveAxis(視覺縱移軸, fovCenter.x, velDefAX1);
                bool moveSuccessBY1 = MoveAxis(BX1_流道橫移軸, fovCenter.y, velDefBY1);

                //對焦
                var velDefAZ1 = SelectVelDef(BZ1_流道頂升升降軸, BZ1VelList);
                double AZ1Offset = MotorOffset.InspStandBy_Z + ProductParam.FocusLocations[0][0];
                bool moveSuccessAZ1 = MoveAxis(BZ1_流道頂升升降軸, AZ1Offset, velDefAZ1);

                var AX1_waitRes = 視覺縱移軸.WaitMotionDone();
                var BY1_waitRes = BX1_流道橫移軸.WaitMotionDone();
                var AZ1_waitRes = BZ1_流道頂升升降軸.WaitMotionDone();

                if (AX1_waitRes && BY1_waitRes && AZ1_waitRes)
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    e.ProcessSuccess = false;

                    if (!AX1_waitRes) LogTrace($"移至拍攝起始位置 失敗，移動:{moveSuccessAX1},等待:{AX1_waitRes}");
                    if (!BY1_waitRes) LogTrace($"移至拍攝起始位置 失敗，移動:{moveSuccessBY1},等待:{BY1_waitRes}");
                    if (!AZ1_waitRes) LogTrace($"移至拍攝起始位置 失敗，移動:{moveSuccessAZ1},等待:{AZ1_waitRes}");
                }

                //設定光源
                VisionController.Lighter.SetLight(ProductParam.Lights.ToArray());
                SpinWait.SpinUntil(() => false, 100);

                //取像              
                VisionController.Trigger1.ManualTrigger();
                SpinWait.SpinUntil(() => false, 100);
            }

            VisionController.Framer.OnGroupAllCaptured -= GetCaptureImage;

        }
        public void GetCaptureImage(object sender, StationCaptureArgs args)
        {
            LaserDetectImage_List.Add(new CustomImage(args.imgs[0]));
        }

        HTuple Center_row_List, Center_col_List;
        List<Point2d> FinalLaserPosition_List = null;
        List<Point2d> FinalLaserPosition_List_Sort = null;

        private void Block偵測_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

            HImage Image = null;
            if (FinalLaserPosition_List != null)
            {
                FinalLaserPosition_List.Clear();
                FinalLaserPosition_List = null;
            }
            //LaserDetectImage_List[0].WriteImage("bmp", 0, @"D:\test0.bmp");
            //LaserDetectImage_List[1].WriteImage("bmp", 0, @"D:\test1.bmp");
            //LaserDetectImage_List[2].WriteImage("bmp", 0, @"D:\test2.bmp");
            //LaserDetectImage_List[3].WriteImage("bmp", 0, @"D:\test3.bmp");

            for (int imageIndex = 0; imageIndex < LaserDetectImage_List.Count; imageIndex++)
            {
                if (Image != null)
                {
                    Image.Dispose();
                    Image = null;
                }

                Image = LaserDetectImage_List[imageIndex].Clone();

                //Search Region     
                HOperatorSet.FindNccModel(Image, TargetModel, -0.01, 0.02, ((double)ProductParam.SearchScore / 100f), 30, 0.5, "true", 3, out HTuple row, out HTuple col, out HTuple angle, out HTuple score);

                HTuple CircleRadius_List = new HTuple();
                for (int index = 0; index < row.Length; index++)
                {
                    HOperatorSet.TupleConcat(CircleRadius_List, ProductParam.CircleRadius, out CircleRadius_List);
                }
                HOperatorSet.GenEmptyObj(out HObject AllCircles);

                HOperatorSet.GenCircle(out HObject Circles, row, col, CircleRadius_List);
                HOperatorSet.Union1(Circles, out AllCircles);
                Circles.Dispose();

                //Target Region

                HRegion AllCircles_region = new HRegion(AllCircles);
                var ReduceImage = Image.ReduceDomain(AllCircles_region);
                var _region = ReduceImage.Threshold((HTuple)ProductParam.ThresholdMin, (HTuple)ProductParam.ThresholdMax);
                ReduceImage.Dispose(); ReduceImage = null;
                Image.Dispose(); Image = null;
                AllCircles_region.Dispose(); AllCircles_region = null;

                //中心點偵測                
                HOperatorSet.Connection(_region, out HObject conn);

                HTuple area;
                HOperatorSet.AreaCenter(conn, out area, out Center_row_List, out Center_col_List);

                //座標轉換
                var TransformCoordinate_List = CoordinateImageToMachine(imageIndex, Center_row_List, Center_col_List);

                //點位檢查
                if (FinalLaserPosition_List == null)
                    FinalLaserPosition_List = new List<Point2d>(TransformCoordinate_List);
                else
                    FinalLaserPosition_List = PositionCheck(FinalLaserPosition_List, TransformCoordinate_List);
            }

            //座標排序
            if (FinalLaserPosition_List != null)
                FinalLaserPosition_List_Sort = FinalLaserPosition_List.GroupBy(p => Math.Round(p.y / 10f)).OrderBy(g => g.Key).SelectMany(g => g.OrderBy(p => p.x)).ToList();
        }

        public List<Point2d> CoordinateImageToMachine(int ImageIndex, HTuple Center_row_List, HTuple Center_col_List)
        {
            List<Point2d> Machine_List = new List<Point2d>();

            var ImageCenter_Machine_X = LaserCaptureLocation[ImageIndex].x;
            var ImageCenter_Machine_Y = LaserCaptureLocation[ImageIndex].y;

            double imageCenter_X = VisionController.Framer.Grabbers[0].Width / 2;
            double imageCenter_Y = VisionController.Framer.Grabbers[0].Height / 2;

            for (int index = 0; index < Center_row_List.Length; index++)
            {
                double x = ImageCenter_Machine_X + (Center_col_List[index] - imageCenter_X) * PixeltoMM;
                double y = ImageCenter_Machine_Y + (Center_row_List[index] - imageCenter_Y) * PixeltoMM;

                Machine_List.Add(new Point2d(x, y));
            }
            return Machine_List;
        }
        public List<Point2d> PositionCheck(List<Point2d> TargetList, List<Point2d> CompareList)
        {
            double tolerance = 10; // 接近距離門檻

            List<Point2d> pointsToAdd = new List<Point2d>();

            foreach (var b in CompareList)
            {
                bool isClose = TargetList.Any(a => Distance(a, b) <= tolerance);

                if (!isClose)
                {
                    TargetList.Add(b);
                }
            }

            return TargetList;
        }
        double Distance(Point2d p1, Point2d p2)
        {
            double dx = p1.x - p2.x;
            double dy = p1.y - p2.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        List<LaserResult> LaserResult_List = new List<LaserResult>();

        private void 雷射高度偵測_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            //if (FinalLaserPosition_List_Sort != null)
            //{
            //    if (FinalLaserPosition_List_Sort.Count != 0)
            //    {
            //        var velDefAX1 = SelectVelDef(視覺縱移軸, AX1VelList);
            //        var velDefBY1 = SelectVelDef(BX1_流道橫移軸, BY1VelList);

            //        for (int index = 0; index < FinalLaserPosition_List_Sort.Count; index++)
            //        {
            //            double distX = FinalLaserPosition_List_Sort[index].x;
            //            double distY = FinalLaserPosition_List_Sort[index].y - LaserToCamera;

            //            bool moveSuccessAX1 = MoveAxis(視覺縱移軸, distX, velDefAX1);
            //            bool moveSuccessBY1 = MoveAxis(BX1_流道橫移軸, distY, velDefBY1);

            //            var AX1_waitRes = 視覺縱移軸.WaitMotionDone();
            //            var BY1_waitRes = BX1_流道橫移軸.WaitMotionDone();

            //            SpinWait.SpinUntil(() => false, 100);

            //            if (AX1_waitRes && AX1_waitRes)
            //            {
            //                if (LaserReader is ILaserDistanceFinder _laser)
            //                {                            
            //                    bool success = false;

            //                    for (int check_index = 0; check_index < 3; check_index++)
            //                    {                                    
            //                        success = _laser.ReadHeight(10000);

            //                        double hetghtvalue = -1;
            //                        if (success)
            //                        {
            //                            hetghtvalue = _laser.GetHeight();

            //                            if (hetghtvalue < 30) //過濾錯誤值
            //                            {
            //                                #region Laser結果輸入
            //                                LaserResult _temp = new LaserResult();
            //                                _temp.ProductIndex_X = (int)_grabIndex.x;
            //                                _temp.ProductIndex_Y = (int)_grabIndex.y;
            //                                _temp.Machine_Position_X = FinalLaserPosition_List_Sort[index].x;
            //                                _temp.Machine_Position_Y = FinalLaserPosition_List_Sort[index].y;
            //                                _temp.Height = hetghtvalue;

            //                                if (hetghtvalue < ProductParam.HeightMax && hetghtvalue > ProductParam.HeightMin)
            //                                    _temp.Result = true;
            //                                else
            //                                    _temp.Result = false;

            //                                _temp.Threshold_Max = ProductParam.HeightMax;
            //                                _temp.Threshold_Min = ProductParam.HeightMin;

            //                                LaserResult_List.Add(_temp);
            //                                #endregion

            //                                break;
            //                            }
            //                            else if (check_index==2) //量測失敗
            //                            {
            //                                #region Laser結果輸入
            //                                LaserResult _temp = new LaserResult();
            //                                _temp.ProductIndex_X = (int)_grabIndex.x;
            //                                _temp.ProductIndex_Y = (int)_grabIndex.x;
            //                                _temp.Machine_Position_X = FinalLaserPosition_List_Sort[index].x;
            //                                _temp.Machine_Position_Y = FinalLaserPosition_List_Sort[index].y;
            //                                _temp.Height = hetghtvalue;

            //                                    _temp.Result = false;

            //                                _temp.Threshold_Max = ProductParam.HeightMax;
            //                                _temp.Threshold_Min = ProductParam.HeightMin;

            //                                LaserResult_List.Add(_temp);
            //                                #endregion
            //                            }
            //                        }
            //                        else
            //                        {
            //                            SpinWait.SpinUntil(() => false, 1000);
            //                        }                                    
            //                    }                             
            //                }
            //            }
            //        }
            //    }
            //}

            if (FinalLaserPosition_List_Sort != null)
            {
                if (FinalLaserPosition_List_Sort.Count != 0)
                {
                    var velDefAX1 = SelectVelDef(視覺縱移軸, AY1VelList);
                    var velDefBY1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);

                    for (int index = 0; index < FinalLaserPosition_List_Sort.Count; index++)
                    {
                        //量測基準點
                        double distX = FinalLaserPosition_List_Sort[index].x - ProductParam.CircleRadius * PixeltoMM - 3;
                        double distY = FinalLaserPosition_List_Sort[index].y - LaserToCamera;

                        bool moveSuccessAX1 = MoveAxis(視覺縱移軸, distX, velDefAX1);
                        bool moveSuccessBY1 = MoveAxis(BX1_流道橫移軸, distY, velDefBY1);

                        var AX1_waitRes = 視覺縱移軸.WaitMotionDone();
                        var BY1_waitRes = BX1_流道橫移軸.WaitMotionDone();

                        SpinWait.SpinUntil(() => false, 100);

                        bool success_Base = false;
                        double hetghtvalue_Base = -1;
                        if (AX1_waitRes && AX1_waitRes)
                        {
                            if (LaserReader is ILaserDistanceFinder _laser)
                            {

                                for (int check_index = 0; check_index < 3; check_index++)
                                {
                                    success_Base = _laser.ReadHeight(10000);

                                    if (success_Base)
                                    {
                                        hetghtvalue_Base = _laser.GetHeight();

                                        if (hetghtvalue_Base < 30) //過濾錯誤值
                                        {
                                            success_Base = true;


                                            break;
                                        }
                                        else if (check_index == 2) //量測失敗
                                        {
                                            success_Base = false;
                                        }
                                    }
                                    else
                                    {
                                        SpinWait.SpinUntil(() => false, 1000);
                                    }
                                }
                            }
                        }

                        //量測高度點
                        distX = FinalLaserPosition_List_Sort[index].x;
                        distY = FinalLaserPosition_List_Sort[index].y - LaserToCamera;

                        moveSuccessAX1 = MoveAxis(視覺縱移軸, distX, velDefAX1);
                        moveSuccessBY1 = MoveAxis(BX1_流道橫移軸, distY, velDefBY1);

                        AX1_waitRes = 視覺縱移軸.WaitMotionDone();
                        BY1_waitRes = BX1_流道橫移軸.WaitMotionDone();

                        SpinWait.SpinUntil(() => false, 100);

                        if (AX1_waitRes && AX1_waitRes)
                        {
                            if (LaserReader is ILaserDistanceFinder _laser)
                            {
                                bool success = false;

                                for (int check_index = 0; check_index < 3; check_index++)
                                {
                                    success = _laser.ReadHeight(10000);

                                    double hetghtvalue = -1;
                                    if (success)
                                    {
                                        hetghtvalue = _laser.GetHeight();

                                        if (hetghtvalue < 30) //過濾錯誤值
                                        {

                                            #region Laser結果輸入
                                            LaserResult _temp = new LaserResult();
                                            _temp.ProductIndex_X = (int)_grabIndex.x;
                                            _temp.ProductIndex_Y = (int)_grabIndex.y;
                                            _temp.Machine_Position_X = FinalLaserPosition_List_Sort[index].x;
                                            _temp.Machine_Position_Y = FinalLaserPosition_List_Sort[index].y;

                                            _temp.Threshold_Max = ProductParam.HeightMax;
                                            _temp.Threshold_Min = ProductParam.HeightMin;

                                            if (success_Base)
                                            {
                                                _temp.Height = hetghtvalue - hetghtvalue_Base;

                                                if (hetghtvalue < ProductParam.HeightMax && hetghtvalue > ProductParam.HeightMin)
                                                    _temp.Result = true;
                                                else
                                                    _temp.Result = false;
                                            }
                                            else
                                            {
                                                _temp.Height = -9999;
                                                _temp.Result = false;
                                            }

                                            LaserResult_List.Add(_temp);
                                            #endregion

                                            break;

                                        }
                                        else if (check_index == 2) //量測失敗
                                        {
                                            #region Laser結果輸入
                                            LaserResult _temp = new LaserResult();
                                            _temp.ProductIndex_X = (int)_grabIndex.x;
                                            _temp.ProductIndex_Y = (int)_grabIndex.x;
                                            _temp.Machine_Position_X = FinalLaserPosition_List_Sort[index].x;
                                            _temp.Machine_Position_Y = FinalLaserPosition_List_Sort[index].y;
                                            _temp.Height = hetghtvalue;

                                            _temp.Result = false;

                                            _temp.Threshold_Max = ProductParam.HeightMax;
                                            _temp.Threshold_Min = ProductParam.HeightMin;

                                            LaserResult_List.Add(_temp);
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        SpinWait.SpinUntil(() => false, 1000);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public class LaserResult
        {
            public int ProductIndex_X;
            public int ProductIndex_Y;

            public double Machine_Position_X;
            public double Machine_Position_Y;

            public double Height;

            public bool Result;

            public double Threshold_Max;
            public double Threshold_Min;
        }

        public void LaserReport()
        {
            string filepath = $@"D:\Coordinator2.0\Products\{ProductName}\LaserData\" + DateTime.Now.ToString("yyyyMMdd") + $"_{ProductName}_{_curLotName}.txt";
            if (Directory.Exists($@"D:\Coordinator2.0\Products\{ProductName}\LaserData"))
            {

                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Close();
                }
            }


            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.WriteLine("Index".PadRight(15) + "Position_X.".PadRight(15) + "Position_Y".PadRight(15) + "HeightValue".PadRight(15) + "Result".PadRight(15) + "Range".PadRight(15));

                for (int index = LaserResult_List.Count - 1; index >= 0; index--)
                {
                    sw.WriteLine($"{LaserResult_List[index].ProductIndex_X}-{LaserResult_List[index].ProductIndex_Y}".PadRight(15)
                        + LaserResult_List[index].Machine_Position_X.ToString("0.00").PadRight(15) + LaserResult_List[index].Machine_Position_Y.ToString("0.00").PadRight(15)
                        + $"{LaserResult_List[index].Height}".PadRight(15)
                        + $"{LaserResult_List[index].Result}".PadRight(15)
                        + $"{LaserResult_List[index].Threshold_Min} ~ {LaserResult_List[index].Threshold_Max}"
                        );
                }

                sw.Close();
            }
        }

        private void 灰卡光衰流程_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (IsGolden || !ProductParam.OpenDecayFlow)
            {
                e.ProcessSuccess = true;
                return;
            }
            var velDefCX1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);
            var velDefAY1 = SelectVelDef(視覺縱移軸, AY1VelList);



            //移動到取像位置
            視覺縱移軸.AbsoluteMove(TATool.LightCalib_Y1, velDefAY1);

            BX1_流道橫移軸.AbsoluteMove(TATool.LightCalib_X1, velDefCX1);
            //執行光衰動作 TODO 待測試打開
            bool ret = false;
            //_wcfClient.Call(s =>
            //{
            //    ret = s.DoLightDecay();
            //});
            //if (!ret)
            //{
            //    MessageBox.Show("光衰動作失敗");
            //}


            e.ProcessSuccess = true;
        }

        private void 是否開啟雷射測高_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            if (ProductParam.UseLaserMeasure)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        #region 雷射段差子流程 (Start)
        
        
        private void 是否所有Map區域都掃完_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            //MapRegionMeasurePoints.Add(new List<double>());
            //LaserMapIndex = MapRegionMeasurePoints.Count;
            LaserMapIndex++;
            if (LaserMapIndex > ProductParam.BigProductMapSetting.MapList.Count)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void 移至Map區域開始位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            //固定x位置，移動AY1，開始位置
            var rightUp = ProductParam.BigProductMapSetting.MapList[LaserMapIndex].PositionRightUp;
            var leftDown = ProductParam.BigProductMapSetting.MapList[LaserMapIndex].PositionLeftDown;

            var distX = (rightUp.x + leftDown.x) / 2 + MotorOffset.VisionCenterChangeToLaser_BX1;
            var distY = rightUp.y + MotorOffset.VisionCenterChangeToLaser_AY1;//TODO 可能要看是右上是開始，還是左下是開始

            var velDefCX1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);
            var velDefAY1 = SelectVelDef(視覺縱移軸, AY1VelList);

            bool moveSuccessCX1 = BX1_流道橫移軸.AbsoluteMove(distX, velDefCX1, 0);
            bool moveSuccessAY1 = 視覺縱移軸.AbsoluteMove(distY, velDefAY1, 0);
            bool waitCX1 = BX1_流道橫移軸.WaitMotionDone(10000);
            bool waitAY1 = 視覺縱移軸.WaitMotionDone(10000);
            var result = waitCX1 && waitAY1;

            e.ProcessSuccess = result;
        }

        private void 雷射開啟_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (_laserFinder == null)
            {

                _laserFinder = (ILaserDistanceFinder)LaserReader;
                _laserFinder.LaserDistanceFinderScanNotify += (s, LaserDistanceFinderArgsArgs) =>
                {
                    //收到雷射掃描結果
                    if (MapRegionMeasurePoints.Count == LaserMapIndex)
                    {
                        MapRegionMeasurePoints.Add(new List<double>());
                    }
                    MapRegionMeasurePoints[LaserMapIndex].Add(LaserDistanceFinderArgsArgs.Height);
                };
            }
            _laserMeasureThread = new Thread(LaserMeasureThread);
            _laserMeasureThread.Start();
        }
        public void LaserMeasureThread()
        {
            while (true)
            {
                var success = _laserFinder.ReadHeight(2000);
                SpinWait.SpinUntil(() => false, 50);
            }
        }
        private void 移至Map區域結束位置_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            //固定x位置，移動AY1

            var rightUp = ProductParam.BigProductMapSetting.MapList[LaserMapIndex].PositionRightUp;
            var leftDown = ProductParam.BigProductMapSetting.MapList[LaserMapIndex].PositionLeftDown;

            var distX = (rightUp.x + leftDown.x) / 2 + MotorOffset.VisionCenterChangeToLaser_BX1;
            var distY = leftDown.y + MotorOffset.VisionCenterChangeToLaser_AY1;//TODO 可能要看是右上是開始，還是左下是開始

            var velDefCX1 = SelectVelDef(BX1_流道橫移軸, BX1VelList);
            var velDefAY1 = SelectVelDef(視覺縱移軸, AY1VelList);

            bool moveSuccessCX1 = BX1_流道橫移軸.AbsoluteMove(distX, velDefCX1, 0);
            bool moveSuccessAY1 = 視覺縱移軸.AbsoluteMove(distY, velDefAY1, 0);
            bool waitCX1 = BX1_流道橫移軸.WaitMotionDone(10000);
            bool waitAY1 = 視覺縱移軸.WaitMotionDone(10000);
            var result = waitCX1 && waitAY1;
            e.ProcessSuccess = result;
        }
        private void 雷射關閉_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            _laserMeasureThread.Abort();
            _laserMeasureThread = null;
            if (MapRegionMaxMinPoints.Count < MapRegionMeasurePoints.Count)
            {
                MapRegionMaxMinPoints.Add(new Point2d(MapRegionMeasurePoints[LaserMapIndex].Max(), MapRegionMeasurePoints[LaserMapIndex].Min()));
            }
        }
        private void 雷射段差結束_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            LaserMapIndex = 0;
            List<double> aveList = new List<double>();
            for (int i = 0; i < MapRegionMaxMinPoints.Count; i++)
            {
                var ave = (MapRegionMaxMinPoints[i].x + MapRegionMaxMinPoints[i].y) / 2;
                aveList.Add(ave);
            }
            if (aveList.Count > 0)
            {
                LaserBestZ = aveList.Average();
            }
            e.ProcessSuccess = true;
        }


        #endregion 雷射段差子流程 (End)



        private void 分區下SG_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (IsPartSG)
            {
                e.ProcessSuccess = true;
                return;
            }
            else
            {
                LogTrace($"--讀取SG資訊_ProcessIn--");

                AllCaptureDone = false;

                //sgQuene裡沒有資料 (異常)
                if (_sgGoCommands.Count == 0)
                {
                    LogTrace("sgCommands為空");
                    _errorMessage = Resources.Alarm_sgCommands為空;
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_SG_Fail", AlarmActive = true });
                    e.ProcessSuccess = false;
                    return;
                }

                GrabInfos curGrabInfo;
                curGrabInfo = _sgGoCommands.Peek(); //讀取一筆Quene資料

                LogTrace($"_sgGoCommands FovRow:{curGrabInfo.FOVInfos[0].FovRow} ,_sgGoCommands FovCol:{curGrabInfo.FOVInfos[0].FovCol}");

                bool ret = false;

                if (!_wcfClient.IsServerOnline)
                {
                    _wcfClient.Dispose();
                    _wcfClient =
                        new WcfDuplexClient<IVisionServerWcf, IVisionServerCallback>(Param.Port, _client,
                            timeout: Param.Timeout);
                    _wcfClient.Open();
                }


                if (CurrentTrayCarrier.IsVirtual) //離線測試
                {
                    if (!VisionController.Framer.HaveHardware)
                    {
                        //沒有硬體的話，就是用離線匯圖的方法
                        int forwardCap = 1;

                        forwardCap = VisionController.ProductSetting.RoundSettings2[0].GetAllCaptures().ToList().Count;
                        int otherTimes = 0;
                        if (TeachInspectTimes > 0)
                        {
                            otherTimes = TeachInspectTimes - 1;
                        }

                        if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
                        {
                            if (Is3DGolden)
                            {
                                OfflineImagePath = @"D:\MyTest\TA2000\Golden3D\";
                            }
                            else if (IsGolden)
                            {
                                OfflineImagePath = @"D:\MyTest\TA2000\Golden2D\";
                            }
                        }
                        else
                        {
                            OfflineImagePath = @"D:\MyTest\TA2000\BigProduct2\";//E:\LI6000\BA3000測試影像

                            //OfflineImagePath = @"D:\BA2000\LI110x110_MosaicBurn";
                        }

                        //新版離線測試
                        _wcfClient.Call(s =>
                        {
                            s.SetVirtualFramerLoadPath(OfflineImagePath, CurrentTrayCarrier.Count + otherTimes, CurrentCol + 1,
                            CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount, new int[] { forwardCap }, null, true); //離線測試用
                        });
                    }
                }


                //SG
                _wcfClient.Call(s =>
                {
                    ret = s.StartGrab(curGrabInfo);
                });



                if (ret == true)
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    LogTrace("SG fail");
                    _errorMessage = Resources.Alarm_SgFailed;
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_SG_Fail", AlarmActive = true });
                    e.ProcessSuccess = false;
                }

                IsPartSG = true;
            }
        }

        private void 小產品拍攝子流程C_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

        }

        private void Mosaic下SG_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            if (IsMosaicSG)
            {
                return;
            }
            else
            {
                LogTrace($"--讀取SG資訊_ProcessIn--");

                AllCaptureDone = false;

                //sgQuene裡沒有資料 (異常)
                if (_sgGoCommandsMosaic.Count == 0)
                {
                    LogTrace("sgCommands為空");
                    _errorMessage = Resources.Alarm_sgCommands為空;
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_SG_Fail", AlarmActive = true });
                    e.ProcessSuccess = false;
                    return;
                }

                GrabInfos curGrabInfoMosaic;
                curGrabInfoMosaic = _sgGoCommandsMosaic.Peek(); //讀取一筆Quene資料

                LogTrace($"_sgGoCommandsMosaic FovRow:{curGrabInfoMosaic.FOVInfos[0].FovRow} ,_sgGoCommandsMosaic FovCol:{curGrabInfoMosaic.FOVInfos[0].FovCol}");

                bool ret = false;

                if (!_wcfClientMosaic.IsServerOnline)
                {
                    _wcfClientMosaic.Dispose();
                    _wcfClientMosaic =
                        new WcfDuplexClient<IVisionServerWcf, IVisionServerCallback>(Param.PortMosaic, _clientMosaic,
                            timeout: Param.TimeoutMosaic);
                    _wcfClientMosaic.Open();
                }

                if (bVirtual) //離線測試
                {
                    if (!VisionController_Mosaic.Framer.HaveHardware)
                    {
                        //沒有硬體的話，就是用離線匯圖的方法
                        int forwardCap = 1;

                        forwardCap = VisionController_Mosaic.ProductSetting.RoundSettings2[0].GetAllCaptures().ToList().Count;
                        int otherTimes = 0;
                        if (TeachInspectTimes > 0)
                        {
                            otherTimes = TeachInspectTimes - 1;
                        }


                        if (CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
                        {
                            if (Is3DGolden)
                            {
                                OfflineImagePath = @"D:\MyTest\TA2000\Golden3D\";
                            }
                            else if (IsGolden)
                            {
                                OfflineImagePath = @"D:\MyTest\TA2000\Golden2D\";
                            }
                            else
                            {
                                OfflineImagePath = @"D:\MyTest\TA2000\SmallProduct\";
                            }
                        }
                        else
                        {
                            OfflineImagePath = @"D:\MyTest\TA2000\BigProduct\";
                            //OfflineImagePath = @"D:\BA2000\LI110x110_MosaicBurn";
                        }

                        List<SingleUnitMap> mosaicMaps = new List<SingleUnitMap>();
                        for (int i = 0; i < ProductParam.BigProductMapSetting.MapCount; i++)
                        {
                            if (ProductParam.BigProductMapSetting.MapList[i].UseType == "Mosaic")
                            {
                                mosaicMaps.Add(ProductParam.BigProductMapSetting.MapList[i]);
                            }
                        }
                        //新版離線測試
                        _wcfClientMosaic.Call(s =>
                        {
                            var mosaicCount = CurrentTrayCarrier.InspectData.InspectionPostion.MosaicTotalCount;

                            s.SetVirtualFramerLoadPathMosaic(OfflineImagePath, CurrentTrayCarrier.Count + otherTimes, CurrentCol + 1,
                            CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount, new int[] { mosaicMaps[0].GroupIndexes.Count }, mosaicMaps[0].MosaicPosition.Count, true); //離線測試用
                            int accCount = mosaicMaps[0].GroupIndexes.Count;
                            int beforeCount = accCount;
                            for (int i = 1; i < mosaicMaps.Count; i++)
                            {
                                s.AddVirtualFramerLoadPathMosaic(OfflineImagePath, CurrentTrayCarrier.Count + otherTimes, CurrentCol + 1,
                            CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount, new int[] { accCount + mosaicMaps[i].GroupIndexes.Count }, mosaicMaps[i].MosaicPosition.Count, true,
                            0, 0, 0, beforeCount); //離線測試用
                                accCount += mosaicMaps[i].GroupIndexes.Count;
                                beforeCount = accCount;
                            }
                        });
                    }
                }

                //單顆檢測設定YIndex固定使用第一個
                _wcfClientMosaic.Call(s =>
                {
                    s.SetSingleMosaicGroupYIndex(0);
                });

                //SG

                _wcfClientMosaic.Call(s =>
                {
                    ret = s.StartGrab(curGrabInfoMosaic);
                });


                if (ret == true)
                {
                    e.ProcessSuccess = true;
                }
                else
                {
                    LogTrace("SG fail");
                    _errorMessage = Resources.Alarm_SgFailed;
                    AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_SG_Fail", AlarmActive = true });
                    e.ProcessSuccess = false;

                }
                IsMosaicSG = true;
            }
        }

        private void 確認此回合影像是否收齊_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {
            var executor = (IControlFlowExecutor)e.Executor;
            if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType == "Mosaic"))
            {
                //不跑大產品分區流程
                _allCaptureDone.Add(true);
            }
            var result = executor.TakeFromQueue(_allCaptureDone, Param.CaptureTimeout * 1000, out bool res);
            if (ProductParam.BigProductMapSetting.MapList.All(x => x.UseType != "Mosaic"))
            {
                //只跑大產品分區流程或者小產品
                _allCaptureDoneMosaic.Add(true);
            }
            var resultMosaic = executor.TakeFromQueue(_allCaptureDoneMosaic, Param.CaptureTimeout * 1000, out bool resMosaic);
            LogTrace($"正常流程 _allCaptureDone.Count:{_allCaptureDone.Count}");

            if (e.Executor.CancelToken.IsCancellationRequested)
            {
                //有人暫停，直接跳出，等等執行再回來看有無AllCaptureDone
                e.ProcessSuccess = false;
                return;
            }

            if (res == false || resMosaic == false)  //取像失敗
            {
                AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_CaptureImage_Not_Enough", AlarmActive = true });

                var service = this.GetHtaService<IDialogService>();

                //過了Timeout時間
                LogTrace("取像Timeout，是否重試?(Yes:繼續檢測流程重新取像，No:直接略過檢測結果與流程)(會做相機重啟)");
                NotifyRedLight?.Invoke(this, new RedLightOnArgs() { IsOn = true });

                var dialogResult = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.YesNo,
                    Caption = Resources.Cap_Alarm,
                    Message = Resources.Alarm_取像Timeout
                });
                if (dialogResult == DialogResult.Yes)
                {
                    CurrentTrayCarrier.IsErrorPassInspect = false;
                }
                else
                {
                    CurrentTrayCarrier.IsErrorPassInspect = true;
                    _sgGoCommands.Clear();//清掉之後得檢測，直接出貨
                    _sgGoCommandsMosaic.Clear();
                }
                NotifyGreenLight?.Invoke(this, new GreenLightOnArgs() { IsOn = true });
                LogTrace($"使用者選擇:{dialogResult.ToString()}");

                if (VisionController.Framer.Grabbers.Count > 0)
                {
                    LogTrace($"相機重啟開始");
                    reconnectTrue = VisionController.Framer.CameraReset();
                    LogTrace($"Reconnect是否成功:{reconnectTrue}");
                }
                else
                {
                    LogTrace($"Framer裡的Grabber數量小於等於0");
                }

            }

            e.ProcessSuccess = true;
            AnyTypeAlram?.Invoke(this, new SecsNotifyArgs() { NotifyName = "Alarm_Inspection_CaptureImage_Not_Enough", AlarmActive = false });

            LogTrace($"當下的一盤觸發數量:{_totalTriggerCount}，當下的一盤Framer取像張數:{VisionController.Framer.Count}");
        }

        private void 是否所有Map都拍完_ConditionCheck(object sender, ControlFlow.Controls.BranchArgs e)
        {
            CurrentMapIndexOrder++;
            if (CurrentMapIndexOrder >= ProductParam.BigProductMapSetting.MapList.Count)//ProductParam.BigProductMapSetting.MapList.Count
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
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
    }
}
