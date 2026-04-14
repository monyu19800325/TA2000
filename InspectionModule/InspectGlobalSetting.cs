using TA2000Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    public partial class InspectGlobalSetting : UserControl, IDisposable
    {
        public InspectGlobalSetting(InspectionModuleParam param)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(param);
        }

        void InitializeBindings(InspectionModuleParam param)
        {
            var fluent = mvvmContext1.OfType<InspectGlobalSettingViewModel>();

            fluent.ViewModel.Init(param);

            fluent.SetBinding(TE_Port, x => x.Text, x => x.Port);
            fluent.SetBinding(TE_Timeout, x => x.Text, x => x.Timeout);
            fluent.SetBinding(TE_OnlineTimeout, x => x.Text, x => x.InspectOnlineTimeout);
            fluent.SetBinding(TE_DataTimeout, x => x.Text, x => x.DataTimeout);
            fluent.SetBinding(TE_CaptureTimeout, x => x.Text, x => x.CaptureTimeout);
            fluent.SetBinding(TE_TrayReportPath, x => x.Text, x => x.TrayReportPath);
            fluent.SetBinding(CE_UseLotOldData, x => x.Checked, x => x.IsUseLotOldData);

            fluent.BindCommand(Btn_SelectTrayPath, b => b.OpenSelectTrayReportPath);
        }
    }
}
