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
    public partial class InspectProductSetting : UserControl
    {
        public InspectProductSetting(InspectionModule module)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module);
        }

        void InitializeBindings(InspectionModule module)
        {
            var fluent = mvvmContext1.OfType<InspectProductSettingViewModel>();

            fluent.ViewModel.Init(module, Pick_dataGrid);

            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;
            LUE_FlyVel.Properties.DataSource = fluent.ViewModel.VelList;

            fluent.SetBinding(CE_Barcode, x => x.Checked, x => x.UseBoatBarcodeReader);
            fluent.SetBinding(CE_LaserMeasure, x => x.Checked, x => x.UseLaserMeasure);
            fluent.SetBinding(CE_InspectByPass, x => x.Checked, x => x.InspectByPass);
            fluent.SetBinding(CE_Reinspect, x => x.Checked, x => x.IsReinspect);
            fluent.SetBinding(CE_Pick, x => x.Checked, x => x.IsPick);
            fluent.SetBinding(LUE_Vel, x => x.EditValue, x => x.Vel);
            fluent.SetBinding(LUE_FlyVel, x => x.EditValue, x => x.FlyVel);
            fluent.SetBinding(TE_FlyVelPercent, x => x.EditValue, x => x.FlyVelPercent);
            fluent.SetBinding(TE_ReinspectCount, x => x.Text, x => x.ReinspectCount);
            fluent.SetBinding(TE_ReinspectCount, x => x.Visible, x => x.ReinspectVisible);
            fluent.SetBinding(labelControl4, x => x.Visible, x => x.ReinspectVisible);
            fluent.SetBinding(SE_FailAlarmCount, x => x.Value, x => x.FailAlarmCount);
            fluent.SetBinding(SE_RejectAlarmCount, x => x.Value, x => x.LotRejectAlarmCount);
            fluent.SetBinding(CE_IsFly, x => x.Checked, x => x.IsFly);

            Pick_dataGrid.CellClick += fluent.ViewModel.Pick_dataGrid_CellClick;
        }
    }
}
