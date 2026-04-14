using DevExpress.Mvvm.POCO;
using HalconDotNet;
using Hta.MotionBase;
using HTA.IFramer;
using HTA.InspectionFlow;
using HTA.LightServer;
using HTA.MainController;
using HTA.Utility.Structure;
using HyperInspection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VisionController2.FlowForm.FlowSettingForm2;
using VisionController2.MosaicController;
using static HTA.MainController.RoundSetting2;

namespace TA2000Modules
{
    public class ProductSettingToolViewModel : IDisposable
    {
        public IAxis XAxis;
        public IAxis YAxis;
        public IAxis ZAxis;
        public FlowSettingForm2ViewModel FlowSettingForm2ViewModel;
        public FlowForm FlowFormData;
        public MosaicViewModel2 Mosaic2Service;
        public virtual double XYStep { get; set; }
        public InspectionModule Module;
        public bool IsFlowSettingDoneEvent = false;
        public bool IsAddImage = false;
        private BlockingCollection<bool> _isAddImageQueue = new BlockingCollection<bool>();
        public BindingList<CaptureTreeView> CaptureData;
        Capture CurrentCapture;
        public HWindowControl HWindowControlMap;
        public HImage MapImage;
        public string ProductName = "";
        public List<string> XYStepList { get; set; } = new List<string>() { "0.01", "0.1", "1", "5", "10", "50" };
        public List<string> ZStepList { get; set; } = new List<string>() { "0.01", "0.1", "1", "5", "10" };
        public List<string> TypeList { get; set; } = new List<string>() { "Mosaic", "Not Mosaic" };
        public List<int> MapIndexList { get; set; } = new List<int>();
        public List<string> MoveVel { get; set; } = new List<string>() { "Very High", "High", "Meduim", "Slow", "Very Slow" };
        string _mapMouseInfo = "";
        public string MapMouseInfo
        {
            get => _mapMouseInfo;
            set
            {
                _mapMouseInfo = value;
                this.RaisePropertyChanged(x => x.MapMouseInfo);
            }
        }

        public List<Point2d> BigMapPosition { get; set; } = new List<Point2d>();
        public CustomImage RealImage = new CustomImage();
        public Panel LightPanel { get; set; }
        public double ImageWidth = 9334;
        public double ImageHeight = 7000;
        double[] groupInfo = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public Point2d LeftTop = new Point2d(0, 0);
        public Point2d RightBot = new Point2d(0, 0);
        public bool IsDrag;
        public Point DragPoint;
        public bool OpenZoomMap = false;
        public bool OpenMoveMap = false;
        public string BtnZoomMapContent
        {
            get
            {
                if (OpenZoomMap)
                    return "關閉縮放";
                else
                    return "開啟縮放";
            }
        }
        public string BtnMoveMapContent
        {
            get
            {
                if (OpenMoveMap)
                    return "關閉移動";
                else
                    return "開啟移動";
            }
        }

        public double XPos
        {
            get => XAxis.ActualPos;
        }

        public double YPos
        {
            get => YAxis.ActualPos;
        }

        public double ZPos
        {
            get => ZAxis.ActualPos;
        }

        public virtual string SelectXYStep
        {
            get; set;
        }

        public virtual string SelectZStep
        {
            get; set;
        }

        public virtual string SelectVel { get; set; }

        public double Gain
        {
            get => Module.VisionController.Framer.Grabbers[0].Gain;
            set
            {
                Module.VisionController.Framer.Grabbers[0].Gain = value;
            }
        }


        public double PitchX
        {
            get => Module.ProductParam.BigProductMapSetting.MosaicPitchX;
            set
            {
                Module.ProductParam.BigProductMapSetting.MosaicPitchX = value;
            }
        }
        public double PitchY
        {
            get => Module.ProductParam.BigProductMapSetting.MosaicPitchY;
            set
            {
                Module.ProductParam.BigProductMapSetting.MosaicPitchY = value;
            }
        }

        double _productRightUpX;
        public double ProductRightUpX
        {
            get => _productRightUpX;
            set
            {
                _productRightUpX = value;
            }
        }
        double _productRightUpY;
        public double ProductRightUpY
        {
            get => _productRightUpY;
            set
            {
                _productRightUpY = value;
            }
        }
        double _productLeftDownX;
        public double ProductLeftDownX
        {
            get => _productLeftDownX;
            set
            {
                _productLeftDownX = value;
            }
        }
        double _productLeftDownY;
        public double ProductLeftDownY
        {
            get => _productLeftDownY;
            set
            {
                _productLeftDownY = value;
            }
        }

        int _selectMapIndex = -1;
        public int SelectMapIndex
        {
            get => _selectMapIndex;
            set
            {
                if (_selectMapIndex == value)
                    return;
                _selectMapIndex = value;
                CurrentRightUpX = Module.ProductParam.BigProductMapSetting.MapList[_selectMapIndex].PositionRightUp.x;
                CurrentRightUpY = Module.ProductParam.BigProductMapSetting.MapList[_selectMapIndex].PositionRightUp.y;
                CurrentLeftDownX = Module.ProductParam.BigProductMapSetting.MapList[_selectMapIndex].PositionLeftDown.x;
                CurrentLeftDownY = Module.ProductParam.BigProductMapSetting.MapList[_selectMapIndex].PositionLeftDown.y;
                this.RaisePropertiesChanged();
            }
        }

        public string SelectType
        {
            get => Module.ProductParam.BigProductMapSetting.MapList[SelectMapIndex].UseType;
            set
            {
                Module.ProductParam.BigProductMapSetting.MapList[SelectMapIndex].UseType = value;
            }
        }

        double _currentRightUpX = 0;
        public double CurrentRightUpX
        {
            get => _currentRightUpX;
            set
            {
                _currentRightUpX = value;
            }
        }
        double _currentRightUpY = 0;
        public double CurrentRightUpY
        {
            get => _currentRightUpY;
            set
            {
                _currentRightUpY = value;
            }
        }
        double _currentLeftDownX = 0;
        public double CurrentLeftDownX
        {
            get => _currentLeftDownX;
            set
            {
                _currentLeftDownX = value;
            }
        }
        double _currentLeftDownY = 0;
        public double CurrentLeftDownY
        {
            get => _currentLeftDownY;
            set
            {
                _currentLeftDownY = value;
            }
        }
        bool _liveOn = false;
        public bool LiveOn
        {
            get => _liveOn;
            set
            {
                if (_liveOn == value)
                {
                    return;
                }
                _liveOn = value;
                Module.VisionController.Trigger1.SetTimmerTrigger(value, 1000);
            }
        }


        /// <summary>
        /// 從左到右，從上到下
        /// </summary>
        public List<Point2d> MotorPosition = new List<Point2d>();
        public void Init(InspectionModule module)
        {
            SelectVel = MoveVel[2];
            Module = module;
            if (Module.ProductParam.BigProductMapSetting.MapList.Count == 0)
            {
                Module.ProductParam.BigProductMapSetting.MapList.Add(new SingleUnitMap());
            }

            MapIndexList.Clear();
            for (int i = 0; i < Module.ProductParam.BigProductMapSetting.MapList.Count; i++)
            {
                MapIndexList.Add(Module.ProductParam.BigProductMapSetting.MapList[i].MapIndex);
            }
            ProductRightUpX = Module.ProductParam.BigProductMapSetting.ProductRightUp.x;
            ProductRightUpY = Module.ProductParam.BigProductMapSetting.ProductRightUp.y;
            ProductLeftDownX = Module.ProductParam.BigProductMapSetting.ProductLeftDown.x;
            ProductLeftDownY = Module.ProductParam.BigProductMapSetting.ProductLeftDown.y;
            SelectMapIndex = MapIndexList.First();
            SelectType = TypeList.First();
            SelectXYStep = XYStepList.First();
            SelectZStep = ZStepList.First();
            Module.VisionController.Framer.OnGroupAllCaptured += OnRealImageIn;

            BigProductInit();
            OpenLightTool();
            ReadMap();
        }

        public void OnRealImageIn(object sender, StationCaptureArgs e)
        {
            //RealImage?.Dispose();
            //RealImage = e.imgs[0];
            //RealImage.GetImageSize(out HTuple width, out HTuple height);

            //ImageWidth = width.D;//TODO 實際上機打開
            //ImageHeight = height.D;
        }

        public void MoveXBackward()
        {
            double dist = XAxis.ActualPos - Convert.ToDouble(SelectXYStep);
            Velocity vel = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);
            XAxis.AbsoluteMove(dist, vel);
            XAxis.WaitMotionDone();
            this.RaisePropertyChanged(x => x.XPos);
        }
        public void MoveXForward()
        {
            double dist = XAxis.ActualPos + Convert.ToDouble(SelectXYStep);
            Velocity vel = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);
            var a = XAxis.AbsoluteMove(dist, vel);
            XAxis.WaitMotionDone();
            this.RaisePropertyChanged(x => x.XPos);
        }
        public void MoveYBackward()
        {
            double dist = YAxis.ActualPos - Convert.ToDouble(SelectXYStep);
            Velocity vel = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);
            YAxis.AbsoluteMove(dist, vel);
            YAxis.WaitMotionDone();
            this.RaisePropertyChanged(x => x.YPos);
        }
        public void MoveYForward()
        {
            double dist = YAxis.ActualPos + Convert.ToDouble(SelectXYStep);
            Velocity vel = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);
            YAxis.AbsoluteMove(dist, vel);
            YAxis.WaitMotionDone();
            this.RaisePropertyChanged(x => x.YPos);
        }
        public void ZUp()
        {
            double dist = ZAxis.ActualPos + Convert.ToDouble(SelectZStep);
            Velocity vel = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, SelectVel);
            ZAxis.AbsoluteMove(dist, vel);
            ZAxis.WaitMotionDone();
            this.RaisePropertyChanged(x => x.ZPos);
        }
        public void ZDown()
        {
            double dist = ZAxis.ActualPos - Convert.ToDouble(SelectZStep);
            Velocity vel = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, SelectVel);
            ZAxis.AbsoluteMove(dist, vel);
            ZAxis.WaitMotionDone();
            this.RaisePropertyChanged(x => x.ZPos);
        }
        public void CaptureGenMap()
        {
            var velAY1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);
            var velCX1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);

            Mosaic2Service.CurrentFovIndex = 0;
            Mosaic2Service.MosaicEditing = true;

            var framer = Module.VisionController.Framer;
            var capture = Mosaic2Service.GetCapture(0);
            for (int i = 0; i < Mosaic2Service.GridY; i++)
            {
                for (int j = 0; j < Mosaic2Service.GridX; j++)
                {

                    Mosaic2Service.SetSelectPos(j, i);

                    //var distX = BigMapPosition[j* Mosaic2Service.GridY + i].x;
                    //var distY = BigMapPosition[j * Mosaic2Service.GridY + i].y;
                    //Module.BX1_流道橫移軸.AbsoluteMove(distX, velCX1,0);
                    //Module.視覺縱移軸.AbsoluteMove(distY, velAY1,0);
                    //Module.BX1_流道橫移軸.WaitMotionDone(10000);
                    //Module.視覺縱移軸.WaitMotionDone(10000);

                    Mosaic2Service.Capture();


                    for (int takes = 0; takes < capture.Length; takes++)
                    {
                        var takeA = _isAddImageQueue.Take();
                    }
                }
            }
            Mosaic2Service.BuildMosaic();
            var image = Mosaic2Service.GetMosaicImage();

            HOperatorSet.Compose3(image * 2, image, image / 2, out HObject rgbImage);
            HImage hImage = new HImage(rgbImage);
            MapImage?.Dispose();
            MapImage = hImage;
            HWindowControlMap.SetFullImagePart(hImage);
            HWindowControlMap.HalconWindow.DispObj(hImage);
            MapImage.GetImageSize(out int width1, out int height1);
            RightBot = new Point2d(width1, height1);
            LeftTop = new Point2d(0, 0);
        }
        public void AddMapIndex()
        {
            MapIndexList.Add(MapIndexList.Count);
            Module.ProductParam.BigProductMapSetting.MapList.Add(new SingleUnitMap() { MapIndex = Module.ProductParam.BigProductMapSetting.MapList.Count });
        }

        public void DeleteMapIndex()
        {
            if (SelectMapIndex < 0)
                return;
            if (MapIndexList.Count == 1)
            {
                MessageBox.Show("至少要保留一個區域");
                return;
            }
            int removeIdx = SelectMapIndex;
            MapIndexList.RemoveAt(removeIdx);
            Module.ProductParam.BigProductMapSetting.MapList.RemoveAt(removeIdx);
            SelectMapIndex = MapIndexList.First();
        }
        public void SignMap()
        {
            SignMap(null, null, null);
        }

        private void SignMap(double? hoverPixelX, double? hoverPixelY, string hoverText)
        {
            HWindowControlMap.HalconWindow.ClearWindow();
            HWindowControlMap.HalconWindow.DispObj(MapImage);
            for (int i = 0; i < MapIndexList.Count; i++)
            {
                try
                {
                    RealPosToPixel(Module.ProductParam.BigProductMapSetting.MapList[i].PositionRightUp.x, Module.ProductParam.BigProductMapSetting.MapList[i].PositionRightUp.y,
                    out var pixelX, out var pixelY);
                    RealPosToPixel(Module.ProductParam.BigProductMapSetting.MapList[i].PositionLeftDown.x, Module.ProductParam.BigProductMapSetting.MapList[i].PositionLeftDown.y,
                        out var pixelX2, out var pixelY2);

                    HOperatorSet.GenRectangle1(out HObject rectangle,
                        pixelY,
                        pixelX2,
                        pixelY2,
                        pixelX);
                    HWindowControlMap.HalconWindow.SetColor("red");
                    HWindowControlMap.HalconWindow.SetDraw("margin");
                    HWindowControlMap.HalconWindow.DispObj(rectangle);

                    double centerX = (pixelX + pixelX2) / 2;
                    double centerY = (pixelY + pixelY2) / 2;
                    //HOperatorSet.GenCrossContourXld(out HObject tmp, centerX,
                    //    centerY, 30, 0);
                    //HWindowControlMap.HalconWindow.DispObj(tmp);
                    HWindowControlMap.HalconWindow.SetTposition((int)centerY, (int)centerX);
                    HWindowControlMap.HalconWindow.WriteString(i.ToString());
                }
                catch (Exception ee)
                {
                    Console.WriteLine("SignMap error:" + ee.ToString());
                }

            }

            if (hoverPixelX.HasValue && hoverPixelY.HasValue && !string.IsNullOrWhiteSpace(hoverText))
            {
                try
                {
                    HOperatorSet.GenCrossContourXld(out HObject hoverCross, hoverPixelY.Value, hoverPixelX.Value, 20, 0);
                    HWindowControlMap.HalconWindow.SetColor("yellow");
                    HWindowControlMap.HalconWindow.DispObj(hoverCross);
                    HWindowControlMap.HalconWindow.SetColor("red");
                    HWindowControlMap.HalconWindow.SetTposition((int)Math.Max(0, hoverPixelY.Value + 12), (int)Math.Max(0, hoverPixelX.Value + 12));
                    HWindowControlMap.HalconWindow.WriteString(hoverText);
                }
                catch (Exception ee)
                {
                    Console.WriteLine("SignMap hover error:" + ee.ToString());
                }
            }
        }
        public void SaveMap()
        {
            MapImage.GetImageSize(out int width1, out int height1);
            RightBot = new Point2d(width1, height1);
            LeftTop = new Point2d(0, 0);
            zoom(new Point(151, 92), 0.1);
            ProductName = Module.ProductName;
            Module.ProductParam.BigProductMapSetting.OriginPixelImageSize = new Point2d(width1, height1);
            for (int i = 0; i < Module.ProductParam.BigProductMapSetting.MapList.Count; i++)
            {
                RealPosToPixel(Module.ProductParam.BigProductMapSetting.MapList[i].PositionRightUp.x, Module.ProductParam.BigProductMapSetting.MapList[i].PositionRightUp.y,
                    out double pixelX, out double pixelY);
                Module.ProductParam.BigProductMapSetting.MapList[i].PixelRightUp.x = pixelX;
                Module.ProductParam.BigProductMapSetting.MapList[i].PixelRightUp.y = pixelY;
                RealPosToPixel(Module.ProductParam.BigProductMapSetting.MapList[i].PositionLeftDown.x, Module.ProductParam.BigProductMapSetting.MapList[i].PositionLeftDown.y,
                    out double pixelX2, out double pixelY2);
                Module.ProductParam.BigProductMapSetting.MapList[i].PixelLeftDown.x = pixelX2;
                Module.ProductParam.BigProductMapSetting.MapList[i].PixelLeftDown.y = pixelY2;
            }

            if (Directory.Exists($@"D:\Coordinator2.0\Products\{ProductName}\BigMap") == false)
            {
                Directory.CreateDirectory($@"D:\Coordinator2.0\Products\{ProductName}\BigMap");
            }

            MapImage.WriteImage("jpg", 0, $@"D:\Coordinator2.0\Products\{ProductName}\BigMap\OriginProductMap.jpg");
            HWindowControlMap.HalconWindow.DumpWindow("jpg", $@"D:\Coordinator2.0\Products\{ProductName}\BigMap\ProductMap.jpg");
            Module.SaveVisionProductParam(this, Module);
        }

        public void ReadMap()
        {
            ProductName = Module.ProductName;
            if (Directory.Exists($@"D:\Coordinator2.0\Products\{ProductName}\BigMap") == false)
            {
                Directory.CreateDirectory($@"D:\Coordinator2.0\Products\{ProductName}\BigMap");
            }
            MapImage?.Dispose();
            if (!File.Exists($@"D:\Coordinator2.0\Products\{ProductName}\BigMap\OriginProductMap.jpg"))
                return;
            MapImage = new HImage();
            MapImage.ReadImage($@"D:\Coordinator2.0\Products\{ProductName}\BigMap\OriginProductMap.jpg");
            //HWindowControlMap.SetFullImagePart(MapImage);
            //HWindowControlMap.HalconWindow.DispObj(MapImage);
            HWindowControlMap.SetFullImagePart(MapImage);
            SignMap();
            MapImage.GetImageSize(out int width, out int height);
            RightBot = new Point2d(width, height);
            LeftTop = new Point2d(0, 0);
        }
        public void ProductRightUpSet()
        {
            Module.ProductParam.BigProductMapSetting.ProductRightUp = new Point2d(ProductRightUpX, ProductRightUpY);

        }
        public void ProductRightUpMove()
        {
            Velocity XVel = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);
            Velocity YVel = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);
            XAxis.AbsoluteMove(ProductRightUpX, XVel, 0);
            YAxis.AbsoluteMove(ProductRightUpY, YVel, 0);
            XAxis.WaitMotionDone();
            YAxis.WaitMotionDone();
        }
        public void ProductLeftDownSet()
        {
            Module.ProductParam.BigProductMapSetting.ProductLeftDown = new Point2d(ProductLeftDownX, ProductLeftDownY);

        }
        public void ProductLeftDownMove()
        {
            Velocity XVel = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);
            Velocity YVel = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);
            XAxis.AbsoluteMove(ProductLeftDownX, XVel, 0);
            YAxis.AbsoluteMove(ProductLeftDownY, YVel, 0);
            XAxis.WaitMotionDone();
            YAxis.WaitMotionDone();

        }
        public void RightUpSet()
        {
            Module.ProductParam.BigProductMapSetting.MapList[SelectMapIndex].PositionRightUp = new Point2d(CurrentRightUpX, CurrentRightUpY);
        }
        public void RightUpMove()
        {
            Velocity XVel = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);
            Velocity YVel = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);
            XAxis.AbsoluteMove(CurrentRightUpX, XVel, 0);
            YAxis.AbsoluteMove(CurrentRightUpY, YVel, 0);
            XAxis.WaitMotionDone();
            YAxis.WaitMotionDone();
        }
        public void LeftDownSet()
        {
            Module.ProductParam.BigProductMapSetting.MapList[SelectMapIndex].PositionLeftDown = new Point2d(CurrentLeftDownX, CurrentLeftDownY);
        }
        public void LeftDownMove()
        {
            Velocity XVel = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);
            Velocity YVel = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);
            XAxis.AbsoluteMove(CurrentLeftDownX, XVel, 0);
            YAxis.AbsoluteMove(CurrentLeftDownY, YVel, 0);
            XAxis.WaitMotionDone();
            YAxis.WaitMotionDone();
        }

        List<RoundSetting2> TempProductSetting;
        public void BigProductInit()
        {
            //TODO 要先計算拍攝位置
            //產品大小、FOV、Overlap求得組幾張圖
            //求實際拍照中心點


            MosaicForm mosaicForm = new MosaicForm(Module.VisionController);

            mosaicForm.FormModel.SelectedMosaicParam.CalibrationControlParam.MovePitch.x = PitchX;
            mosaicForm.FormModel.SelectedMosaicParam.CalibrationControlParam.MovePitch.y = PitchY;

            mosaicForm.SetProductPosition(Module.ProductName, new Point2d(Module.CurrentTrayCarrier.Tray.IcLayout[0, 0].ContainerSize.X, Module.CurrentTrayCarrier.Tray.IcLayout[0, 0].ContainerSize.Y),
                new Point2d[] { new Point2d(Module.MotorOffset.Start_AY1, Module.MotorOffset.Start_AY1) }, //TODO 吸嘴中心是產品中心才這樣寫，如果不是的話，要修改，自己找產品中心在哪(是影像中，不是boat盤上)
                true, true, new Point2d(1, 1), out Point2d[][] pos);
            GetMosaicXYCount(pos[0].ToList(), out var mosaicXYCount);
            int gridX = (int)mosaicXYCount.x;
            int gridY = (int)mosaicXYCount.y;
            BigMapPosition.Clear();
            BigMapPosition.AddRange(pos[0]);
            //產品大小、實際產品中心



            TempProductSetting = Module.VisionController.ProductSetting.RoundSettings2;
            List<RoundSetting2> roundSetting2 = new List<RoundSetting2>();
            roundSetting2.Add(new RoundSetting2());
            roundSetting2[0].Groups.Add(new CaptureGroup() { Name = "UnName", CapturePosition = new string[1] { "CustomVisionFov" } });
            roundSetting2[0].Groups[0].Captures.Add(new RoundSetting2.Capture(Module.VisionController.Lighter.Channel)
            {
                Name = "New capture",
            });
            Module.VisionController.ProductSetting.RoundSettings2 = roundSetting2;

            CreateCaptureTree();
            Mosaic2Service = new MosaicViewModel2(Module.VisionController, Module.VisionController.ProductSetting, CaptureData);
            Mosaic2Service.NotifyImageAdd += AddImageQueue;


            int[] motorX = { 200, 200, 200, 250, 250, 250, 300, 300, 300 };//測試用
            int[] motorY = { 580, 630, 680, 580, 630, 680, 580, 630, 680 };//測試用
            gridX = 3;
            gridY = 3;

            //int[] motorX = { 50, 50, 50, 50, 50, 50, 50, 100, 100, 100, 100, 100, 100, 100, 150, 150, 150, 150, 150, 150, 150, 200, 200, 200, 200, 200, 200, 200,
            //    250, 250, 250,250, 250,250, 250, 300, 300, 300,300, 300,300, 300,350,350,350,350,350,350,350 };//測試用
            //int[] motorY = { 430,480,530,580, 630, 680, 730, 430, 480, 530, 580, 630, 680, 730, 430, 480, 530, 580, 630, 680, 730, 430, 480, 530, 580, 630, 680, 730,
            //430,480,530,580, 630, 680, 730,430,480,530,580, 630, 680, 730,430,480,530,580, 630, 680, 730};//測試用
            //int gridX = 7;
            //int gridY = 7;

            Mosaic2Service.SetMosaicGrid(gridX, gridY);
            Mosaic2Service.FovCount = 1;
            //var inspect = BoatCarrier.InspectData.InspectionPostion;
            for (int f = 0; f < 1; f++)
            {
                Mosaic2Service.MosaicEditing = true;
                Mosaic2Service.CurrentFovIndex = f;
                for (int i = 0; i < Mosaic2Service.GridY; i++)
                {
                    for (int j = 0; j < Mosaic2Service.GridX; j++)
                    {
                        if (true)
                        {
                            //測試用
                            Mosaic2Service.SetMosaicPos(j, i, motorX[j * 3 + i], motorY[j * 3 + i]);
                        }
                        else
                        {
                            //實際上機用
                            Mosaic2Service.SetMosaicPos(j, i, pos[0][j * Mosaic2Service.GridY + i].x, pos[0][j * Mosaic2Service.GridY + i].y);
                        }
                    }
                }
            }
            Module.VisionController.ProductSetting.MosaicSettings.UsingMosaicMethod = HTA.Utility.Calibration.MosaicMethodEm.SimpleMosaic;
            Mosaic2Service.CalcMosaicMap();
            mosaicForm?.Dispose();
        }

        public void GetMosaicXYCount(List<Point2d> mosaicPos, out Point2d mosaicXYCount)
        {
            mosaicXYCount = new Point2d(0, 0);
            if (mosaicPos == null || mosaicPos.Count == 0)
            {
                return;
            }

            int xIndex = 1;
            const double epsilon = 0.000001;
            for (int i = 0; i < mosaicPos.Count - 1; i++)
            {
                if (Math.Abs(mosaicPos[i].x - mosaicPos[i + 1].x) < epsilon)
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

        private bool TryGetMapImageSize(out double width, out double height)
        {
            width = 0;
            height = 0;

            var originSize = Module?.ProductParam?.BigProductMapSetting?.OriginPixelImageSize ?? new Point2d(0, 0);
            if (originSize.x > 0 && originSize.y > 0)
            {
                width = originSize.x;
                height = originSize.y;
                return true;
            }

            if (MapImage == null)
            {
                return false;
            }

            MapImage.GetImageSize(out int imageWidth, out int imageHeight);
            if (imageWidth <= 0 || imageHeight <= 0)
            {
                return false;
            }

            width = imageWidth;
            height = imageHeight;
            return true;
        }

        private bool TryGetBigMapBounds(out double minX, out double maxX, out double minY, out double maxY)
        {
            minX = maxX = minY = maxY = 0;

            IEnumerable<Point2d> positions = BigMapPosition;
            if (positions == null || !positions.Any())
            {
                positions = Module?.ProductParam?.BigProductMapSetting?.MapList?
                    .SelectMany(x => new[] { x.PositionRightUp, x.PositionLeftDown });
            }

            if (positions == null || !positions.Any())
            {
                return false;
            }

            minX = positions.Min(p => p.x);
            maxX = positions.Max(p => p.x);
            minY = positions.Min(p => p.y);
            maxY = positions.Max(p => p.y);

            return maxX > minX && maxY > minY;
        }

        private void CreateCaptureTree()
        {
            CaptureData = new BindingList<CaptureTreeView>();
            foreach (var (round, roundIdx) in Module.VisionController.ProductSetting.RoundSettings2.Select((x, idx) => (x, idx)))
            {
                var root = new CaptureTreeView(round, roundIdx);

                CaptureData.Add(root);

                //展開group
                foreach (var group in round.Groups)
                {
                    var groupNode = new CaptureTreeView(group)
                    {
                        ParentHandle = root.Id
                    };

                    CaptureData.Add(groupNode);

                    //展開capture
                    foreach (var capture in group.Captures)
                    {
                        var captureNode = new CaptureTreeView(capture)
                        {
                            ParentHandle = groupNode.Id
                        };

                        CaptureData.Add(captureNode);
                    }
                }


            }

            CaptureData.ListChanged += (ss, ee) =>
            {
                if (ee.ListChangedType == ListChangedType.ItemAdded)
                {
                    var data = CaptureData[ee.NewIndex];

                    if (data == null) return;
                    if (data.AType == CaptureTreeView.CaptureType.Capture)
                    {
                        if (data.DataHandle is RoundSetting2.CaptureGroup groupInstance)
                        {

                        }
                    }
                    else if (data.AType == CaptureTreeView.CaptureType.Group)
                    {
                        //從view中找到round根group
                        var round = CaptureData.First(x => x.Id == data.ParentHandle);

                        //找到使用的像機與相機idx
                        var framerSourceIndex = round.RoundSetting.CameraSource;
                        var framerSource = Module.VisionController.GetHardware().Framer[framerSourceIndex];

                        if (data.DataHandle is RoundSetting2.CaptureGroup groupInstance)
                        {
                            groupInstance.CameraAxisPosition = new double[framerSource.Count];
                            var cameraInfoService = FlowTools.GetService<List<RoundCameraSetting[]>>(null);
                            groupInstance.CapturePosition =
                                cameraInfoService[framerSourceIndex].Select(x => x.CameraPosition).ToArray();
                        }
                    }
                    else if (data.AType == CaptureTreeView.CaptureType.Round)
                    {

                    }
                }

            };


            var firstCapture = CaptureData.FirstOrDefault(x => x.AType == CaptureTreeView.CaptureType.Capture);
            if (firstCapture != null)
            {
                CurrentCapture = firstCapture.DataHandle;
            }

            var firstCaptureGroup = CaptureData.Where(x => x.AType == CaptureTreeView.CaptureType.Capture).ToArray();
            if (Module.VisionController.GetHardware().Framer[0].Grabbers[0] is IStructured3dFramer structuredFrame && firstCaptureGroup.Length > 1) //結構光相機第一張固定是3D不打光，如果有第二張就用第二張打光
                CurrentCapture = firstCaptureGroup[1].DataHandle;

        }

        public void OnFlowSettingDoEvent(object sender, EventArgs e)
        {
            IsFlowSettingDoneEvent = true;
        }

        private void AddImageQueue(object sender, List<HTA.Utility.Structure.CustomImage> e)
        {
            IsAddImage = true;
            _isAddImageQueue.Add(IsAddImage);
        }

        private void OpenLightTool()
        {
            if (Module.VisionController.ProductSetting.RoundSettings2.Count == 0)
            {
                Module.VisionController.ProductSetting.RoundSettings2.Add(new RoundSetting2());
                Module.VisionController.ProductSetting.RoundSettings2[0].Groups.Add(new CaptureGroup());
                Module.VisionController.ProductSetting.RoundSettings2[0].Groups[0].Captures.Add(new Capture());
            }

            groupInfo = Module.VisionController.ProductSetting.RoundSettings2[0].Groups[0].Captures[0].Light.LightingPersentage.ToArray();
            Module.VisionController.Lighter.SetLight(groupInfo.ToArray());

            var lightForm = new LightForm(Module.VisionController.Lighter);
            lightForm.TopLevel = false;
            lightForm.Parent = LightPanel;
            lightForm.FormBorderStyle = FormBorderStyle.None;
            lightForm.Dock = DockStyle.Fill;
            lightForm.SetToDevice = true;
            lightForm.OnLightValueChange += (obj, args) =>
            {
                for (int i = 0; i < args.Length; i++)
                {
                    groupInfo[i] = args[i];
                    Module.VisionController.ProductSetting.RoundSettings2[0].Groups[0].Captures[0].Light.LightingPersentage[i] = groupInfo[i];
                }
            };
            LightPanel.Controls.Add(lightForm);

            if (LightPanel.Controls.Count >= 2)
            {
                LightPanel.Controls.RemoveAt(0);
            }

            lightForm.ApplyCurrentSetting();
            lightForm.Show();
        }

        public void ClickOpenZoomMap()
        {
            OpenZoomMap = !OpenZoomMap;
            if (!OpenZoomMap)
            {
                MapImage.GetImageSize(out int width1, out int height1);
                RightBot = new Point2d(width1, height1);
                LeftTop = new Point2d(0, 0);
                zoom(new Point(151, 92), 0.1);
            }
            this.RaisePropertyChanged(x => x.BtnZoomMapContent);
        }
        public void ClickOpenMoveMap()
        {
            OpenMoveMap = !OpenMoveMap;
            this.RaisePropertyChanged(x => x.BtnMoveMapContent);
        }

        public void ImgWndMap_MouseUp(object sender, MouseEventArgs e)
        {
            //停止滑鼠作動  
            //if (e.Button == MouseButtons.Left)
            //{
            //    int[] pts = new int[4];
            //    HWindowControlMap.HalconWindow.GetPart(out pts[0], out pts[1], out pts[2], out pts[3]);
            //    LeftTop.y = pts[0];
            //    LeftTop.x = pts[1];
            //    RightBot.y = pts[2];
            //    RightBot.x = pts[3];
            //}
            IsDrag = true;
        }

        public void ImgWndMap_HMouseWheel(object sender, HMouseEventArgs e)
        {
            if (!OpenZoomMap)
                return;
            Point relativePoint = HWindowControlMap.PointToClient(System.Windows.Forms.Cursor.Position);

            HMouseWheel(relativePoint, e);

        }

        public void HMouseWheel(Point relativePoint, HMouseEventArgs e)
        {
            if (e.Delta >= 0)
            {
                zoom(relativePoint, -0.1);
            }
            else
            {
                zoom(relativePoint, 0.1);
            }
        }

        private void zoom(Point center, double factor)
        {
            MapImage.GetImageSize(out int width1, out int height1);
            Point ImgSize = new Point(width1, height1);
            double width = RightBot.x - LeftTop.x;
            double height = RightBot.y - LeftTop.y;
            double centerX = (RightBot.x + LeftTop.x) / 2;
            double centerY = (RightBot.y + LeftTop.y) / 2;
            double currentScale = width / ImgSize.X;
            double newScaleX = Math.Max(Math.Min(currentScale + factor, 1.0), 0.03);
            double newScaleY = Math.Max(Math.Min(currentScale * 1 + factor, 1.0), 0.03);


            width = ImgSize.X * newScaleX * 0.5;
            height = ImgSize.Y * newScaleY * 0.5;
            LeftTop.x = Math.Max((int)(centerX - width), 0);
            LeftTop.y = Math.Max((int)(centerY - height), 0);
            RightBot.x = Math.Min((int)(centerX + width), ImgSize.X - 1);
            RightBot.y = Math.Min((int)(centerY + height), ImgSize.Y - 1);
            HWindowControlMap.HalconWindow.SetPart(Math.Max(0, (int)LeftTop.y),
                Math.Max(0, (int)LeftTop.x),
                Math.Min(ImgSize.Y - 1, (int)RightBot.y),
                Math.Min(ImgSize.X - 1, (int)RightBot.x));


            SignMap();
            //HWindowControlMap.HalconWindow.DispObj(MapImage);
        }

        public void ImgWndMap_HMouseMove(object sender, HMouseEventArgs e)
        {
            if (MapImage == null)
                return;

            PixelToRealPos(e.X, e.Y, out double realPosX, out double realPosY);
            MapMouseInfo = $"Pixel({e.X:F0}, {e.Y:F0})  Real({realPosX:F3}, {realPosY:F3})";

            if (IsDrag)
            {
                UpdateInfoBaseOnMouse(e);
                //HWindowControlMap.HalconWindow.DispObj(MapImage);
                SignMap(e.X, e.Y, MapMouseInfo);
                int[] pts = new int[4];
                HWindowControlMap.HalconWindow.GetPart(out pts[0], out pts[1], out pts[2], out pts[3]);
                LeftTop.y = pts[0];
                LeftTop.x = pts[1];
                RightBot.y = pts[2];
                RightBot.x = pts[3];
            }
            else
            {
                SignMap(e.X, e.Y, MapMouseInfo);
            }
            IsDrag = false;

        }
        public void UpdateInfoBaseOnMouse(HMouseEventArgs e)
        {
            MapImage.GetImageSize(out int width1, out int height1);
            Point ImgSize = new Point(width1, height1);
            double xDiff = -e.X + DragPoint.X;
            double yDiff = -e.Y + DragPoint.Y;

            int[] pts = new int[4];
            HWindowControlMap.HalconWindow.GetPart(out pts[0], out pts[1], out pts[2], out pts[3]);
            LeftTop.y = pts[0];
            LeftTop.x = pts[1];
            RightBot.y = pts[2];
            RightBot.x = pts[3];

            double r1 = LeftTop.y;
            double c1 = LeftTop.x;
            double r2 = RightBot.y;
            double c2 = RightBot.x;
            double focusWidth = c2 - c1;
            double focusHeight = r2 - r1;
            double currentScaleX = HWindowControlMap.Width / focusWidth;
            double currentScaleY = HWindowControlMap.Height / focusHeight;


            {
                r1 = r1 + yDiff / currentScaleY;
                c1 = c1 + xDiff / currentScaleX;
                if (r1 < 0)
                {
                    r1 = 0;
                }

                if (c1 < 0)
                {
                    c1 = 0;
                }

                r2 = r1 + focusHeight;
                c2 = c1 + focusWidth;
                if (r2 >= ImgSize.Y)
                {
                    r2 = ImgSize.Y - 1;
                    r1 = r2 - focusHeight;
                }
                if (c2 >= ImgSize.X)
                {
                    c2 = ImgSize.X - 1;
                    c1 = c2 - focusWidth;
                }
            }

            HWindowControlMap.HalconWindow.SetPart((int)r1, (int)c1, (int)r2, (int)c2);
        }

        public void ImgWndMap_HMouseDown(object sender, HMouseEventArgs e)
        {
            DragPoint.X = (int)e.X;
            DragPoint.Y = (int)e.Y;
            IsDrag = false;
        }

        public void ImgWndReal_HMosueUp(object sender, HMouseEventArgs e)
        {
            IsDrag = true;
            double eHX = 0, eHY = 0;
            double nowX_pos, nowY_pos;
            double X_movedist = 0, Y_movedist = 0;
            var pixeltomm = Module.VisionController.GetHardware().CalibrationLists[0].Pix2MMAvr; //TODO 實際上機台測試
            double pixelxtomm = 0.009, pixelytomm = 0.009;//校正完後的pixel2mm填過來
            bool axisXmoveOutRange = false, axisYmoveOutRange = false;
            double outOfDistX = 0.0, outOfDistY = 0.0;

            bool positiveLimit = false; //正極限
            bool negativeLimit = false; //負極限

            eHX = e.X;
            eHY = e.Y;


            X_movedist = XAxis.ActualPos + (ImageWidth / 2 - eHX) * pixelxtomm;
            Y_movedist = YAxis.ActualPos + (ImageHeight / 2 - eHY) * pixelytomm;

            Velocity XVel = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, SelectVel);
            Velocity YVel = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, SelectVel);

            XAxis.AbsoluteMove(X_movedist, XVel);
            YAxis.AbsoluteMove(Y_movedist, YVel);

            XAxis.WaitMotionDone();
            YAxis.WaitMotionDone();
        }

        public void ImgWndMap_HMouseUp(object sender, HMouseEventArgs e)
        {
            if (OpenMoveMap)
                return;
            PixelToRealPos(e.X, e.Y, out double realPosX, out double realPosY);
            //RealPosToPixel(realPosX, realPosY, out var a, out var b);
        }
        public void PixelToRealPos(double pixelX, double pixelY, out double realPosX, out double realPosY)
        {
            try
            {
                MapImage.GetImageSize(out int width1, out int height1);
                GetMosaicXYCount(BigMapPosition, out var mosaicXYCount);
                int mosaicXCount = (int)mosaicXYCount.x;
                int mosaicYCount = (int)mosaicXYCount.y;

                mosaicXCount = 3;//測試用
                mosaicYCount = 3;//測試用




                //最左邊 200-80 = 120,最右邊300+80=380,   y=ax+b,x=像素,y=實際, 120 = a*0+b, 380 = a*width1+b，解方程

                // 已知點 (x1, y1), (x2, y2)
                double x1 = 0;//影像最左邊位置
                              //double y1 = BigMapPosition[0].x - 60/2;//(最左邊影像的中心位置-FOVx/2)  TODO 實際測試打開
                double y1 = 120;

                double x2 = width1;//影像最右邊位置
                double y2 = 380;
                //var xRight = BigMapPosition.Select(q => q.x).ToList().Max();
                //double y2 = xRight + 60/2;//(最右邊影像的中心位置+FOVx/2)  TODO 實際測試打開，如何知道第幾個是x的最右邊

                // 解線性方程 y = a*x + b
                double a = (y2 - y1) / (x2);
                double b = y1;

                realPosX = a * pixelX + b;

                //最左邊 580-60 = 520,最右邊680+60=740,   y=ax+b,y=像素,x=實際, 0 = a*520+b, height1 = a*740+b，解方程

                x1 = 0;//影像最上邊位置
                y1 = 520;
                //y1 = BigMapPosition[0].y - 60/2;//(最上邊影像的中心位置-FOVy/2)  TODO 實際測試打開

                x2 = height1;//影像最下邊位置
                y2 = 740;
                //var yBottom = BigMapPosition.Select(q => q.y).ToList().Max();
                //y2 = yBottom + 60/2;//(最下邊影像的中心位置+FOVy/2)  TODO 實際測試打開

                // 解線性方程 y = a*x + b
                a = (y2 - y1) / (x2);
                b = y1;

                realPosY = a * pixelY + b;
            }
            catch (Exception e)
            {
                realPosX = 0;
                realPosY = 0;
            }
        }
        public void RealPosToPixel(double realPosX, double realPosY, out double pixelX, out double pixelY)
        {
            try
            {


                MapImage.GetImageSize(out int width1, out int height1);
                GetMosaicXYCount(BigMapPosition, out var mosaicXYCount);
                int mosaicXCount = (int)mosaicXYCount.x;
                int mosaicYCount = (int)mosaicXYCount.y;
                mosaicXCount = 3;//測試用
                mosaicYCount = 3;//測試用




                //最左邊 200-80 = 120,最右邊300+80=380,   y=ax+b,y=像素,x=實際, 0 = a*120+b, width1 = a*380+b，解方程

                // 已知點 (x1, y1), (x2, y2)
                double x1 = 0;//影像最左邊位置
                              //double y1 = BigMapPosition[0].x - Module.CurrentTrayCarrier.Tray.IcLayout[0, 0].ContainerSize.X/2;//(最左邊的位置-產品大小x/2)  TODO 實際測試打開
                double y1 = 120;

                double x2 = width1;//影像最右邊位置
                double y2 = 380;
                //var xRight = BigMapPosition.Select(q => q.x).ToList().Max();
                //double y2 = xRight + Module.CurrentTrayCarrier.Tray.IcLayout[0, 0].ContainerSize.X/2;//(最右邊的位置+產品大小x/2)  TODO 實際測試打開，如何知道第幾個是x的最右邊

                // 解線性方程 y = a*x + b
                double a = (y2 - y1) / (x2);
                double b = y1;

                pixelX = (realPosX - b) / a;

                //最左邊 580-60 = 520,最右邊680+60=740,   y=ax+b,y=像素,x=實際, 0 = a*520+b, height1 = a*740+b，解方程

                x1 = 0;//影像最上邊位置
                y1 = 520;
                //y1 = BigMapPosition[0].y - Module.CurrentTrayCarrier.Tray.IcLayout[0, 0].ContainerSize.Y/2;//(最上邊的位置-產品大小y/2)  TODO 實際測試打開

                x2 = height1;//影像最下邊位置
                y2 = 740;
                //var yBottom = BigMapPosition.Select(q => q.y).ToList().Max();
                //y2 = yBottom + Module.CurrentTrayCarrier.Tray.IcLayout[0, 0].ContainerSize.Y/2;//(最下邊的位置+產品大小y/2)  TODO 實際測試打開

                // 解線性方程 y = a*x + b
                a = (y2 - y1) / (x2);
                b = y1;

                pixelY = (realPosY - b) / a;
            }
            catch (Exception w)
            {
                pixelX = 0; 
                pixelY= 0;
            }
        }


        public void monPixelToRealPos(double pixelX, double pixelY, out double realPosX, out double realPosY)
        {
            realPosX = 0;
            realPosY = 0;

            if (!TryGetMapImageSize(out var width1, out var height1))
            {
                return;
            }

            if (!TryGetBigMapBounds(out var minRealX, out var maxRealX, out var minRealY, out var maxRealY))
            {
                //return;
            }

            double realRangeX = maxRealX - minRealX;
            double realRangeY = maxRealY - minRealY;
            if (realRangeX < 0 || realRangeY < 0 || width1 <= 1 || height1 <= 1)
            {
                realRangeX = 1;
                realRangeY = 1;
                return;
            }

            double pixelSpanX = width1 - 1;
            double pixelSpanY = height1 - 1;
            pixelX = Math.Max(0, Math.Min(pixelX, pixelSpanX));
            pixelY = Math.Max(0, Math.Min(pixelY, pixelSpanY));

            realPosX = minRealX + (pixelX / pixelSpanX) * realRangeX;
            realPosY = minRealY + (pixelY / pixelSpanY) * realRangeY;
        }

        public void monRealPosToPixel(double realPosX, double realPosY, out double pixelX, out double pixelY)
        {
            pixelX = 0;
            pixelY = 0;

            if (!TryGetMapImageSize(out var width1, out var height1))
            {
                return;
            }

            if (!TryGetBigMapBounds(out var minRealX, out var maxRealX, out var minRealY, out var maxRealY))
            {
                return;
            }

            double realRangeX = maxRealX - minRealX;
            double realRangeY = maxRealY - minRealY;
            if (realRangeX <= 0 || realRangeY <= 0)
            {
                return;
            }

            pixelX = (realPosX - minRealX) / realRangeX * width1;
            pixelY = (realPosY - minRealY) / realRangeY * height1;
        }

        public void OpenAdjustMapRegion()
        {
            AdjustMapRegion adjustMapRegion = new AdjustMapRegion(this);
            adjustMapRegion.ShowDialog();
        }

        public void Dispose()
        {
            Mosaic2Service?.Dispose();
            MapImage?.Dispose();
            Module.VisionController.ProductSetting.RoundSettings2 = TempProductSetting;
            Module.VisionController.Framer.OnGroupAllCaptured -= OnRealImageIn;
        }
    }
}
