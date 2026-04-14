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

namespace TA2000Modules
{
    public partial class LightCalibControl : DevExpress.XtraEditors.XtraForm
    {
        private CalibrationModule _calibrationModule;
        public LightCalibControl(CalibrationModule module)
        {
            InitializeComponent();
            _calibrationModule = module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<LightCalibControlViewModel>();

            fluent.ViewModel.Initial(_calibrationModule);

            //按鍵事件
            fluent.BindCommand(Btn_BX1Move, module => module.Cam_BX1_Move);
            fluent.BindCommand(Btn_Save, module => module.Save);
            fluent.BindCommand(Btn_AY1Move, module => module.AY1_Move);
            fluent.BindCommand(Btn_BZ1Move, module => module.BZ1_Move);
            fluent.BindCommand(Btn_AY1MoveGrayCard, module => module.GrayAY1Move);
            fluent.BindCommand(Btn_AZ1MoveGrayCard, module => module.GrayAZ1Move);

            //DataSource
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;

            //參數
            fluent.SetBinding(TE_Cam_XOffset, obj => obj.Text, module => module.X1PosLightCalib);
            fluent.SetBinding(TE_Cam_ZOffset, obj => obj.Text, module => module.Z1PosLightCalib);         
            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.Vel);
            fluent.SetBinding(TE_AY1Pos, obj => obj.Text, module => module.Y1PosLightCalib);
            fluent.SetBinding(TE_GrayCardAY1Pos, obj => obj.Text, module => module.GrayCardY1Pos);
            fluent.SetBinding(TE_GrayCardAZ1Pos, obj => obj.Text, module => module.GrayCardZ1Pos);
        }
    }
}
