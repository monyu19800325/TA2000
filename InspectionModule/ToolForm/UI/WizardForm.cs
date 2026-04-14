using TA2000Modules;
using HalconDotNet;
using HTA.MainController;
using HTAMachine.Machine;
using HTAMachine.Machine.Services;
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
using TA2000Modules;
using TA2000Modules.ToolForm;
using VisionComponent;
using VisionController2.FlowForm.FlowSettingForm2;
using static HTA.MainController.MainController;
using HTAMachine.Machine.Services;
using HTAMachine.Machine;

namespace TA2000Modules
{
    public partial class WizardForm : DevExpress.XtraEditors.XtraForm, IMultilingual
    {
        int pageIndex = -1;
        int prevPageIndex = -1;
        MainController _mainController;
        MainController.ExternalHandleClass _externalHandleClass;
        MainController _mosaicMainController;
        MainController.ExternalHandleClass _mosaicExternalHandleClass;
        public InspectionModule Module;
        ProductSettingTool _productSettingTool;
        FlowForm _flowForm;
        TeachControlWizardForm3 _teachControlWizardForm3;
        CurrentImageView _currentImageView;
        bool _panel1Exist = false;
        Form _panel1Form;
        TeachTestForm _teachTestForm;
        GoldenTeachControl _goldenTeachControl;
        Golden3DTeachControl _golden3dTeachControl;

        public WizardForm(InspectionModule module)
        {
            InitializeComponent();
            _mainController = (MainController)module.VisionController;
            _externalHandleClass = module.VisionController.GetHardware();
            _mosaicMainController = (MainController)module.VisionController_Mosaic;
            _mosaicExternalHandleClass = module.VisionController_Mosaic.GetHardware();
            Module = module;
            Btn_Next_Click(this, null);
        }
        public void OpenComponentFormInPanel(Form form)
        {
            if (form is FlowSettingForm2 flowSettingForm2)
            {
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                this.splitContainerControl2.SplitterPosition = 500;
                this.splitContainerControl2.IsSplitterFixed = true;
                this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                this.splitContainerControl2.Panel1.Controls.Clear();
                this.splitContainerControl2.Panel1.Controls.Add(form);
                form.Scale(new SizeF(1f, 0.85f));
                flowSettingForm2.FormClosed += FlowSetttingFormClosed;
                _panel1Exist = true;
            }
            else
            {
                if (_panel1Exist == true)
                {
                    _panel1Form?.Close();
                    _panel1Form = null;
                }
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                this.splitContainerControl2.SplitterPosition = 560;
                this.splitContainerControl2.IsSplitterFixed =true;
                this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                this.splitContainerControl2.Panel1.Controls.Clear();
                this.splitContainerControl2.Panel1.Controls.Add(form);
                form.Show();
            }
        }

        public void FlowSetttingFormClosed(object sender, FormClosedEventArgs e)
        {
            this.splitContainerControl2.Panel1.Controls.Clear();
            this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            _panel1Exist = false;
        }

        public void CloseComponentForm(BasicComponent cmp)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<BasicComponent>(CloseComponentForm), cmp);
            }
            else
            {
                this.splitContainerControl2.Panel1.Controls.Clear();
                this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            }
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            pageIndex++;
            try
            {
                Module.ShowDefaultWaitFormService.ShowWaitForm();
                ChangePage();
            }
            catch (Exception ex)
            {
                Module.LogTrace($"ChangePage 失敗。exp:{ex}");
                Module.OnAddFatal("InspectionModule", ex.Message, ex);
            }
            finally
            {
                Module.ShowDefaultWaitFormService.CloseWaitForm();
                prevPageIndex = pageIndex;
            }
        }

        private void Btn_Prev_Click(object sender, EventArgs e)
        {
            if (pageIndex < 0)
                return;
            pageIndex--;
            try
            {
                Module.ShowDefaultWaitFormService.ShowWaitForm();
                ChangePage();
            }
            catch (Exception ex)
            {
                Module.LogTrace($"ChangePage 失敗。exp:{ex}");
                Module.OnAddFatal("InspectionModule", ex.Message, ex);
            }
            finally
            {
                Module.ShowDefaultWaitFormService.CloseWaitForm();
                prevPageIndex = pageIndex;
            }
        }
        public void ChangePage()
        {
            _panel1Exist = false;
            this.splitContainerControl1.Panel1.Controls.Clear();
            this.splitContainerControl2.Panel2.Controls.Clear();
            //this.panel1.Controls.Clear();
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            _productSettingTool?.Close();
            Btn_Next.Enabled = true;
            Btn_Prev.Enabled = true;
            CloseFlowForm();
            _teachControlWizardForm3?.Close();
            _teachControlWizardForm3?.Dispose();
            _teachControlWizardForm3 = null;
            _currentImageView?.Close();
            _currentImageView = null;
            _teachTestForm?.Close();
            _teachTestForm = null;
            panel?.Dispose();
            panel = null;
            smallPanel?.Dispose();
            smallPanel = null;
            if (pageIndex == 2)
            {
                if (Module.ProductParam.BigProductMapSetting.MapList.All(x => x.UseType == "Mosaic"))
                {
                    if(prevPageIndex < pageIndex)
                    {
                        pageIndex++;
                    }
                    else
                    {
                        pageIndex--;
                    }
                }
            }
            else if(pageIndex == 1)
            {
                if (Module.ProductParam.BigProductMapSetting.MapList.All(x => x.UseType != "Mosaic") ||
                    Module.CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() ==     ProductTypeEm.SmallProduct)
                {
                    if (prevPageIndex < pageIndex)
                    {
                        pageIndex = 2;
                    }
                    else
                    {
                        pageIndex = 0;
                        if (Module.CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
                        {
                            pageIndex = 2;
                        }
                    }
                }
            }
            else if (pageIndex == 0)
            {
                if (Module.CurrentTrayCarrier.InspectData.InspectionPostion.GetProductType() == ProductTypeEm.SmallProduct)
                {
                    if (prevPageIndex < pageIndex)
                    {
                        pageIndex = 2;
                    }
                    else
                    {
                        pageIndex = 2;
                    }
                }
            }

            switch (pageIndex)
            {
                case 0:
                    this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                    _productSettingTool = new ProductSettingTool(Module);
                    _productSettingTool.TopLevel = false;
                    _productSettingTool.FormBorderStyle = FormBorderStyle.None;
                    _productSettingTool.Dock = DockStyle.Fill;
                    this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                    this.splitContainerControl2.Panel2.Controls.Add(_productSettingTool);
                    _productSettingTool.Show();
                    Btn_Prev.Enabled = false;
                    break;
                case 1:
                    {
                        this.splitContainerControl1.SplitterPosition = 1080;
                        this.splitContainerControl1.IsSplitterFixed = true;
                        _flowForm = new FlowForm(_mosaicMainController, _mosaicExternalHandleClass, true);
                        _flowForm.TopLevel = false;
                        _flowForm.FormBorderStyle = FormBorderStyle.None;
                        _flowForm.Dock = DockStyle.Fill;
                        //_flowForm.ComponentFormOtherAction = OpenComponentFormInPanel;
                        //_flowForm.OnComponentFormClose = CloseComponentForm;
                        this.splitContainerControl1.Panel1.Controls.Add(_flowForm);
                        _flowForm.Show();

                        if (Module.IsGolden)
                        {
                            _flowForm.ComponentFormOtherAction = OpenComponentFormInPanel;
                            _flowForm.OnComponentFormClose = CloseComponentForm;
                            _goldenTeachControl = new GoldenTeachControl(Module, _flowForm);
                            _goldenTeachControl.TopLevel = false;
                            _goldenTeachControl.FormBorderStyle = FormBorderStyle.None;
                            _goldenTeachControl.Dock = DockStyle.Fill;
                            _goldenTeachControl.OnNotifyOuter = GetInspecting;
                            this.splitContainerControl2.Panel2.Controls.Add(_goldenTeachControl);
                            _goldenTeachControl.Show();
                            _goldenTeachControl.FormClosed += (s, e) =>
                            {
                                this.Close();
                            };
                            Btn_Next.Enabled = false;
                        }
                        else
                        {
                            CreateNewPanelView("Mosaic");
                        }
                    }
                    break;
                case 2:
                    {
                        this.splitContainerControl1.SplitterPosition = 1080;
                        _flowForm = new FlowForm(_mainController, _externalHandleClass, true);
                        _flowForm.TopLevel = false;
                        _flowForm.FormBorderStyle = FormBorderStyle.None;
                        _flowForm.Dock = DockStyle.Fill;
                        //_flowForm.ComponentFormOtherAction = OpenComponentFormInPanel;
                        //_flowForm.OnComponentFormClose = CloseComponentForm;
                        this.splitContainerControl1.Panel1.Controls.Add(_flowForm);
                        _flowForm.Show();

                        this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                        if (Module.Is3DGolden)
                        {
                            _flowForm.ComponentFormOtherAction = OpenComponentFormInPanel;
                            _flowForm.OnComponentFormClose = CloseComponentForm;
                            _golden3dTeachControl = new Golden3DTeachControl(Module, _flowForm);
                            _golden3dTeachControl.TopLevel = false;
                            _golden3dTeachControl.FormBorderStyle = FormBorderStyle.None;
                            _golden3dTeachControl.Dock = DockStyle.Fill;
                            _golden3dTeachControl.OnShowSplitPanel = AddSplitPanel1;
                            _golden3dTeachControl.OnNotifyOuter = GetInspecting;
                            this.splitContainerControl2.Panel2.Controls.Add(_golden3dTeachControl);
                            _golden3dTeachControl.Show();
                            _golden3dTeachControl.FormClosed += (s, e) =>
                            {
                                this.Close();
                            };
                            Btn_Next.Enabled = false;
                        }
                        else if (Module.IsGolden)
                        {
                            _flowForm.ComponentFormOtherAction = OpenComponentFormInPanel;
                            _flowForm.OnComponentFormClose = CloseComponentForm;
                            _goldenTeachControl = new GoldenTeachControl(Module, _flowForm);
                            _goldenTeachControl.TopLevel = false;
                            _goldenTeachControl.FormBorderStyle = FormBorderStyle.None;
                            _goldenTeachControl.Dock = DockStyle.Fill;
                            _goldenTeachControl.OnNotifyOuter = GetInspecting;
                            this.splitContainerControl2.Panel2.Controls.Add(_goldenTeachControl);
                            _goldenTeachControl.Show();
                            _goldenTeachControl.FormClosed += (s, e) =>
                            {
                                this.Close();
                            };
                            Btn_Next.Enabled = false;
                        }
                        else
                        {
                            CreateNewPanelView("Not Mosaic");
                        }
                    }
                    break;
                case 3:
                    {
                        if (Module.IsGolden)
                        {
                            break;
                        }
                        this.splitContainerControl1.SplitterPosition = 1000;
                        _currentImageView = new CurrentImageView(_mosaicMainController);
                        _currentImageView.TopLevel = false;
                        _currentImageView.FormBorderStyle = FormBorderStyle.None;
                        _currentImageView.Dock = DockStyle.Fill;
                        this.splitContainerControl1.Panel1.Controls.Add(_currentImageView);
                        _currentImageView.Show();
                        _teachTestForm = new TeachTestForm(Module);
                        _teachTestForm.TopLevel = false;
                        _teachTestForm.FormBorderStyle = FormBorderStyle.None;
                        _teachTestForm.Dock = DockStyle.Fill;
                        _teachTestForm.OnCloseWizard =() =>
                        {
                            this.Close();
                        };
                        _teachTestForm.NotifyTeachInspect =() =>
                        {
                            Btn_Prev.Enabled = false;
                        };
                        this.splitContainerControl2.Panel2.Controls.Add(_teachTestForm);
                        _teachTestForm.Show();
                        Btn_Next.Enabled = false;
                    }
                    break;
                default:
                    break;

            }
        }

        public void AddSplitPanel1(Form form, int size)
        {
            if (this.splitContainerControl2.Panel1.Controls.Count != 0)
                return;
            this.splitContainerControl2.Panel1.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            this.splitContainerControl2.SplitterPosition = size;
            this.splitContainerControl2.IsSplitterFixed = true;
            this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            this.splitContainerControl2.Panel1.Controls.Clear();
            this.splitContainerControl2.Panel1.Controls.Add(form);
            form.Scale(new SizeF(1f, 0.85f));
            form.FormClosed += FlowSetttingFormClosed;
            form.Show();
            _panel1Form = form;
            _panel1Exist = true;
        }
        public void GetInspecting()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(GetInspecting));
            }
            else
            {
                _flowForm?.Close();
                _flowForm?.Dispose();
                _flowForm = null;
                this.splitContainerControl1.Panel1.Controls.Clear();
                this.splitContainerControl1.SplitterPosition = 1300;
                _currentImageView = new CurrentImageView(_mosaicMainController);
                _currentImageView.TopLevel = false;
                _currentImageView.FormBorderStyle = FormBorderStyle.None;
                _currentImageView.Dock = DockStyle.Fill;
                _currentImageView.Scale(new SizeF(0.9f, 1f));
                this.splitContainerControl1.Panel1.Controls.Add(_currentImageView);
                _currentImageView.Show();
            }
        }
        public void ReturnTeach()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(ReturnTeach));
            }
            else
            {
                if (_teachTestForm != null)
                {
                    _teachTestForm.EnableButton();
                    Btn_Prev.Enabled = true;
                    Module.DialogService.ShowDialog(Module, new ShowDialogArgs()
                    {
                        Button = MessageBoxButtons.OK,
                        Caption = "Info",
                        Message = "教讀檢測完成"
                    });
                }
            }
        }

        Panel panel;
        Panel smallPanel;

        public void CreateNewPanelView(string type)
        {
            smallPanel = new Panel();
            smallPanel.Width = 700;
            smallPanel.Height = 50;
            smallPanel.Location = new Point(868, 720);

            var treeView = _flowForm.GetFlowTreeView();


            panel = new Panel();
            panel.Width = treeView.Size.Width + 900;//196
            panel.Height = treeView.Size.Height+100;//627
            if (Math.Abs(panel.Width-1112) > 1)
                panel.Width = 1112;
            if (Math.Abs(panel.Height-727) > 1)
                panel.Height = 727;
            var pointScreen = treeView.PointToScreen(Point.Empty);
            var targetPoint = this.PointToClient(pointScreen);
            panel.Location = new Point(targetPoint.X-2, targetPoint.Y-120);//806 ,114
            if (Math.Abs(panel.Location.X-868) > 1 || Math.Abs(panel.Location.Y+6) > 1)
            {
                panel.Location = new Point(868, -6);
            }

            if (type == "Mosaic")
            {
                _teachControlWizardForm3 = new TeachControlWizardForm3(type, Module, _flowForm, Module.VisionController_Mosaic, this);
            }
            else
            {
                _teachControlWizardForm3 = new TeachControlWizardForm3(type, Module, _flowForm, Module.VisionController, this);
            }
            _teachControlWizardForm3.TopLevel = false;
            _teachControlWizardForm3.FormBorderStyle = FormBorderStyle.None;
            _teachControlWizardForm3.Dock = DockStyle.Fill;


            panel.Controls.Add(_teachControlWizardForm3);
            this.Controls.Add(panel);
            this.Controls.Add(smallPanel);
            _teachControlWizardForm3.Show();
            panel.BringToFront();
            smallPanel.BringToFront();
        }


        private void WizardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseFlowForm();
            _teachControlWizardForm3?.Close();
            _teachControlWizardForm3?.Dispose();
            _teachControlWizardForm3 = null;
            _currentImageView?.Close();
            _currentImageView = null;
        }

        public void CloseFlowForm()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action(CloseFlowForm));
            }
            else
            {
                _flowForm?.Close();
                _flowForm?.Dispose();
                _flowForm = null;
            }
        }

        public void SetLanguage(ComponentResourceManager resources)
        {
            //System.ComponentModel.ComponentResourceManager resources2 = new System.ComponentModel.ComponentResourceManager(typeof(WizardForm));
            //resources2.ApplyResources(this, "$this");

            var oldWindowState = this.WindowState;
            var oldBounds = this.Bounds;
            var oldClientSize = this.ClientSize;

            if (this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;

            var resources2 = new ComponentResourceManager(typeof(WizardForm));

            this.SuspendLayout();
            resources2.ApplyResources(this, "$this");
            this.Bounds = oldBounds;
            this.ClientSize = oldClientSize;

            this.ResumeLayout(false);

            switch (pageIndex)
            {
                case 0:

                    break;
                case 1:
                    {
                        if (Module.IsGolden)
                        {
                            if (_goldenTeachControl is IMultilingual multilingual)
                            {
                                multilingual.SetLanguage(resources);
                            }
                        }
                        else
                        {
                            if (_teachControlWizardForm3 is IMultilingual multilingual2)
                            {
                                multilingual2.SetLanguage(resources);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        if (Module.Is3DGolden)
                        {
                            if (_golden3dTeachControl is IMultilingual multilingual)
                            {
                                multilingual.SetLanguage(resources);
                            }
                        }
                        else
                        {
                            if (_teachControlWizardForm3 is IMultilingual multilingual2)
                            {
                                multilingual2.SetLanguage(resources);
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        if (_teachTestForm is IMultilingual multilingual2)
                        {
                            multilingual2.SetLanguage(resources);
                        }
                    }
                    break;

            }


            this.WindowState = oldWindowState;
            this.PerformLayout();
            this.Refresh();
        }
    }
}
