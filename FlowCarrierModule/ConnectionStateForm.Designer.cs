namespace TA2000Modules
{
    partial class ConnectionStateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionStateForm));
            this.LB_Upper = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LB_LowerY = new DevExpress.XtraEditors.LabelControl();
            this.LB_LowerX = new DevExpress.XtraEditors.LabelControl();
            this.LB_UpperY = new DevExpress.XtraEditors.LabelControl();
            this.LB_UpperX = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbServerNumber = new System.Windows.Forms.Label();
            this.LB_ClientState = new DevExpress.XtraEditors.LabelControl();
            this.LB_ServerState = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_Upper
            // 
            resources.ApplyResources(this.LB_Upper, "LB_Upper");
            this.LB_Upper.Name = "LB_Upper";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // labelControl4
            // 
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Name = "labelControl4";
            // 
            // labelControl5
            // 
            resources.ApplyResources(this.labelControl5, "labelControl5");
            this.labelControl5.Name = "labelControl5";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LB_LowerY);
            this.groupBox1.Controls.Add(this.LB_LowerX);
            this.groupBox1.Controls.Add(this.LB_UpperY);
            this.groupBox1.Controls.Add(this.LB_UpperX);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.LB_Upper);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.labelControl3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // LB_LowerY
            // 
            this.LB_LowerY.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_LowerY.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.LB_LowerY, "LB_LowerY");
            this.LB_LowerY.Name = "LB_LowerY";
            // 
            // LB_LowerX
            // 
            this.LB_LowerX.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_LowerX.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.LB_LowerX, "LB_LowerX");
            this.LB_LowerX.Name = "LB_LowerX";
            // 
            // LB_UpperY
            // 
            this.LB_UpperY.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_UpperY.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.LB_UpperY, "LB_UpperY");
            this.LB_UpperY.Name = "LB_UpperY";
            // 
            // LB_UpperX
            // 
            this.LB_UpperX.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_UpperX.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.LB_UpperX, "LB_UpperX");
            this.LB_UpperX.Name = "LB_UpperX";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbServerNumber);
            this.groupBox2.Controls.Add(this.LB_ClientState);
            this.groupBox2.Controls.Add(this.LB_ServerState);
            this.groupBox2.Controls.Add(this.labelControl4);
            this.groupBox2.Controls.Add(this.labelControl5);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lbServerNumber
            // 
            resources.ApplyResources(this.lbServerNumber, "lbServerNumber");
            this.lbServerNumber.Name = "lbServerNumber";
            // 
            // LB_ClientState
            // 
            this.LB_ClientState.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_ClientState.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.LB_ClientState, "LB_ClientState");
            this.LB_ClientState.Name = "LB_ClientState";
            // 
            // LB_ServerState
            // 
            this.LB_ServerState.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_ServerState.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.LB_ServerState, "LB_ServerState");
            this.LB_ServerState.Name = "LB_ServerState";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // ConnectionStateForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConnectionStateForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LB_Upper;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl LB_LowerY;
        private DevExpress.XtraEditors.LabelControl LB_LowerX;
        private DevExpress.XtraEditors.LabelControl LB_UpperY;
        private DevExpress.XtraEditors.LabelControl LB_UpperX;
        private DevExpress.XtraEditors.LabelControl LB_ClientState;
        private DevExpress.XtraEditors.LabelControl LB_ServerState;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.Label lbServerNumber;
    }
}
