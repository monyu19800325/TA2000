namespace TA2000Modules
{
    partial class TeachTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeachTestForm));
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.Btn_BindFailCode = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_StopInspect = new DevExpress.XtraEditors.SimpleButton();
            this.CB_Burning = new System.Windows.Forms.CheckBox();
            this.TE_InspectTimes = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.Btn_FinishTeach = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Inspect = new DevExpress.XtraEditors.SimpleButton();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_InspectTimes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            resources.ApplyResources(this.groupControl3, "groupControl3");
            this.groupControl3.Controls.Add(this.Btn_BindFailCode);
            this.groupControl3.Controls.Add(this.Btn_StopInspect);
            this.groupControl3.Controls.Add(this.CB_Burning);
            this.groupControl3.Controls.Add(this.TE_InspectTimes);
            this.groupControl3.Controls.Add(this.labelControl15);
            this.groupControl3.Controls.Add(this.Btn_FinishTeach);
            this.groupControl3.Controls.Add(this.Btn_Inspect);
            this.groupControl3.Name = "groupControl3";
            // 
            // Btn_BindFailCode
            // 
            resources.ApplyResources(this.Btn_BindFailCode, "Btn_BindFailCode");
            this.Btn_BindFailCode.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_BindFailCode.Appearance.Font")));
            this.Btn_BindFailCode.Appearance.Options.UseFont = true;
            this.Btn_BindFailCode.Name = "Btn_BindFailCode";
            // 
            // Btn_StopInspect
            // 
            resources.ApplyResources(this.Btn_StopInspect, "Btn_StopInspect");
            this.Btn_StopInspect.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_StopInspect.Appearance.Font")));
            this.Btn_StopInspect.Appearance.Options.UseFont = true;
            this.Btn_StopInspect.Name = "Btn_StopInspect";
            // 
            // CB_Burning
            // 
            resources.ApplyResources(this.CB_Burning, "CB_Burning");
            this.CB_Burning.Name = "CB_Burning";
            this.CB_Burning.UseVisualStyleBackColor = true;
            // 
            // TE_InspectTimes
            // 
            resources.ApplyResources(this.TE_InspectTimes, "TE_InspectTimes");
            this.TE_InspectTimes.Name = "TE_InspectTimes";
            this.TE_InspectTimes.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_InspectTimes.Properties.Appearance.Font")));
            this.TE_InspectTimes.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl15
            // 
            resources.ApplyResources(this.labelControl15, "labelControl15");
            this.labelControl15.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl15.Appearance.Font")));
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Name = "labelControl15";
            // 
            // Btn_FinishTeach
            // 
            resources.ApplyResources(this.Btn_FinishTeach, "Btn_FinishTeach");
            this.Btn_FinishTeach.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_FinishTeach.Appearance.Font")));
            this.Btn_FinishTeach.Appearance.Options.UseFont = true;
            this.Btn_FinishTeach.Name = "Btn_FinishTeach";
            // 
            // Btn_Inspect
            // 
            resources.ApplyResources(this.Btn_Inspect, "Btn_Inspect");
            this.Btn_Inspect.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_Inspect.Appearance.Font")));
            this.Btn_Inspect.Appearance.Options.UseFont = true;
            this.Btn_Inspect.Name = "Btn_Inspect";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // TeachTestForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl3);
            this.Name = "TeachTestForm";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_InspectTimes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton Btn_StopInspect;
        private System.Windows.Forms.CheckBox CB_Burning;
        private DevExpress.XtraEditors.TextEdit TE_InspectTimes;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.SimpleButton Btn_FinishTeach;
        private DevExpress.XtraEditors.SimpleButton Btn_Inspect;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.SimpleButton Btn_BindFailCode;
    }
}