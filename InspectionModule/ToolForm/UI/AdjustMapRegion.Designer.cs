namespace TA2000Modules
{
    partial class AdjustMapRegion
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
            this.hWindow_Map = new HalconDotNet.HWindowControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LUE_MapIndex = new DevExpress.XtraEditors.LookUpEdit();
            this.TE_ExtendedWidth = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.TE_ExtendedHeight = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.Btn_Set = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_MapIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ExtendedWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ExtendedHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindow_Map
            // 
            this.hWindow_Map.BackColor = System.Drawing.Color.Black;
            this.hWindow_Map.BorderColor = System.Drawing.Color.Black;
            this.hWindow_Map.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindow_Map.Location = new System.Drawing.Point(12, 12);
            this.hWindow_Map.Name = "hWindow_Map";
            this.hWindow_Map.Size = new System.Drawing.Size(511, 495);
            this.hWindow_Map.TabIndex = 2;
            this.hWindow_Map.WindowSize = new System.Drawing.Size(511, 495);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(559, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 14);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "選擇Index(Map上)";
            // 
            // LUE_MapIndex
            // 
            this.LUE_MapIndex.Location = new System.Drawing.Point(673, 9);
            this.LUE_MapIndex.Name = "LUE_MapIndex";
            this.LUE_MapIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LUE_MapIndex.Size = new System.Drawing.Size(89, 20);
            this.LUE_MapIndex.TabIndex = 11;
            // 
            // TE_ExtendedWidth
            // 
            this.TE_ExtendedWidth.Location = new System.Drawing.Point(664, 61);
            this.TE_ExtendedWidth.Name = "TE_ExtendedWidth";
            this.TE_ExtendedWidth.Size = new System.Drawing.Size(98, 20);
            this.TE_ExtendedWidth.TabIndex = 48;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(562, 64);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(90, 14);
            this.labelControl5.TabIndex = 49;
            this.labelControl5.Text = "Extended Width";
            // 
            // TE_ExtendedHeight
            // 
            this.TE_ExtendedHeight.Location = new System.Drawing.Point(664, 103);
            this.TE_ExtendedHeight.Name = "TE_ExtendedHeight";
            this.TE_ExtendedHeight.Size = new System.Drawing.Size(98, 20);
            this.TE_ExtendedHeight.TabIndex = 50;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(562, 106);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(93, 14);
            this.labelControl4.TabIndex = 51;
            this.labelControl4.Text = "Extended Height";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // Btn_Set
            // 
            this.Btn_Set.Location = new System.Drawing.Point(562, 151);
            this.Btn_Set.Name = "Btn_Set";
            this.Btn_Set.Size = new System.Drawing.Size(62, 31);
            this.Btn_Set.TabIndex = 52;
            this.Btn_Set.Text = "Set";
            // 
            // AdjustMapRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 519);
            this.Controls.Add(this.Btn_Set);
            this.Controls.Add(this.TE_ExtendedWidth);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.TE_ExtendedHeight);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LUE_MapIndex);
            this.Controls.Add(this.hWindow_Map);
            this.Name = "AdjustMapRegion";
            this.Text = "AdjustMapRegion";
            ((System.ComponentModel.ISupportInitialize)(this.LUE_MapIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ExtendedWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_ExtendedHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindow_Map;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit LUE_MapIndex;
        private DevExpress.XtraEditors.TextEdit TE_ExtendedWidth;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit TE_ExtendedHeight;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.SimpleButton Btn_Set;
    }
}