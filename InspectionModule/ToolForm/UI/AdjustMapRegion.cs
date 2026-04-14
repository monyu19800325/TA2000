using HalconDotNet;
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
    public partial class AdjustMapRegion : DevExpress.XtraEditors.XtraForm
    {
        public AdjustMapRegion(ProductSettingToolViewModel productSettingToolViewModel)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(productSettingToolViewModel);
        }
        void InitializeBindings(ProductSettingToolViewModel productSettingToolViewModel)
        {
            var fluent = mvvmContext1.OfType<AdjustMapRegionViewModel>();

            LUE_MapIndex.Properties.DataSource = fluent.ViewModel.MapIndexList;

            fluent.ViewModel.HWindowControlMap = hWindow_Map;
            fluent.ViewModel.Init(productSettingToolViewModel);

            fluent.BindCommand(Btn_Set, x => x.Set());

            fluent.SetBinding(LUE_MapIndex, le => le.EditValue, x => x.MapIndex);
            fluent.SetBinding(TE_ExtendedWidth, te => te.Text, x => x.ExtendedWidth);
            fluent.SetBinding(TE_ExtendedHeight, te => te.Text, x => x.ExtendedHeight);

            this.FormClosing += fluent.ViewModel.FormClosing;
        }
    }
}
