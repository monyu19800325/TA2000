//using HTA.CdiAuthorityControl;
using DevExpress.Utils.MVVM;
using HTA.CdiAuthorityControl;
using HTA.Utility.Base;
using HTAMachine.Machine;
using HTAMachine.Module;
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
    public partial class BarcodeTestForm : DevExpress.XtraEditors.XtraForm,  ISupportAuthority
    {
        private InspectionModule _module;
        public BarcodeTestForm(InspectionModule module)
        {
            InitializeComponent();
            _module = (InspectionModule)module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(_module);
        }

        void InitializeBindings(InspectionModule module)
        {
            var fluent = mvvmContext1.OfType<BarcodeTestFormViewModel>();
            fluent.ViewModel.Parameter = module;

            fluent.SetBinding(lbl_BarcodeId, c => c.Text, m => m.BarcodeId);
            fluent.BindCommand(btn_BarcodeTest, m => m.BarcodeTest);

            //按鍵事件
            fluent.BindCommand(Btn_Cam_X_Move, m => m.Cam_X_Move); 
                 fluent.BindCommand(Btn_Cam_Z_Move, m => m.Cam_Z_Move);
            fluent.BindCommand(Btn_Save, m => m.Save);
            fluent.BindCommand(Btn_Carrier_Move, m => m.Carrier_Move);
            fluent.BindCommand(Btn_Finish, m => m.Finish);

            //DataSource
            LUE_Vel.Properties.DataSource = fluent.ViewModel.VelList;
            LUE_BarcodeCatchMode.Properties.DataSource = fluent.ViewModel.BarcodeCatahModeList;

            //參數
            fluent.SetBinding(TE_Cam_XOffset, obj => obj.Text, m => m.AX1Offset_X);
            fluent.SetBinding(TE_Cam_ZOffset, obj => obj.Text, m => m.AX1Offset_Z);
            fluent.SetBinding(TE_Carrier_YOffset, obj => obj.Text, m => m.BY1Offset);
            fluent.SetBinding(LUE_Vel, l => l.EditValue, x => x.Vel);
            fluent.SetBinding(LUE_BarcodeCatchMode, l => l.EditValue, x => x.BarcodeCatchMode);

       
            fluent.SetBinding(labelControl6, l => l.Visible, x => x.LabelControl6_Visible);
            fluent.SetBinding(labelControl9, l => l.Visible, x => x.LabelControl9_Visible);
            fluent.SetBinding(TE_Cam_ZOffset, l => l.Visible, x => x.TE_Cam_ZOffset_Visible);
            fluent.SetBinding(Btn_Cam_Z_Move, l => l.Visible, x => x.Btn_Cam_Z_Move_Visible);

            fluent.SetBinding(TS_ProductPosition, obj => obj.IsOn, x => x.Product_IsPosition);
            fluent.SetBinding(TS_Productaside, obj => obj.IsOn, x => x.Product_IsAside);

            fluent.ViewModel.Initial();

            if (LUE_BarcodeCatchMode.EditValue.ToString()==BarcodeCatchModeEm.相機模式.ToString())
            {
                //labelControl6.Visible = true;
                //labelControl9.Visible = true;
                //TE_Cam_ZOffset.Visible = true;
                //Btn_Cam_Z_Move.Visible = true;
            }
        }

        //void InitializeBindings()
        //{
        //    var fluent = mvvmContext1.OfType<BarcodeTestFormViewModel>();
        //}

        public void AuthorityChanged(LevelInfo level)
        {
            //btn_BarcodeTest.Enabled = level.Level > 2;
            //lbl_BarcodeId.Enabled = level.Level > 3;
        }
    }
}
