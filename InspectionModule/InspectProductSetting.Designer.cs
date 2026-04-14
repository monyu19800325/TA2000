namespace TA2000Modules
{
    partial class InspectProductSetting
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InspectProductSetting));
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.CE_Barcode = new DevExpress.XtraEditors.CheckEdit();
            this.CE_InspectByPass = new DevExpress.XtraEditors.CheckEdit();
            this.CE_Reinspect = new DevExpress.XtraEditors.CheckEdit();
            this.CE_Pick = new DevExpress.XtraEditors.CheckEdit();
            this.LUE_Vel = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Pick_dataGrid = new System.Windows.Forms.DataGridView();
            this.LUE_FlyVel = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.TE_FlyVelPercent = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.TE_ReinspectCount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.SE_FailAlarmCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.CE_IsFly = new DevExpress.XtraEditors.CheckEdit();
            this.SE_RejectAlarmCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.CE_LaserMeasure = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_Barcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_InspectByPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_Reinspect.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_Pick.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Vel.Properties)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pick_dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_FlyVel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_FlyVelPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ReinspectCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_FailAlarmCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_IsFly.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_RejectAlarmCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_LaserMeasure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // CE_Barcode
            // 
            resources.ApplyResources(this.CE_Barcode, "CE_Barcode");
            this.CE_Barcode.Name = "CE_Barcode";
            this.CE_Barcode.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_Barcode.Properties.Appearance.Font")));
            this.CE_Barcode.Properties.Appearance.Options.UseFont = true;
            this.CE_Barcode.Properties.Caption = resources.GetString("CE_Barcode.Properties.Caption");
            // 
            // CE_InspectByPass
            // 
            resources.ApplyResources(this.CE_InspectByPass, "CE_InspectByPass");
            this.CE_InspectByPass.Name = "CE_InspectByPass";
            this.CE_InspectByPass.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_InspectByPass.Properties.Appearance.Font")));
            this.CE_InspectByPass.Properties.Appearance.Options.UseFont = true;
            this.CE_InspectByPass.Properties.Caption = resources.GetString("CE_InspectByPass.Properties.Caption");
            // 
            // CE_Reinspect
            // 
            resources.ApplyResources(this.CE_Reinspect, "CE_Reinspect");
            this.CE_Reinspect.Name = "CE_Reinspect";
            this.CE_Reinspect.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_Reinspect.Properties.Appearance.Font")));
            this.CE_Reinspect.Properties.Appearance.Options.UseFont = true;
            this.CE_Reinspect.Properties.Caption = resources.GetString("CE_Reinspect.Properties.Caption");
            // 
            // CE_Pick
            // 
            resources.ApplyResources(this.CE_Pick, "CE_Pick");
            this.CE_Pick.Name = "CE_Pick";
            this.CE_Pick.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_Pick.Properties.Appearance.Font")));
            this.CE_Pick.Properties.Appearance.Options.UseFont = true;
            this.CE_Pick.Properties.Caption = resources.GetString("CE_Pick.Properties.Caption");
            // 
            // LUE_Vel
            // 
            resources.ApplyResources(this.LUE_Vel, "LUE_Vel");
            this.LUE_Vel.Name = "LUE_Vel";
            this.LUE_Vel.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LUE_Vel.Properties.Appearance.Font")));
            this.LUE_Vel.Properties.Appearance.Options.UseFont = true;
            this.LUE_Vel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("LUE_Vel.Properties.Buttons"))))});
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Pick_dataGrid);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Pick_dataGrid
            // 
            this.Pick_dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.Pick_dataGrid, "Pick_dataGrid");
            this.Pick_dataGrid.Name = "Pick_dataGrid";
            this.Pick_dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.Pick_dataGrid.RowTemplate.Height = 24;
            this.Pick_dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            // 
            // LUE_FlyVel
            // 
            resources.ApplyResources(this.LUE_FlyVel, "LUE_FlyVel");
            this.LUE_FlyVel.Name = "LUE_FlyVel";
            this.LUE_FlyVel.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LUE_FlyVel.Properties.Appearance.Font")));
            this.LUE_FlyVel.Properties.Appearance.Options.UseFont = true;
            this.LUE_FlyVel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("LUE_FlyVel.Properties.Buttons"))))});
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // TE_FlyVelPercent
            // 
            resources.ApplyResources(this.TE_FlyVelPercent, "TE_FlyVelPercent");
            this.TE_FlyVelPercent.Name = "TE_FlyVelPercent";
            this.TE_FlyVelPercent.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_FlyVelPercent.Properties.Appearance.Font")));
            this.TE_FlyVelPercent.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            this.labelControl3.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // TE_ReinspectCount
            // 
            resources.ApplyResources(this.TE_ReinspectCount, "TE_ReinspectCount");
            this.TE_ReinspectCount.Name = "TE_ReinspectCount";
            this.TE_ReinspectCount.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_ReinspectCount.Properties.Appearance.Font")));
            this.TE_ReinspectCount.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl4.Appearance.Font")));
            this.labelControl4.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Name = "labelControl4";
            // 
            // SE_FailAlarmCount
            // 
            resources.ApplyResources(this.SE_FailAlarmCount, "SE_FailAlarmCount");
            this.SE_FailAlarmCount.Name = "SE_FailAlarmCount";
            this.SE_FailAlarmCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("SE_FailAlarmCount.Properties.Buttons"))))});
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl5.Appearance.Font")));
            this.labelControl5.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl5, "labelControl5");
            this.labelControl5.Name = "labelControl5";
            // 
            // CE_IsFly
            // 
            resources.ApplyResources(this.CE_IsFly, "CE_IsFly");
            this.CE_IsFly.Name = "CE_IsFly";
            this.CE_IsFly.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_IsFly.Properties.Appearance.Font")));
            this.CE_IsFly.Properties.Appearance.Options.UseFont = true;
            this.CE_IsFly.Properties.Caption = resources.GetString("CE_IsFly.Properties.Caption");
            // 
            // SE_RejectAlarmCount
            // 
            resources.ApplyResources(this.SE_RejectAlarmCount, "SE_RejectAlarmCount");
            this.SE_RejectAlarmCount.Name = "SE_RejectAlarmCount";
            this.SE_RejectAlarmCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("SE_RejectAlarmCount.Properties.Buttons"))))});
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl7.Appearance.Font")));
            this.labelControl7.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl7, "labelControl7");
            this.labelControl7.Name = "labelControl7";
            // 
            // CE_LaserMeasure
            // 
            resources.ApplyResources(this.CE_LaserMeasure, "CE_LaserMeasure");
            this.CE_LaserMeasure.Name = "CE_LaserMeasure";
            this.CE_LaserMeasure.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_LaserMeasure.Properties.Appearance.Font")));
            this.CE_LaserMeasure.Properties.Appearance.Options.UseFont = true;
            this.CE_LaserMeasure.Properties.Caption = resources.GetString("CE_LaserMeasure.Properties.Caption");
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.CE_LaserMeasure);
            this.groupControl1.Controls.Add(this.CE_Barcode);
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // InspectProductSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.SE_RejectAlarmCount);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.CE_IsFly);
            this.Controls.Add(this.SE_FailAlarmCount);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.TE_ReinspectCount);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.TE_FlyVelPercent);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.LUE_FlyVel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LUE_Vel);
            this.Controls.Add(this.CE_Pick);
            this.Controls.Add(this.CE_Reinspect);
            this.Controls.Add(this.CE_InspectByPass);
            this.Name = "InspectProductSetting";
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_Barcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_InspectByPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_Reinspect.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_Pick.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Vel.Properties)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pick_dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_FlyVel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_FlyVelPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ReinspectCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_FailAlarmCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_IsFly.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_RejectAlarmCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_LaserMeasure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.CheckEdit CE_Pick;
        private DevExpress.XtraEditors.CheckEdit CE_Reinspect;
        private DevExpress.XtraEditors.CheckEdit CE_InspectByPass;
        private DevExpress.XtraEditors.CheckEdit CE_Barcode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit LUE_Vel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView Pick_dataGrid;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit TE_FlyVelPercent;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit LUE_FlyVel;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit TE_ReinspectCount;
        private DevExpress.XtraEditors.SpinEdit SE_FailAlarmCount;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit CE_IsFly;
        private DevExpress.XtraEditors.SpinEdit SE_RejectAlarmCount;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CheckEdit CE_LaserMeasure;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}
