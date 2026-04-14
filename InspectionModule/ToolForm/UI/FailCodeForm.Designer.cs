namespace TA2000Modules
{
    partial class FailCodeForm
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
            this.LBC_Component = new DevExpress.XtraEditors.ListBoxControl();
            this.LBC_Spec = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.Btn_AddFailCode = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_BindFailCode = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_UnbindFailCode = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.LBC_BindSpec = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.LBC_Component)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LBC_Spec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LBC_BindSpec)).BeginInit();
            this.SuspendLayout();
            // 
            // LBC_Component
            // 
            this.LBC_Component.Location = new System.Drawing.Point(27, 103);
            this.LBC_Component.Name = "LBC_Component";
            this.LBC_Component.Size = new System.Drawing.Size(138, 237);
            this.LBC_Component.TabIndex = 0;
            // 
            // LBC_Spec
            // 
            this.LBC_Spec.Location = new System.Drawing.Point(189, 103);
            this.LBC_Spec.Name = "LBC_Spec";
            this.LBC_Spec.Size = new System.Drawing.Size(138, 237);
            this.LBC_Spec.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 83);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "檢測元件";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(189, 83);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Spec";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(399, 103);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(530, 237);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // Btn_AddFailCode
            // 
            this.Btn_AddFailCode.Location = new System.Drawing.Point(399, 65);
            this.Btn_AddFailCode.Name = "Btn_AddFailCode";
            this.Btn_AddFailCode.Size = new System.Drawing.Size(99, 32);
            this.Btn_AddFailCode.TabIndex = 5;
            this.Btn_AddFailCode.Text = "新增FailCode";
            // 
            // Btn_BindFailCode
            // 
            this.Btn_BindFailCode.Location = new System.Drawing.Point(620, 65);
            this.Btn_BindFailCode.Name = "Btn_BindFailCode";
            this.Btn_BindFailCode.Size = new System.Drawing.Size(99, 32);
            this.Btn_BindFailCode.TabIndex = 7;
            this.Btn_BindFailCode.Text = "綁定FailCode";
            // 
            // Btn_UnbindFailCode
            // 
            this.Btn_UnbindFailCode.Location = new System.Drawing.Point(734, 65);
            this.Btn_UnbindFailCode.Name = "Btn_UnbindFailCode";
            this.Btn_UnbindFailCode.Size = new System.Drawing.Size(99, 32);
            this.Btn_UnbindFailCode.TabIndex = 8;
            this.Btn_UnbindFailCode.Text = "解綁定FailCode";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(399, 30);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 14);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "MapIndex";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(471, 27);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Size = new System.Drawing.Size(100, 20);
            this.lookUpEdit1.TabIndex = 10;
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(830, 346);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(99, 32);
            this.Btn_Save.TabIndex = 11;
            this.Btn_Save.Text = "Save";
            // 
            // LBC_BindSpec
            // 
            this.LBC_BindSpec.Location = new System.Drawing.Point(931, 120);
            this.LBC_BindSpec.Name = "LBC_BindSpec";
            this.LBC_BindSpec.Size = new System.Drawing.Size(138, 220);
            this.LBC_BindSpec.TabIndex = 12;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(935, 100);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(54, 14);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Bind Spec";
            // 
            // FailCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 509);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.LBC_BindSpec);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.Btn_UnbindFailCode);
            this.Controls.Add(this.Btn_BindFailCode);
            this.Controls.Add(this.Btn_AddFailCode);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LBC_Spec);
            this.Controls.Add(this.LBC_Component);
            this.Name = "FailCodeForm";
            this.Text = "FailCodeForm";
            ((System.ComponentModel.ISupportInitialize)(this.LBC_Component)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LBC_Spec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LBC_BindSpec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl LBC_Component;
        private DevExpress.XtraEditors.ListBoxControl LBC_Spec;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.SimpleButton Btn_UnbindFailCode;
        private DevExpress.XtraEditors.SimpleButton Btn_BindFailCode;
        private DevExpress.XtraEditors.SimpleButton Btn_AddFailCode;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ListBoxControl LBC_BindSpec;
    }
}