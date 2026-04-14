namespace TA2000Modules
{
    partial class ProcessMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessMode));
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.labelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_ProcessMode = new DevExpress.XtraEditors.LabelControl();
            this.lbl_MachineState = new DevExpress.XtraEditors.LabelControl();
            this.Btn_DoorLock = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ChangeMode = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // labelControl
            // 
            resources.ApplyResources(this.labelControl, "labelControl");
            this.labelControl.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl.Appearance.Font")));
            this.labelControl.Appearance.Options.UseFont = true;
            this.labelControl.Name = "labelControl";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Name = "labelControl2";
            // 
            // lbl_ProcessMode
            // 
            resources.ApplyResources(this.lbl_ProcessMode, "lbl_ProcessMode");
            this.lbl_ProcessMode.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lbl_ProcessMode.Appearance.Font")));
            this.lbl_ProcessMode.Appearance.Options.UseFont = true;
            this.lbl_ProcessMode.Name = "lbl_ProcessMode";
            // 
            // lbl_MachineState
            // 
            resources.ApplyResources(this.lbl_MachineState, "lbl_MachineState");
            this.lbl_MachineState.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lbl_MachineState.Appearance.Font")));
            this.lbl_MachineState.Appearance.Options.UseFont = true;
            this.lbl_MachineState.Name = "lbl_MachineState";
            // 
            // Btn_DoorLock
            // 
            resources.ApplyResources(this.Btn_DoorLock, "Btn_DoorLock");
            this.Btn_DoorLock.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_DoorLock.Appearance.Font")));
            this.Btn_DoorLock.Appearance.Options.UseFont = true;
            this.Btn_DoorLock.Name = "Btn_DoorLock";
            // 
            // Btn_ChangeMode
            // 
            resources.ApplyResources(this.Btn_ChangeMode, "Btn_ChangeMode");
            this.Btn_ChangeMode.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("Btn_ChangeMode.Appearance.Font")));
            this.Btn_ChangeMode.Appearance.Options.UseFont = true;
            this.Btn_ChangeMode.Name = "Btn_ChangeMode";
            // 
            // ProcessMode
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_ChangeMode);
            this.Controls.Add(this.Btn_DoorLock);
            this.Controls.Add(this.lbl_MachineState);
            this.Controls.Add(this.lbl_ProcessMode);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl);
            this.Name = "ProcessMode";
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.LabelControl labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton Btn_ChangeMode;
        private DevExpress.XtraEditors.SimpleButton Btn_DoorLock;
        private DevExpress.XtraEditors.LabelControl lbl_MachineState;
        private DevExpress.XtraEditors.LabelControl lbl_ProcessMode;
    }
}
