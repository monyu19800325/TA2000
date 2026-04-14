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
using HTAMachine.Machine.Services;
using HTAMachine.Machine;
using ModuleTemplate;
using HTA.MainController;
using VisionController2.MosaicController;
using HTA.IFramer;
using CalibrationForm;
using Hta.MotionBase;
using ModuleTemplate.Services;
using VisionController2.TinyForm;

using HyperInspection;
using VisionController2.ShadeCorrection;
using HTA.Utility.Structure;
using System.Threading;
using HTA.Utility.Calibration;
using HTA.Utility;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars.Docking;
using CalibrationModule.Properties;


namespace TA2000Modules
{
    public partial class CalibrationModule : ModuleBase, ICanSaveParam //, IInternalVision
    {
       

        MosaicForm _mosaicForm;
        MosaicControlForm _mosaicControlForm;
        IMdiService _mdiService;
        Calibration2DForm _calibrationForm;
        CalibrationControlForm _controlForm;
        LightCalibControl _lightControlForm;
        LightCalibration3 _lightCalibation3;
        CrossCenterControl _crossCenterControlForm;
        LaserCalibControl _laserCalibControl;
        FlowForm _flowForm;
        public Calibration.Halcon.MosaicTool.MotionPoseGrid MotionGrid = new Calibration.Halcon.MosaicTool.MotionPoseGrid();

        /*
         * 視覺縱移軸
         * BX1_流道橫移軸
         * BZ1_流道頂升升降軸
         */
        public List<VelocityData> AY1VelList; //視覺橫移軸
        public List<VelocityData> BZ1VelList; //視覺升降軸       
        public List<VelocityData> BX1VelList; //流道縱移軸    

        [ModuleGlobalSetting] public CalibrationModuleParam Param = new CalibrationModuleParam();

        //這樣Load產品的時間久，這個會多Load一次，所以改成用廣播的方式取得
        //[DefineVisionModule("LI3000Vision")] public IMainController VisionController;
        public IMainController VisionController;
        public IDialogService DialogService;
        public IMachineSimpleController MachineSimpleController;

        public event EventHandler<IModule> SaveGlobalParam;
        public event EventHandler<IModule> SaveProductParam;

        public int StationIndex { get; set; }
        private List<double> _originGains = new List<double>();

        [DefineBroadcast] public event EventHandler<MosaicPosArgs> OnNotifyMosaicPos;

        [DefineBroadcast]
        public event EventHandler<BoatCarrier> GetTrayLayout;

        [DefineEvent]
        public event EventHandler<InspectionParamArgs> GetMotorOffset;



        public BoatCarrier Carrier = new BoatCarrier();
        public string CurrentProductName = "";
        public void PreGetTray(string RecipeName, bool IsCalBig)
        {
            Carrier = new BoatCarrier();
            Carrier.IsPreUse = true;
            Carrier.IsCalBig = IsCalBig;
            Carrier.RecipeName = RecipeName;
            GetTrayLayout?.Invoke(this, Carrier);
        }

        [DefineBroadcastReceive]
        public void OnProductChange(object sender, ProductChangedArgs args)
        {
            CurrentProductName = args.ProductName;
            #region Mosaic Function

            PreGetTray(CurrentProductName, true);

            if (Carrier.IsBigProduct)
            {
                //讀取Mosaic檔案
                var _loadsuccess = TATool.LoadMosaic(out MotionGrid);

                bool _check = false;
                if (_loadsuccess)
                {
                    //整理Mosaic資訊
                    //List<MosaicInfo> _mosaicInfo_List = TATool.SortMosaicInfo(MotionGrid);

                    //確認是否有Mosaic點位
                    //_check = CheckPosition(_mosaicInfo_List);
                }

                //if (!_check)
                //{
                //    MessageBox.Show("此產品沒有做組圖校正，請做完組圖校正後，才可以執行流程");
                //}
            }


            #endregion

        }


        [DefineBroadcastReceive]
        public void GetVisionController(object sender , VisionControllerArgs visionControllerArgs)
        {
            VisionController = visionControllerArgs.VisionController;
        }

        public override void MachineControllerReady()
        {
            base.MachineControllerReady();

            var velService = this.GetHtaService<IAxisConfigVelocityService>();

            velService.DataSourceChanged += VelService_DataSourceChanged;

            //獲取所需軸的所有速度值
            AY1VelList = velService.QueryVelData(視覺縱移軸.Name);
            BZ1VelList = velService.QueryVelData(BZ1_流道頂升升降軸.Name);
            BX1VelList = velService.QueryVelData(BX1_流道橫移軸.Name);

            MachineSimpleController = this.GetHtaService<IMachineSimpleController>();
            DialogService = this.GetHtaService<IDialogService>();
            TATool.GrayCard_Y1 = Param.GrayCardAY1Pos;
            TATool.GrayCard_Z1 = Param.GrayCardBZ1Pos;
            TATool.LightCalib_Y1 = Param.AY1PosLightCalib;
            TATool.LightCalib_Z1 = Param.BZ1PosLightCalib;
            TATool.LightCalib_X1 = Param.BX1PosLightCalib;
        }

        private void VelService_DataSourceChanged(object sender, EventArgs e)
        {
            var velService = sender as IAxisConfigVelocityService;
            AY1VelList = velService.QueryVelData(視覺縱移軸.Name);
            BZ1VelList = velService.QueryVelData(BZ1_流道頂升升降軸.Name);
            BX1VelList = velService.QueryVelData(BX1_流道橫移軸.Name);
        }


        public MotorOffsetParam InspMotorOffsetParam = new MotorOffsetParam();

        /// <summary>
        /// 建構子
        /// </summary>
        public CalibrationModule() : base()
        {
            InitializeComponent();
        }

        public void OnSaveGlobalParam(object sender,IModule module)
        {
            SaveGlobalParam?.Invoke(sender, module);
        }


        public bool CheckPosition(List<MosaicInfo> MosaicInfo_List)
        {
            bool _checkExist = false;
            List<bool> _checkList = new List<bool>();

            var _productX_Count = Carrier.InspectData.InspectionPostion._bigProductX[0].Length;
            var _productY_Count = Carrier.InspectData.InspectionPostion._bigProductY[0].Length;
            var _mosaicPoint_Count = MotionGrid.Poses.Count;


            for (int x_index = 0; x_index < Carrier.InspectData.InspectionPostion.XMaxStepCount; x_index++)
            {
                for (int sub_index = 0; sub_index < Carrier.InspectData.InspectionPostion.MosaicXCount; sub_index++)
                {
                    _checkExist = false;
                    bool _sameX = MotionGrid.Poses.Any(x => x.MotionX == Carrier.InspectData.InspectionPostion._bigProductX[x_index][sub_index]);
                    if (_sameX ) { _checkExist = true; }
                    _checkList.Add(_checkExist);
                }
            }

            for (int y_index = 0; y_index < Carrier.InspectData.InspectionPostion.XMaxStepCount; y_index++)
            {
                for (int sub_index = 0; sub_index < Carrier.InspectData.InspectionPostion.MosaicYCount; sub_index++)
                {
                    _checkExist = false;
                    bool _sameY = MotionGrid.Poses.Any(x => x.MotionY == Carrier.InspectData.InspectionPostion._bigProductY[y_index][sub_index]);
                    if (_sameY) { _checkExist = true; }
                    _checkList.Add(_checkExist);
                }
            }


            //    for (int x_index = 0; x_index < _productX_Count; x_index++)
            //{
            //    for (int y_index = 0; y_index < _productY_Count; y_index++)
            //    {
            //        _checkExist = false;
            //        for (int index = 0; index < _mosaicPoint_Count; index++)
            //        {
            //            if (Carrier.InspectData.InspectionPostion._bigProductX[0][x_index] == MotionGrid.Poses[index].MotionX && Carrier.InspectData.InspectionPostion._bigProductY[0][y_index] == MotionGrid.Poses[index].MotionY)
            //            {
            //                _checkExist = true;
            //                //break;
            //            }
            //        }
            //        _checkList.Add(_checkExist);

            //    }
            //}


            _checkExist = true;
            for (int index = 0; index < _checkList.Count; index++)
            {
                if (_checkList[index] == false)
                {
                    _checkExist = false;
                }

            }

            return _checkExist;
        }


        [GroupDisplay(nameof(Resource.Icon_Calibration), nameof(Resource.CrossCenter), nameof(Resource.cross))]
        public void CrossCenterCalibration()
        {
            if (_mdiService == null)
                _mdiService = this.GetHtaService<IMdiService>();

            if (_flowForm == null)
            {
                var hardware = VisionController.GetHardware();

                _flowForm = (FlowForm)this.GetHtaService<IMdiService>().ShowMdi(this, typeof(FlowForm), new Point(0, 0), new Size(0, 0),
                    new object[] { VisionController, hardware, true }, true);
                _flowForm.CurrentAccessLevel = 4;

                _flowForm.FormClosed += (ss, ee) =>
                {
                    _flowForm = null;
                    _crossCenterControlForm.Close();
                };
            }
            if (_crossCenterControlForm == null)
            {
                _crossCenterControlForm = (CrossCenterControl)_mdiService.ShowMdi(this, typeof(CrossCenterControl), new Point(1300, 0), new Size(0, 0), new object[] { this });
                _crossCenterControlForm.FormClosed += (ss, ee) =>
                {
                    _crossCenterControlForm = null;
                };
            }
        }

        [GroupDisplay(nameof(Resource.Icon_Calibration), nameof(Resource.Icon_CameraCalibration), nameof(Resource.camera))]
        public void CameraCalibration()
        {
            if (_mdiService == null)
                _mdiService = this.GetHtaService<IMdiService>();
            
            if (_calibrationForm == null)
            {
                if (Param.CalibrationLights.Count < VisionController.Lighter.Channel)
                {
                    Param.CalibrationLights.Clear();
                    for (int i = 0; i < VisionController.Lighter.Channel; i++)
                    {
                        Param.CalibrationLights.Add(0);
                    }
                }
                VisionController.Lighter.SetLight(Param.CalibrationLights.ToArray());

                _originGains.Clear();
                _originGains.Add(VisionController.Framer.Grabbers[0].Gain);              
                VisionController.Framer.Grabbers[0].Gain = Param.CalibrationGain;
            

                _calibrationForm = (Calibration2DForm)_mdiService.ShowMdi(this, typeof(Calibration2DForm), new Point(0, 0), new Size(0, 0)
                    , new object[] { VisionController.Trigger1, (StationFramer)VisionController.Framer, VisionController.Lighter });
                _calibrationForm.FormClosed += (ss, ee) =>
                {
                    _calibrationForm = null;
                    _controlForm.Close();
                };
            }
            if (_controlForm == null)
            {
                _controlForm = (CalibrationControlForm)_mdiService.ShowMdi(this, typeof(CalibrationControlForm), new Point(1300, 0), new Size(0, 0), new object[] { this });
                _controlForm.FormClosed += (ss, ee) =>
                {
                    _controlForm = null;
                };
            }
        }

        [GroupDisplay(nameof(Resource.Icon_Calibration), nameof(Resource.Icon_LightCalibration), nameof(Resource.street_light))]
        public void LightCalibration()
        {
            if (_mdiService == null)
                _mdiService = this.GetHtaService<IMdiService>();

            if (_lightCalibation3 == null)
            {

                _lightCalibation3 = (LightCalibration3)_mdiService.ShowMdi(this, typeof(LightCalibration3), new Point(0, 0), new Size(1300, 790)
                    , new object[] { VisionController });
                _lightCalibation3.FormClosed += (ss, ee) =>
                {
                    _lightCalibation3 = null;
                    _lightControlForm?.Close();
                };
            }
            if (_lightControlForm == null)
            {
                _lightControlForm = (LightCalibControl)_mdiService.ShowMdi(this, typeof(LightCalibControl), new Point(1300, 200), new Size(0, 0), new object[] { this });
                _lightControlForm.FormClosed += (ss, ee) =>
                {
                    _lightControlForm = null;
                    TATool.GrayCard_Y1 = Param.GrayCardAY1Pos;
                    TATool.GrayCard_Z1 = Param.GrayCardBZ1Pos;
                    TATool.LightCalib_Y1 = Param.AY1PosLightCalib;
                    TATool.LightCalib_Z1 = Param.BZ1PosLightCalib;
                    TATool.LightCalib_X1 = Param.BX1PosLightCalib;
                };
            }
        }

        [GroupDisplay(nameof(Resource.Icon_Calibration), nameof(Resource.Icon_MosaicCalibration), nameof(Resource.mosaic))]
        public void MosaicCalibration()
        {
            if (_mdiService == null)
                _mdiService = this.GetHtaService<IMdiService>();

            //秀出MosaicForm在CDI上 (Form在VisionControl2.0裡)
            if (_mosaicForm == null)
            {
                //初始化光源參數數量 (與硬體光源數量相同)
                if (Param.MosaicCalibrationLights.Count < VisionController.Lighter.Channel)
                {
                    Param.MosaicCalibrationLights.Clear();
                    for (int i = 0; i < VisionController.Lighter.Channel; i++)
                    {
                        Param.MosaicCalibrationLights.Add(0);
                    }
                }

                VisionController.Lighter.SetLight(Param.MosaicCalibrationLights.ToArray());

                //秀出視窗
                _mosaicForm = (MosaicForm)_mdiService.ShowMdi(this, typeof(MosaicForm), new Point(0, 0), new Size(0, 0), new object[] { VisionController });


                var Inspargs = new InspectionParamArgs();
                GetMotorOffset?.Invoke(this, Inspargs);
                InspMotorOffsetParam = Inspargs.Param;
                PreGetTray("", false);

                double product_x = Carrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.X;
                double product_y = Carrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.Y;
                // double productCenter = Carrier.InspectData.InspectionPostion.; //LI3000
                //double productCenter = Carrier.InspectData.InspectionPostion.MosaicPos.
                //List<Point2d> productCenter = new List<Point2d>();
                //for (int i = 0; i < Carrier.InspectData.InspectionPostion.MosaicPos.Count; i++)
                //{
                //    //productCenter[i] = Carrier.InspectData.InspectionPostion.MosaicPos[i];
                //    productCenter.Add(Carrier.InspectData.InspectionPostion.MosaicPos[i]);
                //}


                //double productCenter_x = Carrier.InspectData.InspectionPostion._blockStepX[0];
                //double productCenter_y = Carrier.InspectData.InspectionPostion._blockStepY[0];

                Point2d[] ProductCenter = new Point2d[Carrier.InspectData.InspectionPostion.XMaxStepCount* Carrier.InspectData.InspectionPostion.YMaxStepCount];
                for (int index = 0; index < ProductCenter.Length; index++)
                {
                    //ProductCenter[index] = new Point2d();

                    int x_index = index / Carrier.InspectData.InspectionPostion.YMaxStepCount;
                          int y_index = index % Carrier.InspectData.InspectionPostion.YMaxStepCount;

                    ProductCenter[index]= new Point2d(Carrier.InspectData.InspectionPostion._blockStepX[x_index] + InspMotorOffsetParam.InspStandBy_X, Carrier.InspectData.InspectionPostion._blockStepY[y_index] + InspMotorOffsetParam.InspStandBy_Y);
                    //for (int j = 0; j < Carrier.InspectData.InspectionPostion.XMaxStepCount; j++)
                    //{
                    //    for (int b = 0; b < Carrier.InspectData.InspectionPostion.YMaxStepCount; b++)
                    //    {
                    //        ProductCenter[j].x = Carrier.InspectData.InspectionPostion._blockStepX[j] + InspMotorOffsetParam.InspStandBy_AX1;
                    //        ProductCenter[b].y = Carrier.InspectData.InspectionPostion._blockStepY[b] + InspMotorOffsetParam.InspStandBy_BY1;
                    //    }
                    //}
                }
               


                // 20250505 註解 待測 (Justin)


                //_mosaicForm.SetProductPosition(CurrentProductName, new HTA.Utility.Structure.Point2d(product_x, product_y),
                //    new HTA.Utility.Structure.Point2d[] { new Point2d(productCenter, 10) }, false, false, out var productCapturePoints);  //設定產品檔資訊與計算組圖位置(多產品切換)  LI3000

                //_mosaicForm.SetProductPosition(CurrentProductName, 
                //    new HTA.Utility.Structure.Point2d(product_x, product_y),
                //    new HTA.Utility.Structure.Point2d[] { new Point2d(productCenter_x + InspMotorOffsetParam.InspStandBy_AX1, productCenter_y+InspMotorOffsetParam.InspStandBy_BY1) }, 
                //    false, false, out var productCapturePoints);  //設定產品檔資訊與計算組圖位置(多產品切換) //舊

                int xCount = Carrier.InspectData.InspectionPostion._blockStepX.Length;
                int yCount = Carrier.InspectData.InspectionPostion._blockStepY.Length;
                _mosaicForm.SetProductPosition(CurrentProductName,
                        new HTA.Utility.Structure.Point2d(product_x, product_y),
                        ProductCenter,
                           false, false, new Point2d(xCount, yCount), out var productCapturePoints);  //設定產品檔資訊與計算組圖位置(多產品切換)


                //List<Point2d> point2Ds = new List<Point2d>();
                //if (productCapturePoints.Length > 0)
                //{
                //    point2Ds.AddRange(productCapturePoints[0]);
                //    NotifyMosaicPos(point2Ds);
                //}


                //加入關閉事件
                _mosaicForm.FormClosed += (ss, ee) =>
                {
                    _mosaicForm = null;
                    _mosaicControlForm.Close();
                };
            }

            //秀出MosaicControlForm
            if (_mosaicControlForm == null)
            {
                //秀出視窗
                _mosaicControlForm = (MosaicControlForm)_mdiService.ShowMdi(this, typeof(MosaicControlForm), new Point(1600, 0), new Size(0, 0), new object[] { this });
                _mosaicControlForm.SetMosaicForm(_mosaicForm);

                //加入關閉事件
                _mosaicControlForm.FormClosed += (ss, ee) =>
                {
                    _mosaicControlForm = null;
                };
            }
        }


        public void NotifyMosaicPos(List<Point2d> point2Ds)
        {
            OnNotifyMosaicPos?.Invoke(this, new MosaicPosArgs()
            {
                MosaicPos = point2Ds
            });
        }

        //TODO 待測試
        ShadeCorrectionForm _shadeCorrectionForm;
        [GroupDisplay(nameof(Resource.Icon_Calibration), nameof(Resource.Icon_ShadeCorrection), nameof(Resource.ShadeCorrection))]
        public void LightShadeCorection()
        {

            if (_mdiService == null)
                _mdiService = this.GetHtaService<IMdiService>();
            if (_shadeCorrectionForm == null)
            {
                _shadeCorrectionForm = (ShadeCorrectionForm)_mdiService.ShowMdi(this, typeof(ShadeCorrectionForm), new Point(0, 0), new Size(0, 0), new object[] { VisionController });
                _shadeCorrectionForm.FormClosed += (ss, ee) =>
                {
                    _shadeCorrectionForm = null;
                    _lightControlForm?.Close();
                };
            }
            if (_lightControlForm == null)
            {
                _lightControlForm = (LightCalibControl)_mdiService.ShowMdi(this, typeof(LightCalibControl), new Point(1300, 200), new Size(0, 0), new object[] { this });
                _lightControlForm.FormClosed += (ss, ee) =>
                {
                    _lightControlForm = null;
                };
            }
        }

        [GroupDisplay(nameof(Resource.Icon_Calibration), nameof(Resource.Icon_LaserCalibration), nameof(Resource.Laser))]
        public void LaserCenterCalibration()
        {
            if (_mdiService == null)
                _mdiService = this.GetHtaService<IMdiService>();

            if (_flowForm == null)
            {
                var hardware = VisionController.GetHardware();

                _flowForm = (FlowForm)this.GetHtaService<IMdiService>().ShowMdi(this, typeof(FlowForm), new Point(0, 0), new Size(0, 0),
                    new object[] { VisionController, hardware, true }, true);
                _flowForm.CurrentAccessLevel = 4;

                _flowForm.FormClosed += (ss, ee) =>
                {
                    _flowForm = null;
                    _laserCalibControl.Close();
                };
            }
            
            if (_laserCalibControl == null)
            {
                _laserCalibControl = (LaserCalibControl)_mdiService.ShowMdi(this, typeof(LaserCalibControl), new Point(_flowForm.Width+1, 0), new Size(0, 0), new object[] { this });
                _laserCalibControl.FormClosed += (ss, ee) =>
                {
                    _laserCalibControl = null;
                };
            }
        }

        private void 開始_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

        }

        private void 結束_ProcessIn(object sender, ControlFlow.Controls.ProcessArgs e)
        {

        }
    }
}
