using AroundIOModule.Properties;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraEditors;
using HTAMachine.Machine;
using HTAMachine.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class ProcessModeViewModel : ISupportParameter
    {
        private AroundIOModule _param;

        private IDispatcherService DispatcherService => this.GetService<IDispatcherService>();

        public object Parameter
        {
            get => _param;
            set
            {
                if (value is AroundIOModule p)
                {
                    _param = p;
                    _param.DataChange += OnNotifyDataChange;
                    DispatcherService.BeginInvoke(() =>
                    {
                        this.RaisePropertiesChanged();
                    });
                }
            }
        }

        private void OnNotifyDataChange(object sender, EventArgs args)
        {
            DispatcherService.BeginInvoke(() =>
            {
                this.RaisePropertiesChanged();
            });
        }

        public string ProcessMode
        {
            get
            {
                var msg = "";
                //if(_param.VersionController1.Version == 0)
                if (_param.SettingService.SystemSetting.SelectVersion == "M00")
                {
                    switch (_param.CurrentModelArgs.ModelName)
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
                    msg += "-";
                    msg += _param.Y065000_設備安全門電磁鎖.CheckIO() ? Resources.Lock : Resources.Unlock;
                }
                return $@"{msg}";
            }
            set
            {

            }
        }

        public bool ProcessModeVisible
        {
            get
            {
                //if(_param.VersionController1.Version == 0)
                if (_param.SettingService.SystemSetting.SelectVersion == "M00")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string MachineState
        {
            get
            {
                var msg1 = "";
                switch (_param._currentState)
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
                return msg1;
            }
            set
            {

            }
        }

        private Color _machineShowColor = Color.Black;
        public Color MechineStateColor
        {
            get
            {
                return _machineShowColor;
            }
            set
            {
                _machineShowColor = value;
            }
        }

        private string _doorLockText;

        public string DoorLockText
        {
            get
            {
                //if(_param.VersionController1.Version == 0)
                if (_param.SettingService.SystemSetting.SelectVersion == "M00")
                {
                    if (_param.Y065000_設備安全門電磁鎖.CheckIO())
                    {
                        _doorLockText = Resources.DoorLock;
                    }
                    else
                    {
                        _doorLockText = Resources.DoorUnlock;
                    }
                    return _doorLockText;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                //if (_param.VersionController1.Version == 0)
                if (_param.SettingService.SystemSetting.SelectVersion == "M00")
                {
                    if (_param.Y065000_設備安全門電磁鎖.CheckIO())
                    {
                        _doorLockText = Resources.DoorLock;
                    }
                    else
                    {
                        _doorLockText = Resources.DoorUnlock;
                    }
                }
            }
        }

        public bool DoorTextVisible
        {
            get
            {
                //if (_param.VersionController1.Version == 0)
                if (_param.SettingService.SystemSetting.SelectVersion == "M00")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool BtnChangeModeVisible
        {
            get
            {
                //if (_param.VersionController1.Version == 0)
                if (_param.SettingService.SystemSetting.SelectVersion == "M00")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DoorLock
        {
            get => _param.Y065000_設備安全門電磁鎖.CheckIO();
            set
            {
                if (_param._currentState == HTAMachine.Machine.HtaMachineController.MachineStateEm.Running)
                    return;

                _param.Y065000_設備安全門電磁鎖.SetIO(value);
            }
        }

        public void ChangeMode()
        {
            if (_param.CurrentModelArgs.ModelName == ModelEm.安全模式)
            {
                _param.ButtonIsRepair = true;
            }
            else
            {
                _param.ButtonIsRepair = false;
            }
        }

        public void DoorLockUnlock()
        {


            if (!_param.X064004_電磁鎖鎖定檢知.CheckIO())
            {
                _param.Y065000_設備安全門電磁鎖.SetIO(true);
                _machineShowColor = Color.Red;
            }
            else
            {
                if (_param._currentState == HTAMachine.Machine.HtaMachineController.MachineStateEm.Running)
                    return;
                _param.Y065000_設備安全門電磁鎖.SetIO(false);
                _machineShowColor = Color.Green;
            }
            this.RaisePropertiesChanged();
        }

        public void Chb_DoorLock_CheckedChanged(object sender, EventArgs e)
        {
            if (_param._currentState == HTAMachine.Machine.HtaMachineController.MachineStateEm.Running)
                return;

            if (sender is SimpleButton checkButton)
            {
                if (_param.X064004_電磁鎖鎖定檢知.CheckIO())
                {
                    checkButton.Text = Resources.DoorLock;
                    _param.Y065000_設備安全門電磁鎖.SetIO(true);
                    _machineShowColor = Color.Red;
                }
                else
                {
                    checkButton.Text = Resources.DoorUnlock;
                    _param.Y065000_設備安全門電磁鎖.SetIO(false);
                    _machineShowColor = Color.Green;
                }
            }
            this.RaisePropertiesChanged();
        }
    }
}
