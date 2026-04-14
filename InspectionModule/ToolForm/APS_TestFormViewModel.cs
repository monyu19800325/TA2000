using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.Xpf;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using Hta.MotionBase;
using HTA.IFramer;
using HTA.Motion3.Virtual;
using HTA.TriggerServer.Triggers.ADLink;
using HTAMachine.Machine;
using HTAMachine.Machine.Services;
using HTAMachine.Module;
using HyperInspection;
using TA2100Modules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VisionController2.FlowForm.FlowSettingForm2;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using static HyperInspection.FlowForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using HalconDotNet;
using HTA.MainController;
using HTA.Utility.Structure;
using static DevExpress.Utils.Svg.CommonSvgImages;
using HTA.LightServer;
using ModuleTemplate.Services;
using HTA.CdiAuthorityControl;
using HTAMachine.Module.AxisConfigModule;
using System.Drawing;
using System.Windows;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.IO;
using ModuleTemplate;
using ControlFlow.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using FontStyle = System.Drawing.FontStyle;
using DevExpress.Data.Linq.Helpers;
using HTA.Utility.MeasureCommon;
using VisionController2.CameraBurnInForm;
using DevExpress.XtraExport.Helpers;
using static DevExpress.Data.Helpers.ExpressiveSortInfo;
using HTA.Utility.Halcon;

namespace TA2100Modules
{
    [POCOViewModel()]
    public class APS_TestFormViewModel : IDisposable //: XtraUserControl
    {
        public IDispatcherService DispatcherService => this.GetService<IDispatcherService>();

        private APS_TestForm form;
        public HWindowControl WindowControl;
        public MainController MainController;
        public InspectionModule Module;
        bool _liveOn = false;
        public CustomImage Image = new CustomImage();
        Queue<bool> _imageQueue = new Queue<bool>();
        int CurrentPartX = 0;
        int CurrentPartY = 0;
        public HImage ReduceImage;
        public double CircleRow;
        public double CircleColumn;
        public double CircleRadius;
        HRegion _region;
        LightForm _lightForm;
        public bool _loopFlg = false;
        public bool _btnRunEnable = true;
        private int _milliseconds = 0;
        public Velocity _ASPAxisSpeed = new Velocity();

        public Action DoSaveGlobalData;

        public List<Point2d> CenterPosList { get; set; } = new List<Point2d>();
        private List<DateTime> _timeStamps = new List<DateTime>();
        private List<double> _temperatureRecords = new List<double>();


        //public string MoveVel { get; set; }
        //public string Move_Axis { get; set; }
        public virtual int stepSetValue { get; set; } = 0;
        public virtual int stepCurrentValue { get; set; } = 0;

        public virtual double CurrentSpeed_Value { get; set; }

        //public virtual double AxisSP_Max_Value { get; set; } = 10;
        public virtual double AxisSP_Min_Value { get; set; } = 1;

        public bool IsSave { get; set; } = true;
        public List<string> VelList { get; set; } = new List<string>();
        public List<string> AxisList { get; set; } = new List<string>();

        public List<string> SelectList { get; set; } = new List<string>();
        public List<int> RectCircleList { get; set; } = new List<int>();
        public List<CircleClass> RectCirclePositions { get; set; } = new List<CircleClass>();
        public List<Rect2Data> Rect2Datas { get; set; } = new List<Rect2Data>();

        public virtual double Length { get; set; } = 10;
        public virtual double MeasureThreshold { get; set; } = 10.0;
        public virtual int MeasureNum { get; set; } = 30;
        public virtual string SelectText { get; set; } = "First";

        private double _axisSP_Max_Value;
        public virtual double AxisSP_Max_Value
        {
            get => _axisSP_Max_Value;
            set
            {
                _axisSP_Max_Value = value;
                this.RaisePropertyChanged(x => x.AxisSP_Max_Value);
            }
        }



        private int _rectCircleIndex = 0;
        public int RectCircleIndex
        {
            get => _rectCircleIndex;
            set
            {
                if (_rectCircleIndex == value)
                {
                    return;
                }
                _rectCircleIndex = value;
                WindowControl.HalconWindow.ClearWindow();
                WindowControl.HalconWindow.DispObj(Image);
                WindowControl.HalconWindow.DispCircle(RectCirclePositions[RectCircleIndex].Row, RectCirclePositions[RectCircleIndex].Col, RectCirclePositions[RectCircleIndex].Radius);
                Circle2d circle = new Circle2d(RectCirclePositions[RectCircleIndex].Row, RectCirclePositions[RectCircleIndex].Col, RectCirclePositions[RectCircleIndex].Radius);
                this.RaisePropertyChanged(x => x.RectCircleIndex);
            }
        }





        public string MoveVel
        {
            get => Module.ProductParam.InspectVel.ToString();
            set
            {
                Module.ProductParam.InspectVel = (MoveVelEm)Enum.Parse(typeof(MoveVelEm), value);
                CurrentSpeed_Value = AxisCheckSpeed();
                this.RaisePropertyChanged(x => x.MoveVel);
            }
        }

        string _axisName = "AY1_視覺縱移軸_左";
        public string Move_Axis
        {

            get => _axisName;
            set
            {
                _axisName = value;
                Step0_Offset = GetAxisOffset(0);
                Step1_Offset = GetAxisOffset(1);
                AxisSP_Max_Value = GetMaxSpeedValue();
                CurrentSpeed_Value = AxisCheckSpeed();
                this.RaisePropertyChanged(x => x.Move_Axis);
            }


            //get => Module.ProductParam.AxisName.ToString();
            //set
            //{
            //    Module.ProductParam.AxisName = (AxisEm)Enum.Parse(typeof(AxisEm), value);
            //    Step0_Offset = GetAxisOffset(0);
            //    Step1_Offset = GetAxisOffset(1);
            //    AxisSP_Max_Value = GetMaxSpeedValue();
            //    CurrentSpeed_Value = AxisCheckSpeed();
            //    this.RaisePropertyChanged(x => x.Move_Axis);
            //}
        }



        public double Step0_Offset //Axis_StepOffset_0  
        {
            get => GetAxisOffset(0);
            set
            {
                SetAxisOffset(0, value);
                this.RaisePropertyChanged(x => x.Step0_Offset);
            }
        }

        public double Step1_Offset //Axis_StepOffset_1  
        {
            get => GetAxisOffset(1);
            set
            {
                SetAxisOffset(1, value);
                this.RaisePropertyChanged(x => x.Step1_Offset);
            }
        }

        public double AZ1_Focus_Offset //Axis_StepOffset_1  
        {
            get => Module.Param.AZ1_TestFocus_Offset;
            set
            {
                Module.Param.AZ1_TestFocus_Offset = value;
                this.RaisePropertyChanged(x => x.AZ1_Focus_Offset);
            }
        }

        private string _delaySeconds = "0";
        public string DelaySeconds
        {
            get => _delaySeconds;
            set
            {
                _delaySeconds = value;
                this.RaisePropertyChanged(x => x.DelaySeconds);
            }
        }


       

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

        int _thresholdMax = 255;
        public int ThresholdMax
        {
            get => _thresholdMax;
            set
            {

                if (value < ThresholdMin)
                {
                    _thresholdMax = ThresholdMin+1;
                }
                _thresholdMax = value;
                var objCount = Image.CountObj();
                if (objCount == 0)
                {
                    return;
                }

                if (CircleRow == 0 && CircleColumn == 0 && CircleRadius == 0)
                {
                    return;
                }

                Circle2d circle = new Circle2d(CircleRow, CircleColumn, CircleRadius);
                var img = MainController.GetHardware().CalibrationLists[0].UnDistortionImage(Image, HTA.Utility.Calibration.UndisotrtInterMethodEm.Cubic);

                var region = circle.GenRegion();
                ReduceImage = img.ReduceDomain(region);
                _region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
                _region.SmallestCircle(out HTuple row, out HTuple column, out HTuple radius);
                Circle2d circleReg = new Circle2d(row, column, radius);
                var success = CircleMeasure(circleReg, img, out HTuple row1, out HTuple column1, out HTuple rad);//TODO 待測試

                if(success == true)
                {
                    Circle2d circle2 = new Circle2d(row1, column1, rad);

                    //_region.AreaCenter(out HTuple row1, out HTuple column1);

                    WindowControl.HalconWindow.ClearWindow();
                    WindowControl.HalconWindow.DispObj(Image);
                    WindowControl.HalconWindow.SetColor("red");
                    WindowControl.HalconWindow.SetDraw("margin");
                    WindowControl.HalconWindow.DispObj(circle2.GenRegion());

                    XPixelPos = column1;
                    YPixelPos = row1;
                    this.RaisePropertyChanged(x => x.ThresholdMax);
                }
            }
        }
        int _thresholdMin = 0;
        public int ThresholdMin
        {
            get => _thresholdMin;
            set
            {
                if (value > _thresholdMax)
                {
                    _thresholdMin = _thresholdMax-1;
                }
                _thresholdMin = value;

                var objCount = Image.CountObj();
                if (objCount == 0)
                {
                    return;
                }

                this.RaisePropertyChanged(x => x.ThresholdMin);
            }
        }


        public void checkImage()
        {

            Circle2d circle = new Circle2d(CircleRow, CircleColumn, CircleRadius);
            var img = MainController.GetHardware().CalibrationLists[0].UnDistortionImage(Image, HTA.Utility.Calibration.UndisotrtInterMethodEm.Cubic);

            var region = circle.GenRegion();
            ReduceImage = img.ReduceDomain(region);
            _region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
            _region = _region.Connection().SelectMax();

            _region.SmallestCircle(out HTuple row, out HTuple column, out HTuple radius);
            Circle2d circleReg = new Circle2d(row, column, radius);
            var success = CircleMeasure(circleReg, img, out HTuple row1, out HTuple column1, out HTuple rad);//TODO 待測試

            if (success == true)
            {
                Circle2d circle2 = new Circle2d(row1, column1, rad);

                //_region.AreaCenter(out HTuple row1, out HTuple column1);

                WindowControl.HalconWindow.ClearWindow();
                WindowControl.HalconWindow.DispObj(Image);
                WindowControl.HalconWindow.SetColor("red");
                WindowControl.HalconWindow.SetDraw("margin");
                WindowControl.HalconWindow.DispObj(circle2.GenRegion());

                XPixelPos = column1;
                YPixelPos = row1;

            }
        }

        private double _xPixelPos;
        public double XPixelPos
        {
            get => _xPixelPos;
            set
            {
                _xPixelPos = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.XPixelPos);
                });
            }
        }

        private double _yPixelPos;
        public double YPixelPos
        {
            get => _yPixelPos;
            set
            {
                _yPixelPos = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.YPixelPos);
                });
            }
        }


        private double _xPixelPosRect;
        public double XPixelPosRect
        {
            get => _xPixelPosRect;
            set
            {
                _xPixelPosRect = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.XPixelPosRect);
                });
            }
        }

        private double _yPixelPosRect;
        public double YPixelPosRect
        {
            get => _yPixelPosRect;
            set
            {
                _yPixelPosRect = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.YPixelPosRect);
                });
            }
        }

        private double _rectPosX;
        public double RectPosX
        {
            get => _rectPosX;
            set
            {
                _rectPosX = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.RectPosX);
                });
            }
        }

        private double _rectPosY;
        public double RectPosY
        {
            get => _rectPosY;
            set
            {
                _rectPosY = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.RectPosY);
                });
            }
        }

        private double _rectRegPosX;
        public double RectRegPosX
        {
            get => _rectRegPosX;
            set
            {
                _rectRegPosX = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.RectRegPosX);
                });
            }
        }

        private double _rectRegPosY;
        public double RectRegPosY
        {
            get => _rectRegPosY;
            set
            {
                _rectRegPosY = Math.Round(value, 2);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.RectRegPosY);
                });
            }
        }

        private double _rectPhi;
        public double RectPhi
        {
            get => _rectPhi;
            set
            {
                _rectPhi = Math.Round(value, 4);
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.RectPhi);
                });
            }
        }


        public void Init(InspectionModule module)
        {
            Module = module;
            MainController = (MainController)module.VisionController;
            MainController.Framer.OnGroupAllCaptured += OnImageIn;

            VelList = Enum.GetNames(typeof(MoveVelEm)).ToList();
            MoveVel = VelList.FirstOrDefault();


            AxisList = Enum.GetNames(typeof(AxisEm)).ToList();
            Move_Axis = AxisList.FirstOrDefault();

            SelectList = Enum.GetNames(typeof(MeasureSelectEm)).ToList();

            AxisSP_Max_Value = GetMaxSpeedValue();
            IniVelocity();

            var groupInfo = new List<double> { 20 * 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            Lights = groupInfo.ToArray();

            RectCircleList = new List<int>() { 0, 1, 2, 3 };
            RectCircleIndex = 0;
            RectCirclePositions.Clear();
            for (int i = 0; i < RectCircleList.Count; i++)
            {
                RectCirclePositions.Add(new CircleClass(0, 0, 0));
            }


        }

        public void SetView(APS_TestForm aPS_TestForm)
        {
            form = aPS_TestForm;
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
                if (CurrentPartX != w || CurrentPartY != h)
                {
                    CurrentPartX = w;
                    CurrentPartY = h;
                    WindowControl.SetFullImagePart(img[0]);
                }

                WindowControl.HalconWindow.DispObj(img[0]);
            }
            Image = img[0];
        }

        public double[] Lights;
        public void SetLight()
        {
            if (_lightForm == null)
            {
                _lightForm = new LightForm(MainController.Lighter, MainController.Lighter.GetLight());
                _lightForm.StartPosition = FormStartPosition.Manual;
                _lightForm.Left = form.Right;
                _lightForm.Top = form.Top;
                if (form.MdiParent != null)
                    _lightForm.MdiParent = form.MdiParent;
                _lightForm.OnLightValueChange += (ss, ee) =>
                {
                    MainController.Lighter.SetLight(ee);
                };
                _lightForm.Closed += (ss, ee) =>
                {
                    _lightForm = null;
                };
            }
            _lightForm.Show();
        }

        public void DrawROI()
        {
            WindowControl.HalconWindow.SetColor("red");
            WindowControl.HalconWindow.SetDraw("margin");
            WindowControl.HalconWindow.DrawCircle(out double row, out double column, out double radius);
            CircleRow = row;
            CircleColumn = column;
            CircleRadius = radius;

            WindowControl.HalconWindow.ClearWindow();
            WindowControl.HalconWindow.DispObj(Image);
            WindowControl.HalconWindow.DispCircle(row, column, radius);
            Circle2d circle = new Circle2d(row, column, radius);
            var region = circle.GenRegion();
            ReduceImage = Image.ReduceDomain(region);
            _region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
            _region.AreaCenter(out HTuple row1, out HTuple column1);
            WindowControl.HalconWindow.DispCross(row1, column1, 100, 0);
            XPixelPos = column1;
            YPixelPos = row1;
        }

        public void Capture()
        {
            _imageQueue.Clear();
            MainController.Trigger1.ManualTrigger();
            if (SpinWait.SpinUntil(() => _imageQueue.Count > 0, 10000))
            {
                _imageQueue.Dequeue();
                Circle2d circle = new Circle2d(CircleRow, CircleColumn, CircleRadius);
                var img = MainController.GetHardware().CalibrationLists[0].UnDistortionImage(Image, HTA.Utility.Calibration.UndisotrtInterMethodEm.Cubic);

                var region = circle.GenRegion();
                ReduceImage = img.ReduceDomain(region);
                _region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
                _region.SmallestCircle(out HTuple row, out HTuple column, out HTuple radius);
                Circle2d circleReg = new Circle2d(row, column, radius);
                var success = CircleMeasure(circleReg, img, out HTuple row1, out HTuple column1, out HTuple rad);//TODO 待測試

                if (success)
                {
                    Circle2d circle2 = new Circle2d(row1, column1, rad);

                    //_region.AreaCenter(out HTuple row1, out HTuple column1);

                    WindowControl.HalconWindow.ClearWindow();
                    WindowControl.HalconWindow.DispObj(Image);
                    WindowControl.HalconWindow.SetColor("red");
                    WindowControl.HalconWindow.SetDraw("margin");
                    WindowControl.HalconWindow.DispObj(circle2.GenRegion());

                    WindowControl.HalconWindow.DispCross(row1, column1, 100, 0);
                    XPixelPos = column1;
                    YPixelPos = row1;
                    CenterPosList.Add(new Point2d(column1, row1));

                    if (_timeStamps == null)
                        _timeStamps = new List<DateTime>();
                    _timeStamps.Add(DateTime.Now);

                    if (_temperatureRecords == null)
                        _temperatureRecords = new List<double>();
                    _temperatureRecords.Add(Module.VisionController.Framer.Grabbers[0].Temperature);

                   // var currentTmp  = Module.VisionController.Framer.Grabbers[0].Temperature;
                }
            }
            else
            {
                //取影像Timeout
                var service = Module.GetHtaService<HTAMachine.Machine.Services.IDialogService>();
                service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "Error",
                    Message = "Capture Image Timeout",
                });
            }
        }

        public bool CircleMeasure(Circle2d tarCircle, HImage img, out HTuple row, out HTuple col, out HTuple rad)
        {
            HMetrologyModel metrologyModel = new HMetrologyModel();
            metrologyModel.CreateMetrologyModel();

            MeasureCircleParam measureCircleParam = new MeasureCircleParam()
            {
                MeasureLenth = Length,
                MeasureThreshold = MeasureThreshold,
                Sigma = 1.0,
                MeasureNum = MeasureNum,
                Polor = MeasurePolorEm.WhiteToBlack,
                Select = ChangeSelectEm(SelectText),
            };



            metrologyModel.AddMetrologyObjectCircleMeasure(
                (HTuple)tarCircle.Y, (HTuple)tarCircle.X, (HTuple)tarCircle.Radius,
                (HTuple)measureCircleParam.MeasureLenth, (HTuple)5.0,
                (HTuple)measureCircleParam.Sigma, (HTuple)measureCircleParam.MeasureThreshold,
                new HTuple(), new HTuple());

            metrologyModel.SetMetrologyObjectParam("all", "min_score", 0.8);
            metrologyModel.SetMetrologyObjectParam("all", "num_measures", measureCircleParam.MeasureNum);
            metrologyModel.SetMetrologyObjectParam("all", "measure_transition", toHalconPolor(measureCircleParam.Polor));
            metrologyModel.ApplyMetrologyModel(img);
            var circle = metrologyModel.GetMetrologyObjectResultContour("all", "all", 0.1);

            HTuple rows = metrologyModel.GetMetrologyObjectResult("all", "all", new HTuple("result_type"), new HTuple("row"));
            HTuple cols = metrologyModel.GetMetrologyObjectResult("all", "all", new HTuple("result_type"), new HTuple("column"));
            HTuple radiuss = metrologyModel.GetMetrologyObjectResult("all", "all", new HTuple("result_type"), new HTuple("radius"));

            row = rows;
            col = cols;
            rad = radiuss;

            metrologyModel.Dispose();

            if (rows.Length == 0)
            {
                MessageBox.Show("量測失敗");
                return false;
            }

            return true;
        }

        string toHalconPolor(HTA.Utility.MeasureCommon.MeasurePolorEm src)
        {
            if (src == HTA.Utility.MeasureCommon.MeasurePolorEm.BlackToWhite)
                return "positive";
            else if (src == HTA.Utility.MeasureCommon.MeasurePolorEm.WhiteToBlack)
                return "negative";

            return "all";
        }

        public MeasureSelectEm ChangeSelectEm(string select)
        {
            switch (select)
            {
                case "First":
                    return MeasureSelectEm.First;
                case "Last":
                    return MeasureSelectEm.Last;
                case "All":
                    return MeasureSelectEm.All;
            }
            return MeasureSelectEm.All;
        }

        public void WriteResult(DateTime _startTime, double _currentSpeed, string _axisName)
        {

            List<Point2d> diffList = new List<Point2d>();
            DateTime startTime = _startTime;
            DateTime endTime = DateTime.Now;
           

            for (int i = 1; i < CenterPosList.Count; i++)
            {
                var diff = CenterPosList[0] - CenterPosList[i];
                diffList.Add(new Point2d(diff.x, diff.y));
            }

            var pixeltomm = ((Calibration.CalibrationSingleCam)Image.Calibration).Pix2MMAvr;

            string baseFolder = "D:\\APSTestForm";
            if (!Directory.Exists(baseFolder))
                Directory.CreateDirectory(baseFolder);

            string timestamp = endTime.ToString("yyyy-MM-dd_HH-mm-ss");
            string reportFolder = Path.Combine(baseFolder, timestamp);

            Directory.CreateDirectory(reportFolder); // 建立子資料夾

            // 建立檔案名安全的軸名稱與速度字串（避免特殊符號）
            string safeAxisName = _axisName.Replace(":", "").Replace(" ", "_");
            string speedStr = _currentSpeed.ToString("F0"); // 取整數或你要的格式

            string axisInfoFileName = $"TestReport_AxisInfo_{safeAxisName}_{speedStr}_{timestamp}.csv";
            string dataFileName = $"TestReport_Data_{safeAxisName}_{speedStr}_{timestamp}.csv";

            string axisInfoPath = Path.Combine(reportFolder, axisInfoFileName);
            string dataPath = Path.Combine(reportFolder, dataFileName);

            double maxDiffX = diffList.Max(p => p.x);
            double minDiffX = diffList.Min(p => p.x);
            double maxDiffY = diffList.Max(p => p.y);
            double minDiffY = diffList.Min(p => p.y);
            double avgDiffX = diffList.Average(p => p.x);
            double avgDiffY = diffList.Average(p => p.y);

            // 找最大/最小 X 與 Y 的值和索引
            int maxXIndex = diffList.IndexOf(diffList.OrderByDescending(p => p.x).First());
            int minXIndex = diffList.IndexOf(diffList.OrderBy(p => p.x).First());
            int maxYIndex = diffList.IndexOf(diffList.OrderByDescending(p => p.y).First());
            int minYIndex = diffList.IndexOf(diffList.OrderBy(p => p.y).First());


            // 原始參考點（中心點）
            double refX_mm = CenterPosList[0].x * pixeltomm;
            double refY_mm = CenterPosList[0].y * pixeltomm;

            double Max_Deviation_Range_X = (maxDiffX - minDiffX) * pixeltomm;
            double Max_Deviation_Range_Y = (maxDiffY - minDiffY) * pixeltomm;


            // 寫入 Axis Info
            using (StreamWriter w = new StreamWriter(axisInfoPath))
            {
                w.WriteLine("Key,Value");
                w.WriteLine($"Machine,TA1000");
                w.WriteLine($"Axis,{_axisName}");
                w.WriteLine($"Pixeltomm,{pixeltomm}");
                w.WriteLine($"Start Time,{startTime:yyyy-MM-dd HH:mm:ss}");
                w.WriteLine($"End Time,{endTime:yyyy-MM-dd HH:mm:ss}");
                w.WriteLine($"Total Comparisons,{diffList.Count}");
                w.WriteLine($"Axis_Speed (mm/sec),{_currentSpeed}");
                w.WriteLine($"Max Deviation Range_X (mm),{Max_Deviation_Range_X:F6}");
                w.WriteLine($"Max Deviation Range_Y (mm),{Max_Deviation_Range_Y:F6}");

                w.WriteLine($"Max X Diff (mm) [Index],{diffList[maxXIndex].x * pixeltomm:F6} ({maxXIndex})");
                w.WriteLine($"Min X Diff (mm) [Index],{diffList[minXIndex].x * pixeltomm:F6} ({minXIndex})");
                w.WriteLine($"Max Y Diff (mm) [Index],{diffList[maxYIndex].y * pixeltomm:F6} ({maxYIndex})");
                w.WriteLine($"Min Y Diff (mm) [Index],{diffList[minYIndex].y * pixeltomm:F6} ({minYIndex})");

                w.WriteLine($"Average X Diff (mm),{avgDiffX * pixeltomm:F6}");
                w.WriteLine($"Average Y Diff (mm),{avgDiffY * pixeltomm:F6}");

            }

            using (StreamWriter w = new StreamWriter(dataPath))
            {
                w.WriteLine("Index,X Diff (mm),Y Diff (mm),X_diff_pixel,Y_diff_pixel,X_pixel,Y_pixel,X_mm,Y_mm,Timestamp");

                for (int i = 1; i < CenterPosList.Count; i++)
                {
                    var diff = CenterPosList[0] - CenterPosList[i];
                    double x_pixel = CenterPosList[i].x;
                    double y_pixel = CenterPosList[i].y;
                    double x_mm = x_pixel * pixeltomm;
                    double y_mm = y_pixel * pixeltomm;
                    double x_diff_pixel = diff.x;
                    double y_diff_pixel = diff.y;

                    string rowtimestamp = (i - 1 < _timeStamps.Count)
                        ? _timeStamps[i - 1].ToString("yyyy-MM-dd HH:mm:ss.fff")
                        : "N/A";

                    string temperature = (i - 1 < _temperatureRecords.Count)
                                   ? _temperatureRecords[i - 1].ToString("F2")
                   : "N/A";

                    w.WriteLine($"{i - 1},{diff.x * pixeltomm:F6},{diff.y * pixeltomm:F6},{x_diff_pixel:F2}," +
                        $"{y_diff_pixel:F2},{x_pixel:F2},{y_pixel:F2},{x_mm:F6},{y_mm:F6},{rowtimestamp},{temperature}");
                }
            }

            // 呼叫畫圖功能
            GenerateDeviationChart(diffList, pixeltomm, reportFolder, timestamp, _axisName, _currentSpeed, startTime, endTime, Max_Deviation_Range_X, Max_Deviation_Range_Y, _timeStamps);
            GenerateTemperatureChart(_timeStamps, _temperatureRecords, reportFolder, timestamp);


            #region old (.TXT)
            //List<Point2d> diffList = new List<Point2d>();
            //DateTime startTime = _startTime;
            //DateTime endTime = DateTime.Now;

            //for (int i = 1; i < CenterPosList.Count; i++)
            //{
            //    var diff = CenterPosList[0] - CenterPosList[i];
            //    //diffList.Add(new Point2d(Math.Abs(diff.x), Math.Abs(diff.y)));
            //    diffList.Add(new Point2d(diff.x,diff.y));
            //}

            //var pixeltomm =  ((Calibration.CalibrationSingleCam)Image.Calibration).Pix2MMAvr;

            ////if (Directory.Exists("D:\\APSTestForm") == false)
            ////    Directory.CreateDirectory("D:\\APSTestForm");

            //string folderPath = "D:\\APSTestForm";
            //if (!Directory.Exists(folderPath))
            //    Directory.CreateDirectory(folderPath);

            //// ➕ 加入時間戳記的檔名
            //string timestamp = endTime.ToString("yyyy-MM-dd_HH-mm-ss");
            //string filePath = Path.Combine(folderPath, $"TestReport_{timestamp}.txt");


            //// 取得最大、最小、平均
            //double maxDiffX = diffList.Max(p => p.x);
            //double minDiffX = diffList.Min(p => p.x);
            //double maxDiffY = diffList.Max(p => p.y);
            //double minDiffY = diffList.Min(p => p.y);

            //double avgDiffX = diffList.Average(p => p.x);
            //double avgDiffY = diffList.Average(p => p.y);




            //using (StreamWriter w = new StreamWriter(filePath))
            //{
            //    w.WriteLine("---------------------------------------------------------------------------------------");
            //    w.WriteLine("APSTestForm Report");
            //    w.WriteLine("---------------------------------------------------------------------------------------");
            //    w.WriteLine($"Machine   : TA1000");
            //    w.WriteLine($"Axis   : {_axisName}");
            //    w.WriteLine($"Start Time          : {startTime:yyyy-MM-dd HH:mm:ss}");
            //    w.WriteLine($"End Time            : {endTime:yyyy-MM-dd HH:mm:ss}");
            //    w.WriteLine($"Total Comparisons   : {diffList.Count}");
            //    w.WriteLine($"Axis_Speed (mm/sec) : {_currentSpeed}");
            //    w.WriteLine("---------------------------------------------------------------------------------------");
            //    w.WriteLine($"Maximum Deviation Range_X Diff (mm)       : {(maxDiffX - minDiffX) * pixeltomm}");
            //    w.WriteLine($"Maximum Deviation Range_Y Diff (mm)       : {(maxDiffY - minDiffY) * pixeltomm}");
            //    w.WriteLine($"Average X Diff (mm)                       : {avgDiffX * pixeltomm}");
            //    w.WriteLine($"Average Y Diff (mm)                       : {avgDiffY * pixeltomm}");
            //    w.WriteLine("=======================================================================================");


            //    for (int i = 0; i < diffList.Count; i++)
            //    {
            //        w.WriteLine($"{i} index Diff x:{diffList[i].x * pixeltomm} mm");
            //        w.WriteLine($"{i} index Diff y:{diffList[i].y * pixeltomm} mm");
            //    }

            //    w.Close();

            //}


            #endregion
        }


        private void GenerateDeviationChart(List<Point2d> diffList, double pixeltomm, string saveFolder, string timestamp, string axisName, double speed, DateTime startTime,
    DateTime endTime, double _Max_Deviation_Range_X, double _Max_Deviation_Range_Y, List<DateTime> timeStamps, int width = 1200, int height = 800, int dpi = 300)
        {
            double[] xVals = new double[diffList.Count];
            double[] yVals = new double[diffList.Count];
            double sumX = 0, sumY = 0;
            double maxX = double.MinValue, minX = double.MaxValue;
            double maxY = double.MinValue, minY = double.MaxValue;
            int maxXIndex = -1, minXIndex = -1, maxYIndex = -1, minYIndex = -1;

            DateTime[] xTimeAxis = timeStamps.Take(diffList.Count).ToArray();

            for (int i = 0; i < diffList.Count; i++)
            {
                xVals[i] = diffList[i].x * pixeltomm;
                yVals[i] = diffList[i].y * pixeltomm;

                sumX += xVals[i];
                sumY += yVals[i];

                if (xVals[i] > maxX) { maxX = xVals[i]; maxXIndex = i; }
                if (xVals[i] < minX) { minX = xVals[i]; minXIndex = i; }
                if (yVals[i] > maxY) { maxY = yVals[i]; maxYIndex = i; }
                if (yVals[i] < minY) { minY = yVals[i]; minYIndex = i; }
            }

            double avgX = sumX / xVals.Length;
            double avgY = sumY / yVals.Length;

            var chart = new Chart
            {
                Size = new System.Drawing.Size(width, height)
            };

            var areaX = new ChartArea("AreaX")
            {
                Position = new ElementPosition(5, 5, 80, 42),
                AxisX = {
     Title = "Time",
     IntervalType = DateTimeIntervalType.Minutes,
     Interval = 5,
     LabelStyle = { Format = "HH:mm" },
     MajorGrid = { LineDashStyle = ChartDashStyle.Dot }
 },
                AxisY = {
     Title = "X Deviation (mm)",
     Minimum = -0.05,
     Maximum = 0.05,
     LabelStyle = { Format = "F6" },
     MajorGrid = { LineDashStyle = ChartDashStyle.Dot }
 }
            };

            var areaY = new ChartArea("AreaY")
            {
                Position = new ElementPosition(5, 55, 80, 38),
                AxisX = {
     Title = "Time",
     IntervalType = DateTimeIntervalType.Minutes,
     Interval = 5,
     LabelStyle = { Format = "HH:mm" },
     MajorGrid = { LineDashStyle = ChartDashStyle.Dot }
 },
                AxisY = {
     Title = "Y Deviation (mm)",
     Minimum = -0.05,
     Maximum = 0.05,
     LabelStyle = { Format = "F6" },
     MajorGrid = { LineDashStyle = ChartDashStyle.Dot }
 }
            };

            chart.ChartAreas.Add(areaX);
            chart.ChartAreas.Add(areaY);

            var seriesX = new Series("X Diff")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                ChartArea = "AreaX",
                LegendText = "X Diff (mm)"
            };

            var seriesY = new Series("Y Diff")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2,
                ChartArea = "AreaY",
                LegendText = "Y Diff (mm)"
            };

            for (int i = 0; i < diffList.Count; i++)
            {
                seriesX.Points.AddXY(xTimeAxis[i], xVals[i]);
                seriesY.Points.AddXY(xTimeAxis[i], yVals[i]);
            }

            // 平均線
            areaX.AxisY.StripLines.Add(new StripLine
            {
                IntervalOffset = avgX,
                BorderColor = Color.Blue,
                BorderDashStyle = ChartDashStyle.Dash,
                BorderWidth = 1,
                Text = $"Avg X: {avgX:F6}",
                TextAlignment = StringAlignment.Near,
                TextLineAlignment = StringAlignment.Far
            });

            areaY.AxisY.StripLines.Add(new StripLine
            {
                IntervalOffset = avgY,
                BorderColor = Color.Red,
                BorderDashStyle = ChartDashStyle.Dash,
                BorderWidth = 1,
                Text = $"Avg Y: {avgY:F6}",
                TextAlignment = StringAlignment.Near,
                TextLineAlignment = StringAlignment.Far
            });

            // ±0.0045 mm 閾值線
            foreach (var area in new[] { areaX, areaY })
            {
                area.AxisY.StripLines.Add(new StripLine
                {
                    IntervalOffset = 0.0045,
                    StripWidth = 0.00001,
                    BorderColor = Color.Gray,
                    BorderWidth = 1
                });
                area.AxisY.StripLines.Add(new StripLine
                {
                    IntervalOffset = -0.0045,
                    StripWidth = 0.00001,
                    BorderColor = Color.Gray,
                    BorderWidth = 1
                });
            }

            // 最大/最小點標記
            seriesX.Points[maxXIndex].MarkerStyle = MarkerStyle.Circle;
            seriesX.Points[maxXIndex].MarkerSize = 8;
            seriesX.Points[maxXIndex].MarkerColor = Color.DarkBlue;
            seriesX.Points[maxXIndex].Label = $"Max X: {xVals[maxXIndex]:F6}";

            seriesX.Points[minXIndex].MarkerStyle = MarkerStyle.Circle;
            seriesX.Points[minXIndex].MarkerSize = 8;
            seriesX.Points[minXIndex].MarkerColor = Color.Cyan;
            seriesX.Points[minXIndex].Label = $"Min X: {xVals[minXIndex]:F6}";

            seriesY.Points[maxYIndex].MarkerStyle = MarkerStyle.Circle;
            seriesY.Points[maxYIndex].MarkerSize = 8;
            seriesY.Points[maxYIndex].MarkerColor = Color.DarkRed;
            seriesY.Points[maxYIndex].Label = $"Max Y: {yVals[maxYIndex]:F6}";

            seriesY.Points[minYIndex].MarkerStyle = MarkerStyle.Circle;
            seriesY.Points[minYIndex].MarkerSize = 8;
            seriesY.Points[minYIndex].MarkerColor = Color.Orange;
            seriesY.Points[minYIndex].Label = $"Min Y: {yVals[minYIndex]:F6}";

            chart.Series.Add(seriesX);
            chart.Series.Add(seriesY);

            // 右上角註解
            string infoText =
                $"Machine: TA1000\n" +
                $"Axis: {axisName}\n" +
                $"Start: {startTime:yyyy-MM-dd HH:mm:ss}\n" +
                $"End:   {endTime:yyyy-MM-dd HH:mm:ss}\n" +
                $"Speed: {speed} mm/sec\n" +
                $"Points: {diffList.Count}\n" +
                $"AxisWaitTime: {_delaySeconds}\n" +
                

                $"Deviation Range X: {_Max_Deviation_Range_X:F6} mm\n" +
                $"Deviation Range Y: {_Max_Deviation_Range_Y:F6} mm\n" +
                $"Max X: {xVals[maxXIndex]:F6} mm (#{maxXIndex})\n" +
                $"Min X: {xVals[minXIndex]:F6} mm (#{minXIndex})\n" +
                $"Max Y: {yVals[maxYIndex]:F6} mm (#{maxYIndex})\n" +
                $"Min Y: {yVals[minYIndex]:F6} mm (#{minYIndex})\n";

            chart.Annotations.Add(new TextAnnotation
            {
                Text = infoText,
                Font = new Font("Consolas", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.FromArgb(230, Color.White),
                LineColor = Color.Gray,
                LineDashStyle = ChartDashStyle.Solid,
                LineWidth = 1,
                X = 86,
                Y = 5,
                Width = 13,
                Height = 40,
                TextStyle = TextStyle.Default,
                Alignment = ContentAlignment.TopLeft,
                AnchorAlignment = ContentAlignment.TopRight
            });

            chart.Legends.Add(new Legend("MainLegend")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            });

            chart.Titles.Add($"Deviation Chart - {timestamp}");

            chart.Invalidate();

            // 儲存 PNG
            string pngPath = Path.Combine(saveFolder, $"DeviationChart_{timestamp}.png");
            chart.SaveImage(pngPath, ChartImageFormat.Png);
        }

        private void GenerateTemperatureChart(List<DateTime> timeStamps, List<double> temperatureRecords, string saveFolder, string timestamp, int width = 1200, int height = 400, int dpi = 300)
        {
            if (timeStamps == null || temperatureRecords == null || timeStamps.Count == 0 || temperatureRecords.Count == 0)
                return;

            int count = Math.Min(timeStamps.Count, temperatureRecords.Count);
            DateTime[] xAxis = timeStamps.Take(count).ToArray();
            double[] yTemp = temperatureRecords.Take(count).ToArray();

            var chart = new Chart
            {
                Size = new System.Drawing.Size(width, height)
            };

            var area = new ChartArea("TempArea")
            {
                AxisX = {
                Title = "Time",
                IntervalType = DateTimeIntervalType.Minutes,
                Interval = 5,
                LabelStyle = { Format = "HH:mm" },
                MajorGrid = { LineDashStyle = ChartDashStyle.Dot }
                },
                AxisY = {
                Title = "Temperature (°C)",
                LabelStyle = { Format = "F2" },
                MajorGrid = { LineDashStyle = ChartDashStyle.Dot }
                }
            };

            chart.ChartAreas.Add(area);

            var series = new Series("Temperature")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Orange,
                LegendText = "Temperature (°C)"
            };

            for (int i = 0; i < count; i++)
            {
                series.Points.AddXY(xAxis[i], yTemp[i]);
            }

            // 平均線
            double avg = yTemp.Average();
            area.AxisY.StripLines.Add(new StripLine
            {
                IntervalOffset = avg,
                BorderColor = Color.Orange,
                BorderDashStyle = ChartDashStyle.Dash,
                BorderWidth = 1,
                Text = $"Avg: {avg:F2}°C",
                TextAlignment = StringAlignment.Near,
                TextLineAlignment = StringAlignment.Far
            });

            chart.Series.Add(series);
            chart.Legends.Add(new Legend("MainLegend") { Docking = Docking.Bottom, Alignment = StringAlignment.Center });
            chart.Titles.Add("Temperature Trend");

            chart.Invalidate();

            string pngPath = Path.Combine(saveFolder, $"TemperatureChart_{timestamp}.png");
            chart.SaveImage(pngPath, ChartImageFormat.Png);
        }



        public void AxisPositionStart()
        {
            string _Axis = Move_Axis;
            switch (_Axis)
            {
                case "AY1_視覺縱移軸_左":

                    StartAxisMoveLoop(Module.AY1_視覺縱移軸_左, Module.AY1VelList);

                    break;

                case "BZ1_流道頂升升降軸":

                    StartAxisMoveLoop(Module.BZ1_流道頂升升降軸, Module.BZ1VelList);

                    break;

                case "BX1_流道橫移軸":

                    StartAxisMoveLoop(Module.BX1_流道橫移軸, Module.BX1VelList);

                    break;
            }
        }



        public void LoopStart()
        {
            CenterPosList.Clear();
            _loopFlg = true;
            //_btnRunEnable = false;
            AxisPositionStart();
        }

        public void LoopEnd()
        {
            _loopFlg = false;
            //_btnRunEnable = true;
            //StopAxisMoveLoop();
        }



        public void IniVelocity()
        {
            _ASPAxisSpeed = _ASPAxisSpeed.GetClone();

        }





        private CancellationTokenSource _cts;

        private async Task AxisMoveLoopAsync(IAxis _Axis, List<VelocityData> _vellist)
        {
            _cts?.Cancel(); // 確保之前的 Task 被取消
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            string _axisName = _Axis.Name;

            await Task.Run(() =>
            {
                _timeStamps.Clear();
                _temperatureRecords.Clear();

                //var velAxis = TATool.SelectVelDef(_Axis, _vellist, MoveVel); // 該軸選擇速度
                _ASPAxisSpeed.MaxVel = CurrentSpeed_Value;
                //velAxis.MaxVel = CurrentSpeed_Value;
                int _loopCount = 1;
                DateTime startTime = DateTime.Now;

                while (true)
                {
                    // if (token.IsCancellationRequested) break;

                    if ((_loopCount <= stepSetValue || token.IsCancellationRequested)&& _loopFlg)
                    {
                        _ASPAxisSpeed.MaxVel = CurrentSpeed_Value < 10 ? 10 : CurrentSpeed_Value;
                       
                        _Axis.AbsoluteMove(Step0_Offset, _ASPAxisSpeed);

                        if (double.TryParse(_delaySeconds, out double seconds))
                        {
                            _milliseconds = (int) (seconds * 1000);
                        }
                        else
                        {
                            MessageBox.Show("輸入的不是有效的數字");
                        }

                        SpinWait.SpinUntil(() => false, _milliseconds); //延時設定

                        Capture();

                        if (SpinWait.SpinUntil(() => false, 500) || token.IsCancellationRequested) break;


                       _Axis.AbsoluteMove(Step1_Offset, _ASPAxisSpeed);    //暫時Bypass


                        if (SpinWait.SpinUntil(() => false, 500) || token.IsCancellationRequested) break;

                        //跨執行續
                        DispatcherService?.Invoke(() =>
                        {
                            stepCurrentValue = _loopCount;
                        });


                        _loopCount++;

                    }
                    else
                    {
                        _loopCount = 0;
                        //跨執行續
                        DispatcherService?.Invoke(() =>
                        {
                            stepCurrentValue = _loopCount;
                        });

                        break;
                    }

                }
                WriteResult(startTime, CurrentSpeed_Value, _axisName);
            }, token);

            StopAxisMoveLoop();

        }


        // 呼叫方法：
        public void StartAxisMoveLoop(IAxis _Axis, List<VelocityData> _vellist)
        {
            _ = AxisMoveLoopAsync(_Axis, _vellist);
        }

        // 停止並釋放資源：
        public void StopAxisMoveLoop()
        {
            //SpinWait.SpinUntil(() => stepCurrentValue == 0, 500);
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        //private void AxisMoveLoop(IAxis _Axis ,List<VelocityData> _vellist )
        //{

        //    var velAxis = TATool.SelectVelDef(_Axis, _vellist, MoveVel); //該軸選擇速度
        //    int _loopCount = 0;
        //    while (_loopFlg)
        //    {

        //        if (_loopCount <= stepSetValue)
        //        {
        //            _Axis.AbsoluteMove(Step0_Offset, velAxis);

        //            SpinWait.SpinUntil(() => false, 500);//等待一秒

        //            _Axis.AbsoluteMove(Step1_Offset, velAxis);

        //            SpinWait.SpinUntil(() => false, 500);//等待一秒
        //            _loopCount++;
        //            stepCurrentValue = _loopCount;
        //        }
        //        else
        //        {
        //            break;
        //        }

        //    }

        //}


        private double GetAxisOffset(int _step)
        {
            string _Axis = Move_Axis;
            double _OffSet = 0;

            if (_step == 0)
            {
                switch (_Axis)
                {
                    case "AY1_視覺縱移軸_左":

                        _OffSet = Module.Param.AX1_Test0_Offset;

                        break;

                    case "BZ1_流道頂升升降軸":

                        _OffSet = Module.Param.AZ1_Test0_Offset;

                        break;

                    case "BX1_流道橫移軸":

                        _OffSet = Module.Param.BY1_Test0_Offset;

                        break;

                }
            }
            else if (_step == 1)
            {
                switch (_Axis)
                {
                    case "AY1_視覺縱移軸_左":

                        _OffSet = Module.Param.AX1_Test1_Offset;

                        break;

                    case "BZ1_流道頂升升降軸":

                        _OffSet = Module.Param.AZ1_Test1_Offset;

                        break;

                    case "BX1_流道橫移軸":

                        _OffSet = Module.Param.BY1_Test1_Offset;

                        break;

                }
            }


            return _OffSet;
        }

        private void SetAxisOffset(int _step, double _offsetVel)
        {
            string _Axis = Move_Axis;

            if (_step == 0)
            {
                switch (_Axis)
                {
                    case "AY1_視覺縱移軸_左":

                        Module.Param.AX1_Test0_Offset = _offsetVel;

                        break;

                    case "BZ1_流道頂升升降軸":

                        Module.Param.AZ1_Test0_Offset = _offsetVel;

                        break;

                    case "BX1_流道橫移軸":

                        Module.Param.BY1_Test0_Offset = _offsetVel;

                        break;

                }
            }
            else if (_step == 1)
            {
                switch (_Axis)
                {
                    case "AY1_視覺縱移軸_左":

                        Module.Param.AX1_Test1_Offset = _offsetVel;

                        break;

                    case "BZ1_流道頂升升降軸":

                        Module.Param.AZ1_Test1_Offset = _offsetVel;

                        break;

                    case "BX1_流道橫移軸":

                        Module.Param.BY1_Test1_Offset = _offsetVel;

                        break;

                }
            }

        }

        public void AxisMove_Focus()
        {
            //var velAZ1 = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.AZ1VelList, MoveVel);
            _ASPAxisSpeed.MaxVel = 2;
            Module.BZ1_流道頂升升降軸.AbsoluteMove(AZ1_Focus_Offset, _ASPAxisSpeed);
        }

        public void AxisMove_Step_0()
        {
            string _Axis = Move_Axis;
            _ASPAxisSpeed.MaxVel = CurrentSpeed_Value;
            switch (_Axis)
            {
                case "AY1_視覺縱移軸_左":

                    //var velAX1 = TATool.SelectVelDef(Module.AY1_視覺縱移軸_左, Module.AX1VelList, MoveVel);
                    //velAX1.MaxVel = CurrentSpeed_Value;
                    Module.AY1_視覺縱移軸_左.AbsoluteMove(Module.Param.AX1_Test0_Offset, _ASPAxisSpeed);

                    break;

                case "BZ1_流道頂升升降軸":

                    //var velAZ1 = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.AZ1VelList, MoveVel);
                    //velAZ1.MaxVel = CurrentSpeed_Value;
                    Module.BZ1_流道頂升升降軸.AbsoluteMove(Module.Param.AZ1_Test0_Offset, _ASPAxisSpeed);


                    break;

                case "BX1_流道橫移軸":

                    //var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BY1VelList, MoveVel);
                    //velBY1.MaxVel = CurrentSpeed_Value;
                    Module.BX1_流道橫移軸.AbsoluteMove(Module.Param.BY1_Test0_Offset, _ASPAxisSpeed);

                    break;

            }


        }

        public void AxisMove_Step_1()
        {
            string _Axis = Move_Axis;
            _ASPAxisSpeed.MaxVel = CurrentSpeed_Value;
            switch (_Axis)
            {
                case "AY1_視覺縱移軸_左":

                    //var velAX1 = TATool.SelectVelDef(Module.AY1_視覺縱移軸_左, Module.AX1VelList, MoveVel);
                    //velAX1.MaxVel = CurrentSpeed_Value;
                    Module.AY1_視覺縱移軸_左.AbsoluteMove(Module.Param.AX1_Test1_Offset, _ASPAxisSpeed);

                    break;

                case "BZ1_流道頂升升降軸":

                    //var velAZ1 = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.AZ1VelList, MoveVel);
                    //velAZ1.MaxVel = CurrentSpeed_Value;
                    Module.BZ1_流道頂升升降軸.AbsoluteMove(Module.Param.AZ1_Test1_Offset, _ASPAxisSpeed);

                    break;

                case "BX1_流道橫移軸":

                    //var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BY1VelList, MoveVel);
                    //velBY1 .MaxVel = CurrentSpeed_Value;
                    Module.BX1_流道橫移軸.AbsoluteMove(Module.Param.BY1_Test1_Offset, _ASPAxisSpeed);

                    break;

            }

        }



        public double AxisCheckSpeed()
        {
            string _Axis = Move_Axis;
            Velocity _speed = null;

            switch (_Axis)
            {
                case "AY1_視覺縱移軸_左":

                    _speed = TATool.SelectVelDef(Module.AY1_視覺縱移軸_左, Module.AY1VelList, MoveVel);
                    //_reVel = _speed.MaxVel;
                    break;

                case "BZ1_流道頂升升降軸":

                    _speed = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, MoveVel);
                    //_reVel = _speed.MaxVel;

                    break;

                case "BX1_流道橫移軸":

                    _speed = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, MoveVel);
                    //_reVel = _speed.MaxVel;
                    break;

            }

            return _speed.MaxVel;

        }


        public double GetMaxSpeedValue()
        {
            string _Axis = Move_Axis;
            Velocity _speed = null;

            switch (_Axis)
            {
                case "AY1_視覺縱移軸_左":

                    _speed = TATool.SelectVelDef(Module.AY1_視覺縱移軸_左, Module.AY1VelList, "VeryHigh");

                    break;

                case "BZ1_流道頂升升降軸":

                    _speed = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, "VeryHigh");

                    break;

                case "BX1_流道橫移軸":

                    _speed = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, "VeryHigh");

                    break;

            }

            return _speed.MaxVel;
        }


        public void Save()
        {
           // Module.SaveParam(this, Module);
        }

        public void LoadImage()
        {
            CustomImage img = new CustomImage();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var filepath = openFileDialog.FileName;
                img.ReadImage(filepath);
            }
            OnImageIn(this, new StationCaptureArgs(0, 0, new List<CustomImage>() { img, img },-1));
        }


        public void Dispose()
        {
            MainController.Framer.OnGroupAllCaptured -= OnImageIn;
        }


        public void DrawRectCircle()
        {
            WindowControl.HalconWindow.SetColor("red");
            WindowControl.HalconWindow.SetDraw("margin");
            WindowControl.HalconWindow.DrawCircle(out double row, out double column, out double radius);
            RectCirclePositions[RectCircleIndex].Row = row;
            RectCirclePositions[RectCircleIndex].Col = column;
            RectCirclePositions[RectCircleIndex].Radius = radius;

            WindowControl.HalconWindow.ClearWindow();
            WindowControl.HalconWindow.DispObj(Image);
            WindowControl.HalconWindow.DispCircle(row, column, radius);
            Circle2d circle = new Circle2d(row, column, radius);
        }

        public void CheckRectCircleImage()
        {
            Circle2d circle = new Circle2d(RectCirclePositions[RectCircleIndex].Row, RectCirclePositions[RectCircleIndex].Col, RectCirclePositions[RectCircleIndex].Radius);
            var img = MainController.GetHardware().CalibrationLists[0].UnDistortionImage(Image, HTA.Utility.Calibration.UndisotrtInterMethodEm.Cubic);

            var region = circle.GenRegion();
            ReduceImage = img.ReduceDomain(region);
            _region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
            _region = _region.Connection().SelectMax();

            _region.SmallestCircle(out HTuple row, out HTuple column, out HTuple radius);
            Circle2d circleReg = new Circle2d(row, column, radius);
            var success = CircleMeasure(circleReg, img, out HTuple row1, out HTuple column1, out HTuple rad);//TODO 待測試

            if (success == true)
            {
                Circle2d circle2 = new Circle2d(row1, column1, rad);

                //_region.AreaCenter(out HTuple row1, out HTuple column1);

                WindowControl.HalconWindow.ClearWindow();
                WindowControl.HalconWindow.DispObj(Image);
                WindowControl.HalconWindow.SetColor("red");
                WindowControl.HalconWindow.SetDraw("margin");
                WindowControl.HalconWindow.DispObj(circle2.GenRegion());

                XPixelPosRect = column1;
                YPixelPosRect = row1;

            }
        }

        public void ShowRect()
        {
            List<Point2d> rects = new List<Point2d>();
            for (int i = 0; i < RectCirclePositions.Count; i++)
            {
                rects.Add(new Point2d(RectCirclePositions[i].Col, RectCirclePositions[i].Row));
            }
            Poly4 poly4 = new Poly4(rects);
            var region = poly4.GenRegion();

            region.SmallestRectangle2(out HTuple row, out HTuple column, out HTuple phi, out HTuple length1, out HTuple length2);
            region.AreaCenter(out HTuple row2, out HTuple column2);
            RectPosX = column;
            RectPosY = row;
            RectRegPosX = column2;
            RectRegPosY = row2;
            RectPhi = phi;
            WindowControl.HalconWindow.ClearWindow();
            WindowControl.HalconWindow.DispObj(Image);
            WindowControl.HalconWindow.SetColor("red");
            WindowControl.HalconWindow.SetDraw("margin");
            HRegion showRegion = new HRegion();
            showRegion.GenRectangle2(row, column, phi, length1, length2);
            WindowControl.HalconWindow.DispObj(showRegion);
            Rect2Datas.Add(new Rect2Data() { RectRow = row, RectCol = column, RectPhi = phi, RegionRow = row2, RegionCol = column2 });
        }

        public bool CalculateRectCircle(int index)
        {
            Circle2d circle = new Circle2d(RectCirclePositions[index].Row, RectCirclePositions[index].Col, RectCirclePositions[index].Radius);
            var img = MainController.GetHardware().CalibrationLists[0].UnDistortionImage(Image, HTA.Utility.Calibration.UndisotrtInterMethodEm.Cubic);

            var region = circle.GenRegion();
            ReduceImage = img.ReduceDomain(region);
            _region = ReduceImage.Threshold((HTuple)ThresholdMin, (HTuple)ThresholdMax);
            _region = _region.Connection().SelectMax();

            _region.SmallestCircle(out HTuple row, out HTuple column, out HTuple radius);
            Circle2d circleReg = new Circle2d(row, column, radius);
            var success = CircleMeasure(circleReg, img, out HTuple row1, out HTuple column1, out HTuple rad);//TODO 待測試

            if (success == true)
            {
                XPixelPosRect = column1;
                YPixelPosRect = row1;
            }
            return success;
        }

        public enum AxisEm
        {
            AY1_視覺縱移軸_左,
            BZ1_流道頂升升降軸,
            BX1_流道橫移軸
        }

        public class CircleClass
        {
            public double Row { get; set; }
            public double Col { get; set; }
            public double Radius { get; set; }
            public CircleClass(double row, double col, double radius)
            {
                Row = row;
                Col = col;
                Radius = radius;
            }
        }

        public class Rect2Data
        {
            public double RectRow { get; set; }
            public double RectCol { get; set; }
            public double RectPhi { get; set; }
            public double RegionRow { get; set; }
            public double RegionCol { get; set; }
        }

    }
}
