namespace TA2000Modules
{
    partial class TestMosaicForm
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
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LUE_CaptureIndex = new DevExpress.XtraEditors.LookUpEdit();
            this.SE_MosaicYCount = new DevExpress.XtraEditors.SpinEdit();
            this.SE_MosaicXCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.LUE_Vel = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.Btn_MoveCapture0 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.TE_Az1Pos = new DevExpress.XtraEditors.TextEdit();
            this.Btn_Az1Move = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_CalMosaic = new DevExpress.XtraEditors.SimpleButton();
            this.TE_CycleCount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Btn_Stop = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Run = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_CreateMap = new DevExpress.XtraEditors.SimpleButton();
            this.TE_Index0Y = new DevExpress.XtraEditors.TextEdit();
            this.Btn_Capture = new DevExpress.XtraEditors.SimpleButton();
            this.TE_Index0X = new DevExpress.XtraEditors.TextEdit();
            this.CB_SaveImage = new System.Windows.Forms.CheckBox();
            this.CB_UseSingleSeam = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_CaptureIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_MosaicYCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_MosaicXCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Vel.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Az1Pos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_CycleCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Index0Y.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Index0X.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(42, 123);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(125, 24);
            this.labelControl1.TabIndex = 94;
            this.labelControl1.Text = "CaptureIndex:";
            // 
            // LUE_CaptureIndex
            // 
            this.LUE_CaptureIndex.Location = new System.Drawing.Point(182, 120);
            this.LUE_CaptureIndex.Name = "LUE_CaptureIndex";
            this.LUE_CaptureIndex.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.LUE_CaptureIndex.Properties.Appearance.Options.UseFont = true;
            this.LUE_CaptureIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LUE_CaptureIndex.Size = new System.Drawing.Size(154, 30);
            this.LUE_CaptureIndex.TabIndex = 93;
            // 
            // SE_MosaicYCount
            // 
            this.SE_MosaicYCount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SE_MosaicYCount.Location = new System.Drawing.Point(451, 77);
            this.SE_MosaicYCount.Name = "SE_MosaicYCount";
            this.SE_MosaicYCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SE_MosaicYCount.Size = new System.Drawing.Size(76, 20);
            this.SE_MosaicYCount.TabIndex = 92;
            // 
            // SE_MosaicXCount
            // 
            this.SE_MosaicXCount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SE_MosaicXCount.Location = new System.Drawing.Point(192, 77);
            this.SE_MosaicXCount.Name = "SE_MosaicXCount";
            this.SE_MosaicXCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SE_MosaicXCount.Size = new System.Drawing.Size(76, 20);
            this.SE_MosaicXCount.TabIndex = 91;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(298, 73);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(144, 24);
            this.labelControl9.TabIndex = 90;
            this.labelControl9.Text = "Mosaic Y Count:";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(41, 73);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(145, 24);
            this.labelControl8.TabIndex = 89;
            this.labelControl8.Text = "Mosaic X Count:";
            // 
            // LUE_Vel
            // 
            this.LUE_Vel.Location = new System.Drawing.Point(216, 19);
            this.LUE_Vel.Name = "LUE_Vel";
            this.LUE_Vel.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.LUE_Vel.Properties.Appearance.Options.UseFont = true;
            this.LUE_Vel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LUE_Vel.Size = new System.Drawing.Size(154, 30);
            this.LUE_Vel.TabIndex = 88;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(38, 22);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(76, 24);
            this.labelControl7.TabIndex = 87;
            this.labelControl7.Text = "移動速度";
            // 
            // Btn_MoveCapture0
            // 
            this.Btn_MoveCapture0.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_MoveCapture0.Appearance.Options.UseFont = true;
            this.Btn_MoveCapture0.Location = new System.Drawing.Point(287, 165);
            this.Btn_MoveCapture0.Margin = new System.Windows.Forms.Padding(5);
            this.Btn_MoveCapture0.Name = "Btn_MoveCapture0";
            this.Btn_MoveCapture0.Size = new System.Drawing.Size(112, 30);
            this.Btn_MoveCapture0.TabIndex = 86;
            this.Btn_MoveCapture0.Text = "移動取像";
            // 
            // Btn_Save
            // 
            this.Btn_Save.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_Save.Appearance.Options.UseFont = true;
            this.Btn_Save.Location = new System.Drawing.Point(743, 140);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(156, 56);
            this.Btn_Save.TabIndex = 85;
            this.Btn_Save.Text = "儲存設定參數";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.TE_Az1Pos);
            this.groupBox1.Controls.Add(this.Btn_Az1Move);
            this.groupBox1.Location = new System.Drawing.Point(545, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 102);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "對焦相機";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(8, 39);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(5);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(79, 24);
            this.labelControl5.TabIndex = 28;
            this.labelControl5.Text = "AZ1 位置";
            // 
            // TE_Az1Pos
            // 
            this.TE_Az1Pos.Location = new System.Drawing.Point(132, 38);
            this.TE_Az1Pos.Margin = new System.Windows.Forms.Padding(5);
            this.TE_Az1Pos.Name = "TE_Az1Pos";
            this.TE_Az1Pos.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.TE_Az1Pos.Properties.Appearance.Options.UseFont = true;
            this.TE_Az1Pos.Size = new System.Drawing.Size(110, 30);
            this.TE_Az1Pos.TabIndex = 25;
            // 
            // Btn_Az1Move
            // 
            this.Btn_Az1Move.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_Az1Move.Appearance.Options.UseFont = true;
            this.Btn_Az1Move.Location = new System.Drawing.Point(250, 21);
            this.Btn_Az1Move.Name = "Btn_Az1Move";
            this.Btn_Az1Move.Size = new System.Drawing.Size(116, 47);
            this.Btn_Az1Move.TabIndex = 23;
            this.Btn_Az1Move.Text = "AZ1 移動";
            // 
            // Btn_CalMosaic
            // 
            this.Btn_CalMosaic.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_CalMosaic.Appearance.Options.UseFont = true;
            this.Btn_CalMosaic.Location = new System.Drawing.Point(743, 41);
            this.Btn_CalMosaic.Name = "Btn_CalMosaic";
            this.Btn_CalMosaic.Size = new System.Drawing.Size(156, 56);
            this.Btn_CalMosaic.TabIndex = 83;
            this.Btn_CalMosaic.Text = "計算組圖精度";
            // 
            // TE_CycleCount
            // 
            this.TE_CycleCount.Location = new System.Drawing.Point(643, 378);
            this.TE_CycleCount.Name = "TE_CycleCount";
            this.TE_CycleCount.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.TE_CycleCount.Properties.Appearance.Options.UseFont = true;
            this.TE_CycleCount.Size = new System.Drawing.Size(109, 30);
            this.TE_CycleCount.TabIndex = 82;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(547, 381);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 24);
            this.labelControl4.TabIndex = 81;
            this.labelControl4.Text = "Cycle次數:";
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_Stop.Appearance.Options.UseFont = true;
            this.Btn_Stop.Location = new System.Drawing.Point(722, 435);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(156, 56);
            this.Btn_Stop.TabIndex = 80;
            this.Btn_Stop.Text = "Stop Cycle";
            // 
            // Btn_Run
            // 
            this.Btn_Run.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_Run.Appearance.Options.UseFont = true;
            this.Btn_Run.Location = new System.Drawing.Point(543, 435);
            this.Btn_Run.Name = "Btn_Run";
            this.Btn_Run.Size = new System.Drawing.Size(156, 56);
            this.Btn_Run.TabIndex = 79;
            this.Btn_Run.Text = "Run Cycle";
            // 
            // Btn_CreateMap
            // 
            this.Btn_CreateMap.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_CreateMap.Appearance.Options.UseFont = true;
            this.Btn_CreateMap.Location = new System.Drawing.Point(545, 41);
            this.Btn_CreateMap.Name = "Btn_CreateMap";
            this.Btn_CreateMap.Size = new System.Drawing.Size(156, 56);
            this.Btn_CreateMap.TabIndex = 78;
            this.Btn_CreateMap.Text = "Create Map";
            // 
            // TE_Index0Y
            // 
            this.TE_Index0Y.Location = new System.Drawing.Point(170, 166);
            this.TE_Index0Y.Name = "TE_Index0Y";
            this.TE_Index0Y.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.TE_Index0Y.Properties.Appearance.Options.UseFont = true;
            this.TE_Index0Y.Size = new System.Drawing.Size(109, 30);
            this.TE_Index0Y.TabIndex = 77;
            // 
            // Btn_Capture
            // 
            this.Btn_Capture.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Btn_Capture.Appearance.Options.UseFont = true;
            this.Btn_Capture.Location = new System.Drawing.Point(545, 139);
            this.Btn_Capture.Name = "Btn_Capture";
            this.Btn_Capture.Size = new System.Drawing.Size(156, 56);
            this.Btn_Capture.TabIndex = 76;
            this.Btn_Capture.Text = "取像組圖";
            // 
            // TE_Index0X
            // 
            this.TE_Index0X.Location = new System.Drawing.Point(45, 166);
            this.TE_Index0X.Name = "TE_Index0X";
            this.TE_Index0X.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.TE_Index0X.Properties.Appearance.Options.UseFont = true;
            this.TE_Index0X.Size = new System.Drawing.Size(109, 30);
            this.TE_Index0X.TabIndex = 75;
            // 
            // CB_SaveImage
            // 
            this.CB_SaveImage.AutoSize = true;
            this.CB_SaveImage.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.CB_SaveImage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CB_SaveImage.Location = new System.Drawing.Point(773, 380);
            this.CB_SaveImage.Name = "CB_SaveImage";
            this.CB_SaveImage.Size = new System.Drawing.Size(105, 28);
            this.CB_SaveImage.TabIndex = 95;
            this.CB_SaveImage.Text = "是否存圖";
            this.CB_SaveImage.UseVisualStyleBackColor = true;
            // 
            // CB_UseSingleSeam
            // 
            this.CB_UseSingleSeam.AutoSize = true;
            this.CB_UseSingleSeam.Checked = true;
            this.CB_UseSingleSeam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_UseSingleSeam.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.CB_UseSingleSeam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CB_UseSingleSeam.Location = new System.Drawing.Point(38, 215);
            this.CB_UseSingleSeam.Name = "CB_UseSingleSeam";
            this.CB_UseSingleSeam.Size = new System.Drawing.Size(162, 28);
            this.CB_UseSingleSeam.TabIndex = 96;
            this.CB_UseSingleSeam.Text = "是否使用單接縫";
            this.CB_UseSingleSeam.UseVisualStyleBackColor = true;
            // 
            // TestMosaicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 511);
            this.Controls.Add(this.CB_UseSingleSeam);
            this.Controls.Add(this.CB_SaveImage);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LUE_CaptureIndex);
            this.Controls.Add(this.SE_MosaicYCount);
            this.Controls.Add(this.SE_MosaicXCount);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.LUE_Vel);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.Btn_MoveCapture0);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_CalMosaic);
            this.Controls.Add(this.TE_CycleCount);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.Btn_Stop);
            this.Controls.Add(this.Btn_Run);
            this.Controls.Add(this.Btn_CreateMap);
            this.Controls.Add(this.TE_Index0Y);
            this.Controls.Add(this.Btn_Capture);
            this.Controls.Add(this.TE_Index0X);
            this.Name = "TestMosaicForm";
            this.Text = "TestMosaicForm";
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_CaptureIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_MosaicYCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SE_MosaicXCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUE_Vel.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Az1Pos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_CycleCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Index0Y.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TE_Index0X.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit LUE_CaptureIndex;
        private DevExpress.XtraEditors.SpinEdit SE_MosaicYCount;
        private DevExpress.XtraEditors.SpinEdit SE_MosaicXCount;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit LUE_Vel;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton Btn_MoveCapture0;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit TE_Az1Pos;
        private DevExpress.XtraEditors.SimpleButton Btn_Az1Move;
        private DevExpress.XtraEditors.SimpleButton Btn_CalMosaic;
        private DevExpress.XtraEditors.TextEdit TE_CycleCount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton Btn_Stop;
        private DevExpress.XtraEditors.SimpleButton Btn_Run;
        private DevExpress.XtraEditors.SimpleButton Btn_CreateMap;
        private DevExpress.XtraEditors.TextEdit TE_Index0Y;
        private DevExpress.XtraEditors.SimpleButton Btn_Capture;
        private DevExpress.XtraEditors.TextEdit TE_Index0X;
        private System.Windows.Forms.CheckBox CB_SaveImage;
        private System.Windows.Forms.CheckBox CB_UseSingleSeam;
    }
}