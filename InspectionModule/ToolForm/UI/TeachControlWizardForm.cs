using TA2000Modules;
using HTA.MainController;
using HyperInspection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.MVVM;
using HTAMachine.Machine;
namespace TA2000Modules
{
    public partial class TeachControlWizardForm : DevExpress.XtraEditors.XtraForm
    {
        public string InspectType = "";
        private HTAMachine.Machine.IModule _module;
        private FlowForm _flowForm;
        private InspectionModule _inspectionModule;
        private InspectionProductParam _param;
        //private SuckerModule _suckerModule;
        private double _offsetDist;
        private double _baseDist;
        //private MainController _mainController;
        private HTA.IFramer.IStationFramer _useFramer;
        private HTA.LightServer.ILighter _useLighter;
        private HTA.TriggerServer.ITriggerChannel _useTrigger;
        private List<double> _groupInfo;
        private string _hintStr;
        private BoatCarrier _carrier;
        private int _moveLimit = 8;
        private ImageWindowAccessor _accessor;
        public IMainController CurrentVisionController;
        public Action<Form,int> OnShowSplitPanel;
        public WizardForm WizardForm;
        public Func<Point, Point,TreeView> OnCreateConverTree;
        TeachControlWizardViewModel _viewModel;
        public TeachControlWizardForm(string inspectType, HTAMachine.Machine.IModule module, HTAMachine.Machine.IModule suckmodule, FlowForm flowForm,IMainController mainController,WizardForm wizardForm)
        {
            InitializeComponent();
            InspectType=inspectType;
            CurrentVisionController = mainController;
            WizardForm = wizardForm;
            if (InspectType == "Mosaic")
            {
                groupBox1.Text = "組圖";
                labelControl12.Text = "MosaicIndex";
                Btn_MovePartPos.Text = "移動組圖位置";
                Btn_AdjustPartPos.Visible = false;
            }
            else
            {
                groupBox1.Text = "分區";
                labelControl12.Text = "分區Index";
                Btn_MovePartPos.Text = "移動分區位置";
                Btn_AdjustPartPos.Text = "微調位置";
                Btn_AdjustPartPos.Visible = true;
            }
            //_suckerModule = (SuckerModule)suckmodule;
            SetupFlowForm(flowForm);
            SetupModule(module);
        }
        public void SetupFlowForm(Form flowForm)
        {
            if (flowForm == null) return;
            _flowForm = (FlowForm)flowForm;
        }

        public void SetupModule(IModule module)
        {
            _module = module;
            _inspectionModule = (InspectionModule)_module;
            _param = _inspectionModule.ProductParam;
            _carrier = _inspectionModule.CurrentTrayCarrier;
            _offsetDist = _param.FocusLocation_Mon;
            
            _baseDist = _inspectionModule.ProductParam.FocusLocation_Mon;

            //_mainController = (MainController)_inspectionModule.VisionController;

            _useFramer = CurrentVisionController.Framer;
            _useLighter = CurrentVisionController.Lighter;
            _useTrigger = CurrentVisionController.Trigger1;
            _groupInfo = new List<double> { 50 * 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _hintStr = null;
            _accessor = _flowForm.GenAccessor();
            //setup something

            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();


        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<TeachControlWizardViewModel>();
            _viewModel = fluent.ViewModel;

            fluent.ViewModel.CurrentVisionController = CurrentVisionController;
            fluent.ViewModel.Type = InspectType;
            fluent.ViewModel.Vision = _inspectionModule;
            fluent.ViewModel.TotalCount = CurrentVisionController.ProductSetting.TotalCapture;
            fluent.ViewModel.SetLighting += CurrentVisionController.Lighter.SetLight;
            fluent.ViewModel.SaveParam += _inspectionModule.SaveParam;
            fluent.ViewModel.SaveVisionProductParam += _inspectionModule.SaveVisionProductParam;
            fluent.ViewModel.OnClose = this.Close;
            fluent.ViewModel.ChangeCamAndCaptureIndexTrigger += _flowForm.ChangeCamAndCapIdx;
            //fluent.ViewModel.GetLightingPercentage += _flowForm.LightingPersentage;
            _flowForm.FlowFormImageIn += fluent.ViewModel.FlowFormImageIn;//待測試，如果要用最新的VisionController2的話，再打開這段
            _flowForm.OnProductSaved += fluent.ViewModel.VisionProductSaved;
            fluent.ViewModel.OnRefreshBuffer = _flowForm.OnBufferRefresh;
            fluent.ViewModel.FlowFormData = _flowForm;
            fluent.ViewModel.OnLog = _inspectionModule.OnAddTrace;
            fluent.ViewModel.OnShowPanel = AddPanel;
            fluent.ViewModel.Initial(_param, _carrier, _baseDist,
                _offsetDist, _moveLimit, _accessor, _useFramer,
                _useLighter, _useTrigger, _groupInfo, _hintStr);

            fluent.BindCommand(Btn_AddCapture, x => x.AddCapture());
            fluent.BindCommand(Btn_AdjustPartPos, x => x.AdjustPartPos());
            fluent.BindCommand(Btn_Capture, x => x.Capture());
            //fluent.BindCommand(Btn_Capture, x => x.CaptureGrayCard());//TODO 要上機測再打開
            fluent.BindCommand(Btn_DeleteCapture, x => x.DeleteCapture(true));
            fluent.BindCommand(Btn_AddGroup, x => x.AddGroup(false));
            fluent.BindCommand(Btn_DeleteGroup, x => x.DeleteGroup());
            fluent.BindCommand(Btn_GetProduct, x => x.GetProduct());
            fluent.BindCommand(Btn_PutProduct, x => x.PutProduct());
            fluent.BindCommand(Btn_MovePartPos, x => x.MovePartPos());
            fluent.BindCommand(Btn_Focus, x => x.Focus());
            fluent.BindCommand(Btn_AdjustLight, x => x.AdjustLight());
            fluent.BindCommand(Btn_TheMapIndexCapture, x => x.TheMapIndexCapture());


            LUE_X.Properties.DataSource = fluent.ViewModel.XList;
            LUE_Y.Properties.DataSource = fluent.ViewModel.YList;
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;
            //LUE_MapIndexMove.Properties.DataSource = fluent.ViewModel.MapIndexMoveList;
            LUE_Type.Properties.DataSource = fluent.ViewModel.TypeList;
            LUE_MosaicPartIndex.Properties.DataSource = fluent.ViewModel.MosaicList;
            LUE_MapIndexImage.Properties.DataSource = fluent.ViewModel.MapIndexImageList;
            LUE_Group.Properties.DataSource = fluent.ViewModel.GroupIndexList;
            LUE_Capture.Properties.DataSource = fluent.ViewModel.CaptureIndexList;



            fluent.SetBinding(LUE_X, le => le.EditValue, x => x.ColX);
            fluent.SetBinding(LUE_Y, le => le.EditValue, x => x.RowY);
            fluent.SetBinding(LUE_Vel, le => le.EditValue, x => x.MoveVel);
            //fluent.SetBinding(LUE_MapIndexMove, le => le.EditValue, x => x.MapIndexMove);
            fluent.SetBinding(LUE_Type, le => le.EditValue, x => x.Type);
            fluent.SetBinding(LUE_MosaicPartIndex, le => le.EditValue, x => x.MosaicPartIndex);
            fluent.SetBinding(LUE_MapIndexImage, le => le.EditValue, x => x.MapIndexImage);
            fluent.SetBinding(LUE_Group, le => le.EditValue, x => x.GroupIndex);
            fluent.SetBinding(LUE_Capture, le => le.EditValue, x => x.CaptureIndex);


        }


        public void AddPanel(Form form,int size)
        {
            OnShowSplitPanel?.Invoke(form,size);
        }
    }
}
