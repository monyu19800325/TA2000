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
    public partial class LaserTestForm : DevExpress.XtraEditors.XtraForm
    {
        public LaserTestForm(InspectionModule module)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module);
        }

        void InitializeBindings(InspectionModule module)
        {
            var fluent = mvvmContext1.OfType<LaserTestViewModel>();

            fluent.ViewModel.Init(module);
            fluent.BindCommand(Btn_MeasureValue, x => x.Measure());
            fluent.BindCommand(Btn_Move, x => x.Move());

            fluent.SetBinding(TE_AY1Pos, c => c.Text, x => x.AY1Pos);
            fluent.SetBinding(TE_CX1Pos, c => c.Text, x => x.CX1Pos);
            fluent.SetBinding(TE_Value, c => c.Text, x => x.Value);
        }
    }
}
