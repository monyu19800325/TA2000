using DevExpress.Mvvm.Xpf;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraPrinting;
using Hta.MotionBase;
using HTA.MainController;
using HTAMachine.Machine;
using HyperInspection;
using TA2100Modules;
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
using DevExpress.Xpo.DB.Helpers;

namespace TA2100Modules
{
    public partial class APS_TestForm : DevExpress.XtraEditors.XtraForm, IMultilingual
    {


        private MVVMContextFluentAPI<TeachControlWizardViewModel> fluent;
        private HTAMachine.Machine.IModule _module;
        private FlowForm _flowForm;
        //private InspectionModule _inspectionModule;
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



        public APS_TestForm(InspectionModule module)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module);
        }

        public void SetLanguage(ComponentResourceManager resources)
        {
            System.ComponentModel.ComponentResourceManager resources2 = new System.ComponentModel.ComponentResourceManager(typeof(TeachControlWizardViewModel));
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


        void InitializeBindings(InspectionModule module)
        {
            var fluent = mvvmContext1.OfType<APS_TestFormViewModel>();
            fluent.ViewModel.Init(module);
            fluent.ViewModel.SetView(this);
            fluent.ViewModel.WindowControl = hWindowControl1;


            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;
            LUE_Axis.Properties.DataSource = fluent.ViewModel.AxisList;
            LUE_Select.Properties.DataSource = fluent.ViewModel.SelectList;
            LUE_RectCircleSelect.Properties.DataSource = fluent.ViewModel.RectCircleList;

            fluent.SetBinding(trackBar1, t => t.Value, x => x.ThresholdMin);
            fluent.SetBinding(trackBar2, t => t.Value, x => x.ThresholdMax);
            fluent.SetBinding(labelControl12, t => t.Text, x => x.ThresholdMax);
            fluent.SetBinding(labelControl13, t => t.Text, x => x.ThresholdMin);
            fluent.SetBinding(toggleSwitch1, t => t.IsOn, x => x.LiveOn);
            fluent.SetBinding(LB_XPixelPos, t => t.Text, x => x.XPixelPos);
            fluent.SetBinding(LB_YPixelPos, t => t.Text, x => x.YPixelPos);
            fluent.SetBinding(LB_RectPosX, obj => obj.Text, m => m.RectPosX);
            fluent.SetBinding(LB_RectPosY, obj => obj.Text, m => m.RectPosY);
            fluent.SetBinding(LB_RectRegPosX, t => t.Text, x => x.RectRegPosX);
            fluent.SetBinding(LB_RectRegPosY, t => t.Text, x => x.RectRegPosY);
            fluent.SetBinding(LB_RectPhi, t => t.Text, x => x.RectPhi);

            fluent.SetBinding(LB_RectCirclePosX, t => t.Text, x => x.XPixelPosRect);
            fluent.SetBinding(LB_RectCirclePosY, t => t.Text, x => x.YPixelPosRect);

            fluent.BindCommand(Btn_SetLight, x => x.SetLight());
            fluent.BindCommand(Btn_DrawROI, x => x.DrawROI());

            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.MoveVel);
            fluent.SetBinding(LUE_Axis, l => l.EditValue, x => x.Move_Axis);
            fluent.SetBinding(LUE_RectCircleSelect, l => l.EditValue, x => x.RectCircleIndex);

            fluent.SetBinding(tex_StepValue, obj => obj.Text, m => m.stepSetValue);
            fluent.SetBinding(tex_POSFocusZ, obj => obj.Text, m => m.AZ1_Focus_Offset);
            fluent.SetBinding(tex_POSStep0, obj => obj.Text, m => m.Step0_Offset);
            fluent.SetBinding(tex_POSStep1, obj => obj.Text, m => m.Step1_Offset);
            fluent.SetBinding(tex_DelayTime, obj => obj.Text, m => m.DelaySeconds);
            fluent.SetBinding(lab_CurrentStep, obj => obj.Text, m => m.stepCurrentValue);
            fluent.SetBinding(lab_StepValue, obj => obj.Text, m => m.stepSetValue);

            fluent.BindCommand(btn_FocusMove, x => x.AxisMove_Focus);
            fluent.BindCommand(btn_Step0Move, x => x.AxisMove_Step_0);
            fluent.BindCommand(btn_Step1Move, x => x.AxisMove_Step_1);

            fluent.BindCommand(btn_StepRun, x => x.LoopStart);
            fluent.BindCommand(btn_StepStop, x => x.LoopEnd);


            fluent.BindCommand(Btn_DrawRectCircle, x => x.DrawRectCircle);
            fluent.BindCommand(Btn_CheckRectCircleImage, x => x.CheckRectCircleImage);
            fluent.BindCommand(Btn_ShowRect, x => x.ShowRect);

            //fluent.SetBinding(btn_StepRun, b => b.Enabled, x => x._btnRunEnable);
            //fluent.SetBinding(btn_Step0Move, b => b.Enabled, x => x._btnRunEnable);
            //fluent.SetBinding(btn_Step1Move, b => b.Enabled, x => x._btnRunEnable);
            //fluent.SetBinding(btn_FocusMove, b => b.Enabled, x => x._btnRunEnable);


            fluent.BindCommand(btn_SavePOS, x => x.Save);
            fluent.BindCommand(btn_LoadImage, x => x.LoadImage());
            fluent.BindCommand(btn_CheckImage, x => x.checkImage());

            fluent.SetBinding(tra_AxisSP, obj => obj.Maximum, m => m.AxisSP_Max_Value);
            fluent.SetBinding(tra_AxisSP, obj => obj.Minimum, m => m.AxisSP_Min_Value);
            fluent.SetBinding(tra_AxisSP, obj => obj.Value, m => m.CurrentSpeed_Value);


            fluent.SetBinding(lab_CurrentSP, obj => obj.Text, m => m.CurrentSpeed_Value);

            fluent.SetBinding(TE_Length, obj => obj.Text, m => m.Length);
            fluent.SetBinding(TE_MeasureThreshold, obj => obj.Text, m => m.MeasureThreshold);
            fluent.SetBinding(TE_MeasureNum, obj => obj.Text, m => m.MeasureNum);
            fluent.SetBinding(LUE_Select, obj => obj.EditValue, m => m.SelectText);
        }

    }
}
   