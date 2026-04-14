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
using DevExpress.XtraEditors;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TA2000Modules
{
    public partial class TeachControlWizardForm3 : DevExpress.XtraEditors.XtraForm,IMultilingual
    {
        public string InspectType = "";
        private HTAMachine.Machine.IModule _module;
        private FlowForm _flowForm;
        private InspectionModule _inspectionModule;
        private InspectionProductParam _param;
        
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
        public Action<Form, int> OnShowSplitPanel;
        public WizardForm WizardForm;
        TeachControlWizardViewModel _viewModel;
        public TeachControlWizardForm3(string inspectType, HTAMachine.Machine.IModule module, FlowForm flowForm, IMainController mainController, WizardForm wizardForm)
        {
            InitializeComponent();
            InspectType=inspectType;
            CurrentVisionController = mainController;
            WizardForm = wizardForm;
            if (InspectType == "Mosaic")
            {
                LC_MosaicPartIndex.Text = "Mosaic Index";
                Btn_MovePartPos.Text = "Move Mosaic Position";
                Btn_AdjustPartPos.Visible = false;
            }
            else
            {
                LC_MosaicPartIndex.Text = "Part Index";
                Btn_MovePartPos.Text = "Move Part Position";
                Btn_AdjustPartPos.Text = "Adjust Position";
                Btn_AdjustPartPos.Visible = true;
            }
           // _suckerModule = (SuckerModule)suckmodule;
            SetupFlowForm(flowForm);
            SetupModule(module);
        }

        public void SetLanguage(ComponentResourceManager resources)
        {
            var oldWindowState = this.WindowState;
            var oldBounds = this.Bounds;
            var oldClientSize = this.ClientSize;

            if (this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;

            var resources2 = new ComponentResourceManager(typeof(TeachControlWizardForm3));

            this.SuspendLayout();
            try
            {
                resources2.ApplyResources(this, "$this");
                this.Bounds = oldBounds;
                this.ClientSize = oldClientSize;

                SetLang(this.Controls, resources2);
            }
            finally
            {
                this.ResumeLayout(false);
            }

            this.WindowState = oldWindowState;
            this.PerformLayout();
            this.Refresh();
        }

        public void SetLang(Control.ControlCollection ctrls, ComponentResourceManager resource)
        {
            foreach (Control ctrl in ctrls)
            {
                var oldBounds = ctrl.Bounds;
                var oldMin = ctrl.MinimumSize;
                var oldMax = ctrl.MaximumSize;
                var oldDock = ctrl.Dock;
                var oldAnchor = ctrl.Anchor;

                resource.ApplyResources(ctrl, ctrl.Name);

                ctrl.Dock = oldDock;
                ctrl.Anchor = oldAnchor;
                ctrl.MinimumSize = oldMin;
                ctrl.MaximumSize = oldMax;
                ctrl.Bounds = oldBounds;

                if (ctrl.HasChildren)
                    SetLang(ctrl.Controls, resource);
            }
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
            _offsetDist = _param.FocusLocation;
             _baseDist = _inspectionModule.MotorOffset.InspStandBy_Z;
            //_baseDist = _inspectionModule.SuckerModuleThis.MotorOffset.InspectPosition_AZ1;

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
            fluent.ViewModel.ShowPanelTabControl = this.ShowPanelTabControl;
            fluent.ViewModel.ShowPanelPage = this.ShowPanelPage;
            fluent.ViewModel.OriginPage = this.OriginPage;
            //fluent.ViewModel.GetLightingPercentage += _flowForm.LightingPersentage;
            _flowForm.FlowFormImageIn += fluent.ViewModel.FlowFormImageIn;//待測試，如果要用最新的VisionController2的話，再打開這段
            _flowForm.OnProductSaved += fluent.ViewModel.VisionProductSaved;
            _flowForm.OnFixCmpPosition = fluent.ViewModel.FixCmpPosition;
            _flowForm.ComponentFormOtherAction = fluent.ViewModel.OpenCmpForm;
            _flowForm.OnComponentFormClose = fluent.ViewModel.CloseCmpForm;
            fluent.ViewModel.OnRefreshBuffer = _flowForm.OnBufferRefresh;
            fluent.ViewModel.FlowFormData = _flowForm;
            fluent.ViewModel.OnLog = _inspectionModule.OnAddTrace;
            fluent.ViewModel.OnShowPanel = AddPanel;
            fluent.ViewModel.Initial(_param, _carrier, _baseDist,
                _offsetDist, _moveLimit, _accessor, _useFramer,
                _useLighter, _useTrigger, _groupInfo, _hintStr);
            fluent.ViewModel.InitMapTreeView(treeView1);
            fluent.ViewModel.SetTabControl(xtraTabControl1, PosPage, CapPage, CmpPage,MapPage);

            fluent.BindCommand(Btn_AdjustPartPos, x => x.AdjustPartPos());
            fluent.BindCommand(Btn_Capture, x => x.Capture());
            //fluent.BindCommand(Btn_Capture, x => x.CaptureGrayCard());//TODO 要上機測再打開
            fluent.BindCommand(Btn_GetProduct, x => x.GetProduct());
            fluent.BindCommand(Btn_PutProduct, x => x.PutProduct());
            fluent.BindCommand(Btn_MovePartPos, x => x.MovePartPos());
            fluent.BindCommand(Btn_Focus, x => x.Focus());
            fluent.BindCommand(Btn_AdjustLight, x => x.AdjustLight());
            fluent.BindCommand(Btn_TheMapIndexCapture, x => x.TheMapIndexCapture());


            LUE_X.Properties.DataSource = fluent.ViewModel.XList;
            LUE_Y.Properties.DataSource = fluent.ViewModel.YList;
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;
            LUE_MosaicPartIndex.Properties.DataSource = fluent.ViewModel.MosaicList;


            fluent.SetBinding(LUE_X, le => le.EditValue, x => x.ColX);
            fluent.SetBinding(LUE_Y, le => le.EditValue, x => x.RowY);
            fluent.SetBinding(LUE_Vel, le => le.EditValue, x => x.MoveVel);

            fluent.SetBinding(LUE_MosaicPartIndex, le => le.EditValue, x => x.MosaicPartIndex);

            //TeachControlWizardViewModel3
            fluent.SetBinding(LC_SelectCap, g => g.Text, x => x.SelectCapText);
            fluent.SetBinding(TE_CaptureName, g => g.Text, x => x.CaptureName);
            fluent.SetBinding(LC_SelectPos, g => g.Text, x => x.SelectPosText);
            fluent.SetBinding(LC_SelectMap,g=>g.Text, x => x.SelectMapText);
            fluent.SetBinding(LC_ComponentName, g => g.Text, x => x.ComponentName);
            fluent.SetBinding(TE_UserDefinedName, g => g.Text, x => x.UserDefinedName);
            fluent.SetBinding(TE_StandardName, g => g.Text, x => x.StandardName);
            fluent.SetBinding(Btn_ExpandAll, g => g.Text, x => x.ExpandAllText);

            fluent.BindCommand(Btn_DeleteComponent, x => x.DeleteComponent(null));
            fluent.BindCommand(Btn_OpenTeach, x => x.OpenTeach());
            fluent.BindCommand(Btn_SetUserDefinedName,x=>x.SetUserDefinedName);
            fluent.BindCommand(Btn_DeletePosition, x => x.DeleteGroup);
            fluent.BindCommand<bool>(Btn_DeleteCapture,vm => vm.DeleteCapture,v => true);
            fluent.BindCommand(Btn_ExpandAll,x=>x.ExpendAll);
            

            SaveProductBtn.Click += (s, e) =>
            {
                fluent.ViewModel.SaveProductImageAndParameter();
            };
            OnlySaveParameter.Click += (s, e) =>
            {
                fluent.ViewModel.SaveOnlyParameter();
            };
            ProductRefreshBtn.Click += (s, e) =>
            {
                fluent.ViewModel.ProductRefresh();
            };
            FlowRun.Click += (s, e) =>
            {
                fluent.ViewModel.FlowRunExecute();
            };


        }


        public void AddPanel(Form form, int size)
        {
            OnShowSplitPanel?.Invoke(form, size);
        }
    }
}
