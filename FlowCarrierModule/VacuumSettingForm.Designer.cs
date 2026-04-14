namespace TA2000Modules
{
    partial class VacuumSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VacuumSettingForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lab_VCValueEstablished = new DevExpress.XtraEditors.LabelControl();
            this.lab_VCValueCurrent = new DevExpress.XtraEditors.LabelControl();
            this.LB_VCEstablished = new DevExpress.XtraEditors.LabelControl();
            this.Btn_VCTrigger = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ValueSave = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SVTrigger = new DevExpress.XtraEditors.SimpleButton();
            this.LB_VCState = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.TE_VCEstablishedValue = new DevExpress.XtraEditors.TextEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_VCEstablishedValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.lab_VCValueEstablished);
            this.groupBox1.Controls.Add(this.lab_VCValueCurrent);
            this.groupBox1.Controls.Add(this.LB_VCEstablished);
            this.groupBox1.Controls.Add(this.Btn_VCTrigger);
            this.groupBox1.Controls.Add(this.Btn_ValueSave);
            this.groupBox1.Controls.Add(this.Btn_SVTrigger);
            this.groupBox1.Controls.Add(this.LB_VCState);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.TE_VCEstablishedValue);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Name = "labelControl1";
            // 
            // lab_VCValueEstablished
            // 
            resources.ApplyResources(this.lab_VCValueEstablished, "lab_VCValueEstablished");
            this.lab_VCValueEstablished.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lab_VCValueEstablished.Appearance.Font")));
            this.lab_VCValueEstablished.Appearance.Options.UseFont = true;
            this.lab_VCValueEstablished.Name = "lab_VCValueEstablished";
            // 
            // lab_VCValueCurrent
            // 
            resources.ApplyResources(this.lab_VCValueCurrent, "lab_VCValueCurrent");
            this.lab_VCValueCurrent.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lab_VCValueCurrent.Appearance.Font")));
            this.lab_VCValueCurrent.Appearance.Options.UseFont = true;
            this.lab_VCValueCurrent.Name = "lab_VCValueCurrent";
            // 
            // LB_VCEstablished
            // 
            resources.ApplyResources(this.LB_VCEstablished, "LB_VCEstablished");
            this.LB_VCEstablished.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_VCEstablished.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LB_VCEstablished.Appearance.Font")));
            this.LB_VCEstablished.Appearance.ForeColor = System.Drawing.Color.White;
            this.LB_VCEstablished.Appearance.Options.UseBackColor = true;
            this.LB_VCEstablished.Appearance.Options.UseFont = true;
            this.LB_VCEstablished.Appearance.Options.UseForeColor = true;
            this.LB_VCEstablished.Name = "LB_VCEstablished";
            // 
            // Btn_VCTrigger
            // 
            resources.ApplyResources(this.Btn_VCTrigger, "Btn_VCTrigger");
            this.Btn_VCTrigger.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_VCTrigger.Appearance.Font")));
            this.Btn_VCTrigger.Appearance.Options.UseBackColor = true;
            this.Btn_VCTrigger.Appearance.Options.UseFont = true;
            this.Btn_VCTrigger.Name = "Btn_VCTrigger";
            // 
            // Btn_ValueSave
            // 
            resources.ApplyResources(this.Btn_ValueSave, "Btn_ValueSave");
            this.Btn_ValueSave.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_ValueSave.Appearance.Font")));
            this.Btn_ValueSave.Appearance.Options.UseFont = true;
            this.Btn_ValueSave.Name = "Btn_ValueSave";
            // 
            // Btn_SVTrigger
            // 
            resources.ApplyResources(this.Btn_SVTrigger, "Btn_SVTrigger");
            this.Btn_SVTrigger.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_SVTrigger.Appearance.Font")));
            this.Btn_SVTrigger.Appearance.Options.UseFont = true;
            this.Btn_SVTrigger.Name = "Btn_SVTrigger";
            // 
            // LB_VCState
            // 
            resources.ApplyResources(this.LB_VCState, "LB_VCState");
            this.LB_VCState.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_VCState.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("LB_VCState.Appearance.Font")));
            this.LB_VCState.Appearance.ForeColor = System.Drawing.Color.White;
            this.LB_VCState.Appearance.Options.UseBackColor = true;
            this.LB_VCState.Appearance.Options.UseFont = true;
            this.LB_VCState.Appearance.Options.UseForeColor = true;
            this.LB_VCState.Name = "LB_VCState";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Name = "labelControl3";
            // 
            // TE_VCEstablishedValue
            // 
            resources.ApplyResources(this.TE_VCEstablishedValue, "TE_VCEstablishedValue");
            this.TE_VCEstablishedValue.Name = "TE_VCEstablishedValue";
            this.TE_VCEstablishedValue.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_VCEstablishedValue.Properties.Appearance.Font")));
            this.TE_VCEstablishedValue.Properties.Appearance.Options.UseFont = true;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // VacuumSettingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "VacuumSettingForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_VCEstablishedValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl LB_VCEstablished;
        private DevExpress.XtraEditors.SimpleButton Btn_VCTrigger;
        private DevExpress.XtraEditors.SimpleButton Btn_ValueSave;
        private DevExpress.XtraEditors.SimpleButton Btn_SVTrigger;
        private DevExpress.XtraEditors.LabelControl LB_VCState;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit TE_VCEstablishedValue;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.LabelControl lab_VCValueCurrent;
        private DevExpress.XtraEditors.LabelControl lab_VCValueEstablished;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
