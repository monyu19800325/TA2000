using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using HalconDotNet;
using HTA.Utility.Structure;
using HTAMachine.Machine.Services;
using HyperInspection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static HyperInspection.FlowForm;
using System.Windows.Forms;
using System.Xml.Serialization;
using TA2000Modules;
using VisionController2.FlowForm.FlowSettingForm2;
using VisionController2.VisionController;
using HTAMachine.Module;
using HTA.Utility;
using VisionController2.CameraBurnInForm;
using VisionController2.MosaicController;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using HTA.Calibration;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class TestMosaicViewModel
    {
        TestMosaicData TestMosaicDatas = new TestMosaicData();
        double[,] MosaicPosX;
        double[,] MosaicPosY;
        public bool UseSingleSeam { get; set; } = false;

        public double Az1Pos
        {
            get => TestMosaicDatas.Az1Pos;
            set
            {
                TestMosaicDatas.Az1Pos = value;
                this.RaisePropertyChanged(x => x.Az1Pos);
            }
        }

        private int _captureIndex;
        public int CaptureIndex
        {
            get => _captureIndex;
            set
            {
                _captureIndex = value;
                this.RaisePropertyChanged(x => x.CaptureIndex);
                this.RaisePropertyChanged(x => x.Index0PosX);
                this.RaisePropertyChanged(x => x.Index0PosY);
            }
        }
        public List<int> CaptureIndexList { get; set; } = new List<int>();

        public bool SaveImage { get; set; } = false;
        int InspectCount = 0;

        public int MosaicXCount
        {
            get => TestMosaicDatas.MosaicXCount;
            set
            {
                if (TestMosaicDatas.MosaicXCount == value)
                    return;
                TestMosaicDatas.MosaicXCount = value;
                TestMosaicDatas.IndexPos.Clear();
                CaptureIndexList.Clear();
                int cnt = 0;
                for (int i = 0; i < TestMosaicDatas.MosaicXCount; i++)
                {
                    for (int j = 0; j < TestMosaicDatas.MosaicYCount; j++)
                    {
                        TestMosaicDatas.IndexPos.Add(new Point2d(0, 0));
                        CaptureIndexList.Add(cnt);
                        cnt++;
                    }
                }
                this.RaisePropertyChanged(x => x.MosaicXCount);
                this.RaisePropertyChanged(x => x.Index0PosX);
                this.RaisePropertyChanged(x => x.Index0PosY);
                this.RaisePropertyChanged(x => x.CaptureIndex);
            }
        }
        public int MosaicYCount
        {
            get => TestMosaicDatas.MosaicYCount;
            set
            {
                if (TestMosaicDatas.MosaicYCount == value)
                    return;
                TestMosaicDatas.MosaicYCount = value;
                TestMosaicDatas.IndexPos.Clear();
                CaptureIndexList.Clear();
                int cnt = 0;
                for (int i = 0; i < TestMosaicDatas.MosaicXCount; i++)
                {
                    for (int j = 0; j < TestMosaicDatas.MosaicYCount; j++)
                    {
                        TestMosaicDatas.IndexPos.Add(new Point2d(0, 0));
                        CaptureIndexList.Add(cnt);
                        cnt++;
                    }
                }
                this.RaisePropertyChanged(x => x.MosaicYCount);
                this.RaisePropertyChanged(x => x.Index0PosX);
                this.RaisePropertyChanged(x => x.Index0PosY);
                this.RaisePropertyChanged(x => x.CaptureIndex);
            }
        }

        public double Index0PosX
        {
            get => TestMosaicDatas.IndexPos[CaptureIndex].x;
            set
            {
                TestMosaicDatas.IndexPos[CaptureIndex].x = value;
                this.RaisePropertyChanged(x => x.Index0PosX);
            }
        }

        public double Index0PosY
        {
            get => TestMosaicDatas.IndexPos[CaptureIndex].y;
            set
            {
                TestMosaicDatas.IndexPos[CaptureIndex].y = value;
                this.RaisePropertyChanged(x => x.Index0PosY);
            }
        }


        public int CycleCount
        {
            get => TestMosaicDatas.CycleCount;
            set
            {
                TestMosaicDatas.CycleCount = value;
                this.RaisePropertyChanged(x => x.CycleCount);
            }
        }



        public int SelectIndex
        {
            get
            {
                if (CamIndex == "Right")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string CamIndex { get; set; }
        public List<string> VelList { get; set; } = new List<string>();
        public List<string> CamList { get; set; } = new List<string>();

        public string MoveVel { get; set; }
        public List<List<HTA.Calibration.MosaicCalibration.AccuracyVerifiedInfo>> CalResult { get; set; } = new List<List<HTA.Calibration.MosaicCalibration.AccuracyVerifiedInfo>>();
        public List<List<MosaicCalibration.AccuracyVerifiedInfo_SingleSeam>> CalResultSingleSeam { get; set; } = new List<List<MosaicCalibration.AccuracyVerifiedInfo_SingleSeam>>();
        public List<List<MosaicCalibration.AccuracyVerifiedInfo_SingleSeam>> CalResultSingleSeam2 { get; set; } = new List<List<MosaicCalibration.AccuracyVerifiedInfo_SingleSeam>>();

        public bool StopFlag { get; set; } = false;

        public Action<string, string> OnLog;
        private FocusTool focus;
        public HTA.IFramer.IStationFramer Framer;
        public HTA.TriggerServer.ITriggerChannel Trigger;
        public double LeftBaseDist;
        public double RightBaseDist;
        public double LeftOffsetDist;
        public double RightOffsetDist;
        public int MoveLimit;
        public ImageWindowAccessor Accessor;
        public HTA.LightServer.ILighter UseLighter;
        public List<double> GroupInfo;
        public string HintStr;
        public InspectionProductParam ProductParam;
        public InspectionModule Vision;
        public Action OnClose;
        public event EventHandler<HTAMachine.Machine.IModule> SaveParam;
        public BoatCarrier BoatCarrier;
        public Action<int, int> ChangeCamAndCaptureIndexTrigger;
        public Action OnRefreshBuffer;
        public FlowForm FlowFormData;
        public Action<double[]> SetLighting;
        public int TotalCount = 1;
        public MosaicViewModel2 Mosaic2Service;
        public FlowSettingForm2ViewModel FlowSettingForm2ViewModel;
        public bool IsFlowSettingDoneEvent = false;

        private int _mCamIdx = 0;//目前此站別僅有一台相機 
        private int _mDirIdx = 0;// 0 : forWard、1 : backWard 
        private int _mCapIdx;//單一相機之光源張數Index
        private int _currentCapIdx = 0;
        HImage MosaicImage;
        HImage MosaicImage2;
        HImage[] AllImage;

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
        private bool isFocus0Deg = true;

        public void Initial(InspectionProductParam param, BoatCarrier boatCarrier,
            double leftBaseDist,
            double rightBaseDist,
            double leftOffsetDist,
            double rightOffsetDist,
            int moveLimit = 20,
            ImageWindowAccessor accessor = null,
            HTA.IFramer.IStationFramer useCam = null,
            HTA.LightServer.ILighter useLighter = null,
            HTA.TriggerServer.ITriggerChannel useTrigger = null,
            List<double> groupInfo = null,
            string hintStr = null)
        {
            ProductParam = param;
            BoatCarrier = boatCarrier;
            LeftBaseDist = leftBaseDist;
            RightBaseDist = rightBaseDist;
            LeftOffsetDist = leftOffsetDist;
            RightOffsetDist = rightOffsetDist;
            MoveLimit = moveLimit;
            Accessor = accessor;
            Framer = useCam;
            UseLighter = useLighter;
            Trigger = useTrigger;
            GroupInfo = groupInfo;
            HintStr = hintStr;

            VelList = Enum.GetNames(typeof(MoveVelEm)).ToList();
            CamList = new List<string>() { "Left", "Right" };
            MoveVel = VelList.FirstOrDefault();
            CamIndex = CamList.FirstOrDefault();
            CaptureIndexList.Clear();
            CaptureIndexList.Add(0);
            TestMosaicDatas.IndexPos.Clear();
            TestMosaicDatas.IndexPos.Add(new Point2d(0, 0));
            MosaicXCount = 1;
            MosaicYCount = 1;
            TestMosaicDatas.Load();
            CaptureIndexList.Clear();
            int cnt = 0;
            for (int i = 0; i < TestMosaicDatas.MosaicXCount; i++)
            {
                for (int j = 0; j < TestMosaicDatas.MosaicYCount; j++)
                {
                    CaptureIndexList.Add(cnt);
                    cnt++;
                }
            }
        }


        public void Capture()
        {
            var queueCount = _isAddImageQueue.Count;

            for (int i = 0; i < queueCount; i++)
            {
                _isAddImageQueue.Take();
            }

            //設定去程
            _mDirIdx = 0;
            _mCamIdx = 0;

            var velAz1 = TATool.SelectVelDef(Vision.BZ1_流道頂升升降軸, Vision.BZ1VelList, MoveVel);
            Vision.BZ1_流道頂升升降軸.AbsoluteMove(TestMosaicDatas.Az1Pos, velAz1, 0);

            var waitAz1 = Vision.BZ1_流道頂升升降軸.WaitMotionDone(10000);

            if (waitAz1)
            {

            }
            else
            {
                var diag = Vision.GetHtaService<HTAMachine.Machine.Services.IDialogService>();
                diag.ShowDialog(this, new ShowDialogArgs() { Message = "AZ1未到位", Button = MessageBoxButtons.OK });
                return;
            }


            if (IsFlowSettingDoneEvent)
            {
                if (Mosaic2Service != null)
                {
                    Mosaic2Service.NotifyImageAdd -= AddImageQueue;
                }
                FlowSettingForm2ViewModel = new FlowSettingForm2ViewModel(Vision.VisionController, FlowFormData._flowHandle, null, false);
                FlowSettingForm2ViewModel.Setting.StopTrigger();

                Mosaic2Service = FlowSettingForm2ViewModel.MosaicViewModel;
                Mosaic2Service.NotifyImageAdd += AddImageQueue;
                IsFlowSettingDoneEvent = false;
            }

            var velAx1 = TATool.SelectVelDef(Vision.BX1_流道橫移軸, Vision.BX1VelList, MoveVel);
            var velAY1 = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel);

            Mosaic2Service.CurrentFovIndex = 0;
            Mosaic2Service.MosaicEditing = true;

            var framer = Vision.VisionController.Framer;
            var capture = Mosaic2Service.GetCapture(_mDirIdx);
            List<double> distXs = new List<double>();
            List<double> distYs = new List<double>();

            //var distXFirst = TestMosaicDatas.IndexPos[0].x - 10;
            //var distYFrist = TestMosaicDatas.IndexPos[0].y + 10;
            //Vision.BX1_流道橫移軸.AbsoluteMove(distYFrist, velAY1, 0);
            //Vision.視覺縱移軸.AbsoluteMove(distXFirst, velAx1, 0);

            //Vision.BX1_流道橫移軸.WaitMotionDone(20000);
            //Vision.視覺縱移軸.WaitMotionDone(20000);

            for (int i = 0; i < Mosaic2Service.GridY; i++)
            {
                //var distX2 = TestMosaicDatas.IndexPos[0].x - 10;
                //Vision.視覺縱移軸.AbsoluteMove(distX2, velAx1, 0);
                //Vision.視覺縱移軸.WaitMotionDone(20000);
                for (int j = 0; j < Mosaic2Service.GridX; j++)
                {

                    var distX = TestMosaicDatas.IndexPos[j * Mosaic2Service.GridY + i].x;
                    var distY = TestMosaicDatas.IndexPos[j * Mosaic2Service.GridY + i].y;
                    //var distY = Vision.MotorOffset.StandBy_AY1 + inspect._bigProductY[afterY][inspect._bigProductY[afterY].Length - 1 - (i *Mosaic2Service.GridY+j)];
                    distXs.Add(distX);
                    distYs.Add(distY);

                    Mosaic2Service.SetSelectPos(j, i);

                    Vision.BX1_流道橫移軸.AbsoluteMove(distY, velAY1, 0);
                    Vision.視覺縱移軸.AbsoluteMove(distX, velAx1, 0);

                    Vision.BX1_流道橫移軸.WaitMotionDone(20000);
                    Vision.視覺縱移軸.WaitMotionDone(20000);

                    Mosaic2Service.Capture();

                    for (int takes = 0; takes < capture.Length; takes++)
                    {
                        SpinWait.SpinUntil(() => _isAddImageQueue.Count > 0, 5000);
                        if (_isAddImageQueue.Count > 0)
                        {

                            var takeA = _isAddImageQueue.Take();
                        }
                        else
                        {
                            MessageBox.Show("沒有收到影像");
                            return;
                        }
                    }
                }
            }
            Mosaic2Service.BuildMosaic();
            MosaicImage?.Dispose();
            MosaicImage = Mosaic2Service.GetMosaicImage(0, 0);
            var allImage = Mosaic2Service.GetImageList(0);


            if (AllImage != null)
            {
                for (int i = 0; i < AllImage.Length; i++)
                {
                    AllImage[i]?.Dispose();
                }
            }

            var cam = Vision.VisionController.ProductSetting.MosaicSettings.CameraMosaicData[0];
            var map = cam.FovMosaicInfos[0].Map;
            var calibration = ((IMosaicController)Vision.VisionController).MosaicParams.ToArray();
            CustomImage[,] a = new CustomImage[MosaicXCount, MosaicYCount];
            string path = "D:\\TestMosaic\\MosaicImage\\";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = allImage[0, i * a.GetLength(1) + j];
                    if (SaveImage)
                    {
                        a[i, j].WriteImage("bmp", 0, path + $"Count{InspectCount}_Ori_{i*a.GetLength(1) + j}_x{distXs[j * a.GetLength(0) + i]}_y{distYs[j * a.GetLength(0) + i]}.bmp");
                    }
                }
            }
            MosaicImage2?.Dispose();
                       
            MosaicImage2 = ((MosaicMap)map).DoMosaicImage(a, out AllImage, out MosaicPosX, out MosaicPosY, UseSingleSeam);

            //尚未加入多產品檔寫入校正功能，請留意 (Justin)
            if (SaveImage)
            {
                MosaicImage2.WriteImage("bmp", 0, path + $"Count{InspectCount}_MosaicImage.bmp");
            }
            var dispatcher = this.GetService<IDispatcherService>();
            if (dispatcher != null)
            {
                dispatcher.BeginInvoke(() =>
                {
                    if (MosaicImage2 != null)
                    {
                        Accessor?.OnShowImage(MosaicImage2);
                        Accessor?.OnUpdate();
                    }
                });

            }

            FlowSettingForm2ViewModel.Done();

            OnRefreshBuffer?.Invoke();


        }

        public void Az1Move()
        {
            Vision.OnAddTrace("CalibrationModule", "Ax1Move Start");
            var vely = TATool.SelectVelDef(Vision.BZ1_流道頂升升降軸, Vision.BZ1VelList, MoveVel.ToString());
            Vision.BZ1_流道頂升升降軸.AbsoluteMove(Az1Pos, vely);
            Vision.OnAddTrace("CalibrationModule", "Ax1Move End");
        }


        public void CreateMap()
        {
            FlowFormData.FlowSettingDoneEvent -= OnFlowSettingDoEvent;
            FlowFormData.FlowSettingDoneEvent += OnFlowSettingDoEvent;
            FlowSettingForm2ViewModel = new FlowSettingForm2ViewModel(Vision.VisionController, FlowFormData._flowHandle, null, false);
            FlowSettingForm2ViewModel.Setting.StopTrigger();

            Mosaic2Service = FlowSettingForm2ViewModel.MosaicViewModel;
            Mosaic2Service.NotifyImageAdd -= AddImageQueue;
            Mosaic2Service.NotifyImageAdd += AddImageQueue;



            Mosaic2Service.SetMosaicGrid(MosaicXCount, MosaicYCount);
            Mosaic2Service.FovCount = 1;


            Mosaic2Service.MosaicEditing = true;
            Mosaic2Service.CurrentFovIndex = 0;



            for (int i = 0; i < Mosaic2Service.GridX; i++)
            {
                for (int j = 0; j < Mosaic2Service.GridY; j++)
                {
                    Mosaic2Service.SetMosaicPos(i, j, TestMosaicDatas.IndexPos[j+i* Mosaic2Service.GridY].x, TestMosaicDatas.IndexPos[j + i * Mosaic2Service.GridY].y);
                }
            }

            Mosaic2Service.CalcMosaicMap();
        }

        public void CalMosaic()
        {
            var cam = Vision.VisionController.ProductSetting.MosaicSettings.CameraMosaicData[0];
            var map = cam.FovMosaicInfos[0].Map;
            var calibration = ((IMosaicController)Vision.VisionController).MosaicParams.ToArray();
            calibration[0].Calibration.Finder.Desc.DotPitch = 2;
            calibration[0].Calibration.Finder.Desc.DotSize = 1;
            bool xForward = true;
            bool yForward = true;

            //AllImage = new HImage[4];
            //HOperatorSet.ReadImage(out HObject Mosaic, "\\\\hta-nas\\Vision 部門資料夾\\專案管理\\其他機型\\TA-1000\\測試\\組圖測試_產品位置之Dot影像蒐集\\組圖結果\\Dot\\LeftMosaicResult.bmp");
            //HOperatorSet.ReadImage(out HObject Image0, "\\\\hta-nas\\Vision 部門資料夾\\專案管理\\其他機型\\TA-1000\\測試\\組圖測試_產品位置之Dot影像蒐集\\Dot\\左\\49.75_608.75.bmp");
            //HOperatorSet.ReadImage(out HObject Image1, "\\\\hta-nas\\Vision 部門資料夾\\專案管理\\其他機型\\TA-1000\\測試\\組圖測試_產品位置之Dot影像蒐集\\Dot\\左\\49.75_653.75.bmp");
            //HOperatorSet.ReadImage(out HObject Image2, "\\\\hta-nas\\Vision 部門資料夾\\專案管理\\其他機型\\TA-1000\\測試\\組圖測試_產品位置之Dot影像蒐集\\Dot\\左\\94.75_608.75.bmp");
            //HOperatorSet.ReadImage(out HObject Image3, "\\\\hta-nas\\Vision 部門資料夾\\專案管理\\其他機型\\TA-1000\\測試\\組圖測試_產品位置之Dot影像蒐集\\Dot\\左\\94.75_653.75.bmp");
            //HImage a0 = new HImage(Image0);
            //HImage a1 = new HImage(Image1);
            //HImage a2 = new HImage(Image2);
            //HImage a3 = new HImage(Image3);
            //MosaicImage2 = new HImage(Mosaic);
            //AllImage[0] = a0;
            //AllImage[1] = a1;
            //AllImage[2] = a2;
            //AllImage[3] = a3;

            if (!UseSingleSeam)
            {
                calibration[0].Calibration.VerifyAccuracyCall(MosaicImage2, AllImage, (MosaicMap)map, true, true, 5, 0.5, out List<HTA.Calibration.MosaicCalibration.AccuracyVerifiedInfo> resultList, out _, out _
                , 0.1, 0.05, 0.1, 5, 50, 1, 30);
                CalResult.Add(resultList);
            }
            else
            {
                calibration[0].Calibration.VerifyAccuracyCall_SingleSeam((HImage)MosaicImage2, AllImage, (MosaicMap)map, xForward, yForward, MosaicPosX, MosaicPosY, out List<HTA.Calibration.MosaicCalibration.AccuracyVerifiedInfo_SingleSeam> resultListSingleSeam1, out _, out _,
                0.2, 0.05, 0.8, -1, 50, 1, 30, 2, 1, true, MosaicCalibration.SelectMaxDiffEm.Original_1);
                calibration[SelectIndex].Calibration.VerifyAccuracyCall_SingleSeam(MosaicImage2, AllImage, (MosaicMap)map, xForward, yForward, MosaicPosX, MosaicPosY, out List<MosaicCalibration.AccuracyVerifiedInfo_SingleSeam> resultListSingleSeam2, out _, out _,
                0.2, 0.05, 0.8, -1, 50, 1, 30, 2, 1, true, MosaicCalibration.SelectMaxDiffEm.Original_2);
                CalResultSingleSeam.Add(resultListSingleSeam1);
                CalResultSingleSeam2.Add(resultListSingleSeam2);
            }

        }

            
        

        public void Index0MoveCapture()
        {
            //設定去程
            _mDirIdx = 0;


            var velAz1 = TATool.SelectVelDef(Vision.BZ1_流道頂升升降軸, Vision.BZ1VelList, MoveVel);
            Vision.BZ1_流道頂升升降軸.AbsoluteMove(TestMosaicDatas.Az1Pos, velAz1, 0);

            var waitAz1 = Vision.BZ1_流道頂升升降軸.WaitMotionDone(10000);

            if (waitAz1)
            {

            }
            else
            {
                var diag = Vision.GetHtaService<HTAMachine.Machine.Services.IDialogService>();
                diag.ShowDialog(this, new ShowDialogArgs() { Message = "AZ1有軸未到位", Button = MessageBoxButtons.OK });
                return;
            }


            var velAx1 = TATool.SelectVelDef(Vision.視覺縱移軸, Vision.AY1VelList, MoveVel);
            var distX = Index0PosX;
            Vision.視覺縱移軸.AbsoluteMove(distX, velAx1, 0);

            var velBY1 = TATool.SelectVelDef(Vision.BX1_流道橫移軸, Vision.BX1VelList, MoveVel);
            var distY = Index0PosY;
            Vision.BX1_流道橫移軸.AbsoluteMove(distY, velBY1, 0);

            Vision.視覺縱移軸.WaitMotionDone(20000);
            Vision.BX1_流道橫移軸.WaitMotionDone(20000);

            var groups = Vision.VisionController.ProductSetting.RoundSettings2[_mDirIdx].Groups;
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

        public void Run()
        {
            StopFlag = false;
            Task runTask = new Task(TaskAction);
            runTask.Start();
        }

        public void TaskAction()
        {
            CalResult.Clear();
            CalResultSingleSeam.Clear();
            CalResultSingleSeam2.Clear();

            for (int i = 0; i < CycleCount; i++)
            {
                InspectCount = i + 1;
                Capture();
                CalMosaic();

                if (StopFlag)
                {
                    break;
                }
            }
            try
            {
                WriteMosaicReport();
            }
            catch(Exception e)
            {
                MessageBox.Show($"第{writeIndex}次，有問題，請確認");
            }
            var diag = Vision.GetHtaService<HTAMachine.Machine.Services.IDialogService>();
            diag.ShowDialog(this, new ShowDialogArgs() { Button = MessageBoxButtons.OK, Caption = "Info", Message = "Finish" });
        }

        public void Stop()
        {
            StopFlag = true;
        }
        int writeIndex = 0;

        public void WriteMosaicReport()
        {
            string baseFolder = "D:\\TestMosaic";
            baseFolder += "\\TestMosaicReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
             if (!Directory.Exists(baseFolder))
                Directory.CreateDirectory(baseFolder);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string csvPath = Path.Combine(baseFolder, $"PitchErrorReport_{timestamp}.csv");
            string chartPath = Path.Combine(baseFolder, $"PitchErrorChart_{timestamp}.png");



            List<List<double>> PitchErrorList = new List<List<double>>();

            if (!UseSingleSeam)
            {

                // ⛔ 動態初始化（不固定 seam 數量）
                int seamCount = CalResult[0].Count;
                int cycleCount = CalResult.Count;

                for (int i = 0; i < seamCount; i++)
                    PitchErrorList.Add(new List<double>());

                for (int i = 0; i < cycleCount; i++)
                {
                    for (int j = 0; j < seamCount; j++)
                    {
                        PitchErrorList[j].Add(CalResult[i][j].DotPitchMaxDiff);
                    }
                }

                // ⬇ 儲存 CSV
                using (StreamWriter w = new StreamWriter(csvPath))
                {
                    w.WriteLine(string.Join(",", Enumerable.Range(1, seamCount).Select(n => $"Seam{n}")));

                    for (int row = 0; row < cycleCount; row++)
                    {
                        var rowValues = new List<string>();
                        for (int col = 0; col < seamCount; col++)
                            rowValues.Add(PitchErrorList[col][row].ToString("F6"));
                        w.WriteLine(string.Join(",", rowValues));
                    }

                    w.WriteLine();
                    w.WriteLine("Seam Index,Max,Min,Diff,Mean,StdDev");

                    for (int i = 0; i < seamCount; i++)
                    {
                        var list = PitchErrorList[i];
                        double max = list.Max(), min = list.Min(), mean = list.Average();
                        double std = GetStd(list);
                        w.WriteLine($"{i + 1},{max:F6},{min:F6},{(max - min):F6},{mean:F6},{std:F6}");
                    }
                }

                // ⬇ 繪製圖表
                GeneratePitchErrorChart(PitchErrorList, chartPath, timestamp);
            }
            else
            {
                using (StreamWriter w = new StreamWriter(baseFolder + "\\" + $"TestMosaicReport_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.txt"))
                {
                    for (int i = 0; i < CalResultSingleSeam[0].Count; i++)
                    {
                        PitchErrorList.Add(new List<double>());
                    }


                    for (int i = 0; i < CalResultSingleSeam.Count; i++)
                    {
                        w.WriteLine($"Inspection Index : {i + 1}");
                        for (int j = 0; j < CalResultSingleSeam[i].Count; j++)
                        {
                            w.WriteLine($"  Seam Index:{j + 1}");
                            w.WriteLine($"  DotCutByOverlap:{CalResultSingleSeam[i][j].DotCutByOverlap}");
                            w.WriteLine($"  Dot Pitch Error :{CalResultSingleSeam[i][j].DotPitchMaxDiff}");
                            w.WriteLine($"  Dot Pitch x Error :{CalResultSingleSeam[i][j].DotPitchMaxDiff_DiffXDiff}");
                            w.WriteLine($"  Dot Pitch y Error :{CalResultSingleSeam[i][j].DotPitchMaxDiff_DiffYDiff}");
                            PitchErrorList[j].Add(CalResultSingleSeam[i][j].DotPitchMaxDiff);
                        }
                        w.WriteLine();
                    }

                    w.WriteLine($"==================================");

                    w.WriteLine("Dot Pitch Error Repeatability");
                    for (int i = 0; i < PitchErrorList.Count; i++)
                    {
                        var max = PitchErrorList[i].Max();
                        var min = PitchErrorList[i].Min();
                        var diff = max - min;
                        //var std = CalculateStandardDeviation(PitchErrorList[i]);
                        var std = PitchErrorList[i].GetStd();
                        double mean = PitchErrorList[i].Average();
                        w.WriteLine($"  Seam Index:{i + 1}");
                        w.WriteLine($"  Max:{max}");
                        w.WriteLine($"  Min:{min}");
                        w.WriteLine($"  Diff:{diff}");
                        w.WriteLine($"  Mean:{mean}");
                        w.WriteLine($"  SD:{std}");
                        w.WriteLine("");
                    }
                    w.Close();
                }
            }

            if (UseSingleSeam)
            {
                using (StreamWriter w = new StreamWriter(baseFolder + "\\" + $"TestMosaicReport_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}_2.txt"))
                {
                    w.WriteLine("---------------------------------------------------------------------------------------");
                    w.WriteLine("Mosaic Report");
                    w.WriteLine("---------------------------------------------------------------------------------------");
                    w.WriteLine($"Machine   : TA1000");
                    w.WriteLine("Origin2");
                    w.WriteLine($"Total Inspection Count :{CycleCount}");
                    w.WriteLine($"Cam:{CamIndex}");


                    PitchErrorList = new List<List<double>>();
                    List<List<double>> CircularityErrorList = new List<List<double>>();


                    for (int i = 0; i < CalResultSingleSeam2[0].Count; i++)
                    {
                        PitchErrorList.Add(new List<double>());
                    }


                    for (int i = 0; i < CalResultSingleSeam2.Count; i++)
                    {
                        w.WriteLine($"Inspection Index : {i + 1}");
                        for (int j = 0; j < CalResultSingleSeam2[i].Count; j++)
                        {
                            w.WriteLine($"  Seam Index:{j + 1}");
                            w.WriteLine($"  DotCutByOverlap:{CalResultSingleSeam2[i][j].DotCutByOverlap}");
                            w.WriteLine($"  Dot Pitch Error :{CalResultSingleSeam2[i][j].DotPitchMaxDiff}");
                            w.WriteLine($"  Dot Pitch x Error :{CalResultSingleSeam2[i][j].DotPitchMaxDiff_DiffXDiff}");
                            w.WriteLine($"  Dot Pitch y Error :{CalResultSingleSeam2[i][j].DotPitchMaxDiff_DiffYDiff}");
                            PitchErrorList[j].Add(CalResultSingleSeam2[i][j].DotPitchMaxDiff);
                        }
                        w.WriteLine();
                    }

                    w.WriteLine($"==================================");

                    w.WriteLine("Dot Pitch Error Repeatability");
                    for (int i = 0; i < PitchErrorList.Count; i++)
                    {
                        var max = PitchErrorList[i].Max();
                        var min = PitchErrorList[i].Min();
                        var diff = max - min;
                        //var std = CalculateStandardDeviation(PitchErrorList[i]);
                        var std = PitchErrorList[i].GetStd();
                        double mean = PitchErrorList[i].Average();
                        w.WriteLine($"  Seam Index:{i + 1}");
                        w.WriteLine($"  Max:{max}");
                        w.WriteLine($"  Min:{min}");
                        w.WriteLine($"  Diff:{diff}");
                        w.WriteLine($"  Mean:{mean}");
                        w.WriteLine($"  SD:{std}");
                        w.WriteLine("");
                    }


                    w.Close();

                }

                using (StreamWriter w = new StreamWriter(baseFolder + "\\" + $"RawDataMosaic_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.csv"))
                {
                    var count = CalResultSingleSeam[0][0].DotPitchList_Mosaic.Count;
                    string text = "";
                    text = "DotPitchList_Mosaic";
                    for (int i = 0; i < count + 1; i++)
                    {
                        text += ",";
                    }
                    text += "DotPitchList_Original1";
                    for (int i = 0; i < count + 1; i++)
                    {
                        text += ",";
                    }
                    text += "DotPitchList_Original2";
                    w.WriteLine($"Inspection Index,Seam Index,DotCutByOverlap,Dot Pitch Error,Dot Pitch x Error,Dot Pitch y Error," +
                        $"DotPitchMaxDiff_DotPitch,DotPitchMaxDiff_DotPitchOri,DotPitchMaxDiff_ImagePos1,DotPitchMaxDiff_ImagePos2," +
                        $"DotPitchMaxDiff_OverlapOrder,DotPitchMaxDiff_PosDiffX,DotPitchMaxDiff_PosDiffY,DotPitchMaxDiff_PosOriDiffX,DotPitchMaxDiff_PosOriDiffY," +
                        $"{text}");
                    for (int i = 0; i < CalResultSingleSeam.Count; i++)
                    {
                        for (int j = 0; j < CalResultSingleSeam[i].Count; j++)
                        {
                            string mosaics = "";
                            string org1 = "";
                            string org2 = "";
                            for (int k = 0; k < CalResultSingleSeam[i][j].DotPitchList_Mosaic.Count; k++)
                            {
                                mosaics += $"{CalResultSingleSeam[i][j].DotPitchList_Mosaic[k]},";
                                org1 += $"{CalResultSingleSeam[i][j].DotPitchList_Original1[k]},";
                                org2 += $"{CalResultSingleSeam[i][j].DotPitchList_Original2[k]},";
                            }
                            w.WriteLine($"{i + 1},{j + 1},{CalResultSingleSeam[i][j].DotCutByOverlap}," +
                                $"{CalResultSingleSeam[i][j].DotPitchMaxDiff},{CalResultSingleSeam[i][j].DotPitchMaxDiff_DiffXDiff},{CalResultSingleSeam[i][j].DotPitchMaxDiff_DiffYDiff}," +
                                $"{CalResultSingleSeam[i][j].DotPitchMaxDiff_DotPitch},{CalResultSingleSeam[i][j].DotPitchMaxDiff_DotPitchOri}," +
                                $"({CalResultSingleSeam[i][j].DotPitchMaxDiff_ImagePos1.X}-{CalResultSingleSeam[i][j].DotPitchMaxDiff_ImagePos1.Y}),({CalResultSingleSeam[i][j].DotPitchMaxDiff_ImagePos2.X}-{CalResultSingleSeam[i][j].DotPitchMaxDiff_ImagePos2.Y})," +
                                $"{CalResultSingleSeam[i][j].DotPitchMaxDiff_OverlapOrder},{CalResultSingleSeam[i][j].DotPitchMaxDiff_PosDiffX},{CalResultSingleSeam[i][j].DotPitchMaxDiff_PosDiffY}," +
                                $"{CalResultSingleSeam[i][j].DotPitchMaxDiff_PosOriDiffX},{CalResultSingleSeam[i][j].DotPitchMaxDiff_PosOriDiffY}," +
                                $"{mosaics},{org1},{org2}");

                        }
                    }

                    w.Close();
                }

                using (StreamWriter w = new StreamWriter(baseFolder + "\\" + $"RawDataMosaic_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}_2.csv"))
                {
                    var count = CalResultSingleSeam2[0][0].DotPitchList_Mosaic.Count;
                    string text = "";
                    text = "DotPitchList_Mosaic";
                    for (int i = 0; i < count + 1; i++)
                    {
                        text += ",";
                    }
                    text += "DotPitchList_Original1";
                    for (int i = 0; i < count + 1; i++)
                    {
                        text += ",";
                    }
                    text += "DotPitchList_Original2";
                    w.WriteLine($"Inspection Index,Seam Index,DotCutByOverlap,Dot Pitch Error,Dot Pitch x Error,Dot Pitch y Error," +
                        $"DotPitchMaxDiff_DotPitch,DotPitchMaxDiff_DotPitchOri,DotPitchMaxDiff_ImagePos1,DotPitchMaxDiff_ImagePos2," +
                        $"DotPitchMaxDiff_OverlapOrder,DotPitchMaxDiff_PosDiffX,DotPitchMaxDiff_PosDiffY,DotPitchMaxDiff_PosOriDiffX,DotPitchMaxDiff_PosOriDiffY," +
                        $"{text}");
                    for (int i = 0; i < CalResultSingleSeam2.Count; i++)
                    {
                        for (int j = 0; j < CalResultSingleSeam2[i].Count; j++)
                        {
                            string mosaics = "";
                            string org1 = "";
                            string org2 = "";
                            for (int k = 0; k < CalResultSingleSeam2[i][j].DotPitchList_Mosaic.Count; k++)
                            {
                                mosaics += $"{CalResultSingleSeam2[i][j].DotPitchList_Mosaic[k]},";
                                org1 += $"{CalResultSingleSeam2[i][j].DotPitchList_Original1[k]},";
                                org2 += $"{CalResultSingleSeam2[i][j].DotPitchList_Original2[k]},";
                            }
                            w.WriteLine($"{i + 1},{j + 1},{CalResultSingleSeam2[i][j].DotCutByOverlap}," +
                                $"{CalResultSingleSeam2[i][j].DotPitchMaxDiff},{CalResultSingleSeam2[i][j].DotPitchMaxDiff_DiffXDiff},{CalResultSingleSeam2[i][j].DotPitchMaxDiff_DiffYDiff}," +
                                $"{CalResultSingleSeam2[i][j].DotPitchMaxDiff_DotPitch},{CalResultSingleSeam2[i][j].DotPitchMaxDiff_DotPitchOri}," +
                                $"({CalResultSingleSeam2[i][j].DotPitchMaxDiff_ImagePos1.X}-{CalResultSingleSeam2[i][j].DotPitchMaxDiff_ImagePos1.Y}),({CalResultSingleSeam2[i][j].DotPitchMaxDiff_ImagePos2.X}-{CalResultSingleSeam2[i][j].DotPitchMaxDiff_ImagePos2.Y})," +
                                $"{CalResultSingleSeam2[i][j].DotPitchMaxDiff_OverlapOrder},{CalResultSingleSeam2[i][j].DotPitchMaxDiff_PosDiffX},{CalResultSingleSeam2[i][j].DotPitchMaxDiff_PosDiffY}," +
                                $"{CalResultSingleSeam2[i][j].DotPitchMaxDiff_PosOriDiffX},{CalResultSingleSeam2[i][j].DotPitchMaxDiff_PosOriDiffY}," +
                                $"{mosaics},{org1},{org2}");

                        }
                    }

                    w.Close();
                }

                // ➤ 產生 CalResultSingleSeam 的圖表
                if (CalResultSingleSeam != null && CalResultSingleSeam.Count > 0)
                {
                    var seam1List = new List<List<double>>();
                    for (int i = 0; i < CalResultSingleSeam[0].Count; i++)
                        seam1List.Add(new List<double>());

                    for (int i = 0; i < CalResultSingleSeam.Count; i++)
                    {
                        for (int j = 0; j < CalResultSingleSeam[i].Count; j++)
                        {
                            seam1List[j].Add(CalResultSingleSeam[i][j].DotPitchMaxDiff);
                        }
                    }

                    string chartPathWithLimit1 = Path.Combine(baseFolder, $"PitchErrorChart_WithLimit_1_{timestamp}.png");
                    GeneratePitchErrorChartWithLimit(seam1List, chartPathWithLimit1, "Dot Pitch Error with Limit - Set 1");
                }

                // ➤ 產生 CalResultSingleSeam2 的圖表（你原本已有 PitchErrorList 填入）
                if (CalResultSingleSeam2 != null && CalResultSingleSeam2.Count > 0)
                {
                    string chartPathWithLimit2 = Path.Combine(baseFolder, $"PitchErrorChart_WithLimit_2_{timestamp}.png");
                    GeneratePitchErrorChartWithLimit(PitchErrorList, chartPathWithLimit2, "Dot Pitch Error with Limit - Set 2");
                }

            }
        }

        private void GeneratePitchErrorChart(List<List<double>> errorData, string savePath, string timestamp)
        {
            var chart = new Chart();
            chart.Size = new Size(1200, 800);
            var area = new ChartArea("PitchArea")
            {
                AxisX = { Title = "Inspection Index", MajorGrid = { LineDashStyle = ChartDashStyle.Dot } },
                AxisY = {
            Title = "Dot Pitch Error (µm)",
            MajorGrid = { LineDashStyle = ChartDashStyle.Dot },
            LabelStyle = { Format = "F3" }
        },
                Position = new ElementPosition(5, 5, 88, 85)
            };
            chart.ChartAreas.Add(area);

            // ➕ ±4.5µm 閾值
            area.AxisY.StripLines.Add(new StripLine
            {
                IntervalOffset = 4.5,
                BorderColor = Color.Gray,
                BorderDashStyle = ChartDashStyle.Solid,
                BorderWidth = 1
            });
            area.AxisY.StripLines.Add(new StripLine
            {
                IntervalOffset = -4.5,
                BorderColor = Color.Gray,
                BorderDashStyle = ChartDashStyle.Solid,
                BorderWidth = 1
            });

            for (int i = 0; i < errorData.Count; i++)
            {
                var series = new Series($"Seam{i + 1}")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    ChartArea = "PitchArea"
                };

                for (int j = 0; j < errorData[i].Count; j++)
                    series.Points.AddXY(j + 1, errorData[i][j]);

                chart.Series.Add(series);
            }

            chart.Legends.Add(new Legend("MainLegend") { Docking = Docking.Bottom });

            // ➕ 統計註解
            string stats = $"Machine: TA1000\nTime: {timestamp}\nTotal Count: {errorData[0].Count}\n";
            for (int i = 0; i < errorData.Count; i++)
            {
                var list = errorData[i];
                stats += $"Seam {i + 1}: Max={list.Max():F3} Min={list.Min():F3} Avg={list.Average():F3} Std={GetStd(list):F3}\n";
            }

            chart.Annotations.Add(new TextAnnotation
            {
                Text = stats,
                Font = new Font("Consolas", 10),
                ForeColor = Color.Black,
                BackColor = Color.FromArgb(230, Color.White),
                LineColor = Color.Gray,
                X = 88,
                Y = 5,
                Width = 12,
                Height = 50,
                TextStyle = TextStyle.Default,
                Alignment = ContentAlignment.TopLeft,
                AnchorAlignment = ContentAlignment.TopRight,
                ClipToChartArea = "PitchArea"
            });

            chart.SaveImage(savePath, ChartImageFormat.Png);
        }

        private void GeneratePitchErrorChartWithLimit(List<List<double>> errorData_um, string savePath, string title = "")
        {
            int seamCount = errorData_um.Count;

            var chart = new Chart
            {
                Width = 1000,
                Height = 300 * seamCount,
                BackColor = Color.White
            };

            chart.Titles.Clear();
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            if (!string.IsNullOrWhiteSpace(title))
                chart.Titles.Add(title);

            for (int i = 0; i < seamCount; i++)
            {
                string areaName = $"SeamArea_{i}";
                var area = new ChartArea(areaName)
                {
                    AxisX = {
                Title = "Inspection Index",
                MajorGrid = { LineDashStyle = ChartDashStyle.Dot }
            },
                    AxisY = {
                Title = "Dot Pitch Error (µm)",
                MajorGrid = { LineDashStyle = ChartDashStyle.Dot },
                Minimum = -10,
                Maximum = 10
            }
                };

                chart.ChartAreas.Add(area);

                var series = new Series($"Seam {i + 1}")
                {
                    ChartType = SeriesChartType.Line,
                    ChartArea = areaName,
                    BorderWidth = 2
                };

                for (int j = 0; j < errorData_um[i].Count; j++)
                {
                    double value_um = errorData_um[i][j]; //µm
                    series.Points.AddXY(j + 1, value_um);
                }

                chart.Series.Add(series);

                // ±4.5 µm 限制線
                foreach (double limit in new[] { 4.5, -4.5 })
                {
                    var limitLine = new Series($"Limit_{limit}_{i}")
                    {
                        ChartType = SeriesChartType.Line,
                        ChartArea = areaName,
                        Color = Color.Red,
                        BorderDashStyle = ChartDashStyle.Dash
                    };
                    limitLine.Points.AddXY(1, limit);
                    limitLine.Points.AddXY(errorData_um[i].Count, limit);
                    chart.Series.Add(limitLine);
                }
            }

            chart.SaveImage(savePath, ChartImageFormat.Png);
        }



        private double GetStd(List<double> values)
        {
            double avg = values.Average();
            double sumSq = values.Sum(v => Math.Pow(v - avg, 2));
            return Math.Sqrt(sumSq / values.Count);
        }


        #region old WriteReport
        //public void WriteReport()
        //{
        //    if (Directory.Exists("D:\\TestMosaic") == false)
        //        Directory.CreateDirectory("D:\\TestMosaic");

        //    using (StreamWriter w = new StreamWriter("D:\\TestMosaic\\" + $"TestMosaicReport_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.txt"))
        //    {
        //        w.WriteLine("---------------------------------------------------------------------------------------");
        //        w.WriteLine("Mosaic Report");
        //        w.WriteLine("---------------------------------------------------------------------------------------");
        //        w.WriteLine($"Machine   : TA1000");
        //        w.WriteLine($"Total Inspection Count :{CycleCount}");


        //        List<List<double>> PitchErrorList = new List<List<double>>();
        //        List<List<double>> CircularityErrorList = new List<List<double>>();

        //        for (int i = 0; i < CalResult[0].Count; i++)
        //        {
        //            PitchErrorList.Add(new List<double>());
        //        }

        //        for (int i = 0; i < CalResult.Count; i++)
        //        {
        //            writeIndex = i + 1;
        //            w.WriteLine($"Inspection Index : {writeIndex}");
        //            for (int j = 0; j < CalResult[i].Count; j++)
        //            {
        //                w.WriteLine($"  Seam Index:{j + 1}");
        //                w.WriteLine($"  Dot Pitch Error :{CalResult[i][j].DotPitchMaxDiff}");
        //                //w.WriteLine($"  Circularity Error :{CalResult[i][j].DotCircularityMaxDiff}");
        //                var result = CalResult[i][j].DotInfoCheckSpec == true ? "Pass" : "Fail";
        //                w.WriteLine($"  Result:{result}");
        //                PitchErrorList[j].Add(CalResult[i][j].DotPitchMaxDiff);
        //            }
        //        }

        //        w.WriteLine($"==================================");

        //        w.WriteLine("Dot Pitch Error Repeatability");
        //        for (int i = 0; i < PitchErrorList.Count; i++)
        //        {
        //            var max = PitchErrorList[i].Max();
        //            var min = PitchErrorList[i].Min();
        //            var diff = max - min;
        //            var mean = PitchErrorList[i].Average();
        //            var std = PitchErrorList[i].GetStd();
        //            w.WriteLine($"  Seam Index:{i + 1}");
        //            w.WriteLine($"  Max:{max}");
        //            w.WriteLine($"  Min:{min}");
        //            w.WriteLine($"  Diff:{diff}");
        //            w.WriteLine($"  Mean:{mean}");
        //            w.WriteLine($"  Std:{std}");
        //            w.WriteLine("");
        //        }

        //        //w.WriteLine($"==================================");

        //        //w.WriteLine("Circularity Error Repeatability");
        //        //for (int i = 0; i < CircularityErrorList.Count; i++)
        //        //{
        //        //    var max = CircularityErrorList[i].Max();
        //        //    var min = CircularityErrorList[i].Min();
        //        //    var diff = max - min;
        //        //    w.WriteLine($"  Seam Index:{i + 1}");
        //        //    w.WriteLine($"  Max:{max}");
        //        //    w.WriteLine($"  Min:{min}");
        //        //    w.WriteLine($"  Min:{diff}");
        //        //}

        //        w.Close();

        //    }
        //}

        #endregion

        private void AddImageQueue(object sender, List<HTA.Utility.Structure.CustomImage> e)
        {
            IsAddImage = true;
            _isAddImageQueue.Add(IsAddImage);
        }

        public void OnFlowSettingDoEvent(object sender, EventArgs e)
        {
            IsFlowSettingDoneEvent = true;
        }

        public void FlowFormImageIn(object sender, FlowFormImageInArgs args)
        {
            args.InsertImage(_currentCapIdx);//你預期的拍攝index
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
            var groupInfo = Vision.VisionController.ProductSetting.RoundSettings2[dirIdx].Groups[groupIdx].Captures[capIdx].Light.LightingPersentage;


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

        public void Save()
        {
            TestMosaicDatas.Save();
        }

        [Serializable]
        public class TestMosaicData
        {
            public int MosaicXCount { get; set; }
            public int MosaicYCount { get; set; }
            public double Az1Pos { get; set; }
            public List<Point2d> IndexPos { get; set; } = new List<Point2d>();
            public int CycleCount { get; set; }

            public void Save()
            {
                string path = "D:\\Coordinator2.0\\TADatas\\TestMosaicData\\";
                string fileName = "TestMosaicDataSetting.xml";
                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);

                var xml = new XmlSerializer(typeof(TestMosaicData));
                var fileInfo = new FileInfo(path + fileName);
                if (fileInfo.Directory != null && fileInfo.Directory.Exists == false) fileInfo.Directory.Create();
                using (var f = new StreamWriter(fileInfo.FullName))
                {
                    xml.Serialize(f, this);
                }
            }

            public void Load()
            {
                string path = "D:\\Coordinator2.0\\TADatas\\TestMosaicData\\";
                string fileName = "TestMosaicDataSetting.xml";
                var fileInfo = new FileInfo(path + fileName);
                var xml = new XmlSerializer(typeof(TestMosaicData));
                if (File.Exists(path + fileName))
                {
                    using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open))
                    {
                        //反序列化XML
                        var data = (TestMosaicData)xml.Deserialize(fs);
                        Az1Pos = data.Az1Pos;
                        IndexPos = data.IndexPos;
                        CycleCount = data.CycleCount;
                        MosaicXCount = data.MosaicXCount;
                        MosaicYCount = data.MosaicYCount;
                    }
                }
            }
        }

    }
}
