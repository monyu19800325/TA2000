namespace TA2000Modules
{
    partial class TCPIPComForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCPIPComForm));
            this.TE_IP = new DevExpress.XtraEditors.TextEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.TE_Port = new DevExpress.XtraEditors.TextEdit();
            this.Btn_Connect = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.TE_ServerPort = new DevExpress.XtraEditors.TextEdit();
            this.Btn_ServerOnline = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_ReplyDeliveryOK = new DevExpress.XtraEditors.SimpleButton();
            this.lbServerNumber = new System.Windows.Forms.Label();
            this.Btn_Offline = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ServerReplyGotBoat = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ServerSendStop = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ServerSendRun = new DevExpress.XtraEditors.SimpleButton();
            this.LB_ServerLight = new DevExpress.XtraEditors.LabelControl();
            this.LB_ClientLight = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_ReadNewDeliveryBoat = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_DisConnect = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ClientDeliveryBoat = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ClientSendStop = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ClientSendRun = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.TE_IP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Port.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ServerPort.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TE_IP
            // 
            resources.ApplyResources(this.TE_IP, "TE_IP");
            this.TE_IP.Name = "TE_IP";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
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
            // TE_Port
            // 
            resources.ApplyResources(this.TE_Port, "TE_Port");
            this.TE_Port.Name = "TE_Port";
            // 
            // Btn_Connect
            // 
            resources.ApplyResources(this.Btn_Connect, "Btn_Connect");
            this.Btn_Connect.Name = "Btn_Connect";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // TE_ServerPort
            // 
            resources.ApplyResources(this.TE_ServerPort, "TE_ServerPort");
            this.TE_ServerPort.Name = "TE_ServerPort";
            // 
            // Btn_ServerOnline
            // 
            resources.ApplyResources(this.Btn_ServerOnline, "Btn_ServerOnline");
            this.Btn_ServerOnline.Name = "Btn_ServerOnline";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.Btn_ReplyDeliveryOK);
            this.groupBox1.Controls.Add(this.lbServerNumber);
            this.groupBox1.Controls.Add(this.Btn_Offline);
            this.groupBox1.Controls.Add(this.Btn_ServerReplyGotBoat);
            this.groupBox1.Controls.Add(this.Btn_ServerSendStop);
            this.groupBox1.Controls.Add(this.Btn_ServerSendRun);
            this.groupBox1.Controls.Add(this.LB_ServerLight);
            this.groupBox1.Controls.Add(this.Btn_ServerOnline);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.TE_ServerPort);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // Btn_ReplyDeliveryOK
            // 
            resources.ApplyResources(this.Btn_ReplyDeliveryOK, "Btn_ReplyDeliveryOK");
            this.Btn_ReplyDeliveryOK.Name = "Btn_ReplyDeliveryOK";
            // 
            // lbServerNumber
            // 
            resources.ApplyResources(this.lbServerNumber, "lbServerNumber");
            this.lbServerNumber.Name = "lbServerNumber";
            // 
            // Btn_Offline
            // 
            resources.ApplyResources(this.Btn_Offline, "Btn_Offline");
            this.Btn_Offline.Name = "Btn_Offline";
            // 
            // Btn_ServerReplyGotBoat
            // 
            resources.ApplyResources(this.Btn_ServerReplyGotBoat, "Btn_ServerReplyGotBoat");
            this.Btn_ServerReplyGotBoat.Name = "Btn_ServerReplyGotBoat";
            // 
            // Btn_ServerSendStop
            // 
            resources.ApplyResources(this.Btn_ServerSendStop, "Btn_ServerSendStop");
            this.Btn_ServerSendStop.Name = "Btn_ServerSendStop";
            // 
            // Btn_ServerSendRun
            // 
            resources.ApplyResources(this.Btn_ServerSendRun, "Btn_ServerSendRun");
            this.Btn_ServerSendRun.Name = "Btn_ServerSendRun";
            // 
            // LB_ServerLight
            // 
            resources.ApplyResources(this.LB_ServerLight, "LB_ServerLight");
            this.LB_ServerLight.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_ServerLight.Appearance.Options.UseBackColor = true;
            this.LB_ServerLight.Name = "LB_ServerLight";
            // 
            // LB_ClientLight
            // 
            resources.ApplyResources(this.LB_ClientLight, "LB_ClientLight");
            this.LB_ClientLight.Appearance.BackColor = System.Drawing.Color.Red;
            this.LB_ClientLight.Appearance.Options.UseBackColor = true;
            this.LB_ClientLight.Name = "LB_ClientLight";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.Btn_ReadNewDeliveryBoat);
            this.groupBox2.Controls.Add(this.Btn_DisConnect);
            this.groupBox2.Controls.Add(this.Btn_ClientDeliveryBoat);
            this.groupBox2.Controls.Add(this.Btn_ClientSendStop);
            this.groupBox2.Controls.Add(this.LB_ClientLight);
            this.groupBox2.Controls.Add(this.Btn_ClientSendRun);
            this.groupBox2.Controls.Add(this.Btn_Connect);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.TE_IP);
            this.groupBox2.Controls.Add(this.TE_Port);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // Btn_ReadNewDeliveryBoat
            // 
            resources.ApplyResources(this.Btn_ReadNewDeliveryBoat, "Btn_ReadNewDeliveryBoat");
            this.Btn_ReadNewDeliveryBoat.Name = "Btn_ReadNewDeliveryBoat";
            // 
            // Btn_DisConnect
            // 
            resources.ApplyResources(this.Btn_DisConnect, "Btn_DisConnect");
            this.Btn_DisConnect.Name = "Btn_DisConnect";
            // 
            // Btn_ClientDeliveryBoat
            // 
            resources.ApplyResources(this.Btn_ClientDeliveryBoat, "Btn_ClientDeliveryBoat");
            this.Btn_ClientDeliveryBoat.Name = "Btn_ClientDeliveryBoat";
            // 
            // Btn_ClientSendStop
            // 
            resources.ApplyResources(this.Btn_ClientSendStop, "Btn_ClientSendStop");
            this.Btn_ClientSendStop.Name = "Btn_ClientSendStop";
            // 
            // Btn_ClientSendRun
            // 
            resources.ApplyResources(this.Btn_ClientSendRun, "Btn_ClientSendRun");
            this.Btn_ClientSendRun.Name = "Btn_ClientSendRun";
            // 
            // TCPIPComForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TCPIPComForm";
            ((System.ComponentModel.ISupportInitialize)(this.TE_IP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Port.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ServerPort.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit TE_IP;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit TE_Port;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton Btn_Connect;
        private DevExpress.XtraEditors.SimpleButton Btn_ServerOnline;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit TE_ServerPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl LB_ServerLight;
        private DevExpress.XtraEditors.LabelControl LB_ClientLight;
        private DevExpress.XtraEditors.SimpleButton Btn_ClientDeliveryBoat;
        private DevExpress.XtraEditors.SimpleButton Btn_ClientSendStop;
        private DevExpress.XtraEditors.SimpleButton Btn_ClientSendRun;
        private DevExpress.XtraEditors.SimpleButton Btn_ServerReplyGotBoat;
        private DevExpress.XtraEditors.SimpleButton Btn_ServerSendStop;
        private DevExpress.XtraEditors.SimpleButton Btn_ServerSendRun;
        private DevExpress.XtraEditors.SimpleButton Btn_DisConnect;
        private DevExpress.XtraEditors.SimpleButton Btn_Offline;
        private DevExpress.XtraEditors.SimpleButton Btn_ReplyDeliveryOK;
        private System.Windows.Forms.Label lbServerNumber;
        private DevExpress.XtraEditors.SimpleButton Btn_ReadNewDeliveryBoat;
    }
}