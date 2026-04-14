using HalconDotNet;
using HTA.IFramer;
using HTA.MainController;
using HTA.Utility.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules.ToolForm
{
    public partial class CurrentImageView : Form
    {
        int CurrentPartX = 0;
        int CurrentPartY = 0;
        private IMainController _mainController;

        public CurrentImageView(IMainController mainController)
        {
            InitializeComponent();
            _mainController = mainController;
            ((StationFramer)_mainController.Framer).OnGroupAllCaptured += OnImageGrabDone;
        }


        public void OnImageGrabDone(object sender, HTA.IFramer.StationCaptureArgs e)
        {
            if (this.Visible == true)
            {
                ShowTheImage(e.imgs);
            }
        }

        private void ShowTheImage(List<CustomImage> img)
        {
            //check the part
            img[0].GetImageSize(out int w, out int h);

            if (CurrentPartX != w || CurrentPartY != h)
            {
                CurrentPartX = w;
                CurrentPartY = h;
                hWindowControl1.SetFullImagePart(img[0]);               
            }

            hWindowControl1.HalconWindow.DispObj(img[0]);        
        }

        private void CurrentImageView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((StationFramer)_mainController.Framer).OnGroupAllCaptured -= OnImageGrabDone;
        }
    }
}
