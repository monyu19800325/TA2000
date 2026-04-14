namespace TA2000Modules
{
    partial class LaserTeachControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserTeachControl));
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.Btn_SetTarget = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SearchTarget = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SetLight = new DevExpress.XtraEditors.SimpleButton();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.LB_SearchScore = new DevExpress.XtraEditors.LabelControl();
            this.NUD_SearchScore = new System.Windows.Forms.NumericUpDown();
            this.Btn_Test = new DevExpress.XtraEditors.SimpleButton();
            this.LB_HeightMax = new DevExpress.XtraEditors.LabelControl();
            this.LB_HeightMin = new DevExpress.XtraEditors.LabelControl();
            this.NUD_HeightMax = new System.Windows.Forms.NumericUpDown();
            this.NUD_HeightMin = new System.Windows.Forms.NumericUpDown();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SearchScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_HeightMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_HeightMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            resources.ApplyResources(this.hWindowControl1, "hWindowControl1");
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.WindowSize = new System.Drawing.Size(515, 404);
            // 
            // Btn_SetTarget
            // 
            resources.ApplyResources(this.Btn_SetTarget, "Btn_SetTarget");
            this.Btn_SetTarget.Name = "Btn_SetTarget";
            // 
            // Btn_SearchTarget
            // 
            resources.ApplyResources(this.Btn_SearchTarget, "Btn_SearchTarget");
            this.Btn_SearchTarget.Name = "Btn_SearchTarget";
            // 
            // Btn_Save
            // 
            resources.ApplyResources(this.Btn_Save, "Btn_Save");
            this.Btn_Save.Name = "Btn_Save";
            // 
            // Btn_SetLight
            // 
            resources.ApplyResources(this.Btn_SetLight, "Btn_SetLight");
            this.Btn_SetLight.Name = "Btn_SetLight";
            // 
            // toggleSwitch1
            // 
            resources.ApplyResources(this.toggleSwitch1, "toggleSwitch1");
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = resources.GetString("toggleSwitch1.Properties.OffText");
            this.toggleSwitch1.Properties.OnText = resources.GetString("toggleSwitch1.Properties.OnText");
            // 
            // labelControl11
            // 
            resources.ApplyResources(this.labelControl11, "labelControl11");
            this.labelControl11.Name = "labelControl11";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // trackBar2
            // 
            resources.ApplyResources(this.trackBar2, "trackBar2");
            this.trackBar2.Maximum = 255;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Value = 255;
            // 
            // labelControl12
            // 
            resources.ApplyResources(this.labelControl12, "labelControl12");
            this.labelControl12.Name = "labelControl12";
            // 
            // labelControl13
            // 
            resources.ApplyResources(this.labelControl13, "labelControl13");
            this.labelControl13.Name = "labelControl13";
            // 
            // trackBar1
            // 
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.Maximum = 254;
            this.trackBar1.Name = "trackBar1";
            // 
            // LB_SearchScore
            // 
            this.LB_SearchScore.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LB_SearchScore.Appearance.Font")));
            this.LB_SearchScore.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.LB_SearchScore, "LB_SearchScore");
            this.LB_SearchScore.Name = "LB_SearchScore";
            // 
            // NUD_SearchScore
            // 
            resources.ApplyResources(this.NUD_SearchScore, "NUD_SearchScore");
            this.NUD_SearchScore.Name = "NUD_SearchScore";
            this.NUD_SearchScore.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // Btn_Test
            // 
            resources.ApplyResources(this.Btn_Test, "Btn_Test");
            this.Btn_Test.Name = "Btn_Test";
            // 
            // LB_HeightMax
            // 
            this.LB_HeightMax.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LB_HeightMax.Appearance.Font")));
            this.LB_HeightMax.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.LB_HeightMax, "LB_HeightMax");
            this.LB_HeightMax.Name = "LB_HeightMax";
            // 
            // LB_HeightMin
            // 
            this.LB_HeightMin.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LB_HeightMin.Appearance.Font")));
            this.LB_HeightMin.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.LB_HeightMin, "LB_HeightMin");
            this.LB_HeightMin.Name = "LB_HeightMin";
            // 
            // NUD_HeightMax
            // 
            resources.ApplyResources(this.NUD_HeightMax, "NUD_HeightMax");
            this.NUD_HeightMax.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.NUD_HeightMax.Name = "NUD_HeightMax";
            this.NUD_HeightMax.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // NUD_HeightMin
            // 
            resources.ApplyResources(this.NUD_HeightMin, "NUD_HeightMin");
            this.NUD_HeightMin.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.NUD_HeightMin.Name = "NUD_HeightMin";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            // 
            // LaserTeachControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.NUD_HeightMin);
            this.Controls.Add(this.NUD_HeightMax);
            this.Controls.Add(this.LB_HeightMin);
            this.Controls.Add(this.LB_HeightMax);
            this.Controls.Add(this.Btn_Test);
            this.Controls.Add(this.NUD_SearchScore);
            this.Controls.Add(this.LB_SearchScore);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.Btn_SetLight);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.Btn_SearchTarget);
            this.Controls.Add(this.Btn_SetTarget);
            this.Controls.Add(this.hWindowControl1);
            this.Name = "LaserTeachControl";
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SearchScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_HeightMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_HeightMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private DevExpress.XtraEditors.SimpleButton Btn_SetTarget;
        private DevExpress.XtraEditors.SimpleButton Btn_SearchTarget;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private DevExpress.XtraEditors.SimpleButton Btn_SetLight;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.TrackBar trackBar2;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private System.Windows.Forms.TrackBar trackBar1;
        private DevExpress.XtraEditors.LabelControl LB_SearchScore;
        private System.Windows.Forms.NumericUpDown NUD_SearchScore;
        private System.Windows.Forms.NumericUpDown NUD_HeightMin;
        private System.Windows.Forms.NumericUpDown NUD_HeightMax;
        private DevExpress.XtraEditors.LabelControl LB_HeightMin;
        private DevExpress.XtraEditors.LabelControl LB_HeightMax;
        private DevExpress.XtraEditors.SimpleButton Btn_Test;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}