namespace TA2000Modules
{
    partial class InspectionModule
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
            this.檢測流程開始 = new ControlFlow.Controls.StartItem();
            this.視覺是否bypass = new ControlFlow.Controls.BranchItem();
            this.WCF通訊上線 = new ControlFlow.Controls.ProcessItem();
            this.灰卡光衰流程 = new ControlFlow.Controls.ProcessItem();
            this.所有軸移動到Standby位置 = new ControlFlow.Controls.ProcessItem();
            this.讀取檢測序列資訊IR_FN_NL = new ControlFlow.Controls.ProcessItem();
            this.等待載盤進入 = new ControlFlow.Controls.ProcessItem();
            this.是否Barcode教讀 = new ControlFlow.Controls.BranchItem();
            this.是否拍攝Barcode = new ControlFlow.Controls.BranchItem();
            this.是否教讀 = new ControlFlow.Controls.BranchItem();
            this.是否抽檢 = new ControlFlow.Controls.BranchItem();
            this.建立SG_Quene = new ControlFlow.Controls.ProcessItem();
            this.建立Grab_Quene = new ControlFlow.Controls.ProcessItem();
            this.讀取SG資訊 = new ControlFlow.Controls.ProcessItem();
            this.是否為大產品 = new ControlFlow.Controls.BranchItem();
            this.小產品拍攝子流程C = new ControlFlow.Controls.ProcessItem();
            this.小產品拍攝開始 = new ControlFlow.Controls.StartItem();
            this.是否飛拍 = new ControlFlow.Controls.BranchItem();
            this.移動點拍起始位置 = new ControlFlow.Controls.ProcessItem();
            this.下IP = new ControlFlow.Controls.ProcessItem();
            this.移至飛拍起始位置 = new ControlFlow.Controls.ProcessItem();
            this.建立TriggerTable = new ControlFlow.Controls.ProcessItem();
            this.移至飛拍結束位置 = new ControlFlow.Controls.ProcessItem();
            this.此回合是否拍攝完畢 = new ControlFlow.Controls.BranchItem();
            this.MainFlow_Node1 = new ControlFlow.Controls.ProcessItem();
            this.確認此回合影像是否收齊 = new ControlFlow.Controls.ProcessItem();
            this.是否全部產品拍攝完成 = new ControlFlow.Controls.BranchItem();
            this.等待所有結果產出 = new ControlFlow.Controls.ProcessItem();
            this.確認產品結果是否OK = new ControlFlow.Controls.ProcessItem();
            this.是否FailAlarm確認 = new ControlFlow.Controls.BranchItem();
            this.是否重測 = new ControlFlow.Controls.BranchItem();
            this.通知流道模組 = new ControlFlow.Controls.ProcessItem();
            this.建立Single_SG_Quene = new ControlFlow.Controls.ProcessItem();
            this.讀取IR1資訊 = new ControlFlow.Controls.ProcessItem();
            this.FailAlarm子流程C = new ControlFlow.Controls.ProcessItem();
            this.FailAlarm開始 = new ControlFlow.Controls.StartItem();
            this.Boat盤限制解除 = new ControlFlow.Controls.ProcessItem();
            this.流道軸移動至人員確認位置 = new ControlFlow.Controls.ProcessItem();
            this.人員確認 = new ControlFlow.Controls.ProcessItem();
            this.流道偵測產品是否存在 = new ControlFlow.Controls.ProcessItem();
            this.流道軸移動回檢測位置 = new ControlFlow.Controls.ProcessItem();
            this.Boat盤限制開啟 = new ControlFlow.Controls.ProcessItem();
            this.雷射測高子流程 = new ControlFlow.Controls.ProcessItem();
            this.雷射測高流程開始 = new ControlFlow.Controls.StartItem();
            this.是否開啟雷射測高 = new ControlFlow.Controls.BranchItem();
            this.雷射段差結束 = new ControlFlow.Controls.ProcessItem();
            this.是否所有Map區域都掃完 = new ControlFlow.Controls.BranchItem();
            this.移至Map區域開始位置 = new ControlFlow.Controls.ProcessItem();
            this.雷射開啟 = new ControlFlow.Controls.ProcessItem();
            this.移至Map區域結束位置 = new ControlFlow.Controls.ProcessItem();
            this.雷射關閉 = new ControlFlow.Controls.ProcessItem();
            this.大產品拍攝子流程C = new ControlFlow.Controls.ProcessItem();
            this.大產品拍攝開始 = new ControlFlow.Controls.StartItem();
            this.是否組圖 = new ControlFlow.Controls.BranchItem();
            this.分區下SG = new ControlFlow.Controls.ProcessItem();
            this.移動指定位置 = new ControlFlow.Controls.ProcessItem();
            this.下IP拍照2 = new ControlFlow.Controls.ProcessItem();
            this.是否所有影像拍完 = new ControlFlow.Controls.BranchItem();
            this.是否所有Map都拍完 = new ControlFlow.Controls.BranchItem();
            this.大產品拍攝完畢 = new ControlFlow.Controls.ProcessItem();
            this.Mosaic下SG = new ControlFlow.Controls.ProcessItem();
            this.移至拍攝起始位置 = new ControlFlow.Controls.ProcessItem();
            this.下IP拍照 = new ControlFlow.Controls.ProcessItem();
            this.是否產品影像拍攝完成 = new ControlFlow.Controls.BranchItem();
            this.教讀 = new ControlFlow.Controls.ProcessItem();
            this.Barcode拍攝子流程C = new ControlFlow.Controls.ProcessItem();
            this.Barcode拍攝開始 = new ControlFlow.Controls.StartItem();
            this.移至Barcode位置 = new ControlFlow.Controls.ProcessItem();
            this.是否使用相機拍攝 = new ControlFlow.Controls.BranchItem();
            this.Barcode機拍攝Barcode = new ControlFlow.Controls.ProcessItem();
            this.是否讀取成功 = new ControlFlow.Controls.BranchItem();
            this.手動輸入Barcode = new ControlFlow.Controls.ProcessItem();
            this.儲存Barcode資訊 = new ControlFlow.Controls.ProcessItem();
            this.相機拍攝Barcode = new ControlFlow.Controls.ProcessItem();
            this.Barcode教讀 = new ControlFlow.Controls.ProcessItem();
            this.視覺Bypass之Barcode拍攝子流程C = new ControlFlow.Controls.ProcessItem();
            this.視覺Bypass之Barcode拍攝開始 = new ControlFlow.Controls.StartItem();
            this.等待載盤進入_VisionBypass = new ControlFlow.Controls.ProcessItem();
            this.是否Barcode教讀_VisionBypass = new ControlFlow.Controls.BranchItem();
            this.是否拍攝Barcode_VisionBypass = new ControlFlow.Controls.BranchItem();
            this.通知流道模組_VisionBypass = new ControlFlow.Controls.ProcessItem();
            this.Barcode拍攝子流程C_VisionBypass = new ControlFlow.Controls.ProcessItem();
            this.Barcode教讀_VisionBypass = new ControlFlow.Controls.ProcessItem();
            this.拍攝點位收集 = new ControlFlow.Controls.ProcessItem();
            this.點位移動取像 = new ControlFlow.Controls.ProcessItem();
            this.Block偵測 = new ControlFlow.Controls.ProcessItem();
            this.雷射高度偵測 = new ControlFlow.Controls.ProcessItem();
            this.雷射測高開始 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.controlFlow1 = new ControlFlow.ControlFlow();
            this.SmallProduct_No1 = new System.Windows.Forms.Label();
            this.SmallProduct_Yes1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.controlFlow2 = new ControlFlow.ControlFlow();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.controlFlow3 = new ControlFlow.ControlFlow();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.controlFlow4 = new ControlFlow.ControlFlow();
            this.VB_BarcodeCatch_No2 = new System.Windows.Forms.Label();
            this.VB_BarcodeCatch_Yes2 = new System.Windows.Forms.Label();
            this.VB_BarcodeCatch_No1 = new System.Windows.Forms.Label();
            this.VB_BarcodeCatch_Yes1 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.controlFlow5 = new ControlFlow.ControlFlow();
            this.BarcodeCatch_No2 = new System.Windows.Forms.Label();
            this.BarcodeCatch_Yes2 = new System.Windows.Forms.Label();
            this.BarcodeCatch_No1 = new System.Windows.Forms.Label();
            this.BarcodeCatch_Yes1 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.controlFlow6 = new ControlFlow.ControlFlow();
            this.label1 = new System.Windows.Forms.Label();
            this.No1 = new System.Windows.Forms.Label();
            this.Yes1 = new System.Windows.Forms.Label();
            this.No2 = new System.Windows.Forms.Label();
            this.Yes2 = new System.Windows.Forms.Label();
            this.No5 = new System.Windows.Forms.Label();
            this.Yes5 = new System.Windows.Forms.Label();
            this.No4 = new System.Windows.Forms.Label();
            this.Yes4 = new System.Windows.Forms.Label();
            this.No3 = new System.Windows.Forms.Label();
            this.Yes3 = new System.Windows.Forms.Label();
            this.No7 = new System.Windows.Forms.Label();
            this.Yes7 = new System.Windows.Forms.Label();
            this.No8 = new System.Windows.Forms.Label();
            this.Yes8 = new System.Windows.Forms.Label();
            this.Yes9 = new System.Windows.Forms.Label();
            this.No9 = new System.Windows.Forms.Label();
            this.Yes10 = new System.Windows.Forms.Label();
            this.No10 = new System.Windows.Forms.Label();
            this.雷射測高開始.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.controlFlow1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.controlFlow2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.controlFlow3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.controlFlow4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.controlFlow5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.controlFlow6.SuspendLayout();
            this.SuspendLayout();
            // 
            // 檢測流程開始
            // 
            this.檢測流程開始.BackColor = System.Drawing.Color.Chocolate;
            this.檢測流程開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.檢測流程開始.DoNotStopAtThisProcess = false;
            this.檢測流程開始.ErrorDescription = "";
            this.檢測流程開始.ExceptionWaitTime = 100;
            this.檢測流程開始.LastExecuteMs = ((long)(0));
            this.檢測流程開始.Location = new System.Drawing.Point(36, 47);
            this.檢測流程開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.檢測流程開始.Name = "檢測流程開始";
            this.檢測流程開始.NextProcess = this.視覺是否bypass;
            this.檢測流程開始.Size = new System.Drawing.Size(100, 40);
            this.檢測流程開始.StopWhenError = true;
            this.檢測流程開始.TabIndex = 1;
            // 
            // 視覺是否bypass
            // 
            this.視覺是否bypass.BackColor = System.Drawing.Color.Cyan;
            this.視覺是否bypass.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.視覺是否bypass.DoNotStopAtThisProcess = false;
            this.視覺是否bypass.ErrorDescription = "";
            this.視覺是否bypass.FalseProcess = this.WCF通訊上線;
            this.視覺是否bypass.Location = new System.Drawing.Point(29, 112);
            this.視覺是否bypass.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.視覺是否bypass.Name = "視覺是否bypass";
            this.視覺是否bypass.Size = new System.Drawing.Size(115, 57);
            this.視覺是否bypass.TabIndex = 2;
            this.視覺是否bypass.TrueProcess = this.視覺Bypass之Barcode拍攝子流程C;
            this.視覺是否bypass.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.視覺是否bypass_ConditionCheck);
            // 
            // WCF通訊上線
            // 
            this.WCF通訊上線.BackColor = System.Drawing.Color.Cyan;
            this.WCF通訊上線.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.WCF通訊上線.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.WCF通訊上線.DoNotStopAtThisProcess = false;
            this.WCF通訊上線.ErrorDescription = "";
            this.WCF通訊上線.ExceptionWaitTime = 100;
            this.WCF通訊上線.LastExecuteMs = ((long)(0));
            this.WCF通訊上線.Location = new System.Drawing.Point(36, 193);
            this.WCF通訊上線.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.WCF通訊上線.Name = "WCF通訊上線";
            this.WCF通訊上線.NextProcess = this.灰卡光衰流程;
            this.WCF通訊上線.Size = new System.Drawing.Size(100, 40);
            this.WCF通訊上線.StopWhenError = true;
            this.WCF通訊上線.TabIndex = 4;
            this.WCF通訊上線.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.WCF通訊上線_ProcessIn);
            // 
            // 灰卡光衰流程
            // 
            this.灰卡光衰流程.BackColor = System.Drawing.Color.Cyan;
            this.灰卡光衰流程.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.灰卡光衰流程.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.灰卡光衰流程.DoNotStopAtThisProcess = false;
            this.灰卡光衰流程.ErrorDescription = "";
            this.灰卡光衰流程.ExceptionWaitTime = 100;
            this.灰卡光衰流程.LastExecuteMs = ((long)(0));
            this.灰卡光衰流程.Location = new System.Drawing.Point(36, 248);
            this.灰卡光衰流程.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.灰卡光衰流程.Name = "灰卡光衰流程";
            this.灰卡光衰流程.NextProcess = this.所有軸移動到Standby位置;
            this.灰卡光衰流程.Size = new System.Drawing.Size(100, 40);
            this.灰卡光衰流程.StopWhenError = true;
            this.灰卡光衰流程.TabIndex = 74;
            this.灰卡光衰流程.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.灰卡光衰流程_ProcessIn);
            // 
            // 所有軸移動到Standby位置
            // 
            this.所有軸移動到Standby位置.BackColor = System.Drawing.Color.Cyan;
            this.所有軸移動到Standby位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.所有軸移動到Standby位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.所有軸移動到Standby位置.DoNotStopAtThisProcess = false;
            this.所有軸移動到Standby位置.ErrorDescription = "";
            this.所有軸移動到Standby位置.ExceptionWaitTime = 100;
            this.所有軸移動到Standby位置.LastExecuteMs = ((long)(0));
            this.所有軸移動到Standby位置.Location = new System.Drawing.Point(36, 307);
            this.所有軸移動到Standby位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.所有軸移動到Standby位置.Name = "所有軸移動到Standby位置";
            this.所有軸移動到Standby位置.NextProcess = this.讀取檢測序列資訊IR_FN_NL;
            this.所有軸移動到Standby位置.Size = new System.Drawing.Size(100, 40);
            this.所有軸移動到Standby位置.StopWhenError = true;
            this.所有軸移動到Standby位置.TabIndex = 5;
            this.所有軸移動到Standby位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.所有軸移動到Standby位置_ProcessIn);
            // 
            // 讀取檢測序列資訊IR_FN_NL
            // 
            this.讀取檢測序列資訊IR_FN_NL.BackColor = System.Drawing.Color.Cyan;
            this.讀取檢測序列資訊IR_FN_NL.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.讀取檢測序列資訊IR_FN_NL.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.讀取檢測序列資訊IR_FN_NL.DoNotStopAtThisProcess = false;
            this.讀取檢測序列資訊IR_FN_NL.ErrorDescription = "";
            this.讀取檢測序列資訊IR_FN_NL.ExceptionWaitTime = 100;
            this.讀取檢測序列資訊IR_FN_NL.LastExecuteMs = ((long)(0));
            this.讀取檢測序列資訊IR_FN_NL.Location = new System.Drawing.Point(36, 377);
            this.讀取檢測序列資訊IR_FN_NL.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.讀取檢測序列資訊IR_FN_NL.Name = "讀取檢測序列資訊IR_FN_NL";
            this.讀取檢測序列資訊IR_FN_NL.NextProcess = this.等待載盤進入;
            this.讀取檢測序列資訊IR_FN_NL.Size = new System.Drawing.Size(100, 40);
            this.讀取檢測序列資訊IR_FN_NL.StopWhenError = true;
            this.讀取檢測序列資訊IR_FN_NL.TabIndex = 6;
            this.讀取檢測序列資訊IR_FN_NL.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.讀取檢測序列資訊IR_FN_NL_ProcessIn);
            // 
            // 等待載盤進入
            // 
            this.等待載盤進入.BackColor = System.Drawing.Color.Cyan;
            this.等待載盤進入.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待載盤進入.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待載盤進入.DoNotStopAtThisProcess = false;
            this.等待載盤進入.ErrorDescription = "";
            this.等待載盤進入.ExceptionWaitTime = 100;
            this.等待載盤進入.LastExecuteMs = ((long)(0));
            this.等待載盤進入.Location = new System.Drawing.Point(36, 451);
            this.等待載盤進入.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待載盤進入.Margin = new System.Windows.Forms.Padding(4);
            this.等待載盤進入.Name = "等待載盤進入";
            this.等待載盤進入.NextProcess = this.是否Barcode教讀;
            this.等待載盤進入.Size = new System.Drawing.Size(100, 40);
            this.等待載盤進入.StopWhenError = true;
            this.等待載盤進入.TabIndex = 7;
            this.等待載盤進入.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待載盤進入_ProcessIn);
            // 
            // 是否Barcode教讀
            // 
            this.是否Barcode教讀.BackColor = System.Drawing.Color.Cyan;
            this.是否Barcode教讀.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否Barcode教讀.DoNotStopAtThisProcess = false;
            this.是否Barcode教讀.ErrorDescription = "";
            this.是否Barcode教讀.FalseProcess = this.是否拍攝Barcode;
            this.是否Barcode教讀.Location = new System.Drawing.Point(29, 530);
            this.是否Barcode教讀.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否Barcode教讀.Name = "是否Barcode教讀";
            this.是否Barcode教讀.Size = new System.Drawing.Size(115, 50);
            this.是否Barcode教讀.TabIndex = 60;
            this.是否Barcode教讀.TrueProcess = this.Barcode教讀;
            this.是否Barcode教讀.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否Barcode教讀_ConditionCheck);
            // 
            // 是否拍攝Barcode
            // 
            this.是否拍攝Barcode.BackColor = System.Drawing.Color.Cyan;
            this.是否拍攝Barcode.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否拍攝Barcode.DoNotStopAtThisProcess = false;
            this.是否拍攝Barcode.ErrorDescription = "";
            this.是否拍攝Barcode.FalseProcess = this.是否教讀;
            this.是否拍攝Barcode.Location = new System.Drawing.Point(37, 658);
            this.是否拍攝Barcode.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否拍攝Barcode.Name = "是否拍攝Barcode";
            this.是否拍攝Barcode.Size = new System.Drawing.Size(99, 47);
            this.是否拍攝Barcode.TabIndex = 62;
            this.是否拍攝Barcode.TrueProcess = this.Barcode拍攝子流程C;
            this.是否拍攝Barcode.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否拍攝Barcode_ConditionCheck);
            // 
            // 是否教讀
            // 
            this.是否教讀.BackColor = System.Drawing.Color.Cyan;
            this.是否教讀.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否教讀.DoNotStopAtThisProcess = false;
            this.是否教讀.ErrorDescription = "";
            this.是否教讀.FalseProcess = this.是否抽檢;
            this.是否教讀.Location = new System.Drawing.Point(215, 672);
            this.是否教讀.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否教讀.Margin = new System.Windows.Forms.Padding(4);
            this.是否教讀.Name = "是否教讀";
            this.是否教讀.Size = new System.Drawing.Size(97, 54);
            this.是否教讀.TabIndex = 8;
            this.是否教讀.TrueProcess = this.教讀;
            this.是否教讀.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否教讀_ConditionCheck);
            // 
            // 是否抽檢
            // 
            this.是否抽檢.BackColor = System.Drawing.Color.Cyan;
            this.是否抽檢.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否抽檢.DoNotStopAtThisProcess = false;
            this.是否抽檢.ErrorDescription = "";
            this.是否抽檢.FalseProcess = this.建立SG_Quene;
            this.是否抽檢.Location = new System.Drawing.Point(268, 532);
            this.是否抽檢.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否抽檢.Margin = new System.Windows.Forms.Padding(4);
            this.是否抽檢.Name = "是否抽檢";
            this.是否抽檢.Size = new System.Drawing.Size(91, 49);
            this.是否抽檢.TabIndex = 10;
            this.是否抽檢.TrueProcess = this.建立Single_SG_Quene;
            this.是否抽檢.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否抽檢_ConditionCheck);
            // 
            // 建立SG_Quene
            // 
            this.建立SG_Quene.BackColor = System.Drawing.Color.Cyan;
            this.建立SG_Quene.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.建立SG_Quene.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.建立SG_Quene.DoNotStopAtThisProcess = false;
            this.建立SG_Quene.ErrorDescription = "";
            this.建立SG_Quene.ExceptionWaitTime = 100;
            this.建立SG_Quene.LastExecuteMs = ((long)(0));
            this.建立SG_Quene.Location = new System.Drawing.Point(263, 438);
            this.建立SG_Quene.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.建立SG_Quene.Margin = new System.Windows.Forms.Padding(4);
            this.建立SG_Quene.Name = "建立SG_Quene";
            this.建立SG_Quene.NextProcess = this.建立Grab_Quene;
            this.建立SG_Quene.Size = new System.Drawing.Size(100, 40);
            this.建立SG_Quene.StopWhenError = true;
            this.建立SG_Quene.TabIndex = 15;
            this.建立SG_Quene.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.建立SG_Quene_ProcessIn);
            // 
            // 建立Grab_Quene
            // 
            this.建立Grab_Quene.BackColor = System.Drawing.Color.Cyan;
            this.建立Grab_Quene.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.建立Grab_Quene.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.建立Grab_Quene.DoNotStopAtThisProcess = false;
            this.建立Grab_Quene.ErrorDescription = "";
            this.建立Grab_Quene.ExceptionWaitTime = 100;
            this.建立Grab_Quene.LastExecuteMs = ((long)(0));
            this.建立Grab_Quene.Location = new System.Drawing.Point(316, 333);
            this.建立Grab_Quene.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.建立Grab_Quene.Margin = new System.Windows.Forms.Padding(4);
            this.建立Grab_Quene.Name = "建立Grab_Quene";
            this.建立Grab_Quene.NextProcess = this.讀取SG資訊;
            this.建立Grab_Quene.Size = new System.Drawing.Size(100, 40);
            this.建立Grab_Quene.StopWhenError = true;
            this.建立Grab_Quene.TabIndex = 17;
            this.建立Grab_Quene.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.建立Grab_Quene_ProcessIn);
            // 
            // 讀取SG資訊
            // 
            this.讀取SG資訊.BackColor = System.Drawing.Color.Cyan;
            this.讀取SG資訊.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.讀取SG資訊.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.讀取SG資訊.DoNotStopAtThisProcess = false;
            this.讀取SG資訊.ErrorDescription = "";
            this.讀取SG資訊.ExceptionWaitTime = 100;
            this.讀取SG資訊.LastExecuteMs = ((long)(0));
            this.讀取SG資訊.Location = new System.Drawing.Point(316, 257);
            this.讀取SG資訊.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.讀取SG資訊.Margin = new System.Windows.Forms.Padding(4);
            this.讀取SG資訊.Name = "讀取SG資訊";
            this.讀取SG資訊.NextProcess = this.是否為大產品;
            this.讀取SG資訊.Size = new System.Drawing.Size(100, 40);
            this.讀取SG資訊.StopWhenError = true;
            this.讀取SG資訊.TabIndex = 18;
            this.讀取SG資訊.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.讀取SG資訊_ProcessIn);
            // 
            // 是否為大產品
            // 
            this.是否為大產品.BackColor = System.Drawing.Color.Cyan;
            this.是否為大產品.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否為大產品.DoNotStopAtThisProcess = true;
            this.是否為大產品.ErrorDescription = "";
            this.是否為大產品.FalseProcess = this.小產品拍攝子流程C;
            this.是否為大產品.Location = new System.Drawing.Point(317, 100);
            this.是否為大產品.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否為大產品.Margin = new System.Windows.Forms.Padding(4);
            this.是否為大產品.Name = "是否為大產品";
            this.是否為大產品.Size = new System.Drawing.Size(99, 53);
            this.是否為大產品.TabIndex = 19;
            this.是否為大產品.TrueProcess = this.雷射測高子流程;
            this.是否為大產品.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否為大產品_ConditionCheck);
            // 
            // 小產品拍攝子流程C
            // 
            this.小產品拍攝子流程C.BackColor = System.Drawing.Color.Cyan;
            this.小產品拍攝子流程C.ChildProcess = this.小產品拍攝開始;
            this.小產品拍攝子流程C.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.小產品拍攝子流程C.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.小產品拍攝子流程C.DoNotStopAtThisProcess = true;
            this.小產品拍攝子流程C.ErrorDescription = "";
            this.小產品拍攝子流程C.ExceptionWaitTime = 100;
            this.小產品拍攝子流程C.LastExecuteMs = ((long)(0));
            this.小產品拍攝子流程C.Location = new System.Drawing.Point(504, 119);
            this.小產品拍攝子流程C.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.小產品拍攝子流程C.Margin = new System.Windows.Forms.Padding(4);
            this.小產品拍攝子流程C.Name = "小產品拍攝子流程C";
            this.小產品拍攝子流程C.NextProcess = this.此回合是否拍攝完畢;
            this.小產品拍攝子流程C.Size = new System.Drawing.Size(117, 40);
            this.小產品拍攝子流程C.StopWhenError = true;
            this.小產品拍攝子流程C.TabIndex = 21;
            this.小產品拍攝子流程C.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.小產品拍攝子流程C_ProcessIn);
            // 
            // 小產品拍攝開始
            // 
            this.小產品拍攝開始.BackColor = System.Drawing.Color.Chocolate;
            this.小產品拍攝開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.小產品拍攝開始.DoNotStopAtThisProcess = true;
            this.小產品拍攝開始.ErrorDescription = "";
            this.小產品拍攝開始.ExceptionWaitTime = 100;
            this.小產品拍攝開始.LastExecuteMs = ((long)(0));
            this.小產品拍攝開始.Location = new System.Drawing.Point(123, 137);
            this.小產品拍攝開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.小產品拍攝開始.Margin = new System.Windows.Forms.Padding(4);
            this.小產品拍攝開始.Name = "小產品拍攝開始";
            this.小產品拍攝開始.NextProcess = this.是否飛拍;
            this.小產品拍攝開始.Size = new System.Drawing.Size(100, 40);
            this.小產品拍攝開始.StopWhenError = true;
            this.小產品拍攝開始.TabIndex = 32;
            // 
            // 是否飛拍
            // 
            this.是否飛拍.BackColor = System.Drawing.Color.Cyan;
            this.是否飛拍.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否飛拍.DoNotStopAtThisProcess = true;
            this.是否飛拍.ErrorDescription = "";
            this.是否飛拍.FalseProcess = this.移動點拍起始位置;
            this.是否飛拍.Location = new System.Drawing.Point(106, 212);
            this.是否飛拍.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否飛拍.Margin = new System.Windows.Forms.Padding(4);
            this.是否飛拍.Name = "是否飛拍";
            this.是否飛拍.Size = new System.Drawing.Size(135, 56);
            this.是否飛拍.TabIndex = 33;
            this.是否飛拍.TrueProcess = this.移至飛拍起始位置;
            this.是否飛拍.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否飛拍_ConditionCheck);
            // 
            // 移動點拍起始位置
            // 
            this.移動點拍起始位置.BackColor = System.Drawing.Color.Cyan;
            this.移動點拍起始位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移動點拍起始位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移動點拍起始位置.DoNotStopAtThisProcess = true;
            this.移動點拍起始位置.ErrorDescription = "";
            this.移動點拍起始位置.ExceptionWaitTime = 100;
            this.移動點拍起始位置.LastExecuteMs = ((long)(0));
            this.移動點拍起始位置.Location = new System.Drawing.Point(124, 308);
            this.移動點拍起始位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移動點拍起始位置.Margin = new System.Windows.Forms.Padding(4);
            this.移動點拍起始位置.Name = "移動點拍起始位置";
            this.移動點拍起始位置.NextProcess = this.下IP;
            this.移動點拍起始位置.Size = new System.Drawing.Size(100, 40);
            this.移動點拍起始位置.StopWhenError = true;
            this.移動點拍起始位置.TabIndex = 34;
            this.移動點拍起始位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移動點拍起始位置_ProcessIn);
            // 
            // 下IP
            // 
            this.下IP.BackColor = System.Drawing.Color.Cyan;
            this.下IP.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.下IP.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.下IP.DoNotStopAtThisProcess = true;
            this.下IP.ErrorDescription = "";
            this.下IP.ExceptionWaitTime = 100;
            this.下IP.LastExecuteMs = ((long)(0));
            this.下IP.Location = new System.Drawing.Point(124, 396);
            this.下IP.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.下IP.Margin = new System.Windows.Forms.Padding(4);
            this.下IP.Name = "下IP";
            this.下IP.Size = new System.Drawing.Size(100, 40);
            this.下IP.StopWhenError = true;
            this.下IP.TabIndex = 35;
            this.下IP.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.下IP_ProcessIn);
            // 
            // 移至飛拍起始位置
            // 
            this.移至飛拍起始位置.BackColor = System.Drawing.Color.Cyan;
            this.移至飛拍起始位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移至飛拍起始位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移至飛拍起始位置.DoNotStopAtThisProcess = true;
            this.移至飛拍起始位置.ErrorDescription = "";
            this.移至飛拍起始位置.ExceptionWaitTime = 100;
            this.移至飛拍起始位置.LastExecuteMs = ((long)(0));
            this.移至飛拍起始位置.Location = new System.Drawing.Point(297, 220);
            this.移至飛拍起始位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移至飛拍起始位置.Margin = new System.Windows.Forms.Padding(4);
            this.移至飛拍起始位置.Name = "移至飛拍起始位置";
            this.移至飛拍起始位置.NextProcess = this.建立TriggerTable;
            this.移至飛拍起始位置.Size = new System.Drawing.Size(100, 40);
            this.移至飛拍起始位置.StopWhenError = true;
            this.移至飛拍起始位置.TabIndex = 36;
            this.移至飛拍起始位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移至飛拍起始位置_ProcessIn);
            // 
            // 建立TriggerTable
            // 
            this.建立TriggerTable.BackColor = System.Drawing.Color.Cyan;
            this.建立TriggerTable.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.建立TriggerTable.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.建立TriggerTable.DoNotStopAtThisProcess = true;
            this.建立TriggerTable.ErrorDescription = "";
            this.建立TriggerTable.ExceptionWaitTime = 100;
            this.建立TriggerTable.LastExecuteMs = ((long)(0));
            this.建立TriggerTable.Location = new System.Drawing.Point(297, 308);
            this.建立TriggerTable.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.建立TriggerTable.Margin = new System.Windows.Forms.Padding(4);
            this.建立TriggerTable.Name = "建立TriggerTable";
            this.建立TriggerTable.NextProcess = this.移至飛拍結束位置;
            this.建立TriggerTable.Size = new System.Drawing.Size(100, 40);
            this.建立TriggerTable.StopWhenError = true;
            this.建立TriggerTable.TabIndex = 37;
            this.建立TriggerTable.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.建立TriggerTable_ProcessIn);
            // 
            // 移至飛拍結束位置
            // 
            this.移至飛拍結束位置.BackColor = System.Drawing.Color.Cyan;
            this.移至飛拍結束位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移至飛拍結束位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移至飛拍結束位置.DoNotStopAtThisProcess = true;
            this.移至飛拍結束位置.ErrorDescription = "";
            this.移至飛拍結束位置.ExceptionWaitTime = 100;
            this.移至飛拍結束位置.LastExecuteMs = ((long)(0));
            this.移至飛拍結束位置.Location = new System.Drawing.Point(297, 396);
            this.移至飛拍結束位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移至飛拍結束位置.Margin = new System.Windows.Forms.Padding(4);
            this.移至飛拍結束位置.Name = "移至飛拍結束位置";
            this.移至飛拍結束位置.Size = new System.Drawing.Size(100, 40);
            this.移至飛拍結束位置.StopWhenError = true;
            this.移至飛拍結束位置.TabIndex = 38;
            this.移至飛拍結束位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移至飛拍結束位置_ProcessIn);
            // 
            // 此回合是否拍攝完畢
            // 
            this.此回合是否拍攝完畢.BackColor = System.Drawing.Color.Cyan;
            this.此回合是否拍攝完畢.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.此回合是否拍攝完畢.DoNotStopAtThisProcess = true;
            this.此回合是否拍攝完畢.ErrorDescription = "";
            this.此回合是否拍攝完畢.FalseProcess = this.MainFlow_Node1;
            this.此回合是否拍攝完畢.Location = new System.Drawing.Point(561, 173);
            this.此回合是否拍攝完畢.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.此回合是否拍攝完畢.Name = "此回合是否拍攝完畢";
            this.此回合是否拍攝完畢.Size = new System.Drawing.Size(138, 60);
            this.此回合是否拍攝完畢.TabIndex = 51;
            this.此回合是否拍攝完畢.TrueProcess = this.確認此回合影像是否收齊;
            this.此回合是否拍攝完畢.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.此回合是否拍攝完畢_ConditionCheck);
            // 
            // MainFlow_Node1
            // 
            this.MainFlow_Node1.BackColor = System.Drawing.Color.Cyan;
            this.MainFlow_Node1.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.MainFlow_Node1.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.MainFlow_Node1.DoNotStopAtThisProcess = true;
            this.MainFlow_Node1.ErrorDescription = "";
            this.MainFlow_Node1.ExceptionWaitTime = 100;
            this.MainFlow_Node1.LastExecuteMs = ((long)(0));
            this.MainFlow_Node1.Location = new System.Drawing.Point(365, 224);
            this.MainFlow_Node1.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.MainFlow_Node1.Name = "MainFlow_Node1";
            this.MainFlow_Node1.NextProcess = this.是否為大產品;
            this.MainFlow_Node1.Size = new System.Drawing.Size(3, 3);
            this.MainFlow_Node1.StopWhenError = true;
            this.MainFlow_Node1.TabIndex = 53;
            this.MainFlow_Node1.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.MainFlow_Node1_ProcessIn);
            // 
            // 確認此回合影像是否收齊
            // 
            this.確認此回合影像是否收齊.BackColor = System.Drawing.Color.Cyan;
            this.確認此回合影像是否收齊.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.確認此回合影像是否收齊.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.確認此回合影像是否收齊.DoNotStopAtThisProcess = false;
            this.確認此回合影像是否收齊.ErrorDescription = "";
            this.確認此回合影像是否收齊.ExceptionWaitTime = 100;
            this.確認此回合影像是否收齊.LastExecuteMs = ((long)(0));
            this.確認此回合影像是否收齊.Location = new System.Drawing.Point(752, 183);
            this.確認此回合影像是否收齊.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.確認此回合影像是否收齊.Margin = new System.Windows.Forms.Padding(4);
            this.確認此回合影像是否收齊.Name = "確認此回合影像是否收齊";
            this.確認此回合影像是否收齊.NextProcess = this.是否全部產品拍攝完成;
            this.確認此回合影像是否收齊.Size = new System.Drawing.Size(100, 40);
            this.確認此回合影像是否收齊.StopWhenError = true;
            this.確認此回合影像是否收齊.TabIndex = 75;
            this.確認此回合影像是否收齊.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.確認此回合影像是否收齊_ProcessIn);
            // 
            // 是否全部產品拍攝完成
            // 
            this.是否全部產品拍攝完成.BackColor = System.Drawing.Color.Cyan;
            this.是否全部產品拍攝完成.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否全部產品拍攝完成.DoNotStopAtThisProcess = false;
            this.是否全部產品拍攝完成.ErrorDescription = "";
            this.是否全部產品拍攝完成.FalseProcess = this.建立Grab_Quene;
            this.是否全部產品拍攝完成.Location = new System.Drawing.Point(530, 279);
            this.是否全部產品拍攝完成.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否全部產品拍攝完成.Margin = new System.Windows.Forms.Padding(4);
            this.是否全部產品拍攝完成.Name = "是否全部產品拍攝完成";
            this.是否全部產品拍攝完成.Size = new System.Drawing.Size(242, 60);
            this.是否全部產品拍攝完成.TabIndex = 22;
            this.是否全部產品拍攝完成.TrueProcess = this.等待所有結果產出;
            this.是否全部產品拍攝完成.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否全部產品拍攝完成_ConditionCheck);
            // 
            // 等待所有結果產出
            // 
            this.等待所有結果產出.BackColor = System.Drawing.Color.Cyan;
            this.等待所有結果產出.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待所有結果產出.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待所有結果產出.DoNotStopAtThisProcess = false;
            this.等待所有結果產出.ErrorDescription = "";
            this.等待所有結果產出.ExceptionWaitTime = 100;
            this.等待所有結果產出.LastExecuteMs = ((long)(0));
            this.等待所有結果產出.Location = new System.Drawing.Point(574, 371);
            this.等待所有結果產出.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待所有結果產出.Margin = new System.Windows.Forms.Padding(4);
            this.等待所有結果產出.Name = "等待所有結果產出";
            this.等待所有結果產出.NextProcess = this.確認產品結果是否OK;
            this.等待所有結果產出.Size = new System.Drawing.Size(108, 40);
            this.等待所有結果產出.StopWhenError = true;
            this.等待所有結果產出.TabIndex = 23;
            this.等待所有結果產出.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待所有結果產出_ProcessIn);
            // 
            // 確認產品結果是否OK
            // 
            this.確認產品結果是否OK.BackColor = System.Drawing.Color.Cyan;
            this.確認產品結果是否OK.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.確認產品結果是否OK.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.確認產品結果是否OK.DoNotStopAtThisProcess = false;
            this.確認產品結果是否OK.ErrorDescription = "";
            this.確認產品結果是否OK.ExceptionWaitTime = 100;
            this.確認產品結果是否OK.LastExecuteMs = ((long)(0));
            this.確認產品結果是否OK.Location = new System.Drawing.Point(574, 441);
            this.確認產品結果是否OK.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.確認產品結果是否OK.Margin = new System.Windows.Forms.Padding(4);
            this.確認產品結果是否OK.Name = "確認產品結果是否OK";
            this.確認產品結果是否OK.NextProcess = this.是否FailAlarm確認;
            this.確認產品結果是否OK.Size = new System.Drawing.Size(108, 40);
            this.確認產品結果是否OK.StopWhenError = true;
            this.確認產品結果是否OK.TabIndex = 24;
            this.確認產品結果是否OK.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.確認產品結果是否OK_ProcessIn);
            // 
            // 是否FailAlarm確認
            // 
            this.是否FailAlarm確認.BackColor = System.Drawing.Color.Cyan;
            this.是否FailAlarm確認.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否FailAlarm確認.DoNotStopAtThisProcess = false;
            this.是否FailAlarm確認.ErrorDescription = "";
            this.是否FailAlarm確認.FalseProcess = this.是否重測;
            this.是否FailAlarm確認.Location = new System.Drawing.Point(561, 513);
            this.是否FailAlarm確認.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否FailAlarm確認.Name = "是否FailAlarm確認";
            this.是否FailAlarm確認.Size = new System.Drawing.Size(133, 44);
            this.是否FailAlarm確認.TabIndex = 57;
            this.是否FailAlarm確認.TrueProcess = this.FailAlarm子流程C;
            this.是否FailAlarm確認.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否FailAlarm確認_ConditionCheck);
            // 
            // 是否重測
            // 
            this.是否重測.BackColor = System.Drawing.Color.Cyan;
            this.是否重測.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否重測.DoNotStopAtThisProcess = false;
            this.是否重測.ErrorDescription = "";
            this.是否重測.FalseProcess = this.通知流道模組;
            this.是否重測.Location = new System.Drawing.Point(573, 587);
            this.是否重測.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否重測.Margin = new System.Windows.Forms.Padding(4);
            this.是否重測.Name = "是否重測";
            this.是否重測.Size = new System.Drawing.Size(109, 49);
            this.是否重測.TabIndex = 25;
            this.是否重測.TrueProcess = this.建立Single_SG_Quene;
            this.是否重測.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否重測_ConditionCheck);
            // 
            // 通知流道模組
            // 
            this.通知流道模組.BackColor = System.Drawing.Color.Cyan;
            this.通知流道模組.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.通知流道模組.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.通知流道模組.DoNotStopAtThisProcess = false;
            this.通知流道模組.ErrorDescription = "";
            this.通知流道模組.ExceptionWaitTime = 100;
            this.通知流道模組.LastExecuteMs = ((long)(0));
            this.通知流道模組.Location = new System.Drawing.Point(323, 717);
            this.通知流道模組.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.通知流道模組.Margin = new System.Windows.Forms.Padding(4);
            this.通知流道模組.Name = "通知流道模組";
            this.通知流道模組.NextProcess = this.所有軸移動到Standby位置;
            this.通知流道模組.Size = new System.Drawing.Size(231, 40);
            this.通知流道模組.StopWhenError = true;
            this.通知流道模組.TabIndex = 26;
            this.通知流道模組.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.通知流道模組_ProcessIn);
            // 
            // 建立Single_SG_Quene
            // 
            this.建立Single_SG_Quene.BackColor = System.Drawing.Color.Cyan;
            this.建立Single_SG_Quene.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.建立Single_SG_Quene.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.建立Single_SG_Quene.DoNotStopAtThisProcess = false;
            this.建立Single_SG_Quene.ErrorDescription = "";
            this.建立Single_SG_Quene.ExceptionWaitTime = 100;
            this.建立Single_SG_Quene.LastExecuteMs = ((long)(0));
            this.建立Single_SG_Quene.Location = new System.Drawing.Point(398, 536);
            this.建立Single_SG_Quene.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.建立Single_SG_Quene.Margin = new System.Windows.Forms.Padding(4);
            this.建立Single_SG_Quene.Name = "建立Single_SG_Quene";
            this.建立Single_SG_Quene.NextProcess = this.讀取IR1資訊;
            this.建立Single_SG_Quene.Size = new System.Drawing.Size(118, 40);
            this.建立Single_SG_Quene.StopWhenError = true;
            this.建立Single_SG_Quene.TabIndex = 14;
            this.建立Single_SG_Quene.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.建立Single_SG_Quene_ProcessIn);
            // 
            // 讀取IR1資訊
            // 
            this.讀取IR1資訊.BackColor = System.Drawing.Color.Cyan;
            this.讀取IR1資訊.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.讀取IR1資訊.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.讀取IR1資訊.DoNotStopAtThisProcess = false;
            this.讀取IR1資訊.ErrorDescription = "";
            this.讀取IR1資訊.ExceptionWaitTime = 100;
            this.讀取IR1資訊.LastExecuteMs = ((long)(0));
            this.讀取IR1資訊.Location = new System.Drawing.Point(368, 438);
            this.讀取IR1資訊.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.讀取IR1資訊.Margin = new System.Windows.Forms.Padding(4);
            this.讀取IR1資訊.Name = "讀取IR1資訊";
            this.讀取IR1資訊.NextProcess = this.建立Grab_Quene;
            this.讀取IR1資訊.Size = new System.Drawing.Size(100, 40);
            this.讀取IR1資訊.StopWhenError = true;
            this.讀取IR1資訊.TabIndex = 16;
            this.讀取IR1資訊.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.讀取IR1資訊_ProcessIn);
            // 
            // FailAlarm子流程C
            // 
            this.FailAlarm子流程C.BackColor = System.Drawing.Color.Cyan;
            this.FailAlarm子流程C.ChildProcess = this.FailAlarm開始;
            this.FailAlarm子流程C.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.FailAlarm子流程C.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.FailAlarm子流程C.DoNotStopAtThisProcess = false;
            this.FailAlarm子流程C.ErrorDescription = "";
            this.FailAlarm子流程C.ExceptionWaitTime = 100;
            this.FailAlarm子流程C.LastExecuteMs = ((long)(0));
            this.FailAlarm子流程C.Location = new System.Drawing.Point(677, 559);
            this.FailAlarm子流程C.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.FailAlarm子流程C.Name = "FailAlarm子流程C";
            this.FailAlarm子流程C.NextProcess = this.是否重測;
            this.FailAlarm子流程C.Size = new System.Drawing.Size(70, 40);
            this.FailAlarm子流程C.StopWhenError = true;
            this.FailAlarm子流程C.TabIndex = 56;
            // 
            // FailAlarm開始
            // 
            this.FailAlarm開始.BackColor = System.Drawing.Color.Chocolate;
            this.FailAlarm開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.FailAlarm開始.DoNotStopAtThisProcess = false;
            this.FailAlarm開始.ErrorDescription = "";
            this.FailAlarm開始.ExceptionWaitTime = 100;
            this.FailAlarm開始.LastExecuteMs = ((long)(0));
            this.FailAlarm開始.Location = new System.Drawing.Point(152, 87);
            this.FailAlarm開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.FailAlarm開始.Name = "FailAlarm開始";
            this.FailAlarm開始.NextProcess = this.Boat盤限制解除;
            this.FailAlarm開始.Size = new System.Drawing.Size(100, 40);
            this.FailAlarm開始.StopWhenError = true;
            this.FailAlarm開始.TabIndex = 1;
            // 
            // Boat盤限制解除
            // 
            this.Boat盤限制解除.BackColor = System.Drawing.Color.Cyan;
            this.Boat盤限制解除.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Boat盤限制解除.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Boat盤限制解除.DoNotStopAtThisProcess = false;
            this.Boat盤限制解除.ErrorDescription = "";
            this.Boat盤限制解除.ExceptionWaitTime = 100;
            this.Boat盤限制解除.LastExecuteMs = ((long)(0));
            this.Boat盤限制解除.Location = new System.Drawing.Point(152, 165);
            this.Boat盤限制解除.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Boat盤限制解除.Name = "Boat盤限制解除";
            this.Boat盤限制解除.NextProcess = this.流道軸移動至人員確認位置;
            this.Boat盤限制解除.Size = new System.Drawing.Size(100, 40);
            this.Boat盤限制解除.StopWhenError = true;
            this.Boat盤限制解除.TabIndex = 27;
            this.Boat盤限制解除.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Boat盤限制解除_ProcessIn);
            // 
            // 流道軸移動至人員確認位置
            // 
            this.流道軸移動至人員確認位置.BackColor = System.Drawing.Color.Cyan;
            this.流道軸移動至人員確認位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道軸移動至人員確認位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道軸移動至人員確認位置.DoNotStopAtThisProcess = false;
            this.流道軸移動至人員確認位置.ErrorDescription = "";
            this.流道軸移動至人員確認位置.ExceptionWaitTime = 100;
            this.流道軸移動至人員確認位置.LastExecuteMs = ((long)(0));
            this.流道軸移動至人員確認位置.Location = new System.Drawing.Point(152, 233);
            this.流道軸移動至人員確認位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道軸移動至人員確認位置.Name = "流道軸移動至人員確認位置";
            this.流道軸移動至人員確認位置.NextProcess = this.人員確認;
            this.流道軸移動至人員確認位置.Size = new System.Drawing.Size(100, 40);
            this.流道軸移動至人員確認位置.StopWhenError = true;
            this.流道軸移動至人員確認位置.TabIndex = 31;
            this.流道軸移動至人員確認位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.流道軸移動至人員確認位置_ProcessIn);
            // 
            // 人員確認
            // 
            this.人員確認.BackColor = System.Drawing.Color.Cyan;
            this.人員確認.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.人員確認.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.人員確認.DoNotStopAtThisProcess = false;
            this.人員確認.ErrorDescription = "";
            this.人員確認.ExceptionWaitTime = 100;
            this.人員確認.LastExecuteMs = ((long)(0));
            this.人員確認.Location = new System.Drawing.Point(152, 306);
            this.人員確認.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.人員確認.Name = "人員確認";
            this.人員確認.NextProcess = this.流道偵測產品是否存在;
            this.人員確認.Size = new System.Drawing.Size(100, 40);
            this.人員確認.StopWhenError = true;
            this.人員確認.TabIndex = 28;
            this.人員確認.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.人員確認_ProcessIn);
            // 
            // 流道偵測產品是否存在
            // 
            this.流道偵測產品是否存在.BackColor = System.Drawing.Color.Cyan;
            this.流道偵測產品是否存在.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道偵測產品是否存在.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道偵測產品是否存在.DoNotStopAtThisProcess = false;
            this.流道偵測產品是否存在.ErrorDescription = "";
            this.流道偵測產品是否存在.ExceptionWaitTime = 100;
            this.流道偵測產品是否存在.LastExecuteMs = ((long)(0));
            this.流道偵測產品是否存在.Location = new System.Drawing.Point(152, 380);
            this.流道偵測產品是否存在.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道偵測產品是否存在.Name = "流道偵測產品是否存在";
            this.流道偵測產品是否存在.NextProcess = this.流道軸移動回檢測位置;
            this.流道偵測產品是否存在.Size = new System.Drawing.Size(100, 40);
            this.流道偵測產品是否存在.StopWhenError = true;
            this.流道偵測產品是否存在.TabIndex = 30;
            this.流道偵測產品是否存在.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.流道偵測產品是否存在_ProcessIn);
            // 
            // 流道軸移動回檢測位置
            // 
            this.流道軸移動回檢測位置.BackColor = System.Drawing.Color.Cyan;
            this.流道軸移動回檢測位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.流道軸移動回檢測位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.流道軸移動回檢測位置.DoNotStopAtThisProcess = false;
            this.流道軸移動回檢測位置.ErrorDescription = "";
            this.流道軸移動回檢測位置.ExceptionWaitTime = 100;
            this.流道軸移動回檢測位置.LastExecuteMs = ((long)(0));
            this.流道軸移動回檢測位置.Location = new System.Drawing.Point(152, 449);
            this.流道軸移動回檢測位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.流道軸移動回檢測位置.Name = "流道軸移動回檢測位置";
            this.流道軸移動回檢測位置.NextProcess = this.Boat盤限制開啟;
            this.流道軸移動回檢測位置.Size = new System.Drawing.Size(100, 40);
            this.流道軸移動回檢測位置.StopWhenError = true;
            this.流道軸移動回檢測位置.TabIndex = 32;
            this.流道軸移動回檢測位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.流道軸移動回檢測位置_ProcessIn);
            // 
            // Boat盤限制開啟
            // 
            this.Boat盤限制開啟.BackColor = System.Drawing.Color.Cyan;
            this.Boat盤限制開啟.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Boat盤限制開啟.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Boat盤限制開啟.DoNotStopAtThisProcess = false;
            this.Boat盤限制開啟.ErrorDescription = "";
            this.Boat盤限制開啟.ExceptionWaitTime = 100;
            this.Boat盤限制開啟.LastExecuteMs = ((long)(0));
            this.Boat盤限制開啟.Location = new System.Drawing.Point(152, 518);
            this.Boat盤限制開啟.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Boat盤限制開啟.Name = "Boat盤限制開啟";
            this.Boat盤限制開啟.Size = new System.Drawing.Size(100, 40);
            this.Boat盤限制開啟.StopWhenError = true;
            this.Boat盤限制開啟.TabIndex = 29;
            this.Boat盤限制開啟.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Boat盤限制開啟_ProcessIn);
            // 
            // 雷射測高子流程
            // 
            this.雷射測高子流程.BackColor = System.Drawing.Color.Cyan;
            this.雷射測高子流程.ChildProcess = this.雷射測高流程開始;
            this.雷射測高子流程.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.雷射測高子流程.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.雷射測高子流程.DoNotStopAtThisProcess = false;
            this.雷射測高子流程.ErrorDescription = "";
            this.雷射測高子流程.ExceptionWaitTime = 100;
            this.雷射測高子流程.LastExecuteMs = ((long)(0));
            this.雷射測高子流程.Location = new System.Drawing.Point(497, 72);
            this.雷射測高子流程.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.雷射測高子流程.Name = "雷射測高子流程";
            this.雷射測高子流程.NextProcess = this.大產品拍攝子流程C;
            this.雷射測高子流程.Size = new System.Drawing.Size(57, 40);
            this.雷射測高子流程.StopWhenError = true;
            this.雷射測高子流程.TabIndex = 73;
            // 
            // 雷射測高流程開始
            // 
            this.雷射測高流程開始.BackColor = System.Drawing.Color.Chocolate;
            this.雷射測高流程開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.雷射測高流程開始.DoNotStopAtThisProcess = false;
            this.雷射測高流程開始.ErrorDescription = "";
            this.雷射測高流程開始.ExceptionWaitTime = 100;
            this.雷射測高流程開始.LastExecuteMs = ((long)(0));
            this.雷射測高流程開始.Location = new System.Drawing.Point(269, 12);
            this.雷射測高流程開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.雷射測高流程開始.Name = "雷射測高流程開始";
            this.雷射測高流程開始.NextProcess = this.是否開啟雷射測高;
            this.雷射測高流程開始.Size = new System.Drawing.Size(124, 40);
            this.雷射測高流程開始.StopWhenError = true;
            this.雷射測高流程開始.TabIndex = 1;
            // 
            // 是否開啟雷射測高
            // 
            this.是否開啟雷射測高.BackColor = System.Drawing.Color.Cyan;
            this.是否開啟雷射測高.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否開啟雷射測高.DoNotStopAtThisProcess = false;
            this.是否開啟雷射測高.ErrorDescription = "";
            this.是否開啟雷射測高.FalseProcess = this.雷射段差結束;
            this.是否開啟雷射測高.Location = new System.Drawing.Point(278, 83);
            this.是否開啟雷射測高.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否開啟雷射測高.Margin = new System.Windows.Forms.Padding(4);
            this.是否開啟雷射測高.Name = "是否開啟雷射測高";
            this.是否開啟雷射測高.Size = new System.Drawing.Size(109, 49);
            this.是否開啟雷射測高.TabIndex = 82;
            this.是否開啟雷射測高.TrueProcess = this.是否所有Map區域都掃完;
            this.是否開啟雷射測高.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否開啟雷射測高_ConditionCheck);
            // 
            // 雷射段差結束
            // 
            this.雷射段差結束.BackColor = System.Drawing.Color.Cyan;
            this.雷射段差結束.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.雷射段差結束.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.雷射段差結束.DoNotStopAtThisProcess = false;
            this.雷射段差結束.ErrorDescription = "";
            this.雷射段差結束.ExceptionWaitTime = 100;
            this.雷射段差結束.LastExecuteMs = ((long)(0));
            this.雷射段差結束.Location = new System.Drawing.Point(421, 88);
            this.雷射段差結束.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.雷射段差結束.Name = "雷射段差結束";
            this.雷射段差結束.Size = new System.Drawing.Size(100, 40);
            this.雷射段差結束.StopWhenError = true;
            this.雷射段差結束.TabIndex = 86;
            this.雷射段差結束.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.雷射段差結束_ProcessIn);
            // 
            // 是否所有Map區域都掃完
            // 
            this.是否所有Map區域都掃完.BackColor = System.Drawing.Color.Cyan;
            this.是否所有Map區域都掃完.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否所有Map區域都掃完.DoNotStopAtThisProcess = true;
            this.是否所有Map區域都掃完.ErrorDescription = "";
            this.是否所有Map區域都掃完.FalseProcess = this.移至Map區域開始位置;
            this.是否所有Map區域都掃完.Location = new System.Drawing.Point(242, 169);
            this.是否所有Map區域都掃完.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否所有Map區域都掃完.Margin = new System.Windows.Forms.Padding(4);
            this.是否所有Map區域都掃完.Name = "是否所有Map區域都掃完";
            this.是否所有Map區域都掃完.Size = new System.Drawing.Size(181, 72);
            this.是否所有Map區域都掃完.TabIndex = 85;
            this.是否所有Map區域都掃完.TrueProcess = this.雷射段差結束;
            this.是否所有Map區域都掃完.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否所有Map區域都掃完_ConditionCheck);
            // 
            // 移至Map區域開始位置
            // 
            this.移至Map區域開始位置.BackColor = System.Drawing.Color.Cyan;
            this.移至Map區域開始位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移至Map區域開始位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移至Map區域開始位置.DoNotStopAtThisProcess = false;
            this.移至Map區域開始位置.ErrorDescription = "";
            this.移至Map區域開始位置.ExceptionWaitTime = 100;
            this.移至Map區域開始位置.LastExecuteMs = ((long)(0));
            this.移至Map區域開始位置.Location = new System.Drawing.Point(55, 185);
            this.移至Map區域開始位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移至Map區域開始位置.Name = "移至Map區域開始位置";
            this.移至Map區域開始位置.NextProcess = this.雷射開啟;
            this.移至Map區域開始位置.Size = new System.Drawing.Size(100, 40);
            this.移至Map區域開始位置.StopWhenError = true;
            this.移至Map區域開始位置.TabIndex = 83;
            this.移至Map區域開始位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移至Map區域開始位置_ProcessIn);
            // 
            // 雷射開啟
            // 
            this.雷射開啟.BackColor = System.Drawing.Color.Cyan;
            this.雷射開啟.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.雷射開啟.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.雷射開啟.DoNotStopAtThisProcess = false;
            this.雷射開啟.ErrorDescription = "";
            this.雷射開啟.ExceptionWaitTime = 100;
            this.雷射開啟.LastExecuteMs = ((long)(0));
            this.雷射開啟.Location = new System.Drawing.Point(55, 280);
            this.雷射開啟.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.雷射開啟.Name = "雷射開啟";
            this.雷射開啟.NextProcess = this.移至Map區域結束位置;
            this.雷射開啟.Size = new System.Drawing.Size(100, 40);
            this.雷射開啟.StopWhenError = true;
            this.雷射開啟.TabIndex = 87;
            this.雷射開啟.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.雷射開啟_ProcessIn);
            // 
            // 移至Map區域結束位置
            // 
            this.移至Map區域結束位置.BackColor = System.Drawing.Color.Cyan;
            this.移至Map區域結束位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移至Map區域結束位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移至Map區域結束位置.DoNotStopAtThisProcess = false;
            this.移至Map區域結束位置.ErrorDescription = "";
            this.移至Map區域結束位置.ExceptionWaitTime = 100;
            this.移至Map區域結束位置.LastExecuteMs = ((long)(0));
            this.移至Map區域結束位置.Location = new System.Drawing.Point(55, 359);
            this.移至Map區域結束位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移至Map區域結束位置.Name = "移至Map區域結束位置";
            this.移至Map區域結束位置.NextProcess = this.雷射關閉;
            this.移至Map區域結束位置.Size = new System.Drawing.Size(100, 40);
            this.移至Map區域結束位置.StopWhenError = true;
            this.移至Map區域結束位置.TabIndex = 84;
            this.移至Map區域結束位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移至Map區域結束位置_ProcessIn);
            // 
            // 雷射關閉
            // 
            this.雷射關閉.BackColor = System.Drawing.Color.Cyan;
            this.雷射關閉.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.雷射關閉.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.雷射關閉.DoNotStopAtThisProcess = false;
            this.雷射關閉.ErrorDescription = "";
            this.雷射關閉.ExceptionWaitTime = 100;
            this.雷射關閉.LastExecuteMs = ((long)(0));
            this.雷射關閉.Location = new System.Drawing.Point(283, 359);
            this.雷射關閉.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.雷射關閉.Name = "雷射關閉";
            this.雷射關閉.NextProcess = this.是否所有Map區域都掃完;
            this.雷射關閉.Size = new System.Drawing.Size(100, 40);
            this.雷射關閉.StopWhenError = true;
            this.雷射關閉.TabIndex = 88;
            this.雷射關閉.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.雷射關閉_ProcessIn);
            // 
            // 大產品拍攝子流程C
            // 
            this.大產品拍攝子流程C.BackColor = System.Drawing.Color.Cyan;
            this.大產品拍攝子流程C.ChildProcess = this.大產品拍攝開始;
            this.大產品拍攝子流程C.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.大產品拍攝子流程C.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.大產品拍攝子流程C.DoNotStopAtThisProcess = true;
            this.大產品拍攝子流程C.ErrorDescription = "";
            this.大產品拍攝子流程C.ExceptionWaitTime = 100;
            this.大產品拍攝子流程C.LastExecuteMs = ((long)(0));
            this.大產品拍攝子流程C.Location = new System.Drawing.Point(630, 72);
            this.大產品拍攝子流程C.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.大產品拍攝子流程C.Margin = new System.Windows.Forms.Padding(4);
            this.大產品拍攝子流程C.Name = "大產品拍攝子流程C";
            this.大產品拍攝子流程C.NextProcess = this.此回合是否拍攝完畢;
            this.大產品拍攝子流程C.Size = new System.Drawing.Size(117, 40);
            this.大產品拍攝子流程C.StopWhenError = true;
            this.大產品拍攝子流程C.TabIndex = 20;
            this.大產品拍攝子流程C.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.大產品拍攝子流程C_ProcessIn);
            // 
            // 大產品拍攝開始
            // 
            this.大產品拍攝開始.BackColor = System.Drawing.Color.Chocolate;
            this.大產品拍攝開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.大產品拍攝開始.DoNotStopAtThisProcess = true;
            this.大產品拍攝開始.ErrorDescription = "";
            this.大產品拍攝開始.ExceptionWaitTime = 100;
            this.大產品拍攝開始.LastExecuteMs = ((long)(0));
            this.大產品拍攝開始.Location = new System.Drawing.Point(295, 21);
            this.大產品拍攝開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.大產品拍攝開始.Margin = new System.Windows.Forms.Padding(4);
            this.大產品拍攝開始.Name = "大產品拍攝開始";
            this.大產品拍攝開始.NextProcess = this.是否組圖;
            this.大產品拍攝開始.Size = new System.Drawing.Size(100, 40);
            this.大產品拍攝開始.StopWhenError = true;
            this.大產品拍攝開始.TabIndex = 41;
            // 
            // 是否組圖
            // 
            this.是否組圖.BackColor = System.Drawing.Color.Cyan;
            this.是否組圖.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否組圖.DoNotStopAtThisProcess = true;
            this.是否組圖.ErrorDescription = "";
            this.是否組圖.FalseProcess = this.分區下SG;
            this.是否組圖.Location = new System.Drawing.Point(317, 134);
            this.是否組圖.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否組圖.Margin = new System.Windows.Forms.Padding(4);
            this.是否組圖.Name = "是否組圖";
            this.是否組圖.Size = new System.Drawing.Size(135, 75);
            this.是否組圖.TabIndex = 49;
            this.是否組圖.TrueProcess = this.Mosaic下SG;
            this.是否組圖.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否組圖_ConditionCheck);
            // 
            // 分區下SG
            // 
            this.分區下SG.BackColor = System.Drawing.Color.Cyan;
            this.分區下SG.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.分區下SG.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.分區下SG.DoNotStopAtThisProcess = true;
            this.分區下SG.ErrorDescription = "";
            this.分區下SG.ExceptionWaitTime = 100;
            this.分區下SG.LastExecuteMs = ((long)(0));
            this.分區下SG.Location = new System.Drawing.Point(498, 82);
            this.分區下SG.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.分區下SG.Margin = new System.Windows.Forms.Padding(4);
            this.分區下SG.Name = "分區下SG";
            this.分區下SG.NextProcess = this.移動指定位置;
            this.分區下SG.Size = new System.Drawing.Size(100, 40);
            this.分區下SG.StopWhenError = true;
            this.分區下SG.TabIndex = 53;
            this.分區下SG.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.分區下SG_ProcessIn);
            // 
            // 移動指定位置
            // 
            this.移動指定位置.BackColor = System.Drawing.Color.Cyan;
            this.移動指定位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移動指定位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移動指定位置.DoNotStopAtThisProcess = true;
            this.移動指定位置.ErrorDescription = "";
            this.移動指定位置.ExceptionWaitTime = 100;
            this.移動指定位置.LastExecuteMs = ((long)(0));
            this.移動指定位置.Location = new System.Drawing.Point(498, 169);
            this.移動指定位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移動指定位置.Margin = new System.Windows.Forms.Padding(4);
            this.移動指定位置.Name = "移動指定位置";
            this.移動指定位置.NextProcess = this.下IP拍照2;
            this.移動指定位置.Size = new System.Drawing.Size(100, 40);
            this.移動指定位置.StopWhenError = true;
            this.移動指定位置.TabIndex = 50;
            this.移動指定位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移動指定位置_ProcessIn);
            // 
            // 下IP拍照2
            // 
            this.下IP拍照2.BackColor = System.Drawing.Color.Cyan;
            this.下IP拍照2.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.下IP拍照2.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.下IP拍照2.DoNotStopAtThisProcess = true;
            this.下IP拍照2.ErrorDescription = "";
            this.下IP拍照2.ExceptionWaitTime = 100;
            this.下IP拍照2.LastExecuteMs = ((long)(0));
            this.下IP拍照2.Location = new System.Drawing.Point(649, 373);
            this.下IP拍照2.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.下IP拍照2.Margin = new System.Windows.Forms.Padding(4);
            this.下IP拍照2.Name = "下IP拍照2";
            this.下IP拍照2.NextProcess = this.是否所有影像拍完;
            this.下IP拍照2.Size = new System.Drawing.Size(100, 40);
            this.下IP拍照2.StopWhenError = true;
            this.下IP拍照2.TabIndex = 51;
            this.下IP拍照2.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.下IP拍照2_ProcessIn);
            // 
            // 是否所有影像拍完
            // 
            this.是否所有影像拍完.BackColor = System.Drawing.Color.Cyan;
            this.是否所有影像拍完.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否所有影像拍完.DoNotStopAtThisProcess = true;
            this.是否所有影像拍完.ErrorDescription = "";
            this.是否所有影像拍完.FalseProcess = this.移動指定位置;
            this.是否所有影像拍完.Location = new System.Drawing.Point(487, 263);
            this.是否所有影像拍完.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否所有影像拍完.Margin = new System.Windows.Forms.Padding(4);
            this.是否所有影像拍完.Name = "是否所有影像拍完";
            this.是否所有影像拍完.Size = new System.Drawing.Size(135, 75);
            this.是否所有影像拍完.TabIndex = 52;
            this.是否所有影像拍完.TrueProcess = this.是否所有Map都拍完;
            this.是否所有影像拍完.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否所有影像拍完_ConditionCheck);
            // 
            // 是否所有Map都拍完
            // 
            this.是否所有Map都拍完.BackColor = System.Drawing.Color.Cyan;
            this.是否所有Map都拍完.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否所有Map都拍完.DoNotStopAtThisProcess = true;
            this.是否所有Map都拍完.ErrorDescription = "";
            this.是否所有Map都拍完.FalseProcess = this.是否組圖;
            this.是否所有Map都拍完.Location = new System.Drawing.Point(317, 338);
            this.是否所有Map都拍完.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否所有Map都拍完.Margin = new System.Windows.Forms.Padding(4);
            this.是否所有Map都拍完.Name = "是否所有Map都拍完";
            this.是否所有Map都拍完.Size = new System.Drawing.Size(135, 75);
            this.是否所有Map都拍完.TabIndex = 55;
            this.是否所有Map都拍完.TrueProcess = this.大產品拍攝完畢;
            this.是否所有Map都拍完.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否所有Map都拍完_ConditionCheck);
            // 
            // 大產品拍攝完畢
            // 
            this.大產品拍攝完畢.BackColor = System.Drawing.Color.Cyan;
            this.大產品拍攝完畢.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.大產品拍攝完畢.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.大產品拍攝完畢.DoNotStopAtThisProcess = true;
            this.大產品拍攝完畢.ErrorDescription = "";
            this.大產品拍攝完畢.ExceptionWaitTime = 100;
            this.大產品拍攝完畢.LastExecuteMs = ((long)(0));
            this.大產品拍攝完畢.Location = new System.Drawing.Point(329, 504);
            this.大產品拍攝完畢.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.大產品拍攝完畢.Margin = new System.Windows.Forms.Padding(4);
            this.大產品拍攝完畢.Name = "大產品拍攝完畢";
            this.大產品拍攝完畢.Size = new System.Drawing.Size(100, 40);
            this.大產品拍攝完畢.StopWhenError = true;
            this.大產品拍攝完畢.TabIndex = 45;
            this.大產品拍攝完畢.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.大產品拍攝完畢_ProcessIn);
            // 
            // Mosaic下SG
            // 
            this.Mosaic下SG.BackColor = System.Drawing.Color.Cyan;
            this.Mosaic下SG.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Mosaic下SG.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Mosaic下SG.DoNotStopAtThisProcess = true;
            this.Mosaic下SG.ErrorDescription = "";
            this.Mosaic下SG.ExceptionWaitTime = 100;
            this.Mosaic下SG.LastExecuteMs = ((long)(0));
            this.Mosaic下SG.Location = new System.Drawing.Point(20, 91);
            this.Mosaic下SG.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Mosaic下SG.Margin = new System.Windows.Forms.Padding(4);
            this.Mosaic下SG.Name = "Mosaic下SG";
            this.Mosaic下SG.NextProcess = this.移至拍攝起始位置;
            this.Mosaic下SG.Size = new System.Drawing.Size(100, 40);
            this.Mosaic下SG.StopWhenError = true;
            this.Mosaic下SG.TabIndex = 54;
            this.Mosaic下SG.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Mosaic下SG_ProcessIn);
            // 
            // 移至拍攝起始位置
            // 
            this.移至拍攝起始位置.BackColor = System.Drawing.Color.Cyan;
            this.移至拍攝起始位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移至拍攝起始位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移至拍攝起始位置.DoNotStopAtThisProcess = true;
            this.移至拍攝起始位置.ErrorDescription = "";
            this.移至拍攝起始位置.ExceptionWaitTime = 100;
            this.移至拍攝起始位置.LastExecuteMs = ((long)(0));
            this.移至拍攝起始位置.Location = new System.Drawing.Point(20, 151);
            this.移至拍攝起始位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移至拍攝起始位置.Margin = new System.Windows.Forms.Padding(4);
            this.移至拍攝起始位置.Name = "移至拍攝起始位置";
            this.移至拍攝起始位置.NextProcess = this.下IP拍照;
            this.移至拍攝起始位置.Size = new System.Drawing.Size(100, 40);
            this.移至拍攝起始位置.StopWhenError = true;
            this.移至拍攝起始位置.TabIndex = 42;
            this.移至拍攝起始位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移至拍攝起始位置_ProcessIn);
            // 
            // 下IP拍照
            // 
            this.下IP拍照.BackColor = System.Drawing.Color.Cyan;
            this.下IP拍照.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.下IP拍照.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.下IP拍照.DoNotStopAtThisProcess = true;
            this.下IP拍照.ErrorDescription = "";
            this.下IP拍照.ExceptionWaitTime = 100;
            this.下IP拍照.LastExecuteMs = ((long)(0));
            this.下IP拍照.Location = new System.Drawing.Point(10, 353);
            this.下IP拍照.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.下IP拍照.Margin = new System.Windows.Forms.Padding(4);
            this.下IP拍照.Name = "下IP拍照";
            this.下IP拍照.NextProcess = this.是否產品影像拍攝完成;
            this.下IP拍照.Size = new System.Drawing.Size(100, 40);
            this.下IP拍照.StopWhenError = true;
            this.下IP拍照.TabIndex = 43;
            this.下IP拍照.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.下IP拍照_ProcessIn);
            // 
            // 是否產品影像拍攝完成
            // 
            this.是否產品影像拍攝完成.BackColor = System.Drawing.Color.Cyan;
            this.是否產品影像拍攝完成.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否產品影像拍攝完成.DoNotStopAtThisProcess = true;
            this.是否產品影像拍攝完成.ErrorDescription = "";
            this.是否產品影像拍攝完成.FalseProcess = this.移至拍攝起始位置;
            this.是否產品影像拍攝完成.Location = new System.Drawing.Point(206, 228);
            this.是否產品影像拍攝完成.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否產品影像拍攝完成.Margin = new System.Windows.Forms.Padding(4);
            this.是否產品影像拍攝完成.Name = "是否產品影像拍攝完成";
            this.是否產品影像拍攝完成.Size = new System.Drawing.Size(135, 75);
            this.是否產品影像拍攝完成.TabIndex = 44;
            this.是否產品影像拍攝完成.TrueProcess = this.是否所有Map都拍完;
            this.是否產品影像拍攝完成.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否產品影像拍攝完成_ConditionCheck);
            // 
            // 教讀
            // 
            this.教讀.BackColor = System.Drawing.Color.Cyan;
            this.教讀.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.教讀.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.教讀.DoNotStopAtThisProcess = false;
            this.教讀.ErrorDescription = "";
            this.教讀.ExceptionWaitTime = 100;
            this.教讀.LastExecuteMs = ((long)(0));
            this.教讀.Location = new System.Drawing.Point(346, 636);
            this.教讀.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.教讀.Margin = new System.Windows.Forms.Padding(4);
            this.教讀.Name = "教讀";
            this.教讀.NextProcess = this.是否抽檢;
            this.教讀.Size = new System.Drawing.Size(51, 40);
            this.教讀.StopWhenError = true;
            this.教讀.TabIndex = 9;
            this.教讀.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.教讀_ProcessIn);
            // 
            // Barcode拍攝子流程C
            // 
            this.Barcode拍攝子流程C.BackColor = System.Drawing.Color.Cyan;
            this.Barcode拍攝子流程C.ChildProcess = this.Barcode拍攝開始;
            this.Barcode拍攝子流程C.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Barcode拍攝子流程C.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Barcode拍攝子流程C.DoNotStopAtThisProcess = false;
            this.Barcode拍攝子流程C.ErrorDescription = "";
            this.Barcode拍攝子流程C.ExceptionWaitTime = 100;
            this.Barcode拍攝子流程C.LastExecuteMs = ((long)(0));
            this.Barcode拍攝子流程C.Location = new System.Drawing.Point(43, 746);
            this.Barcode拍攝子流程C.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Barcode拍攝子流程C.Name = "Barcode拍攝子流程C";
            this.Barcode拍攝子流程C.NextProcess = this.是否教讀;
            this.Barcode拍攝子流程C.Size = new System.Drawing.Size(87, 40);
            this.Barcode拍攝子流程C.StopWhenError = true;
            this.Barcode拍攝子流程C.TabIndex = 63;
            // 
            // Barcode拍攝開始
            // 
            this.Barcode拍攝開始.BackColor = System.Drawing.Color.Chocolate;
            this.Barcode拍攝開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Barcode拍攝開始.DoNotStopAtThisProcess = false;
            this.Barcode拍攝開始.ErrorDescription = "";
            this.Barcode拍攝開始.ExceptionWaitTime = 100;
            this.Barcode拍攝開始.LastExecuteMs = ((long)(0));
            this.Barcode拍攝開始.Location = new System.Drawing.Point(160, 51);
            this.Barcode拍攝開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Barcode拍攝開始.Name = "Barcode拍攝開始";
            this.Barcode拍攝開始.NextProcess = this.移至Barcode位置;
            this.Barcode拍攝開始.Size = new System.Drawing.Size(115, 40);
            this.Barcode拍攝開始.StopWhenError = true;
            this.Barcode拍攝開始.TabIndex = 51;
            // 
            // 移至Barcode位置
            // 
            this.移至Barcode位置.BackColor = System.Drawing.Color.Cyan;
            this.移至Barcode位置.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.移至Barcode位置.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.移至Barcode位置.DoNotStopAtThisProcess = false;
            this.移至Barcode位置.ErrorDescription = "";
            this.移至Barcode位置.ExceptionWaitTime = 100;
            this.移至Barcode位置.LastExecuteMs = ((long)(0));
            this.移至Barcode位置.Location = new System.Drawing.Point(160, 126);
            this.移至Barcode位置.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.移至Barcode位置.Name = "移至Barcode位置";
            this.移至Barcode位置.NextProcess = this.是否使用相機拍攝;
            this.移至Barcode位置.Size = new System.Drawing.Size(115, 40);
            this.移至Barcode位置.StopWhenError = true;
            this.移至Barcode位置.TabIndex = 57;
            this.移至Barcode位置.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.移至Barcode位置_ProcessIn);
            // 
            // 是否使用相機拍攝
            // 
            this.是否使用相機拍攝.BackColor = System.Drawing.Color.Cyan;
            this.是否使用相機拍攝.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否使用相機拍攝.DoNotStopAtThisProcess = false;
            this.是否使用相機拍攝.ErrorDescription = "";
            this.是否使用相機拍攝.FalseProcess = this.Barcode機拍攝Barcode;
            this.是否使用相機拍攝.Location = new System.Drawing.Point(154, 201);
            this.是否使用相機拍攝.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否使用相機拍攝.Name = "是否使用相機拍攝";
            this.是否使用相機拍攝.Size = new System.Drawing.Size(127, 53);
            this.是否使用相機拍攝.TabIndex = 52;
            this.是否使用相機拍攝.TrueProcess = this.相機拍攝Barcode;
            this.是否使用相機拍攝.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否使用相機拍攝_ConditionCheck);
            // 
            // Barcode機拍攝Barcode
            // 
            this.Barcode機拍攝Barcode.BackColor = System.Drawing.Color.Cyan;
            this.Barcode機拍攝Barcode.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Barcode機拍攝Barcode.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Barcode機拍攝Barcode.DoNotStopAtThisProcess = false;
            this.Barcode機拍攝Barcode.ErrorDescription = "";
            this.Barcode機拍攝Barcode.ExceptionWaitTime = 100;
            this.Barcode機拍攝Barcode.LastExecuteMs = ((long)(0));
            this.Barcode機拍攝Barcode.Location = new System.Drawing.Point(59, 290);
            this.Barcode機拍攝Barcode.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Barcode機拍攝Barcode.Name = "Barcode機拍攝Barcode";
            this.Barcode機拍攝Barcode.NextProcess = this.是否讀取成功;
            this.Barcode機拍攝Barcode.Size = new System.Drawing.Size(100, 40);
            this.Barcode機拍攝Barcode.StopWhenError = true;
            this.Barcode機拍攝Barcode.TabIndex = 50;
            this.Barcode機拍攝Barcode.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Barcode機拍攝Barcode_ProcessIn);
            // 
            // 是否讀取成功
            // 
            this.是否讀取成功.BackColor = System.Drawing.Color.Cyan;
            this.是否讀取成功.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否讀取成功.DoNotStopAtThisProcess = false;
            this.是否讀取成功.ErrorDescription = "";
            this.是否讀取成功.FalseProcess = this.手動輸入Barcode;
            this.是否讀取成功.Location = new System.Drawing.Point(160, 365);
            this.是否讀取成功.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否讀取成功.Name = "是否讀取成功";
            this.是否讀取成功.Size = new System.Drawing.Size(120, 75);
            this.是否讀取成功.TabIndex = 54;
            this.是否讀取成功.TrueProcess = this.儲存Barcode資訊;
            this.是否讀取成功.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否讀取成功_ConditionCheck);
            // 
            // 手動輸入Barcode
            // 
            this.手動輸入Barcode.BackColor = System.Drawing.Color.Cyan;
            this.手動輸入Barcode.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.手動輸入Barcode.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.手動輸入Barcode.DoNotStopAtThisProcess = false;
            this.手動輸入Barcode.ErrorDescription = "";
            this.手動輸入Barcode.ExceptionWaitTime = 100;
            this.手動輸入Barcode.LastExecuteMs = ((long)(0));
            this.手動輸入Barcode.Location = new System.Drawing.Point(322, 383);
            this.手動輸入Barcode.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.手動輸入Barcode.Name = "手動輸入Barcode";
            this.手動輸入Barcode.NextProcess = this.儲存Barcode資訊;
            this.手動輸入Barcode.Size = new System.Drawing.Size(100, 40);
            this.手動輸入Barcode.StopWhenError = true;
            this.手動輸入Barcode.TabIndex = 55;
            this.手動輸入Barcode.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.手動輸入Barcode_ProcessIn);
            // 
            // 儲存Barcode資訊
            // 
            this.儲存Barcode資訊.BackColor = System.Drawing.Color.Cyan;
            this.儲存Barcode資訊.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.儲存Barcode資訊.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.儲存Barcode資訊.DoNotStopAtThisProcess = false;
            this.儲存Barcode資訊.ErrorDescription = "";
            this.儲存Barcode資訊.ExceptionWaitTime = 100;
            this.儲存Barcode資訊.LastExecuteMs = ((long)(0));
            this.儲存Barcode資訊.Location = new System.Drawing.Point(138, 504);
            this.儲存Barcode資訊.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.儲存Barcode資訊.Name = "儲存Barcode資訊";
            this.儲存Barcode資訊.Size = new System.Drawing.Size(165, 40);
            this.儲存Barcode資訊.StopWhenError = true;
            this.儲存Barcode資訊.TabIndex = 56;
            this.儲存Barcode資訊.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.儲存Barcode資訊_ProcessIn);
            // 
            // 相機拍攝Barcode
            // 
            this.相機拍攝Barcode.BackColor = System.Drawing.Color.Cyan;
            this.相機拍攝Barcode.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.相機拍攝Barcode.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.相機拍攝Barcode.DoNotStopAtThisProcess = false;
            this.相機拍攝Barcode.ErrorDescription = "";
            this.相機拍攝Barcode.ExceptionWaitTime = 100;
            this.相機拍攝Barcode.LastExecuteMs = ((long)(0));
            this.相機拍攝Barcode.Location = new System.Drawing.Point(285, 290);
            this.相機拍攝Barcode.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.相機拍攝Barcode.Name = "相機拍攝Barcode";
            this.相機拍攝Barcode.NextProcess = this.是否讀取成功;
            this.相機拍攝Barcode.Size = new System.Drawing.Size(100, 40);
            this.相機拍攝Barcode.StopWhenError = true;
            this.相機拍攝Barcode.TabIndex = 53;
            this.相機拍攝Barcode.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.相機拍攝Barcode_ProcessIn);
            // 
            // Barcode教讀
            // 
            this.Barcode教讀.BackColor = System.Drawing.Color.Cyan;
            this.Barcode教讀.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Barcode教讀.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Barcode教讀.DoNotStopAtThisProcess = false;
            this.Barcode教讀.ErrorDescription = "";
            this.Barcode教讀.ExceptionWaitTime = 100;
            this.Barcode教讀.LastExecuteMs = ((long)(0));
            this.Barcode教讀.Location = new System.Drawing.Point(117, 597);
            this.Barcode教讀.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Barcode教讀.Name = "Barcode教讀";
            this.Barcode教讀.NextProcess = this.是否拍攝Barcode;
            this.Barcode教讀.Size = new System.Drawing.Size(100, 40);
            this.Barcode教讀.StopWhenError = true;
            this.Barcode教讀.TabIndex = 61;
            this.Barcode教讀.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Barcode教讀_ProcessIn);
            // 
            // 視覺Bypass之Barcode拍攝子流程C
            // 
            this.視覺Bypass之Barcode拍攝子流程C.BackColor = System.Drawing.Color.Cyan;
            this.視覺Bypass之Barcode拍攝子流程C.ChildProcess = this.視覺Bypass之Barcode拍攝開始;
            this.視覺Bypass之Barcode拍攝子流程C.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.視覺Bypass之Barcode拍攝子流程C.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.視覺Bypass之Barcode拍攝子流程C.DoNotStopAtThisProcess = false;
            this.視覺Bypass之Barcode拍攝子流程C.ErrorDescription = "";
            this.視覺Bypass之Barcode拍攝子流程C.ExceptionWaitTime = 100;
            this.視覺Bypass之Barcode拍攝子流程C.LastExecuteMs = ((long)(0));
            this.視覺Bypass之Barcode拍攝子流程C.Location = new System.Drawing.Point(192, 113);
            this.視覺Bypass之Barcode拍攝子流程C.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.視覺Bypass之Barcode拍攝子流程C.Name = "視覺Bypass之Barcode拍攝子流程C";
            this.視覺Bypass之Barcode拍攝子流程C.NextProcess = this.視覺是否bypass;
            this.視覺Bypass之Barcode拍攝子流程C.Size = new System.Drawing.Size(111, 55);
            this.視覺Bypass之Barcode拍攝子流程C.StopWhenError = true;
            this.視覺Bypass之Barcode拍攝子流程C.TabIndex = 3;
            // 
            // 視覺Bypass之Barcode拍攝開始
            // 
            this.視覺Bypass之Barcode拍攝開始.BackColor = System.Drawing.Color.Chocolate;
            this.視覺Bypass之Barcode拍攝開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.視覺Bypass之Barcode拍攝開始.DoNotStopAtThisProcess = false;
            this.視覺Bypass之Barcode拍攝開始.ErrorDescription = "";
            this.視覺Bypass之Barcode拍攝開始.ExceptionWaitTime = 100;
            this.視覺Bypass之Barcode拍攝開始.LastExecuteMs = ((long)(0));
            this.視覺Bypass之Barcode拍攝開始.Location = new System.Drawing.Point(148, 68);
            this.視覺Bypass之Barcode拍攝開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.視覺Bypass之Barcode拍攝開始.Name = "視覺Bypass之Barcode拍攝開始";
            this.視覺Bypass之Barcode拍攝開始.NextProcess = this.等待載盤進入_VisionBypass;
            this.視覺Bypass之Barcode拍攝開始.Size = new System.Drawing.Size(121, 40);
            this.視覺Bypass之Barcode拍攝開始.StopWhenError = true;
            this.視覺Bypass之Barcode拍攝開始.TabIndex = 1;
            // 
            // 等待載盤進入_VisionBypass
            // 
            this.等待載盤進入_VisionBypass.BackColor = System.Drawing.Color.Cyan;
            this.等待載盤進入_VisionBypass.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.等待載盤進入_VisionBypass.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.等待載盤進入_VisionBypass.DoNotStopAtThisProcess = false;
            this.等待載盤進入_VisionBypass.ErrorDescription = "";
            this.等待載盤進入_VisionBypass.ExceptionWaitTime = 100;
            this.等待載盤進入_VisionBypass.LastExecuteMs = ((long)(0));
            this.等待載盤進入_VisionBypass.Location = new System.Drawing.Point(159, 143);
            this.等待載盤進入_VisionBypass.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.等待載盤進入_VisionBypass.Margin = new System.Windows.Forms.Padding(4);
            this.等待載盤進入_VisionBypass.Name = "等待載盤進入_VisionBypass";
            this.等待載盤進入_VisionBypass.NextProcess = this.是否Barcode教讀_VisionBypass;
            this.等待載盤進入_VisionBypass.Size = new System.Drawing.Size(100, 40);
            this.等待載盤進入_VisionBypass.StopWhenError = true;
            this.等待載盤進入_VisionBypass.TabIndex = 8;
            this.等待載盤進入_VisionBypass.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.等待載盤進入_ProcessIn);
            // 
            // 是否Barcode教讀_VisionBypass
            // 
            this.是否Barcode教讀_VisionBypass.BackColor = System.Drawing.Color.Cyan;
            this.是否Barcode教讀_VisionBypass.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否Barcode教讀_VisionBypass.DoNotStopAtThisProcess = false;
            this.是否Barcode教讀_VisionBypass.ErrorDescription = "";
            this.是否Barcode教讀_VisionBypass.FalseProcess = this.是否拍攝Barcode_VisionBypass;
            this.是否Barcode教讀_VisionBypass.Location = new System.Drawing.Point(146, 211);
            this.是否Barcode教讀_VisionBypass.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否Barcode教讀_VisionBypass.Name = "是否Barcode教讀_VisionBypass";
            this.是否Barcode教讀_VisionBypass.Size = new System.Drawing.Size(126, 77);
            this.是否Barcode教讀_VisionBypass.TabIndex = 11;
            this.是否Barcode教讀_VisionBypass.TrueProcess = this.Barcode教讀_VisionBypass;
            this.是否Barcode教讀_VisionBypass.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否Barcode教讀_ConditionCheck);
            // 
            // 是否拍攝Barcode_VisionBypass
            // 
            this.是否拍攝Barcode_VisionBypass.BackColor = System.Drawing.Color.Cyan;
            this.是否拍攝Barcode_VisionBypass.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.是否拍攝Barcode_VisionBypass.DoNotStopAtThisProcess = false;
            this.是否拍攝Barcode_VisionBypass.ErrorDescription = "";
            this.是否拍攝Barcode_VisionBypass.FalseProcess = this.通知流道模組_VisionBypass;
            this.是否拍攝Barcode_VisionBypass.Location = new System.Drawing.Point(159, 356);
            this.是否拍攝Barcode_VisionBypass.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.是否拍攝Barcode_VisionBypass.Name = "是否拍攝Barcode_VisionBypass";
            this.是否拍攝Barcode_VisionBypass.Size = new System.Drawing.Size(100, 79);
            this.是否拍攝Barcode_VisionBypass.TabIndex = 13;
            this.是否拍攝Barcode_VisionBypass.TrueProcess = this.Barcode拍攝子流程C_VisionBypass;
            this.是否拍攝Barcode_VisionBypass.ConditionCheck += new System.EventHandler<ControlFlow.Controls.BranchArgs>(this.是否拍攝Barcode_ConditionCheck);
            // 
            // 通知流道模組_VisionBypass
            // 
            this.通知流道模組_VisionBypass.BackColor = System.Drawing.Color.Cyan;
            this.通知流道模組_VisionBypass.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.通知流道模組_VisionBypass.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.通知流道模組_VisionBypass.DoNotStopAtThisProcess = false;
            this.通知流道模組_VisionBypass.ErrorDescription = "";
            this.通知流道模組_VisionBypass.ExceptionWaitTime = 100;
            this.通知流道模組_VisionBypass.LastExecuteMs = ((long)(0));
            this.通知流道模組_VisionBypass.Location = new System.Drawing.Point(169, 516);
            this.通知流道模組_VisionBypass.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.通知流道模組_VisionBypass.Name = "通知流道模組_VisionBypass";
            this.通知流道模組_VisionBypass.Size = new System.Drawing.Size(100, 40);
            this.通知流道模組_VisionBypass.StopWhenError = true;
            this.通知流道模組_VisionBypass.TabIndex = 10;
            this.通知流道模組_VisionBypass.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.通知流道模組_ProcessIn);
            // 
            // Barcode拍攝子流程C_VisionBypass
            // 
            this.Barcode拍攝子流程C_VisionBypass.BackColor = System.Drawing.Color.Cyan;
            this.Barcode拍攝子流程C_VisionBypass.ChildProcess = this.Barcode拍攝開始;
            this.Barcode拍攝子流程C_VisionBypass.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Barcode拍攝子流程C_VisionBypass.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Barcode拍攝子流程C_VisionBypass.DoNotStopAtThisProcess = false;
            this.Barcode拍攝子流程C_VisionBypass.ErrorDescription = "";
            this.Barcode拍攝子流程C_VisionBypass.ExceptionWaitTime = 100;
            this.Barcode拍攝子流程C_VisionBypass.LastExecuteMs = ((long)(0));
            this.Barcode拍攝子流程C_VisionBypass.Location = new System.Drawing.Point(296, 369);
            this.Barcode拍攝子流程C_VisionBypass.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Barcode拍攝子流程C_VisionBypass.Name = "Barcode拍攝子流程C_VisionBypass";
            this.Barcode拍攝子流程C_VisionBypass.NextProcess = this.通知流道模組_VisionBypass;
            this.Barcode拍攝子流程C_VisionBypass.Size = new System.Drawing.Size(116, 53);
            this.Barcode拍攝子流程C_VisionBypass.StopWhenError = true;
            this.Barcode拍攝子流程C_VisionBypass.TabIndex = 9;
            // 
            // Barcode教讀_VisionBypass
            // 
            this.Barcode教讀_VisionBypass.BackColor = System.Drawing.Color.Cyan;
            this.Barcode教讀_VisionBypass.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Barcode教讀_VisionBypass.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Barcode教讀_VisionBypass.DoNotStopAtThisProcess = false;
            this.Barcode教讀_VisionBypass.ErrorDescription = "";
            this.Barcode教讀_VisionBypass.ExceptionWaitTime = 100;
            this.Barcode教讀_VisionBypass.LastExecuteMs = ((long)(0));
            this.Barcode教讀_VisionBypass.Location = new System.Drawing.Point(266, 289);
            this.Barcode教讀_VisionBypass.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Barcode教讀_VisionBypass.Name = "Barcode教讀_VisionBypass";
            this.Barcode教讀_VisionBypass.NextProcess = this.是否拍攝Barcode_VisionBypass;
            this.Barcode教讀_VisionBypass.Size = new System.Drawing.Size(100, 40);
            this.Barcode教讀_VisionBypass.StopWhenError = true;
            this.Barcode教讀_VisionBypass.TabIndex = 12;
            this.Barcode教讀_VisionBypass.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Barcode教讀_ProcessIn);
            // 
            // 拍攝點位收集
            // 
            this.拍攝點位收集.BackColor = System.Drawing.Color.Cyan;
            this.拍攝點位收集.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.拍攝點位收集.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.拍攝點位收集.DoNotStopAtThisProcess = false;
            this.拍攝點位收集.ErrorDescription = "";
            this.拍攝點位收集.ExceptionWaitTime = 100;
            this.拍攝點位收集.LastExecuteMs = ((long)(0));
            this.拍攝點位收集.Location = new System.Drawing.Point(3, 434);
            this.拍攝點位收集.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.拍攝點位收集.Name = "拍攝點位收集";
            this.拍攝點位收集.NextProcess = this.點位移動取像;
            this.拍攝點位收集.Size = new System.Drawing.Size(126, 10);
            this.拍攝點位收集.StopWhenError = true;
            this.拍攝點位收集.TabIndex = 81;
            this.拍攝點位收集.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.拍攝點位收集_ProcessIn);
            // 
            // 點位移動取像
            // 
            this.點位移動取像.BackColor = System.Drawing.Color.Cyan;
            this.點位移動取像.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.點位移動取像.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.點位移動取像.DoNotStopAtThisProcess = false;
            this.點位移動取像.ErrorDescription = "";
            this.點位移動取像.ExceptionWaitTime = 100;
            this.點位移動取像.LastExecuteMs = ((long)(0));
            this.點位移動取像.Location = new System.Drawing.Point(3, 478);
            this.點位移動取像.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.點位移動取像.Name = "點位移動取像";
            this.點位移動取像.NextProcess = this.Block偵測;
            this.點位移動取像.Size = new System.Drawing.Size(126, 10);
            this.點位移動取像.StopWhenError = true;
            this.點位移動取像.TabIndex = 80;
            this.點位移動取像.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.點位移動_ProcessIn);
            // 
            // Block偵測
            // 
            this.Block偵測.BackColor = System.Drawing.Color.Cyan;
            this.Block偵測.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.Block偵測.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.Block偵測.DoNotStopAtThisProcess = false;
            this.Block偵測.ErrorDescription = "";
            this.Block偵測.ExceptionWaitTime = 100;
            this.Block偵測.LastExecuteMs = ((long)(0));
            this.Block偵測.Location = new System.Drawing.Point(3, 514);
            this.Block偵測.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.Block偵測.Name = "Block偵測";
            this.Block偵測.NextProcess = this.雷射高度偵測;
            this.Block偵測.Size = new System.Drawing.Size(126, 10);
            this.Block偵測.StopWhenError = true;
            this.Block偵測.TabIndex = 76;
            this.Block偵測.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.Block偵測_ProcessIn);
            // 
            // 雷射高度偵測
            // 
            this.雷射高度偵測.BackColor = System.Drawing.Color.Cyan;
            this.雷射高度偵測.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.雷射高度偵測.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.雷射高度偵測.DoNotStopAtThisProcess = false;
            this.雷射高度偵測.ErrorDescription = "";
            this.雷射高度偵測.ExceptionWaitTime = 100;
            this.雷射高度偵測.LastExecuteMs = ((long)(0));
            this.雷射高度偵測.Location = new System.Drawing.Point(3, 544);
            this.雷射高度偵測.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.雷射高度偵測.Name = "雷射高度偵測";
            this.雷射高度偵測.Size = new System.Drawing.Size(126, 10);
            this.雷射高度偵測.StopWhenError = true;
            this.雷射高度偵測.TabIndex = 77;
            this.雷射高度偵測.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.雷射高度偵測_ProcessIn);
            // 
            // 雷射測高開始
            // 
            this.雷射測高開始.Controls.Add(this.tabPage1);
            this.雷射測高開始.Controls.Add(this.tabPage2);
            this.雷射測高開始.Controls.Add(this.tabPage3);
            this.雷射測高開始.Controls.Add(this.tabPage4);
            this.雷射測高開始.Controls.Add(this.tabPage5);
            this.雷射測高開始.Controls.Add(this.tabPage6);
            this.雷射測高開始.Location = new System.Drawing.Point(942, 52);
            this.雷射測高開始.Name = "雷射測高開始";
            this.雷射測高開始.SelectedIndex = 0;
            this.雷射測高開始.Size = new System.Drawing.Size(751, 793);
            this.雷射測高開始.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.controlFlow1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(743, 767);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "小產品拍攝子流程";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // controlFlow1
            // 
            this.controlFlow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow1.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow1.Controls.Add(this.SmallProduct_No1);
            this.controlFlow1.Controls.Add(this.SmallProduct_Yes1);
            this.controlFlow1.Controls.Add(this.移至飛拍結束位置);
            this.controlFlow1.Controls.Add(this.建立TriggerTable);
            this.controlFlow1.Controls.Add(this.移至飛拍起始位置);
            this.controlFlow1.Controls.Add(this.下IP);
            this.controlFlow1.Controls.Add(this.移動點拍起始位置);
            this.controlFlow1.Controls.Add(this.是否飛拍);
            this.controlFlow1.Controls.Add(this.小產品拍攝開始);
            this.controlFlow1.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow1.Location = new System.Drawing.Point(3, 3);
            this.controlFlow1.Name = "controlFlow1";
            this.controlFlow1.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow1.RefreshAllowed = false;
            this.controlFlow1.Size = new System.Drawing.Size(737, 761);
            this.controlFlow1.TabIndex = 0;
            // 
            // SmallProduct_No1
            // 
            this.SmallProduct_No1.AutoSize = true;
            this.SmallProduct_No1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SmallProduct_No1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SmallProduct_No1.Location = new System.Drawing.Point(183, 272);
            this.SmallProduct_No1.Name = "SmallProduct_No1";
            this.SmallProduct_No1.Size = new System.Drawing.Size(27, 18);
            this.SmallProduct_No1.TabIndex = 40;
            this.SmallProduct_No1.Text = "No";
            // 
            // SmallProduct_Yes1
            // 
            this.SmallProduct_Yes1.AutoSize = true;
            this.SmallProduct_Yes1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SmallProduct_Yes1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SmallProduct_Yes1.Location = new System.Drawing.Point(245, 218);
            this.SmallProduct_Yes1.Name = "SmallProduct_Yes1";
            this.SmallProduct_Yes1.Size = new System.Drawing.Size(29, 18);
            this.SmallProduct_Yes1.TabIndex = 39;
            this.SmallProduct_Yes1.Text = "Yes";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.controlFlow2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(743, 767);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "大產品拍攝子流程";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // controlFlow2
            // 
            this.controlFlow2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow2.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow2.Controls.Add(this.是否所有Map都拍完);
            this.controlFlow2.Controls.Add(this.Mosaic下SG);
            this.controlFlow2.Controls.Add(this.分區下SG);
            this.controlFlow2.Controls.Add(this.是否所有影像拍完);
            this.controlFlow2.Controls.Add(this.下IP拍照2);
            this.controlFlow2.Controls.Add(this.移動指定位置);
            this.controlFlow2.Controls.Add(this.是否組圖);
            this.controlFlow2.Controls.Add(this.大產品拍攝完畢);
            this.controlFlow2.Controls.Add(this.是否產品影像拍攝完成);
            this.controlFlow2.Controls.Add(this.下IP拍照);
            this.controlFlow2.Controls.Add(this.移至拍攝起始位置);
            this.controlFlow2.Controls.Add(this.大產品拍攝開始);
            this.controlFlow2.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow2.Location = new System.Drawing.Point(3, 3);
            this.controlFlow2.Name = "controlFlow2";
            this.controlFlow2.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow2.RefreshAllowed = false;
            this.controlFlow2.Size = new System.Drawing.Size(737, 761);
            this.controlFlow2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.controlFlow3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(743, 767);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "FailAlarm子流程";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // controlFlow3
            // 
            this.controlFlow3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow3.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow3.Controls.Add(this.流道軸移動回檢測位置);
            this.controlFlow3.Controls.Add(this.流道軸移動至人員確認位置);
            this.controlFlow3.Controls.Add(this.流道偵測產品是否存在);
            this.controlFlow3.Controls.Add(this.Boat盤限制開啟);
            this.controlFlow3.Controls.Add(this.人員確認);
            this.controlFlow3.Controls.Add(this.Boat盤限制解除);
            this.controlFlow3.Controls.Add(this.FailAlarm開始);
            this.controlFlow3.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow3.Location = new System.Drawing.Point(0, 0);
            this.controlFlow3.Name = "controlFlow3";
            this.controlFlow3.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow3.RefreshAllowed = false;
            this.controlFlow3.Size = new System.Drawing.Size(743, 767);
            this.controlFlow3.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.controlFlow4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(743, 767);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "視覺Bypass之Barcode拍攝子流程";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // controlFlow4
            // 
            this.controlFlow4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow4.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow4.Controls.Add(this.VB_BarcodeCatch_No2);
            this.controlFlow4.Controls.Add(this.VB_BarcodeCatch_Yes2);
            this.controlFlow4.Controls.Add(this.VB_BarcodeCatch_No1);
            this.controlFlow4.Controls.Add(this.VB_BarcodeCatch_Yes1);
            this.controlFlow4.Controls.Add(this.是否拍攝Barcode_VisionBypass);
            this.controlFlow4.Controls.Add(this.Barcode教讀_VisionBypass);
            this.controlFlow4.Controls.Add(this.是否Barcode教讀_VisionBypass);
            this.controlFlow4.Controls.Add(this.通知流道模組_VisionBypass);
            this.controlFlow4.Controls.Add(this.Barcode拍攝子流程C_VisionBypass);
            this.controlFlow4.Controls.Add(this.等待載盤進入_VisionBypass);
            this.controlFlow4.Controls.Add(this.視覺Bypass之Barcode拍攝開始);
            this.controlFlow4.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow4.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow4.Location = new System.Drawing.Point(0, 0);
            this.controlFlow4.Name = "controlFlow4";
            this.controlFlow4.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow4.RefreshAllowed = false;
            this.controlFlow4.Size = new System.Drawing.Size(743, 767);
            this.controlFlow4.TabIndex = 0;
            // 
            // VB_BarcodeCatch_No2
            // 
            this.VB_BarcodeCatch_No2.AutoSize = true;
            this.VB_BarcodeCatch_No2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VB_BarcodeCatch_No2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.VB_BarcodeCatch_No2.Location = new System.Drawing.Point(178, 438);
            this.VB_BarcodeCatch_No2.Name = "VB_BarcodeCatch_No2";
            this.VB_BarcodeCatch_No2.Size = new System.Drawing.Size(27, 18);
            this.VB_BarcodeCatch_No2.TabIndex = 72;
            this.VB_BarcodeCatch_No2.Text = "No";
            // 
            // VB_BarcodeCatch_Yes2
            // 
            this.VB_BarcodeCatch_Yes2.AutoSize = true;
            this.VB_BarcodeCatch_Yes2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VB_BarcodeCatch_Yes2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.VB_BarcodeCatch_Yes2.Location = new System.Drawing.Point(261, 369);
            this.VB_BarcodeCatch_Yes2.Name = "VB_BarcodeCatch_Yes2";
            this.VB_BarcodeCatch_Yes2.Size = new System.Drawing.Size(29, 18);
            this.VB_BarcodeCatch_Yes2.TabIndex = 71;
            this.VB_BarcodeCatch_Yes2.Text = "Yes";
            // 
            // VB_BarcodeCatch_No1
            // 
            this.VB_BarcodeCatch_No1.AutoSize = true;
            this.VB_BarcodeCatch_No1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VB_BarcodeCatch_No1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.VB_BarcodeCatch_No1.Location = new System.Drawing.Point(178, 291);
            this.VB_BarcodeCatch_No1.Name = "VB_BarcodeCatch_No1";
            this.VB_BarcodeCatch_No1.Size = new System.Drawing.Size(27, 18);
            this.VB_BarcodeCatch_No1.TabIndex = 70;
            this.VB_BarcodeCatch_No1.Text = "No";
            // 
            // VB_BarcodeCatch_Yes1
            // 
            this.VB_BarcodeCatch_Yes1.AutoSize = true;
            this.VB_BarcodeCatch_Yes1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VB_BarcodeCatch_Yes1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.VB_BarcodeCatch_Yes1.Location = new System.Drawing.Point(268, 225);
            this.VB_BarcodeCatch_Yes1.Name = "VB_BarcodeCatch_Yes1";
            this.VB_BarcodeCatch_Yes1.Size = new System.Drawing.Size(29, 18);
            this.VB_BarcodeCatch_Yes1.TabIndex = 55;
            this.VB_BarcodeCatch_Yes1.Text = "Yes";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.controlFlow5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(743, 767);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Barcode拍攝子流程";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // controlFlow5
            // 
            this.controlFlow5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow5.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow5.Controls.Add(this.BarcodeCatch_No2);
            this.controlFlow5.Controls.Add(this.BarcodeCatch_Yes2);
            this.controlFlow5.Controls.Add(this.BarcodeCatch_No1);
            this.controlFlow5.Controls.Add(this.BarcodeCatch_Yes1);
            this.controlFlow5.Controls.Add(this.移至Barcode位置);
            this.controlFlow5.Controls.Add(this.儲存Barcode資訊);
            this.controlFlow5.Controls.Add(this.手動輸入Barcode);
            this.controlFlow5.Controls.Add(this.是否讀取成功);
            this.controlFlow5.Controls.Add(this.相機拍攝Barcode);
            this.controlFlow5.Controls.Add(this.是否使用相機拍攝);
            this.controlFlow5.Controls.Add(this.Barcode拍攝開始);
            this.controlFlow5.Controls.Add(this.Barcode機拍攝Barcode);
            this.controlFlow5.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlFlow5.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow5.Location = new System.Drawing.Point(0, 0);
            this.controlFlow5.Name = "controlFlow5";
            this.controlFlow5.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow5.RefreshAllowed = false;
            this.controlFlow5.Size = new System.Drawing.Size(743, 767);
            this.controlFlow5.TabIndex = 0;
            // 
            // BarcodeCatch_No2
            // 
            this.BarcodeCatch_No2.AutoSize = true;
            this.BarcodeCatch_No2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarcodeCatch_No2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BarcodeCatch_No2.Location = new System.Drawing.Point(278, 376);
            this.BarcodeCatch_No2.Name = "BarcodeCatch_No2";
            this.BarcodeCatch_No2.Size = new System.Drawing.Size(27, 18);
            this.BarcodeCatch_No2.TabIndex = 72;
            this.BarcodeCatch_No2.Text = "No";
            // 
            // BarcodeCatch_Yes2
            // 
            this.BarcodeCatch_Yes2.AutoSize = true;
            this.BarcodeCatch_Yes2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarcodeCatch_Yes2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BarcodeCatch_Yes2.Location = new System.Drawing.Point(186, 443);
            this.BarcodeCatch_Yes2.Name = "BarcodeCatch_Yes2";
            this.BarcodeCatch_Yes2.Size = new System.Drawing.Size(29, 18);
            this.BarcodeCatch_Yes2.TabIndex = 71;
            this.BarcodeCatch_Yes2.Text = "Yes";
            // 
            // BarcodeCatch_No1
            // 
            this.BarcodeCatch_No1.AutoSize = true;
            this.BarcodeCatch_No1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarcodeCatch_No1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BarcodeCatch_No1.Location = new System.Drawing.Point(128, 203);
            this.BarcodeCatch_No1.Name = "BarcodeCatch_No1";
            this.BarcodeCatch_No1.Size = new System.Drawing.Size(27, 18);
            this.BarcodeCatch_No1.TabIndex = 70;
            this.BarcodeCatch_No1.Text = "No";
            // 
            // BarcodeCatch_Yes1
            // 
            this.BarcodeCatch_Yes1.AutoSize = true;
            this.BarcodeCatch_Yes1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarcodeCatch_Yes1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BarcodeCatch_Yes1.Location = new System.Drawing.Point(282, 202);
            this.BarcodeCatch_Yes1.Name = "BarcodeCatch_Yes1";
            this.BarcodeCatch_Yes1.Size = new System.Drawing.Size(29, 18);
            this.BarcodeCatch_Yes1.TabIndex = 70;
            this.BarcodeCatch_Yes1.Text = "Yes";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.controlFlow6);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(743, 767);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "雷射測高子流程";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // controlFlow6
            // 
            this.controlFlow6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(191)))), ((int)(((byte)(214)))));
            this.controlFlow6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlFlow6.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.controlFlow6.Controls.Add(this.雷射關閉);
            this.controlFlow6.Controls.Add(this.雷射開啟);
            this.controlFlow6.Controls.Add(this.雷射段差結束);
            this.controlFlow6.Controls.Add(this.是否所有Map區域都掃完);
            this.controlFlow6.Controls.Add(this.移至Map區域結束位置);
            this.controlFlow6.Controls.Add(this.移至Map區域開始位置);
            this.controlFlow6.Controls.Add(this.是否開啟雷射測高);
            this.controlFlow6.Controls.Add(this.拍攝點位收集);
            this.controlFlow6.Controls.Add(this.點位移動取像);
            this.controlFlow6.Controls.Add(this.雷射高度偵測);
            this.controlFlow6.Controls.Add(this.Block偵測);
            this.controlFlow6.Controls.Add(this.雷射測高流程開始);
            this.controlFlow6.CreateType = ControlFlow.Controls.CreateTypeEm.Task;
            this.controlFlow6.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.controlFlow6.Location = new System.Drawing.Point(3, 3);
            this.controlFlow6.Name = "controlFlow6";
            this.controlFlow6.Priority = System.Threading.ThreadPriority.Normal;
            this.controlFlow6.RefreshAllowed = false;
            this.controlFlow6.Size = new System.Drawing.Size(671, 607);
            this.controlFlow6.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(948, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(657, 34);
            this.label1.TabIndex = 39;
            this.label1.Text = "子流程區域";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // No1
            // 
            this.No1.AutoSize = true;
            this.No1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No1.Location = new System.Drawing.Point(263, 584);
            this.No1.Name = "No1";
            this.No1.Size = new System.Drawing.Size(27, 18);
            this.No1.TabIndex = 41;
            this.No1.Text = "No";
            // 
            // Yes1
            // 
            this.Yes1.AutoSize = true;
            this.Yes1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes1.Location = new System.Drawing.Point(310, 630);
            this.Yes1.Name = "Yes1";
            this.Yes1.Size = new System.Drawing.Size(29, 18);
            this.Yes1.TabIndex = 40;
            this.Yes1.Text = "Yes";
            // 
            // No2
            // 
            this.No2.AutoSize = true;
            this.No2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No2.Location = new System.Drawing.Point(276, 512);
            this.No2.Name = "No2";
            this.No2.Size = new System.Drawing.Size(27, 18);
            this.No2.TabIndex = 43;
            this.No2.Text = "No";
            // 
            // Yes2
            // 
            this.Yes2.AutoSize = true;
            this.Yes2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes2.Location = new System.Drawing.Point(358, 531);
            this.Yes2.Name = "Yes2";
            this.Yes2.Size = new System.Drawing.Size(29, 18);
            this.Yes2.TabIndex = 42;
            this.Yes2.Text = "Yes";
            // 
            // No5
            // 
            this.No5.AutoSize = true;
            this.No5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No5.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No5.Location = new System.Drawing.Point(594, 714);
            this.No5.Name = "No5";
            this.No5.Size = new System.Drawing.Size(27, 18);
            this.No5.TabIndex = 45;
            this.No5.Text = "No";
            // 
            // Yes5
            // 
            this.Yes5.AutoSize = true;
            this.Yes5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes5.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes5.Location = new System.Drawing.Point(546, 618);
            this.Yes5.Name = "Yes5";
            this.Yes5.Size = new System.Drawing.Size(29, 18);
            this.Yes5.TabIndex = 44;
            this.Yes5.Text = "Yes";
            // 
            // No4
            // 
            this.No4.AutoSize = true;
            this.No4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No4.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No4.Location = new System.Drawing.Point(509, 291);
            this.No4.Name = "No4";
            this.No4.Size = new System.Drawing.Size(27, 18);
            this.No4.TabIndex = 47;
            this.No4.Text = "No";
            // 
            // Yes4
            // 
            this.Yes4.AutoSize = true;
            this.Yes4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes4.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes4.Location = new System.Drawing.Point(637, 338);
            this.Yes4.Name = "Yes4";
            this.Yes4.Size = new System.Drawing.Size(29, 18);
            this.Yes4.TabIndex = 46;
            this.Yes4.Text = "Yes";
            // 
            // No3
            // 
            this.No3.AutoSize = true;
            this.No3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No3.Location = new System.Drawing.Point(452, 162);
            this.No3.Name = "No3";
            this.No3.Size = new System.Drawing.Size(27, 18);
            this.No3.TabIndex = 49;
            this.No3.Text = "No";
            // 
            // Yes3
            // 
            this.Yes3.AutoSize = true;
            this.Yes3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes3.Location = new System.Drawing.Point(451, 66);
            this.Yes3.Name = "Yes3";
            this.Yes3.Size = new System.Drawing.Size(29, 18);
            this.Yes3.TabIndex = 48;
            this.Yes3.Text = "Yes";
            // 
            // No7
            // 
            this.No7.AutoSize = true;
            this.No7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No7.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No7.Location = new System.Drawing.Point(594, 558);
            this.No7.Name = "No7";
            this.No7.Size = new System.Drawing.Size(27, 18);
            this.No7.TabIndex = 58;
            this.No7.Text = "No";
            // 
            // Yes7
            // 
            this.Yes7.AutoSize = true;
            this.Yes7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes7.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes7.Location = new System.Drawing.Point(696, 513);
            this.Yes7.Name = "Yes7";
            this.Yes7.Size = new System.Drawing.Size(29, 18);
            this.Yes7.TabIndex = 59;
            this.Yes7.Text = "Yes";
            // 
            // No8
            // 
            this.No8.AutoSize = true;
            this.No8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No8.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No8.Location = new System.Drawing.Point(52, 170);
            this.No8.Name = "No8";
            this.No8.Size = new System.Drawing.Size(27, 18);
            this.No8.TabIndex = 64;
            this.No8.Text = "No";
            // 
            // Yes8
            // 
            this.Yes8.AutoSize = true;
            this.Yes8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes8.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes8.Location = new System.Drawing.Point(154, 113);
            this.Yes8.Name = "Yes8";
            this.Yes8.Size = new System.Drawing.Size(29, 18);
            this.Yes8.TabIndex = 65;
            this.Yes8.Text = "Yes";
            // 
            // Yes9
            // 
            this.Yes9.AutoSize = true;
            this.Yes9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes9.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes9.Location = new System.Drawing.Point(145, 534);
            this.Yes9.Name = "Yes9";
            this.Yes9.Size = new System.Drawing.Size(29, 18);
            this.Yes9.TabIndex = 66;
            this.Yes9.Text = "Yes";
            // 
            // No9
            // 
            this.No9.AutoSize = true;
            this.No9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No9.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No9.Location = new System.Drawing.Point(52, 583);
            this.No9.Name = "No9";
            this.No9.Size = new System.Drawing.Size(27, 18);
            this.No9.TabIndex = 67;
            this.No9.Text = "No";
            // 
            // Yes10
            // 
            this.Yes10.AutoSize = true;
            this.Yes10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Yes10.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Yes10.Location = new System.Drawing.Point(50, 709);
            this.Yes10.Name = "Yes10";
            this.Yes10.Size = new System.Drawing.Size(29, 18);
            this.Yes10.TabIndex = 68;
            this.Yes10.Text = "Yes";
            // 
            // No10
            // 
            this.No10.AutoSize = true;
            this.No10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.No10.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.No10.Location = new System.Drawing.Point(142, 661);
            this.No10.Name = "No10";
            this.No10.Size = new System.Drawing.Size(27, 18);
            this.No10.TabIndex = 69;
            this.No10.Text = "No";
            // 
            // InspectionModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.確認此回合影像是否收齊);
            this.Controls.Add(this.灰卡光衰流程);
            this.Controls.Add(this.雷射測高子流程);
            this.Controls.Add(this.No10);
            this.Controls.Add(this.Yes10);
            this.Controls.Add(this.No9);
            this.Controls.Add(this.Yes9);
            this.Controls.Add(this.Yes8);
            this.Controls.Add(this.No8);
            this.Controls.Add(this.Barcode拍攝子流程C);
            this.Controls.Add(this.是否拍攝Barcode);
            this.Controls.Add(this.Barcode教讀);
            this.Controls.Add(this.是否Barcode教讀);
            this.Controls.Add(this.Yes7);
            this.Controls.Add(this.No7);
            this.Controls.Add(this.是否FailAlarm確認);
            this.Controls.Add(this.FailAlarm子流程C);
            this.Controls.Add(this.MainFlow_Node1);
            this.Controls.Add(this.此回合是否拍攝完畢);
            this.Controls.Add(this.No3);
            this.Controls.Add(this.Yes3);
            this.Controls.Add(this.No4);
            this.Controls.Add(this.Yes4);
            this.Controls.Add(this.No5);
            this.Controls.Add(this.Yes5);
            this.Controls.Add(this.No2);
            this.Controls.Add(this.Yes2);
            this.Controls.Add(this.No1);
            this.Controls.Add(this.Yes1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.雷射測高開始);
            this.Controls.Add(this.通知流道模組);
            this.Controls.Add(this.是否重測);
            this.Controls.Add(this.確認產品結果是否OK);
            this.Controls.Add(this.等待所有結果產出);
            this.Controls.Add(this.是否全部產品拍攝完成);
            this.Controls.Add(this.小產品拍攝子流程C);
            this.Controls.Add(this.大產品拍攝子流程C);
            this.Controls.Add(this.是否為大產品);
            this.Controls.Add(this.讀取SG資訊);
            this.Controls.Add(this.建立Grab_Quene);
            this.Controls.Add(this.讀取IR1資訊);
            this.Controls.Add(this.建立SG_Quene);
            this.Controls.Add(this.建立Single_SG_Quene);
            this.Controls.Add(this.是否抽檢);
            this.Controls.Add(this.教讀);
            this.Controls.Add(this.是否教讀);
            this.Controls.Add(this.等待載盤進入);
            this.Controls.Add(this.讀取檢測序列資訊IR_FN_NL);
            this.Controls.Add(this.所有軸移動到Standby位置);
            this.Controls.Add(this.WCF通訊上線);
            this.Controls.Add(this.視覺Bypass之Barcode拍攝子流程C);
            this.Controls.Add(this.視覺是否bypass);
            this.Controls.Add(this.檢測流程開始);
            this.Name = "InspectionModule";
            this.Size = new System.Drawing.Size(1708, 946);
            this.Controls.SetChildIndex(this.檢測流程開始, 0);
            this.Controls.SetChildIndex(this.視覺是否bypass, 0);
            this.Controls.SetChildIndex(this.視覺Bypass之Barcode拍攝子流程C, 0);
            this.Controls.SetChildIndex(this.WCF通訊上線, 0);
            this.Controls.SetChildIndex(this.所有軸移動到Standby位置, 0);
            this.Controls.SetChildIndex(this.讀取檢測序列資訊IR_FN_NL, 0);
            this.Controls.SetChildIndex(this.等待載盤進入, 0);
            this.Controls.SetChildIndex(this.是否教讀, 0);
            this.Controls.SetChildIndex(this.教讀, 0);
            this.Controls.SetChildIndex(this.是否抽檢, 0);
            this.Controls.SetChildIndex(this.建立Single_SG_Quene, 0);
            this.Controls.SetChildIndex(this.建立SG_Quene, 0);
            this.Controls.SetChildIndex(this.讀取IR1資訊, 0);
            this.Controls.SetChildIndex(this.建立Grab_Quene, 0);
            this.Controls.SetChildIndex(this.讀取SG資訊, 0);
            this.Controls.SetChildIndex(this.是否為大產品, 0);
            this.Controls.SetChildIndex(this.大產品拍攝子流程C, 0);
            this.Controls.SetChildIndex(this.小產品拍攝子流程C, 0);
            this.Controls.SetChildIndex(this.是否全部產品拍攝完成, 0);
            this.Controls.SetChildIndex(this.等待所有結果產出, 0);
            this.Controls.SetChildIndex(this.確認產品結果是否OK, 0);
            this.Controls.SetChildIndex(this.是否重測, 0);
            this.Controls.SetChildIndex(this.通知流道模組, 0);
            this.Controls.SetChildIndex(this.雷射測高開始, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.Yes1, 0);
            this.Controls.SetChildIndex(this.No1, 0);
            this.Controls.SetChildIndex(this.Yes2, 0);
            this.Controls.SetChildIndex(this.No2, 0);
            this.Controls.SetChildIndex(this.Yes5, 0);
            this.Controls.SetChildIndex(this.No5, 0);
            this.Controls.SetChildIndex(this.Yes4, 0);
            this.Controls.SetChildIndex(this.No4, 0);
            this.Controls.SetChildIndex(this.Yes3, 0);
            this.Controls.SetChildIndex(this.No3, 0);
            this.Controls.SetChildIndex(this.此回合是否拍攝完畢, 0);
            this.Controls.SetChildIndex(this.MainFlow_Node1, 0);
            this.Controls.SetChildIndex(this.FailAlarm子流程C, 0);
            this.Controls.SetChildIndex(this.是否FailAlarm確認, 0);
            this.Controls.SetChildIndex(this.No7, 0);
            this.Controls.SetChildIndex(this.Yes7, 0);
            this.Controls.SetChildIndex(this.是否Barcode教讀, 0);
            this.Controls.SetChildIndex(this.Barcode教讀, 0);
            this.Controls.SetChildIndex(this.是否拍攝Barcode, 0);
            this.Controls.SetChildIndex(this.Barcode拍攝子流程C, 0);
            this.Controls.SetChildIndex(this.No8, 0);
            this.Controls.SetChildIndex(this.Yes8, 0);
            this.Controls.SetChildIndex(this.Yes9, 0);
            this.Controls.SetChildIndex(this.No9, 0);
            this.Controls.SetChildIndex(this.Yes10, 0);
            this.Controls.SetChildIndex(this.No10, 0);
            this.Controls.SetChildIndex(this.雷射測高子流程, 0);
            this.Controls.SetChildIndex(this.灰卡光衰流程, 0);
            this.Controls.SetChildIndex(this.確認此回合影像是否收齊, 0);
            this.Controls.SetChildIndex(this.currentProcessString, 0);
            this.雷射測高開始.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.controlFlow1.ResumeLayout(false);
            this.controlFlow1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.controlFlow2.ResumeLayout(false);
            this.controlFlow2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.controlFlow3.ResumeLayout(false);
            this.controlFlow3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.controlFlow4.ResumeLayout(false);
            this.controlFlow4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.controlFlow5.ResumeLayout(false);
            this.controlFlow5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.controlFlow6.ResumeLayout(false);
            this.controlFlow6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlFlow.Controls.StartItem 檢測流程開始;
        private ControlFlow.Controls.BranchItem 視覺是否bypass;
        private ControlFlow.Controls.ProcessItem 視覺Bypass之Barcode拍攝子流程C;
        private ControlFlow.Controls.ProcessItem WCF通訊上線;
        private ControlFlow.Controls.ProcessItem 所有軸移動到Standby位置;
        private ControlFlow.Controls.ProcessItem 讀取檢測序列資訊IR_FN_NL;
        private ControlFlow.Controls.ProcessItem 等待載盤進入;
        private ControlFlow.Controls.BranchItem 是否教讀;
        private ControlFlow.Controls.ProcessItem 教讀;
        private ControlFlow.Controls.BranchItem 是否抽檢;
        private ControlFlow.Controls.ProcessItem 建立SG_Quene;
        private ControlFlow.Controls.ProcessItem 建立Grab_Quene;
        private ControlFlow.Controls.ProcessItem 建立Single_SG_Quene;
        private ControlFlow.Controls.ProcessItem 讀取IR1資訊;
        private ControlFlow.Controls.ProcessItem 讀取SG資訊;
        private ControlFlow.Controls.BranchItem 是否為大產品;
        private ControlFlow.Controls.ProcessItem 小產品拍攝子流程C;
        private ControlFlow.Controls.BranchItem 是否全部產品拍攝完成;
        private ControlFlow.Controls.ProcessItem 等待所有結果產出;
        private ControlFlow.Controls.ProcessItem 確認產品結果是否OK;
        private ControlFlow.Controls.BranchItem 是否重測;
        private ControlFlow.Controls.ProcessItem 大產品拍攝子流程C;
        private ControlFlow.Controls.ProcessItem 通知流道模組;
        private System.Windows.Forms.TabControl 雷射測高開始;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private ControlFlow.ControlFlow controlFlow1;
        private System.Windows.Forms.Label SmallProduct_No1;
        private System.Windows.Forms.Label SmallProduct_Yes1;
        private ControlFlow.Controls.ProcessItem 移至飛拍結束位置;
        private ControlFlow.Controls.ProcessItem 建立TriggerTable;
        private ControlFlow.Controls.ProcessItem 移至飛拍起始位置;
        private ControlFlow.Controls.ProcessItem 下IP;
        private ControlFlow.Controls.ProcessItem 移動點拍起始位置;
        private ControlFlow.Controls.BranchItem 是否飛拍;
        private ControlFlow.Controls.StartItem 小產品拍攝開始;
        private System.Windows.Forms.Label No1;
        private System.Windows.Forms.Label Yes1;
        private System.Windows.Forms.Label No2;
        private System.Windows.Forms.Label Yes2;
        private System.Windows.Forms.Label No5;
        private System.Windows.Forms.Label Yes5;
        private System.Windows.Forms.Label No4;
        private System.Windows.Forms.Label Yes4;
        private System.Windows.Forms.Label No3;
        private System.Windows.Forms.Label Yes3;
        private ControlFlow.Controls.ProcessItem Barcode機拍攝Barcode;
        private ControlFlow.Controls.BranchItem 此回合是否拍攝完畢;
        private ControlFlow.Controls.ProcessItem MainFlow_Node1;
        private System.Windows.Forms.TabPage tabPage3;
        private ControlFlow.ControlFlow controlFlow3;
        private ControlFlow.Controls.ProcessItem FailAlarm子流程C;
        private ControlFlow.Controls.StartItem FailAlarm開始;
        private ControlFlow.Controls.BranchItem 是否FailAlarm確認;
        private System.Windows.Forms.Label No7;
        private System.Windows.Forms.Label Yes7;
        private ControlFlow.Controls.ProcessItem Boat盤限制解除;
        private ControlFlow.Controls.ProcessItem 人員確認;
        private ControlFlow.Controls.ProcessItem 流道偵測產品是否存在;
        private ControlFlow.Controls.ProcessItem Boat盤限制開啟;
        private ControlFlow.Controls.ProcessItem 流道軸移動至人員確認位置;
        private ControlFlow.Controls.ProcessItem 流道軸移動回檢測位置;
        private System.Windows.Forms.TabPage tabPage4;
        private ControlFlow.ControlFlow controlFlow4;
        private ControlFlow.Controls.StartItem 視覺Bypass之Barcode拍攝開始;
        private ControlFlow.Controls.ProcessItem 等待載盤進入_VisionBypass;
        private ControlFlow.Controls.ProcessItem Barcode拍攝子流程C_VisionBypass;
        private ControlFlow.Controls.ProcessItem 通知流道模組_VisionBypass;
        private ControlFlow.Controls.BranchItem 是否Barcode教讀;
        private ControlFlow.Controls.ProcessItem Barcode教讀;
        private ControlFlow.Controls.BranchItem 是否拍攝Barcode;
        private System.Windows.Forms.TabPage tabPage5;
        private ControlFlow.ControlFlow controlFlow5;
        private ControlFlow.Controls.ProcessItem Barcode拍攝子流程C;
        private ControlFlow.Controls.BranchItem 是否使用相機拍攝;
        private ControlFlow.Controls.StartItem Barcode拍攝開始;
        private ControlFlow.Controls.ProcessItem 相機拍攝Barcode;
        private ControlFlow.Controls.ProcessItem 手動輸入Barcode;
        private ControlFlow.Controls.BranchItem 是否讀取成功;
        private ControlFlow.Controls.ProcessItem 儲存Barcode資訊;
        private ControlFlow.Controls.ProcessItem 移至Barcode位置;
        private ControlFlow.Controls.BranchItem 是否Barcode教讀_VisionBypass;
        private ControlFlow.Controls.ProcessItem Barcode教讀_VisionBypass;
        private ControlFlow.Controls.BranchItem 是否拍攝Barcode_VisionBypass;
        private System.Windows.Forms.Label No8;
        private System.Windows.Forms.Label Yes8;
        private System.Windows.Forms.Label Yes9;
        private System.Windows.Forms.Label VB_BarcodeCatch_No2;
        private System.Windows.Forms.Label VB_BarcodeCatch_Yes2;
        private System.Windows.Forms.Label VB_BarcodeCatch_No1;
        private System.Windows.Forms.Label VB_BarcodeCatch_Yes1;
        private System.Windows.Forms.Label BarcodeCatch_No1;
        private System.Windows.Forms.Label BarcodeCatch_Yes1;
        private System.Windows.Forms.Label No9;
        private System.Windows.Forms.Label Yes10;
        private System.Windows.Forms.Label No10;
        private System.Windows.Forms.Label BarcodeCatch_No2;
        private System.Windows.Forms.Label BarcodeCatch_Yes2;
        private ControlFlow.Controls.ProcessItem 雷射測高子流程;
        private System.Windows.Forms.TabPage tabPage6;
        private ControlFlow.ControlFlow controlFlow6;
        private ControlFlow.Controls.StartItem 雷射測高流程開始;
        private ControlFlow.Controls.ProcessItem 雷射高度偵測;
        private ControlFlow.Controls.ProcessItem Block偵測;
        private ControlFlow.Controls.ProcessItem 點位移動取像;
        private ControlFlow.Controls.ProcessItem 拍攝點位收集;
        private ControlFlow.Controls.ProcessItem 灰卡光衰流程;
        private ControlFlow.Controls.BranchItem 是否開啟雷射測高;
        private ControlFlow.Controls.ProcessItem 雷射段差結束;
        private ControlFlow.Controls.ProcessItem 移至Map區域開始位置;
        private ControlFlow.Controls.ProcessItem 雷射開啟;
        private ControlFlow.Controls.ProcessItem 移至Map區域結束位置;
        private ControlFlow.Controls.ProcessItem 雷射關閉;
        private ControlFlow.Controls.BranchItem 是否所有Map區域都掃完;
        private ControlFlow.Controls.StartItem 大產品拍攝開始;
        private ControlFlow.Controls.BranchItem 是否組圖;
        private ControlFlow.Controls.ProcessItem 分區下SG;
        private ControlFlow.Controls.ProcessItem 移動指定位置;
        private ControlFlow.Controls.ProcessItem 下IP拍照2;
        private ControlFlow.Controls.BranchItem 是否所有影像拍完;
        private ControlFlow.Controls.ProcessItem 大產品拍攝完畢;
        private ControlFlow.Controls.ProcessItem Mosaic下SG;
        private ControlFlow.Controls.ProcessItem 移至拍攝起始位置;
        private ControlFlow.Controls.ProcessItem 下IP拍照;
        private ControlFlow.Controls.BranchItem 是否產品影像拍攝完成;
        private ControlFlow.ControlFlow controlFlow2;
        private ControlFlow.Controls.ProcessItem 確認此回合影像是否收齊;
        private ControlFlow.Controls.BranchItem 是否所有Map都拍完;
    }
}
