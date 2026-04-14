namespace TA2000Modules
{
    partial class WizardForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardForm));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_Prev = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Next = new DevExpress.XtraEditors.SimpleButton();
            this.TreeViewImage = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel2.Controls.Add(this.panel2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1176, 568);
            this.splitContainerControl1.SplitterPosition = 535;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainerControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 530);
            this.panel1.TabIndex = 0;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(631, 530);
            this.splitContainerControl2.SplitterPosition = 301;
            this.splitContainerControl2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_Prev);
            this.panel2.Controls.Add(this.Btn_Next);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 530);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 38);
            this.panel2.TabIndex = 6;
            // 
            // Btn_Prev
            // 
            this.Btn_Prev.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Prev.Location = new System.Drawing.Point(481, 0);
            this.Btn_Prev.Name = "Btn_Prev";
            this.Btn_Prev.Size = new System.Drawing.Size(75, 38);
            this.Btn_Prev.TabIndex = 1;
            this.Btn_Prev.Text = "Prev";
            this.Btn_Prev.Click += new System.EventHandler(this.Btn_Prev_Click);
            // 
            // Btn_Next
            // 
            this.Btn_Next.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Next.Location = new System.Drawing.Point(556, 0);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(75, 38);
            this.Btn_Next.TabIndex = 0;
            this.Btn_Next.Text = "Next";
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // TreeViewImage
            // 
            this.TreeViewImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeViewImage.ImageStream")));
            this.TreeViewImage.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeViewImage.Images.SetKeyName(0, "check-mark.png");
            this.TreeViewImage.Images.SetKeyName(1, "icon (1).png");
            this.TreeViewImage.Images.SetKeyName(2, "folder.png");
            this.TreeViewImage.Images.SetKeyName(3, "icon.png");
            this.TreeViewImage.Images.SetKeyName(4, "image_SXg_icon.ico");
            // 
            // WizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 568);
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WizardForm";
            this.Text = "WizardForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WizardForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton Btn_Prev;
        private DevExpress.XtraEditors.SimpleButton Btn_Next;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private System.Windows.Forms.ImageList TreeViewImage;
    }
}