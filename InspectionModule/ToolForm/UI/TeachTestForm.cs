using DevExpress.Mvvm.Xpf;
using DevExpress.XtraPrinting;
using HyperInspection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace TA2000Modules
{
    public partial class TeachTestForm : DevExpress.XtraEditors.XtraForm 
    {
        public Action<bool> NotifyEnable;
        public Action NotifyTeachInspect;
        public Action OnCloseWizard;
        public TeachTestForm(InspectionModule inspectionModule)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(inspectionModule);
        }

        void InitializeBindings(InspectionModule inspectionModule)
        {
            var fluent = mvvmContext1.OfType<TeachTestViewModel>();

            fluent.ViewModel.Vision = inspectionModule;
            //LUE_MapIndexImage.Properties.DataSource = fluent.ViewModel.MapList;

            fluent.ViewModel.Init();

            fluent.ViewModel.OnStartInspect = TeachInspect;
            NotifyEnable = fluent.ViewModel.InspectEnabledAllComponent;
            fluent.SetBinding(TE_InspectTimes, x => x.Text, x => x.InspectCounts);
            fluent.SetBinding(CB_Burning, x => x.Checked, x => x.IsBurning);
            //fluent.SetBinding(LUE_MapIndexImage, x => x.EditValue, x => x.MapIndex);
            fluent.SetBinding(Btn_Inspect, x => x.Enabled, x => x.InspectBtnEnable);
            fluent.SetBinding(Btn_StopInspect, x => x.Enabled, x => x.InspectStopBtnEnable);
            fluent.SetBinding(Btn_FinishTeach, x => x.Enabled, x => x.FinishTeachEnable);

            fluent.BindCommand(Btn_FinishTeach, x => x.FinishTeach());
            fluent.BindCommand(Btn_Inspect, x => x.Inspect());
            fluent.BindCommand(Btn_StopInspect, x => x.StopInspect());
            fluent.BindCommand(Btn_BindFailCode, x => x.BindFailCode());


            fluent.ViewModel.SaveParam += inspectionModule.SaveParam;
            fluent.ViewModel.OnClose = FinishCloseWizard;
            fluent.ViewModel.OnLog = inspectionModule.OnAddTrace;
        }

        public void EnableButton()
        {
            NotifyEnable?.Invoke(true);
        }

        public void TeachInspect()
        {
            NotifyTeachInspect?.Invoke();
        }

        public void FinishCloseWizard()
        {
            OnCloseWizard?.Invoke();
        }
    }
}
