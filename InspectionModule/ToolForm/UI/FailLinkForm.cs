using DevExpress.Mvvm.DataAnnotations;
using DevExpress.XtraEditors.TextEditController;
using HTA.Com.TCPIP;
using HTA.MainController;
using HTA.TriggerServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionController2.VisionController.Common;

namespace TA2000Modules
{
    public partial class FailLinkForm : DevExpress.XtraEditors.XtraForm
    {
        public FailLinkForm(InspectionModule inspectionModule)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(inspectionModule);

        }

        void InitializeBindings(InspectionModule inspectionModule)
        {
            var fluent = mvvmContext1.OfType<FailLinkViewModel>();
            fluent.ViewModel.Init(inspectionModule);

            fluent.BindCommand(Btn_Save, x => x.Save);
            gridControl2.DataSource = fluent.ViewModel.FailTableLinks;
        }
    }

    [POCOViewModel]
    public class FailLinkViewModel
    {
        InspectionModule _inspectionModule;
        public List<FailTableLink> FailTableLinks { get; set; } = new List<FailTableLink>();
        public ComponentSpecMap ComponentSpecMap;
        public void Init(InspectionModule inspectionModule)
        {
            _inspectionModule = inspectionModule;

            for (int m = 0; m < inspectionModule.ProductParam.BigProductMapSetting.MapList.Count; m++)
            {
                if (inspectionModule.ProductParam.BigProductMapSetting.MapList[m].UseType == "Mosaic")
                {
                    ComponentSpecMap = ((MainController)inspectionModule.VisionController_Mosaic).GetComponentSpecMaps();
                }
                else
                {
                    ComponentSpecMap = ((MainController)inspectionModule.VisionController).GetComponentSpecMaps();
                }

                for (int i = 0; i < ComponentSpecMap.CanCauseFailItems.Count; i++)
                {
                    var matches = Regex.Matches(ComponentSpecMap.CanCauseFailItems[i].Name, @"(\d+)");
                    if (matches.Count == 0) continue;
                    var mapIndex = int.Parse(matches[0].Groups[1].Value);
                    if (mapIndex != m)
                        continue;
                    if (ComponentSpecMap.CanCauseFailItems[i].UniSpec == null)
                    {
                        //for (int j = 0; j < ComponentSpecMap.CanCauseFailItems[i].InnerDefect.Length; j++)
                        //{
                        //    FailTableLinks.Add(new FailTableLink()
                        //    {
                        //        MapIndex = mapIndex,
                        //        Name = ComponentSpecMap.CanCauseFailItems[i].Name,
                        //        LinkName = ComponentSpecMap.CanCauseFailItems[i].Name,
                        //        Spec = new ComponentBindSpec()
                        //        {
                        //            ComponentIndex = ComponentSpecMap.CanCauseFailItems[i].Source,
                        //            Spec = new SingleSpec()
                        //            {
                        //                Name = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[j].FeatureName,
                        //                Active = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[j].Active,
                        //                LowBound = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[j].Min,
                        //                HighBound = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[j].Max
                        //            }
                        //        }
                        //    });
                        //}

                        //D2目前只用第一個代替，因為結果是fail，裡面的Spec結果都會是fail，所以不建立多個，只建一個
                        FailTableLinks.Add(new FailTableLink()
                        {
                            MapIndex = mapIndex,
                            Name = ComponentSpecMap.CanCauseFailItems[i].Name,
                            LinkName = ComponentSpecMap.CanCauseFailItems[i].Name,
                            Spec = new ComponentBindSpec()
                            {
                                ComponentIndex = ComponentSpecMap.CanCauseFailItems[i].Source,
                                Spec = new SingleSpec()
                                {
                                    Name = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[0].FeatureName,
                                    Active = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[0].Active,
                                    LowBound = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[0].Min,
                                    HighBound = ComponentSpecMap.CanCauseFailItems[i].InnerDefect[0].Max
                                }
                            }
                        });
                    }
                    else
                    {
                        for (int j = 0; j < ComponentSpecMap.CanCauseFailItems[i].UniSpec.Count; j++)
                        {
                            FailTableLinks.Add(new FailTableLink()
                            {
                                MapIndex = mapIndex,
                                Name = ComponentSpecMap.CanCauseFailItems[i].Name + "-" + ComponentSpecMap.CanCauseFailItems[i].UniSpec[j].Name,
                                LinkName = ComponentSpecMap.CanCauseFailItems[i].Name + "-" + ComponentSpecMap.CanCauseFailItems[i].UniSpec[j].Name,
                                Spec = new ComponentBindSpec()
                                {
                                    ComponentIndex = ComponentSpecMap.CanCauseFailItems[i].Source,
                                    Spec = new SingleSpec()
                                    {
                                        Name = ComponentSpecMap.CanCauseFailItems[i].UniSpec[j].Name,
                                        Active = ComponentSpecMap.CanCauseFailItems[i].UniSpec[j].Active,
                                        LowBound = ComponentSpecMap.CanCauseFailItems[i].UniSpec[j].LowBound,
                                        HighBound = ComponentSpecMap.CanCauseFailItems[i].UniSpec[j].HighBound
                                    }
                                }
                            });
                        }
                    }
                }
            }
            Load();
        }

        public void Load()
        {
            for (int i = 0; i < FailTableLinks.Count; i++)
            {
                var target = _inspectionModule.ProductParam.FailTableLinks.FirstOrDefault(x => x.Name == FailTableLinks[i].Name);
                if (target == null)
                    continue;
                FailTableLinks[i].LinkName = target.LinkName;
            }

        }

        public void Save()
        {
            _inspectionModule.ProductParam.FailTableLinks = FailTableLinks.DeepClone();
            _inspectionModule.SaveVisionProductParam(this, _inspectionModule);
        }
    }

    [Serializable]
    public class FailTableLink
    {
        public string Name { get; set; }
        [Browsable(false)]
        public int MapIndex { get; set; }
        [Browsable(false)]
        public ComponentBindSpec Spec { get; set; } = null;
        [DisplayName("User Display Name")]
        public string LinkName { get; set; } = "";
        [Browsable(false)]
        public string FailGridCode { get; set; } = "F";
        [Browsable(false)]
        public Color FailGridColor { get; set; } = Color.Red;
    }
}
