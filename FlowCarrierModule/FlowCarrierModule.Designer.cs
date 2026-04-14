namespace TA2000Modules
{
    partial class FlowCarrierModule
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.controlFlow1 = new ControlFlow.ControlFlow();
            this.流道傳送軸_Guide動作_STP = new ControlFlow.Controls.ProcessItem();
            this.入料_流道傳送軸_真空開啟_STP = new ControlFlow.Controls.ProcessItem();
            this.入料_流道傳送軸_真空開啟逾時_Err = new ControlFlow.Controls.ProcessItem();
            this.入料_流道傳送軸_頂升汽缸開啟_STP = new ControlFlow.Controls.ProcessItem();
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_入料動作_OK_END = new ControlFlow.Controls.ProcessItem();
            this.等待_流道傳送軸_到位檢知逾時_Err = new ControlFlow.Controls.ProcessItem();
            this.等待_流道傳送軸_到位檢知_STP = new ControlFlow.Controls.ProcessItem();
            this.等待_流道傳送軸_減速檢知逾時_Err = new ControlFlow.Controls.ProcessItem();
            this.等待_流道傳送軸_減速檢知_STP = new ControlFlow.Controls.ProcessItem();
            this.等待_流道傳送軸_Load檢知逾時_Err = new ControlFlow.Controls.ProcessItem();
            this.等待_流道傳送軸_Load檢知_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_啟動_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_停止汽缸_Up_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_入料動作_Start = new ControlFlow.Controls.StartItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.controlFlow2 = new ControlFlow.ControlFlow();
            this.有料_Guide動作_STP = new ControlFlow.Controls.ProcessItem();
            this.有料_流道傳送軸_真空開啟_STP = new ControlFlow.Controls.ProcessItem();
            this.有料_流道傳送軸_頂升汽缸開啟_STP = new ControlFlow.Controls.ProcessItem();
            this.流道_含Boat_OK_END = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_到位檢知脫離_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_停止汽缸_上升_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_正轉靠位動作_STP = new ControlFlow.Controls.ProcessItem();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.流道傳送軸_反轉脫離動作_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_到位檢知_Check = new ControlFlow.Controls.BranchItem();
            this.流道傳送軸_停止汽缸下降_STP = new ControlFlow.Controls.ProcessItem();
            this.流道_含Boat_Start = new ControlFlow.Controls.StartItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.controlFlow3 = new ControlFlow.ControlFlow();
            this.有料_流道傳送軸_頂升汽缸關閉_STP = new ControlFlow.Controls.ProcessItem();
            this.發送_下位SMEMA_Flg = new ControlFlow.Controls.ProcessItem();
            this.等待_下位SMEMA_Flg = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸啟動出料_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_出料動作_Err = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_出料動作_OK = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_真空_關閉_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_定位汽缸_關閉_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_停止汽缸_下降_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_出料動作_Start = new ControlFlow.Controls.StartItem();
            this.FlowCarrier_Start = new ControlFlow.Controls.StartItem();
            this.流道狀態確認_STP = new ControlFlow.Controls.ProcessItem();
            this.流道判定有無Boat_Check = new ControlFlow.Controls.BranchItem();
            this.流道_Y軸移動_Load定位點_STP = new ControlFlow.Controls.ProcessItem();
            this.發送_上位Load_Ready_Flg = new ControlFlow.Controls.ProcessItem();
            this.取得_上位Load_OK_Flg = new ControlFlow.Controls.ProcessItem();
            this.取得_上位SMEMA_Flg = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_入料動作_SubLoop = new ControlFlow.Controls.ProcessItem();
            this.流道_Y軸移動_Detection定位點_STP = new ControlFlow.Controls.ProcessItem();
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg = new ControlFlow.Controls.ProcessItem();
            this.取得_流道_Y軸_Detection_OK_Flg = new ControlFlow.Controls.ProcessItem();
            this.流道_Y軸移動_Unload定位點_STP = new ControlFlow.Controls.ProcessItem();
            this.流道傳送軸_出料動作_SubLoop = new ControlFlow.Controls.ProcessItem();
            this.BoatUnload判定_Check = new ControlFlow.Controls.BranchItem();
            this.流道_有Boat_SubLoop = new ControlFlow.Controls.ProcessItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Golden模式不做事 = new ControlFlow.Controls.ProcessItem();
            this.是否為Golden模式 = new ControlFlow.Controls.BranchItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.controlFlow1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.controlFlow2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.controlFlow3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(713, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(466, 683);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.controlFlow1);
            this.tabPage1.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(458, 657);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "流道傳送軸_入料動作";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // controlFlow1
            // 
            this.controlFlow1.AutoScroll = true;
            this.controlFlow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow1.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow1.Controls.Add(this.流道傳送軸_Guide動作_STP);
            this.controlFlow1.Controls.Add(this.入料_流道傳送軸_真空開啟逾時_Err);
            this.controlFlow1.Controls.Add(this.入料_流道傳送軸_頂升汽缸開啟逾時_Err);
            this.controlFlow1.Controls.Add(this.入料_流道傳送軸_頂升汽缸開啟_STP);
            this.controlFlow1.Controls.Add(this.入料_流道傳送軸_真空開啟_STP);
            this.controlFlow1.Controls.Add(this.等待_流道傳送軸_到位檢知逾時_Err);
            this.controlFlow1.Controls.Add(this.等待_流道傳送軸_減速檢知逾時_Err);
            this.controlFlow1.Controls.Add(this.等待_流道傳送軸_Load檢知逾時_Err);
            this.controlFlow1.Controls.Add(this.流道傳送軸_入料動作_OK_END);
            this.controlFlow1.Controls.Add(this.等待_流道傳送軸_到位檢知_STP);
            this.controlFlow1.Controls.Add(this.等待_流道傳送軸_減速檢知_STP);
            this.controlFlow1.Controls.Add(this.流道傳送軸_啟動_STP);
            this.controlFlow1.Controls.Add(this.等待_流道傳送軸_Load檢知_STP);
            this.controlFlow1.Controls.Add(this.流道傳送軸_停止汽缸_Up_STP);
            this.controlFlow1.Controls.Add(this.流道傳送軸_入料動作_Start);
            this.controlFlow1.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow1.Location = new System.Drawing.Point(2, 2);
            this.controlFlow1.Name = "controlFlow1";
            this.controlFlow1.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow1.RefreshAllowed = false;
            this.controlFlow1.Size = new System.Drawing.Size(454, 653);
            this.controlFlow1.TabIndex = 0;
            // 
            // 流道傳送軸_Guide動作_STP
            // 
            this.流道傳送軸_Guide動作_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_Guide動作_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_Guide動作_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_Guide動作_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_Guide動作_STP.ErrorDescription = "";
            this.流道傳送軸_Guide動作_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_Guide動作_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_Guide動作_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_Guide動作_STP.Location = new System.Drawing.Point(33, 392);
            this.流道傳送軸_Guide動作_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_Guide動作_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_Guide動作_STP.Name = "流道傳送軸_Guide動作_STP";
            this.流道傳送軸_Guide動作_STP.NextProcess = this.入料_流道傳送軸_真空開啟_STP;
            this.流道傳送軸_Guide動作_STP.Size = new System.Drawing.Size(164, 41);
            this.流道傳送軸_Guide動作_STP.StopWhenError = true;
            this.流道傳送軸_Guide動作_STP.TabIndex = 35;
            this.流道傳送軸_Guide動作_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.流道傳送軸_Guide動作_STP_ProcessIn);
            // 
            // 入料_流道傳送軸_真空開啟_STP
            // 
            this.入料_流道傳送軸_真空開啟_STP.BackColor = System.Drawing.Color.Cyan;
            this.入料_流道傳送軸_真空開啟_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.入料_流道傳送軸_真空開啟_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.入料_流道傳送軸_真空開啟_STP.DoNotStopAtThisProcess = false;
            this.入料_流道傳送軸_真空開啟_STP.ErrorDescription = "";
            this.入料_流道傳送軸_真空開啟_STP.ExceptionWaitTime = 100;
            this.入料_流道傳送軸_真空開啟_STP.FailProcess = this.入料_流道傳送軸_真空開啟逾時_Err;
            this.入料_流道傳送軸_真空開啟_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.入料_流道傳送軸_真空開啟_STP.LastExecuteMs = ((long)(0));
            this.入料_流道傳送軸_真空開啟_STP.Location = new System.Drawing.Point(33, 459);
            this.入料_流道傳送軸_真空開啟_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.入料_流道傳送軸_真空開啟_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.入料_流道傳送軸_真空開啟_STP.Name = "入料_流道傳送軸_真空開啟_STP";
            this.入料_流道傳送軸_真空開啟_STP.NextProcess = this.入料_流道傳送軸_頂升汽缸開啟_STP;
            this.入料_流道傳送軸_真空開啟_STP.Size = new System.Drawing.Size(164, 41);
            this.入料_流道傳送軸_真空開啟_STP.StopWhenError = true;
            this.入料_流道傳送軸_真空開啟_STP.TabIndex = 30;
            this.入料_流道傳送軸_真空開啟_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.入料_CVRA_真空開啟_STP_ProcessIn);
            this.入料_流道傳送軸_真空開啟_STP.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.入料_流道傳送軸_真空開啟_STP.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 入料_流道傳送軸_真空開啟逾時_Err
            // 
            this.入料_流道傳送軸_真空開啟逾時_Err.BackColor = System.Drawing.Color.Cyan;
            this.入料_流道傳送軸_真空開啟逾時_Err.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.入料_流道傳送軸_真空開啟逾時_Err.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.入料_流道傳送軸_真空開啟逾時_Err.DoNotStopAtThisProcess = false;
            this.入料_流道傳送軸_真空開啟逾時_Err.ErrorDescription = "";
            this.入料_流道傳送軸_真空開啟逾時_Err.ExceptionWaitTime = 100;
            this.入料_流道傳送軸_真空開啟逾時_Err.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.入料_流道傳送軸_真空開啟逾時_Err.LastExecuteMs = ((long)(0));
            this.入料_流道傳送軸_真空開啟逾時_Err.Location = new System.Drawing.Point(258, 459);
            this.入料_流道傳送軸_真空開啟逾時_Err.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.入料_流道傳送軸_真空開啟逾時_Err.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.入料_流道傳送軸_真空開啟逾時_Err.Name = "入料_流道傳送軸_真空開啟逾時_Err";
            this.入料_流道傳送軸_真空開啟逾時_Err.NextProcess = this.入料_流道傳送軸_真空開啟_STP;
            this.入料_流道傳送軸_真空開啟逾時_Err.Size = new System.Drawing.Size(136, 41);
            this.入料_流道傳送軸_真空開啟逾時_Err.StopWhenError = true;
            this.入料_流道傳送軸_真空開啟逾時_Err.TabIndex = 33;
            this.入料_流道傳送軸_真空開啟逾時_Err.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.入料_CVRA_真空開啟逾時_Err_ProcessIn);
            this.入料_流道傳送軸_真空開啟逾時_Err.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.入料_流道傳送軸_真空開啟逾時_Err.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 入料_流道傳送軸_頂升汽缸開啟_STP
            // 
            this.入料_流道傳送軸_頂升汽缸開啟_STP.BackColor = System.Drawing.Color.Cyan;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.DoNotStopAtThisProcess = false;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.ErrorDescription = "";
            this.入料_流道傳送軸_頂升汽缸開啟_STP.ExceptionWaitTime = 100;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.FailProcess = this.入料_流道傳送軸_頂升汽缸開啟逾時_Err;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.入料_流道傳送軸_頂升汽缸開啟_STP.LastExecuteMs = ((long)(0));
            this.入料_流道傳送軸_頂升汽缸開啟_STP.Location = new System.Drawing.Point(33, 522);
            this.入料_流道傳送軸_頂升汽缸開啟_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.入料_流道傳送軸_頂升汽缸開啟_STP.Name = "入料_流道傳送軸_頂升汽缸開啟_STP";
            this.入料_流道傳送軸_頂升汽缸開啟_STP.NextProcess = this.流道傳送軸_入料動作_OK_END;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.Size = new System.Drawing.Size(164, 41);
            this.入料_流道傳送軸_頂升汽缸開啟_STP.StopWhenError = true;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.TabIndex = 31;
            this.入料_流道傳送軸_頂升汽缸開啟_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.入料_CVRA_頂升汽缸開啟_STP_ProcessIn);
            this.入料_流道傳送軸_頂升汽缸開啟_STP.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.入料_流道傳送軸_頂升汽缸開啟_STP.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 入料_流道傳送軸_頂升汽缸開啟逾時_Err
            // 
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.BackColor = System.Drawing.Color.Cyan;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.DoNotStopAtThisProcess = false;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.ErrorDescription = "";
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.ExceptionWaitTime = 100;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.LastExecuteMs = ((long)(0));
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.Location = new System.Drawing.Point(258, 522);
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.Name = "入料_流道傳送軸_頂升汽缸開啟逾時_Err";
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.NextProcess = this.入料_流道傳送軸_頂升汽缸開啟_STP;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.Size = new System.Drawing.Size(136, 41);
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.StopWhenError = true;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.TabIndex = 34;
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.入料_CVRA_頂升汽缸開啟逾時_Err_ProcessIn);
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.入料_流道傳送軸_頂升汽缸開啟逾時_Err.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 流道傳送軸_入料動作_OK_END
            // 
            this.流道傳送軸_入料動作_OK_END.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_入料動作_OK_END.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_入料動作_OK_END.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_入料動作_OK_END.DoNotStopAtThisProcess = false;
            this.流道傳送軸_入料動作_OK_END.ErrorDescription = "";
            this.流道傳送軸_入料動作_OK_END.ExceptionWaitTime = 100;
            this.流道傳送軸_入料動作_OK_END.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_入料動作_OK_END.LastExecuteMs = ((long)(0));
            this.流道傳送軸_入料動作_OK_END.Location = new System.Drawing.Point(33, 591);
            this.流道傳送軸_入料動作_OK_END.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_入料動作_OK_END.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_入料動作_OK_END.Name = "流道傳送軸_入料動作_OK_END";
            this.流道傳送軸_入料動作_OK_END.Size = new System.Drawing.Size(164, 41);
            this.流道傳送軸_入料動作_OK_END.StopWhenError = true;
            this.流道傳送軸_入料動作_OK_END.TabIndex = 24;
            this.流道傳送軸_入料動作_OK_END.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_入料動作_OK_END_ProcessIn);
            this.流道傳送軸_入料動作_OK_END.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.流道傳送軸_入料動作_OK_END.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 等待_流道傳送軸_到位檢知逾時_Err
            // 
            this.等待_流道傳送軸_到位檢知逾時_Err.BackColor = System.Drawing.Color.Cyan;
            this.等待_流道傳送軸_到位檢知逾時_Err.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待_流道傳送軸_到位檢知逾時_Err.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待_流道傳送軸_到位檢知逾時_Err.DoNotStopAtThisProcess = false;
            this.等待_流道傳送軸_到位檢知逾時_Err.ErrorDescription = "";
            this.等待_流道傳送軸_到位檢知逾時_Err.ExceptionWaitTime = 100;
            this.等待_流道傳送軸_到位檢知逾時_Err.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.等待_流道傳送軸_到位檢知逾時_Err.LastExecuteMs = ((long)(0));
            this.等待_流道傳送軸_到位檢知逾時_Err.Location = new System.Drawing.Point(258, 328);
            this.等待_流道傳送軸_到位檢知逾時_Err.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待_流道傳送軸_到位檢知逾時_Err.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.等待_流道傳送軸_到位檢知逾時_Err.Name = "等待_流道傳送軸_到位檢知逾時_Err";
            this.等待_流道傳送軸_到位檢知逾時_Err.NextProcess = this.等待_流道傳送軸_到位檢知_STP;
            this.等待_流道傳送軸_到位檢知逾時_Err.Size = new System.Drawing.Size(136, 41);
            this.等待_流道傳送軸_到位檢知逾時_Err.StopWhenError = true;
            this.等待_流道傳送軸_到位檢知逾時_Err.TabIndex = 29;
            this.等待_流道傳送軸_到位檢知逾時_Err.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待_CVRA_到位檢知逾時_Err_ProcessIn);
            this.等待_流道傳送軸_到位檢知逾時_Err.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.等待_流道傳送軸_到位檢知逾時_Err.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 等待_流道傳送軸_到位檢知_STP
            // 
            this.等待_流道傳送軸_到位檢知_STP.BackColor = System.Drawing.Color.Cyan;
            this.等待_流道傳送軸_到位檢知_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待_流道傳送軸_到位檢知_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待_流道傳送軸_到位檢知_STP.DoNotStopAtThisProcess = false;
            this.等待_流道傳送軸_到位檢知_STP.ErrorDescription = "";
            this.等待_流道傳送軸_到位檢知_STP.ExceptionWaitTime = 100;
            this.等待_流道傳送軸_到位檢知_STP.FailProcess = this.等待_流道傳送軸_到位檢知逾時_Err;
            this.等待_流道傳送軸_到位檢知_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.等待_流道傳送軸_到位檢知_STP.LastExecuteMs = ((long)(0));
            this.等待_流道傳送軸_到位檢知_STP.Location = new System.Drawing.Point(33, 328);
            this.等待_流道傳送軸_到位檢知_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待_流道傳送軸_到位檢知_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.等待_流道傳送軸_到位檢知_STP.Name = "等待_流道傳送軸_到位檢知_STP";
            this.等待_流道傳送軸_到位檢知_STP.NextProcess = this.流道傳送軸_Guide動作_STP;
            this.等待_流道傳送軸_到位檢知_STP.Size = new System.Drawing.Size(164, 41);
            this.等待_流道傳送軸_到位檢知_STP.StopWhenError = true;
            this.等待_流道傳送軸_到位檢知_STP.TabIndex = 24;
            this.等待_流道傳送軸_到位檢知_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待_CVRA_到位檢知_STP_ProcessIn);
            this.等待_流道傳送軸_到位檢知_STP.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.等待_流道傳送軸_到位檢知_STP.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 等待_流道傳送軸_減速檢知逾時_Err
            // 
            this.等待_流道傳送軸_減速檢知逾時_Err.BackColor = System.Drawing.Color.Cyan;
            this.等待_流道傳送軸_減速檢知逾時_Err.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待_流道傳送軸_減速檢知逾時_Err.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待_流道傳送軸_減速檢知逾時_Err.DoNotStopAtThisProcess = false;
            this.等待_流道傳送軸_減速檢知逾時_Err.ErrorDescription = "";
            this.等待_流道傳送軸_減速檢知逾時_Err.ExceptionWaitTime = 100;
            this.等待_流道傳送軸_減速檢知逾時_Err.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.等待_流道傳送軸_減速檢知逾時_Err.LastExecuteMs = ((long)(0));
            this.等待_流道傳送軸_減速檢知逾時_Err.Location = new System.Drawing.Point(258, 259);
            this.等待_流道傳送軸_減速檢知逾時_Err.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待_流道傳送軸_減速檢知逾時_Err.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.等待_流道傳送軸_減速檢知逾時_Err.Name = "等待_流道傳送軸_減速檢知逾時_Err";
            this.等待_流道傳送軸_減速檢知逾時_Err.NextProcess = this.等待_流道傳送軸_減速檢知_STP;
            this.等待_流道傳送軸_減速檢知逾時_Err.Size = new System.Drawing.Size(136, 41);
            this.等待_流道傳送軸_減速檢知逾時_Err.StopWhenError = true;
            this.等待_流道傳送軸_減速檢知逾時_Err.TabIndex = 27;
            this.等待_流道傳送軸_減速檢知逾時_Err.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待_CVRA_低速檢知逾時_Err_ProcessIn);
            this.等待_流道傳送軸_減速檢知逾時_Err.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.等待_流道傳送軸_減速檢知逾時_Err.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 等待_流道傳送軸_減速檢知_STP
            // 
            this.等待_流道傳送軸_減速檢知_STP.BackColor = System.Drawing.Color.Cyan;
            this.等待_流道傳送軸_減速檢知_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待_流道傳送軸_減速檢知_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待_流道傳送軸_減速檢知_STP.DoNotStopAtThisProcess = false;
            this.等待_流道傳送軸_減速檢知_STP.ErrorDescription = "";
            this.等待_流道傳送軸_減速檢知_STP.ExceptionWaitTime = 100;
            this.等待_流道傳送軸_減速檢知_STP.FailProcess = this.等待_流道傳送軸_減速檢知逾時_Err;
            this.等待_流道傳送軸_減速檢知_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.等待_流道傳送軸_減速檢知_STP.LastExecuteMs = ((long)(0));
            this.等待_流道傳送軸_減速檢知_STP.Location = new System.Drawing.Point(33, 259);
            this.等待_流道傳送軸_減速檢知_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待_流道傳送軸_減速檢知_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.等待_流道傳送軸_減速檢知_STP.Name = "等待_流道傳送軸_減速檢知_STP";
            this.等待_流道傳送軸_減速檢知_STP.NextProcess = this.等待_流道傳送軸_到位檢知_STP;
            this.等待_流道傳送軸_減速檢知_STP.Size = new System.Drawing.Size(164, 41);
            this.等待_流道傳送軸_減速檢知_STP.StopWhenError = true;
            this.等待_流道傳送軸_減速檢知_STP.TabIndex = 24;
            this.等待_流道傳送軸_減速檢知_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待_CVRA_低速檢知_STP_ProcessIn);
            this.等待_流道傳送軸_減速檢知_STP.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.等待_流道傳送軸_減速檢知_STP.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 等待_流道傳送軸_Load檢知逾時_Err
            // 
            this.等待_流道傳送軸_Load檢知逾時_Err.BackColor = System.Drawing.Color.Cyan;
            this.等待_流道傳送軸_Load檢知逾時_Err.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待_流道傳送軸_Load檢知逾時_Err.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待_流道傳送軸_Load檢知逾時_Err.DoNotStopAtThisProcess = false;
            this.等待_流道傳送軸_Load檢知逾時_Err.ErrorDescription = "";
            this.等待_流道傳送軸_Load檢知逾時_Err.ExceptionWaitTime = 100;
            this.等待_流道傳送軸_Load檢知逾時_Err.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.等待_流道傳送軸_Load檢知逾時_Err.LastExecuteMs = ((long)(0));
            this.等待_流道傳送軸_Load檢知逾時_Err.Location = new System.Drawing.Point(258, 121);
            this.等待_流道傳送軸_Load檢知逾時_Err.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待_流道傳送軸_Load檢知逾時_Err.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.等待_流道傳送軸_Load檢知逾時_Err.Name = "等待_流道傳送軸_Load檢知逾時_Err";
            this.等待_流道傳送軸_Load檢知逾時_Err.NextProcess = this.等待_流道傳送軸_Load檢知_STP;
            this.等待_流道傳送軸_Load檢知逾時_Err.Size = new System.Drawing.Size(136, 41);
            this.等待_流道傳送軸_Load檢知逾時_Err.StopWhenError = true;
            this.等待_流道傳送軸_Load檢知逾時_Err.TabIndex = 25;
            this.等待_流道傳送軸_Load檢知逾時_Err.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待_CVRA_Load檢知逾時_Err_ProcessIn);
            this.等待_流道傳送軸_Load檢知逾時_Err.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.等待_流道傳送軸_Load檢知逾時_Err.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 等待_流道傳送軸_Load檢知_STP
            // 
            this.等待_流道傳送軸_Load檢知_STP.BackColor = System.Drawing.Color.Cyan;
            this.等待_流道傳送軸_Load檢知_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待_流道傳送軸_Load檢知_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待_流道傳送軸_Load檢知_STP.DoNotStopAtThisProcess = false;
            this.等待_流道傳送軸_Load檢知_STP.ErrorDescription = "";
            this.等待_流道傳送軸_Load檢知_STP.ExceptionWaitTime = 100;
            this.等待_流道傳送軸_Load檢知_STP.FailProcess = this.等待_流道傳送軸_Load檢知逾時_Err;
            this.等待_流道傳送軸_Load檢知_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.等待_流道傳送軸_Load檢知_STP.LastExecuteMs = ((long)(0));
            this.等待_流道傳送軸_Load檢知_STP.Location = new System.Drawing.Point(33, 121);
            this.等待_流道傳送軸_Load檢知_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待_流道傳送軸_Load檢知_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.等待_流道傳送軸_Load檢知_STP.Name = "等待_流道傳送軸_Load檢知_STP";
            this.等待_流道傳送軸_Load檢知_STP.NextProcess = this.流道傳送軸_啟動_STP;
            this.等待_流道傳送軸_Load檢知_STP.Size = new System.Drawing.Size(164, 41);
            this.等待_流道傳送軸_Load檢知_STP.StopWhenError = true;
            this.等待_流道傳送軸_Load檢知_STP.TabIndex = 24;
            this.等待_流道傳送軸_Load檢知_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待_CVRA_Load檢知_STP_ProcessIn);
            this.等待_流道傳送軸_Load檢知_STP.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.等待_流道傳送軸_Load檢知_STP.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 流道傳送軸_啟動_STP
            // 
            this.流道傳送軸_啟動_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_啟動_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_啟動_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_啟動_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_啟動_STP.ErrorDescription = "";
            this.流道傳送軸_啟動_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_啟動_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_啟動_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_啟動_STP.Location = new System.Drawing.Point(33, 190);
            this.流道傳送軸_啟動_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_啟動_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_啟動_STP.Name = "流道傳送軸_啟動_STP";
            this.流道傳送軸_啟動_STP.NextProcess = this.等待_流道傳送軸_減速檢知_STP;
            this.流道傳送軸_啟動_STP.Size = new System.Drawing.Size(164, 41);
            this.流道傳送軸_啟動_STP.StopWhenError = true;
            this.流道傳送軸_啟動_STP.TabIndex = 24;
            this.流道傳送軸_啟動_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_啟動_STP_ProcessIn);
            this.流道傳送軸_啟動_STP.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.流道傳送軸_啟動_STP.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 流道傳送軸_停止汽缸_Up_STP
            // 
            this.流道傳送軸_停止汽缸_Up_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_停止汽缸_Up_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_停止汽缸_Up_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_停止汽缸_Up_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_停止汽缸_Up_STP.ErrorDescription = "";
            this.流道傳送軸_停止汽缸_Up_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_停止汽缸_Up_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_停止汽缸_Up_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_停止汽缸_Up_STP.Location = new System.Drawing.Point(33, 56);
            this.流道傳送軸_停止汽缸_Up_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_停止汽缸_Up_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_停止汽缸_Up_STP.Name = "流道傳送軸_停止汽缸_Up_STP";
            this.流道傳送軸_停止汽缸_Up_STP.NextProcess = this.等待_流道傳送軸_Load檢知_STP;
            this.流道傳送軸_停止汽缸_Up_STP.Size = new System.Drawing.Size(164, 41);
            this.流道傳送軸_停止汽缸_Up_STP.StopWhenError = true;
            this.流道傳送軸_停止汽缸_Up_STP.TabIndex = 5;
            this.流道傳送軸_停止汽缸_Up_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_停止汽缸_Up_STP_ProcessIn);
            this.流道傳送軸_停止汽缸_Up_STP.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.流道傳送軸_停止汽缸_Up_STP.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 流道傳送軸_入料動作_Start
            // 
            this.流道傳送軸_入料動作_Start.BackColor = System.Drawing.Color.Chocolate;
            this.流道傳送軸_入料動作_Start.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_入料動作_Start.DoNotStopAtThisProcess = false;
            this.流道傳送軸_入料動作_Start.ErrorDescription = "";
            this.流道傳送軸_入料動作_Start.ExceptionWaitTime = 100;
            this.流道傳送軸_入料動作_Start.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_入料動作_Start.LastExecuteMs = ((long)(0));
            this.流道傳送軸_入料動作_Start.Location = new System.Drawing.Point(281, 56);
            this.流道傳送軸_入料動作_Start.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_入料動作_Start.Name = "流道傳送軸_入料動作_Start";
            this.流道傳送軸_入料動作_Start.NextProcess = this.流道傳送軸_停止汽缸_Up_STP;
            this.流道傳送軸_入料動作_Start.Size = new System.Drawing.Size(113, 41);
            this.流道傳送軸_入料動作_Start.StopWhenError = true;
            this.流道傳送軸_入料動作_Start.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.controlFlow2);
            this.tabPage2.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(458, 657);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "流道_有Boat";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // controlFlow2
            // 
            this.controlFlow2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow2.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow2.Controls.Add(this.有料_Guide動作_STP);
            this.controlFlow2.Controls.Add(this.有料_流道傳送軸_頂升汽缸開啟_STP);
            this.controlFlow2.Controls.Add(this.有料_流道傳送軸_真空開啟_STP);
            this.controlFlow2.Controls.Add(this.流道傳送軸_到位檢知脫離_STP);
            this.controlFlow2.Controls.Add(this.label6);
            this.controlFlow2.Controls.Add(this.label5);
            this.controlFlow2.Controls.Add(this.流道傳送軸_停止汽缸_上升_STP);
            this.controlFlow2.Controls.Add(this.流道傳送軸_反轉脫離動作_STP);
            this.controlFlow2.Controls.Add(this.流道_含Boat_OK_END);
            this.controlFlow2.Controls.Add(this.流道傳送軸_正轉靠位動作_STP);
            this.controlFlow2.Controls.Add(this.流道傳送軸_到位檢知_Check);
            this.controlFlow2.Controls.Add(this.流道傳送軸_停止汽缸下降_STP);
            this.controlFlow2.Controls.Add(this.流道_含Boat_Start);
            this.controlFlow2.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow2.Location = new System.Drawing.Point(2, 2);
            this.controlFlow2.Name = "controlFlow2";
            this.controlFlow2.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow2.RefreshAllowed = false;
            this.controlFlow2.Size = new System.Drawing.Size(454, 653);
            this.controlFlow2.TabIndex = 0;
            // 
            // 有料_Guide動作_STP
            // 
            this.有料_Guide動作_STP.BackColor = System.Drawing.Color.Cyan;
            this.有料_Guide動作_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.有料_Guide動作_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.有料_Guide動作_STP.DoNotStopAtThisProcess = false;
            this.有料_Guide動作_STP.ErrorDescription = "";
            this.有料_Guide動作_STP.ExceptionWaitTime = 100;
            this.有料_Guide動作_STP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.有料_Guide動作_STP.LastExecuteMs = ((long)(0));
            this.有料_Guide動作_STP.Location = new System.Drawing.Point(36, 354);
            this.有料_Guide動作_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.有料_Guide動作_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.有料_Guide動作_STP.Name = "有料_Guide動作_STP";
            this.有料_Guide動作_STP.NextProcess = this.有料_流道傳送軸_真空開啟_STP;
            this.有料_Guide動作_STP.Size = new System.Drawing.Size(171, 32);
            this.有料_Guide動作_STP.StopWhenError = true;
            this.有料_Guide動作_STP.TabIndex = 36;
            this.有料_Guide動作_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.流道傳送軸_Guide動作_STP_ProcessIn);
            // 
            // 有料_流道傳送軸_真空開啟_STP
            // 
            this.有料_流道傳送軸_真空開啟_STP.BackColor = System.Drawing.Color.Cyan;
            this.有料_流道傳送軸_真空開啟_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.有料_流道傳送軸_真空開啟_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.有料_流道傳送軸_真空開啟_STP.DoNotStopAtThisProcess = false;
            this.有料_流道傳送軸_真空開啟_STP.ErrorDescription = "";
            this.有料_流道傳送軸_真空開啟_STP.ExceptionWaitTime = 100;
            this.有料_流道傳送軸_真空開啟_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.有料_流道傳送軸_真空開啟_STP.LastExecuteMs = ((long)(0));
            this.有料_流道傳送軸_真空開啟_STP.Location = new System.Drawing.Point(38, 425);
            this.有料_流道傳送軸_真空開啟_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.有料_流道傳送軸_真空開啟_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.有料_流道傳送軸_真空開啟_STP.Name = "有料_流道傳送軸_真空開啟_STP";
            this.有料_流道傳送軸_真空開啟_STP.NextProcess = this.有料_流道傳送軸_頂升汽缸開啟_STP;
            this.有料_流道傳送軸_真空開啟_STP.Size = new System.Drawing.Size(167, 39);
            this.有料_流道傳送軸_真空開啟_STP.StopWhenError = true;
            this.有料_流道傳送軸_真空開啟_STP.TabIndex = 26;
            this.有料_流道傳送軸_真空開啟_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.有料_CVRA_真空開啟_STP_ProcessIn);
            // 
            // 有料_流道傳送軸_頂升汽缸開啟_STP
            // 
            this.有料_流道傳送軸_頂升汽缸開啟_STP.BackColor = System.Drawing.Color.Cyan;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.DoNotStopAtThisProcess = false;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.ErrorDescription = "";
            this.有料_流道傳送軸_頂升汽缸開啟_STP.ExceptionWaitTime = 100;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.有料_流道傳送軸_頂升汽缸開啟_STP.LastExecuteMs = ((long)(0));
            this.有料_流道傳送軸_頂升汽缸開啟_STP.Location = new System.Drawing.Point(38, 495);
            this.有料_流道傳送軸_頂升汽缸開啟_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.有料_流道傳送軸_頂升汽缸開啟_STP.Name = "有料_流道傳送軸_頂升汽缸開啟_STP";
            this.有料_流道傳送軸_頂升汽缸開啟_STP.NextProcess = this.流道_含Boat_OK_END;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.Size = new System.Drawing.Size(167, 37);
            this.有料_流道傳送軸_頂升汽缸開啟_STP.StopWhenError = true;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.TabIndex = 27;
            this.有料_流道傳送軸_頂升汽缸開啟_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.有料_CVRA_頂升汽缸開啟_STP_ProcessIn);
            // 
            // 流道_含Boat_OK_END
            // 
            this.流道_含Boat_OK_END.BackColor = System.Drawing.Color.Cyan;
            this.流道_含Boat_OK_END.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道_含Boat_OK_END.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道_含Boat_OK_END.DoNotStopAtThisProcess = false;
            this.流道_含Boat_OK_END.ErrorDescription = "";
            this.流道_含Boat_OK_END.ExceptionWaitTime = 100;
            this.流道_含Boat_OK_END.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道_含Boat_OK_END.LastExecuteMs = ((long)(0));
            this.流道_含Boat_OK_END.Location = new System.Drawing.Point(40, 562);
            this.流道_含Boat_OK_END.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道_含Boat_OK_END.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道_含Boat_OK_END.Name = "流道_含Boat_OK_END";
            this.流道_含Boat_OK_END.Size = new System.Drawing.Size(162, 40);
            this.流道_含Boat_OK_END.StopWhenError = true;
            this.流道_含Boat_OK_END.TabIndex = 24;
            this.流道_含Boat_OK_END.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CV_含Boat_OK_END_ProcessIn);
            // 
            // 流道傳送軸_到位檢知脫離_STP
            // 
            this.流道傳送軸_到位檢知脫離_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_到位檢知脫離_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_到位檢知脫離_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_到位檢知脫離_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_到位檢知脫離_STP.ErrorDescription = "";
            this.流道傳送軸_到位檢知脫離_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_到位檢知脫離_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_到位檢知脫離_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_到位檢知脫離_STP.Location = new System.Drawing.Point(276, 201);
            this.流道傳送軸_到位檢知脫離_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_到位檢知脫離_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_到位檢知脫離_STP.Name = "流道傳送軸_到位檢知脫離_STP";
            this.流道傳送軸_到位檢知脫離_STP.NextProcess = this.流道傳送軸_停止汽缸_上升_STP;
            this.流道傳送軸_到位檢知脫離_STP.Size = new System.Drawing.Size(122, 42);
            this.流道傳送軸_到位檢知脫離_STP.StopWhenError = true;
            this.流道傳送軸_到位檢知脫離_STP.TabIndex = 25;
            this.流道傳送軸_到位檢知脫離_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_到位檢知脫離_STP_ProcessIn);
            // 
            // 流道傳送軸_停止汽缸_上升_STP
            // 
            this.流道傳送軸_停止汽缸_上升_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_停止汽缸_上升_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_停止汽缸_上升_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_停止汽缸_上升_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_停止汽缸_上升_STP.ErrorDescription = "";
            this.流道傳送軸_停止汽缸_上升_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_停止汽缸_上升_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_停止汽缸_上升_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_停止汽缸_上升_STP.Location = new System.Drawing.Point(276, 282);
            this.流道傳送軸_停止汽缸_上升_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_停止汽缸_上升_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_停止汽缸_上升_STP.Name = "流道傳送軸_停止汽缸_上升_STP";
            this.流道傳送軸_停止汽缸_上升_STP.NextProcess = this.流道傳送軸_正轉靠位動作_STP;
            this.流道傳送軸_停止汽缸_上升_STP.Size = new System.Drawing.Size(122, 40);
            this.流道傳送軸_停止汽缸_上升_STP.StopWhenError = true;
            this.流道傳送軸_停止汽缸_上升_STP.TabIndex = 24;
            this.流道傳送軸_停止汽缸_上升_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_停止汽缸_上升_STP_ProcessIn);
            // 
            // 流道傳送軸_正轉靠位動作_STP
            // 
            this.流道傳送軸_正轉靠位動作_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_正轉靠位動作_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_正轉靠位動作_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_正轉靠位動作_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_正轉靠位動作_STP.ErrorDescription = "";
            this.流道傳送軸_正轉靠位動作_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_正轉靠位動作_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_正轉靠位動作_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_正轉靠位動作_STP.Location = new System.Drawing.Point(34, 282);
            this.流道傳送軸_正轉靠位動作_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_正轉靠位動作_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_正轉靠位動作_STP.Name = "流道傳送軸_正轉靠位動作_STP";
            this.流道傳送軸_正轉靠位動作_STP.NextProcess = this.有料_Guide動作_STP;
            this.流道傳送軸_正轉靠位動作_STP.Size = new System.Drawing.Size(176, 40);
            this.流道傳送軸_正轉靠位動作_STP.StopWhenError = true;
            this.流道傳送軸_正轉靠位動作_STP.TabIndex = 24;
            this.流道傳送軸_正轉靠位動作_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_正轉靠位動作_STP_ProcessIn);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(133, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 18);
            this.label6.TabIndex = 24;
            this.label6.Text = "False";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(289, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 18);
            this.label5.TabIndex = 24;
            this.label5.Text = "True";
            // 
            // 流道傳送軸_反轉脫離動作_STP
            // 
            this.流道傳送軸_反轉脫離動作_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_反轉脫離動作_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_反轉脫離動作_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_反轉脫離動作_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_反轉脫離動作_STP.ErrorDescription = "";
            this.流道傳送軸_反轉脫離動作_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_反轉脫離動作_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_反轉脫離動作_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_反轉脫離動作_STP.Location = new System.Drawing.Point(276, 112);
            this.流道傳送軸_反轉脫離動作_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_反轉脫離動作_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_反轉脫離動作_STP.Name = "流道傳送軸_反轉脫離動作_STP";
            this.流道傳送軸_反轉脫離動作_STP.NextProcess = this.流道傳送軸_到位檢知脫離_STP;
            this.流道傳送軸_反轉脫離動作_STP.Size = new System.Drawing.Size(122, 34);
            this.流道傳送軸_反轉脫離動作_STP.StopWhenError = true;
            this.流道傳送軸_反轉脫離動作_STP.TabIndex = 24;
            this.流道傳送軸_反轉脫離動作_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_反轉脫離動作_STP_ProcessIn);
            // 
            // 流道傳送軸_到位檢知_Check
            // 
            this.流道傳送軸_到位檢知_Check.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_到位檢知_Check.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_到位檢知_Check.DoNotStopAtThisProcess = false;
            this.流道傳送軸_到位檢知_Check.ErrorDescription = "";
            this.流道傳送軸_到位檢知_Check.FalseProcess = this.流道傳送軸_正轉靠位動作_STP;
            this.流道傳送軸_到位檢知_Check.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_到位檢知_Check.Location = new System.Drawing.Point(20, 187);
            this.流道傳送軸_到位檢知_Check.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_到位檢知_Check.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_到位檢知_Check.Name = "流道傳送軸_到位檢知_Check";
            this.流道傳送軸_到位檢知_Check.Size = new System.Drawing.Size(204, 61);
            this.流道傳送軸_到位檢知_Check.TabIndex = 20;
            this.流道傳送軸_到位檢知_Check.TrueProcess = this.流道傳送軸_反轉脫離動作_STP;
            this.流道傳送軸_到位檢知_Check.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.CVRA_到位檢知_Check_ConditionCheck);
            // 
            // 流道傳送軸_停止汽缸下降_STP
            // 
            this.流道傳送軸_停止汽缸下降_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_停止汽缸下降_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_停止汽缸下降_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_停止汽缸下降_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_停止汽缸下降_STP.ErrorDescription = "";
            this.流道傳送軸_停止汽缸下降_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_停止汽缸下降_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_停止汽缸下降_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_停止汽缸下降_STP.Location = new System.Drawing.Point(40, 130);
            this.流道傳送軸_停止汽缸下降_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_停止汽缸下降_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_停止汽缸下降_STP.Name = "流道傳送軸_停止汽缸下降_STP";
            this.流道傳送軸_停止汽缸下降_STP.NextProcess = this.流道傳送軸_到位檢知_Check;
            this.流道傳送軸_停止汽缸下降_STP.Size = new System.Drawing.Size(162, 34);
            this.流道傳送軸_停止汽缸下降_STP.StopWhenError = true;
            this.流道傳送軸_停止汽缸下降_STP.TabIndex = 19;
            this.流道傳送軸_停止汽缸下降_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_停止汽缸_下降_STP_ProcessIn);
            // 
            // 流道_含Boat_Start
            // 
            this.流道_含Boat_Start.BackColor = System.Drawing.Color.Chocolate;
            this.流道_含Boat_Start.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道_含Boat_Start.DoNotStopAtThisProcess = false;
            this.流道_含Boat_Start.ErrorDescription = "";
            this.流道_含Boat_Start.ExceptionWaitTime = 100;
            this.流道_含Boat_Start.LastExecuteMs = ((long)(0));
            this.流道_含Boat_Start.Location = new System.Drawing.Point(31, 42);
            this.流道_含Boat_Start.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道_含Boat_Start.Name = "流道_含Boat_Start";
            this.流道_含Boat_Start.NextProcess = this.流道傳送軸_停止汽缸下降_STP;
            this.流道_含Boat_Start.Size = new System.Drawing.Size(100, 27);
            this.流道_含Boat_Start.StopWhenError = true;
            this.流道_含Boat_Start.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.controlFlow3);
            this.tabPage3.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(458, 657);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "流道傳送軸_出料動作";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // controlFlow3
            // 
            this.controlFlow3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow3.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow3.Controls.Add(this.有料_流道傳送軸_頂升汽缸關閉_STP);
            this.controlFlow3.Controls.Add(this.流道傳送軸_真空_關閉_STP);
            this.controlFlow3.Controls.Add(this.流道傳送軸_定位汽缸_關閉_STP);
            this.controlFlow3.Controls.Add(this.流道傳送軸_出料動作_Err);
            this.controlFlow3.Controls.Add(this.流道傳送軸_出料動作_OK);
            this.controlFlow3.Controls.Add(this.流道傳送軸啟動出料_STP);
            this.controlFlow3.Controls.Add(this.流道傳送軸_停止汽缸_下降_STP);
            this.controlFlow3.Controls.Add(this.等待_下位SMEMA_Flg);
            this.controlFlow3.Controls.Add(this.發送_下位SMEMA_Flg);
            this.controlFlow3.Controls.Add(this.流道傳送軸_出料動作_Start);
            this.controlFlow3.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow3.Location = new System.Drawing.Point(0, 0);
            this.controlFlow3.Name = "controlFlow3";
            this.controlFlow3.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow3.RefreshAllowed = false;
            this.controlFlow3.Size = new System.Drawing.Size(458, 657);
            this.controlFlow3.TabIndex = 0;
            // 
            // 有料_流道傳送軸_頂升汽缸關閉_STP
            // 
            this.有料_流道傳送軸_頂升汽缸關閉_STP.BackColor = System.Drawing.Color.Cyan;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.DoNotStopAtThisProcess = false;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.ErrorDescription = "";
            this.有料_流道傳送軸_頂升汽缸關閉_STP.ExceptionWaitTime = 100;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.有料_流道傳送軸_頂升汽缸關閉_STP.LastExecuteMs = ((long)(0));
            this.有料_流道傳送軸_頂升汽缸關閉_STP.Location = new System.Drawing.Point(28, 276);
            this.有料_流道傳送軸_頂升汽缸關閉_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.有料_流道傳送軸_頂升汽缸關閉_STP.Name = "有料_流道傳送軸_頂升汽缸關閉_STP";
            this.有料_流道傳送軸_頂升汽缸關閉_STP.NextProcess = this.發送_下位SMEMA_Flg;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.Size = new System.Drawing.Size(205, 42);
            this.有料_流道傳送軸_頂升汽缸關閉_STP.StopWhenError = true;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.TabIndex = 28;
            this.有料_流道傳送軸_頂升汽缸關閉_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.有料_CVRA_頂升汽缸關閉_STP_ProcessIn);
            // 
            // 發送_下位SMEMA_Flg
            // 
            this.發送_下位SMEMA_Flg.BackColor = System.Drawing.Color.Cyan;
            this.發送_下位SMEMA_Flg.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.發送_下位SMEMA_Flg.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.發送_下位SMEMA_Flg.DoNotStopAtThisProcess = false;
            this.發送_下位SMEMA_Flg.ErrorDescription = "";
            this.發送_下位SMEMA_Flg.ExceptionWaitTime = 100;
            this.發送_下位SMEMA_Flg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.發送_下位SMEMA_Flg.LastExecuteMs = ((long)(0));
            this.發送_下位SMEMA_Flg.Location = new System.Drawing.Point(28, 351);
            this.發送_下位SMEMA_Flg.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.發送_下位SMEMA_Flg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.發送_下位SMEMA_Flg.Name = "發送_下位SMEMA_Flg";
            this.發送_下位SMEMA_Flg.NextProcess = this.等待_下位SMEMA_Flg;
            this.發送_下位SMEMA_Flg.Size = new System.Drawing.Size(205, 42);
            this.發送_下位SMEMA_Flg.StopWhenError = true;
            this.發送_下位SMEMA_Flg.TabIndex = 20;
            this.發送_下位SMEMA_Flg.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.發送_下位SMEMA_Flg_ProcessIn);
            // 
            // 等待_下位SMEMA_Flg
            // 
            this.等待_下位SMEMA_Flg.BackColor = System.Drawing.Color.Cyan;
            this.等待_下位SMEMA_Flg.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待_下位SMEMA_Flg.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待_下位SMEMA_Flg.DoNotStopAtThisProcess = false;
            this.等待_下位SMEMA_Flg.ErrorDescription = "";
            this.等待_下位SMEMA_Flg.ExceptionWaitTime = 100;
            this.等待_下位SMEMA_Flg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.等待_下位SMEMA_Flg.LastExecuteMs = ((long)(0));
            this.等待_下位SMEMA_Flg.Location = new System.Drawing.Point(28, 425);
            this.等待_下位SMEMA_Flg.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待_下位SMEMA_Flg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.等待_下位SMEMA_Flg.Name = "等待_下位SMEMA_Flg";
            this.等待_下位SMEMA_Flg.NextProcess = this.流道傳送軸啟動出料_STP;
            this.等待_下位SMEMA_Flg.Size = new System.Drawing.Size(205, 42);
            this.等待_下位SMEMA_Flg.StopWhenError = true;
            this.等待_下位SMEMA_Flg.TabIndex = 24;
            this.等待_下位SMEMA_Flg.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待_下位SMEMA_Flg_ProcessIn);
            // 
            // 流道傳送軸啟動出料_STP
            // 
            this.流道傳送軸啟動出料_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸啟動出料_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸啟動出料_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸啟動出料_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸啟動出料_STP.ErrorDescription = "";
            this.流道傳送軸啟動出料_STP.ExceptionWaitTime = 100;
            this.流道傳送軸啟動出料_STP.FailProcess = this.流道傳送軸_出料動作_Err;
            this.流道傳送軸啟動出料_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸啟動出料_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸啟動出料_STP.Location = new System.Drawing.Point(28, 496);
            this.流道傳送軸啟動出料_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸啟動出料_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸啟動出料_STP.Name = "流道傳送軸啟動出料_STP";
            this.流道傳送軸啟動出料_STP.NextProcess = this.流道傳送軸_出料動作_OK;
            this.流道傳送軸啟動出料_STP.Size = new System.Drawing.Size(205, 42);
            this.流道傳送軸啟動出料_STP.StopWhenError = true;
            this.流道傳送軸啟動出料_STP.TabIndex = 24;
            this.流道傳送軸啟動出料_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CV啟動出料_STP_ProcessIn);
            // 
            // 流道傳送軸_出料動作_Err
            // 
            this.流道傳送軸_出料動作_Err.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_出料動作_Err.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_出料動作_Err.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_出料動作_Err.DoNotStopAtThisProcess = false;
            this.流道傳送軸_出料動作_Err.ErrorDescription = "";
            this.流道傳送軸_出料動作_Err.ExceptionWaitTime = 100;
            this.流道傳送軸_出料動作_Err.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_出料動作_Err.LastExecuteMs = ((long)(0));
            this.流道傳送軸_出料動作_Err.Location = new System.Drawing.Point(311, 496);
            this.流道傳送軸_出料動作_Err.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_出料動作_Err.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_出料動作_Err.Name = "流道傳送軸_出料動作_Err";
            this.流道傳送軸_出料動作_Err.NextProcess = this.流道傳送軸啟動出料_STP;
            this.流道傳送軸_出料動作_Err.Size = new System.Drawing.Size(128, 42);
            this.流道傳送軸_出料動作_Err.StopWhenError = true;
            this.流道傳送軸_出料動作_Err.TabIndex = 26;
            this.流道傳送軸_出料動作_Err.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_出料動作_Err_ProcessIn);
            this.流道傳送軸_出料動作_Err.ProcessError += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessError);
            this.流道傳送軸_出料動作_Err.ProcessErrorDone += new System.EventHandler<ControlFlow.Executor.ExecuteFailArgs>(this.Alarm操作_ProcessErrorDone);
            // 
            // 流道傳送軸_出料動作_OK
            // 
            this.流道傳送軸_出料動作_OK.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_出料動作_OK.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_出料動作_OK.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_出料動作_OK.DoNotStopAtThisProcess = false;
            this.流道傳送軸_出料動作_OK.ErrorDescription = "";
            this.流道傳送軸_出料動作_OK.ExceptionWaitTime = 100;
            this.流道傳送軸_出料動作_OK.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_出料動作_OK.LastExecuteMs = ((long)(0));
            this.流道傳送軸_出料動作_OK.Location = new System.Drawing.Point(28, 569);
            this.流道傳送軸_出料動作_OK.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_出料動作_OK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_出料動作_OK.Name = "流道傳送軸_出料動作_OK";
            this.流道傳送軸_出料動作_OK.Size = new System.Drawing.Size(205, 42);
            this.流道傳送軸_出料動作_OK.StopWhenError = true;
            this.流道傳送軸_出料動作_OK.TabIndex = 25;
            this.流道傳送軸_出料動作_OK.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVRA_出料動作_OK_ProcessIn);
            // 
            // 流道傳送軸_真空_關閉_STP
            // 
            this.流道傳送軸_真空_關閉_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_真空_關閉_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_真空_關閉_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_真空_關閉_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_真空_關閉_STP.ErrorDescription = "";
            this.流道傳送軸_真空_關閉_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_真空_關閉_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_真空_關閉_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_真空_關閉_STP.Location = new System.Drawing.Point(28, 209);
            this.流道傳送軸_真空_關閉_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_真空_關閉_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_真空_關閉_STP.Name = "流道傳送軸_真空_關閉_STP";
            this.流道傳送軸_真空_關閉_STP.NextProcess = this.有料_流道傳送軸_頂升汽缸關閉_STP;
            this.流道傳送軸_真空_關閉_STP.Size = new System.Drawing.Size(205, 42);
            this.流道傳送軸_真空_關閉_STP.StopWhenError = true;
            this.流道傳送軸_真空_關閉_STP.TabIndex = 26;
            this.流道傳送軸_真空_關閉_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.真空汽缸_關閉_STP_ProcessIn);
            // 
            // 流道傳送軸_定位汽缸_關閉_STP
            // 
            this.流道傳送軸_定位汽缸_關閉_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_定位汽缸_關閉_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_定位汽缸_關閉_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_定位汽缸_關閉_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_定位汽缸_關閉_STP.ErrorDescription = "";
            this.流道傳送軸_定位汽缸_關閉_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_定位汽缸_關閉_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_定位汽缸_關閉_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_定位汽缸_關閉_STP.Location = new System.Drawing.Point(28, 142);
            this.流道傳送軸_定位汽缸_關閉_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_定位汽缸_關閉_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_定位汽缸_關閉_STP.Name = "流道傳送軸_定位汽缸_關閉_STP";
            this.流道傳送軸_定位汽缸_關閉_STP.NextProcess = this.流道傳送軸_真空_關閉_STP;
            this.流道傳送軸_定位汽缸_關閉_STP.Size = new System.Drawing.Size(205, 42);
            this.流道傳送軸_定位汽缸_關閉_STP.StopWhenError = true;
            this.流道傳送軸_定位汽缸_關閉_STP.TabIndex = 25;
            this.流道傳送軸_定位汽缸_關閉_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.定位汽缸_關閉_STP_ProcessIn);
            // 
            // 流道傳送軸_停止汽缸_下降_STP
            // 
            this.流道傳送軸_停止汽缸_下降_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_停止汽缸_下降_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_停止汽缸_下降_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_停止汽缸_下降_STP.DoNotStopAtThisProcess = false;
            this.流道傳送軸_停止汽缸_下降_STP.ErrorDescription = "";
            this.流道傳送軸_停止汽缸_下降_STP.ExceptionWaitTime = 100;
            this.流道傳送軸_停止汽缸_下降_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_停止汽缸_下降_STP.LastExecuteMs = ((long)(0));
            this.流道傳送軸_停止汽缸_下降_STP.Location = new System.Drawing.Point(28, 70);
            this.流道傳送軸_停止汽缸_下降_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_停止汽缸_下降_STP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.流道傳送軸_停止汽缸_下降_STP.Name = "流道傳送軸_停止汽缸_下降_STP";
            this.流道傳送軸_停止汽缸_下降_STP.NextProcess = this.流道傳送軸_定位汽缸_關閉_STP;
            this.流道傳送軸_停止汽缸_下降_STP.Size = new System.Drawing.Size(205, 42);
            this.流道傳送軸_停止汽缸_下降_STP.StopWhenError = true;
            this.流道傳送軸_停止汽缸_下降_STP.TabIndex = 24;
            this.流道傳送軸_停止汽缸_下降_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.停止汽缸_下降_STP_ProcessIn);
            // 
            // 流道傳送軸_出料動作_Start
            // 
            this.流道傳送軸_出料動作_Start.BackColor = System.Drawing.Color.Chocolate;
            this.流道傳送軸_出料動作_Start.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_出料動作_Start.DoNotStopAtThisProcess = false;
            this.流道傳送軸_出料動作_Start.ErrorDescription = "";
            this.流道傳送軸_出料動作_Start.ExceptionWaitTime = 100;
            this.流道傳送軸_出料動作_Start.LastExecuteMs = ((long)(0));
            this.流道傳送軸_出料動作_Start.Location = new System.Drawing.Point(253, 58);
            this.流道傳送軸_出料動作_Start.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_出料動作_Start.Name = "流道傳送軸_出料動作_Start";
            this.流道傳送軸_出料動作_Start.NextProcess = this.流道傳送軸_停止汽缸_下降_STP;
            this.流道傳送軸_出料動作_Start.Size = new System.Drawing.Size(83, 28);
            this.流道傳送軸_出料動作_Start.StopWhenError = true;
            this.流道傳送軸_出料動作_Start.TabIndex = 2;
            // 
            // FlowCarrier_Start
            // 
            this.FlowCarrier_Start.BackColor = System.Drawing.Color.Chocolate;
            this.FlowCarrier_Start.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.FlowCarrier_Start.DoNotStopAtThisProcess = false;
            this.FlowCarrier_Start.ErrorDescription = "";
            this.FlowCarrier_Start.ExceptionWaitTime = 100;
            this.FlowCarrier_Start.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FlowCarrier_Start.LastExecuteMs = ((long)(0));
            this.FlowCarrier_Start.Location = new System.Drawing.Point(24, 55);
            this.FlowCarrier_Start.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.FlowCarrier_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FlowCarrier_Start.Name = "FlowCarrier_Start";
            this.FlowCarrier_Start.NextProcess = this.是否為Golden模式;
            this.FlowCarrier_Start.Size = new System.Drawing.Size(173, 38);
            this.FlowCarrier_Start.StopWhenError = true;
            this.FlowCarrier_Start.TabIndex = 2;
            this.FlowCarrier_Start.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.FlowCarrier_Start_ProcessIn);
            // 
            // 流道狀態確認_STP
            // 
            this.流道狀態確認_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道狀態確認_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道狀態確認_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道狀態確認_STP.DoNotStopAtThisProcess = false;
            this.流道狀態確認_STP.ErrorDescription = "";
            this.流道狀態確認_STP.ExceptionWaitTime = 100;
            this.流道狀態確認_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道狀態確認_STP.LastExecuteMs = ((long)(0));
            this.流道狀態確認_STP.Location = new System.Drawing.Point(24, 202);
            this.流道狀態確認_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道狀態確認_STP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道狀態確認_STP.Name = "流道狀態確認_STP";
            this.流道狀態確認_STP.NextProcess = this.流道判定有無Boat_Check;
            this.流道狀態確認_STP.Size = new System.Drawing.Size(173, 47);
            this.流道狀態確認_STP.StopWhenError = true;
            this.流道狀態確認_STP.TabIndex = 19;
            this.流道狀態確認_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CV狀態確認_STP_ProcessIn);
            // 
            // 流道判定有無Boat_Check
            // 
            this.流道判定有無Boat_Check.BackColor = System.Drawing.Color.Cyan;
            this.流道判定有無Boat_Check.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道判定有無Boat_Check.DoNotStopAtThisProcess = false;
            this.流道判定有無Boat_Check.ErrorDescription = "";
            this.流道判定有無Boat_Check.FalseProcess = this.流道_Y軸移動_Load定位點_STP;
            this.流道判定有無Boat_Check.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道判定有無Boat_Check.Location = new System.Drawing.Point(24, 277);
            this.流道判定有無Boat_Check.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道判定有無Boat_Check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道判定有無Boat_Check.Name = "流道判定有無Boat_Check";
            this.流道判定有無Boat_Check.Size = new System.Drawing.Size(173, 56);
            this.流道判定有無Boat_Check.TabIndex = 12;
            this.流道判定有無Boat_Check.TrueProcess = this.BoatUnload判定_Check;
            this.流道判定有無Boat_Check.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.CV判定有無Boat_Check_ConditionCheck);
            // 
            // 流道_Y軸移動_Load定位點_STP
            // 
            this.流道_Y軸移動_Load定位點_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道_Y軸移動_Load定位點_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道_Y軸移動_Load定位點_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道_Y軸移動_Load定位點_STP.DoNotStopAtThisProcess = false;
            this.流道_Y軸移動_Load定位點_STP.ErrorDescription = "";
            this.流道_Y軸移動_Load定位點_STP.ExceptionWaitTime = 100;
            this.流道_Y軸移動_Load定位點_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道_Y軸移動_Load定位點_STP.LastExecuteMs = ((long)(0));
            this.流道_Y軸移動_Load定位點_STP.Location = new System.Drawing.Point(12, 378);
            this.流道_Y軸移動_Load定位點_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道_Y軸移動_Load定位點_STP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道_Y軸移動_Load定位點_STP.Name = "流道_Y軸移動_Load定位點_STP";
            this.流道_Y軸移動_Load定位點_STP.NextProcess = this.發送_上位Load_Ready_Flg;
            this.流道_Y軸移動_Load定位點_STP.Size = new System.Drawing.Size(197, 47);
            this.流道_Y軸移動_Load定位點_STP.StopWhenError = true;
            this.流道_Y軸移動_Load定位點_STP.TabIndex = 4;
            this.流道_Y軸移動_Load定位點_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVYA_移動_Load定位點_STP_ProcessIn);
            // 
            // 發送_上位Load_Ready_Flg
            // 
            this.發送_上位Load_Ready_Flg.BackColor = System.Drawing.Color.Cyan;
            this.發送_上位Load_Ready_Flg.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.發送_上位Load_Ready_Flg.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.發送_上位Load_Ready_Flg.DoNotStopAtThisProcess = false;
            this.發送_上位Load_Ready_Flg.ErrorDescription = "";
            this.發送_上位Load_Ready_Flg.ExceptionWaitTime = 100;
            this.發送_上位Load_Ready_Flg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.發送_上位Load_Ready_Flg.LastExecuteMs = ((long)(0));
            this.發送_上位Load_Ready_Flg.Location = new System.Drawing.Point(20, 455);
            this.發送_上位Load_Ready_Flg.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.發送_上位Load_Ready_Flg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.發送_上位Load_Ready_Flg.Name = "發送_上位Load_Ready_Flg";
            this.發送_上位Load_Ready_Flg.NextProcess = this.取得_上位Load_OK_Flg;
            this.發送_上位Load_Ready_Flg.Size = new System.Drawing.Size(179, 47);
            this.發送_上位Load_Ready_Flg.StopWhenError = true;
            this.發送_上位Load_Ready_Flg.TabIndex = 13;
            this.發送_上位Load_Ready_Flg.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.發送_上位Load_Ready_Flg_ProcessIn);
            // 
            // 取得_上位Load_OK_Flg
            // 
            this.取得_上位Load_OK_Flg.BackColor = System.Drawing.Color.Cyan;
            this.取得_上位Load_OK_Flg.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.取得_上位Load_OK_Flg.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.取得_上位Load_OK_Flg.DoNotStopAtThisProcess = false;
            this.取得_上位Load_OK_Flg.ErrorDescription = "";
            this.取得_上位Load_OK_Flg.ExceptionWaitTime = 100;
            this.取得_上位Load_OK_Flg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.取得_上位Load_OK_Flg.LastExecuteMs = ((long)(0));
            this.取得_上位Load_OK_Flg.Location = new System.Drawing.Point(24, 533);
            this.取得_上位Load_OK_Flg.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.取得_上位Load_OK_Flg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.取得_上位Load_OK_Flg.Name = "取得_上位Load_OK_Flg";
            this.取得_上位Load_OK_Flg.NextProcess = this.取得_上位SMEMA_Flg;
            this.取得_上位Load_OK_Flg.Size = new System.Drawing.Size(173, 47);
            this.取得_上位Load_OK_Flg.StopWhenError = true;
            this.取得_上位Load_OK_Flg.TabIndex = 5;
            this.取得_上位Load_OK_Flg.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.取得_上位Load_OK_Flg_ProcessIn);
            // 
            // 取得_上位SMEMA_Flg
            // 
            this.取得_上位SMEMA_Flg.BackColor = System.Drawing.Color.Cyan;
            this.取得_上位SMEMA_Flg.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.取得_上位SMEMA_Flg.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.取得_上位SMEMA_Flg.DoNotStopAtThisProcess = false;
            this.取得_上位SMEMA_Flg.ErrorDescription = "";
            this.取得_上位SMEMA_Flg.ExceptionWaitTime = 100;
            this.取得_上位SMEMA_Flg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.取得_上位SMEMA_Flg.LastExecuteMs = ((long)(0));
            this.取得_上位SMEMA_Flg.Location = new System.Drawing.Point(24, 607);
            this.取得_上位SMEMA_Flg.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.取得_上位SMEMA_Flg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.取得_上位SMEMA_Flg.Name = "取得_上位SMEMA_Flg";
            this.取得_上位SMEMA_Flg.NextProcess = this.流道傳送軸_入料動作_SubLoop;
            this.取得_上位SMEMA_Flg.Size = new System.Drawing.Size(173, 47);
            this.取得_上位SMEMA_Flg.StopWhenError = true;
            this.取得_上位SMEMA_Flg.TabIndex = 6;
            this.取得_上位SMEMA_Flg.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.取得_上位SMEMA_Flg_ProcessIn);
            // 
            // 流道傳送軸_入料動作_SubLoop
            // 
            this.流道傳送軸_入料動作_SubLoop.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_入料動作_SubLoop.ChildProcess = this.流道傳送軸_入料動作_Start;
            this.流道傳送軸_入料動作_SubLoop.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_入料動作_SubLoop.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_入料動作_SubLoop.DoNotStopAtThisProcess = false;
            this.流道傳送軸_入料動作_SubLoop.ErrorDescription = "";
            this.流道傳送軸_入料動作_SubLoop.ExceptionWaitTime = 100;
            this.流道傳送軸_入料動作_SubLoop.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_入料動作_SubLoop.LastExecuteMs = ((long)(0));
            this.流道傳送軸_入料動作_SubLoop.Location = new System.Drawing.Point(24, 683);
            this.流道傳送軸_入料動作_SubLoop.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_入料動作_SubLoop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道傳送軸_入料動作_SubLoop.Name = "流道傳送軸_入料動作_SubLoop";
            this.流道傳送軸_入料動作_SubLoop.NextProcess = this.流道_Y軸移動_Detection定位點_STP;
            this.流道傳送軸_入料動作_SubLoop.Size = new System.Drawing.Size(173, 47);
            this.流道傳送軸_入料動作_SubLoop.StopWhenError = true;
            this.流道傳送軸_入料動作_SubLoop.TabIndex = 8;
            // 
            // 流道_Y軸移動_Detection定位點_STP
            // 
            this.流道_Y軸移動_Detection定位點_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道_Y軸移動_Detection定位點_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道_Y軸移動_Detection定位點_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道_Y軸移動_Detection定位點_STP.DoNotStopAtThisProcess = false;
            this.流道_Y軸移動_Detection定位點_STP.ErrorDescription = "";
            this.流道_Y軸移動_Detection定位點_STP.ExceptionWaitTime = 100;
            this.流道_Y軸移動_Detection定位點_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道_Y軸移動_Detection定位點_STP.LastExecuteMs = ((long)(0));
            this.流道_Y軸移動_Detection定位點_STP.Location = new System.Drawing.Point(253, 455);
            this.流道_Y軸移動_Detection定位點_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道_Y軸移動_Detection定位點_STP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道_Y軸移動_Detection定位點_STP.Name = "流道_Y軸移動_Detection定位點_STP";
            this.流道_Y軸移動_Detection定位點_STP.NextProcess = this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg;
            this.流道_Y軸移動_Detection定位點_STP.Size = new System.Drawing.Size(203, 47);
            this.流道_Y軸移動_Detection定位點_STP.StopWhenError = true;
            this.流道_Y軸移動_Detection定位點_STP.TabIndex = 11;
            this.流道_Y軸移動_Detection定位點_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVYA_移動_Detection定位點_STP_ProcessIn);
            // 
            // 發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg
            // 
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.BackColor = System.Drawing.Color.Cyan;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.DoNotStopAtThisProcess = false;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.ErrorDescription = "";
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.ExceptionWaitTime = 100;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.LastExecuteMs = ((long)(0));
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.Location = new System.Drawing.Point(253, 533);
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.Name = "發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg";
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.NextProcess = this.取得_流道_Y軸_Detection_OK_Flg;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.Size = new System.Drawing.Size(203, 47);
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.StopWhenError = true;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.TabIndex = 15;
            this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.發出_CVYADetection定位點_允許開始檢測_Ok_Flg_ProcessIn);
            // 
            // 取得_流道_Y軸_Detection_OK_Flg
            // 
            this.取得_流道_Y軸_Detection_OK_Flg.BackColor = System.Drawing.Color.Cyan;
            this.取得_流道_Y軸_Detection_OK_Flg.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.取得_流道_Y軸_Detection_OK_Flg.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.取得_流道_Y軸_Detection_OK_Flg.DoNotStopAtThisProcess = false;
            this.取得_流道_Y軸_Detection_OK_Flg.ErrorDescription = "";
            this.取得_流道_Y軸_Detection_OK_Flg.ExceptionWaitTime = 100;
            this.取得_流道_Y軸_Detection_OK_Flg.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.取得_流道_Y軸_Detection_OK_Flg.LastExecuteMs = ((long)(0));
            this.取得_流道_Y軸_Detection_OK_Flg.Location = new System.Drawing.Point(253, 607);
            this.取得_流道_Y軸_Detection_OK_Flg.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.取得_流道_Y軸_Detection_OK_Flg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.取得_流道_Y軸_Detection_OK_Flg.Name = "取得_流道_Y軸_Detection_OK_Flg";
            this.取得_流道_Y軸_Detection_OK_Flg.NextProcess = this.流道_Y軸移動_Unload定位點_STP;
            this.取得_流道_Y軸_Detection_OK_Flg.Size = new System.Drawing.Size(203, 47);
            this.取得_流道_Y軸_Detection_OK_Flg.StopWhenError = true;
            this.取得_流道_Y軸_Detection_OK_Flg.TabIndex = 16;
            this.取得_流道_Y軸_Detection_OK_Flg.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.取得_CVYADetection_OK_Flg_ProcessIn);
            // 
            // 流道_Y軸移動_Unload定位點_STP
            // 
            this.流道_Y軸移動_Unload定位點_STP.BackColor = System.Drawing.Color.Cyan;
            this.流道_Y軸移動_Unload定位點_STP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道_Y軸移動_Unload定位點_STP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道_Y軸移動_Unload定位點_STP.DoNotStopAtThisProcess = false;
            this.流道_Y軸移動_Unload定位點_STP.ErrorDescription = "";
            this.流道_Y軸移動_Unload定位點_STP.ExceptionWaitTime = 100;
            this.流道_Y軸移動_Unload定位點_STP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道_Y軸移動_Unload定位點_STP.LastExecuteMs = ((long)(0));
            this.流道_Y軸移動_Unload定位點_STP.Location = new System.Drawing.Point(518, 282);
            this.流道_Y軸移動_Unload定位點_STP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道_Y軸移動_Unload定位點_STP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道_Y軸移動_Unload定位點_STP.Name = "流道_Y軸移動_Unload定位點_STP";
            this.流道_Y軸移動_Unload定位點_STP.NextProcess = this.流道傳送軸_出料動作_SubLoop;
            this.流道_Y軸移動_Unload定位點_STP.Size = new System.Drawing.Size(190, 47);
            this.流道_Y軸移動_Unload定位點_STP.StopWhenError = true;
            this.流道_Y軸移動_Unload定位點_STP.TabIndex = 17;
            this.流道_Y軸移動_Unload定位點_STP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CVYA_移動_Unload定位點_STP_ProcessIn);
            // 
            // 流道傳送軸_出料動作_SubLoop
            // 
            this.流道傳送軸_出料動作_SubLoop.BackColor = System.Drawing.Color.Cyan;
            this.流道傳送軸_出料動作_SubLoop.ChildProcess = this.流道傳送軸_出料動作_Start;
            this.流道傳送軸_出料動作_SubLoop.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道傳送軸_出料動作_SubLoop.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道傳送軸_出料動作_SubLoop.DoNotStopAtThisProcess = false;
            this.流道傳送軸_出料動作_SubLoop.ErrorDescription = "";
            this.流道傳送軸_出料動作_SubLoop.ExceptionWaitTime = 100;
            this.流道傳送軸_出料動作_SubLoop.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道傳送軸_出料動作_SubLoop.LastExecuteMs = ((long)(0));
            this.流道傳送軸_出料動作_SubLoop.Location = new System.Drawing.Point(518, 202);
            this.流道傳送軸_出料動作_SubLoop.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道傳送軸_出料動作_SubLoop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道傳送軸_出料動作_SubLoop.Name = "流道傳送軸_出料動作_SubLoop";
            this.流道傳送軸_出料動作_SubLoop.NextProcess = this.流道狀態確認_STP;
            this.流道傳送軸_出料動作_SubLoop.Size = new System.Drawing.Size(190, 47);
            this.流道傳送軸_出料動作_SubLoop.StopWhenError = true;
            this.流道傳送軸_出料動作_SubLoop.TabIndex = 18;
            // 
            // BoatUnload判定_Check
            // 
            this.BoatUnload判定_Check.BackColor = System.Drawing.Color.Cyan;
            this.BoatUnload判定_Check.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.BoatUnload判定_Check.DoNotStopAtThisProcess = false;
            this.BoatUnload判定_Check.ErrorDescription = "";
            this.BoatUnload判定_Check.FalseProcess = this.流道_有Boat_SubLoop;
            this.BoatUnload判定_Check.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BoatUnload判定_Check.Location = new System.Drawing.Point(253, 277);
            this.BoatUnload判定_Check.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.BoatUnload判定_Check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BoatUnload判定_Check.Name = "BoatUnload判定_Check";
            this.BoatUnload判定_Check.Size = new System.Drawing.Size(203, 56);
            this.BoatUnload判定_Check.TabIndex = 14;
            this.BoatUnload判定_Check.TrueProcess = this.流道_Y軸移動_Unload定位點_STP;
            this.BoatUnload判定_Check.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.BoatUnload判定_Check_ConditionCheck);
            // 
            // 流道_有Boat_SubLoop
            // 
            this.流道_有Boat_SubLoop.BackColor = System.Drawing.Color.Cyan;
            this.流道_有Boat_SubLoop.ChildProcess = this.流道_含Boat_Start;
            this.流道_有Boat_SubLoop.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道_有Boat_SubLoop.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道_有Boat_SubLoop.DoNotStopAtThisProcess = false;
            this.流道_有Boat_SubLoop.ErrorDescription = "";
            this.流道_有Boat_SubLoop.ExceptionWaitTime = 100;
            this.流道_有Boat_SubLoop.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.流道_有Boat_SubLoop.LastExecuteMs = ((long)(0));
            this.流道_有Boat_SubLoop.Location = new System.Drawing.Point(253, 378);
            this.流道_有Boat_SubLoop.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道_有Boat_SubLoop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.流道_有Boat_SubLoop.Name = "流道_有Boat_SubLoop";
            this.流道_有Boat_SubLoop.NextProcess = this.流道_Y軸移動_Detection定位點_STP;
            this.流道_有Boat_SubLoop.Size = new System.Drawing.Size(203, 47);
            this.流道_有Boat_SubLoop.StopWhenError = true;
            this.流道_有Boat_SubLoop.TabIndex = 7;
            this.流道_有Boat_SubLoop.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.CV_含Boat_SubLoop_ProcessIn);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(201, 285);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 18);
            this.label1.TabIndex = 20;
            this.label1.Text = "True";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(114, 342);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "False";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(462, 285);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "True";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(359, 342);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "False";
            // 
            // Golden模式不做事
            // 
            this.Golden模式不做事.BackColor = System.Drawing.Color.Cyan;
            this.Golden模式不做事.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Golden模式不做事.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Golden模式不做事.DoNotStopAtThisProcess = false;
            this.Golden模式不做事.ErrorDescription = "";
            this.Golden模式不做事.ExceptionWaitTime = 100;
            this.Golden模式不做事.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Golden模式不做事.LastExecuteMs = ((long)(0));
            this.Golden模式不做事.Location = new System.Drawing.Point(296, 116);
            this.Golden模式不做事.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Golden模式不做事.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Golden模式不做事.Name = "Golden模式不做事";
            this.Golden模式不做事.Size = new System.Drawing.Size(125, 47);
            this.Golden模式不做事.StopWhenError = true;
            this.Golden模式不做事.TabIndex = 25;
            this.Golden模式不做事.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Golden模式不做事_ProcessIn);
            // 
            // 是否為Golden模式
            // 
            this.是否為Golden模式.BackColor = System.Drawing.Color.Cyan;
            this.是否為Golden模式.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否為Golden模式.DoNotStopAtThisProcess = false;
            this.是否為Golden模式.ErrorDescription = "";
            this.是否為Golden模式.FalseProcess = this.流道狀態確認_STP;
            this.是否為Golden模式.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.是否為Golden模式.Location = new System.Drawing.Point(24, 116);
            this.是否為Golden模式.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否為Golden模式.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.是否為Golden模式.Name = "是否為Golden模式";
            this.是否為Golden模式.Size = new System.Drawing.Size(173, 56);
            this.是否為Golden模式.TabIndex = 24;
            this.是否為Golden模式.TrueProcess = this.Golden模式不做事;
            this.是否為Golden模式.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否為Golden模式_ConditionCheck);
            // 
            // FlowCarrierModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Golden模式不做事);
            this.Controls.Add(this.是否為Golden模式);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.流道狀態確認_STP);
            this.Controls.Add(this.流道傳送軸_出料動作_SubLoop);
            this.Controls.Add(this.流道_Y軸移動_Unload定位點_STP);
            this.Controls.Add(this.取得_流道_Y軸_Detection_OK_Flg);
            this.Controls.Add(this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg);
            this.Controls.Add(this.BoatUnload判定_Check);
            this.Controls.Add(this.發送_上位Load_Ready_Flg);
            this.Controls.Add(this.流道判定有無Boat_Check);
            this.Controls.Add(this.流道_Y軸移動_Detection定位點_STP);
            this.Controls.Add(this.流道傳送軸_入料動作_SubLoop);
            this.Controls.Add(this.流道_有Boat_SubLoop);
            this.Controls.Add(this.取得_上位SMEMA_Flg);
            this.Controls.Add(this.取得_上位Load_OK_Flg);
            this.Controls.Add(this.流道_Y軸移動_Load定位點_STP);
            this.Controls.Add(this.FlowCarrier_Start);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FlowCarrierModule";
            this.Size = new System.Drawing.Size(1226, 737);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.FlowCarrier_Start, 0);
            this.Controls.SetChildIndex(this.流道_Y軸移動_Load定位點_STP, 0);
            this.Controls.SetChildIndex(this.取得_上位Load_OK_Flg, 0);
            this.Controls.SetChildIndex(this.取得_上位SMEMA_Flg, 0);
            this.Controls.SetChildIndex(this.流道_有Boat_SubLoop, 0);
            this.Controls.SetChildIndex(this.流道傳送軸_入料動作_SubLoop, 0);
            this.Controls.SetChildIndex(this.流道_Y軸移動_Detection定位點_STP, 0);
            this.Controls.SetChildIndex(this.流道判定有無Boat_Check, 0);
            this.Controls.SetChildIndex(this.發送_上位Load_Ready_Flg, 0);
            this.Controls.SetChildIndex(this.BoatUnload判定_Check, 0);
            this.Controls.SetChildIndex(this.發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg, 0);
            this.Controls.SetChildIndex(this.取得_流道_Y軸_Detection_OK_Flg, 0);
            this.Controls.SetChildIndex(this.流道_Y軸移動_Unload定位點_STP, 0);
            this.Controls.SetChildIndex(this.流道傳送軸_出料動作_SubLoop, 0);
            this.Controls.SetChildIndex(this.流道狀態確認_STP, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.currentProcessString, 0);
            this.Controls.SetChildIndex(this.是否為Golden模式, 0);
            this.Controls.SetChildIndex(this.Golden模式不做事, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.controlFlow1.ResumeLayout(false);
            this.controlFlow1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.controlFlow2.ResumeLayout(false);
            this.controlFlow2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.controlFlow3.ResumeLayout(false);
            this.controlFlow3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ControlFlow.ControlFlow controlFlow1;
        private System.Windows.Forms.TabPage tabPage2;
        private ControlFlow.ControlFlow controlFlow2;
        private System.Windows.Forms.TabPage tabPage3;
        private ControlFlow.ControlFlow controlFlow3;
        private ControlFlow.Controls.StartItem FlowCarrier_Start;
        private ControlFlow.Controls.ProcessItem 流道_Y軸移動_Load定位點_STP;
        private ControlFlow.Controls.ProcessItem 取得_上位Load_OK_Flg;
        private ControlFlow.Controls.ProcessItem 取得_上位SMEMA_Flg;
        private ControlFlow.Controls.ProcessItem 流道_有Boat_SubLoop;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_入料動作_SubLoop;
        private ControlFlow.Controls.ProcessItem 流道_Y軸移動_Detection定位點_STP;
        private ControlFlow.Controls.BranchItem 流道判定有無Boat_Check;
        private ControlFlow.Controls.ProcessItem 發送_上位Load_Ready_Flg;
        private ControlFlow.Controls.BranchItem BoatUnload判定_Check;
        private ControlFlow.Controls.ProcessItem 發出_流道_Y軸_Detection定位點_允許開始檢測_Ok_Flg;
        private ControlFlow.Controls.ProcessItem 取得_流道_Y軸_Detection_OK_Flg;
        private ControlFlow.Controls.ProcessItem 流道_Y軸移動_Unload定位點_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_出料動作_SubLoop;
        private ControlFlow.Controls.ProcessItem 流道狀態確認_STP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_停止汽缸_Up_STP;
        private ControlFlow.Controls.StartItem 流道傳送軸_入料動作_Start;
        private ControlFlow.Controls.ProcessItem 等待_流道傳送軸_Load檢知逾時_Err;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_入料動作_OK_END;
        private ControlFlow.Controls.ProcessItem 等待_流道傳送軸_到位檢知_STP;
        private ControlFlow.Controls.ProcessItem 等待_流道傳送軸_減速檢知_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_啟動_STP;
        private ControlFlow.Controls.ProcessItem 等待_流道傳送軸_Load檢知_STP;
        private ControlFlow.Controls.ProcessItem 等待_流道傳送軸_減速檢知逾時_Err;
        private ControlFlow.Controls.ProcessItem 等待_流道傳送軸_到位檢知逾時_Err;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_停止汽缸_上升_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_反轉脫離動作_STP;
        private ControlFlow.Controls.ProcessItem 流道_含Boat_OK_END;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_正轉靠位動作_STP;
        private ControlFlow.Controls.BranchItem 流道傳送軸_到位檢知_Check;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_停止汽缸下降_STP;
        private ControlFlow.Controls.StartItem 流道_含Boat_Start;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private ControlFlow.Controls.ProcessItem 發送_下位SMEMA_Flg;
        private ControlFlow.Controls.StartItem 流道傳送軸_出料動作_Start;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_出料動作_Err;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_出料動作_OK;
        private ControlFlow.Controls.ProcessItem 流道傳送軸啟動出料_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_停止汽缸_下降_STP;
        private ControlFlow.Controls.ProcessItem 等待_下位SMEMA_Flg;
        private ControlFlow.Controls.ProcessItem 入料_流道傳送軸_頂升汽缸開啟_STP;
        private ControlFlow.Controls.ProcessItem 入料_流道傳送軸_真空開啟_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_到位檢知脫離_STP;
        private ControlFlow.Controls.ProcessItem 有料_流道傳送軸_真空開啟_STP;
        private ControlFlow.Controls.ProcessItem 有料_流道傳送軸_頂升汽缸開啟_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_定位汽缸_關閉_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_真空_關閉_STP;
        private ControlFlow.Controls.ProcessItem 入料_流道傳送軸_真空開啟逾時_Err;
        private ControlFlow.Controls.ProcessItem 入料_流道傳送軸_頂升汽缸開啟逾時_Err;
        private ControlFlow.Controls.ProcessItem 有料_流道傳送軸_頂升汽缸關閉_STP;
        private ControlFlow.Controls.ProcessItem 流道傳送軸_Guide動作_STP;
        private ControlFlow.Controls.ProcessItem 有料_Guide動作_STP;
        private ControlFlow.Controls.ProcessItem Golden模式不做事;
        private ControlFlow.Controls.BranchItem 是否為Golden模式;
    }
}
