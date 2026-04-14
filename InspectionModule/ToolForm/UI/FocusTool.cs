using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hta.MotionBase;
using HTA.Utility.FormTool;
using System.Threading;
using HalconDotNet;
using DevExpress.Utils.MVVM;
using HTAMachine.Machine;
using HTA.MainController;

namespace TA2000Modules
{
    public partial class FocusTool : DevExpress.XtraEditors.XtraForm, IMultilingual
    {
        public double OldOffsetData;
        public double NewOffsetData;
        public double BaseDist;

        public int MoveTime = 1000;
        public int MoveLimit;
        public Func<string, bool> CheckIoStateNotify;
        public Action OnClose;
        public Action SendGrab;
        public Action<string, string> OnLog;
        public int CamIndex = 0;
        public MVVMContextFluentAPI<FocusViewModel> fluent;
        private HTA.Utility.Structure.Rect1 _focusRoi = new HTA.Utility.Structure.Rect1();

        private IAxis _CurrentAxis;

        private ImageWindowAccessor _accessor;
        private HTA.IFramer.IStationFramer _UseFramer;
        private HTA.LightServer.ILighter _UseLighter;
        private HTA.TriggerServer.ITriggerChannel _UseTrigger;
        private List<double> _groupInfo;
        private string _hintStr;

        private List<double> _focusValue = new List<double>();
        List<double> _offsetList = new List<double>();
        IMainController _controller;
        int _targetGroup = 0;
        public FocusTool(IAxis axis,
            double baseDist,
            double offsetDist,
            int moveLimit = 2,
            int camIndex = 0,
            Action<string,string> onLog =null,
            ImageWindowAccessor accessor =null,
            HTA.IFramer.IStationFramer useCam = null,
            HTA.LightServer.ILighter useLighter = null,
            HTA.TriggerServer.ITriggerChannel useTrigger = null,
            List<double> groupInfo = null,
            string hintStr = null,
            IMainController mainController = null,
            int targetGroup = 0,
            List<double> offsetList = null)
        {            
            InitializeComponent();
            _CurrentAxis = axis;
            _accessor = accessor;
            _UseFramer = useCam;
            _UseLighter = useLighter;
            _UseTrigger = useTrigger;
            _groupInfo = groupInfo;
            _hintStr = hintStr;
            MoveLimit = moveLimit;
            OnLog = onLog;
            CamIndex = camIndex;
            _controller = mainController;
            _offsetList = offsetList;
            _targetGroup = targetGroup;

            BaseDist = baseDist;
            OldOffsetData = offsetDist;
            NewOffsetData = offsetDist;
            TE_AxisName.Text = axis.Name;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
            fluent.ViewModel.MoveToFocusDist();
            SetLightSettingToForm();
        }

        public void InitializeBindings()
        {
            fluent = mvvmContext1.OfType<FocusViewModel>();
            fluent.ViewModel.OnLog = OnLog;

            //按鍵事件
            fluent.BindCommand(Btn_Move, module => module.Move);
            fluent.BindCommand(Btn_BaseMove, module => module.MoveBasePos);
            fluent.BindCommand(Btn_SelectRegion, module => module.SelectRegion);
            fluent.BindCommand(Btn_AutoFocus, module => module.AutoFocus);

            //DataSource
            LUE_Dir.Properties.DataSource = fluent.ViewModel.DirList;
            LUE_Step.Properties.DataSource = fluent.ViewModel.StepList;
            LUE_GroupIndex.Properties.DataSource = fluent.ViewModel.GroupIndexes;
            LUE_CaptureIndex.Properties.DataSource = fluent.ViewModel.InGroupCaptureIndexes;

            //參數
            fluent.SetBinding(SE_CapTimes, obj => obj.Value, module => module.CaptureTimes);
            fluent.SetBinding(SE_MoveRange, obj => obj.Value, module => module.MoveRange);
            fluent.SetBinding(TE_Offset, obj => obj.Text, module => module.Offset);
            fluent.SetBinding(TE_Pos, obj => obj.Text, module => module.CurrentPos);
            fluent.SetBinding(LUE_GroupIndex, obj => obj.EditValue, module => module.SelectGroupIndex);
            fluent.SetBinding(LUE_CaptureIndex, obj => obj.EditValue, module => module.SelectCaptureIndex);
            if (_offsetList == null)
            {
                _offsetList = new List<double>();
            }

            fluent.ViewModel.Initial(_CurrentAxis, BaseDist, OldOffsetData, MoveLimit, CamIndex,
                _accessor, _UseFramer, _UseLighter, _UseTrigger, _groupInfo, _hintStr,_controller, _targetGroup, _offsetList);

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
                    return fluent.ViewModel.StepList.IndexOf((ItemString)objectValue); ;
                });

            fluent.ViewModel.SetChart(chart1);

            FormClosing += (ss, ee) =>
            {
                fluent.ViewModel.DisposeAction();
                fluent.ViewModel.NewOffsetData = fluent.ViewModel.GetOffsetData();
            };
            SE_CapTimes.Properties.MaxValue = 30;
            SE_MoveRange.Properties.MaxValue = 12;
            timer1.Start();

            labelControl10.Visible = false;
            labelControl11.Visible = false;
            LUE_GroupIndex.Visible = false;
            LUE_CaptureIndex.Visible = false;
        }

        private void SetLightSettingToForm()
        {
            //update light channel
            if (_groupInfo == null)
            {
                _groupInfo = Enumerable.Repeat(0.0, GetActualLightChannel()).ToList();
            }



            //光源取得
            HTA.LightServer.ILighter lighter = _UseLighter ?? new HTA.LightServer.VirtualLighter(8);

            var lightForm = new HTA.LightServer.LightForm(lighter, _groupInfo.ToArray());
            lightForm.TopLevel = false;
            lightForm.Parent = LightPanel;
            lightForm.FormBorderStyle = FormBorderStyle.None;
            lightForm.Dock = DockStyle.Fill;
            lightForm.SetToDevice = true;
            lightForm.OnLightValueChange += (obj, args) =>
            {
                for (int i = 0; i < args.Length; i++)
                {
                    _groupInfo[i] = args[i];
                }
            };
            LightPanel.Controls.Add(lightForm);

            if (LightPanel.Controls.Count >= 2)
            {
                LightPanel.Controls.RemoveAt(0);
            }

            lightForm.ApplyCurrentSetting();
            lightForm.Show();
        }

        public int GetActualLightChannel()
        {
            return _UseLighter.Channel;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TE_Pos.Text = _CurrentAxis.ActualPos.ToString("#0.00000");
        }

        public void SetLanguage(ComponentResourceManager resources)
        {
            System.ComponentModel.ComponentResourceManager resources2 = new System.ComponentModel.ComponentResourceManager(typeof(FocusTool));
            resources2.ApplyResources(this, "$this");
            SetLang(this.Controls, resources2);
            this.Refresh();
        }

        public void SetLang(Control.ControlCollection ctrls, ComponentResourceManager resource)
        {
            foreach (Control ctrl in ctrls)
            {
                resource.ApplyResources(ctrl, ctrl.Name);
                if (ctrl.HasChildren)
                    SetLang(ctrl.Controls, resource);
            }
        }
    }    
}