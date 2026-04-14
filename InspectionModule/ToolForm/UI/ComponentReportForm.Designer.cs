using System.Windows.Forms;

namespace TA2100Modules
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
            this.LV_CustomShow.GridLines = true;
            this.LV_CustomShow.HideSelection = false;
            this.LV_CustomShow.Location = new System.Drawing.Point(12, 12);
            this.LV_CustomShow.Name = "LV_CustomShow";
            this.LV_CustomShow.Size = new System.Drawing.Size(810, 127);
            this.LV_CustomShow.TabIndex = 1;
            this.LV_CustomShow.UseCompatibleStateImageBehavior = false;
            this.LV_CustomShow.View = System.Windows.Forms.View.Details;
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(828, 12);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(36, 33);
            this.BtnClose.TabIndex = 16;
            this.BtnClose.Text = "Hide";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnAdvance
            // 
            this.BtnAdvance.Location = new System.Drawing.Point(828, 106);
            this.BtnAdvance.Name = "BtnAdvance";
            this.BtnAdvance.Size = new System.Drawing.Size(75, 33);
            this.BtnAdvance.TabIndex = 17;
            this.BtnAdvance.Text = "Close Data";
            this.BtnAdvance.UseVisualStyleBackColor = true;
            this.BtnAdvance.Click += new System.EventHandler(this.btn_Advance_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabP_Image);
            this.tabControl1.Location = new System.Drawing.Point(12, 145);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(903, 573);
            this.tabControl1.TabIndex = 18;
            // 
            // TabP_Image
            // 
            this.TabP_Image.Controls.Add(this.hWindowControl);
            this.TabP_Image.Controls.Add(this.label4);
            this.TabP_Image.Controls.Add(this.COB_ImageSelect);
            this.TabP_Image.Location = new System.Drawing.Point(4, 22);
            this.TabP_Image.Name = "TabP_Image";
            this.TabP_Image.Padding = new System.Windows.Forms.Padding(3);
            this.TabP_Image.Size = new System.Drawing.Size(895, 547);
            this.TabP_Image.TabIndex = 1;
            this.TabP_Image.Text = "Image";
            this.TabP_Image.UseVisualStyleBackColor = true;
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(22, 24);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(668, 449);
            this.hWindowControl.TabIndex = 3;
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
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(754, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "影像選擇";
            // 
            // COB_ImageSelect
            // 
            this.COB_ImageSelect.FormattingEnabled = true;
            this.COB_ImageSelect.Location = new System.Drawing.Point(756, 81);
            this.COB_ImageSelect.Name = "COB_ImageSelect";
            this.COB_ImageSelect.Size = new System.Drawing.Size(121, 20);
            this.COB_ImageSelect.TabIndex = 1;
            this.COB_ImageSelect.SelectedIndexChanged += new System.EventHandler(this.COB_ImageSelect_SelectedIndexChanged);
            // 
            // ComponentReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 731);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.BtnAdvance);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.LV_CustomShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(950, 950);
            this.MaximizeBox = false;
            this.Name = "ComponentReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ComponentReportForm";
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