using HTA.CdiAuthorityControl;
using HTAMachine.Machine;
using HTAMachine.Module.DockPanel;
using HTAMachine.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AroundIOModule.Properties;

namespace TA2000Modules
{
    [RegisterCdiDocker]
    public partial class ProcessMode : DevExpress.XtraEditors.XtraUserControl, IInjectModule, ISupportAuthority, IMultilingual
    {
        private AroundIOModule _module;

        public ProcessMode()
        {
            InitializeComponent();
        }


        public void InjectModule(IModule module)
        {
            _module = (AroundIOModule)module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(_module);
        }

        void InitializeBindings(AroundIOModule module)
        {
            var fluent = mvvmContext1.OfType<ProcessModeViewModel>();
            fluent.ViewModel.Parameter = module;

            fluent.SetBinding(lbl_ProcessMode, c => c.Text, m => m.ProcessMode);
            fluent.SetBinding(lbl_MachineState, c => c.Text, m => m.MachineState);
            fluent.SetBinding(lbl_MachineState, c => c.ForeColor, m => m.MechineStateColor);
            fluent.SetBinding(Btn_DoorLock, c => c.Text, m => m.DoorLockText);
            fluent.SetBinding(lbl_ProcessMode, c => c.Visible, m => m.ProcessModeVisible);
            fluent.SetBinding(labelControl, c => c.Visible, m => m.ProcessModeVisible);
            fluent.SetBinding(Btn_DoorLock, c => c.Visible, m => m.DoorTextVisible);
            //fluent.SetBinding(Btn_ChangeMode, c => c.Visible, m => m.BtnChangeModeVisible);

            Btn_ChangeMode.Visible = false;
            //fluent.BindCommand(Btn_ChangeMode, x => x.ChangeMode());
            //fluent.SetBinding(chb_DoorLock, s => s.Checked, c => c.DoorLock);
            fluent.BindCommand(Btn_DoorLock,x=>x.DoorLockUnlock());


            //Btn_DoorLock.Click += fluent.ViewModel.Chb_DoorLock_CheckedChanged;
        }

        public void AuthorityChanged(LevelInfo level)
        {
        }


        public void SetLanguage(ComponentResourceManager resources)
        {
            System.ComponentModel.ComponentResourceManager resources2 = new System.ComponentModel.ComponentResourceManager(typeof(ProcessMode));
            resources2.ApplyResources(this, "$this");
            SetLang(this.Controls, resources2);

            var msg = "";
            switch (_module.CurrentModelArgs.ModelName)
            {
                //case ModelEm.一般模式:
                //    msg = Resources.NormalMode;
                //    break;
                case ModelEm.安全模式:
                    msg = Resources.SafeMode;
                    break;
                case ModelEm.維修模式:
                    msg = Resources.MaintanenceMode;
                    break;
            }
            //if (_module.VersionController1.Version == 0)
            if (_module.SettingService.SystemSetting.SelectVersion == "M00")
            {
                msg += _module.Y065000_設備安全門電磁鎖.CheckIO() ? Resources.Lock : Resources.Unlock;
            }


            lbl_ProcessMode.Text = $@"{msg}";

            var msg1 = "";
            switch (_module._currentState)
            {
                case HtaMachineController.MachineStateEm.Running:
                    msg1 = Resources.Running;
                    break;
                case HtaMachineController.MachineStateEm.UnInitial:
                    msg1 = Resources.Uninitial;
                    break;
                case HtaMachineController.MachineStateEm.Idle:
                    msg1 = Resources.Idle;
                    break;
            }

            lbl_MachineState.Text = msg1;

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
    }
}
