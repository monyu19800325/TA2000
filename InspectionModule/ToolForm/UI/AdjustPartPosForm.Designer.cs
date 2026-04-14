namespace TA2000Modules
{
    partial class AdjustPartPosForm
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
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.LUE_MosaicPartIndex = new DevExpress.XtraEditors.LookUpEdit();
            this.TE_OffsetY = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.TE_OffsetX = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.Btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.LUE_MosaicPartIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OffsetY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OffsetX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(73, 43);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(55, 14);
            this.labelControl12.TabIndex = 2;
            this.labelControl12.Text = "分區Index";
            // 
            // LUE_MosaicPartIndex
            // 
            this.LUE_MosaicPartIndex.Location = new System.Drawing.Point(160, 40);
            this.LUE_MosaicPartIndex.Name = "LUE_MosaicPartIndex";
            this.LUE_MosaicPartIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LUE_MosaicPartIndex.Size = new System.Drawing.Size(68, 20);
            this.LUE_MosaicPartIndex.TabIndex = 3;
            // 
            // TE_OffsetY
            // 
            this.TE_OffsetY.Location = new System.Drawing.Point(160, 116);
            this.TE_OffsetY.Name = "TE_OffsetY";
            this.TE_OffsetY.Size = new System.Drawing.Size(68, 20);
            this.TE_OffsetY.TabIndex = 61;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(73, 119);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(46, 14);
            this.labelControl15.TabIndex = 62;
            this.labelControl15.Text = "Offset Y";
            // 
            // TE_OffsetX
            // 
            this.TE_OffsetX.Location = new System.Drawing.Point(160, 93);
            this.TE_OffsetX.Name = "TE_OffsetX";
            this.TE_OffsetX.Size = new System.Drawing.Size(68, 20);
            this.TE_OffsetX.TabIndex = 59;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(73, 96);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(45, 14);
            this.labelControl14.TabIndex = 60;
            this.labelControl14.Text = "Offset X";
            // 
            // Btn_OK
            // 
            this.Btn_OK.Location = new System.Drawing.Point(73, 168);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(70, 23);
            this.Btn_OK.TabIndex = 63;
            this.Btn_OK.Text = "OK";
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(180, 168);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(74, 23);
            this.Btn_Cancel.TabIndex = 64;
            this.Btn_Cancel.Text = "Cancel";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // AdjustPartPosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 203);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.TE_OffsetY);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.TE_OffsetX);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.LUE_MosaicPartIndex);
            this.Name = "AdjustPartPosForm";
            this.Text = "AdjustPartPosForm";
            ((System.ComponentModel.ISupportInitialize)(this.LUE_MosaicPartIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OffsetY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_OffsetX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LookUpEdit LUE_MosaicPartIndex;
        private DevExpress.XtraEditors.TextEdit TE_OffsetY;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.TextEdit TE_OffsetX;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SimpleButton Btn_OK;
        private DevExpress.XtraEditors.SimpleButton Btn_Cancel;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}