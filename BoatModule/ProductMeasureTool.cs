using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using HTA.Utility.Structure;
//using HTASystemController;
//using HTA.Motion.Base;
//using Motion.Motion;
//using Base.Motion.Global;
//using HTA.LightServer;
//using Base.ProductOffset;
//using Base.Motion.Motion;
//using HTA.TriggerServer;
using ObjectDraw;
//using TrayViewObject.Container;
//using TrayViewObject.TrayMap;
using System.Runtime.CompilerServices;

using Hta.Container;
using Hta.MotionBase;
//using UA1000Module;
using static VisionController2.FlowForm.ATLaserRoundSettingFrm.ATLaserRoundForm;
using DisplayForm;
using HTA.MainController;
//using DevExpress.Mvvm.Native;
//using DevExpress.Utils.Extensions;
//using static UA1000Module.ProductMeasureToolClass;
using HTAMachine.Machine;
using HTAMachine.Setting;
//using static DevExpress.XtraEditors.TextEdit;
using TA2000Modules;
using static TA2000Modules.InspectionPostion;
using ModuleTemplate.Services;
using static ObjectDraw.ObjectDraw;
using HTA.LightServer;

namespace Base.ProductMeasureTool
{
    public partial class ProductMeasureTool : Form
    {
        private BoatModule _boatModule;
        private MainController _mainController;//紀錄相機資訊
        //暫存軸動位置
        private IAxis _AxisEventX, _AxisEventY, _AxisEventZ;

        HTA.InspectionFlow.Flow FlowHandle;

        //UAProductSettingData _UAProductSettingData;
        double Currentscale = 1;//當前縮放比例
        public event EventHandler<IModule> _SaveParam_Product;//儲存Product參數

        HTA.IFramer.StationFramer useFramer = null;
        HTA.LightServer.ILighter useLighter = null;
        HTA.TriggerServer.ITriggerChannel useTrigger = null;
        int CurrentStationFramerSource;
        //打光預設為0.0
        List<double> groupInfo = Enumerable.Repeat<double>(0.0, 16).ToList<double>();
        public ProductMeasureToolClass _ProductMeasureToolClass;


        public BoatSettingData boatData;//_ProductMeasureToolClass.Boat
        /// <summary>
        /// prodcut setting
        /// </summary>
        private DieGroupViewModel _productViewMode;

        //public BaseProductSettingData productData;
        public StripeSettingData stripeData;//_ProductMeasureToolClass.Stripe
        public Action PutProduct;
        double PixeltoMM;
        double ImageHeight;
        double ImageWidth;
        double FOV_X;
        double FOV_Y;
        double Rel_X;
        double Rel_Y;
        private string ProjectName = "";

        private bool CheckProjectIsUA => ProjectName.Equals("TA2000");
        /// <summary>
        /// 超出AxisX極限的距離Offset(因為UA軸行程限制，影像中心無法到達Boat邊緣，但相機視野可涵蓋到Boat邊緣，利用影像位置推算實際位置)
        /// </summary>
        private double outOfAxisXOffset = 0.0;
        /// <summary>
        /// 超出AxisY極限的距離Offset(因為UA軸行程限制，影像中心無法到達Boat邊緣，但相機視野可涵蓋到Boat邊緣，利用影像位置推算實際位置)
        /// </summary>
        private double outOfAxisYOffset = 0.0;
        /// <summary>
        /// 超出AxisX極限的距離Offset(顯示影像座標)
        /// </summary>
        private double outOfAxisXOffsetImage = 0.0;
        /// <summary>
        ///  超出AxisY極限的距離Offset(顯示影像座標)
        /// </summary>
        private double outOfAxisYOffsetImage = 0.0;

        //-----------------------object draw------------------------
        TrayContainer _container;
        // TrayViewObject = new TrayViewObject();
        //TrayContainerView _view;
        private TrayViewForm _trayViewForm;

        Graphics g;
        bool _formUpdating = false;
        //-----------------------object draw------------------------

        BoatLayoutSetting _BoatDesc = new BoatLayoutSetting();

        /// <summary>
        /// 是否可使用視窗移動功能
        /// </summary>
        bool _canViewMove = false;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="framer">GlobalObj使用的相機</param>
        /// <param name="lighter">GlobalObj使用的光源</param>
        /// <param name="trigger">GlobalObj使用的觸發</param>
        /// <param name="_CurrentStationFramerSource">GlobalObj使用當前站別的相機</param>
        /// <param name="boatSetting">Boat盤資料</param>
        /// <param name="productSetting">產品資料(各自專案輸入各自專案的productsetting，要記得繼承BaseProductSettingData)</param>
        /// <param name="PixelToMM">校正後得到的PixelToMM</param>
        /// <param name="AxisXEvent">X軸</param>
        /// <param name="AxisYEvent">Y軸</param>
        /// <param name="AxisZEvent">Z軸(沒有可省略)</param>
        /// <param name="stripeSetting">stripe資料(沒有可省略)</param>
        public ProductMeasureTool(
            int _CurrentStationFramerSource,
            //ProductMeasureToolClass.BoatSettingData boatSetting,
            //ProductMeasureToolClass.BaseProductSettingData productSetting,
            //double PixelToMM,
            HTAMachine.Machine.IModule module,
            //ProductMeasureToolClass.StripeSettingData stripeSetting = null,
            string InputProjectName = "")
        {
            InitializeComponent();

            try
            {
                SetupModule(module);
                _formUpdating = true;

                ProjectName = InputProjectName;
                //Initial
                boatData = _ProductMeasureToolClass.Boat;// _loadmodule.BoatParam.ProductMeasureToolInfo.UAProductSettingDataInfo.Boat;
                _productViewMode = new DieGroupViewModel(_ProductMeasureToolClass._BaseProductSettingData);
                stripeData = _ProductMeasureToolClass.Stripe;
                CurrentStationFramerSource = _CurrentStationFramerSource;





                //PixeltoMM = PixelToMM;

                //Initial





                COB_XY_Step.SelectedIndex = 0;
                COB_Z_step.SelectedIndex = 0;
                CB_DieGroup.SelectedIndex = 0;
                CB_Align.SelectedIndex = 0;
                COB_Vel.SelectedIndex = 2;
                ImgWnd.HalconWindow.SetColor("forest green");
                FlowHandle = new HTA.InspectionFlow.Flow();

                TryAddFramer();

                //-----------------------object draw------------------------
                _AxisEventX = _boatModule.視覺縱移軸;
                _AxisEventY = _boatModule.BX1_流道橫移軸;

                if (_boatModule.BZ1_流道頂升升降軸 != null)
                {
                    _AxisEventZ = _boatModule.BZ1_流道頂升升降軸;
                }
                Timer_GetPos.Start();
                //------------------------改打光初始值----------------------
                groupInfo[0] = 50 * 4.5;
                groupInfo[2] = 30 * 4.5;
                OpenLightTool();
                //-----------------------------------------------------------
                if (_productViewMode.UsingBoat_1)
                {

                    this.tabPage_boat.Parent = tabControl1;
                    this.tabPage_stripe.Parent = null;
                    this.tabPage2.Parent = null;

                    GB_LBlock_LProduct.Visible = false;
                    GB_RBlock_RProduct.Text = "Boat右上角產品";
                    GB_RBlock_LProduct.Text = "Boat左下角產品";

                    //產品檔基本資料
                    LB_BoatBlockCol.Visible = false;
                    LB_BoatBlockRow.Visible = false;
                    NUD_BoatBlockRow.Visible = false;
                    NUD_BoatBlockCol.Visible = false;


                }
                else
                {
                    this.tabPage_boat.Parent = null;
                    this.tabPage_stripe.Parent = tabControl1;

                    GB_LBlock_LProduct.Visible = true;
                    GB_BoatPos.Text = "Boat右上角產品"; // = "Stripe右上角位置"; //(PTI 反應改為Boat)

                    //產品檔基本資料
                    LB_BoatRow.Text = "一個Block裡產品Row數量";
                    LB_BoatCol.Text = "一個Block裡產品Col數量";
                    LB_BoatThickness.Text = "Stripe厚度";

                    if (_productViewMode.NoBlock)
                    {
                        GB_LBlock_LProduct.Visible = false;
                        NUD_BoatBlockRow.Enabled = false;
                        NUD_BoatBlockCol.Enabled = false;
                    }

                }
                //--------------設定初始相機增益值----------------
                ChangeCamGain(15.0);
                CamGainEdit.Text = Convert.ToString(useFramer[0].Gain);

                label34.Visible = false;
                GB_Align.Visible = false;

                //-----------------------object draw------------------------
                _container = new TrayContainer();
                if (_trayViewForm == null)
                {
                    tabPage2.Parent = tabControl1;
                    _trayViewForm = new TrayViewForm();
                    tabPage2.Controls.Add(_trayViewForm.objectDraw1);

                }


                //_trayViewForm = new TrayContainerView(_container, objectDraw1);
                //g = this.objectDraw1.CreateGraphics();
                //objectDraw1.Settings.DisplayAnchors = false;
                //objectDraw1.Settings.InverseX = true;

                DialogResult result = MessageBox.Show("軸即將移動至基準位置\nX軸位置：173 mm\nY軸位置：632 mm",
                     "Info",
                    MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                {
                    MoveAxis(_AxisEventX, 173, _AxisEventX.BaseVelocity[2]);//medium速度
                    MoveAxis(_AxisEventY, 622, _AxisEventX.BaseVelocity[2]);//medium速度

                    _canViewMove = true;
                }
                else
                {
                    MessageBox.Show("動作取消，將無法使用點選視窗移動功能");

                    _canViewMove = false;
                }
            }
            catch
            {

            }
            finally
            {
                _formUpdating = false;
                // _SaveParam_Product += _loadmodule.UA_LoadModule_SaveProductParam;
            }//



        }

        BoatCarrier _boatCarrier = null;
        public void SetupModule(HTAMachine.Machine.IModule module)
        {
            //參數進行型態轉換
            _boatModule = (BoatModule)module;
            _mainController = (MainController)_boatModule.Boat_VisionController;
            useFramer = (HTA.IFramer.StationFramer)_mainController.Framer;
            useLighter = _mainController.Lighter;
            useTrigger = _mainController.Trigger1;




            //設定軸動位置
            _AxisEventX = _boatModule.BX1_流道橫移軸;
            _AxisEventY = _boatModule.視覺縱移軸;
            _AxisEventZ = _boatModule.BZ1_流道頂升升降軸;
            ImageHeight = useFramer[0].Height;
            ImageWidth = useFramer[0].Width;

            if (useFramer[0] is HTA.IFramer.IAreaScanCalibration areaScanCalibration)
            {


                PixeltoMM = areaScanCalibration.CalibrationHandle.Pix2MMX;
                areaScanCalibration.CalibrationHandle.CalculateFOV(out double fovX, out double fovY, out double relX, out double relY);
                FOV_X = fovX;
                FOV_Y = fovY;
                Rel_X = relX;
                Rel_Y = relY;
            }

            //BoatParam.ProductMeasureToolInfo.UAProductSettingDataInfo.Boat.Width = BoatParam.TrayInfo.TrayContainerDesc.ContainerSize.X;
            //BoatParam.ProductMeasureToolInfo.UAProductSettingDataInfo.Boat.Length = BoatParam.TrayInfo.TrayContainerDesc.ContainerSize.Y;

            _BoatDesc = _boatModule.Settings;

            _ProductMeasureToolClass = _boatModule.ProductMeasureToolInfo;
            //boatData = _loadmodule.;
            //BoatSettingData
            boatData = _boatModule.ProductMeasureToolInfo.Boat;
            bool bMyProductInfo = _BoatDesc.bUseSingleIC ? true : (_BoatDesc.bUseStripe ? false : true);
            _boatModule.ProductMeasureToolInfo._BaseProductSettingData.UsingBoat_1 = bMyProductInfo;
            _boatModule.ProductMeasureToolInfo._BaseProductSettingData.NoBlock = bMyProductInfo;
            //_loadmodule._SingleICLayoutSettingForm = new SingleICLayoutSettingForm(_BoatDesc);
            //
            _boatCarrier = new BoatCarrier();
            _boatModule.OnRequestTray(this, _boatCarrier);
            ProductMeasureToolInfoSetting(_boatModule, _boatCarrier);
            int bNyFOVProductState = 0;
            if (_boatModule.Settings.bUseFOVSingleProduct) { bNyFOVProductState = 0; }
            else if (_boatModule.Settings.bUseFOVMultieProduct) { bNyFOVProductState = 2; }
            else if (_boatModule.Settings.bUseFOVMultiProductMax) { bNyFOVProductState = 1; }
            InspectionPostion inspectionPostion = new InspectionPostion(_boatCarrier, _boatModule.Settings.IsFovSingle, _boatModule.ProductName, _boatCarrier.IsCalBig,
                bMyProductInfo, bNyFOVProductState, _boatModule.Settings.bFOVProductXNum, _boatModule.Settings.bFOVProductYNum,
                _boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.X, _boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.Y);


            _SaveParam_Product += _boatModule.SaveBoatProductParam;

        }
        #region Product

        public void useSingleIC(bool value)
        {

            _BoatDesc.bUseSingleIC = value;

        }

        public void useStripe(bool value)
        {

            _BoatDesc.bUseStripe = value;

        }

        public void IsFovSingle(bool value)
        {

            _BoatDesc.IsFovSingle = value;

        }

        //public void FOV_SIZE_Num(int value)
        //{

        //    if (value < 0 || value > 54) value = 54;
        //    _BoatDesc.SetFOV = value;

        //}
        //public void FOV_X_Num(int value)
        //{

        //    _BoatDesc.SetFOV_X_Num = value;

        //}
        //public void FOV_Y_Num(int value)
        //{

        //    _BoatDesc.SetFOV_Y_Num = value;

        //}

        /// <summary>
        /// 將BoatLayout資訊帶到ProductMeasureToolForm上
        /// </summary>
        /// <param name="boatModule"></param>
        /// <param name="boatCarrier"></param>
        public void ProductMeasureToolInfoSetting(BoatModule boatModule, BoatCarrier boatCarrier)
        {
            bool bMyProductInfo = _BoatDesc.bUseSingleIC ? true : (_BoatDesc.bUseStripe ? false : true);

            if (bMyProductInfo)
            {
                boatModule.ProductMeasureToolInfo.Boat.Length = boatModule.Settings.TrayInfo.TrayContainerDesc.ContainerSize.X;
                boatModule.ProductMeasureToolInfo.Boat.Width = boatModule.Settings.TrayInfo.TrayContainerDesc.ContainerSize.Y;
                boatModule.ProductMeasureToolInfo.Boat.RowNum = boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.Y;
                boatModule.ProductMeasureToolInfo.Boat.ColNum = boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.X;
                boatModule.ProductMeasureToolInfo.Boat.PitchX = boatModule.Settings.TrayInfo.BlockContainerDesc.Pitch.X;
                boatModule.ProductMeasureToolInfo.Boat.PitchY = boatModule.Settings.TrayInfo.BlockContainerDesc.Pitch.Y;
            }
            else
            {
                boatModule.ProductMeasureToolInfo.Stripe.Boat_L = boatModule.Settings.TrayInfo.TrayContainerDesc.ContainerSize.X;
                boatModule.ProductMeasureToolInfo.Stripe.Boat_W = boatModule.Settings.TrayInfo.TrayContainerDesc.ContainerSize.Y;
                boatModule.ProductMeasureToolInfo.Stripe.Boat_Thickness = boatModule.Settings.TrayInfo.TrayContainerDesc.ContainerThickness;
                boatModule.ProductMeasureToolInfo.Stripe.ProductRowNum = boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.Y;
                boatModule.ProductMeasureToolInfo.Stripe.ProductColNum = boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.X;
            }
        }
        #endregion

        #region Block
        //Block SizeX
        public void BlockSizeX(double value)
        {

            if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize.X) < 0.001) return;
            _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize.Y);

        }

        //Block SizeY
        public void BlockSizeY(double value)
        {

            if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize.Y) < 0.001) return;
            _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize = new Point2D(_BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize.X, value);

        }

        //Block到Tray的GapX (必須定義在大容器 TrayContainerDesc)
        public void BlockFistGapX(double value)
        {

            if (Math.Abs(value - _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap.X) < 0.001) return;
            _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap.Y);

        }

        //Block到Tray的GapY (必須定義在大容器 TrayContainerDesc)
        public void BlockFistGapY(double value)
        {

            if (Math.Abs(value - _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap.Y) < 0.001) return;
            _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap = new Point2D(_BoatDesc.TrayInfo.TrayContainerDesc.FirstGap.X, value);

        }

        //Block數量X (必須定義在大容器 TrayContainerDesc)
        public void BlockDimNumX(int value)
        {

            if (_BoatDesc.TrayInfo.TrayContainerDesc.SubDimSize.X == value)
                return;
            _BoatDesc.TrayInfo.TrayContainerDesc.SubDimSize = new Point(value, _BoatDesc.TrayInfo.TrayContainerDesc.SubDimSize.Y);

        }

        //Block數量Y (必須定義在大容器 TrayContainerDesc)
        public void BlockDimNumY(int value)
        {

            if (_BoatDesc.TrayInfo.TrayContainerDesc.SubDimSize.Y == value)
                return;
            _BoatDesc.TrayInfo.TrayContainerDesc.SubDimSize = new Point(_BoatDesc.TrayInfo.TrayContainerDesc.SubDimSize.X, value);

        }
        #endregion

        #region Tray
        //Tray 尺寸X(mm) (TrayDesc.TrayContainerDesc 定義 Tray)
        public void TraySizeX(double value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                if (Math.Abs(value - _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.X) < 0.001) return;
                _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.Y);
                _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.Y);
            }
            else if (_BoatDesc.bUseStripe)
            {
                if (Math.Abs(value - _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.X) < 0.001) return;
                _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.Y);
            }

        }

        //Tray尺寸Y(mm) (TrayDesc.TrayContainerDesc定義 Tray)
        public void TraySizeY(double value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                if (Math.Abs(value - _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.Y) < 0.001) return;
                _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize = new Point2D(_BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.X, Math.Round(value, 2, MidpointRounding.AwayFromZero));
                _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize = new Point2D(_BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.X, Math.Round(value, 2, MidpointRounding.AwayFromZero));
            }
            else if (_BoatDesc.bUseStripe)
            {
                if (Math.Abs(value - _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.Y) < 0.001) return;
                _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize = new Point2D(_BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize.X, Math.Round(value, 2, MidpointRounding.AwayFromZero));
            }

        }
        public void SetBlockSizeXY(double BlockSizeX, double BlockSizeY)
        {
            //if (Math.Abs(BlockSizeX - _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize.X) < 0.001) return;
            //if (Math.Abs(BlockSizeY - _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize.Y) < 0.001) return;
            _BoatDesc.TrayInfo.BlockContainerDesc.ContainerSize = new Point2D(BlockSizeX, BlockSizeY);

        }
        //Block到Tray的GapX (必須定義在大容器 TrayContainerDesc)
        public void BlockFistGapXY(double valueX, double valueY)
        {

            //if (Math.Abs(valueX - _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap.X) < 0.001) return;
            //if (Math.Abs(valueY - _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap.Y) < 0.001) return;
            _BoatDesc.TrayInfo.TrayContainerDesc.FirstGap = new Point2D(valueX, valueY);

        }


        //第一顆產品邊界到Block邊界距離X(mm) (TrayDesc.BlockContainerDesc來定義Block)
        public void IcFirstGapX(double value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.X) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.Y);
            }
            else if (_BoatDesc.bUseStripe)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.X) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.Y);
            }

        }

        //第一顆產品邊界到Block邊界距離Y(mm) (TrayDesc.BlockContainerDesc來定義Block)
        public void IcFirstGapY(double value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.Y) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap = new Point2D(_BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.X, Math.Round(value, 2, MidpointRounding.AwayFromZero));
            }
            else if (_BoatDesc.bUseStripe)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.Y) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.FirstGap = new Point2D(_BoatDesc.TrayInfo.BlockContainerDesc.FirstGap.X, Math.Round(value, 2, MidpointRounding.AwayFromZero));
            }

        }

        //Block裡產品數量X (TrayDesc.BlockContainerDesc來定義Block)
        public void IcDimX(int value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.X) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize = new Point(value, _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.Y);
            }
            else if (_BoatDesc.bUseStripe)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.X) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize = new Point(value, _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.Y);
            }

        }

        //Block裡產品數量Y (TrayDesc.BlockContainerDesc來定義Block)
        public void IcDimY(int value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.Y) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize = new Point(_BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.X, value);
            }
            else if (_BoatDesc.bUseStripe)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.Y) < 0.001) return;
                _BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize = new Point(_BoatDesc.TrayInfo.BlockContainerDesc.SubDimSize.X, value);
            }

        }
        #endregion

        #region IC 
        //產品數量尺寸X (TrayDesc.IcDesc來定義產品)
        public void IcSizeX(double value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                //if (_BoatDesc.TrayInfo.IcDesc.ContainerSize.X == value) return;
                _BoatDesc.TrayInfo.IcDesc.ContainerSize = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.IcDesc.ContainerSize.Y);
            }
            else if (_BoatDesc.bUseStripe)
            {
                //if (_BoatDesc.TrayInfo.IcDesc.ContainerSize.X == value) return;
                _BoatDesc.TrayInfo.IcDesc.ContainerSize = new Point2D(Math.Round(value, 2, MidpointRounding.AwayFromZero), _BoatDesc.TrayInfo.IcDesc.ContainerSize.Y);
            }

        }

        //產品數量尺寸Y(TrayDesc.IcDesc來定義產品)
        public void IcSizeY(double value)
        {

            if (_BoatDesc.bUseSingleIC)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.IcDesc.ContainerSize.Y) < 0.001) return;
                _BoatDesc.TrayInfo.IcDesc.ContainerSize = new Point2D(_BoatDesc.TrayInfo.IcDesc.ContainerSize.X, Math.Round(value, 2, MidpointRounding.AwayFromZero));
            }
            else if (_BoatDesc.bUseStripe)
            {
                //if (Math.Abs(value - _BoatDesc.TrayInfo.IcDesc.ContainerSize.Y) < 0.001) return;
                _BoatDesc.TrayInfo.IcDesc.ContainerSize = new Point2D(_BoatDesc.TrayInfo.IcDesc.ContainerSize.X, Math.Round(value, 2, MidpointRounding.AwayFromZero));
            }

        }
        #endregion

        public class DieGroupViewModel : INotifyPropertyChanged
        {
            private readonly BaseProductSettingData _setting;


            public DieGroupViewModel(BaseProductSettingData setting)
            {
                this._setting = setting;
                this.SetDieGroupSize(6);
            }


            public bool UsingBoat_1 => _setting.UsingBoat_1;

            public bool NoBlock => _setting.NoBlock;


            #region ProductCenter
            double ProductCenterX => 0.5 * (RightUpBlock_RightUpProduct_RightUpCorner_X + RightUpBlock_RightUpProduct_LeftDownCorner_X);


            double ProductCenterY => 0.5 * (RightUpBlock_RightUpProduct_RightUpCorner_Y + RightUpBlock_RightUpProduct_LeftDownCorner_Y);
            #endregion

            #region Select Die Group


            private int _selectGroupIndex = -1;
            public int SelectGroup
            {
                get => _selectGroupIndex;
                set
                {
                    if (value == _selectGroupIndex) return;
                    _selectGroupIndex = value;
                    OnPropertyChanged(nameof(SelectGroup));
                    OnPropertyChanged(nameof(DieGroupIgnore));
                    OnPropertyChanged(nameof(DieGroupX));
                    OnPropertyChanged(nameof(DieGroupY));
                }
            }
            public bool DieGroupIgnore
            {
                get
                {
                    if (_selectGroupIndex == -1) return false;
                    if (_selectGroupIndex >= _setting.DieGroup_Ignore.Length) return false;
                    return _setting.DieGroup_Ignore[_selectGroupIndex];
                }
                set
                {
                    if (_selectGroupIndex == -1) return;
                    if (_selectGroupIndex >= _setting.DieGroup_Ignore.Length) return;
                    //cant ignore first die group
                    if (_selectGroupIndex == 0 && value == true) return;
                    _setting.DieGroup_Ignore[_selectGroupIndex] = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DieGroupIgnoreInvert));
                }
            }
            public bool DieGroupIgnoreInvert => !DieGroupIgnore;
            public double DieGroupX
            {
                get
                {
                    if (_selectGroupIndex == -1) return 0.0;
                    if (_selectGroupIndex >= _setting.DieGroup_CX.Length) return 0.0;
                    return ProductCenterX + _setting.DieGroup_CX[_selectGroupIndex];
                }
                set
                {
                    if (_selectGroupIndex == -1) return;
                    if (_selectGroupIndex >= _setting.DieGroup_CX.Length) return;
                    _setting.DieGroup_CX[_selectGroupIndex] = value - ProductCenterX;
                    OnPropertyChanged();
                }
            }
            public double DieGroupY
            {
                get
                {
                    if (_selectGroupIndex == -1) return 0.0;
                    if (_selectGroupIndex >= _setting.DieGroup_CY.Length) return 0.0;
                    return ProductCenterY + _setting.DieGroup_CY[_selectGroupIndex];
                }
                set
                {
                    if (_selectGroupIndex == -1) return;
                    if (_selectGroupIndex >= _setting.DieGroup_CY.Length) return;
                    _setting.DieGroup_CY[_selectGroupIndex] = value - ProductCenterY;
                    OnPropertyChanged();
                }
            }




            #endregion


            #region Select Die Group


            private int _selectAlignIndex = -1;
            public int SelectAlign
            {
                get => _selectAlignIndex;
                set
                {
                    if (value == _selectAlignIndex) return;
                    _selectAlignIndex = value;
                    OnPropertyChanged(nameof(SelectAlign));
                    OnPropertyChanged(nameof(AlignName));
                    OnPropertyChanged(nameof(AlignIgnore));
                    OnPropertyChanged(nameof(AlignX));
                    OnPropertyChanged(nameof(AlignY));

                }
            }

            public bool AlignIgnore
            {
                get
                {
                    if (_selectAlignIndex == -1) return false;
                    if (_selectAlignIndex >= _setting.Align_Ignore.Length) return false;
                    return _setting.DieGroup_Ignore[_selectAlignIndex];
                }
                set
                {
                    if (_selectAlignIndex == -1) return;
                    if (_selectAlignIndex >= _setting.Align_Ignore.Length) return;
                    //cant ignore first die group
                    if (_selectAlignIndex == 0 && value == false) return;
                    _setting.Align_Ignore[_selectAlignIndex] = value;
                }
            }
            public double AlignX
            {
                get
                {
                    if (_selectAlignIndex == -1) return 0.0;
                    if (_selectAlignIndex >= _setting.Align_CX.Length) return 0.0;
                    return ProductCenterX + _setting.Align_CX[_selectAlignIndex];
                }
                set
                {
                    if (_selectAlignIndex == -1) return;
                    if (_selectAlignIndex >= _setting.Align_CX.Length) return;
                    _setting.Align_CX[_selectAlignIndex] = value - ProductCenterX;
                }
            }
            public double AlignY
            {
                get
                {
                    if (_selectAlignIndex == -1) return 0.0;
                    if (_selectAlignIndex >= _setting.Align_CY.Length) return 0.0;
                    return ProductCenterY + _setting.Align_CY[_selectAlignIndex];
                }
                set
                {
                    if (_selectAlignIndex == -1) return;
                    if (_selectAlignIndex >= _setting.Align_CY.Length) return;
                    _setting.Align_CY[_selectAlignIndex] = value - ProductCenterY;
                }
            }

            public string AlignName
            {
                get
                {
                    switch (_selectAlignIndex)
                    {
                        case 0:
                            return "右上定位點Index:";
                        case 1:
                            return "右下定位點Index:";
                        case 2:
                            return "左下定位點Index:";
                        case 3:
                            return "左上定位點Index:";
                    }
                    return "";
                }
            }

            #endregion




            #region Properties
            public double BoatOriginPosition_X
            {
                get => _setting.BoatOriginPosition_X;
                set
                {
                    if (value == BoatOriginPosition_X) return;
                    _setting.BoatOriginPosition_X = value;
                    OnPropertyChanged(nameof(BoatOriginPosition_X));
                }
            }
            public double BoatOriginPosition_Y
            {
                get => _setting.BoatOriginPosition_Y;
                set
                {
                    if (value == BoatOriginPosition_Y) return;
                    _setting.BoatOriginPosition_Y = value;
                    OnPropertyChanged(nameof(BoatOriginPosition_Y));
                }
            }


            public double RightUpBlock_RightUpProduct_RightUpCorner_X
            {
                get => _setting.RightUpBlock_RightUpProduct_RightUpCorner_X;
                set
                {
                    if (value == RightUpBlock_RightUpProduct_RightUpCorner_X) return;
                    _setting.RightUpBlock_RightUpProduct_RightUpCorner_X = value;
                    OnPropertyChanged(nameof(RightUpBlock_RightUpProduct_RightUpCorner_X));
                }
            }
            public double RightUpBlock_RightUpProduct_RightUpCorner_Y
            {
                get => _setting.RightUpBlock_RightUpProduct_RightUpCorner_Y;
                set
                {
                    if (value == RightUpBlock_RightUpProduct_RightUpCorner_Y) return;
                    _setting.RightUpBlock_RightUpProduct_RightUpCorner_Y = value;
                    OnPropertyChanged(nameof(RightUpBlock_RightUpProduct_RightUpCorner_Y));
                }
            }

            public double RightUpBlock_LeftDownProduct_LeftDownCorner_X
            {
                get => _setting.RightUpBlock_LeftDownProduct_LeftDownCorner_X;
                set
                {
                    if (value == RightUpBlock_LeftDownProduct_LeftDownCorner_X) return;
                    _setting.RightUpBlock_LeftDownProduct_LeftDownCorner_X = value;
                    OnPropertyChanged(nameof(RightUpBlock_LeftDownProduct_LeftDownCorner_X));
                }
            }
            public double RightUpBlock_LeftDownProduct_LeftDownCorner_Y
            {
                get => _setting.RightUpBlock_LeftDownProduct_LeftDownCorner_Y;
                set
                {
                    if (value == RightUpBlock_LeftDownProduct_LeftDownCorner_Y) return;
                    _setting.RightUpBlock_LeftDownProduct_LeftDownCorner_Y = value;
                    OnPropertyChanged(nameof(RightUpBlock_LeftDownProduct_LeftDownCorner_Y));
                }
            }

            public double RightUpBlock_LeftDownProduct_RightUpCorner_X
            {
                get => _setting.RightUpBlock_LeftDownProduct_RightUpCorner_X;
                set
                {
                    if (value == RightUpBlock_LeftDownProduct_RightUpCorner_X) return;
                    _setting.RightUpBlock_LeftDownProduct_RightUpCorner_X = value;
                    OnPropertyChanged(nameof(RightUpBlock_LeftDownProduct_RightUpCorner_X));
                }
            }
            public double RightUpBlock_LeftDownProduct_RightUpCorner_Y
            {
                get => _setting.RightUpBlock_LeftDownProduct_RightUpCorner_Y;
                set
                {
                    if (value == RightUpBlock_LeftDownProduct_RightUpCorner_Y) return;
                    _setting.RightUpBlock_LeftDownProduct_RightUpCorner_Y = value;
                    OnPropertyChanged(nameof(RightUpBlock_LeftDownProduct_RightUpCorner_Y));
                }
            }

            public double LeftDownBlock_LeftDownProduct_LeftDownCorner_X
            {
                get => _setting.LeftDownBlock_LeftDownProduct_LeftDownCorner_X;
                set
                {
                    if (value == LeftDownBlock_LeftDownProduct_LeftDownCorner_X) return;
                    _setting.LeftDownBlock_LeftDownProduct_LeftDownCorner_X = value;
                    OnPropertyChanged(nameof(LeftDownBlock_LeftDownProduct_LeftDownCorner_X));
                }
            }
            public double LeftDownBlock_LeftDownProduct_LeftDownCorner_Y
            {
                get => _setting.LeftDownBlock_LeftDownProduct_LeftDownCorner_Y;
                set
                {
                    if (value == LeftDownBlock_LeftDownProduct_LeftDownCorner_Y) return;
                    _setting.LeftDownBlock_LeftDownProduct_LeftDownCorner_Y = value;
                    OnPropertyChanged(nameof(LeftDownBlock_LeftDownProduct_LeftDownCorner_Y));
                }
            }

            public double LeftDownBlock_LeftDownProduct_RightUpCorner_X
            {
                get => _setting.LeftDownBlock_LeftDownProduct_RightUpCorner_X;
                set
                {
                    if (value == LeftDownBlock_LeftDownProduct_RightUpCorner_X) return;
                    _setting.LeftDownBlock_LeftDownProduct_RightUpCorner_X = value;
                    OnPropertyChanged(nameof(LeftDownBlock_LeftDownProduct_RightUpCorner_X));
                }
            }
            public double LeftDownBlock_LeftDownProduct_RightUpCorner_Y
            {
                get => _setting.LeftDownBlock_LeftDownProduct_RightUpCorner_Y;
                set
                {
                    if (value == LeftDownBlock_LeftDownProduct_RightUpCorner_Y) return;
                    _setting.LeftDownBlock_LeftDownProduct_RightUpCorner_Y = value;
                    OnPropertyChanged(nameof(LeftDownBlock_LeftDownProduct_RightUpCorner_Y));
                }
            }

            public double RightUpBlock_RightUpProduct_LeftDownCorner_X
            {
                get => _setting.RightUpBlock_RightUpProduct_LeftDownCorner_X;
                set
                {
                    if (value == RightUpBlock_RightUpProduct_LeftDownCorner_X) return;
                    _setting.RightUpBlock_RightUpProduct_LeftDownCorner_X = value;
                    OnPropertyChanged(nameof(RightUpBlock_RightUpProduct_LeftDownCorner_X));
                }
            }

            public double RightUpBlock_RightUpProduct_LeftDownCorner_Y
            {
                get => _setting.RightUpBlock_RightUpProduct_LeftDownCorner_Y;
                set
                {
                    if (value == RightUpBlock_RightUpProduct_LeftDownCorner_Y) return;
                    _setting.RightUpBlock_RightUpProduct_LeftDownCorner_Y = value;
                    OnPropertyChanged(nameof(RightUpBlock_RightUpProduct_LeftDownCorner_Y));
                }
            }

            public double RightUpBlock_RightUpCorner_X
            {
                get => _setting.RightUpBlock_RightUpCorner_X;
                set
                {
                    if (value == RightUpBlock_RightUpCorner_X) return;
                    _setting.RightUpBlock_RightUpCorner_X = value;
                    OnPropertyChanged(nameof(RightUpBlock_RightUpCorner_X));
                }
            }
            public double RightUpBlock_RightUpCorner_Y
            {
                get => _setting.RightUpBlock_RightUpCorner_Y;
                set
                {
                    if (value == RightUpBlock_RightUpCorner_Y) return;
                    _setting.RightUpBlock_RightUpCorner_Y = value;
                    OnPropertyChanged(nameof(RightUpBlock_RightUpCorner_Y));
                }
            }
            public double RightUpBlock_LeftDownCorner_X
            {
                get => _setting.RightUpBlock_LeftDownCorner_X;
                set
                {
                    if (value == RightUpBlock_LeftDownCorner_X) return;
                    _setting.RightUpBlock_LeftDownCorner_X = value;
                    OnPropertyChanged(nameof(RightUpBlock_LeftDownCorner_X));
                }
            }
            public double RightUpBlock_LeftDownCorner_Y
            {
                get => _setting.RightUpBlock_LeftDownCorner_Y;
                set
                {
                    if (value == RightUpBlock_LeftDownCorner_Y) return;
                    _setting.RightUpBlock_LeftDownCorner_Y = value;
                    OnPropertyChanged(nameof(RightUpBlock_LeftDownCorner_Y));
                }
            }

            public double RightUpBlock_RightUpProduct_Center_X => Math.Abs(RightUpBlock_RightUpProduct_LeftDownCorner_X +
                                                            RightUpBlock_RightUpProduct_RightUpCorner_X) / 2;

            public double RightUpBlock_RightUpProduct_Center_Y => Math.Abs(RightUpBlock_RightUpProduct_LeftDownCorner_Y +
                                                            RightUpBlock_RightUpProduct_RightUpCorner_Y) / 2;

            public double BarcodePosX
            {
                get => _setting.ProductBarcode_X + RightUpBlock_RightUpProduct_Center_X;
                set
                {
                    _setting.ProductBarcode_X = value - RightUpBlock_RightUpProduct_Center_X;
                }
            }
            public double BarcodePosY
            {
                get => _setting.ProductBarcode_Y + RightUpBlock_RightUpProduct_Center_Y;
                set
                {
                    _setting.ProductBarcode_Y = value - RightUpBlock_RightUpProduct_Center_Y;
                }
            }

            public double Boat1_Thickness
            {
                get => _setting.Boat1_Thickness;
                set
                {
                    if (value == Boat1_Thickness) return;
                    _setting.Boat1_Thickness = value;
                    OnPropertyChanged(nameof(Boat1_Thickness));
                }
            }

            public int BlockRowNum
            {
                get => _setting.BlockRowNum;
                set
                {
                    if (value == BlockRowNum) return;
                    _setting.BlockRowNum = value;
                    OnPropertyChanged(nameof(BlockRowNum));
                }
            }
            public int BlockColNum
            {
                get => _setting.BlockColNum;
                set
                {
                    if (value == BlockColNum) return;
                    _setting.BlockColNum = value;
                    OnPropertyChanged(nameof(BlockColNum));
                }
            }

            public double P_L => _setting.P_L;

            public double P_W => _setting.P_W;

            #endregion


            #region property change
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string name = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            internal void CalcProductSize()
            {
                _setting.P_L = Math.Abs(_setting.RightUpBlock_RightUpProduct_LeftDownCorner_X -
                                                                     _setting.RightUpBlock_RightUpProduct_RightUpCorner_X);
                _setting.P_W = Math.Abs(_setting.RightUpBlock_RightUpProduct_LeftDownCorner_Y -
                                                                     _setting.RightUpBlock_RightUpProduct_RightUpCorner_Y);

                //UA使用
                _setting.IC_Width = _setting.P_W;
                _setting.IC_Lenght = _setting.P_L;
            }

            internal int DieGroupCount()
            {
                return _setting.DieGroup_Ignore.Length;
            }

            internal void SetDieGroupSize(int newLength)
            {
                if (_setting.DieGroup_Ignore.Length != newLength)
                {
                    var ori = _setting.DieGroup_Ignore;
                    _setting.DieGroup_Ignore = new bool[] { false, true, true, true, true, true };
                    var copyLen = Math.Min(ori.Length, _setting.DieGroup_Ignore.Length);
                    for (int i = 0; i < copyLen; i++)
                        _setting.DieGroup_Ignore[i] = ori[i];
                }
                if (_setting.DieGroup_CX.Length != newLength)
                {
                    var ori = _setting.DieGroup_CX;
                    _setting.DieGroup_CX = new double[newLength];
                    var copyLen = Math.Min(ori.Length, _setting.DieGroup_Ignore.Length);
                    for (int i = 0; i < copyLen; i++)
                        _setting.DieGroup_CX[i] = ori[i];
                }
                if (_setting.DieGroup_CY.Length != newLength)
                {
                    var ori = _setting.DieGroup_CY;
                    _setting.DieGroup_CY = new double[newLength];
                    var copyLen = Math.Min(ori.Length, _setting.DieGroup_Ignore.Length);
                    for (int i = 0; i < copyLen; i++)
                        _setting.DieGroup_CY[i] = ori[i];
                }

            }

            internal void SetOriginalPose(double x, double y)
            {
                _setting.BoatOriginPosition_X = x;
                _setting.BoatOriginPosition_Y = y;
            }


            #endregion
        }

        private void Timer_GetPos_Tick(object sender, EventArgs e)
        {
            //if (FActionAxis == null)
            //{
            //    TE_AX1_pos.Text = "0";
            //    TE_BY1_pos.Text = "0";
            //}
            //else
            //{
            TE_AX1_pos.Text = _AxisEventX.ActualPos.ToString("#0.00");
            TE_BY1_pos.Text = _AxisEventY.ActualPos.ToString("#0.00");
            TB_AZ1_Pos.Text = _AxisEventZ.ActualPos.ToString("#0.00");
            //}
        }

        #region 移動位置XYZ

        private double GetXYStep()
        {
            return Convert.ToDouble(COB_XY_Step.SelectedItem);
        }

        /// <summary>
        /// 影像向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_BY1_Forward_Click(object sender, EventArgs e)
        {
            outOfAxisXOffsetImage = 0;
            outOfAxisYOffsetImage = 0;
            double dist_BY1 = _AxisEventY.ActualPos - GetXYStep();

            if (AxisInterferenceUA(_AxisEventX.ActualPos, dist_BY1))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            bool axisYmoveOutRange = false;

            if (!CheckAxisMoveDist(_AxisEventY, dist_BY1, out double axisYLimitDist, out double outOfDistY))
            {
                if (axisYLimitDist < dist_BY1)
                    dist_BY1 = axisYLimitDist - 1;
                if (axisYLimitDist > dist_BY1)
                    dist_BY1 = axisYLimitDist + 1;
                axisYmoveOutRange = true;
            }

            bool movssuccess = MoveAxis(_AxisEventY, dist_BY1, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);

            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      dist_BY1,
            //                      0);

            if (axisYmoveOutRange)
                MessageBox.Show("Y軸已接近極限");

            if (!movssuccess)
                MessageBox.Show("移動失敗，超出移動範圍");
        }

        /// <summary>
        /// 影像向上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_BY1_Back_Click(object sender, EventArgs e)
        {
            outOfAxisXOffset = 0;
            outOfAxisYOffset = 0;
            outOfAxisXOffsetImage = 0;
            outOfAxisYOffsetImage = 0;

            double dist_BY1 = _AxisEventY.ActualPos + GetXYStep();
            bool axisYmoveOutRange = false;

            if (AxisInterferenceUA(_AxisEventX.ActualPos, dist_BY1))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            if (!CheckAxisMoveDist(_AxisEventY, dist_BY1, out double axisYLimitDist, out double outOfDistY))
            {
                if (axisYLimitDist < dist_BY1)
                    dist_BY1 = axisYLimitDist - 1;
                if (axisYLimitDist > dist_BY1)
                    dist_BY1 = axisYLimitDist + 1;
                axisYmoveOutRange = true;
            }
            bool movssuccess = MoveAxis(_AxisEventY, dist_BY1, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      dist_BY1,
            //                      0);
            if (axisYmoveOutRange)
                MessageBox.Show("Y軸已接近極限");

            if (!movssuccess)
                MessageBox.Show("移動失敗，超出移動範圍");
        }

        private void Btn_AX1_Foraward_Click(object sender, EventArgs e)
        {
            outOfAxisXOffsetImage = 0;
            outOfAxisYOffsetImage = 0;
            double dist_AX1 = _AxisEventX.ActualPos - GetXYStep();

            if (AxisInterferenceUA(dist_AX1, _AxisEventY.ActualPos))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            bool axisXmoveOutRange = false;

            if (!CheckAxisMoveDist(_AxisEventY, dist_AX1, out double axisXLimitDist, out _))
            {
                if (axisXLimitDist < dist_AX1)
                    dist_AX1 = axisXLimitDist - 1;
                if (axisXLimitDist > dist_AX1)
                    dist_AX1 = axisXLimitDist + 1;
                axisXmoveOutRange = true;
            }
            bool movssuccess = MoveAxis(_AxisEventX, dist_AX1, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      dist_AX1,
            //                      0);

            if (axisXmoveOutRange)
                MessageBox.Show("X軸已接近極限");

            if (!movssuccess)
                MessageBox.Show("移動失敗，超出移動範圍");
        }

        private void Btn_AX1_Back_Click(object sender, EventArgs e)
        {
            double dist_AX1 = _AxisEventX.ActualPos + GetXYStep();

            if (AxisInterferenceUA(dist_AX1, _AxisEventY.ActualPos))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            bool axisXmoveOutRange = false;
            outOfAxisXOffsetImage = 0;
            outOfAxisYOffsetImage = 0;

            if (!CheckAxisMoveDist(_AxisEventY, dist_AX1, out double axisXLimitDist, out _))
            {
                if (axisXLimitDist < dist_AX1)
                    dist_AX1 = axisXLimitDist - 1;
                if (axisXLimitDist > dist_AX1)
                    dist_AX1 = axisXLimitDist + 1;
                axisXmoveOutRange = true;
            }
            bool movssuccess = MoveAxis(_AxisEventX, dist_AX1, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                     _AxisEventX,
            //                    _AxisEventX.AxisNo,
            //                    dist_AX1,
            //                    0);

            if (axisXmoveOutRange)
                MessageBox.Show("X軸已接近極限");

            if (!movssuccess)
                MessageBox.Show("移動失敗，超出移動範圍");
        }
        private void Btn_MoveZBackward_Click(object sender, EventArgs e)
        {
            double Z_movedist = 0;

            Z_movedist = _AxisEventZ.ActualPos - Convert.ToDouble(COB_Z_step.SelectedItem);
            bool movssuccess = MoveAxis(_AxisEventZ, Z_movedist, _AxisEventZ.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //    _AxisEventZ,
            //    _AxisEventZ.AxisNo,
            //    Z_movedist,
            //    0);
            if (!movssuccess)
                MessageBox.Show("移動失敗，超出移動範圍");
        }

        private void Btn_MoveZForward_Click(object sender, EventArgs e)
        {
            double Z_movedist = 0;
            Z_movedist = _AxisEventZ.ActualPos + Convert.ToDouble(COB_Z_step.SelectedItem);
            bool movssuccess = MoveAxis(_AxisEventZ, Z_movedist, _AxisEventZ.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //    _AxisEventZ,
            //    _AxisEventZ.AxisNo,
            //    Z_movedist,
            //    0);

            if (!movssuccess)
                MessageBox.Show("移動失敗，超出移動範圍");
        }

        private double GetAxisXTotalPosition()
        {
            if (outOfAxisXOffset != 0)
                return outOfAxisXOffset;

            return Convert.ToDouble(TE_AX1_pos.Text);
        }

        private double GetAxisYTotalPosition()
        {
            if (outOfAxisYOffset != 0)
                return outOfAxisYOffset;

            return Convert.ToDouble(TE_BY1_pos.Text);
        }

        #endregion


        private void ShowData()
        {
            try
            {
                //------------------------------------
                // original pose
                TB_BoatOriginX.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.BoatOriginPosition_X),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_BoatOriginY.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.BoatOriginPosition_Y),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                //------------------------------------
                // each block start and end position
                TB_ProductLeftDownPos1X.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_X),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_ProductLeftDownPos1Y.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_Y),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_ProductRightUpPos1X.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_X),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_ProductRightUpPos1Y.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_Y),
                    true, DataSourceUpdateMode.OnPropertyChanged);

                TB_ProductLeftDownPos2X.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_X),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_ProductLeftDownPos2Y.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_Y),
                    true, DataSourceUpdateMode.OnPropertyChanged);

                TB_ProductRightUpPos2X.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_X),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_ProductRightUpPos2Y.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_Y),
                    true, DataSourceUpdateMode.OnPropertyChanged);

                TB_ProductLeftDownPos3X.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_X),
                   true, DataSourceUpdateMode.OnPropertyChanged);
                TB_ProductLeftDownPos3Y.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_Y),
                    true, DataSourceUpdateMode.OnPropertyChanged);

                TB_ProductRightUpPos3X.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_X),
                   true, DataSourceUpdateMode.OnPropertyChanged);
                TB_ProductRightUpPos3Y.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_Y),
                    true, DataSourceUpdateMode.OnPropertyChanged);


                TB_BlockRightUpPosX.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_RightUpCorner_X),
                  true, DataSourceUpdateMode.OnPropertyChanged);
                TB_BlockRightUpPosY.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_RightUpCorner_Y),
                  true, DataSourceUpdateMode.OnPropertyChanged);
                TB_BlockRLeftDownPosX.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_LeftDownCorner_X),
                  true, DataSourceUpdateMode.OnPropertyChanged);
                TB_BlockRLeftDownPosY.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.RightUpBlock_LeftDownCorner_Y),
                  true, DataSourceUpdateMode.OnPropertyChanged);


                TB_BarcodeX.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.BarcodePosX),
                   true, DataSourceUpdateMode.OnPropertyChanged);
                TB_BarcodeY.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.BarcodePosY),
                    true, DataSourceUpdateMode.OnPropertyChanged);

                //------------------------------------
                // boat setting





                if (_productViewMode.UsingBoat_1 == true)
                {
                    TB_BoatL.Text = boatData.Length.ToString();
                    TB_BoatW.Text = boatData.Width.ToString();
                    NUD_BoatRow.Value = boatData.RowNum;
                    NUD_BoatCol.Value = boatData.ColNum;
                    TB_BoatThickness.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.Boat1_Thickness),
                           true, DataSourceUpdateMode.OnPropertyChanged);
                    TB_ProductThickness.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.Boat1_Thickness),
                           true, DataSourceUpdateMode.OnPropertyChanged);
                }
                else
                {
                    TB_BoatL.Text = stripeData.Boat_L.ToString();
                    TB_BoatW.Text = stripeData.Boat_W.ToString();
                    NUD_BoatRow.Value = stripeData.ProductRowNum;
                    NUD_BoatCol.Value = stripeData.ProductColNum;

                    NUD_BoatBlockRow.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.BlockRowNum),
                       true, DataSourceUpdateMode.OnPropertyChanged);
                    NUD_BoatBlockCol.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.BlockColNum),
                           true, DataSourceUpdateMode.OnPropertyChanged);

                    TB_BoatThickness.Text = stripeData.Boat_Thickness.ToString();
                    TB_ProductThickness.DataBindings.Add("Value", _productViewMode, nameof(_productViewMode.Boat1_Thickness),
                           true, DataSourceUpdateMode.OnPropertyChanged);

                }

                if (TB_BoatL.Text == "0")
                {
                    TB_BoatL.Text = 322.6.ToString();
                }

                if (TB_BoatW.Text == "0")
                {
                    TB_BoatW.Text = 137.ToString();
                }


                //---------------------------
                // binding group data
                CB_DieGroup.DataBindings.Add(nameof(CB_DieGroup.SelectedIndex), _productViewMode, nameof(_productViewMode.SelectGroup),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                CB_Ignore.DataBindings.Add(nameof(CB_Ignore.Checked), _productViewMode, nameof(_productViewMode.DieGroupIgnore),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_DieGroupX.DataBindings.Add(nameof(TB_DieGroupX.Value), _productViewMode, nameof(_productViewMode.DieGroupX),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_DieGroupY.DataBindings.Add(nameof(TB_DieGroupY.Value), _productViewMode, nameof(_productViewMode.DieGroupY),
                    true, DataSourceUpdateMode.OnPropertyChanged);

                //binding visible
                TB_DieGroupX.DataBindings.Add(nameof(TB_DieGroupX.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_DieGroupY.DataBindings.Add(nameof(TB_DieGroupY.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                LB_DieGroupX.DataBindings.Add(nameof(LB_DieGroupX.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                LB_DieGroupY.DataBindings.Add(nameof(LB_DieGroupY.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                Btn_DieGroupSetPos.DataBindings.Add(nameof(Btn_DieGroupSetPos.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                Btn_DieGroupGo.DataBindings.Add(nameof(Btn_DieGroupGo.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);


                //---------------------------
                // binding align data
                CB_Align.DataBindings.Add(nameof(CB_Align.SelectedIndex), _productViewMode, nameof(_productViewMode.SelectAlign),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                CB_IgnoreAlign.DataBindings.Add(nameof(CB_IgnoreAlign.Checked), _productViewMode, nameof(_productViewMode.AlignIgnore),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_AlignX.DataBindings.Add(nameof(TB_AlignX.Value), _productViewMode, nameof(_productViewMode.AlignX),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_AlignY.DataBindings.Add(nameof(TB_AlignY.Value), _productViewMode, nameof(_productViewMode.AlignY),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                //binding visible
                TB_AlignX.DataBindings.Add(nameof(TB_AlignX.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                TB_AlignY.DataBindings.Add(nameof(TB_AlignY.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                LB_AlignX.DataBindings.Add(nameof(LB_AlignX.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                LB_AlignY.DataBindings.Add(nameof(LB_AlignY.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                Btn_AlignSetPos.DataBindings.Add(nameof(Btn_AlignSetPos.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                Btn_AlignGo.DataBindings.Add(nameof(Btn_AlignGo.Enabled), _productViewMode, nameof(_productViewMode.DieGroupIgnoreInvert),
                    true, DataSourceUpdateMode.OnPropertyChanged);
                LB_Align.DataBindings.Add(nameof(LB_Align.Text), _productViewMode, nameof(_productViewMode.AlignName),
                    true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception e)
            {

            }

        }

        private void Btn_SetPos1_Click(object sender, EventArgs e)
        {

            _productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_X = GetAxisXTotalPosition();
            _productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_Y = GetAxisYTotalPosition();

        }

        private void Btn_SetPos2_Click(object sender, EventArgs e)
        {
            _productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_X = GetAxisXTotalPosition();
            _productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_Y = GetAxisYTotalPosition();
        }

        private void Btn_SetPos3_Click(object sender, EventArgs e)
        {
            _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_X = GetAxisXTotalPosition();
            _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_Y = GetAxisYTotalPosition();
        }

        private void Btn_SetPos4_Click(object sender, EventArgs e)
        {
            _productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_X = GetAxisXTotalPosition();
            _productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_Y = GetAxisYTotalPosition();
        }

        private void Btn_SetPos5_Click(object sender, EventArgs e)
        {
            _productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_X = GetAxisXTotalPosition();
            _productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_Y = GetAxisYTotalPosition();
        }

        private void Btn_SetPos6_Click(object sender, EventArgs e)
        {
            _productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_X = GetAxisXTotalPosition();
            _productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_Y = GetAxisYTotalPosition();

        }
        private void Btn_SetPos7_Click(object sender, EventArgs e)
        {
            _productViewMode.RightUpBlock_RightUpCorner_X = GetAxisXTotalPosition();
            _productViewMode.RightUpBlock_RightUpCorner_Y = GetAxisYTotalPosition();
        }

        private void Btn_SetPos8_Click(object sender, EventArgs e)
        {
            _productViewMode.RightUpBlock_LeftDownCorner_X = GetAxisXTotalPosition();
            _productViewMode.RightUpBlock_LeftDownCorner_Y = GetAxisYTotalPosition();
        }



        private void BtnSetBoatOrigin_Click(object sender, EventArgs e)
        {
            _productViewMode.BoatOriginPosition_X = GetAxisXTotalPosition();
            _productViewMode.BoatOriginPosition_Y = GetAxisYTotalPosition();
        }

        private void BtnGoBoatOrigin_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.BoatOriginPosition_X;
            double Y_movedist = _productViewMode.BoatOriginPosition_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                            out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);

            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);

            ////WaitAxisDone(_AxisEventX);
            ////WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos1_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_X;
            double Y_movedist = _productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);

            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos2_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_X;
            double Y_movedist = _productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos3_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_X;
            double Y_movedist = _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);

            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos4_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_X;
            double Y_movedist = _productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos5_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_X;
            double Y_movedist = _productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos6_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_X;
            double Y_movedist = _productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            ////_Move.MoveExecuteImpl(MoveVel,
            ////                      _AxisEventX,
            ////                      _AxisEventX.AxisNo,
            ////                      X_movedist,
            ////                      0);
            ////_Move.MoveExecuteImpl(MoveVel,
            ////                      _AxisEventY,
            ////                      _AxisEventY.AxisNo,
            ////                      Y_movedist,
            ////                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos7_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.RightUpBlock_RightUpCorner_X;
            double Y_movedist = _productViewMode.RightUpBlock_RightUpCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            ////_Move.MoveExecuteImpl(MoveVel,
            ////                      _AxisEventX,
            ////                      _AxisEventX.AxisNo,
            ////                      X_movedist,
            ////                      0);
            ////_Move.MoveExecuteImpl(MoveVel,
            ////                      _AxisEventY,
            ////                      _AxisEventY.AxisNo,
            ////                      Y_movedist,
            ////                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void Btn_GoPos8_Click(object sender, EventArgs e)
        {
            double X_movedist = _productViewMode.RightUpBlock_LeftDownCorner_X;
            double Y_movedist = _productViewMode.RightUpBlock_LeftDownCorner_Y;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            MoveCheckAxisMoveDist(X_movedist, Y_movedist,
                out X_movedist, out Y_movedist);
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            ////_Move.MoveExecuteImpl(MoveVel,
            ////                      _AxisEventX,
            ////                      _AxisEventX.AxisNo,
            ////                      X_movedist,
            ////                      0);
            ////_Move.MoveExecuteImpl(MoveVel,
            ////                      _AxisEventY,
            ////                      _AxisEventY.AxisNo,
            ////                      Y_movedist,
            ////                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }


        //public event EventHandler<IModule> _SaveParam_Product;//儲存Product參數
        private void SaveDataBtn_Click(object sender, EventArgs e)
        {
            CalcProductSetting();

            // _SaveParam_Product += _loadmodule.UA_LoadModule_SaveProductParam;
            _SaveParam_Product?.Invoke(this, _boatModule);

            _trayViewForm.objectDraw1.MouseClickObject -= viewMouseClickObject; //解除委派事件

            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            _trayViewForm.objectDraw1.MouseClickObject -= viewMouseClickObject; //解除委派事件

            this.Close();
        }

        public void CalcProductSetting2()
        {
            //BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize
        }
        public void CalcProductSetting()
        {
            double RightUpBlock_RightUpProduct_Center_X = 0;
            double RightUpBlock_RightUpProduct_Center_Y = 0;
            double RightUpBlock_LeftDownProduct_Center_X = 0;
            double RightUpBlock_LeftDownProduct_Center_Y = 0;
            double LeftDownBlock_LeftDownProduct_Center_X = 0;
            double LeftDownBlock_LeftDownProduct_Center_Y = 0;

            //最右上的產品中心位置
            RightUpBlock_RightUpProduct_Center_X = (_productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_X +
                                                            _productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_X) / 2;

            RightUpBlock_RightUpProduct_Center_Y = (_productViewMode.RightUpBlock_RightUpProduct_LeftDownCorner_Y +
                                                            _productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_Y) / 2;

            ////最左下的產品中心位置
            RightUpBlock_LeftDownProduct_Center_X = (_productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_X +
                                                             _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_X) / 2;
            RightUpBlock_LeftDownProduct_Center_Y = (_productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_Y +
                                                             _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_Y) / 2;

            LeftDownBlock_LeftDownProduct_Center_X = (_productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_X +
                                                              _productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_X) / 2;
            LeftDownBlock_LeftDownProduct_Center_Y = (_productViewMode.LeftDownBlock_LeftDownProduct_LeftDownCorner_Y +
                                                              _productViewMode.LeftDownBlock_LeftDownProduct_RightUpCorner_Y) / 2;


            double blockCenter_X = (RightUpBlock_RightUpProduct_Center_X + RightUpBlock_LeftDownProduct_Center_X) / 2;
            double blockCenter_Y = (RightUpBlock_RightUpProduct_Center_Y + RightUpBlock_LeftDownProduct_Center_Y) / 2;
            double blockCenter_X_Length = Math.Abs(_productViewMode.BoatOriginPosition_X - blockCenter_X);
            double blockCenter_Y_Length = Math.Abs(_productViewMode.BoatOriginPosition_Y - blockCenter_Y);
            //計算產品大小
            _productViewMode.CalcProductSize();


            if (true)
            {

                #region boat盤

                //儲存產品檔基本資料
                boatData.ColNum = (int)NUD_BoatCol.Value;
                boatData.RowNum = (int)NUD_BoatRow.Value;
                boatData.Length = Convert.ToDouble(TB_BoatL.Text);
                boatData.Width = Convert.ToDouble(TB_BoatW.Text);


                //計算產品之間的Pitch
                boatData.PitchX = Math.Abs(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_X -
                                           _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_X) /
                                  (boatData.ColNum - 1);
                if (double.IsNaN(boatData.PitchX))
                    boatData.PitchX = 0;

                if (boatData.RowNum == 1)
                {
                    boatData.PitchY = 0;
                }
                else
                {
                    boatData.PitchY = Math.Abs(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_Y - _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_Y) / (boatData.RowNum - 1);
                }

                //第一顆產品到原點距離
                boatData.X = Math.Abs(_productViewMode.BoatOriginPosition_X - RightUpBlock_RightUpProduct_Center_X);
                boatData.Y = Math.Abs(RightUpBlock_RightUpProduct_Center_Y - _productViewMode.BoatOriginPosition_Y);

                #endregion

            }

            double ICCentet2Block_X = 0;
            double ICCentet2Block_Y = 0;
            if (_BoatDesc.bUseStripe)
            {
                #region Stripe
                //儲存產品檔基本資料
                stripeData.ProductColNum = (int)NUD_BoatCol.Value;
                stripeData.ProductRowNum = (int)NUD_BoatRow.Value;
                stripeData.Boat_L = Convert.ToDouble(TB_BoatL.Text);
                stripeData.Boat_W = Convert.ToDouble(TB_BoatW.Text);
                stripeData.Boat_Thickness = Convert.ToDouble(TB_BoatThickness.Text);

                //第一個block中心
                double FirstBlockCenter_X = 0;
                double FirstBlockCenter_Y = 0;
                FirstBlockCenter_X = Math.Abs(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_X +
                                              _productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_X) / 2;
                FirstBlockCenter_Y = Math.Abs(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_Y +
                                              _productViewMode.RightUpBlock_LeftDownProduct_LeftDownCorner_Y) / 2;

                var BO_X = _productViewMode.RightUpBlock_RightUpCorner_X;// (stripeData.Boat_W / 2);

                var BO_Y = _productViewMode.RightUpBlock_RightUpCorner_Y;// (stripeData.Boat_L / 2);

                //計算產品之間的Pitch
                if (stripeData.ProductColNum != 1)
                {

                    stripeData.P_PDL = Math.Abs(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_X - _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_X) / (stripeData.ProductColNum - 1);
                }
                else
                {
                    stripeData.P_PDL = 0;
                }
                if (stripeData.ProductRowNum != 1)
                {

                    stripeData.P_PDW = Math.Abs(_productViewMode.RightUpBlock_RightUpProduct_RightUpCorner_Y - _productViewMode.RightUpBlock_LeftDownProduct_RightUpCorner_Y) / (stripeData.ProductRowNum - 1);
                }
                else
                {
                    stripeData.P_PDW = 0;
                }
                //第一顆產品到自己block中心距離
                stripeData.P_PBCX = Math.Abs(FirstBlockCenter_X - RightUpBlock_RightUpProduct_Center_X);
                stripeData.P_PBCY = Math.Abs(FirstBlockCenter_Y - RightUpBlock_RightUpProduct_Center_Y);

                //第一個block中心到產品邊緣距離
                //productData.P_BCX = Math.Abs(FirstBlockCenter_X - productData.BoatOriginPosition_X);
                //productData.P_BCY = Math.Abs(FirstBlockCenter_Y - productData.BoatOriginPosition_Y);

                stripeData.P_BCX = Math.Abs(FirstBlockCenter_X - _productViewMode.BoatOriginPosition_X);
                stripeData.P_BCY = Math.Abs(FirstBlockCenter_Y - _productViewMode.BoatOriginPosition_Y);


                //block中心與另一個block中心距離
                if (_productViewMode.BlockColNum > 1)
                {

                    stripeData.P_BDL = Math.Abs(RightUpBlock_LeftDownProduct_Center_X - LeftDownBlock_LeftDownProduct_Center_X) / (_productViewMode.BlockColNum - 1);
                }
                else
                {
                    stripeData.P_BDL = 0;
                }
                if (_productViewMode.BlockRowNum > 1)
                {

                    stripeData.P_BDW = Math.Abs(RightUpBlock_LeftDownProduct_Center_Y - LeftDownBlock_LeftDownProduct_Center_Y) / (_productViewMode.BlockRowNum - 1);
                    stripeData.P_BCY = Math.Abs(stripeData.P_BCY - stripeData.P_BDW);//LC的P_BCY定義是靠近StopBar的block中心到Stopbar距離
                }
                else
                {
                    stripeData.P_BDW = 0;
                }

                //第一顆產品中心到block右上角的距離
                //第一顆產品到原點距離 - block邊緣到原點的距離
                ICCentet2Block_X = Math.Abs(BO_X - RightUpBlock_RightUpProduct_Center_X);// Math.Abs(boatData.X - (stripeData.Boat_L / 2 - Math.Abs(FirstBlockCenter_X - _productViewMode.BoatOriginPosition_X)));
                ICCentet2Block_Y = Math.Abs(BO_Y - RightUpBlock_RightUpProduct_Center_Y);// Math.Abs(boatData.Y - (stripeData.Boat_W / 2 - Math.Abs(FirstBlockCenter_Y - _productViewMode.BoatOriginPosition_Y)));


                #endregion
            }

            //Mon
            _BoatDesc.TrayInfo.TrayContainerDesc.ContainerSize = new Point2D(Convert.ToDouble(TB_BoatL.Text), Convert.ToDouble(TB_BoatW.Text));
            _BoatDesc.TrayInfo.TrayContainerDesc.SubDimSize = new Point(Convert.ToInt16(NUD_BoatBlockCol.Value), Convert.ToInt16(NUD_BoatBlockRow.Value));
            //_BoatDesc.TrayInfo.IcDesc.ContainerSize = new Point2D((double)stripeData.P_PDL, (double)stripeData.P_PDW);

            //_BoatDesc.bUseSingleIC = true;
            if (_BoatDesc.bUseStripe)
            {

            }
            //_loadmodule._SingleICLayoutSettingForm
            useSingleIC(_BoatDesc.bUseSingleIC);
            useStripe(_BoatDesc.bUseStripe);
            //FOV_X_Num();
            //FOV_Y_Num(boatData.RowNum);

            //Block Info
            //第一顆Block到自己block中心距離
            //TraySizeX(Convert.ToDouble(stripeData.P_BCX));
            ////boatData.Length
            //TraySizeY(Convert.ToDouble(stripeData.P_BCY));
            var Block_L = Math.Abs(_productViewMode.RightUpBlock_RightUpCorner_X - _productViewMode.RightUpBlock_LeftDownCorner_X);
            var Block_W = Math.Abs(_productViewMode.RightUpBlock_RightUpCorner_Y - _productViewMode.RightUpBlock_LeftDownCorner_Y);
            SetBlockSizeXY(Convert.ToDouble(Block_L), Convert.ToDouble(Block_W));
            BlockFistGapXY(Convert.ToDouble(stripeData.P_BCX), Convert.ToDouble(stripeData.P_BCY));

            ////BlockFirstGapXNum
            ////FirstGapX //第一顆產品到自己block中心距離
            IcFirstGapX(ICCentet2Block_X);
            ////FirstGapY
            IcFirstGapY(ICCentet2Block_Y);



            //IC DimX =boatData.ColNum
            IcDimX(stripeData.ProductColNum);
            //IC DimY = boatData.RowNum
            IcDimY(stripeData.ProductRowNum);

            // sizeX
            IcSizeX(Convert.ToDouble(_productViewMode.P_L));
            //sizeY
            IcSizeY(Math.Abs(_productViewMode.P_W));

            bool bMyProductInfo = _BoatDesc.bUseSingleIC ? true : (_BoatDesc.bUseStripe ? false : true);
            //_boatModule._BoatLayoutSettingForm = new BoatLayoutSettingForm(_BoatDesc, _boatModule);
            _boatModule._BoatLayoutSettingForm.UpdateBoatLayoutSettingView(_BoatDesc);//更新視窗顯示

            //BoatCarrier _boatCarrier = new BoatCarrier();
            //_boatModule.OnRequestTray(this, _boatCarrier);
            //ProductMeasureToolInfoSetting(_boatModule, _boatCarrier);

            int bNyFOVProductState = 0;
            if (_boatModule.Settings.bUseFOVSingleProduct) { bNyFOVProductState = 0; }
            else if (_boatModule.Settings.bUseFOVMultieProduct) { bNyFOVProductState = 2; }
            else if (_boatModule.Settings.bUseFOVMultiProductMax) { bNyFOVProductState = 1; }

            InspectionPostion inspectionPostion = new InspectionPostion(_boatCarrier, _boatModule.Settings.IsFovSingle, _boatModule.ProductName, _boatCarrier.IsCalBig,
                bMyProductInfo, bNyFOVProductState, _boatModule.Settings.bFOVProductXNum, _boatModule.Settings.bFOVProductYNum,
                _boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.X, _boatModule.Settings.TrayInfo.BlockContainerDesc.SubDimSize.Y);

        }

        #region Trigger & Light 調整打光

        private void TurnOnTheTrigger()
        {

            //int value = 1;
            //int frameRateTime = (1000 / value);
            //useTrigger?.SetTimmerTrigger(true, frameRateTime);

            if (int.TryParse(FrameRateValEdit.Text, out int value))
            {
                int frameRateTime = 0;
                if (value <= 30)
                    frameRateTime = (1000 / value);
                else
                    frameRateTime = 1000 / 30;

                useTrigger?.SetTimmerTrigger(true, frameRateTime);
            }

        }

        private void TurnOffTheTrigger()
        {
            //int value = 1;
            //int frameRateTime = (1000 / value);
            //useTrigger?.SetTimmerTrigger(false, frameRateTime);

            if (int.TryParse(FrameRateValEdit.Text, out int value))
            {
                int frameRateTime = 0;
                if (value <= 30)
                    frameRateTime = (1000 / value);
                else
                    frameRateTime = 1000 / 30;


                useTrigger?.SetTimmerTrigger(false, frameRateTime);
            }
        }

        private void OpenLightTool()
        {
            //TurnOnTheTrigger();
            useLighter.SetLight(groupInfo.ToArray());

            var lightForm = new LightForm(useLighter);
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

        private void Bar_CameraGain_Scroll(object sender, EventArgs e)
        {
            CamGainEdit.Text = (Bar_CameraGain.Value / 1.0).ToString();
            ChangeCamGain((Bar_CameraGain.Value / 1.0));
            SpinWait.SpinUntil(() => false, 100);

        }

        private void ChangeCamGain(double value)
        {
            if (value > 50)
                value = 50;

            int gain = (int)value;
            if (gain == 0)
            {
                gain = 1;
            }
            useFramer[0].Gain = gain;

        }

        private void Bar_FrameRate_Scroll(object sender, EventArgs e)
        {
            FrameRateValEdit.Text = Bar_FrameRate.Value.ToString();
            TurnOffTheTrigger();
            Thread.Sleep(100);
            TurnOnTheTrigger();
        }

        #endregion

        #region 影像顯示
        private HTA.IFramer.BlockQueue<List<HTA.Utility.Structure.CustomImage>> InFram =
            new HTA.IFramer.BlockQueue<List<HTA.Utility.Structure.CustomImage>>(100);
        private HImage m_curImage;
        private Point ImgSize;
        private Point2d mPrevImageSize = new Point2d(-1, -1);
        private Point2d LeftTop = new Point2d(), RightBot = new Point2d();
        protected string drawMode = "margin";
        private Thread InsertThread;

        private delegate void VoidDelegate();

        public void SetDraw(string mode)
        {
            drawMode = mode;
            ImgWnd.HalconWindow.SetDraw(drawMode);
        }



        public void ImgShow()
        {
            if (InvokeRequired)
            {
                this.Invoke(new VoidDelegate(ImgShow));
            }
            else
            if (m_curImage != null && m_curImage.IsInitialized())
            {
                ImgWnd.HalconWindow.DispObj(m_curImage);
                SetDraw(drawMode);

                ////cross mark
                //if (IsDispCrossMark)
                {
                    m_curImage.GetImageSize(out int Width, out int Height);
                    ImgWnd.HalconWindow.DispCross(new HTuple(Height / 2),   //center row
                        new HTuple(Width / 2),                              //center col
                        new HTuple(3000.0),                                 //size
                        new HTuple(0.0));                                   //rotation                             
                    HTuple a = new HTuple();
                }

                if (outOfAxisYOffsetImage != 0 || outOfAxisXOffsetImage != 0)
                {
                    m_curImage.GetImageSize(out int Width, out int Height);
                    double showOutOfAxisY = outOfAxisYOffsetImage == 0.0 ? (double)Height / 2 : outOfAxisYOffsetImage;
                    double showOutOfAxisX = outOfAxisXOffsetImage == 0.0 ? (double)Width / 2 : outOfAxisXOffsetImage;

                    ImgWnd.HalconWindow.DispCross(
                        showOutOfAxisY,   //center row
                        showOutOfAxisX,                              //center col
                        new HTuple(3000.0),                                 //size
                        new HTuple(0.0));                                   //rotation                             
                    HTuple a = new HTuple();
                }
            }
        }

        public void SetImg(HImage _img)
        {
            //ownImageQueue.TryAdd(_img);
            //if (onDrag)
            //    return;

            try
            {
                m_curImage = _img;
                m_curImage.GetImageSize(out int width, out int height);

                ImgSize.X = width;
                ImgSize.Y = height;
                if (ImgSize.X != mPrevImageSize.x || ImgSize.Y != mPrevImageSize.y)
                {
                    LeftTop.x = 0;
                    LeftTop.y = 0;
                    RightBot.x = ImgSize.X;
                    RightBot.y = ImgSize.Y;
                    mPrevImageSize.x = ImgSize.X;
                    mPrevImageSize.y = ImgSize.Y;
                }

                ImgWnd.HalconWindow.SetPart((int)LeftTop.y, (int)LeftTop.x, (int)RightBot.y, (int)RightBot.x);
                ImgShow();
            }
            catch (Exception e)
            {
                var t = e.Message;
            }
        }

        private void UpdateFrameThread()
        {
            int mCameraIdx = 0;
            while (true)
            {
                InFram.Pop(out List<CustomImage> eImage);

                // update = false;
                // ClearObj();
                SetImg(eImage[mCameraIdx].Instance);
                // Update(true);
            }
        }



        /// <summary>
        /// 點螢幕，可以動到指定位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgWnd_HMouseUp(object sender, HMouseEventArgs e)
        {
            if (!_canViewMove) return;
            
            double eHX = 0, eHY = 0;
            double nowX_pos, nowY_pos;
            double X_movedist = 0, Y_movedist = 0;
            double pixelxtomm = PixeltoMM, pixelytomm = PixeltoMM;//校正完後的pixel2mm填過來
            bool axisXmoveOutRange = false, axisYmoveOutRange = false;
            double outOfDistX = 0.0, outOfDistY = 0.0;
            outOfAxisXOffset = 0;
            outOfAxisYOffset = 0;
            outOfAxisXOffsetImage = 0;
            outOfAxisYOffsetImage = 0;
            bool positiveLimit = false; //正極限
            bool negativeLimit = false; //負極限

            eHX = e.X;
            eHY = e.Y;

            nowX_pos = Convert.ToDouble(TE_AX1_pos.Text);
            nowY_pos = Convert.ToDouble(TE_BY1_pos.Text);

            X_movedist = nowX_pos + (eHX - ImageWidth / 2) * pixelxtomm;
            Y_movedist = nowY_pos + (eHY - ImageHeight / 2) * pixelytomm;

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }

            if (!CheckAxisMoveDist(_AxisEventX, X_movedist,
                                    out double axisXLimitDist, out outOfDistX))
            {
                outOfAxisXOffset = X_movedist;
                if (axisXLimitDist < X_movedist)
                {
                    X_movedist = axisXLimitDist - 1;
                    positiveLimit = true;
                }
                else if (axisXLimitDist > X_movedist)
                {
                    X_movedist = axisXLimitDist + 1;
                    positiveLimit = false; //超正極限
                }
                axisXmoveOutRange = true;
            }

            if (!CheckAxisMoveDist(_AxisEventY, Y_movedist,
                                    out double axisYLimitDist, out outOfDistY))
            {
                outOfAxisYOffset = Y_movedist;
                if (axisYLimitDist < Y_movedist)
                    Y_movedist = axisYLimitDist - 1;
                else if (axisYLimitDist > Y_movedist)
                {
                    Y_movedist = axisYLimitDist + 1;
                    negativeLimit = true; //超出負極限
                }

                axisYmoveOutRange = true;
            }
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);

            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);


            if (axisXmoveOutRange)
            {
                if (MessageBox.Show("X軸已接近正極限，是否仍然要記錄點擊座標?",
                        "Hint", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (positiveLimit)
                    {
                        outOfAxisXOffsetImage = (ImageWidth / 2) + ((-outOfDistX - 1) / pixelxtomm);
                    }
                    else
                    {
                        outOfAxisXOffsetImage = (ImageWidth / 2) + ((outOfDistX + 1) / pixelxtomm);
                    }

                }
                else
                {
                    outOfAxisXOffset = 0;
                }
            }

            if (axisYmoveOutRange)
            {
                if (MessageBox.Show("Y軸已接近負極限，是否仍然要記錄點擊座標?",
                        "Hint", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (negativeLimit)
                    {
                        outOfAxisYOffsetImage = (ImageHeight / 2) + (outOfDistY + 1) / pixelxtomm;
                    }
                    else
                    {
                        outOfAxisYOffsetImage = (ImageHeight / 2) - (outOfDistY - 1) / pixelxtomm;
                    }
                }
                else
                {
                    outOfAxisYOffset = 0;
                }
            }
        }

        private void MoveCheckAxisMoveDist(double moveXDist, double moveYDist,
                                           out double calMoveXDist, out double calMoveYDist)
        {
            outOfAxisXOffset = 0;
            outOfAxisYOffset = 0;
            outOfAxisXOffsetImage = 0;
            outOfAxisYOffsetImage = 0;

            if (!CheckAxisMoveDist(_AxisEventX, moveXDist,
                    out double axisXLimitDist, out double outOfDistX))
            {
                outOfAxisXOffset = moveXDist;
                if (axisXLimitDist < moveXDist)
                {
                    moveXDist = axisXLimitDist - 1;
                    outOfAxisXOffsetImage = (ImageWidth / 2) + (outOfDistX - 1) * PixeltoMM;
                }

                if (axisXLimitDist > moveXDist)
                {
                    moveXDist = axisXLimitDist + 1;
                    outOfAxisXOffsetImage = (ImageWidth / 2) + (outOfDistX + 1) * PixeltoMM;
                }

            }

            if (!CheckAxisMoveDist(_AxisEventY, moveYDist,
                    out double axisYLimitDist, out double outOfDistY))
            {
                outOfAxisYOffset = moveYDist;
                if (axisYLimitDist < moveYDist)
                {
                    moveYDist = axisYLimitDist - 1;
                    outOfAxisYOffsetImage = (ImageHeight / 2) + (outOfDistY + 1) * PixeltoMM;
                }

                if (axisYLimitDist > moveYDist)
                {
                    moveYDist = axisYLimitDist + 1;
                    outOfAxisYOffsetImage = (ImageHeight / 2) + (outOfDistY - 1) * PixeltoMM;
                }
            }

            calMoveXDist = moveXDist;
            calMoveYDist = moveYDist;
        }

        /// <summary>
        /// 確認軸行程是否在極限範圍內，若為否輸出超出距離(目前只有UA1000使用)
        /// </summary>
        /// <param name="axisEvent"></param>
        /// <param name="moveDist"></param>
        /// <param name="outOfDist"></param>
        /// <returns></returns>
        private bool CheckAxisMoveDist(IAxis axisEvent, double moveDist,
                                       out double axisLimitDist, out double outOfDist)
        {
            outOfDist = 0.0;
            // axisLimitDist = axisEvent.AxisParams.maxDist;
            axisLimitDist = axisEvent.OriginalMoveMax;
            if (!ProjectName.Equals("UA1000"))
                return true;

            if (axisEvent.OriginalMoveMax < moveDist)
            {
                outOfDist = moveDist - axisEvent.OriginalMoveMax; ;
                return false;
            }

            //必須再確認一下Mon
            if (axisEvent.OriginalMoveMin > moveDist)
            {
                axisLimitDist = axisEvent.OriginalMoveMin;
                outOfDist = axisEvent.OriginalMoveMin - moveDist;
                return false;
            }

            return true;
        }

        private void TryAddFramer()
        {
            useFramer.OnGroupAllCaptured += DispFrame;
            InsertThread = new Thread(UpdateFrameThread);
            InsertThread.Start();

        }

        private void DispFrame(object sender, HTA.IFramer.StationCaptureArgs e)
        {
            if (e.GroupIndex == CurrentStationFramerSource)
            {
                InFram.Push(e.imgs);
            }
        }




        #endregion

        #region 設定其他位置(DieGroup Barcode)



        private void CB_DieGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void Btn_DieGroupSetPos_Click(object sender, EventArgs e)
        {
            double actualPosX = _AxisEventX.ActualPos;
            double actualPosY = _AxisEventY.ActualPos;

            _productViewMode.DieGroupX = actualPosX;
            _productViewMode.DieGroupY = actualPosY;
        }

        private void Btn_DieGroupGo_Click(object sender, EventArgs e)
        {
            double X_movedist = 0, Y_movedist = 0;
            X_movedist = Convert.ToDouble(TB_DieGroupX.Text);
            Y_movedist = Convert.ToDouble(TB_DieGroupY.Text);

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void CB_Ignore_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void Btn_AlignSetPos_Click(object sender, EventArgs e)
        {
            double actualPosX = _AxisEventX.ActualPos;
            double actualPosY = _AxisEventY.ActualPos;

            _productViewMode.AlignX = actualPosX;
            _productViewMode.AlignY = actualPosY;
        }

        private void Btn_AlignGo_Click(object sender, EventArgs e)
        {
            double X_movedist = 0, Y_movedist = 0;
            X_movedist = Convert.ToDouble(TB_AlignX.Text);
            Y_movedist = Convert.ToDouble(TB_AlignY.Text);

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);

            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);
            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);
        }

        private void CB_Align_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        private void CB_IgnoreAlign_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void Btn_SetBarcodePos_Click(object sender, EventArgs e)
        {
            //相對於產品中心的Barcode位置
            _productViewMode.BarcodePosX = _AxisEventX.ActualPos;
            _productViewMode.BarcodePosY = _AxisEventY.ActualPos;
        }

        private void Btn_GoBarcodePos_Click(object sender, EventArgs e)
        {
            double X_movedist = 0, Y_movedist = 0;
            X_movedist = Convert.ToDouble(TB_BarcodeX.Text);
            Y_movedist = Convert.ToDouble(TB_BarcodeY.Text);

            if (AxisInterferenceUA(X_movedist, Y_movedist))
            {
                MessageBox.Show("X軸和Y軸不能同時接近原點!");
                return;
            }
            MoveAxis(_AxisEventX, X_movedist, _AxisEventX.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventX,
            //                      _AxisEventX.AxisNo,
            //                      X_movedist,
            //                      0);
            //_Move.MoveExecuteImpl(MoveVel,
            //                      _AxisEventY,
            //                      _AxisEventY.AxisNo,
            //                      Y_movedist,
            //                      0);

            //WaitAxisDone(_AxisEventX);
            //WaitAxisDone(_AxisEventY);

        }

        #endregion

        #region ObjectDraw Show Boat盤示意圖
        //private void DrawFOV(Point Location)
        //{

        //    //objectDraw1.Refresh();
        //    Pen pi = new Pen(Brushes.DodgerBlue, 2);
        //    Rectangle rect1 = new Rectangle();
        //    float FOVsize_x, FOVsize_y;//原本40
        //    if (FOV_X < 1)
        //    {
        //        FOVsize_x = 40;
        //        FOVsize_y = 40;
        //    }
        //    else
        //    {
        //        FOVsize_x = (float)FOV_X - 2;
        //        FOVsize_y = (float)FOV_Y - 2;
        //    }

        //    int Loacation_TansPixel_X = (int)(((Sizer.X - (Location.X - _productViewMode.BoatOriginPosition_X)) / Sizer.X) * objectDraw1.Width);
        //    int Loacation_TansPixel_Y = (int)(((_productViewMode.BoatOriginPosition_Y - Location.Y) / Sizer.Y) * objectDraw1.Height);
        //    int SizetoDraw_x = (int)(FOVsize_x / Sizer.X * objectDraw1.Width);
        //    int SizetoDraw_y = (int)(FOVsize_y / Sizer.Y * objectDraw1.Height);
        //    if (Loacation_TansPixel_X + (SizetoDraw_x / 2) > objectDraw1.Width)
        //    {
        //        Loacation_TansPixel_X = objectDraw1.Width - ((int)SizetoDraw_x / 2) - 2;
        //    }
        //    if (Loacation_TansPixel_X - (SizetoDraw_x / 2) < 0)
        //    {
        //        Loacation_TansPixel_X = 0 + ((int)SizetoDraw_x / 2);
        //    }
        //    if (Loacation_TansPixel_Y + (SizetoDraw_y / 2) > objectDraw1.Height)
        //    {
        //        Loacation_TansPixel_Y = objectDraw1.Height - ((int)SizetoDraw_y / 2) - 2;
        //    }
        //    if (Loacation_TansPixel_Y - (SizetoDraw_y / 2) < 0)
        //    {
        //        Loacation_TansPixel_Y = 0 + ((int)SizetoDraw_y / 2);
        //    }
        //    rect1.X = (int)((Loacation_TansPixel_X) - SizetoDraw_x / 2);
        //    rect1.Y = (int)((Loacation_TansPixel_Y) - SizetoDraw_y / 2);
        //    rect1.Size = new Size(SizetoDraw_x, SizetoDraw_y);
        //    g.DrawRectangle(pi, rect1);
        //}
        private void Btn_Calc_Click(object sender, EventArgs e)
        {
            CalcProductSetting();

            _trayViewForm.Height = (int)float.Parse(TB_BoatW.Text);
            _trayViewForm.Width = (int)float.Parse(TB_BoatL.Text);

            Sizer = new PointF(float.Parse(TB_BoatL.Text), float.Parse(TB_BoatW.Text));

            if (_trayViewForm == null)
                _trayViewForm = new TrayViewForm();

            TrayContainer newTray = null;

            //設定產品繪製資訊
            //newTray = _productViewMode.UsingBoat_1? _BoatDesc.TrayInfo.GenerateTray() : _BoatDesc.StripeInfo.GenerateTray();

            newTray = _BoatDesc.TrayInfo.GenerateTray();
         
            _trayViewForm.SetupTray(newTray);
  
            _trayViewForm.objectDraw1.MouseClickObject -= viewMouseClickObject;
            _trayViewForm.objectDraw1.MouseClickObject += viewMouseClickObject;


            //_trayViewForm.TopMost = true;

            //var leftPos = new Point(this.Right, this.Top);
            //var posOnScreen = this.PointToScreen(leftPos);
            //_trayViewForm.Left = posOnScreen.X + 20;
            //_trayViewForm.Top = posOnScreen.Y;
            //_trayViewForm.Show();



            //if (_productViewMode.UsingBoat_1 == true)
            //{
            //    _trayViewForm.BlockSizeY = float.Parse(TB_BoatW.Text);
            //    _view.BlockSizeX = float.Parse(TB_BoatL.Text);

            //    _view.ProductCountX = boatData.ColNum;
            //    _view.ProductCountY = boatData.RowNum;
            //    _view.ProductSizeX = (float)_productViewMode.P_L;
            //    _view.ProductSizeY = (float)_productViewMode.P_W;
            //    _view.ToFirstProductX = (float)boatData.X;
            //    _view.ToFirstProductY = (float)boatData.Y;
            //    _view.ProductGapX = (float)boatData.PitchX;
            //    _view.ProductGapY = (float)boatData.PitchY;
            //}
            //else
            //{
            //    _view.ProductCountX = stripeData.ProductColNum;
            //    _view.ProductCountY = stripeData.ProductRowNum;

            //    _view.ProductSizeX = (float)_productViewMode.P_L;
            //    _view.ProductSizeY = (float)_productViewMode.P_W;

            //    _view.BlockFirstGapX = (float)stripeData.P_BCX - (float)stripeData.P_PBCX - (float)_productViewMode.P_L / 2;
            //    //_view.BlockFirstGapY = float.Parse(TB_BoatW.Text) - ((float)stripeData.P_BCY + (float)stripeData.P_PBCY) - (float)productData.P_W / 2;
            //    //_view.BlockFirstGapY = float.Parse(TB_BoatW.Text) - (float)stripeData.P_BCY - (float)stripeData.P_BDW;
            //    _view.BlockFirstGapY = float.Parse(TB_BoatW.Text) - ((float)stripeData.P_BCY + (float)stripeData.P_PBCY + (float)_productViewMode.P_W / 2 + (float)stripeData.P_BDW);

            //    _view.ToFirstProductX = (float)_productViewMode.P_L / 2;
            //    _view.ToFirstProductY = (float)_productViewMode.P_W / 2;
            //    _view.BlockCountX = _productViewMode.BlockColNum;
            //    _view.BlockCountY = _productViewMode.BlockRowNum;

            //    _view.ProductGapX = (float)stripeData.P_PDL;
            //    _view.ProductGapY = (float)stripeData.P_PDW;

            //    _view.BlockGapX = (float)stripeData.P_BDL;
            //    _view.BlockGapY = (float)stripeData.P_BDW;

            //    //_view.BlockSizeX = (_view.ProductCountX) * _view.ProductSizeX;
            //    _view.BlockSizeX = (_view.ProductCountX - 1) * _view.ProductGapX + _view.ProductSizeX;
            //    //_view.BlockSizeY = (_view.ProductCountY) * _view.ProductSizeY;
            //    _view.BlockSizeY = (_view.ProductCountY - 1) * _view.ProductGapY + _view.ProductSizeY;

            //}

            //Sizer = new PointF(float.Parse(TB_BoatL.Text), float.Parse(TB_BoatW.Text));
            //objectDraw1.Refresh();
            //_view.Paint();
            //DrawFOV(new Point(0, 0));

            tabPage2.Parent = tabControl1;
            //tabControl1.SelectedIndex = 1;
            //objectDraw1.MouseClickObject += (ss, ee) =>
            //{
            //    double X_movedist = _productViewMode.BoatOriginPosition_X + (_view.Width - (_view.Width - ConvertPercentToTrayCoord(ee.obj.Center).X));
            //    double Y_movedist = _productViewMode.BoatOriginPosition_Y - (ConvertPercentToTrayCoord(ee.obj.Center).Y);

            //    MoveAxis(_AxisEventX, X_movedist);
            //    MoveAxis(_AxisEventY, Y_movedist);

            //};
        }

        public PointF Sizer { get; set; } = new PointF(322.6f, 137f);

        public Point2D ConvertPercentToTrayCoord(Point2D persentInObjectDraw)
        {
            return new Point2D(persentInObjectDraw.X / 100.0 * (double)Sizer.X, persentInObjectDraw.Y / 100.0 * (double)Sizer.Y);
        }
        //private void objectDraw1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    //DrawFOV(new Point((int)_AxisEventX.ActualPos, (int)_AxisEventY.ActualPos));
        //}

        //private void ImgWnd_HMouseWheel(object sender, HMouseEventArgs e)
        //{
        //    Point relativePoint = new Point();
        //    relativePoint.X = (int)e.X;
        //    relativePoint.Y = (int)e.Y;
        //    if ((Currentscale >= 1 && e.Delta < 0) || (Currentscale <= 0.1 && e.Delta >= 0))
        //    {
        //        return;
        //    }
        //    if (e.Delta >= 0)
        //        zoom(relativePoint, -0.1);
        //    else
        //        zoom(relativePoint, 0.1);
        //    ImgWnd.HalconWindow.DispObj(m_curImage);
        //}

        /// <summary>
        /// 影像縮放功能
        /// </summary>
        /// <param name="center">縮放中心</param>
        /// <param name="factor">縮放比例</param>
        //private void zoom(Point center, double factor)
        //{
        //    Currentscale += factor;
        //    double width = ImgSize.X * Currentscale * 0.5;
        //    double height = ImgSize.Y * Currentscale * 0.5;
        //    double centerX = center.X;
        //    double centerY = center.Y;
        //    int x_fix = 0, y_fix = 0;

        //    if ((int)(centerY - height) < 0)
        //    {
        //        y_fix = 0 - (int)(centerY - height);

        //    }
        //    LeftTop.y = (int)(centerY - height);
        //    if ((int)(centerX - width) < 0)
        //    {
        //        x_fix = 0 - (int)(centerX - width);
        //    }
        //    LeftTop.x = (int)(centerX - width);

        //    if ((int)(centerY + height) > ImgSize.Y - 1)
        //    {
        //        y_fix += ImgSize.Y - 1 - (int)(centerY + height);
        //    }

        //    RightBot.y = (int)(centerY + height);


        //    if ((int)(centerX + width) > ImgSize.X - 1)
        //    {
        //        x_fix += ImgSize.X - 1 - (int)(centerX + width);
        //    }

        //    RightBot.x = (int)(centerX + width);
        //    ImgWnd.HalconWindow.SetPart((int)LeftTop.y + y_fix,
        //       (int)LeftTop.x + x_fix,
        //       (int)RightBot.y + y_fix,
        //       (int)RightBot.x + x_fix);
        //    LeftTop.y += y_fix;
        //    LeftTop.x += x_fix;
        //    RightBot.y += y_fix;
        //    RightBot.x += x_fix;

        //}



        #endregion

        #region 主畫面動作(shown closing)
        private void ProductMeasureTool_Shown(object sender, EventArgs e)
        {
            ShowData();
            PutProduct?.Invoke();
            TurnOnTheTrigger();
        }

        private void TB_BoatL_TextChanged(object sender, EventArgs e)
        {

        }

        private void TB_BoatW_TextChanged(object sender, EventArgs e)
        {

        }

        private void NUD_BoatRow_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NUD_BoatCol_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NUD_BoatBlockRow_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NUD_BoatBlockCol_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TB_ProductThickness_TextChanged(object sender, EventArgs e)
        {

        }

        private void TB_BoatThickness_TextChanged(object sender, EventArgs e)
        {

        }

        private void COB_Z_step_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TB_DieGroupX_TextChanged(object sender, EventArgs e)
        {

        }

        private void TB_AlignX_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ProductMeasureTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer_GetPos.Stop();

            TurnOffTheTrigger();

            useFramer.OnGroupAllCaptured -= DispFrame;

            _trayViewForm.objectDraw1.MouseClickObject -= viewMouseClickObject; //解除委派事件

            InsertThread?.Abort();



        }


        #endregion

        ///// <summary>
        ///// 等待軸移動完成
        ///// </summary>
        ///// <param name="AxisName">預等待的軸</param>
        //private void //WaitAxisDone(MotionAxisEvent axis)
        //{
        //    bool ret = SpinWait.SpinUntil(() =>
        //    {
        //        return axis.IsRunning() == false;
        //    }, BaseController.MovingTime);

        //    if (!ret)
        //    {
        //        MessageBox.Show($"{axis.AxisName} timeout");
        //    }
        //}

        #region 軸干涉

        /// <summary>
        /// UA: X軸和Y軸同時在原點時，Y軸上的barcode支架可能與X軸上光源側邊線圈碰撞
        /// </summary>
        public bool AxisInterferenceUA(double AxisXDist, double AxisYDist)
        {
            //if (!CheckProjectIsUA) //只在UA專案判斷
            //    return false;

            if ((AxisXDist == 0 && AxisYDist < 150) ||
                (AxisYDist == 0 && AxisXDist < 10))
            {
                return true;
            }

            return false;
        }

        internal void SetupOriginalPosition(double x, double y)
        {
            _productViewMode.SetOriginalPose(x, y);
        }

        #endregion
        public bool MoveAxis(IAxis axis, double dist, Velocity vel = null, int moveTimeout = 10000, int waitTimeout = 10000)
        {
            Velocity axisVel = new Velocity();

            if (vel == null)
                axisVel = axis.MoveVelocity;
            else
                axisVel = vel;

            bool moveResult = axis.AbsoluteMove(dist, axisVel, moveTimeout);
            bool waitResult = axis.WaitMotionDone(waitTimeout);
            bool isMoveSuccess = moveResult && waitResult && (axis.ActualPos - dist < 1.0) && (axis.ActualPos - dist > -1.0);
            // LogTrace($"MoveAxis - Axis:{axis.Name}, dist:{dist}, ActualPos:{axis.ActualPos}, IsMoveSuccess:{isMoveSuccess}, moveTimeout:{moveTimeout}, waitTimeout:{waitTimeout}");

            return isMoveSuccess;
        }
        private void viewMouseClickObject(object sender, MouseMoveOnObjectEvenArg e)
        {
            //_trayViewForm
            //newTray

            var _transPoint = doConvertPercentToTrayCoord(e.obj.Center);
            double X_movedist = _productViewMode.BoatOriginPosition_X - ( (_trayViewForm.Width - _transPoint.X));
            double Y_movedist = _productViewMode.BoatOriginPosition_Y + (_transPoint.Y);

            // X_movedist = (e.obj.Center).X;
             //Y_movedist = (e.obj.Center).Y;

            MoveAxis(_AxisEventX, X_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
            MoveAxis(_AxisEventY, Y_movedist, _AxisEventY.BaseVelocity[COB_Vel.SelectedIndex]);
        }
        private Point2D doConvertPercentToTrayCoord(Point2D persentInObjectDraw)
        {
            Point2D tmp = new Point2D(persentInObjectDraw.X / 100.0 * (double)Sizer.X, persentInObjectDraw.Y / 100.0 * (double)Sizer.Y);
            //Point2D tmp = new Point2D(persentInObjectDraw.X / 100.0 * 310, persentInObjectDraw.Y / 100.0 * 160);
            return tmp;
        }
    }
}
