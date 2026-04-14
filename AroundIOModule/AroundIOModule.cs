using AroundIOModule.Properties;
using Hta.MotionBase;
using HTA.Motion3.Virtual;
using HTAMachine.Machine;
using HTAMachine.Machine.Services;
using HTAMachine.Module;
using MachineUtility.Events;
using ModuleTemplate;
using ModuleTemplate.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static HTAMachine.Machine.HtaMachineController;

namespace TA2000Modules
{
    //TODO 需要考慮暫停時，有人動軸後，需要做甚麼判斷?
    //這個Module給周邊IO表填入使用
    //進維修模式:放慢速度，解鎖開門OK
    //進安全模式:解鎖開門，進ST
    //沒解鎖，強硬開門，進ST
    public partial class AroundIOModule : IModule, IAroundIO, ICanGetHtaService, ICanLog, ICanSaveParam
    {
        //#region Define Hardware
        ////INPUT (I/O)
        //[DefineHardware] public IInputIO X064000_設備安全系統正常;//重製按鈕有被按之後，才會亮
        //[DefineHardware] public IInputIO X064001_設備安全門系統正常;

        //[DefineHardware] public IInputIO X064002_光閘門開啟檢知; //M01
        //[DefineHardware] public IInputIO X064003_光閘觸發檢知; //M01

        //[DefineHardware] public IInputIO X064004_電磁鎖鎖定檢知;
        //[DefineHardware] public IInputIO X064006_設備安全門關閉;
        //[DefineHardware] public IInputIO X064008_啟動按鈕開關;
        //[DefineHardware] public IInputIO X064009_停止按鈕開關;
        //[DefineHardware] public IInputIO X064011_正壓源檢知;
        //[DefineHardware] public IInputIO X064012_負壓源檢知;
        //[DefineHardware] public IInputIO X064014_EQ1_上位1_通知已放料信號;
        //[DefineHardware] public IInputIO X064015_EQ2_下位1_通知Ready信號;
        //[DefineHardware] public IInputIO X066014_靜電消除器_警報_Warning;
        //[DefineHardware] public IInputIO X066015_靜電消除器_異常_Alarm;
        //[DefineHardware] public IAnalogInputIO AI128000_頂升治具真空流量計; //類比


        ////OUTPUT (I/O)
        //[DefineHardware] public IOutputIO Y065000_設備安全門電磁鎖;//希望解鎖有一個介面給他按
        //[DefineHardware] public IOutputIO Y065001_設備安全門系統_Reset;
        //[DefineHardware] public IOutputIO Y065003_正壓源電磁閥;
        //[DefineHardware] public IOutputIO Y065005_警示燈_紅;
        //[DefineHardware] public IOutputIO Y065006_警示燈_橙;
        //[DefineHardware] public IOutputIO Y065007_警示燈_綠;
        //[DefineHardware] public IOutputIO Y065008_警示燈_蜂鳴器;
        //[DefineHardware] public IOutputIO Y065009_EQ1_上位1_通知上位Ready信號;
        //[DefineHardware] public IOutputIO Y065010_EQ2_下位1_通知已放料信號;
        //[DefineHardware] public IOutputIO Y067003_治具真空吸取電磁閥;
        //[DefineHardware] public IOutputIO Y067015_靜電消除器_放電停止;

        


        //#endregion

        #region Define Data
        public Guid UniqId { get; set; }
        public IAroundIOService service;
        public IMachineSimpleController machineService;
        private IDialogService _dialogService;
        public MachineStateEm _currentState;
        private bool _buzzing { get; set; } = false;
        private MachineStateArgs _machinestateArgs = new MachineStateArgs();
        public string UserName { get; set; }
        private SafetyArgs _safetyArgs = new SafetyArgs();
        public ModelArgs CurrentModelArgs = new ModelArgs() { ModelName = ModelEm.安全模式 };
        private IShowWorkLogService _showWorkLogService;
        public bool ButtonIsRepair = false;
        public bool LastButtonIsRepair = false;
        private bool _isDisposed = false;
        public bool IsST = false;
        /// <summary>
        /// 需不需要Reset EtherCat
        /// </summary>
        bool _needReset = false;
        Thread _staticThread;
        public IMachineSystemSettingService SettingService;
        public VersionController VersionController1 = new VersionController();

        #endregion

        #region Define Event

        public event Action<string, string> AddTrace;
        public event Action<string, string> AddDebug;
        public event Action<string, string, Exception> AddFatal;
        public event EventHandler<object> RunMachine;
        public event EventHandler<object> PauseMachine;
        public event EventHandler<EventArgs> DataChange;
        public event EventHandler<IModule> SaveGlobalParam;
        public event EventHandler<IModule> SaveProductParam;

        #endregion

        #region Define Broadcast

        [DefineBroadcast] public event EventHandler<MachineStateArgs> OnNotifyMachineState;
        [DefineBroadcast] public event EventHandler<EmoEventArgs> OnEmgHappen;
        [DefineBroadcast] public event EventHandler<SafetyArgs> OnNotifySafetyState;
        [DefineBroadcast] public event EventHandler<ModelArgs> OnNotifyModelState;
        [DefineBroadcast] public event EventHandler<MotorSTArgs> OnNotifyMotorSTArgs;
        [DefineBroadcast] public event EventHandler<VersionController> NotifyVersion;
        [DefineBroadcast] public event EventHandler<NotifyResetMotionRequest> NotifyMotionReset;
        [DefineBroadcast] public event EventHandler<CalibNotify> NotifyCalib;
        [DefineBroadcast] public event EventHandler<ReConnectTriggerArgs> NotifyReconnectTrigger; 

        #endregion

        #region Showing in CDI2.0 

        [GroupDisplay(nameof(Resources.ClassifyTool), nameof(Resources.ProcessForm), nameof(Resources.customer_support))]
        public void ShowProcessModeForm()
        {
            var service = this.GetHtaService<IDockPanelService>();
            service.Show(this, nameof(ProcessMode));
        }

        #endregion

        #region Define Broadcast Receive

        /// <summary>
        /// 接收廣播亮紅燈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isOn"></param>
        [DefineBroadcastReceive]
        public void AlarmRedLight(object sender, RedLightOnArgs args)
        {
            if (args.IsOn)
            {
                Y065007_警示燈_綠.SetIO(false);
                Y065006_警示燈_橙.SetIO(false);
                Y065005_警示燈_紅.SetIO(true);
                StartBuzzing();
            }
        }

        /// <summary>
        /// 接收廣播亮黃燈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isOn"></param>
        [DefineBroadcastReceive]
        public void OrangeLight(object sender, OrangeLightOnArgs args)
        {
            if (args.IsOn)
            {
                Y065007_警示燈_綠.SetIO(false);
                Y065006_警示燈_橙.SetIO(true);
                Y065005_警示燈_紅.SetIO(false);
                StopBuzz();
            }
        }

        /// <summary>
        /// 接收廣播亮綠燈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isOn"></param>
        [DefineBroadcastReceive]
        public void GreenLight(object sender, GreenLightOnArgs args)
        {
            if (args.IsOn)
            {
                Y065007_警示燈_綠.SetIO(true);
                Y065006_警示燈_橙.SetIO(false);
                Y065005_警示燈_紅.SetIO(false);
                StopBuzz();
            }
        }





        #endregion


        #region Module Initial Function

        [ModuleInitialFunction(0, -1)]
        public void CheckResetMotion()
        {
            var service = this.GetHtaService<IMotionConnectService>();
            var result = service.GetIoMappingSuccess(HTA.Motion3.Info.PublisherEm.AdLinkEtherCat, 0);
            if (_needReset || !result)
            {
                NotifyMotionReset?.Invoke(this, new NotifyResetMotionRequest(false));
                service?.MotionReconnect();
                if (SettingService.SystemSetting.SelectVersion == "M00")
                {
                    Y065000_設備安全門電磁鎖.SetIO(true);//上鎖
                }
                NotifyReconnectTrigger?.Invoke(this, new ReConnectTriggerArgs());
                SaveGlobalParam?.Invoke(this, null);   //存全部Module的global參數
            }

            //if (DoLightTest() == false)
            //{
            //    throw new Exception($"系統重置InitialStep0_CheckMotionConnectError");
            //}

            NotifyMotionReset?.Invoke(this, new NotifyResetMotionRequest(true));
            _needReset = false;
        }


        #endregion


        #region Funtion
        public void Dispose()
        {
            _isDisposed = true;
            _staticThread?.Abort();
        }

        private void LogTrace(string msg)
        {
            AddTrace?.Invoke("AroundIOModule", $@"{msg}");
            string timeStr = DateTime.Now.ToString("HH:mm:ss") + " : ";
            _showWorkLogService?.AddWorkLog(this, timeStr + msg);
        }
        private void LogTrace(string funName, string msg)
        {
            AddTrace?.Invoke($@"{funName}", $@"{msg}");
            _showWorkLogService?.AddWorkLog(this, DateTime.Now.ToString("HH:mm:ss") + " : " + msg);
        }


        public void GetMachineState(object sender, MachineStateEm state)
        {
            _currentState = state;
            //_machinestateArgs.machineState = _currentState;
            OnNotifyMachineState?.Invoke(this, _machinestateArgs);
            if (state == MachineStateEm.Running)
            {
                LogTrace("GetMachineState Running");
                //Running時

                BeforeRunCheckST();

                Y065007_警示燈_綠.SetIO(true);
                Y065006_警示燈_橙.SetIO(false);
                Y065005_警示燈_紅.SetIO(false);
                StopBuzz();
            }
            else if (state == MachineStateEm.Idle || state == MachineStateEm.UnInitial || state == MachineStateEm.Setup)
            {
                LogTrace($"GetMachineState {state.ToString()}");
                Y065007_警示燈_綠.SetIO(false);
                if (!_needReset)
                {
                    Y065006_警示燈_橙.SetIO(true);
                }
                Y065005_警示燈_紅.SetIO(false);

                StopBuzz();

            }
            DataChange?.Invoke(this, new EventArgs());
        }


        /// <summary>
        /// 靜電消除器委派
        /// </summary>
        public void InitStaticEliminator()
        {
            _staticThread = new Thread(StaticEliminatorThread);
            _staticThread.Start();
        }

        public void StaticEliminatorThread()
        {
            while (true)
            {
                if (X066014_靜電風扇_1_異常 == null)
                {
                    LogTrace($@"X066014_靜電風扇_1_異常 null");
                    break;
                }

                if (X066015_靜電風扇_2_異常 == null)
                {
                    LogTrace($@"X066015_靜電消除器_異常_Alarm null");
                    break;
                }

                if (X066014_靜電風扇_1_異常.CheckIO())
                {
                    LogTrace($@"X066014_靜電消除器_警報 觸發");
                    MessageBox.Show(Resources.StaticWarning,
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                }

                SpinWait.SpinUntil(() => false, 1000);

                if (X066015_靜電風扇_2_異常.CheckIO())
                {
                    LogTrace($@"X066015_靜電消除器_異常 觸發");
                    MessageBox.Show(Resources.StaticAlarm,
                    "Alarm",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                }

                SpinWait.SpinUntil(() => false, 1000);
            }
        }


        public void StartBuzzing()
        {
            if (_buzzing)
            {
                return;
            }

            _buzzing = true;

            Task.Run(() =>
            {
                while (_buzzing)
                {
                    Y065008_警示燈_蜂鳴器.SetIO(true);
                    Task.Delay(150).Wait();
                    Y065008_警示燈_蜂鳴器.SetIO(false);
                    Task.Delay(100).Wait();
                    Y065008_警示燈_蜂鳴器.SetIO(true);
                    Task.Delay(100).Wait();
                    Y065008_警示燈_蜂鳴器.SetIO(false);
                    Task.Delay(2000).Wait();
                }
            }).ContinueWith(t => Y065008_警示燈_蜂鳴器.SetIO(false));
        }

        public void StartBuzzOne()
        {
            Y065008_警示燈_蜂鳴器.SetIO(true);
            Task.Delay(150).Wait();
            Y065008_警示燈_蜂鳴器.SetIO(false);
            Task.Delay(100).Wait();
            Y065008_警示燈_蜂鳴器.SetIO(true);
            Task.Delay(100).Wait();
            Y065008_警示燈_蜂鳴器.SetIO(false);
        }
        public void StopBuzz()
        {
            _buzzing = false;
        }
        public void MainStart()
        {
            X064008_啟動按鈕開關.OnIOChange += ((sender, args) =>
            {
                if (X064008_啟動按鈕開關.CheckIO())
                {
                    LogTrace("X064008_啟動按鈕開關 start");
                    machineService.Run();
                    LogTrace("X064008_啟動按鈕開關 end");
                }

            });
        }

        public void MainStop()
        {
            X064009_停止按鈕開關.OnIOChange += ((sender, args) =>
            {
                if (X064009_停止按鈕開關.CheckIO())
                {
                    LogTrace("X064009_停止按鈕開關 start");
                    machineService.Stop();
                    LogTrace("X064009_停止按鈕開關 end");
                }

            });
        }


        //等待電控劃分急停檢知
        public void EMOButtonEvent()
        {
            /*
            X064006_急停檢知.OnIOChange += ((sender, args) =>
            {
                if (X064006_急停檢知.CheckIO())
                {
                    OnEmgHappen?.Invoke(this, new EmoEventArgs());
                    machineService?.Abort();
                }
            });
            */
            X064000_設備安全系統正常.OnIOChange += ((sender, args) =>
            {
                if (X064000_設備安全系統正常.CheckIO() == false)
                {
                    //等同按EMO
                    LogTrace("X064000_設備安全系統正常 EMO被按或斷電 start");
                    _needReset = true;
                    OnEmgHappen?.Invoke(this, new EmoEventArgs());
                    machineService?.Abort();
                    LogTrace("X064000_設備安全系統正常 EMO被按或斷電 end");
                }
            });



        }

        /// <summary>
        /// 安全模式與維修模式的Event
        /// </summary>
        public void ModelEvent()
        {
            //按鈕會去切換門鎖，所以這邊的事件是依據介面上按鈕
            X064004_電磁鎖鎖定檢知.OnIOChange += ((s, e) =>
            {
                if (X064004_電磁鎖鎖定檢知.CheckIO() == false)
                {
                    //解鎖
                    InRepairModel();
                    LogTrace($@"X064004_電磁鎖鎖定檢知:false 解鎖");
                }
                else
                {
                    //上鎖
                    InSaftyModel();
                    LogTrace($@"X064004_電磁鎖鎖定檢知:true 上鎖");
                }
            });


            
        }
        public void BeforeRunCheckST()
        {
            if (IsST)
            {
                OnNotifyMotorSTArgs?.Invoke(this, new MotorSTArgs() { IsST = true });//通知要Servo Off Servo On
                NotifyCalib?.Invoke(this, new CalibNotify());
                IsST = false;
            }
        }

        public void SaftyDoorEvent()
        {
            if (SettingService.SystemSetting.SelectVersion == "M00")
            {
                X064006_設備安全門關閉.OnIOChange += ((sender, args) =>
                {
                    if (X064006_設備安全門關閉.CheckIO() == false)
                    {
                        //門打開
                        Task.Factory.StartNew(() =>
                        {
                            MessageBox.Show("設備安全門開啟",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                        });
                        IsST = true;//門打開進ST或斷電

                        //門打開
                        if (_currentState == MachineStateEm.Running)
                        {
                            ////Run的時候，門打開等同EMO
                            //OnEmgHappen?.Invoke(this, new EmoEventArgs());
                            //machineService?.Abort();
                            machineService?.Stop();
                        }
                        StartBuzzOne();
                        //_dialogService?.ShowDialog(this, new ShowDialogArgs()
                        //{
                        //    Caption = "Warning",
                        //    Message = "設備安全門開啟",

                        //});
                    }
                    else
                    {
                        Y065001_設備安全門系統_Reset_SP.SetIO(true);
                        SpinWait.SpinUntil(() => false, 500);
                        Y065001_設備安全門系統_Reset_SP.SetIO(false);

                    }


                });
            }
            else if (SettingService.SystemSetting.SelectVersion == "M01")
            {
                //X064002_光閘門開啟檢知.OnIOChange += ((sender, args) =>
                //{
                //    if (X064002_光閘門開啟檢知.CheckIO())
                //    {
                //        //門打開
                //        LogTrace("X064002_光閘門開啟 start");
                //        if (_currentState == MachineStateEm.Running)
                //        {
                //            //Run的時候，門打開暫停
                //            machineService?.Stop();
                //        }
                //        StartBuzzOne();
                //        MessageBox.Show("設備安全門開啟",
                //        "Warning",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning,
                //        MessageBoxDefaultButton.Button1,
                //        MessageBoxOptions.DefaultDesktopOnly);
                //    }
                //    else
                //    {
                //        //關上門後，將各軸Servo off Servo on
                //        LogTrace("X064002_光閘門關閉 start");
                //        BeforeRunCheckST();
                //        LogTrace("X064002_光閘門關閉 end");
                //    }
                //});
            }
        }



        public void MotorSTEvent()
        {
            if(X064001_設備安全門系統正常 != null)
            {
                if (SettingService.SystemSetting.SelectVersion == "M00")
                {
                    X064001_設備安全門系統正常.OnIOChange += ((sender, args) =>
                    {
                        if (X064001_設備安全門系統正常.CheckIO())
                        {
                            BeforeRunCheckST();
                        }
                        else
                        {
                            IsST = true;//已經ST
                        }
                    });
                }
                else if (SettingService.SystemSetting.SelectVersion == "M01")
                {
                    //X064003_SP.OnIOChange += ((sender, args) =>
                    //{
                    //    if (X064003_SP.CheckIO())
                    //    {
                    //        LogTrace("X064003_光閘觸發檢知 觸發 start");
                    //        Y065007_警示燈_綠.SetIO(false);
                    //        Y065006_警示燈_橙.SetIO(false);
                    //        Y065005_警示燈_紅.SetIO(true);
                    //        StartBuzzing();

                    //        if (!IsST)
                    //        {
                    //            LogTrace("X064003_光閘觸發檢知 觸發 IsST=false");
                    //            MessageBox.Show("光閘觸發",
                    //            "Warning",
                    //            MessageBoxButtons.OK,
                    //            MessageBoxIcon.Warning,
                    //            MessageBoxDefaultButton.Button1,
                    //            MessageBoxOptions.DefaultDesktopOnly);
                    //        }

                    //        IsST = true;//只要有碰過，就進ST，之後執行後才清掉
                    //        LogTrace("X064003_光閘觸發檢知 觸發 end");
                    //    }
                    //    else
                    //    {
                    //        //關上門後，將各軸Servo off Servo on
                    //        //TODO 目前測試用，之後正式的時候，將這裡刪除，統一關門做
                    //        //BeforeRunCheckST();
                    //        LogTrace("X064003_光閘觸發檢知 不觸發 start");
                    //        Y065007_警示燈_綠.SetIO(false);
                    //        Y065006_警示燈_橙.SetIO(true);
                    //        Y065005_警示燈_紅.SetIO(false);
                    //        StopBuzz();
                    //    }
                    //});
                }
            }
           
        }


        

        public void InNormalModel()
        {
            //會上鎖，確認其他安全裝置是否異常
            //LogTrace("InNormalModel Start");
            //Y065000_設備安全門電磁鎖.SetIO(true);
            //Y065001_維修模式建立.SetIO(false);
            //Y065002_安全模式建立.SetIO(false);
            //CurrentModelArgs.ModelName = ModelEm.一般模式;
            //OnNotifyModelState?.Invoke(this, CurrentModelArgs);
            //LogTrace("InNormalModel End");
        }

        /// <summary>
        /// 進入安全模式，有三種轉換狀態:1.機台狀態改變時、2.鑰匙切換時、3.介面按鈕改變時
        /// </summary>
        public void InSaftyModel()
        {
            LogTrace("InSaftyModel Start");
            Y065000_設備安全門電磁鎖.SetIO(true);//上鎖
            var velService = this.GetHtaService<IAxisConfigVelocityService>();
            velService.SetAllAxisToSafeMode();
            CurrentModelArgs.ModelName = ModelEm.安全模式;
            OnNotifyModelState?.Invoke(this, CurrentModelArgs);
            LogTrace("InSaftyModel End");
            DataChange?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 進入維修模式，有三種轉換狀態:1.機台狀態改變時、3.介面按鈕改變時
        /// </summary>
        public void InRepairModel()
        {
            if (SettingService.SystemSetting.SelectVersion == "M00")
            {
                LogTrace("InRepairModel Start");
                var velService = this.GetHtaService<IAxisConfigVelocityService>();
                velService.SetAllAxisToMaintenanceMode(50);
                CurrentModelArgs.ModelName = ModelEm.維修模式;
                OnNotifyModelState?.Invoke(this, CurrentModelArgs);
                LogTrace("InRepairModel End");
                DataChange?.Invoke(this, new EventArgs());
            }
            else if (SettingService.SystemSetting.SelectVersion == "M01")
            {
                //M01
            }
        }

        public void PreSetupIO()
        {

        }

        public void MachineControllerActive()
        {
            LogTrace(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name + "=>" + "start");

            if (File.Exists("D:\\Coordinator2.0\\LIDatas\\VersionController.xml"))
            {
                var xml = new XmlSerializer(typeof(VersionController));
                using (FileStream fs = new FileStream("D:\\Coordinator2.0\\LIDatas\\VersionController.xml", FileMode.Open))
                {
                    //反序列化XML
                    var data = (VersionController)xml.Deserialize(fs);
                    VersionController1 = data;
                }
            }
            else
            {
                VersionController1 = new VersionController();
                var xml = new XmlSerializer(typeof(VersionController));
                var fileInfo = new FileInfo("D:\\Coordinator2.0\\LIDatas\\VersionController.xml");
                if (fileInfo.Directory != null && fileInfo.Directory.Exists == false) fileInfo.Directory.Create();
                using (var f = new StreamWriter(fileInfo.FullName))
                {
                    xml.Serialize(f, VersionController1);
                }
            }

            SettingService = this.GetHtaService<IMachineSystemSettingService>();
            service = this.GetHtaService<IAroundIOService>();
            machineService = this.GetHtaService<IMachineSimpleController>();
            MainStart();
            MainStop();
            EMOButtonEvent();
            SaftyDoorEvent();
            InitStaticEliminator();
            //if(VersionController1.Version == 0)
            //if (SettingService.SystemSetting.SelectVersion == "M00")
            {
                ModelEvent();
            }
            MotorSTEvent();
        }

        public void MachineControllerReady()
        {
            //if(VersionController1.Version == 0)
            //if (SettingService.SystemSetting.SelectVersion == "M00")
            //{
            //    InSaftyModel();
            //}

            NotifyVersion?.Invoke(this, VersionController1);
        }

        public void MachineUiReady()
        {
            _showWorkLogService = this.GetHtaService<IShowWorkLogService>();
            _showWorkLogService.ShowWorkLog(this);
        }

        public void 設備安全門電磁鎖干涉(object sender, FetchExamineIoArgs args)
        {
            if (args.ActionStatus == true)
            {
                if (X064001_設備安全門系統正常 is VirtualInputIO)
                {
                    X064001_設備安全門系統正常.SetIO(true);
                }


                if (!X064001_設備安全門系統正常.CheckIO())
                {
                    LogTrace("設備安全門電磁鎖干涉 start");
                    MessageBox.Show("請關好門後再上鎖",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    args.SetFetchExamine(true);
                }
            }
        }
        #endregion


    }
}
