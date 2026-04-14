using DevExpress.Utils.MVVM;
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
using static HTA.MainController.MainController;

namespace TA2000Modules
{
    public partial class CrossCenterControl : DevExpress.XtraEditors.XtraForm
    {
        private CalibrationModule _calibrationModule;
        public CrossCenterControl(CalibrationModule module)
        {
            InitializeComponent();
            _calibrationModule = module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<CrossCenterControlViewModel>();

            fluent.ViewModel.Initial(_calibrationModule);

            //按鍵事件     
            fluent.BindCommand(Btn_Save, module => module.Save);
            fluent.BindCommand(Btn_Camera_Y_Move, m => m.MoveAY1);
            fluent.BindCommand(Btn_Camera_Z_Move, m => m.MoveBZ1);
            fluent.BindCommand(Btn_Carrier_X_Move, m => m.MoveBX1);           

            //DataSource
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;

            //參數          
            fluent.SetBinding(TE_AY1Offset, obj => obj.Text, module => module.AY1Offset);
            fluent.SetBinding(TE_AZ1Offset, obj => obj.Text, module => module.BZ1Offset);
            fluent.SetBinding(TE_BX1Offset, obj => obj.Text, module => module.BX1Offset);
          
            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.Vel);

            fluent.SetBinding(TS_ProductJacking, obj => obj.IsOn, module => module.Product_IsJacking);
            fluent.SetBinding(TS_Productaside, obj => obj.IsOn, module => module.Product_IsAside);
        }
    }
}
