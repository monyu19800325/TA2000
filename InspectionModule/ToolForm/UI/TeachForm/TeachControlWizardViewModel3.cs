using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Utils.Design;
using DevExpress.Utils.MVVM.Services;
using DevExpress.Utils.Taskbar;
using DevExpress.Utils.Text.Internal;
using DevExpress.XtraTab;
using HTA.InspectionFlow;
using HTA.Utility.FormTool;
using HyperInspection;
using InspectionModule.Properties;
using LVDATA;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionComponent;
using static HTA.InspectionFlow.Component.SourceSelectViewModel;
using static HyperInspection.FlowForm;

namespace TA2000Modules
{
    public partial class TeachControlWizardViewModel
    {
        public enum TreeNodeEm { Position,Capture,Component,Inspect}
        public virtual string SelectPosText { get; set; }
        public virtual string SelectCapText { get; set; }
        public virtual string SelectMapText { get; set; }
        public virtual string ComponentName { get; set; }
        public virtual string UserDefinedName { get; set; }
        public virtual string StandardName { get; set; }
        public virtual string CaptureName { get; set; }

        bool _isExpandAll = true;
        public bool IsExpandAll 
        {
            get => _isExpandAll;
            set
            {
                _isExpandAll = value;
            }
        }
        public virtual string ExpandAllText { get; set; } = "Collapase All";

        public TreeView MapTreeView;
        public XtraTabControl TabControl;
        public XtraTabPage PoistionTabPage;
        public XtraTabPage CaptureTabPage;
        public XtraTabPage ComponentTabPage;
        public XtraTabPage MapTabPage;
        public TreeNode CurrentNode;
        public XtraTabControl ShowPanelTabControl;
        public XtraTabPage ShowPanelPage;
        public XtraTabPage OriginPage;
        public List<BasicComponent> Components { get; set; }
        public int MoveCmpIndex = -1;
        public bool IsNewCapture = false;
        public List<HTA.MainController.RoundSetting2.Capture> AllCaptures { get; set; } = new List<HTA.MainController.RoundSetting2.Capture>();
        /// <summary>
        /// 用於複製參數的handle
        /// </summary>
        private CopyParamClass _paramCopyHandle = null;
        TreeNode _flowTreeNode;
        bool Editable = true;
        List<string> _tempCmpUserDefinedNameMosaic = new List<string>();
        List<string> _tempCmpUserDefinedName = new List<string>();

        /// <summary>
        /// 初始化自己寫的flow tree view
        /// </summary>
        /// <param name="treeView"></param>
        public void InitMapTreeView(TreeView treeView)
        {
            MapTreeView = treeView;
            MapTreeView.NodeMouseClick += MapTreeNodeMouseClick;
            MapTreeView.MouseClick += MapTreeMouseClick;
            MapTreeView.NodeMouseDoubleClick +=(s, e) =>
            {
                if (e.Node.ForeColor == Color.Gray)
                    return;
                if (QueryNodeType2(e.Node) == TreeNodeEm.Component)
                {
                    int index = ExtractTreeNodeIndex2(e.Node);
                    if (e.Node.ForeColor == Color.Blue)
                    {
                        MessageBox.Show("Component was ignore!!, cant enter");
                        return;

                    }
                    FlowFormData.OpenComponent(index);
                }
            };

            UpdateMapTree();
        }

        /// <summary>
        /// 初始化 Set右邊的tab control
        /// </summary>
        /// <param name="xtraTabControl"></param>
        /// <param name="posPage"></param>
        /// <param name="capPage"></param>
        /// <param name="cmpPage"></param>
        /// <param name="mapPage"></param>
        public void SetTabControl(XtraTabControl xtraTabControl,XtraTabPage posPage,XtraTabPage capPage,XtraTabPage cmpPage,XtraTabPage mapPage)
        {
            ShowPanelTabControl.ShowTabHeader = DefaultBoolean.False;
            TabControl = xtraTabControl;
            PoistionTabPage = posPage;
            CaptureTabPage = capPage;
            ComponentTabPage = cmpPage;
            MapTabPage = mapPage;

            SelectMapText = MapTreeView.Nodes[0].Text;
            TabControl.TabPages.Clear();
            TabControl.TabPages.Add(MapTabPage);
            TabControl.TabPages.Add(PoistionTabPage);
            TabControl.TabPages.Add(CaptureTabPage);
            TabControl.TabPages.Add(ComponentTabPage);
            TabControl.SelectedTabPage = MapTabPage;
            PoistionTabPage.PageVisible = false;
            CaptureTabPage.PageVisible = false;
            ComponentTabPage.PageVisible = false;
            var matches = Regex.Matches(MapTreeView.Nodes[0].Text, @"_(\d+)");
            int mapIndex = int.Parse(matches[0].Groups[1].Value);
            MapIndexImage = mapIndex;

            FlowFormData.AttachCalibration();
        }

        /// <summary>
        /// 初始化暫存的自定義名稱
        /// </summary>
        public void InitTempDefinedName()
        {
            if (Type == "Mosaic")
            {
                for (int i = 0; i < ProductParam.ComponentUserDefinedNamesMosaic.Count; i++)
                {
                    if (i > Components.Count)
                        continue;
                    _tempCmpUserDefinedNameMosaic.Add(ProductParam.ComponentUserDefinedNamesMosaic[i]);
                }

                for (int i = 0; i < Components.Count; i++)
                {
                    if (_tempCmpUserDefinedNameMosaic.Count < i+1)
                    {
                        _tempCmpUserDefinedNameMosaic.Add("");
                    }
                }
            }
            else
            {
                for (int i = 0; i < ProductParam.ComponentUserDefinedNames.Count; i++)
                {
                    if (i > Components.Count)
                        continue;
                    _tempCmpUserDefinedName.Add(ProductParam.ComponentUserDefinedNames[i]);
                }

                for (int i = 0; i < Components.Count; i++)
                {
                    if (_tempCmpUserDefinedName.Count < i+1)
                    {
                        _tempCmpUserDefinedName.Add("");
                    }
                }
            }
        }
        /// <summary>
        /// 更新左側自己寫的flow tree
        /// </summary>
        public void UpdateMapTree()
        {
            if (MapTreeView ==null)
            {
                return;
            }

            AllCaptures.Clear();
            foreach (var group in CurrentVisionController.ProductSetting.RoundSettings2[0].Groups)
            {
                foreach (var cap in group.Captures)
                {
                    AllCaptures.Add(cap);
                }
            }

            UpdateCmpCaptureName();

            MapTreeView.Nodes.Clear();
            List<SingleUnitMap> finalMaps = new List<SingleUnitMap>();
            for (int i = 0; i < TempSingleUnitMap.Count; i++)
            {
                if (TempSingleUnitMap[i].UseType == Type)
                {
                    finalMaps.Add(TempSingleUnitMap[i]);
                }
            }

            int inspectCount = 0;

            for (int map = 0; map < finalMaps.Count; map++)
            {
                TreeNode rootNode = new TreeNode()
                {
                    Name = $"Root_{finalMaps[map].MapIndex}",
                    Text = $"Map_{finalMaps[map].MapIndex}",
                    Tag = -1,
                    ImageIndex = 1
                };
                MapTreeView.Nodes.Add(rootNode);
                int mapPosCount = 0;
                TreeNode inspectNode = new TreeNode();
                for (int i = 0; i < CurrentVisionController.ProductSetting.RoundSettings2[0].Groups.Count; i++)
                {
                    if (!finalMaps[map].GroupIndexes.Any(x => x == CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[i].Id))
                    {
                        continue;
                    }
                    string posName = $"Position_{i}";

                    if (Type != "Mosaic")
                    {
                        var flowFolderInfo = FlowFormData._flowSetupHandle.GetFolderInfo();
                        var targetFolder = flowFolderInfo.Folders.FirstOrDefault(x => x.FolderName.Contains(CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[i].Name));
                        var matches = Regex.Matches(targetFolder.FolderName, @"_(\d+)");
                        int part = int.Parse(matches[2].Groups[1].Value);
                        posName = $"Position_{i};Part_{part}";
                        if (mapPosCount%finalMaps[map].MosaicPosition.Count == 0)
                        {
                            inspectNode = new TreeNode()
                            {
                                Name = $"Inspect_{inspectCount}",
                                Text = $"Inspect_{inspectCount}",
                                Tag = -1,
                                ImageIndex = 5
                            };
                            inspectCount++;
                            rootNode.Nodes.Add(inspectNode);
                        }
                        mapPosCount++;
                    }
                    TreeNode groupNode = new TreeNode()
                    {
                        Name = $"{CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[i].Name}",
                        Text = posName,
                        Tag = -1,
                        ImageIndex = 2
                    };
                    if (Type == "Mosaic")
                    {
                        rootNode.Nodes.Add(groupNode);
                    }
                    else
                    {
                        inspectNode.Nodes.Add(groupNode);
                    }

                    for (int j = 0; j < CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[i].Captures.Count; j++)
                    {
                        var target = AllCaptures.FirstOrDefault(x => x.Id == CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[i].Captures[j].Id);
                        var capIndex = AllCaptures.IndexOf(target);
                        TreeNode captureNode = new TreeNode()
                        {
                            Name = $"{CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[i].Captures[j].Name}",
                            Text = $"Capture_{capIndex}",
                            Tag = -1,
                            ImageIndex = 4
                        };
                        groupNode.Nodes.Add(captureNode);

                        for (int k = 0; k < Components.Count; k++)
                        {
                            var data = Components[k].GetDataByMode();
                            var image = data.List.FirstOrDefault(x => x.TypeName == "SourceLine3");
                            var second = image["Second"];
                            var cmpCaptureIndex = Convert.ToInt32(second[0]);
                            if (capIndex == cmpCaptureIndex)
                            {
                                var useColor = DecideColor2(k);
                                string definedName = "";
                                if (Type == "Mosaic")
                                {
                                    if (_tempCmpUserDefinedNameMosaic[k] != "")
                                        definedName = $" ({_tempCmpUserDefinedNameMosaic[k]})";
                                }
                                else
                                {
                                    if (_tempCmpUserDefinedName[k] != "")
                                        definedName = $" ({_tempCmpUserDefinedName[k]})";
                                }
                                TreeNode cmpNode = new TreeNode()
                                {
                                    Name = $"{Components[k].DataName}",
                                    Text = $"{Components[k].Name}{definedName}",
                                    Tag = k,
                                    ImageIndex = 3,
                                    ForeColor = useColor
                                };
                                captureNode.Nodes.Add(cmpNode);
                            }
                        }
                    }
                }
            }
            FlowFormData.ExternalUpdateComponent();
            MapTreeView.Update();
            MapTreeView.ExpandAll();
            FlowFormData._resultBuffer.RefreshAll();
        }
        public bool AddComponent(bool isNewCapture = false)
        {
            IsNewCapture = isNewCapture;
            bool isAdd = FlowFormData.AddCmpOnFlowForm();
            if (!isAdd)
                return false;
            var index = Components.FindIndex(x => x.DataName.StartsWith($"M{MapIndexImage}_G{GroupIndex}"));
            int targetIndex = index + MoveCmpIndex;
            if (IsNewCapture)
            {
                targetIndex = MoveCmpIndex;
                int captureCount = 0;
                for (int i = 0; i <= GroupIndex; i++)
                {
                    captureCount += CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[i].Captures.Count;
                }
                CaptureIndex = captureCount -1;
            }

            UpdateCmpCaptureName();

            var lv4 = Components[targetIndex].GetDataByMode();
            for (int j = 0; j < lv4.Count; j++)
            {
                if (lv4[j].TypeName == "SourceLine3")
                {
                    CandidateSource sourceTargets;
                    if (Type != "Mosaic")
                    {
                        sourceTargets = SourceList.FirstOrDefault(x => x.Name.Contains($"MapIndex_{MapIndexImage};Group_{GroupIndex};Capture_{CaptureIndex}"));
                    }
                    else
                    {
                        sourceTargets = SourceList.FirstOrDefault(x => x.Name.Contains($"MapIndex_{MapIndexImage};Group_{GroupIndex};Capture_{CaptureIndex}"));
                    }

                    if (sourceTargets == null)
                        continue;

                    var selected = sourceTargets;
                    lv4[j]["First"].Set(selected.Index[0]);
                    lv4[j]["Second"].Set(selected.Index[1]);
                    lv4[j]["Third"].Set(selected.Index[2]);
                }
                if (lv4[j].TypeName == "SourceLine1")
                {
                    var lv3Type = lv4[j]["Type"].Get<string>();
                    var targetCmps = Components.Where(x => lv3Type.Contains(x.Name)).ToList();
                    if (targetCmps.Count == 0)
                    {
                        continue;
                    }
                    List<int> indexes = new List<int>();
                    foreach (var item in targetCmps)
                    {
                        var cmpIndex = Components.IndexOf(item);
                        if (cmpIndex > targetIndex)
                        {
                            continue;
                        }
                        indexes.Add(cmpIndex);
                    }
                    if (indexes.Count == 0)
                        continue;
                    lv4[j]["First"].Set<int>(indexes[indexes.Count-1]+1);
                }
            }
            DoDefinedNameFunc(targetIndex, "Insert");
            ReNameCustomerName();
            UpdateMapTree();
            return true;
        }

        /// <summary>
        /// 在加元件的時候，出現要固定在某個元件下面時(AddCmpOnFlowForm)，會進這個方法
        /// </summary>
        public void FixCmpPosition()
        {
            //取得除了自己的所有名字
            List<string> componentList = FlowFormData._flowHandle.GetCmpNames();
            componentList.RemoveAt(componentList.Count - 1);

            var targetList = componentList.Where(x => x.StartsWith($"M{MapIndexImage}_G{GroupIndex}")).ToList();

            if (IsNewCapture)
            {
                var targets = componentList.Where(x => x.StartsWith($"M{MapIndexImage}_G{GroupIndex}")).ToList();
                if (targets.Count > 0)
                {
                    var targetIndex = componentList.IndexOf(targets[targets.Count-1]);
                    FlowFormData._flowSetupHandle.Move(FlowFormData._flowHandle.Count - 1, targetIndex + 1);
                    FlowFormData._resultBuffer.CmpAdded(targetIndex + 1);
                    MoveCmpIndex = targetIndex + 1;
                }
            }
            else
            {
                IndexSelectForm2 selectForm = new IndexSelectForm2(targetList);
                selectForm.StartPosition = FormStartPosition.CenterScreen;
                selectForm.ShowDialog();
                if (selectForm.SelectIndex != -1)
                {
                    var index = componentList.FindIndex(x => x.StartsWith($"M{MapIndexImage}_G{GroupIndex}"));
                    FlowFormData._flowSetupHandle.Move(FlowFormData._flowHandle.Count - 1, index + selectForm.SelectIndex + 1);
                    FlowFormData._resultBuffer.CmpAdded(selectForm.SelectIndex + 1);
                    MoveCmpIndex = selectForm.SelectIndex + 1;
                }
                else
                {
                    FlowFormData._flowSetupHandle.Move(FlowFormData._flowHandle.Count - 1, targetList.Count - 1);
                    FlowFormData._resultBuffer.CmpAdded(targetList.Count - 1);
                    MoveCmpIndex = targetList.Count - 1;
                }
            }
        }

        public void OpenTeach()
        {
            if (CurrentNode.ForeColor == Color.Gray)
                return;
            if (QueryNodeType2(CurrentNode) == TreeNodeEm.Component)
            {
                int index = ExtractTreeNodeIndex2(CurrentNode);
                if (CurrentNode.ForeColor == Color.Blue)
                {
                    MessageBox.Show("Component was ignore!!, cant enter");
                    return;

                }
                FlowFormData.OpenComponent(index);
            }
        }

        public void DeleteComponent(List<TreeNode> delNodes = null)
        {
            if (delNodes == null)
            {
                var flowTree = FlowFormData.GetFlowTreeView();
                var flowNodes = GetAllNodes(flowTree).ToList();
                var target = flowNodes.FirstOrDefault(x => x.Tag.ToString() == CurrentNode.Tag.ToString());
                var matches = Regex.Matches(target.Text, @"(\d+)");
                var mapIndex = matches[0].Groups[1].Value;
                var groupIndex = matches[1].Groups[1].Value;
                var posCount = flowNodes.Where(x => x.Text.Contains($"M{mapIndex}_G{groupIndex}")).ToList().Count;
                if (posCount == 1)
                {
                    MessageBox.Show("此影像只剩一個Component，不可再刪除Component，如要刪除影像，點擊Delete Capture");
                    return;
                }
                var index = Convert.ToInt32(target.Tag);
                flowTree.SelectedNode = target;
                bool success = FlowFormData.DeleteCmp();
                if (success)
                    DoDefinedNameFunc(index, "Delete");
            }
            else
            {
                for (int i = delNodes.Count-1; i >= 0; i--)
                {
                    var flowTree = FlowFormData.GetFlowTreeView();
                    var flowNodes = GetAllNodes(flowTree).ToList();//這裡得出的list不是照順序
                    var target = flowNodes.FirstOrDefault(x => x.Text.ToString() == delNodes[i].Text.ToString());
                    if (target == null)
                        continue;
                    var index = Convert.ToInt32(target.Tag);
                    flowTree.SelectedNode = target;
                    bool success = FlowFormData.DeleteCmp();
                    if (success)
                        DoDefinedNameFunc(index, "Delete");
                }
            }
            ReNameCustomerName();
            UpdateMapTree();
        }

      
        public void MapTreeNodeMouseClick(object sender,TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (QueryNodeType2(e.Node) == TreeNodeEm.Position)
                {
                    MapTreeView.SelectedNode = e.Node;
                    {
                        ContextMenuStrip TempMenu = new ContextMenuStrip();
                        TempMenu.Items.Clear();
                        TempMenu.Items.Add("Enable");
                        TempMenu.Items[TempMenu.Items.Count - 1].Click += EnableRange2;
                        TempMenu.Items.Add("Ignore");
                        TempMenu.Items[TempMenu.Items.Count - 1].Click += IgnoreRange2;
                        TempMenu.Show(Cursor.Position);
                    }
                }
            }
            else
            {
                if (e.Node.Text.Contains("Position"))
                {
                    CurrentNode = e.Node;
                    var matches = Regex.Matches(e.Node.Name, @"_(\d+)");
                    int mapIndex = int.Parse(matches[0].Groups[1].Value);
                    int groupIndex = int.Parse(matches[1].Groups[1].Value);
                    MapIndexImage = mapIndex;
                    GroupIndex = groupIndex;
                    SelectPosText = e.Node.Text;
                    //TabControl.TabPages.Clear();
                    //TabControl.TabPages.Add(PoistionTabPage);
                    MapTabPage.PageVisible = false;
                    CaptureTabPage.PageVisible = false;
                    ComponentTabPage.PageVisible = false;
                    PoistionTabPage.PageVisible = true;
                    TabControl.SelectedTabPage = PoistionTabPage;
                }
                else if (e.Node.Text.Contains("Capture"))
                {
                    CurrentNode = e.Node;
                    var matches = Regex.Matches(e.Node.Name, @"_(\d+)");
                    int mapIndex = int.Parse(matches[0].Groups[1].Value);
                    int groupIndex = int.Parse(matches[1].Groups[1].Value);
                    var target = CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[groupIndex].Captures.FirstOrDefault(x => x.Name == e.Node.Name);
                    var captureIndex = CurrentVisionController.ProductSetting.RoundSettings2[0].Groups[groupIndex].Captures.IndexOf(target);
                    MapIndexImage = mapIndex;
                    GroupIndex = groupIndex;
                    CaptureIndex = CaptureIndexList[captureIndex];
                    CaptureName = target.Name;
                    SelectCapText = e.Node.Text;
                    //TabControl.TabPages.Clear();
                    //TabControl.TabPages.Add(CaptureTabPage);
                    MapTabPage.PageVisible = false;
                    CaptureTabPage.PageVisible = true;
                    ComponentTabPage.PageVisible = false;
                    PoistionTabPage.PageVisible = false;
                    TabControl.SelectedTabPage = CaptureTabPage;
                }
                else if (e.Node.Text.Contains("Map"))
                {
                    CurrentNode = e.Node;
                    SelectMapText = e.Node.Text;
                    //TabControl.TabPages.Clear();
                    //TabControl.TabPages.Add(MapTabPage);
                    MapTabPage.PageVisible = true;
                    CaptureTabPage.PageVisible = false;
                    ComponentTabPage.PageVisible = false;
                    PoistionTabPage.PageVisible = false;
                    TabControl.SelectedTabPage = MapTabPage;
                    var matches = Regex.Matches(e.Node.Text, @"_(\d+)");
                    int mapIndex = int.Parse(matches[0].Groups[1].Value);
                    MapIndexImage = mapIndex;
                }
                else if (e.Node.Text.StartsWith("Inspect"))
                {

                }
                else
                {
                    //component
                    CurrentNode = e.Node;
                    ComponentName = e.Node.Text;
                    //TabControl.TabPages.Clear();
                    //TabControl.TabPages.Add(ComponentTabPage);
                    MapTabPage.PageVisible = false;
                    CaptureTabPage.PageVisible = false;
                    ComponentTabPage.PageVisible = true;
                    PoistionTabPage.PageVisible = false;
                    TabControl.SelectedTabPage = ComponentTabPage;
                    StandardName =  e.Node.Name;
                    var splits = StandardName.Split('_');
                    UserDefinedName = "";
                    if(Type == "Mosaic")
                    {
                        if (splits.Length > 4)
                        {
                            UserDefinedName = splits[4];
                        }
                    }
                    else
                    {
                        if (splits.Length > 5)
                        {
                            UserDefinedName = splits[5];
                        }
                    }
                }
            }
        }

        public void MapTreeMouseClick(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = MapTreeView.SelectedNode;
                if (node == null) return;
                if (node.Text.StartsWith("Map"))
                {
                    ContextMenuStrip TempMenu = new ContextMenuStrip();
                    TempMenu.Items.Clear();
                    var tmp = new ToolStripMenuItem("Add Position");
                    tmp.Click += (s, ee) =>
                    {
                        AddGroup();
                    };
                    TempMenu.Items.Add(tmp);
                    TempMenu.Show(Cursor.Position);
                }
                else
                {
                    TreeNodeEm type = QueryNodeType2(node);
                    if (type == TreeNodeEm.Component)
                    {
                        int index = ExtractTreeNodeIndex2(node);
                        {
                            ContextMenuStrip TempMenu = new ContextMenuStrip();
                            var currentState = FlowFormData._flowSetupHandle.IsKeyComponent(index);
                            //-------------加入enable and disable-------------------------
                            TempMenu.Items.Clear();
                            {
                                var tmp = new ToolStripMenuItem("Enable");
                                tmp.Tag = VisionComponent.ComponentProperty.ComponentStateEm.Enable;
                                tmp.Click += SetComponentState2;
                                TempMenu.Items.Add(tmp);

                                if (currentState == ComponentProperty.ComponentStateEm.Enable)
                                {
                                    //tmp.Image = Resources._checked;
                                }
                            }

                            if (FlowFormData._flowSetupHandle.CanCauseFail(index))
                            {
                                var tmp = new ToolStripMenuItem("Disable");
                                tmp.Tag = VisionComponent.ComponentProperty.ComponentStateEm.Disable;
                                tmp.Click += SetComponentState2;
                                TempMenu.Items.Add(tmp);
                                if (currentState == ComponentProperty.ComponentStateEm.Disable)
                                {
                                    //tmp.Image = Resources._checked;
                                }
                            }

                            {
                                var tmp = new ToolStripMenuItem("Ignore");
                                tmp.Tag = VisionComponent.ComponentProperty.ComponentStateEm.Ignore;
                                tmp.Click += SetComponentState2;
                                TempMenu.Items.Add(tmp);
                                if (currentState == ComponentProperty.ComponentStateEm.Ignore)
                                {
                                    //tmp.Image = Resources._checked;
                                }
                            }



                            //加入分隔線
                            TempMenu.Items.Add(new ToolStripSeparator());

                            if (FlowFormData._flowHandle[index].HaveSpec())
                            {
                                AttachSpeckItem2(TempMenu, FlowFormData._flowHandle[index]);
                            }


                            //加入分隔線
                            //參數拷貝與複製
                            {
                                //是否可以複製
                                var param = FlowFormData._flowHandle[index].GetComponentParam();
                                if (param != null)
                                {
                                    var tmp = new ToolStripMenuItem("Copy parameters");
                                    tmp.Click += (s, args) =>
                                    {
                                        _paramCopyHandle = new CopyParamClass()
                                        {
                                            TypeName = FlowFormData._flowHandle[index].TypeName,
                                            Param = param
                                        };
                                    };
                                    TempMenu.Items.Add(tmp);
                                }

                                if (_paramCopyHandle != null && FlowFormData._flowHandle[index].TypeName == _paramCopyHandle.TypeName)
                                {
                                    var tmp = new ToolStripMenuItem("Apply params");
                                    tmp.Click += (s, args) =>
                                    {
                                        FlowFormData._flowHandle[index].SetComponentParam(_paramCopyHandle.Param);
                                    };
                                    TempMenu.Items.Add(tmp);
                                }
                            }

                            TempMenu.Show(Cursor.Position);
                        }
                    }
                    else if(type == TreeNodeEm.Position)
                    {
                        ContextMenuStrip TempMenu = new ContextMenuStrip();
                        TempMenu.Items.Clear();
                        var tmp = new ToolStripMenuItem("Add Capture");
                        tmp.Click += (s, ee) =>
                        {
                            AddCapture();
                        };
                        TempMenu.Items.Add(tmp);
                        TempMenu.Show(Cursor.Position);
                    }
                    else if(type == TreeNodeEm.Capture)
                    {
                        ContextMenuStrip TempMenu = new ContextMenuStrip();
                        TempMenu.Items.Clear();
                        var tmp = new ToolStripMenuItem("Add Component");
                        tmp.Click += (s, ee) =>
                        {
                            AddComponent();
                        };
                        TempMenu.Items.Add(tmp);
                        TempMenu.Show(Cursor.Position);
                    }
                }
            }
        }

        public void SaveProductImageAndParameter()
        {
            FlowFormData.SaveProductImageAndParameter();
        }
        public void SaveOnlyParameter()
        {
            FlowFormData.SaveOnlyParameter();
        }
        public void ProductRefresh()
        {
            FlowFormData.ProductRefresh();
        }
        public void FlowRunExecute()
        {
            FlowFormData.FlowRunExecute();
        }

        public void SetUserDefinedName()
        {
            var target = Components.FirstOrDefault(x => x.DataName == CurrentNode.Name);
            var index = Components.IndexOf(target);
            var splits = target.DataName.Split('_');
            string standardName = "";
            int standardCount = 4;
            if (Type != "Mosaic")
                standardCount = 5;
            for (int i = 0; i < standardCount; i++)
            {
                if (i == 0)
                {
                    standardName += splits[i];
                }
                else
                {
                    standardName += "_" + splits[i];
                }
            }
            if (Type == "Mosaic")
            {
                _tempCmpUserDefinedNameMosaic[index] = UserDefinedName;
                if (UserDefinedName == "")
                {
                    target.DataName = standardName;
                }
                else
                {
                    target.DataName = standardName + $"_{_tempCmpUserDefinedNameMosaic[index]}";
                }
            }
            else
            {
                _tempCmpUserDefinedName[index] = UserDefinedName;
                if (UserDefinedName == "")
                {
                    target.DataName = standardName;
                }
                else
                {
                    target.DataName = standardName + $"_{_tempCmpUserDefinedName[index]}";
                }
            }
            StandardName = target.DataName;
            UpdateMapTree();
            CurrentNode.Name = target.DataName;
        }

        public void ExpendAll()
        {
            if (IsExpandAll)
            {
                //現在已經展開，要做縮合
                ExpandAllText = "Expand All";
                MapTreeView.CollapseAll();
                for (int i = 0; i < MapTreeView.Nodes.Count; i++)
                {
                    MapTreeView.Nodes[i].Expand();
                }
            }
            else
            {
                //現在已經縮合，要做展開
                ExpandAllText = "Collapase All";
                MapTreeView.ExpandAll();
            }
            IsExpandAll =!IsExpandAll;
        }

        /// <summary>
        /// 取得treeView所有的nodes，但這個list不照順序
        /// </summary>
        /// <param name="treeView"></param>
        /// <returns></returns>
        IEnumerable<TreeNode> GetAllNodes(TreeView treeView)
        {
            var stack = new Stack<TreeNode>();

            foreach (TreeNode node in treeView.Nodes)
                stack.Push(node);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                foreach (TreeNode child in current.Nodes)
                    stack.Push(child);
            }
        }

        /// <summary>
        /// 處理自定義名稱的方法
        /// </summary>
        /// <param name="index"></param>
        /// <param name="doAction"></param>
        public void DoDefinedNameFunc(int index, string doAction)
        {
            switch (doAction)
            {
                case "Insert":
                    if (Type == "Mosaic")
                    {
                        _tempCmpUserDefinedNameMosaic.Insert(index, "");
                    }
                    else
                    {
                        while(_tempCmpUserDefinedName.Count < index)
                        {
                            _tempCmpUserDefinedName.Add("");
                        }
                        _tempCmpUserDefinedName.Insert(index, "");
                    }
                    break;
                case "Delete":
                    if (Type == "Mosaic")
                    {
                        _tempCmpUserDefinedNameMosaic.RemoveAt(index);
                    }
                    else
                    {
                        _tempCmpUserDefinedName.RemoveAt(index);
                    }
                    break;
            }
        }

        /// <summary>
        /// 更新元件裡的影像名稱並建立影像來源的list
        /// </summary>
        public void UpdateCmpCaptureName()
        {
            var standardInput = FlowFormData._flowSetupHandle.StandardInputFormat;
            var sFlow = Flow2.GetInitSFlowData(standardInput, true);
            FlowFormData.AddCaptureName(sFlow);
            LvTypeEm wannaType = LvTypeEm.HIMAGE;
            SourceList.Clear();
            for (int j = 0; j < sFlow.Count; j++)
            {
                for (int k = 0; k < sFlow[j].Count; k++)
                {
                    if (sFlow[j][k].DataType == wannaType)
                    {
                        var candidate = new CandidateSource
                        {
                            Name = $"{sFlow.Name}-{sFlow[j].Name}-{sFlow[j][k].Name}",
                            Index = new int[3] { 0, j, k }
                        };
                        SourceList.Add(candidate);
                    }
                }
            }
        }
       
        TreeNodeEm QueryNodeType2(TreeNode node)
        {
            if (node.Text.Contains("Position"))
                return TreeNodeEm.Position;
            else if (node.Name.Contains("Capture"))
                return TreeNodeEm.Capture;
            else if (node.Name.StartsWith("Inspect"))
                return TreeNodeEm.Inspect;
            else
                return TreeNodeEm.Component;
        }

        int ExtractTreeNodeIndex2(TreeNode node)
        {
            var target = Components.FirstOrDefault(x => x.DataName == node.Name);
            if (target == null)
            {
                return -1;
            }
            var index = Components.IndexOf(target);
            

            return index;
        }
        private void SetComponentState2(object sender, EventArgs e)
        {
            var ctrl = (ToolStripMenuItem)sender;
            var state = (VisionComponent.ComponentProperty.ComponentStateEm)(ctrl.Tag);
            TreeNode node = MapTreeView.SelectedNode;
            int index = ExtractTreeNodeIndex2(node);
            FlowFormData._flowHandle.SetComponentSate(index, state);
            UpdateMapTree();
            FlowFormData._resultBuffer.Refresh(FlowFormData._flowHandle[index]);
        }



        private void AttachSpeckItem2(ContextMenuStrip tempMenu, BasicComponent cmp)
        {
            //--------------加入獨立規格-----------------------------------
            ToolStripMenuItem SpeckSplitCheck = new ToolStripMenuItem();
            if (cmp.CurrentProp.SeparatedSpec)
            {
                SpeckSplitCheck.Text = "Separater Specs";
                //SpeckSplitCheck.Image = Resources._checked;

            }
            else
            {
                SpeckSplitCheck.Text = "Uni-Spec";
                //SpeckSplitCheck.Image = Resources.close;
            }
            SpeckSplitCheck.Click += (se, arg) =>
            {
                cmp.CurrentProp.SeparatedSpec = !cmp.CurrentProp.SeparatedSpec;
            };


            tempMenu.Items.Add(SpeckSplitCheck);
        }

        
 private void EnableRange2(object sender, EventArgs e)
        {
            int folderIndex = ExtractTreeNodeIndex2(MapTreeView.SelectedNode);
            HTA.InspectionFlow.FolderList folderInfo = FlowFormData._flowSetupHandle.GetFolderInfo();
            var currentFolder = folderInfo[folderIndex];

            int startIndex = currentFolder.Contain.First;
            int endIndex = currentFolder.Contain.Second;
            for (int i = endIndex - 1; i >= startIndex; i--)
            {
                FlowFormData._flowHandle.SetComponentSate(i, ComponentProperty.ComponentStateEm.Enable);
            }
            UpdateMapTree();
        }

        private void IgnoreRange2(object sender, EventArgs e)
        {
            int folderIndex = ExtractTreeNodeIndex2(MapTreeView.SelectedNode);
            HTA.InspectionFlow.FolderList folderInfo = FlowFormData._flowSetupHandle.GetFolderInfo();
            var currentFolder = folderInfo[folderIndex];

            int startIndex = currentFolder.Contain.First;
            int endIndex = currentFolder.Contain.Second;
            for (int i = endIndex - 1; i >= startIndex; i--)
            {
                FlowFormData._flowHandle.SetComponentSate(i, ComponentProperty.ComponentStateEm.Ignore);
            }
            UpdateMapTree();
        }
        private Color DecideColor2(int cmpIndex)
        {
            var valid = FlowFormData._flowHandle[cmpIndex].IndexValid(cmpIndex + 1);
            var componentStateEm = FlowFormData._flowSetupHandle.IsKeyComponent(cmpIndex);

            var useColor = Color.Black;
            if (valid == false)
            {
                useColor = Color.Red;
            }
            else
            {
                switch (componentStateEm)
                {
                    case ComponentProperty.ComponentStateEm.Enable:
                        useColor = Color.Black;
                        break;
                    case ComponentProperty.ComponentStateEm.Disable:
                        useColor = Color.Gray;
                        break;
                    case ComponentProperty.ComponentStateEm.Ignore:
                        useColor = Color.Blue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }


            if (Editable == false)
            {
                //處理marklearning的部分
                if (FlowFormData._flowHandle[cmpIndex].TypeName.ToLower().Contains("markinspection"))
                {
                    if (useColor != Color.Blue)
                        useColor = Color.Black;
                }
                else
                {
                    useColor = Color.Gray;
                }
            }

            return useColor;
        }

        
        public void ShowOnTeachPanel(Form form)
        {
            ShowPanelTabControl.ShowTabHeader = DefaultBoolean.False;
            ShowPanelTabControl.TabPages.Clear();
            ShowPanelTabControl.TabPages.Add(ShowPanelPage);
            ShowPanelTabControl.SelectedTabPage = ShowPanelPage; 
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.FormClosed += ChangePanel;
            ShowPanelPage.Controls.Add(form);
            
            form.Show();
        }

        public void ChangePanel(object sender,FormClosedEventArgs e)
        {
            ShowPanelTabControl.TabPages.Clear();
            ShowPanelTabControl.TabPages.Add(OriginPage);
            ShowPanelTabControl.SelectedTabPage = OriginPage;
        }

        public void OpenCmpForm(Form form)
        {
            ShowPanelTabControl.ShowTabHeader = DefaultBoolean.False;
            ShowPanelTabControl.TabPages.Clear();
            ShowPanelTabControl.TabPages.Add(ShowPanelPage);
            ShowPanelTabControl.SelectedTabPage = ShowPanelPage;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.FormClosed += ChangePanel;
            ShowPanelPage.Controls.Add(form);

            form.Show();
        }

        public void CloseCmpForm(BasicComponent cmp)
        {
            _dispatcher.Invoke(() =>
            {
                ShowPanelTabControl.TabPages.Clear();
                ShowPanelTabControl.TabPages.Add(OriginPage);
                ShowPanelTabControl.SelectedTabPage = OriginPage;
            });
        }

        
        
    }
}
