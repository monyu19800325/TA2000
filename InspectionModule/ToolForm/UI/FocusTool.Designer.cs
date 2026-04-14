namespace TA2000Modules
{
    partial class FocusTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FocusTool));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.LUE_Step = new DevExpress.XtraEditors.LookUpEdit();
            this.LUE_Dir = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Btn_BaseMove = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Move = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.Btn_AutoFocus = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectRegion = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.SE_CapTimes = new DevExpress.XtraEditors.SpinEdit();
            this.SE_MoveRange = new DevExpress.XtraEditors.SpinEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.LightPanel = new DevExpress.XtraEditors.PanelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TE_Offset = new DevExpress.XtraEditors.TextEdit();
            this.TE_Pos = new DevExpress.XtraEditors.TextEdit();
            this.TE_AxisName = new DevExpress.XtraEditors.TextEdit();
            this.LUE_CaptureIndex = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.LUE_GroupIndex = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Step.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Dir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_CapTimes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_MoveRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LightPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Offset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Pos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_AxisName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_CaptureIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_GroupIndex.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Name = "labelControl1";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Name = "labelControl3";
            // 
            // groupControl1
            // 
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Controls.Add(this.LUE_Step);
            this.groupControl1.Controls.Add(this.LUE_Dir);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.Btn_BaseMove);
            this.groupControl1.Controls.Add(this.Btn_Move);
            this.groupControl1.Name = "groupControl1";
            // 
            // LUE_Step
            // 
            resources.ApplyResources(this.LUE_Step, "LUE_Step");
            this.LUE_Step.Name = "LUE_Step";
            this.LUE_Step.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LUE_Step.Properties.Appearance.Font")));
            this.LUE_Step.Properties.Appearance.Options.UseFont = true;
            this.LUE_Step.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("LUE_Step.Properties.Buttons"))))});
            // 
            // LUE_Dir
            // 
            resources.ApplyResources(this.LUE_Dir, "LUE_Dir");
            this.LUE_Dir.Name = "LUE_Dir";
            this.LUE_Dir.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LUE_Dir.Properties.Appearance.Font")));
            this.LUE_Dir.Properties.Appearance.Options.UseFont = true;
            this.LUE_Dir.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("LUE_Dir.Properties.Buttons"))))});
            // 
            // labelControl5
            // 
            resources.ApplyResources(this.labelControl5, "labelControl5");
            this.labelControl5.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl5.Appearance.Font")));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Name = "labelControl5";
            // 
            // labelControl4
            // 
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl4.Appearance.Font")));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Name = "labelControl4";
            // 
            // Btn_BaseMove
            // 
            resources.ApplyResources(this.Btn_BaseMove, "Btn_BaseMove");
            this.Btn_BaseMove.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_BaseMove.Appearance.Font")));
            this.Btn_BaseMove.Appearance.Options.UseFont = true;
            this.Btn_BaseMove.Name = "Btn_BaseMove";
            // 
            // Btn_Move
            // 
            resources.ApplyResources(this.Btn_Move, "Btn_Move");
            this.Btn_Move.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_Move.Appearance.Font")));
            this.Btn_Move.Appearance.Options.UseFont = true;
            this.Btn_Move.Name = "Btn_Move";
            // 
            // groupControl2
            // 
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Controls.Add(this.chart1);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.Btn_AutoFocus);
            this.groupControl2.Controls.Add(this.Btn_SelectRegion);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.SE_CapTimes);
            this.groupControl2.Controls.Add(this.SE_MoveRange);
            this.groupControl2.Name = "groupControl2";
            // 
            // chart1
            // 
            resources.ApplyResources(this.chart1, "chart1");
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            // 
            // labelControl8
            // 
            resources.ApplyResources(this.labelControl8, "labelControl8");
            this.labelControl8.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl8.Appearance.Font")));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Name = "labelControl8";
            // 
            // Btn_AutoFocus
            // 
            resources.ApplyResources(this.Btn_AutoFocus, "Btn_AutoFocus");
            this.Btn_AutoFocus.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_AutoFocus.Appearance.Font")));
            this.Btn_AutoFocus.Appearance.Options.UseFont = true;
            this.Btn_AutoFocus.Name = "Btn_AutoFocus";
            // 
            // Btn_SelectRegion
            // 
            resources.ApplyResources(this.Btn_SelectRegion, "Btn_SelectRegion");
            this.Btn_SelectRegion.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_SelectRegion.Appearance.Font")));
            this.Btn_SelectRegion.Appearance.Options.UseFont = true;
            this.Btn_SelectRegion.Name = "Btn_SelectRegion";
            // 
            // labelControl7
            // 
            resources.ApplyResources(this.labelControl7, "labelControl7");
            this.labelControl7.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl7.Appearance.Font")));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Name = "labelControl7";
            // 
            // labelControl6
            // 
            resources.ApplyResources(this.labelControl6, "labelControl6");
            this.labelControl6.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl6.Appearance.Font")));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Name = "labelControl6";
            // 
            // SE_CapTimes
            // 
            resources.ApplyResources(this.SE_CapTimes, "SE_CapTimes");
            this.SE_CapTimes.Name = "SE_CapTimes";
            this.SE_CapTimes.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("SE_CapTimes.Properties.Appearance.Font")));
            this.SE_CapTimes.Properties.Appearance.Options.UseFont = true;
            this.SE_CapTimes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("SE_CapTimes.Properties.Buttons"))))});
            // 
            // SE_MoveRange
            // 
            resources.ApplyResources(this.SE_MoveRange, "SE_MoveRange");
            this.SE_MoveRange.Name = "SE_MoveRange";
            this.SE_MoveRange.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("SE_MoveRange.Properties.Appearance.Font")));
            this.SE_MoveRange.Properties.Appearance.Options.UseFont = true;
            this.SE_MoveRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("SE_MoveRange.Properties.Buttons"))))});
            // 
            // groupControl3
            // 
            resources.ApplyResources(this.groupControl3, "groupControl3");
            this.groupControl3.Controls.Add(this.LightPanel);
            this.groupControl3.Name = "groupControl3";
            // 
            // LightPanel
            // 
            resources.ApplyResources(this.LightPanel, "LightPanel");
            this.LightPanel.Name = "LightPanel";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TE_Offset
            // 
            resources.ApplyResources(this.TE_Offset, "TE_Offset");
            this.TE_Offset.Name = "TE_Offset";
            this.TE_Offset.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_Offset.Properties.Appearance.Font")));
            this.TE_Offset.Properties.Appearance.Options.UseFont = true;
            this.TE_Offset.Properties.Appearance.Options.UseTextOptions = true;
            this.TE_Offset.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 
            // TE_Pos
            // 
            resources.ApplyResources(this.TE_Pos, "TE_Pos");
            this.TE_Pos.Name = "TE_Pos";
            this.TE_Pos.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_Pos.Properties.Appearance.Font")));
            this.TE_Pos.Properties.Appearance.Options.UseFont = true;
            this.TE_Pos.Properties.Appearance.Options.UseTextOptions = true;
            this.TE_Pos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 
            // TE_AxisName
            // 
            resources.ApplyResources(this.TE_AxisName, "TE_AxisName");
            this.TE_AxisName.Name = "TE_AxisName";
            this.TE_AxisName.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_AxisName.Properties.Appearance.Font")));
            this.TE_AxisName.Properties.Appearance.Options.UseFont = true;
            this.TE_AxisName.Properties.Appearance.Options.UseTextOptions = true;
            this.TE_AxisName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 
            // LUE_CaptureIndex
            // 
            resources.ApplyResources(this.LUE_CaptureIndex, "LUE_CaptureIndex");
            this.LUE_CaptureIndex.Name = "LUE_CaptureIndex";
            this.LUE_CaptureIndex.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LUE_CaptureIndex.Properties.Appearance.Font")));
            this.LUE_CaptureIndex.Properties.Appearance.Options.UseFont = true;
            this.LUE_CaptureIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("LUE_CaptureIndex.Properties.Buttons"))))});
            // 
            // labelControl11
            // 
            resources.ApplyResources(this.labelControl11, "labelControl11");
            this.labelControl11.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl11.Appearance.Font")));
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Name = "labelControl11";
            // 
            // LUE_GroupIndex
            // 
            resources.ApplyResources(this.LUE_GroupIndex, "LUE_GroupIndex");
            this.LUE_GroupIndex.Name = "LUE_GroupIndex";
            this.LUE_GroupIndex.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LUE_GroupIndex.Properties.Appearance.Font")));
            this.LUE_GroupIndex.Properties.Appearance.Options.UseFont = true;
            this.LUE_GroupIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("LUE_GroupIndex.Properties.Buttons"))))});
            // 
            // labelControl10
            // 
            resources.ApplyResources(this.labelControl10, "labelControl10");
            this.labelControl10.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl10.Appearance.Font")));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Name = "labelControl10";
            // 
            // FocusTool
            // 
            resources.ApplyResources(this, "$this");
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LUE_CaptureIndex);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.LUE_GroupIndex);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.TE_Offset);
            this.Controls.Add(this.TE_Pos);
            this.Controls.Add(this.TE_AxisName);
            this.Name = "FocusTool";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Step.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Dir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_CapTimes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_MoveRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LightPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Offset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Pos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_AxisName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_CaptureIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_GroupIndex.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit TE_AxisName;
        private DevExpress.XtraEditors.TextEdit TE_Pos;
        private DevExpress.XtraEditors.TextEdit TE_Offset;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton Btn_BaseMove;
        private DevExpress.XtraEditors.SimpleButton Btn_Move;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton Btn_AutoFocus;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectRegion;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SpinEdit SE_CapTimes;
        private DevExpress.XtraEditors.SpinEdit SE_MoveRange;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.PanelControl LightPanel;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LookUpEdit LUE_Step;
        private DevExpress.XtraEditors.LookUpEdit LUE_Dir;
        private DevExpress.XtraEditors.LookUpEdit LUE_CaptureIndex;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LookUpEdit LUE_GroupIndex;
        private DevExpress.XtraEditors.LabelControl labelControl10;
    }
}