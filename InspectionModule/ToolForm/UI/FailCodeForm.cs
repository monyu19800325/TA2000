using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HTA.MainController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionComponent;
using VisionController2.VisionController.Common;
using static VisionController2.VisionController.Common.ComponentSpecMap;
using DevExpress.Utils.MVVM;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using System.Text.RegularExpressions;
using HTA.TriggerServer;
namespace TA2000Modules
{
    public partial class FailCodeForm : DevExpress.XtraEditors.XtraForm
    {

        public ComponentSpecMap ComponentSpecMap1;
        public FailCode FailCode1;
        MVVMContextFluentAPI<FailCodeViewModel> _fluent;
        public FailCodeForm(InspectionModule inspectionModule, IMainController mainController,int mapIndex)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(inspectionModule, mainController,mapIndex);
        }

        void InitializeBindings(InspectionModule inspectionModule, IMainController mainController,int mapIndex)
        {
            var fluent = mvvmContext1.OfType<FailCodeViewModel>();
            _fluent =fluent;
            fluent.ViewModel.Init(inspectionModule, mainController,mapIndex);
            fluent.ViewModel.OnUpdateGridView = UpdateGridView;


            lookUpEdit1.Properties.DataSource = fluent.ViewModel.MapList;

            LBC_Component.DataSource = fluent.ViewModel.CanFailCmpList;
            LBC_Spec.DataSource = fluent.ViewModel.SpecList;
            gridControl1.DataSource = fluent.ViewModel.FailCodeList;
            LBC_BindSpec.DataSource = fluent.ViewModel.BindSpecList;

            LBC_Component.DisplayMember = "CmpName";
            LBC_Spec.DisplayMember = "Name";
            LBC_BindSpec.DisplayMember = "Name";

            fluent.BindCommand(Btn_AddFailCode, x => x.AddFailCode());
            fluent.BindCommand(Btn_BindFailCode, x => x.BindFailCode());
            fluent.BindCommand(Btn_UnbindFailCode, x => x.UnbindFailCode());
            fluent.BindCommand(Btn_Save, x => x.Save());

            fluent.SetBinding(LBC_Component, l => l.SelectedIndex, x => x.SelectFailCmpIndex);
            fluent.SetBinding(LBC_Spec, l => l.SelectedIndex, x => x.SelectSpecIndex);
            fluent.SetBinding(LBC_BindSpec, l => l.SelectedIndex, x => x.SelectBindSpecIndex);
            fluent.SetBinding(lookUpEdit1, l => l.EditValue, x => x.MapIndex);

            gridView1.FocusedRowChanged += fluent.ViewModel.RowFocusedChanged;

            var btnDelete = new RepositoryItemButtonEdit();
            btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;

            // 加一顆按鈕
            btnDelete.Buttons.Clear();
            btnDelete.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(
                DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph,
                "Delete",   // 文字（可空）
                -1,
                true,
                true,
                false,
                DevExpress.XtraEditors.ImageLocation.MiddleCenter,
                null
            ));

            btnDelete.ButtonClick += fluent.ViewModel.DeleteFailCode;

            gridControl1.RepositoryItems.Add(btnDelete);

            var colBtn = gridView1.Columns.Add();
            colBtn.Caption = "";
            colBtn.FieldName = ""; // 不用真的存在
            colBtn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            colBtn.ColumnEdit = btnDelete;
            colBtn.Visible = true;
            colBtn.VisibleIndex = gridView1.Columns.Count;
            colBtn.Width = 60;

        }

        public void UpdateGridView()
        {
            gridView1.RefreshData();
        }

    }

    [POCOViewModel]
    public class FailCodeViewModel
    {
        public BindingList<FailCode> FailCodeList { get; set; } = new BindingList<FailCode>();
        public BindingList<SpecFailDecription> CanFailCmpList { get; set; } = new BindingList<SpecFailDecription>();
        public BindingList<SingleSpec> SpecList { get; set; } = new BindingList<SingleSpec>();
        public BindingList<SingleSpec> BindSpecList { get; set; } = new BindingList<SingleSpec>();
        public Action OnUpdateGridView;

        int _selectFailCmpIndex = 0;
        public int SelectFailCmpIndex
        {
            get => _selectFailCmpIndex;
            set
            {
                _selectFailCmpIndex = value;
                UpdateSpecList();
            }
        }
        public virtual int SelectSpecIndex { get; set; } = 0;

        public int SelectBindSpecIndex { get; set; }
        public virtual int SelectFailCodeIndex { get; set; } = 0;

        InspectionModule _inspectionModule;

        public ComponentSpecMap ComponentSpecMap;
        public MainController CurrentVisionController;
        int _mapIndex;
        public int MapIndex 
        {
            get => _mapIndex;
            set 
            {
                _mapIndex = value;
            }
        }
        public List<int> MapList { get; set; } = new List<int>();

        public void Init(InspectionModule inspectionModule, IMainController mainController,int inputMapIndex)
        {
            _inspectionModule = inspectionModule;
            CurrentVisionController = (MainController)mainController;
            ComponentSpecMap = CurrentVisionController.GetComponentSpecMaps();
            MapList.Clear();

            for (int i = 0; i < inputMapIndex+1; i++)
            {
                MapList.Add(i);
            }
            MapIndex = MapList.FirstOrDefault(x=>x == inputMapIndex);

            for (int i = 0; i < ComponentSpecMap.CanCauseFailItems.Count; i++)
            {
                var matches = Regex.Matches(ComponentSpecMap.CanCauseFailItems[i].Name, @"(\d+)");
                if(matches.Count == 0)
                {
                    var spec = new SpecFailDecription("DefaultName", ComponentSpecMap.CanCauseFailItems[i]);
                    spec.FindCmpName(ComponentSpecMap.CanCauseInvalidItems);
                    CanFailCmpList.Add(spec);
                }
                else
                {
                    var mapIndex = int.Parse(matches[0].Groups[1].Value);
                    if(MapIndex == mapIndex)
                    {
                        var spec = new SpecFailDecription("DefaultName", ComponentSpecMap.CanCauseFailItems[i]);
                        spec.FindCmpName(ComponentSpecMap.CanCauseInvalidItems);
                        CanFailCmpList.Add(spec);
                    }
                }
            }
            UpdateSpecList();
            Load();
        }

        public void UpdateSpecList()
        {
            SpecList.Clear();
            List<SingleSpec> temps = new List<SingleSpec>();
            if (CanFailCmpList.Count == 0)
                return;
            if (CanFailCmpList[SelectFailCmpIndex].Spec.UniSpec == null)
            {
                for (int i = 0; i < CanFailCmpList[SelectFailCmpIndex].Spec.InnerDefect.Length; i++)
                {
                    var singleSpec = new SingleSpec()
                    {
                        Name = CanFailCmpList[SelectFailCmpIndex].Spec.InnerDefect[i].FeatureName,
                        Active = CanFailCmpList[SelectFailCmpIndex].Spec.InnerDefect[i].Active,
                        LowBound = CanFailCmpList[SelectFailCmpIndex].Spec.InnerDefect[i].Min,
                        HighBound = CanFailCmpList[SelectFailCmpIndex].Spec.InnerDefect[i].Max
                    };
                    SpecList.Add(singleSpec);
                }
            }
            else
            {
                for (int i = 0; i < CanFailCmpList[SelectFailCmpIndex].Spec.UniSpec.Count; i++)
                {
                    var singleSpec = new SingleSpec() 
                    {
                        Name = CanFailCmpList[SelectFailCmpIndex].Spec.UniSpec[i].Name,
                        Active = CanFailCmpList[SelectFailCmpIndex].Spec.UniSpec[i].Active,
                        LowBound = CanFailCmpList[SelectFailCmpIndex].Spec.UniSpec[i].LowBound,
                        HighBound = CanFailCmpList[SelectFailCmpIndex].Spec.UniSpec[i].HighBound
                    };
                    SpecList.Add(singleSpec);
                }
            }
            //SpecList = temps;
        }


        public void AddFailCode()
        {
            FailCodeList.Add(new FailCode());
            //OnUpdateGridView?.Invoke();
        }

        public void DeleteFailCode(object sender,ButtonPressedEventArgs e)
        {
            FailCodeList.RemoveAt(SelectFailCodeIndex);
            //OnUpdateGridView?.Invoke();
        }

        public void BindFailCode()
        {
            var cmpIndex = CanFailCmpList[SelectFailCmpIndex].Spec.Source;
            var exist = FailCodeList[SelectFailCodeIndex].BindSpecs.Any(x => x.ComponentIndex == cmpIndex && x.Spec.Name == SpecList[SelectSpecIndex].Name);
            if (exist)
                return;
            FailCodeList[SelectFailCodeIndex].BindSpecs.Add(new ComponentBindSpec(cmpIndex, SpecList[SelectSpecIndex]));
            ShowBindFailCode(null, null);
        }

        public void UnbindFailCode()
        {
            FailCodeList[SelectFailCodeIndex].BindSpecs.RemoveAt(SelectBindSpecIndex);
            ShowBindFailCode(null,null);
        }

        public void ShowBindFailCode(object sender, ButtonPressedEventArgs e)
        {
            BindSpecList.Clear();
            for (int i = 0; i < FailCodeList[SelectFailCodeIndex].BindSpecs.Count; i++)
            {
                BindSpecList.Add(FailCodeList[SelectFailCodeIndex].BindSpecs[i].Spec);
            }
        }

        public void Load()
        {
            for (int i = 0; i < _inspectionModule.FailCodeParam.MapFailCodes.Count; i++)
            {
                if(_inspectionModule.FailCodeParam.MapFailCodes[i].MapIndex == MapIndex)
                {
                    for (int j = 0; j < _inspectionModule.FailCodeParam.MapFailCodes[i].FailCodes.Count; j++)
                    {
                        FailCodeList.Add(_inspectionModule.FailCodeParam.MapFailCodes[i].FailCodes[j]);
                    }
                }
            }
        }

        public void Save()
        {
            MapFailCode mapFailCode = new MapFailCode() { MapIndex = MapIndex, FailCodes = FailCodeList.ToList() };
            var target = _inspectionModule.FailCodeParam.MapFailCodes.FirstOrDefault(x => x.MapIndex == MapIndex);
            if (target != null)
            {
                var index = _inspectionModule.FailCodeParam.MapFailCodes.IndexOf(target);
                _inspectionModule.FailCodeParam.MapFailCodes[index] = mapFailCode;
            }
            else
            {
                _inspectionModule.FailCodeParam.MapFailCodes.Add(mapFailCode);
            }
            _inspectionModule.SaveVisionProductParam(this,_inspectionModule);
        }

        public void RowFocusedChanged(object sender, FocusedRowChangedEventArgs args)
        {
            SelectFailCodeIndex = args.FocusedRowHandle;
            ShowBindFailCode(null, null);
        }
    }

    [Serializable]
    public class SingleSpec
    {
        public string Name { get; set; } = "";

        public bool Active { get; set; } = false;

        public double LowBound { get; set; } = -99999.0;

        public double HighBound { get; set; } = 99999.0;

    }

    public class SpecFailDecription
    {
        public string CmpName { get; set; }
        public SpecDescription Spec { get; set; }

        public SpecFailDecription(string cmpName,SpecDescription spec)
        {
            CmpName = cmpName;
            Spec = spec;
        }

        public void FindCmpName(List<ComponentDescription> componentDescriptions)
        {
            var target = componentDescriptions.FirstOrDefault(x => x.Index == Spec.Source);
            CmpName = target.CustomName + ":" + target.Name;
        }
    }


    /// <summary>
    /// 紀錄是哪個元件的哪個Spec
    /// </summary>
    [Serializable]
    public class ComponentBindSpec
    {
        public int ComponentIndex { get; set; }
        public SingleSpec Spec { get; set; }

        public ComponentBindSpec() { }

        public ComponentBindSpec(int componentIndex, SingleSpec spec)
        {
            ComponentIndex = componentIndex;
            Spec = spec;
        }
    }

    /// <summary>
    /// 紀錄FailCode的結構
    /// </summary>
    public class FailCode
    {
        /// <summary>
        /// 0開始之後都是Fail的項目
        /// </summary>
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public List<ComponentBindSpec> BindSpecs { get; set; } = new List<ComponentBindSpec>();
    }
}
