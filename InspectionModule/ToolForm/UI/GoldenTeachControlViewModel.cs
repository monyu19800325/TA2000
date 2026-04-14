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
using System.IO;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class GoldenTeachControlViewModel :IDisposable
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
        public virtual bool LUEMoasicEnable { get; set; } = true;
        public int MosaicIndex { get; set; }
        public virtual bool BtnMoveMosaicEnable { get; set; } = true;

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
       // public SuckerModule SuckerModule;
        public Action OnClose;
        public event EventHandler<IModule> SaveParam;
        public BoatCarrier BoatCarrier;
        public Action<int, int> ChangeCamAndCaptureIndexTrigger;
        public Action OnRefreshBuffer;
        public FlowForm FlowFormData;
        public Action<double[]> SetLighting;
        public int TotalCount = 1;
        public MosaicViewModel2 Mosaic2Service;
        public FlowSettingForm2ViewModel FlowSettingForm2ViewModel;
        public bool IsFlowSettingDoneEvent = false;
        public Action NotifyOuter;
        private int _mCamIdx = 0;//目前此站別僅有一台相機 
        private int _mDirIdx = 0;// 0 : forWard、1 : backWard 
        private int _mCapIdx;//單一相機之光源張數Index
        private int _currentCapIdx = 0;
        protected ISplashScreenService SplashScreenService => this.GetService<ISplashScreenService>();
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
        public List<int> MosaicList { get; set; } = new List<int>();

        public bool Stop = false;

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

            if (Vision.CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.BigProduct)
            {
                //BigProductInit();
                TempSingleUnitMap = Vision.ProductParam.BigProductMapSetting.MapList.DeepClone();
                BigProductCalc(); 
                ProductParam.BigProductMapSetting.MapList[0].UseType = "Mosaic";
            }
            else
            {
                ProductParam.BigProductMapSetting.MapList[0].UseType = "Not Mosaic";
            }

        }


        public void BigProductCalc()
        {

            var mosaicForm = new MosaicForm(Vision.VisionController_Mosaic);
            List<Point2d[][]> point2Ds = new List<Point2d[][]>();
            var centerX = (TempSingleUnitMap[0].PositionRightUp.x + TempSingleUnitMap[0].PositionLeftDown.x)/2;
            var centerY = (TempSingleUnitMap[0].PositionRightUp.y + TempSingleUnitMap[0].PositionLeftDown.y)/2;
            var sizeX = Math.Abs(TempSingleUnitMap[0].PositionRightUp.x - TempSingleUnitMap[0].PositionLeftDown.x);
            var sizeY = Math.Abs(TempSingleUnitMap[0].PositionRightUp.y - TempSingleUnitMap[0].PositionLeftDown.y);
            //先暫時隔離 Mon 2026/02/04

            mosaicForm.FormModel.SelectedMosaicParam.CalibrationControlParam.MovePitch.x = ProductParam.BigProductMapSetting.MosaicPitchX;
            mosaicForm.FormModel.SelectedMosaicParam.CalibrationControlParam.MovePitch.y = ProductParam.BigProductMapSetting.MosaicPitchY;

            mosaicForm.SetProductPosition(Vision.ProductName, new HTA.Utility.Structure.Point2d(sizeX, sizeY),
                    new Point2d[] { new Point2d(centerX, centerY) }, false, false, new Point2d(1, 1), out var productCapturePoints);
            
            point2Ds.Add(productCapturePoints);
            TempSingleUnitMap[0].MosaicPosition.Clear();
            TempSingleUnitMap[0].MosaicPosition.AddRange(productCapturePoints[0]);

            BigProductInitMosaic(TempSingleUnitMap);

            MosaicList.Clear();
            for (int i = 0; i < TempSingleUnitMap[0].MosaicPosition.Count; i++)
            {
                MosaicList.Add(i);
            }
            MosaicIndex = MosaicList.FirstOrDefault();
            mosaicForm?.Dispose();
        }

        public void BigProductInitMosaic(List<SingleUnitMap> mosaicMaps)
        {

            FlowFormData.FlowSettingDoneEvent += OnFlowSettingDoEvent;
            FlowSettingForm2ViewModel = new FlowSettingForm2ViewModel(Vision.VisionController_Mosaic, FlowFormData._flowHandle, null, Vision.VisionController_Mosaic.Framer.HaveHardware);
            FlowSettingForm2ViewModel.Setting.StopTrigger();

            Mosaic2Service = FlowSettingForm2ViewModel.MosaicViewModel;
            Mosaic2Service.NotifyImageAdd += AddImageQueue;

            Mosaic2Service.FovCount = BoatCarrier.InspectData.InspectionPostion.YMaxStepCount*BoatCarrier.InspectData.InspectionPostion.XMaxStepCount*mosaicMaps.Count;
            var inspect = BoatCarrier.InspectData.InspectionPostion;
            for (int yIndex = 0; yIndex < BoatCarrier.InspectData.InspectionPostion.YMaxStepCount; yIndex++)
            {
                Mosaic2Service.CurrentYIndex = yIndex;
                for (int xIndex = 0; xIndex < BoatCarrier.InspectData.InspectionPostion.XMaxStepCount; xIndex++)
                {
                    Mosaic2Service.CurrentXIndex = xIndex;
                    for (int g = 0; g < mosaicMaps.Count; g++)
                    {
                        Mosaic2Service.MosaicEditing = true;
                        Mosaic2Service.CurrentFovIndex = (xIndex + BoatCarrier.InspectData.InspectionPostion.XMaxStepCount*yIndex)*mosaicMaps.Count + g;

                        Mosaic2Service.CurrentGroupIndex = g;
                        GetMosaicXYCount(mosaicMaps[g].MosaicPosition, out var mosaicXYCount);
                        mosaicMaps[g].MosaicXYCount.x = mosaicXYCount.x;
                        mosaicMaps[g].MosaicXYCount.y = mosaicXYCount.y;
                        Mosaic2Service.SetMosaicGrid((int)mosaicXYCount.x, (int)mosaicXYCount.y);

                        for (int i = 0; i < (int)mosaicXYCount.x; i++)
                        {
                            for (int j = 0; j < (int)mosaicXYCount.y; j++)
                            {
                                Mosaic2Service.SetMosaicPos(i, j, mosaicMaps[g].MosaicPosition[i*((int)mosaicXYCount.y)+j].x, mosaicMaps[g].MosaicPosition[i*((int)mosaicXYCount.y)+j].y);
                            }
                        }
                    }
                }
            }
            Mosaic2Service.CalcMosaicMap();
        }

        public void GetMosaicXYCount(List<Point2d> mosaicPos, out Point2d mosaicXYCount)
        {
            mosaicXYCount = new Point2d(0, 0);
            int xIndex = 1;
            for (int i = 0; i < mosaicPos.Count-1; i++)
            {
                if (mosaicPos[i].x == mosaicPos[i+1].x)
                {
                    xIndex++;
                }
                else
                {
                    break;
                }
            }
            int xCount = mosaicPos.Count / xIndex;


            int yCount = xIndex;
            mosaicXYCount = new Point2d(xCount, yCount);
        }

        public void Capture()
        {
            //這台Golden固定組2x2
            var queueCount = _isAddImageQueue.Count;

            for (int i = 0; i < queueCount; i++)
            {
                _isAddImageQueue.Take();
            }

            //設定去程
            _mDirIdx = 0;

            if (Vision.CurrentTrayCarrier.InspectData.InspectionPostion.ProductType
                == ProductTypeEm.SmallProduct)
            {
                //移動至顆檢測位置
                //移動至預備位置
                //var velx_standby = TATool.SelectVelDef(Vision.BX2_流道傳送軸, Vision.BX1VelList, MoveVel.ToString());
                //var vely_standby = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel.ToString());
                //Vision.BX2_流道傳送軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_X, velx_standby);
                //Vision.視覺縱移軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_Y, vely_standby);

                //var centerX = Vision.SuckerModuleThis.MotorOffset.InspectStandBy_CX1;
                //var centerY = Vision.SuckerModuleThis.MotorOffset.InspecttPosition_AY1;
                var velx_standby = TATool.SelectVelDef(Vision.BX2_流道傳送軸, Vision.BX1VelList, MoveVel.ToString());
                var vely_standby = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel.ToString());
                Vision.BX2_流道傳送軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_X, velx_standby);
                Vision.視覺縱移軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_Y, vely_standby);

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
            else
            {

                //移動至預備位置
                var velx_standby = TATool.SelectVelDef(Vision.BX2_流道傳送軸, Vision.BX1VelList, MoveVel.ToString());
            var vely_standby = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel.ToString());
            Vision.BX2_流道傳送軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_X, velx_standby);
            Vision.視覺縱移軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_Y, vely_standby);


            //移動到對焦位置
            var velAZ1 = TATool.SelectVelDef(Vision.BZ1_流道頂升升降軸, Vision.BZ1VelList, MoveVel);
            Vision.BZ1_流道頂升升降軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_Z + Vision.ProductParam.FocusLocation_Mon, velAZ1);


                if (Mosaic2Service != null)
                {
                    Mosaic2Service.NotifyImageAdd -= AddImageQueue;
                }

                FlowSettingForm2ViewModel = new FlowSettingForm2ViewModel(Vision.VisionController_Mosaic, FlowFormData._flowHandle, null, Vision.VisionController_Mosaic.Framer.HaveHardware);
                FlowSettingForm2ViewModel.Setting.StopTrigger();

                Mosaic2Service = FlowSettingForm2ViewModel.MosaicViewModel;
                Mosaic2Service.NotifyImageAdd += AddImageQueue;

                var inspect = BoatCarrier.InspectData.InspectionPostion;

                var velAY1 = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel.ToString());
                var velCX1 = TATool.SelectVelDef(Vision.BX2_流道傳送軸, Vision.BX1VelList, MoveVel.ToString());

               
                Vision.BX2_流道傳送軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_X, velx_standby);
                Vision.視覺縱移軸.AbsoluteMove(Vision.MotorOffset.InspStandBy_Y, vely_standby);


                Mosaic2Service.CurrentFovIndex = RowY - 1;  //此Round的第n顆
                Mosaic2Service.MosaicEditing = true;

                Mosaic2Service.CurrentGroupIndex = 0;
                Mosaic2Service.GridX = (int)TempSingleUnitMap[0].MosaicXYCount.x;
                Mosaic2Service.GridY = (int)TempSingleUnitMap[0].MosaicXYCount.y;

                var framer = Vision.VisionController_Mosaic.Framer;
                var capture = Mosaic2Service.GetCapture(_mDirIdx);
                for (int i = 0; i < Mosaic2Service.GridY; i++)
                {
                    for (int j = 0; j < Mosaic2Service.GridX; j++)
                    {
                        double distX = TempSingleUnitMap[0].MosaicPosition[i*Mosaic2Service.GridX+j].x;

                        double distY = TempSingleUnitMap[0].MosaicPosition[i*Mosaic2Service.GridX+j].y;

                    Vision.BX1_流道橫移軸.AbsoluteMove(distX, velCX1);
                    Vision.視覺縱移軸.AbsoluteMove(distY, velAY1);

                     Mosaic2Service.SetSelectPos(j, i);

                        Mosaic2Service.Capture(0);

                        for (int takes = 0; takes < capture.Length; takes++)
                        {
                            var takeA = _isAddImageQueue.Take();
                        }
                    }
                }
                Mosaic2Service.ShadeCorrectionMosaic(Vision.VisionController_Mosaic.GetHardware());
                Mosaic2Service.BuildMosaic();
                var image = Mosaic2Service.GetMosaicImage();
                var dispatcher = this.GetService<IDispatcherService>();
                if (dispatcher != null)
            {
                dispatcher.BeginInvoke(() =>
                {
                    if (image != null)
                    {
                        Accessor?.OnShowImage(image);
                        Accessor?.OnUpdate();
                    }
                });

            }

            FlowSettingForm2ViewModel.Done();

            //為了把校正資訊寫在影像上
            FlowFormData.AttachCalibration();

            OnRefreshBuffer?.Invoke();
        }
        }


        public void Focus()
        {
            OnLog?.Invoke("InspectionModule", "-Teach FocusLeft Start");
            if (focus != null)
                return;
            focus = new FocusTool(Vision.BZ1_流道頂升升降軸, BaseDist, OffsetDist,
                MoveLimit,0, OnLog, Accessor, Framer, UseLighter, Trigger, GroupInfo, HintStr);

            focus.FormClosing += (s, er) =>
            {
                ProductParam.FocusLocation_Mon = focus.fluent.ViewModel.NewOffsetData;
                OffsetDist = ProductParam.FocusLocation_Mon;
                focus?.Dispose();
                focus = null;
                //要儲存Offset
            };
            focus.Show();
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


        public void MosaicMove()
        {
            var inspect = BoatCarrier.InspectData.InspectionPostion;

            var velAX1 = TATool.SelectVelDef(Vision.BX1_流道橫移軸, Vision.BX1VelList, MoveVel);
            var velBY1 = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel);

            var afterX = inspect.SingleToGroupX(ColX - 1);
            var distX = TempSingleUnitMap[0].MosaicPosition[MosaicIndex].x;

            var afterY = inspect.SingleToGroupY(RowY - 1);
            var distY = TempSingleUnitMap[0].MosaicPosition[MosaicIndex].y;

            Vision.BX1_流道橫移軸.AbsoluteMove(distX, velAX1);
            Vision.視覺縱移軸.AbsoluteMove(distY, velBY1);


            var velAZ1 = TATool.SelectVelDef(Vision.BZ1_流道頂升升降軸, Vision.BZ1VelList, MoveVel);

            var AZ1_distZ = Vision.MotorOffset.InspStandBy_Z + Vision.ProductParam.FocusLocation_Mon;

            Vision.BZ1_流道頂升升降軸.AbsoluteMove(AZ1_distZ, velAZ1);

            //目前這個只做移動，如果也要拍照，把下面註解打開
            //var groups = Vision.VisionController_Mosaic.ProductSetting.RoundSettings2[_mDirIdx].Groups;
            //for (int i = 0; i < groups.Count; i++)
            //{
            //    //第幾個Group
            //    for (int j = 0; j < groups[i].Captures.Count; j++)
            //    {
            //        //的第幾張影像

            //        //設定光源
            //        _mCapIdx = i * groups[i].Captures.Count + j;
            //        UpdateSelectIndex(i, j);

            //        SpinWait.SpinUntil(() => false, 300);
            //        SendGrab();
            //        SpinWait.SpinUntil(() =>
            //        {
            //            Application.DoEvents();//這樣才會即時更新畫面
            //            return false;
            //        }, 500);
            //        OnRefreshBuffer?.Invoke();
            //    }
            //}
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


            //OnLog?.Invoke("InspectionModule", "-Teach StopInspect Start");
            //Stop = true;
            //OnLog?.Invoke("InspectionModule", "-Teach StopInspect End");
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
            FlowSettingForm2ViewModel?.Dispose();
            Mosaic2Service?.Dispose();
        }
        public void VisionProductSaved(object sender, ProductSaveArgs args)
        {
            if(TempSingleUnitMap[0].UseType == "Mosaic")
            {
                Vision.ProductParam.BigProductMapSetting.MapList[0] = TempSingleUnitMap[0];
                Vision.SaveVisionProductParam(this, Vision);
            }
        }
    }
}
