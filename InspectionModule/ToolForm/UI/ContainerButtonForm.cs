using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    public partial class ContainerButtonForm : DevExpress.XtraEditors.XtraForm
    {
        public string ButtonResult = "";
        public ContainerButtonForm(Form form, string[] buttonsName = null)
        {
            InitializeComponent();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(form);
            form.Show();

            if(buttonsName == null)
            {
                SimpleButton simpleButton = new SimpleButton();
                simpleButton.Text = "Close";
                simpleButton.Dock = DockStyle.Fill;
                simpleButton.Click += (s, e) =>
                {
                    ButtonResult = "Close";
                    this.Close();
                };
                this.panel2.Controls.Add(simpleButton);
            }
            else
            {
                for (int i = 0; i < buttonsName.Length; i++)
                {
                    SimpleButton simpleButton = new SimpleButton();
                    simpleButton.Text = buttonsName[i];
                    simpleButton.Dock = DockStyle.Right;
                    //simpleButton.Width = this.panel2.Width / buttonsName.Length;
                    simpleButton.Click += (s, e) =>
                    {
                        ButtonResult = simpleButton.Text;
                        this.Close();
                    };
                    this.panel2.Controls.Add(simpleButton);
                }
            }
        }

        
    }
}
