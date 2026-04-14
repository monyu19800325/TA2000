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
using VisionController2.MosaicController;

namespace TA2000Modules
{
    public partial class MosaicControlForm : DevExpress.XtraEditors.XtraForm
    {

        MVVMContextFluentAPI<MosaicControlViewModel> _fluent;

        public MosaicControlForm(CalibrationModule module)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module);
        }

        void InitializeBindings(CalibrationModule module)
        {
            var fluent = mvvmContext1.OfType<MosaicControlViewModel>();
            _fluent = fluent;
            fluent.ViewModel.Init(module);

            //按鍵事件
            fluent.BindCommand(Btn_CaptureAll, x => x.CaptureAll);
            fluent.BindCommand(Btn_CaptureCurrent, x => x.CaptureSelect);
            fluent.BindCommand(Btn_Save, x => x.Save);
            fluent.BindCommand(Btn_AZ1_Move, x => x.AZ1_Move);
            fluent.BindCommand(Btn_SetProductPos, x => x.SetProductPos);
            fluent.BindCommand(Btn_MultiProductCapture, x => x.MulitProductMosaicCalib);

            //DataSource
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;
            LUE_X.Properties.DataSource = fluent.ViewModel.XList;
            LUE_Y.Properties.DataSource = fluent.ViewModel.YList;

            //參數
            fluent.SetBinding(LUE_Vel, obj => obj.EditValue, x => x.MoveVel);
            fluent.SetBinding(LUE_X, obj => obj.EditValue, x => x.SelectX);
            fluent.SetBinding(LUE_Y, obj => obj.EditValue, x => x.SelectY);
            fluent.SetBinding(TE_AZ1Pos, obj => obj.Text, x => x.AZ1Pos);


            //Btn_SetProductPos.Visible = false;//測試再打開

        }

        public void SetMosaicForm(MosaicForm mosaicForm)
        {
            _fluent.ViewModel.MosaicForm = mosaicForm;
        }

    }
}
