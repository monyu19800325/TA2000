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
    public partial class Pxl3DCalibControl : DevExpress.XtraEditors.XtraForm
    {
        private CalibrationModule _calibrationModule;
        public Pxl3DCalibControl(CalibrationModule module)
        {
            InitializeComponent();
            _calibrationModule = module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<Pxl3DCalibControlViewModel>();

            fluent.ViewModel.Initial(_calibrationModule);

            //按鍵事件
            fluent.BindCommand(Btn_CX1Move, module => module.CX1_Move);

            fluent.BindCommand(Btn_AY1Move, module => module.AY1_Move);
            fluent.BindCommand(Btn_AZ1Move, module => module.AZ1_Move);

            fluent.BindCommand(Btn_Save, module => module.Save);

            //DataSource
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;

            //參數
            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.Vel);

            fluent.SetBinding(TE_Cam_XOffset, obj => obj.Text, module => module.X1Pos3D);
            fluent.SetBinding(TE_Cam_ZOffset, obj => obj.Text, module => module.Z1Pos3D);
            fluent.SetBinding(TE_AY1Pos, obj => obj.Text, module => module.Y1Pos3D);
        }
    }
}
