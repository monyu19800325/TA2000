using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTA.LightServer;
using System.Windows.Forms;
using HTA.MainController;
using TA2000Modules;
using HalconDotNet;
using HTA.Utility.Structure;
using HTA.IFramer;
using DevExpress.Mvvm.POCO;
//using System.IO.Packaging;
using static DevExpress.Utils.Svg.CommonSvgImages;
using System.IO;
using HTA.MotionBase.Utility;
using System.Threading;
using HTA.Utility.Calibration;
using System.ComponentModel;

namespace TA2000Modules
{
    public class LaserTeachControlViewModel
    {
        bool _liveOn = false;
        LightForm _lightForm;
        private LaserTeachControl form;
        public HWindowControl WindowControl;
        public MainController MainController;
        public InspectionModule InspectionModule;
        public DataGridView DataGridView;

        public CustomImage Image = null;
        public HImage ReduceImage;
        public double CircleRow;
        public double CircleColumn;
        public double CircleRadius
        {
            get => InspectionModule.ProductParam.CircleRadius;
            set
            {
                InspectionModule.ProductParam.CircleRadius = value;
            }
        }
        /// <summary>
        /// NCC搜尋到的圓的中心與半徑
        /// </summary>
        public HTuple CircleRow_List;
        public HTuple CircleCol_List;
        public HTuple CircleRadius_List;

        double Machine_X = 0;
        double Machine_Y = 0;

        HRegion _detection_Region;
       
        Queue<bool> _imageQueue = new Queue<bool>();

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
                MainController.Trigger1.SetTimmerTrigger(value, 1000);
            }
        }


        public int ThresholdMax
        {
            get => InspectionModule.ProductParam.ThresholdMax;
            set
            {
                if (value < ThresholdMin)
                {
                    InspectionModule.ProductParam.ThresholdMax = ThresholdMin + 1;
                }
                InspectionModule.ProductParam.ThresholdMax = value;
                this.RaisePropertyChanged(x => x.ThresholdMax);

                var objCount = Image.CountObj();
                if (objCount == 0)
                {
                    return;
                }
                HOperatorSet.GenEmptyObj(out HObject AllCircles);

                HOperatorSet.GenCircle(out HObject Circles, CircleRow_List, CircleCol_List, CircleRadius_List);
                HOperatorSet.Union1(Circles, out AllCircles);
                Circles.Dispose();

                HRegion AllCircles_region = new HRegion(AllCircles);
                ReduceImage = Image.ReduceDomain(AllCircles_region);

                if (_detection_Region != null)
                {
                    _detection_Region.Dispose();
                    _detection_Region = null;
                }

                _detection_Region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
                AllCircles.Dispose(); AllCircles = null;
                AllCircles_region.Dispose(); AllCircles_region = null;
                ReduceImage.Dispose(); ReduceImage = null;

                WindowControl.HalconWindow.ClearWindow();
                WindowControl.HalconWindow.DispObj(Image);
                WindowControl.HalconWindow.SetColor("red");
                WindowControl.HalconWindow.SetDraw("fill");
                WindowControl.HalconWindow.DispObj(_detection_Region);

                this.RaisePropertyChanged(x => x.ThresholdMax);
            }
        }

        public int ThresholdMin
        {
            get => InspectionModule.ProductParam.ThresholdMin;
            set
            {
                if (value > InspectionModule.ProductParam.ThresholdMin)
                {
                    InspectionModule.ProductParam.ThresholdMin = ThresholdMax - 1;
                }
                InspectionModule.ProductParam.ThresholdMin = value;
                this.RaisePropertyChanged(x => x.ThresholdMin);

                var objCount = Image.CountObj();
                if (objCount == 0)
                {
                    return;
                }

                HOperatorSet.GenEmptyObj(out HObject AllCircles);

                HOperatorSet.GenCircle(out HObject Circles, CircleRow_List, CircleCol_List, CircleRadius_List);
                HOperatorSet.Union1(Circles, out AllCircles);
                Circles.Dispose();

                HRegion AllCircles_region = new HRegion(AllCircles);
                ReduceImage = Image.ReduceDomain(AllCircles_region);

                if(_detection_Region!=null)
                {
                    _detection_Region.Dispose();
                    _detection_Region = null;
                }

                _detection_Region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
                AllCircles.Dispose(); AllCircles = null;
                AllCircles_region.Dispose(); AllCircles_region = null;
                ReduceImage.Dispose(); ReduceImage = null;

                WindowControl.HalconWindow.ClearWindow();
                WindowControl.HalconWindow.DispObj(Image);
                WindowControl.HalconWindow.SetColor("red");
                WindowControl.HalconWindow.SetDraw("fill");
                WindowControl.HalconWindow.DispObj(_detection_Region);

                this.RaisePropertyChanged(x => x.ThresholdMin);
            }
        }


        public int SearchScore
        {
            get => InspectionModule.ProductParam.SearchScore;
            set
            {
                InspectionModule.ProductParam.SearchScore = value;
                this.RaisePropertyChanged(x => x.SearchScore);
            }
        }
       

        public int HeightMax
        {
            get => InspectionModule.ProductParam.HeightMax;
            set
            {
                if (value < HeightMin)
                    HeightMax = HeightMin + 1;

                InspectionModule.ProductParam.HeightMax = value;
                this.RaisePropertyChanged(x => x.HeightMax);
            }
        }
        public int HeightMin
        {
            get => InspectionModule.ProductParam.HeightMin;
            set
            {
                if (value > HeightMax)
                    HeightMin = HeightMax - 1;

                InspectionModule.ProductParam.HeightMin = value;
                this.RaisePropertyChanged(x => x.HeightMin);
            }
        }
     
        public BindingList<LaserResult> _laserResult { get; } = new BindingList<LaserResult>(); //真正的資料集合：會通知 DataGridView 更新
        public BindingSource LaserResultsSource { get; } = new BindingSource();//建議再包一層 BindingSource（排序/ResetBindings 方便）

        public void Init(InspectionModule module)
        {
            InspectionModule = module;
            MainController = (MainController)module.VisionController;
            MainController.Framer.OnGroupAllCaptured += OnImageIn;

            Machine_X = InspectionModule.視覺縱移軸.ActualPos;
            Machine_Y = InspectionModule.BX1_流道橫移軸.ActualPos;

            if (File.Exists($@"D:\Coordinator2.0\Products\{InspectionModule.ProductName}\LaserData\LaserBlockModel.ncm"))
                HOperatorSet.ReadNccModel($@"D:\Coordinator2.0\Products\{InspectionModule.ProductName}\LaserData\LaserBlockModel.ncm", out module.TargetModel);

            LaserResultsSource.DataSource = _laserResult;         
        }


        public void SetLight()
        {
            if (_lightForm == null)
            {
                if (InspectionModule.ProductParam.Lights.ToArray().Length == 0)
                {
                    InspectionModule.ProductParam.Lights = new List<double>();
                    for (int index = 0; index < MainController.Lighter.Channel; index++)
                    {
                        if (index == 0)
                            InspectionModule.ProductParam.Lights.Add(50);
                        else
                            InspectionModule.ProductParam.Lights.Add(0);
                    }
              
                }

                InspectionModule.VisionController.Lighter.SetLight(InspectionModule.ProductParam.Lights.ToArray());

                _lightForm = new LightForm(MainController.Lighter, InspectionModule.ProductParam.Lights.ToArray());
                _lightForm.StartPosition = FormStartPosition.Manual;
                _lightForm.Left = form.Right;
                _lightForm.Top = form.Top;
                if (form.MdiParent != null)
                    _lightForm.MdiParent = form.MdiParent;
                _lightForm.OnLightValueChange += (ss, ee) =>
                {
                    InspectionModule.ProductParam.Lights = ee.ToList();
                    MainController.Lighter.SetLight(ee);
                };
                _lightForm.Closed += (ss, ee) =>
                {
                    _lightForm = null;
                };
            }
            _lightForm.Show();      
        }

        public void SetView(LaserTeachControl laserTeachControl)
        {
            form = laserTeachControl;
        }

        public void SetROI()
        {
            WindowControl.HalconWindow.SetColor("red");
            WindowControl.HalconWindow.SetDraw("margin");
            //WindowControl.HalconWindow.DrawCircle(out double row, out double column, out double radius);
            WindowControl.HalconWindow.DrawRectangle1(out double row_Top, out double col_Left, out double row_down, out double col_Right);
            //CircleRow = row;
            //CircleColumn = column;
            //CircleRadius = radius;

            WindowControl.HalconWindow.ClearWindow();
            WindowControl.HalconWindow.DispObj(Image);
            //WindowControl.HalconWindow.DispCircle(row, column, radius);
            WindowControl.HalconWindow.DispRectangle1(row_Top, col_Left, row_down, col_Right);
        }

        
        public void SetTarget()
        {
            WindowControl.HalconWindow.SetColor("red");
            WindowControl.HalconWindow.SetDraw("margin");
            WindowControl.HalconWindow.DrawCircle(out double row, out double column, out double radius);
            CircleRow = row;
            CircleColumn = column;
            CircleRadius = radius;
            InspectionModule.ProductParam.CircleRadius = radius;

            WindowControl.HalconWindow.ClearWindow(); 
            WindowControl.HalconWindow.DispObj(Image);
            WindowControl.HalconWindow.DispCircle(row, column, radius);

            Circle2d circle = new Circle2d(row, column, radius);
            var region = circle.GenRegion();
            ReduceImage = Image.ReduceDomain(region);
            var cropImage = ReduceImage.CropDomain();

            if (InspectionModule.TargetModel != null)
            {
                InspectionModule.TargetModel.Dispose(); 
                InspectionModule.TargetModel = null;
            }
            HOperatorSet.CreateNccModel(cropImage, 4, -0.1, 0.1, 0.01, "use_polarity", out InspectionModule.TargetModel);
        }

        public void SearchTarget()
        {
            if (InspectionModule.TargetModel != null)
            {
                //Search
                HOperatorSet.FindNccModel(Image, InspectionModule.TargetModel, -0.01, 0.02, ((double)SearchScore / 100f), 30, 0.5, "true", 3, out HTuple row, out HTuple col, out HTuple angle, out HTuple score);

                //Paint
                WindowControl.HalconWindow.ClearWindow();
                WindowControl.HalconWindow.DispObj(Image);

                if (CircleRadius_List != null) CircleRadius_List.Dispose(); CircleRadius_List = null;
                CircleRadius_List = new HTuple();
                for (int index = 0; index < row.Length; index++)
                {
                    HOperatorSet.TupleConcat(CircleRadius_List, CircleRadius, out CircleRadius_List);
                }
                WindowControl.HalconWindow.DispCircle(row, col, CircleRadius_List);

                CircleRow_List = row;
                CircleCol_List = col;

                //儲存Region
                HOperatorSet.GenEmptyObj(out HObject AllCircles);
                HOperatorSet.GenCircle(out HObject Circles, CircleRow_List, CircleCol_List, CircleRadius_List);
                HOperatorSet.Union1(Circles, out AllCircles);
                Circles.Dispose();

                HRegion AllCircles_region = new HRegion(AllCircles);
                ReduceImage = Image.ReduceDomain(AllCircles_region);

                if (_detection_Region != null)
                {
                    _detection_Region.Dispose();
                    _detection_Region = null;
                }

                _detection_Region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
                AllCircles.Dispose(); AllCircles = null;
                AllCircles_region.Dispose(); AllCircles_region = null;
                ReduceImage.Dispose(); ReduceImage = null;

                WindowControl.HalconWindow.ClearWindow();
                WindowControl.HalconWindow.DispObj(Image);
                WindowControl.HalconWindow.SetColor("red");
                WindowControl.HalconWindow.SetDraw("fill");
                WindowControl.HalconWindow.DispObj(_detection_Region);
            }
            else
                MessageBox.Show("Please set targer first !");

        }

        public void OnImageIn(object sender, StationCaptureArgs args)
        {
            Image?.Dispose();
            Image = args.imgs[0];
            ShowTheImage(args.imgs);
            _imageQueue.Enqueue(true);
        }

        public void ShowTheImage(List<CustomImage> img)
        {
            //check the part
            img[0].GetImageSize(out int w, out int h);

            if (WindowControl != null)
            {
                //if (CurrentPartX != w || CurrentPartY != h)
                //{
                //    CurrentPartX = w;
                //    CurrentPartY = h;
                WindowControl.SetFullImagePart(img[0]);
                //}

                WindowControl.HalconWindow.DispObj(img[0]);
            }
            Image = img[0];
        }
        public class LaserResult
        {
            //public Point2d Machine_Position = new Point2d(-1, -1);
            public double Machine_Position_X { get; set; } = -1;
            public double Machine_Position_Y { get; set; } = -1;
            public double Height { get; set; } = -1;
        }

        //public List<LaserResult> _laserResult;
        double PixeltoMM = 1;
        public void Test()
        {     
            if (Image.Calibration is ICalibrated _calibrated)
            {
                PixeltoMM = _calibrated.Pix2MMX;
            }

           Image.GetImageSize(out int width, out int height);
            var imageCenter_X = width / 2;
            var imageCenter_Y = height / 2;

           
            var velDefAX1 = InspectionModule.SelectVelDef(InspectionModule.BX1_流道橫移軸, InspectionModule.BX1VelList);
            var velDefBY1 = InspectionModule.SelectVelDef(InspectionModule.視覺縱移軸, InspectionModule.AY1VelList);

            //清空DataGridView
            _laserResult.Clear();

            for (int index = 0; index < CircleRow_List.Length; index++)
            {
                
                //雷射中心移動
                double dist_X = Machine_X + (CircleCol_List[index].D - (double)imageCenter_X)* PixeltoMM;
                double dist_Y = Machine_Y + (CircleRow_List[index].D - (double)imageCenter_Y)* PixeltoMM - InspectionModule.LaserToCamera; 

                bool moveSuccessAX1 = InspectionModule.MoveAxis(InspectionModule.BX1_流道橫移軸, dist_X, velDefAX1);
                bool moveSuccessBY1 = InspectionModule.MoveAxis(InspectionModule.視覺縱移軸, dist_Y, velDefBY1);

                var AX1_waitRes = InspectionModule.BX1_流道橫移軸.WaitMotionDone();
                var BY1_waitRes = InspectionModule.視覺縱移軸.WaitMotionDone();

                //移動完成後量測
                if (AX1_waitRes && AX1_waitRes)
                {
                    if (InspectionModule.LaserReader is ILaserDistanceFinder _laser)
                    {
                                               
                        bool success = false;
                      
                        for (int check_index = 0; check_index < 3; check_index++)
                        {
                            SpinWait.SpinUntil(() => false, 1000);
                            success = _laser.ReadHeight(10000);

                            if (success)
                            {
                                LaserResult _LR = new LaserResult();
                                _LR.Height = _laser.GetHeight();
                                _LR.Machine_Position_X = dist_X;
                                _LR.Machine_Position_Y = dist_Y;

                                _laserResult.Add(_LR);

                                break;
                            }                           
                        }                          

                    }
                }
            }           
        }

        public void Reset()
        {

            //asdf
        }
        public void Save()
        {
            InspectionModule.SaveParam(this, InspectionModule);

            if (InspectionModule.TargetModel != null)
            {
                if (!Directory.Exists($@"D:\Coordinator2.0\Products\{InspectionModule.ProductName}\LaserData"))
                {
                    Directory.CreateDirectory($@"D:\Coordinator2.0\Products\{InspectionModule.ProductName}\LaserData");
                }
                HOperatorSet.WriteNccModel(InspectionModule.TargetModel, $@"D:\Coordinator2.0\Products\{InspectionModule.ProductName}\LaserData\LaserBlockModel.ncm");
                
            }
        }
        
        public void Dispose()
        {
            MainController.Framer.OnGroupAllCaptured -= OnImageIn;
        }
    }
}
