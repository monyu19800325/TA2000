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
    public partial class BoatGlobalSettingForm : UserControl
    {
        public BoatGlobalSettingForm(MachineTraySetting _handle)
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = _handle;
        }


    }
}

