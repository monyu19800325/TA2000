using DevExpress.Mvvm.Xpf;
using DevExpress.XtraPrinting;
using HTA.MainController;
using HTAMachine.Machine;
using HyperInspection;
using ModuleTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using DevExpress.Utils.MVVM;
namespace TA2000Modules
{
    public partial class GoldenTeachControl : DevExpress.XtraEditors.XtraForm, ITeachingSetup, IMultilingual
    {
        private MVVMContextFluentAPI<GoldenTeachControlViewModel> fluent;
        private HTAMachine.Machine.IModule _module;
        private FlowForm _flowForm;
        private InspectionModule _inspectionModule;
        //private SuckerModule _suckerModule;
        private InspectionProductParam _param;
        private double _offsetDist;
        private double _baseDist;

        private MainController _mainController;
        private HTA.IFramer.IStationFramer _useFramer;
        private HTA.LightServer.ILighter _useLighter;
        private HTA.TriggerServer.ITriggerChannel _useTrigger;
        private List<double> _groupInfo;
        private string _hintStr;
        private BoatCarrier _carrier;
        private int _moveLimit = 8;
        private ImageWindowAccessor _accessor;
        public Action OnNotifyOuter;
        public GoldenTeachControl(HTAMachine.Machine.IModule module, FlowForm flowForm)
        {
            InitializeComponent();
            //_suckerModule = (SuckerModule)suckermodule;
            SetupFlowForm(flowForm);
            SetupModule(module);
        }

        public void SetLanguage(ComponentResourceManager resources)
        {
            System.ComponentModel.ComponentResourceManager resources2 = new System.ComponentModel.ComponentResourceManager(typeof(GoldenTeachControl));
            resources2.ApplyResources(this, "$this");
            SetLang(this.Controls, resources2);
            this.Refresh();
        }

        public void SetLang(Control.ControlCollection ctrls, ComponentResourceManager resource)
        {
            foreach (Control ctrl in ctrls)
            {
                resource.ApplyResources(ctrl, ctrl.Name);
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
            _offsetDist = _param.FocusLocation_Mon;          
            _baseDist = _param.FocusLocation_Mon;            

            _mainController = (MainController)_inspectionModule.VisionController;

            _useFramer = _mainController.Framer;
            _useLighter = _mainController.Lighter;
            _useTrigger = _mainController.Trigger1;
            _groupInfo = new List<double> { 50 * 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _hintStr = null;
            _accessor = _flowForm.GenAccessor();
            //setup something

            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();


        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<GoldenTeachControlViewModel>();

            //ViewModel委派
            fluent.ViewModel.Vision = _inspectionModule;
            //fluent.ViewModel.SuckerModule = _suckerModule;
            fluent.ViewModel.TotalCount = _mainController.ProductSetting.TotalCapture;
            fluent.ViewModel.SetLighting += _mainController.Lighter.SetLight;
            fluent.ViewModel.SaveParam += _inspectionModule.SaveParam;
            fluent.ViewModel.OnClose = this.Close;
            fluent.ViewModel.ChangeCamAndCaptureIndexTrigger += _flowForm.ChangeCamAndCapIdx;
            _flowForm.FlowFormImageIn += fluent.ViewModel.FlowFormImageIn;//待測試，如果要用最新的VisionController2的話，再打開這段
            _flowForm.OnProductSaved += fluent.ViewModel.VisionProductSaved;
            fluent.ViewModel.OnRefreshBuffer = _flowForm.OnBufferRefresh;
            fluent.ViewModel.FlowFormData = _flowForm;
            fluent.ViewModel.OnLog = _inspectionModule.OnAddTrace;
            fluent.ViewModel.NotifyOuter = SendNotifyOuter;

            fluent.ViewModel.Initial(_param, _carrier, _baseDist,
                _offsetDist,  _moveLimit, _accessor, _useFramer,
                _useLighter, _useTrigger, _groupInfo, _hintStr);

            fluent.BindCommand(Btn_Capture0, c => c.Capture);
            fluent.BindCommand(Btn_Focus, c => c.Focus);         
            fluent.BindCommand(Btn_Inspect, c => c.Inspect);
            fluent.BindCommand(Btn_StopInspect, c => c.StopInspect);
            fluent.BindCommand(Btn_FinishGolden, c => c.FinishGolden);
            fluent.BindCommand(Btn_MoveMosaic, c => c.MosaicMove);

            LUE_X.Properties.DataSource = fluent.ViewModel.XList;
            LUE_Y.Properties.DataSource = fluent.ViewModel.YList;
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;

            LUE_Mosaic.Properties.DataSource = fluent.ViewModel.MosaicList;
            fluent.SetBinding(LUE_Mosaic, l => l.Enabled, x => x.LUEMoasicEnable);
            fluent.SetBinding(LUE_Mosaic, l => l.EditValue, x => x.MosaicIndex);
            fluent.SetBinding(Btn_MoveMosaic, l => l.Enabled, x => x.BtnMoveMosaicEnable);
            fluent.SetBinding(LUE_X, l => l.EditValue, x => x.ColX);
            fluent.SetBinding(LUE_Y, l => l.EditValue, x => x.RowY);
            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.MoveVel);
            fluent.SetBinding(TE_InspectTimes, l => l.Text, x => x.InspectCounts);
            fluent.SetBinding(Btn_Capture0, l => l.Enabled, x => x.IsCapture0Enable);
            fluent.SetBinding(Btn_Focus, l => l.Enabled, x => x.IsFocusEnable);          
            fluent.SetBinding(Btn_Inspect, l => l.Enabled, x => x.IsInspectEnable);
            fluent.SetBinding(Btn_StopInspect, l => l.Enabled, x => x.IsStopInspectEnable);
            fluent.SetBinding(Btn_FinishGolden, l => l.Enabled, x => x.IsFinishGoldenEnable);
            fluent.SetBinding(TE_InspectTimes, l => l.Enabled, x => x.IsInspectTimesEnable);
        }
        public void SendNotifyOuter()
        {
            OnNotifyOuter?.Invoke();
        }
    }
}
