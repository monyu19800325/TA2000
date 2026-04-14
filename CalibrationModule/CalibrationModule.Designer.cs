namespace TA2000Modules
{
    partial class CalibrationModule
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
            this.開始 = new ControlFlow.Controls.StartItem();
            this.結束 = new ControlFlow.Controls.ProcessItem();
            this.SuspendLayout();
            // 
            // 開始
            // 
            this.開始.BackColor = System.Drawing.Color.Chocolate;
            this.開始.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.開始.DoNotStopAtThisProcess = false;
            this.開始.ExceptionWaitTime = 100;
            this.開始.LastExecuteMs = ((long)(0));
            this.開始.Location = new System.Drawing.Point(122, 67);
            this.開始.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.開始.Name = "開始";
            this.開始.NextProcess = this.結束;
            this.開始.Size = new System.Drawing.Size(100, 40);
            this.開始.StopWhenError = true;
            this.開始.TabIndex = 1;
            this.開始.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.開始_ProcessIn);
            // 
            // 結束
            // 
            this.結束.BackColor = System.Drawing.Color.Cyan;
            this.結束.ChildState = ControlFlow.Controls.ChildStateEm.Done;
            this.結束.ConnectAnchor = ControlFlow.Controls.AnchorEm.None;
            this.結束.DoNotStopAtThisProcess = false;
            this.結束.ExceptionWaitTime = 100;
            this.結束.LastExecuteMs = ((long)(0));
            this.結束.Location = new System.Drawing.Point(122, 145);
            this.結束.LogType = ControlFlow.Controls.LogRecordType.Trace;
            this.結束.Name = "結束";
            this.結束.Size = new System.Drawing.Size(100, 40);
            this.結束.StopWhenError = true;
            this.結束.TabIndex = 2;
            this.結束.ProcessIn += new System.EventHandler<ControlFlow.Controls.ProcessArgs>(this.結束_ProcessIn);
            // 
            // CalibrationModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.結束);
            this.Controls.Add(this.開始);
            this.Name = "CalibrationModule";
            this.Controls.SetChildIndex(this.currentProcessString, 0);
            this.Controls.SetChildIndex(this.開始, 0);
            this.Controls.SetChildIndex(this.結束, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlFlow.Controls.StartItem 開始;
        private ControlFlow.Controls.ProcessItem 結束;
    }
}
