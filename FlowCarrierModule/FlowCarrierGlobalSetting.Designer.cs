namespace TA2000Modules
{
    partial class FlowCarrierGlobalSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowCarrierGlobalSetting));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RG_LowerCom = new DevExpress.XtraEditors.RadioGroup();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TE_ClientPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.TE_ClientIP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.TE_ServerPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RG_UpperCom = new DevExpress.XtraEditors.RadioGroup();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.CE_UseInterLock = new DevExpress.XtraEditors.CheckEdit();
            this.CE_TCPIPuseXML = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CE_EnableSendOtherDataClient = new DevExpress.XtraEditors.CheckEdit();
            this.CE_EnableSendOtherDataServer = new DevExpress.XtraEditors.CheckEdit();
            this.TE_OnlyDataClientPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.TE_OnlyDataServerPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RG_LowerCom.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ClientPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ClientIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ServerPort.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RG_UpperCom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_UseInterLock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_TCPIPuseXML.Properties)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CE_EnableSendOtherDataClient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_EnableSendOtherDataServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OnlyDataClientPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OnlyDataServerPort.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.RG_LowerCom);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // RG_LowerCom
            // 
            resources.ApplyResources(this.RG_LowerCom, "RG_LowerCom");
            this.RG_LowerCom.Name = "RG_LowerCom";
            this.RG_LowerCom.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("RG_LowerCom.Properties.Appearance.Font")));
            this.RG_LowerCom.Properties.Appearance.Options.UseFont = true;
            this.RG_LowerCom.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("RG_LowerCom.Properties.Items"))), resources.GetString("RG_LowerCom.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("RG_LowerCom.Properties.Items2"))), resources.GetString("RG_LowerCom.Properties.Items3")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("RG_LowerCom.Properties.Items4"))), resources.GetString("RG_LowerCom.Properties.Items5"))});
            this.RG_LowerCom.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.TE_ClientPort);
            this.groupBox2.Controls.Add(this.labelControl14);
            this.groupBox2.Controls.Add(this.TE_ClientIP);
            this.groupBox2.Controls.Add(this.labelControl13);
            this.groupBox2.Controls.Add(this.TE_ServerPort);
            this.groupBox2.Controls.Add(this.labelControl12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // TE_ClientPort
            // 
            resources.ApplyResources(this.TE_ClientPort, "TE_ClientPort");
            this.TE_ClientPort.Name = "TE_ClientPort";
            this.TE_ClientPort.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_ClientPort.Properties.Appearance.Font")));
            this.TE_ClientPort.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl14
            // 
            resources.ApplyResources(this.labelControl14, "labelControl14");
            this.labelControl14.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl14.Appearance.Font")));
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Name = "labelControl14";
            // 
            // TE_ClientIP
            // 
            resources.ApplyResources(this.TE_ClientIP, "TE_ClientIP");
            this.TE_ClientIP.Name = "TE_ClientIP";
            this.TE_ClientIP.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_ClientIP.Properties.Appearance.Font")));
            this.TE_ClientIP.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl13
            // 
            resources.ApplyResources(this.labelControl13, "labelControl13");
            this.labelControl13.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl13.Appearance.Font")));
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Name = "labelControl13";
            // 
            // TE_ServerPort
            // 
            resources.ApplyResources(this.TE_ServerPort, "TE_ServerPort");
            this.TE_ServerPort.Name = "TE_ServerPort";
            this.TE_ServerPort.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_ServerPort.Properties.Appearance.Font")));
            this.TE_ServerPort.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl12
            // 
            resources.ApplyResources(this.labelControl12, "labelControl12");
            this.labelControl12.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl12.Appearance.Font")));
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Name = "labelControl12";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.RG_UpperCom);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // RG_UpperCom
            // 
            resources.ApplyResources(this.RG_UpperCom, "RG_UpperCom");
            this.RG_UpperCom.Name = "RG_UpperCom";
            this.RG_UpperCom.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("RG_UpperCom.Properties.Appearance.Font")));
            this.RG_UpperCom.Properties.Appearance.Options.UseFont = true;
            this.RG_UpperCom.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("RG_UpperCom.Properties.Items"))), resources.GetString("RG_UpperCom.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("RG_UpperCom.Properties.Items2"))), resources.GetString("RG_UpperCom.Properties.Items3")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("RG_UpperCom.Properties.Items4"))), resources.GetString("RG_UpperCom.Properties.Items5"))});
            this.RG_UpperCom.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // CE_UseInterLock
            // 
            resources.ApplyResources(this.CE_UseInterLock, "CE_UseInterLock");
            this.CE_UseInterLock.Name = "CE_UseInterLock";
            this.CE_UseInterLock.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_UseInterLock.Properties.Appearance.Font")));
            this.CE_UseInterLock.Properties.Appearance.Options.UseFont = true;
            this.CE_UseInterLock.Properties.Caption = resources.GetString("CE_UseInterLock.Properties.Caption");
            this.CE_UseInterLock.Properties.DisplayValueChecked = resources.GetString("CE_UseInterLock.Properties.DisplayValueChecked");
            this.CE_UseInterLock.Properties.DisplayValueGrayed = resources.GetString("CE_UseInterLock.Properties.DisplayValueGrayed");
            this.CE_UseInterLock.Properties.DisplayValueUnchecked = resources.GetString("CE_UseInterLock.Properties.DisplayValueUnchecked");
            this.CE_UseInterLock.Properties.GlyphVerticalAlignment = ((DevExpress.Utils.VertAlignment)(resources.GetObject("CE_UseInterLock.Properties.GlyphVerticalAlignment")));
            // 
            // CE_TCPIPuseXML
            // 
            resources.ApplyResources(this.CE_TCPIPuseXML, "CE_TCPIPuseXML");
            this.CE_TCPIPuseXML.Name = "CE_TCPIPuseXML";
            this.CE_TCPIPuseXML.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_TCPIPuseXML.Properties.Appearance.Font")));
            this.CE_TCPIPuseXML.Properties.Appearance.Options.UseFont = true;
            this.CE_TCPIPuseXML.Properties.Caption = resources.GetString("CE_TCPIPuseXML.Properties.Caption");
            this.CE_TCPIPuseXML.Properties.DisplayValueChecked = resources.GetString("CE_TCPIPuseXML.Properties.DisplayValueChecked");
            this.CE_TCPIPuseXML.Properties.DisplayValueGrayed = resources.GetString("CE_TCPIPuseXML.Properties.DisplayValueGrayed");
            this.CE_TCPIPuseXML.Properties.DisplayValueUnchecked = resources.GetString("CE_TCPIPuseXML.Properties.DisplayValueUnchecked");
            this.CE_TCPIPuseXML.Properties.GlyphVerticalAlignment = ((DevExpress.Utils.VertAlignment)(resources.GetObject("CE_TCPIPuseXML.Properties.GlyphVerticalAlignment")));
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.CE_EnableSendOtherDataClient);
            this.groupBox4.Controls.Add(this.CE_EnableSendOtherDataServer);
            this.groupBox4.Controls.Add(this.TE_OnlyDataClientPort);
            this.groupBox4.Controls.Add(this.labelControl1);
            this.groupBox4.Controls.Add(this.TE_OnlyDataServerPort);
            this.groupBox4.Controls.Add(this.labelControl3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // CE_EnableSendOtherDataClient
            // 
            resources.ApplyResources(this.CE_EnableSendOtherDataClient, "CE_EnableSendOtherDataClient");
            this.CE_EnableSendOtherDataClient.Name = "CE_EnableSendOtherDataClient";
            this.CE_EnableSendOtherDataClient.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_EnableSendOtherDataClient.Properties.Appearance.Font")));
            this.CE_EnableSendOtherDataClient.Properties.Appearance.Options.UseFont = true;
            this.CE_EnableSendOtherDataClient.Properties.Caption = resources.GetString("CE_EnableSendOtherDataClient.Properties.Caption");
            this.CE_EnableSendOtherDataClient.Properties.DisplayValueChecked = resources.GetString("CE_EnableSendOtherDataClient.Properties.DisplayValueChecked");
            this.CE_EnableSendOtherDataClient.Properties.DisplayValueGrayed = resources.GetString("CE_EnableSendOtherDataClient.Properties.DisplayValueGrayed");
            this.CE_EnableSendOtherDataClient.Properties.DisplayValueUnchecked = resources.GetString("CE_EnableSendOtherDataClient.Properties.DisplayValueUnchecked");
            this.CE_EnableSendOtherDataClient.Properties.GlyphVerticalAlignment = ((DevExpress.Utils.VertAlignment)(resources.GetObject("CE_EnableSendOtherDataClient.Properties.GlyphVerticalAlignment")));
            // 
            // CE_EnableSendOtherDataServer
            // 
            resources.ApplyResources(this.CE_EnableSendOtherDataServer, "CE_EnableSendOtherDataServer");
            this.CE_EnableSendOtherDataServer.Name = "CE_EnableSendOtherDataServer";
            this.CE_EnableSendOtherDataServer.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("CE_EnableSendOtherDataServer.Properties.Appearance.Font")));
            this.CE_EnableSendOtherDataServer.Properties.Appearance.Options.UseFont = true;
            this.CE_EnableSendOtherDataServer.Properties.Caption = resources.GetString("CE_EnableSendOtherDataServer.Properties.Caption");
            this.CE_EnableSendOtherDataServer.Properties.DisplayValueChecked = resources.GetString("CE_EnableSendOtherDataServer.Properties.DisplayValueChecked");
            this.CE_EnableSendOtherDataServer.Properties.DisplayValueGrayed = resources.GetString("CE_EnableSendOtherDataServer.Properties.DisplayValueGrayed");
            this.CE_EnableSendOtherDataServer.Properties.DisplayValueUnchecked = resources.GetString("CE_EnableSendOtherDataServer.Properties.DisplayValueUnchecked");
            this.CE_EnableSendOtherDataServer.Properties.GlyphVerticalAlignment = ((DevExpress.Utils.VertAlignment)(resources.GetObject("CE_EnableSendOtherDataServer.Properties.GlyphVerticalAlignment")));
            // 
            // TE_OnlyDataClientPort
            // 
            resources.ApplyResources(this.TE_OnlyDataClientPort, "TE_OnlyDataClientPort");
            this.TE_OnlyDataClientPort.Name = "TE_OnlyDataClientPort";
            this.TE_OnlyDataClientPort.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_OnlyDataClientPort.Properties.Appearance.Font")));
            this.TE_OnlyDataClientPort.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Name = "labelControl1";
            // 
            // TE_OnlyDataServerPort
            // 
            resources.ApplyResources(this.TE_OnlyDataServerPort, "TE_OnlyDataServerPort");
            this.TE_OnlyDataServerPort.Name = "TE_OnlyDataServerPort";
            this.TE_OnlyDataServerPort.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("TE_OnlyDataServerPort.Properties.Appearance.Font")));
            this.TE_OnlyDataServerPort.Properties.Appearance.Options.UseFont = true;
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Name = "labelControl3";
            // 
            // FlowCarrierGlobalSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.CE_UseInterLock);
            this.Controls.Add(this.CE_TCPIPuseXML);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FlowCarrierGlobalSetting";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RG_LowerCom.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ClientPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ClientIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ServerPort.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RG_UpperCom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_UseInterLock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_TCPIPuseXML.Properties)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CE_EnableSendOtherDataClient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CE_EnableSendOtherDataServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OnlyDataClientPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OnlyDataServerPort.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.RadioGroup RG_LowerCom;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit TE_ClientPort;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.TextEdit TE_ClientIP;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit TE_ServerPort;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.RadioGroup RG_UpperCom;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.CheckEdit CE_UseInterLock;
        private DevExpress.XtraEditors.CheckEdit CE_TCPIPuseXML;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.CheckEdit CE_EnableSendOtherDataClient;
        private DevExpress.XtraEditors.CheckEdit CE_EnableSendOtherDataServer;
        private DevExpress.XtraEditors.TextEdit TE_OnlyDataClientPort;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit TE_OnlyDataServerPort;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
