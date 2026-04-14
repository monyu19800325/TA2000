using TA2000Modules;
using DevExpress.Utils.MVVM;
using HTAMachine.Machine;
using HTAMachine.Module;
using HTAMachine.Module.DockPanel;
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
    [RegisterCdiDocker]
    public partial class UPHCalculate : DevExpress.XtraEditors.XtraUserControl, IInjectModule
    {

        FlowCarrierModule _module;

        public UPHCalculate()
        {
            InitializeComponent();
        }

        public void InjectModule(IModule module)
        {
            _module = (FlowCarrierModule)module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<UPHCalculateViewModel>();

            fluent.ViewModel.Init(_module);


            fluent.SetBinding(LB_TotalCount, l => l.Text, x => x.TotalCount);
            fluent.SetBinding(LB_SpendTime, l => l.Text, x => x.ShowSpendTime);
            fluent.SetBinding(LB_UPH, l => l.Text, x => x.UPH);
            fluent.SetBinding(LB_InspectionSpendTime, l => l.Text, x => x.ShowInspectionSpendTime);
            fluent.SetBinding(LB_InspectionUPH, l => l.Text, x => x.InspectionUPH);
        }
    }
}
