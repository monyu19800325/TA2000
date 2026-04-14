using FlowCarrierModule;
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
using System.Runtime.InteropServices;

namespace TA2000Modules
{
    public partial class VacuumSettingForm : DevExpress.XtraEditors.XtraForm
    {
       

        public VacuumSettingForm(FlowCarrierModule module)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module.Param, module);
            
        }

        void InitializeBindings(FlowCarrierGlobalParam param, FlowCarrierModule module)
        {
            
            var fluent = mvvmContext1.OfType<VacuumViewModel>();
            fluent.ViewModel.Init(param, module);

            //寫入上限數值
            fluent.SetBinding(TE_VCEstablishedValue, x => x.Text, x => x.CVBoatVacuumEstablishedValue);

            //燈號
            fluent.SetBinding(LB_VCState, x => x.BackColor, x => x.StateLight);
            fluent.SetBinding(LB_VCEstablished, x => x.BackColor, x => x.EstablishedLight);
            fluent.SetBinding(Btn_VCTrigger, x => x.ForeColor, x => x.btn_VCLight);
            fluent.SetBinding(Btn_SVTrigger, x => x.ForeColor, x => x.btn_SVLight);


            //修改文字內容
            fluent.SetBinding(Btn_VCTrigger, x => x.Text, x => x.btn_VCText);
            fluent.SetBinding(Btn_SVTrigger, x => x.Text, x => x.btn_SVText);


            //fluent.SetBinding(lab_VCValueCurrent,
            //    x => x.Text, x => "Vacuum Value (Current)  : " + x.CVBoatVacuumCurrentValue + "pa");
            //fluent.SetBinding(lab_VCValueEstablished,
            //    x => x.Text, x => "Vacuum Value (Established)  : " + x.CVBoatVacuumEstablishedValue + "pa");

            fluent.SetBinding(lab_VCValueCurrent, x => x.Text, x => x.CVBoatVacuumCurrentValue);
            fluent.SetBinding(lab_VCValueEstablished, x => x.Text, x => x.CVBoatVacuumEstablishedValue);



            //觸發事件
            fluent.BindCommand(Btn_VCTrigger, x => x.VC_Trigger());
            fluent.BindCommand(Btn_SVTrigger,x => x.SV_Trigger());
            fluent.BindCommand(Btn_ValueSave, x => x.SaveData());

            //關閉後事件
            FormClosing += fluent.ViewModel.OnFormClose;
        }

    }
}
