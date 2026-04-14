using DevExpress.Utils.MVVM;
using HTA.MainController;
using HyperInspection;
using TA2000Modules.ToolForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TA2000Modules;

namespace TA2000Modules
{
    public partial class TestMosaicForm : DevExpress.XtraEditors.XtraForm
    {
        public TestMosaicForm()
        {
            InitializeComponent();
        }

        private MVVMContextFluentAPI<TeachControlWizardViewModel> fluent;
        private HTAMachine.Machine.IModule _module;
        private FlowForm _flowForm;
        private InspectionModule _inspectionModule;
        private InspectionProductParam _param;
        private double _leftOffsetDist;
        private double _leftBaseDist;
        private double _rightBaseDist;
        private double _rightOffsetDist;
        private MainController _mainController;
        private HTA.IFramer.IStationFramer _useFramer;
        private HTA.LightServer.ILighter _useLighter;
        private HTA.TriggerServer.ITriggerChannel _useTrigger;
        private List<double> _groupInfo;
        private string _hintStr;
        private BoatCarrier _carrier;
        private int _moveLimit = 20;
        private ImageWindowAccessor _accessor;
        public TestMosaicForm(HTAMachine.Machine.IModule module, FlowForm flowForm)
        {
            InitializeComponent();
            SetupFlowForm(flowForm);
            SetupModule(module);
        }

        public void SetupFlowForm(Form flowForm)
        {
            if (flowForm == null) return;
            _flowForm = (FlowForm)flowForm;
        }

        public void SetupModule(HTAMachine.Machine.IModule module)
        {
            _module = module;
            _inspectionModule = (InspectionModule)_module;
            _param = _inspectionModule.ProductParam;
            _carrier = _inspectionModule.CurrentTrayCarrier;

            _mainController = (MainController)_inspectionModule.VisionController_Mosaic;

            _useFramer = _mainController.Framer;
            _useLighter = _mainController.Lighter;
            _useTrigger = _mainController.Trigger1;
            _groupInfo = new List<double> { 50 * 4.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _hintStr = null;
            _accessor = _flowForm.GenAccessor();
            //setup something

            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<TestMosaicViewModel>();

            //ViewModel委派
            fluent.ViewModel.Vision = _inspectionModule;
            fluent.ViewModel.TotalCount = _mainController.ProductSetting.TotalCapture;
            fluent.ViewModel.SetLighting += _mainController.Lighter.SetLight;
            fluent.ViewModel.SaveParam += _inspectionModule.SaveVisionProductParam;
            fluent.ViewModel.OnClose = this.Close;
            fluent.ViewModel.ChangeCamAndCaptureIndexTrigger += _flowForm.ChangeCamAndCapIdx;
            //fluent.ViewModel.GetLightingPercentage += _flowForm.LightingPersentage;
            _flowForm.FlowFormImageIn += fluent.ViewModel.FlowFormImageIn;//待測試，如果要用最新的VisionController2的話，再打開這段
            fluent.ViewModel.OnRefreshBuffer = _flowForm.OnBufferRefresh;
            fluent.ViewModel.FlowFormData = _flowForm;
            fluent.ViewModel.OnLog = _inspectionModule.OnAddTrace;

            fluent.ViewModel.Initial(_param, _carrier, _leftBaseDist, _rightBaseDist,
                _leftOffsetDist, _rightOffsetDist, _moveLimit, _accessor, _useFramer,
                _useLighter, _useTrigger, _groupInfo, _hintStr);


            fluent.BindCommand(Btn_Capture, c => c.Capture);
            fluent.BindCommand(Btn_Az1Move, c => c.Az1Move);
            fluent.BindCommand(Btn_CreateMap, c => c.CreateMap);
            fluent.BindCommand(Btn_CalMosaic, c => c.CalMosaic);
            fluent.BindCommand(Btn_Run, c => c.Run);
            fluent.BindCommand(Btn_Stop, c => c.Stop);
            fluent.BindCommand(Btn_Save, c => c.Save);
            fluent.BindCommand(Btn_MoveCapture0, c => c.Index0MoveCapture);

            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;
            LUE_CaptureIndex.Properties.DataSource = fluent.ViewModel.CaptureIndexList;

            fluent.SetBinding(TE_Az1Pos, x => x.Text, v => v.Az1Pos);
            fluent.SetBinding(TE_Index0X, x => x.Text, v => v.Index0PosX);
            fluent.SetBinding(TE_Index0Y, x => x.Text, v => v.Index0PosY);
            fluent.SetBinding(TE_CycleCount, x => x.Text, v => v.CycleCount);
            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.MoveVel);
            fluent.SetBinding(LUE_CaptureIndex, l => l.EditValue, x => x.CaptureIndex);
            fluent.SetBinding(SE_MosaicXCount, l => l.Value, x => x.MosaicXCount);
            fluent.SetBinding(SE_MosaicYCount, l => l.Value, x => x.MosaicYCount);
            fluent.SetBinding(CB_SaveImage, l => l.Checked, x => x.SaveImage);
            fluent.SetBinding(CB_UseSingleSeam, l => l.Checked, x => x.UseSingleSeam);
        }
    }
}
