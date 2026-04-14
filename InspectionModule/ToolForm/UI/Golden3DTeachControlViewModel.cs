using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HyperInspection;
using static HyperInspection.FlowForm;
using VisionController2.FlowForm.FlowSettingForm2;
using HTAMachine.Machine;
using HTA.MainController;
using HTAMachine.Machine.Services;
using HTAMachine.Module;
using static TA2000Modules.InspectionModule;
using HTA.Utility.Common;
using VisionController2.MosaicController;
using HTA.Utility.Structure;
using HTA.TriggerServer;

namespace TA2000Modules
{
    //3D golden 為小產品，一個Fov看到一顆產品，一顆產品裡有component(4x4顆)，1row,1col
    [POCOViewModel()]
    public class Golden3DTeachControlViewModel : IDisposable
    {
        public List<int> XList { get; set; } = new List<int>();
        public List<int> YList { get; set; } = new List<int>();
        public List<string> VelList { get; set; } = new List<string>();
        public int ColX { get; set; }
        public int RowY { get; set; }
        public string MoveVel { get; set; }
        public int InspectCounts
        {
            get => Vision.InspectCounts;
            set
            {
                if (Vision.InspectCounts == value)
                    return;
                Vision.InspectCounts = value;
                this.RaisePropertyChanged(x => x.InspectCounts);
            }
        }

        public virtual bool IsCapture0Enable { get; set; } = true;
        public virtual bool IsFocusEnable { get; set; } = true;
        public virtual bool IsInspectEnable { get; set; } = true;
        public virtual bool IsStopInspectEnable { get; set; }
        public virtual bool IsFinishGoldenEnable { get; set; } = true;
        public virtual bool IsInspectTimesEnable { get; set; } = true;

        public Action<string, string> OnLog;
        private FocusTool focus;
        public HTA.IFramer.IStationFramer Framer;
        public HTA.TriggerServer.ITriggerChannel Trigger;
        public double BaseDist;
        public double OffsetDist;
        public int MoveLimit;
        public ImageWindowAccessor Accessor;
        public HTA.LightServer.ILighter UseLighter;
        public List<double> GroupInfo;
        public string HintStr;
        public InspectionProductParam ProductParam;
        public InspectionModule Vision;
        //public SuckerModule SuckerModule;
        public Action OnClose;
        public event EventHandler<IModule> SaveParam;
        public BoatCarrier BoatCarrier;
        public Action<int, int> ChangeCamAndCaptureIndexTrigger;
        public Action OnRefreshBuffer;
        public FlowForm FlowFormData;
        public Action<double[]> SetLighting;
        public int TotalCount = 1;
        public bool IsFlowSettingDoneEvent = false;

        private int _mCamIdx = 0;//目前此站別僅有一台相機 
        private int _mDirIdx = 0;// 0 : forWard、1 : backWard 
        private int _mCapIdx;//單一相機之光源張數Index
        private int _currentCapIdx = 0;
        public Action NotifyOuter;
        public List<SingleUnitMap> TempSingleUnitMap;

        /// <summary> ForwardCaptureNum
        /// 將使用事件透過 teachProcess、ＦｌｏｗＦｏｒｍ取得ｆｏｒｗａｒｄ取像總張數
        /// </summary> 為 flowSetting 內的FlowHandle.ProductSetting.RunningSettings[0].CaptureNum
        public int ForwardCaptureNum = 1;
        public Func<int, int, List<double>> GetLightingPercentage;
        public double Pin1OffsetDist;
        public double PVIOffsetDist;
        public Form MdiParent;
        public bool IsAddImage = false;
        private BlockingCollection<bool> _isAddImageQueue = new BlockingCollection<bool>();
        private bool _offlineTest = false;

        public bool Stop = false;

        public ContainerButtonForm ContainerForm;
        public Action<Form, int> OnShowPanel;
        public event EventHandler<IModule> SaveVisionProductParam;


        public void Initial(InspectionProductParam param, BoatCarrier boatCarrier,
            double baseDist,
            double offsetDist,
            int moveLimit = 2,
            ImageWindowAccessor accessor = null,
            HTA.IFramer.IStationFramer useCam = null,
            HTA.LightServer.ILighter useLighter = null,
            HTA.TriggerServer.ITriggerChannel useTrigger = null,
            List<double> groupInfo = null,
            string hintStr = null)
        {
            ProductParam = param;
            BoatCarrier = boatCarrier;
            BaseDist = baseDist;
            OffsetDist = offsetDist;
            MoveLimit = moveLimit;
            Accessor = accessor;
            Framer = useCam;
            UseLighter = useLighter;
            Trigger = useTrigger;
            GroupInfo = groupInfo;
            HintStr = hintStr;

            VelList = Enum.GetNames(typeof(MoveVelEm)).ToList();

            XList.Clear();
            YList.Clear();
            for (int i = 0; i < Vision.CurrentTrayCarrier.InspectData.InspectionPostion.XMaxStepCount; i++)
            {
                XList.Add(i+1);
            }
            for (int i = 0; i < Vision.CurrentTrayCarrier.InspectData.InspectionPostion.YMaxStepCount; i++)
            {
                YList.Add(i+1);
            }
            ColX = XList.FirstOrDefault();
            RowY = YList.FirstOrDefault();
            MoveVel = VelList.FirstOrDefault();
            ProductParam.BigProductMapSetting.MapList[0].UseType = "Not Mosaic";
        }


        
        public void Capture()
        {
            //3D Golden 一個Fov看到一顆產品，一顆產品裡有component(4x4顆)，1row,1col
            var queueCount = _isAddImageQueue.Count;

            for (int i = 0; i < queueCount; i++)
            {
                _isAddImageQueue.Take();
            }

            //設定去程
            _mDirIdx = 0;


            //移動至預備位置
            var velx_standby = TATool.SelectVelDef(Vision.BX1_流道橫移軸, Vision.BX1VelList, MoveVel.ToString());
            var vely_standby = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel.ToString());

            //將產品移到準備位置
            Vision.視覺縱移軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_Y, vely_standby);
            Vision.BX1_流道橫移軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_X, velx_standby);


            //移動到對焦位置
            var velAZ1 = TATool.SelectVelDef(Vision.BZ1_流道頂升升降軸, Vision.BZ1VelList, MoveVel);
            Vision.BZ1_流道頂升升降軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_Z + Vision.ProductParam.FocusLocation_Mon, velAZ1);


            var groups = Vision.VisionController_Mosaic.ProductSetting.RoundSettings2[_mDirIdx].Groups;
            for (int i = 0; i < groups.Count; i++)
            {
                //第幾個Group
                for (int j = 0; j < groups[i].Captures.Count; j++)
                {
                    //的第幾張影像

                    //設定光源
                    _mCapIdx = i * groups[i].Captures.Count + j;
                    UpdateSelectIndex(i, j);

                    SpinWait.SpinUntil(() => false, 300);
                    SendGrab();
                    SpinWait.SpinUntil(() =>
                    {
                        Application.DoEvents();//這樣才會即時更新畫面
                        return false;
                    }, 500);
                    OnRefreshBuffer?.Invoke();
                }
            }


        }



        public void Focus()
        {
            OnLog?.Invoke("InspectionModule", "-Teach FocusLeft Start");
            if (ContainerForm != null)
            {
                return;
            }
            if (focus != null)
                return;
            focus = new FocusTool(Vision.BZ1_流道頂升升降軸, BaseDist, OffsetDist,
                MoveLimit, 0, OnLog, Accessor, Framer, UseLighter, Trigger, GroupInfo, HintStr);

            focus.FormClosing += (s, er) =>
            {
                ProductParam.FocusLocation_Mon = focus.fluent.ViewModel.NewOffsetData;
                OffsetDist = ProductParam.FocusLocation_Mon;
                focus?.Dispose();
                focus = null;
                SaveVisionProductParam?.Invoke(this, Vision);
                //要儲存Offset
            };
            if (ContainerForm == null)
            {
                ContainerForm = new ContainerButtonForm(focus);
                ContainerForm.FormClosed +=(s, e) =>
                {
                    focus.Close();
                    ContainerForm.Dispose();
                    ContainerForm = null;
                };
                OnShowPanel?.Invoke(ContainerForm, 1000);
            }
            //focus.Show();
            OnLog?.Invoke("InspectionModule", "-Teach FocusLeft End");

        }


        public void Inspect()
        {
            OnLog?.Invoke("InspectionModule", "-Teach Inspect Start");
            Vision.GoInspect = true;
            Vision.GoInsepctQueue.Add(Vision.GoInspect);
            SaveParam?.Invoke(this, Vision);
            InspectEnabledAllComponent(false);
            NotifyOuter?.Invoke();
            //OnClose?.Invoke();
            OnLog?.Invoke("InspectionModule", "-Teach Inspect End");

        }

        /// <summary>
        /// 臨時暫停檢測(會等待目前整盤檢測結束後再暫停)
        /// </summary>
        public void StopInspect()
        {
            OnLog?.Invoke("InspectionModule", "-Teach StopInspect Start");
            Vision.TeachImmediatelyStop = true;

            //這邊要用Task，介面才不會卡死
            Task.Factory.StartNew(() =>
            {
                var dispatcher = this.GetService<IDispatcherService>();
                dispatcher.Invoke(() =>
                {
                    IsStopInspectEnable = false;
                });
                var service = Vision.GetHtaService<HTAMachine.Machine.Services.IDialogService>();
                var show = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "提示",
                    Message = "等待檢測暫停"
                });
                Vision.TeachBackQueue.Take();
                dispatcher.Invoke(() =>
                {
                    InspectEnabledAllComponent(true);
                });
            });

            OnLog?.Invoke("InspectionModule", "-Teach StopInspect End");
        }

        public void FinishGolden()
        {
            Vision.Teaching = false;
            Vision.GoInspect = true;
            Vision.GoInsepctQueue.Add(Vision.GoInspect);
            Vision.GoldenFinishFlag = true;
        }

        private void InspectEnabledAllComponent(bool value)
        {
            IsFinishGoldenEnable = value;
            IsCapture0Enable = value;
            IsFocusEnable = value;
            IsInspectEnable = value;
            IsInspectTimesEnable = value;
            IsStopInspectEnable = !value;
        }

        private void UpdateSelectIndex(int groupIdx, int inGroupCapIdx)
        {
            OnLog?.Invoke("InspectionModule", "-Teach UpdateSelectIndex Start");
            var camIdx = Math.Min(Framer.Count - 1, Math.Max(0, _mCamIdx));

            int capIdx = _mCapIdx;
            _currentCapIdx = capIdx;
            //capIdx = Math.Max(0, capIdx);
            ChangeCamAndCapFunc(camIdx, capIdx);

            SetCurrentLightToDevice(_mDirIdx, inGroupCapIdx, groupIdx);
            OnLog?.Invoke("InspectionModule", "-Teach UpdateSelectIndex End");
        }

        private void ChangeCamAndCapFunc(int camIdx, int captureIdx)
        {
            OnLog?.Invoke("InspectionModule", $"-Teach ChangeCamAndCapFunc camIdx={camIdx},captureIdx={captureIdx}  Start");
            ChangeCamAndCaptureIndexTrigger?.Invoke(camIdx, captureIdx);
            OnLog?.Invoke("InspectionModule", $"-Teach ChangeCamAndCapFunc camIdx={camIdx},captureIdx={captureIdx} End");
        }

        private void SetCurrentLightToDevice(int dirIdx, int capIdx, int groupIdx)
        {
            OnLog?.Invoke("InspectionModule", $"-Teach SetCurrentLightToDevice dirIdx={dirIdx},capIdx={capIdx}  Start");
            if (capIdx == -1)
                return;

            var captureGroupIdx = groupIdx;//第幾個GroupCapture
            //設定到光源上
            var groupInfo = Vision.VisionController_Mosaic.ProductSetting.RoundSettings2[dirIdx].Groups[groupIdx].Captures[capIdx].Light.LightingPersentage;


            double[] groupInfoArr = new double[32];
            for (int i = 0; i < groupInfo.Count; i++)
            {
                groupInfoArr[i] = groupInfo[i];
            }
            SetLighting?.Invoke(groupInfoArr);
            OnLog?.Invoke("InspectionModule", $"-Teach SetCurrentLightToDevice dirIdx={dirIdx},capIdx={capIdx},groupIdx={groupIdx}  End");
        }
        private void SendGrab()
        {
            Trigger.ManualTrigger();
        }

        //待測試
        public void FlowFormImageIn(object sender, FlowFormImageInArgs args)
        {
            args.InsertImage(_currentCapIdx);//你預期的拍攝index
        }
        private void AddImageQueue(object sender, List<HTA.Utility.Structure.CustomImage> e)
        {
            IsAddImage = true;
            _isAddImageQueue.Add(IsAddImage);
        }

        public void OnFlowSettingDoEvent(object sender, EventArgs e)
        {
            IsFlowSettingDoneEvent = true;
        }

        public void Dispose()
        {

        }
    }
}
