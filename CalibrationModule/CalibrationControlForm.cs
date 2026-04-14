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
    public partial class CalibrationControlForm : DevExpress.XtraEditors.XtraForm
    {
        private CalibrationModule _calibrationModule;
        public CalibrationControlForm(CalibrationModule module)
        {
            InitializeComponent();
            _calibrationModule = module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<CalibrationControlViewModel>();

            fluent.ViewModel.Initial(_calibrationModule);

            //按鍵事件
            fluent.BindCommand(Btn_AX1_Move, module => module.Cam_AX1_Move);
            fluent.BindCommand(Btn_AZ1_Move, module => module.Cam_AZ1_Move);
            fluent.BindCommand(Btn_Save, module => module.Save);
            fluent.BindCommand(Btn_Carrier_Move, module => module.Carrier_Move);
           

            //DataSource
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;

            //參數
            fluent.SetBinding(TE_Cam_XOffset, obj => obj.Text, module => module.AX1Offset);
            fluent.SetBinding(TE_Cam_ZOffset, obj => obj.Text, module => module.AZ1Offset);
            fluent.SetBinding(TE_Carrier_YOffset, obj => obj.Text, module => module.BY1Offset);
            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.Vel);
            fluent.SetBinding(TS_ProductJacking, obj => obj.IsOn, module => module.Product_IsJacking);
            fluent.SetBinding(TS_Productaside, obj => obj.IsOn, module => module.Product_IsAside);            
        }
    }
}
