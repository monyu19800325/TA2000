using DevExpress.Utils.MVVM;
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
    public partial class FlowCarrierGlobalSetting : UserControl
    {
        public FlowCarrierGlobalSetting(FlowCarrierGlobalParam param)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(param);
        }

        void InitializeBindings(FlowCarrierGlobalParam param)
        {
            var fluent = mvvmContext1.OfType<FlowCarrierGlobalSettingViewModel>();

            fluent.ViewModel.Init(param);


            fluent.SetBinding(TE_ServerPort, x => x.Text, x => x.ServerPort);
            fluent.SetBinding(TE_ClientIP, x => x.Text, x => x.ClientIP);
            fluent.SetBinding(TE_ClientPort, x => x.Text, x => x.ClientPort);
            fluent.SetBinding(RG_UpperCom, x => x.SelectedIndex, x => x.UpperType);
            fluent.SetBinding(RG_LowerCom, x => x.SelectedIndex, x => x.LowerType);
            fluent.SetBinding(groupBox2, x => x.Visible, x => x.TCPIPGroupVisible);
            fluent.SetBinding(CE_TCPIPuseXML, x => x.Checked, x => x.TCPIPUseXml);
            fluent.SetBinding(CE_UseInterLock, x => x.Checked, x => x.TCPIPUseInterLock);

            fluent.SetBinding(CE_EnableSendOtherDataServer, x => x.Checked, x => x.EnableOnlyDataTCPIPServer);
            fluent.SetBinding(CE_EnableSendOtherDataClient, x => x.Checked, x => x.EnableOnlyDataTCPIPClient);
            fluent.SetBinding(TE_OnlyDataServerPort, x => x.Text, x => x.OnlyDataServerPort);
            fluent.SetBinding(TE_OnlyDataClientPort, x => x.Text, x => x.OnlyDataClientPort);
        }
    }
}
