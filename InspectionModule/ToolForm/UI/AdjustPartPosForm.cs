using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
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
    public partial class AdjustPartPosForm : DevExpress.XtraEditors.XtraForm
    {
        public DialogResult Result = DialogResult.Cancel;
        public AdjustPartPosForm(List<AdjustPartPos> adjustPartPoses)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(adjustPartPoses);
        }
        void InitializeBindings(List<AdjustPartPos> adjustPartPoses)
        {
            var fluent = mvvmContext1.OfType<AdjustPartPosViewModel>();

            fluent.ViewModel.Init(adjustPartPoses);
            fluent.ViewModel.ResultAction = DialogResultFunc;
            LUE_MosaicPartIndex.Properties.DataSource = fluent.ViewModel.MosaicPartIndexList;

            fluent.SetBinding(LUE_MosaicPartIndex, le => le.EditValue, x => x.MosaicPartIndex);
            fluent.SetBinding(TE_OffsetX, te => te.Text, x => x.OffsetX);
            fluent.SetBinding(TE_OffsetY, te => te.Text, x => x.OffsetY);

            fluent.BindCommand(Btn_OK, x => x.OK());
            fluent.BindCommand(Btn_Cancel, x => x.Cancel());
        }

        public void DialogResultFunc(DialogResult dialogResult)
        {
            this.DialogResult = dialogResult;
            this.Close();
        }

    }

    [POCOViewModel]
    public class AdjustPartPosViewModel
    {
        public List<int> MosaicPartIndexList { get; set; } = new List<int>();
        public Action<DialogResult> ResultAction;

        int _mosaicPartIndex = 0;
        public int MosaicPartIndex 
        { 
            get=> _mosaicPartIndex;
            set
            {
                _mosaicPartIndex = value;
                this.RaisePropertiesChanged();
            } 
        }
        public double OffsetX 
        {
            get => AdjustPartPoses[MosaicPartIndex].OffsetX;
            set
            {
                AdjustPartPoses[MosaicPartIndex].OffsetX = value;
                this.RaisePropertyChanged(x => OffsetX);
            }
        }
        public double OffsetY 
        {
            get => AdjustPartPoses[MosaicPartIndex].OffsetY;
            set
            {
                AdjustPartPoses[MosaicPartIndex].OffsetY = value;
                this.RaisePropertyChanged(x => OffsetY);
            }
        }

        public List<AdjustPartPos> AdjustPartPoses;
        public void Init(List<AdjustPartPos> adjustPartPoses)
        {
            AdjustPartPoses = adjustPartPoses;
            for (int i = 0; i < adjustPartPoses.Count; i++)
            {
                MosaicPartIndexList.Add(i);
            }
            MosaicPartIndex = MosaicPartIndexList[0];
        }

        public void OK()
        {
            ResultAction?.Invoke(DialogResult.OK);
        }

        public void Cancel()
        {
            ResultAction?.Invoke(DialogResult.Cancel);
        }
    }
}
