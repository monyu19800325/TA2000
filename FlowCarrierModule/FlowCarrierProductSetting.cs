//using FlowCarrierModule;
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
    public partial class FlowCarrierProductSetting : UserControl
    {
        public FlowCarrierProductSetting(FlowCarrierProductParam param)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(param);
        }

        void InitializeBindings(FlowCarrierProductParam param)
        {
            var fluent = mvvmContext1.OfType<FlowCarrierProductSettingViewModel>();

            fluent.ViewModel.Init(param);

            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;

            fluent.SetBinding(TE_LoadTimeout, x => x.Text, x => x.LoadTimeout);
            fluent.SetBinding(TE_UnloadTimeout, x => x.Text, x => x.UnloadTimeout);
            fluent.SetBinding(TE_CylinderTimeout, x => x.Text, x => x.CylinderTimeout);
            fluent.SetBinding(TE_DecTimeout, x => x.Text, x => x.DecTimeout);
            fluent.SetBinding(TE_StopTimeout, x => x.Text, x => x.StopTimeout);
            
            fluent.SetBinding(TopSV_Pick, x => x.Checked, x => x.TopSVPick);
            fluent.SetBinding(GuideSV_Pick, x => x.Checked, x => x.GuideSVPick);
            fluent.SetBinding(StopSV_Pick, x => x.Checked, x => x.StopSVPick);
            fluent.SetBinding(VC_Pick, x => x.Checked, x => x.VCPick);
            fluent.SetBinding(VCCheck_Pick, x => x.Checked, x => x.VCCheckPick);

            fluent.BindCommand(Btn_BoatBypass, x => x.BoatBypass());

            fluent.SetBinding(LUE_Vel, x => x.EditValue, x => x.Vel);

        }
    }
}
