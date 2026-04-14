namespace TA2000Modules
{
    partial class LaserTestForm
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
            this.TE_CX1Pos = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.TE_AY1Pos = new DevExpress.XtraEditors.TextEdit();
            this.Btn_Move = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.TE_Value = new DevExpress.XtraEditors.TextEdit();
            this.Btn_MeasureValue = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TE_CX1Pos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_AY1Pos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Value.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // TE_CX1Pos
            // 
            this.TE_CX1Pos.Location = new System.Drawing.Point(127, 50);
            this.TE_CX1Pos.Name = "TE_CX1Pos";
            this.TE_CX1Pos.Size = new System.Drawing.Size(100, 20);
            this.TE_CX1Pos.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(36, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "CX1 Position";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(36, 91);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "AY1 Position";
            // 
            // TE_AY1Pos
            // 
            this.TE_AY1Pos.Location = new System.Drawing.Point(127, 91);
            this.TE_AY1Pos.Name = "TE_AY1Pos";
            this.TE_AY1Pos.Size = new System.Drawing.Size(100, 20);
            this.TE_AY1Pos.TabIndex = 4;
            // 
            // Btn_Move
            // 
            this.Btn_Move.Location = new System.Drawing.Point(243, 50);
            this.Btn_Move.Name = "Btn_Move";
            this.Btn_Move.Size = new System.Drawing.Size(115, 61);
            this.Btn_Move.TabIndex = 6;
            this.Btn_Move.Text = "Move";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(52, 89);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 14);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Value";
            // 
            // TE_Value
            // 
            this.TE_Value.Location = new System.Drawing.Point(105, 86);
            this.TE_Value.Name = "TE_Value";
            this.TE_Value.Size = new System.Drawing.Size(100, 20);
            this.TE_Value.TabIndex = 8;
            // 
            // Btn_MeasureValue
            // 
            this.Btn_MeasureValue.Location = new System.Drawing.Point(221, 82);
            this.Btn_MeasureValue.Name = "Btn_MeasureValue";
            this.Btn_MeasureValue.Size = new System.Drawing.Size(115, 29);
            this.Btn_MeasureValue.TabIndex = 9;
            this.Btn_MeasureValue.Text = "Measure Value";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.Btn_Move);
            this.groupControl1.Controls.Add(this.TE_CX1Pos);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.TE_AY1Pos);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(424, 165);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Axis Control";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.Btn_MeasureValue);
            this.groupControl2.Controls.Add(this.TE_Value);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Location = new System.Drawing.Point(12, 199);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(424, 192);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "Laser Measure";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // LaserTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 421);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "LaserTestForm";
            this.Text = "LaserTestForm";
            ((System.ComponentModel.ISupportInitialize)(this.TE_CX1Pos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_AY1Pos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Value.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit TE_CX1Pos;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit TE_AY1Pos;
        private DevExpress.XtraEditors.SimpleButton Btn_Move;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit TE_Value;
        private DevExpress.XtraEditors.SimpleButton Btn_MeasureValue;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}