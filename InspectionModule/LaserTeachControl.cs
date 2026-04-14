using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Mvvm.Native;
using DevExpress.Utils.MVVM;
using DevExpress.XtraEditors;
using HTA.Com.TCPIP;
using TA2000Modules;
using static TA2000Modules.LaserTeachControlViewModel;

namespace TA2000Modules
{
    public partial class LaserTeachControl : DevExpress.XtraEditors.XtraForm
    {
        public LaserTeachControl(InspectionModule module)
        {
            InitializeComponent();

            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module);

            //設定初始光源
            module.VisionController.Lighter.SetLight(module.ProductParam.Lights.ToArray());          
        }
      
        void InitializeBindings(InspectionModule module)
        {
            var fluent = mvvmContext1.OfType<LaserTeachControlViewModel>();
            fluent.ViewModel.Init(module);
            fluent.ViewModel.SetView(this);
            fluent.ViewModel.WindowControl = hWindowControl1;

            #region DataGridView Setting
            // DataGridView 設定（可視需求）         
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Height",
                HeaderText = "Height",
                DataPropertyName = "Height",
               DefaultCellStyle = { Format = "0.00" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Machine_Position_X",
                HeaderText = "X",
                DataPropertyName = "Machine_Position_X",
                DefaultCellStyle = { Format = "0.00" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Machine_Position_Y",
                HeaderText = "Y",
                DataPropertyName = "Machine_Position_Y",
                DefaultCellStyle = { Format = "0.00" }
            });
            dataGridView1.DataSource = fluent.ViewModel.LaserResultsSource; // 綁一次就好
            #endregion

            fluent.SetBinding(toggleSwitch1, t => t.IsOn, x => x.LiveOn);

            fluent.BindCommand(Btn_SetLight, x => x.SetLight());
            //fluent.BindCommand(Btn_SetROI, x => x.SetROI());
            fluent.BindCommand(Btn_SetTarget, x => x.SetTarget());
            fluent.BindCommand(Btn_SearchTarget, x => x.SearchTarget());
            fluent.BindCommand(Btn_Test, x => x.Test());
            //fluent.BindCommand(Btn_Reset, x => x.Reset());
            fluent.BindCommand(Btn_Save, x => x.Save());
            

            fluent.SetBinding(trackBar1, t => t.Value, x => x.ThresholdMin);
            fluent.SetBinding(trackBar2, t => t.Value, x => x.ThresholdMax);
            fluent.SetBinding(labelControl12, t => t.Text, x => x.ThresholdMax);
            fluent.SetBinding(labelControl13, t => t.Text, x => x.ThresholdMin);

            fluent.SetBinding(NUD_SearchScore, t => t.Value, x => x.SearchScore);
            fluent.SetBinding(NUD_HeightMax, t => t.Value, x => x.HeightMax);
            fluent.SetBinding(NUD_HeightMin, t => t.Value, x => x.HeightMin);


            FormClosing += (ss, ee) =>
            {
                fluent.ViewModel.Dispose();              
            };
        }
    }
}