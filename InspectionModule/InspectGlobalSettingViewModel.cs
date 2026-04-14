using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using TA2000Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class InspectGlobalSettingViewModel
    {
        public InspectionModuleParam InspectionModuleParam { get; set; } = new InspectionModuleParam();






        public int Port 
        { 
            get => InspectionModuleParam.Port;
            set 
            { 
                InspectionModuleParam.Port = value;
                this.RaisePropertyChanged(x => x.Port);
            }
        }

        public int Timeout
        {
            get => InspectionModuleParam.Timeout;
            set
            {
                InspectionModuleParam.Timeout = value;
                this.RaisePropertyChanged(x => x.Timeout);
            }
        }

        public int InspectOnlineTimeout
        {
            get => InspectionModuleParam.InspectOnlineTimeout;
            set
            {
                InspectionModuleParam.InspectOnlineTimeout = value;
                this.RaisePropertyChanged(x => x.InspectOnlineTimeout);
            }
        }

        public int DataTimeout
        {
            get => InspectionModuleParam.DataTimeout;
            set
            {
                InspectionModuleParam.DataTimeout = value;
                this.RaisePropertyChanged(x => x.DataTimeout);
            }
        }

        public int CaptureTimeout
        {
            get => InspectionModuleParam.CaptureTimeout;
            set
            {
                InspectionModuleParam.CaptureTimeout = value;
                this.RaisePropertyChanged(x => x.CaptureTimeout);
            }
        }

        public string TrayReportPath
        {
            get => InspectionModuleParam.TrayReportPath;
            set
            {
                InspectionModuleParam.TrayReportPath = value;
                this.RaisePropertyChanged(x => x.TrayReportPath);
            }
        }
        public bool IsUseLotOldData
        {
            get => InspectionModuleParam.IsUseLotOldData;
            set
            {
                InspectionModuleParam.IsUseLotOldData = value;
                this.RaisePropertyChanged(x => x.IsUseLotOldData);
            }
        }

        public InspectGlobalSettingViewModel()
        {
        }

        public void Init(InspectionModuleParam param)
        {
            InspectionModuleParam = param;
        }


        public void OpenSelectTrayReportPath()
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            if (path.ShowDialog() == DialogResult.OK)
            {
                TrayReportPath = path.SelectedPath + "\\";
            }
        }
    }
}
