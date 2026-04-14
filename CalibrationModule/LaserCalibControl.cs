using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.MVVM;
using DevExpress.XtraEditors;
using TA2000Modules;

namespace TA2000Modules
{
    public partial class LaserCalibControl : DevExpress.XtraEditors.XtraForm
    {
        private CalibrationModule _calibrationModule;
        public LaserCalibControl(CalibrationModule module)
        {
            InitializeComponent();

            _calibrationModule = module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<LaserCalibControlViewModel>();

            fluent.ViewModel.Initial(_calibrationModule);

            //按鍵事件     
            fluent.BindCommand(Btn_Save, module => module.Save);
            fluent.BindCommand(Btn_Camera_X_Move, m => m.MoveAX1);           
            fluent.BindCommand(Btn_Carrier_Y_Move, m => m.MoveBY1);
            fluent.BindCommand(Btn_Camera_Z_Move, m => m.MoveAZ1);

            //fluent.BindCommand(Btn_ZeroCalibration, m => m.ZeroCalibration);
            fluent.BindCommand(Btn_LaserCenterSearch, m => m.CenterSearch);
            fluent.BindCommand(Btn_JudgeTest, m => m.MoveToCameraCenter);
            fluent.BindCommand(Btn_LaserCenterMove, m => m.LaserCenterMove);

            fluent.BindCommand(Btn_Offset_X_Move, m => m.Offset_X_Move);
            fluent.BindCommand(Btn_Offset_Y_Move, m => m.Offset_Y_Move);

            //DataSource
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;

            //參數          
            fluent.SetBinding(TE_AX1Offset, obj => obj.Text, module => module.AX1Offset);           
            fluent.SetBinding(TE_BY1Offset, obj => obj.Text, module => module.BY1Offset);
            fluent.SetBinding(TE_AZ1Offset, obj => obj.Text, module => module.AZ1Offset);

            fluent.SetBinding(TE_LaserCenterX, obj => obj.Text, module => module.LaserCenterX);
            fluent.SetBinding(TE_LaserCenterY, obj => obj.Text, module => module.LaserCenterY);

            fluent.SetBinding(TE_OffsetX, obj => obj.Text, module => module.LaserOffsetX);
            fluent.SetBinding(TE_OffsetY, obj => obj.Text, module => module.LaserOffsetY);

            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.Vel);

            //DataSource
            LUE_Dir.Properties.DataSource = fluent.ViewModel.DirList;
            LUE_Step.Properties.DataSource = fluent.ViewModel.StepList;

            fluent.SetBinding(LUE_Dir, obj => obj.EditValue, module => module.MoveDir,
               intValue =>
               {
                   return fluent.ViewModel.DirList[fluent.ViewModel.MoveDir];
               },
               objectValue =>
               {
                   return fluent.ViewModel.DirList.IndexOf((ItemString)objectValue);
               });
            fluent.SetBinding(LUE_Step, obj => obj.EditValue, module => module.Step,
                intValue =>
                {
                    return fluent.ViewModel.StepList[fluent.ViewModel.Step];
                },
                objectValue =>
                {
                    return fluent.ViewModel.StepList.IndexOf((ItemString)objectValue);
                });
        }

        private void Btn_LaserCenterSearch_Enter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();

            toolTip.AutoPopDelay = 5000;   // 顯示多久 (ms)
            toolTip.InitialDelay = 500;    // 滑鼠停留多久後顯示
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(Btn_LaserCenterSearch, "First manual move to center of Gauge Block");
        }
    }
}