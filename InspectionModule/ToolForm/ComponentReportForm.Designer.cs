using System.Windows.Forms;

namespace TA2000Modules
{
    partial class ComponentReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentReportForm));
            this.LV_CustomShow = new System.Windows.Forms.ListView();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnAdvance = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabP_Image = new System.Windows.Forms.TabPage();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.label4 = new System.Windows.Forms.Label();
            this.COB_ImageSelect = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.TabP_Image.SuspendLayout();
            this.SuspendLayout();
            // 
            // LV_CustomShow
            // 
            resources.ApplyResources(this.LV_CustomShow, "LV_CustomShow");
            this.LV_CustomShow.GridLines = true;
            this.LV_CustomShow.HideSelection = false;
            this.LV_CustomShow.Name = "LV_CustomShow";
            this.LV_CustomShow.UseCompatibleStateImageBehavior = false;
            this.LV_CustomShow.View = System.Windows.Forms.View.Details;
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnAdvance
            // 
            resources.ApplyResources(this.BtnAdvance, "BtnAdvance");
            this.BtnAdvance.Name = "BtnAdvance";
            this.BtnAdvance.UseVisualStyleBackColor = true;
            this.BtnAdvance.Click += new System.EventHandler(this.btn_Advance_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.TabP_Image);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // TabP_Image
            // 
            resources.ApplyResources(this.TabP_Image, "TabP_Image");
            this.TabP_Image.Controls.Add(this.hWindowControl);
            this.TabP_Image.Controls.Add(this.label4);
            this.TabP_Image.Controls.Add(this.COB_ImageSelect);
            this.TabP_Image.Name = "TabP_Image";
            this.TabP_Image.UseVisualStyleBackColor = true;
            // 
            // hWindowControl
            // 
            resources.ApplyResources(this.hWindowControl, "hWindowControl");
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.WindowSize = new System.Drawing.Size(668, 449);
            this.hWindowControl.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseMove);
            this.hWindowControl.HMouseDown += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseDown);
            this.hWindowControl.HMouseUp += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseUp);
            this.hWindowControl.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseWheel);
            this.hWindowControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hWindowControl_MouseDown);
            this.hWindowControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hWindowControl_MouseMove);
            this.hWindowControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hWindowControl_MouseUp);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // COB_ImageSelect
            // 
            resources.ApplyResources(this.COB_ImageSelect, "COB_ImageSelect");
            this.COB_ImageSelect.FormattingEnabled = true;
            this.COB_ImageSelect.Name = "COB_ImageSelect";
            this.COB_ImageSelect.SelectedIndexChanged += new System.EventHandler(this.COB_ImageSelect_SelectedIndexChanged);
            // 
            // ComponentReportForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.BtnAdvance);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LV_CustomShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ComponentReportForm";
            this.Shown += new System.EventHandler(this.ComponentReportForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.TabP_Image.ResumeLayout(false);
            this.TabP_Image.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView LV_CustomShow;
        private Button BtnClose;
        private Button BtnAdvance;
        private TabControl tabControl1;
        private TabPage TabP_Image;
        private Label label4;
        private ComboBox COB_ImageSelect;
        private HalconDotNet.HWindowControl hWindowControl;
    }
}