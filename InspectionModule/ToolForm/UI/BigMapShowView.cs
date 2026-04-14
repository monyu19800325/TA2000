using DevExpress.XtraEditors;
using HalconDotNet;
using HTAMachine.Machine;
using HTAMachine.Module;
using HTAMachine.Module.DockPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    [RegisterCdiDocker]
    public partial class BigMapShowView : XtraForm, IInjectModule
    {
        HImage MapImage;
        InspectionModule Module;
        List<Button> MapButton = new List<Button>();
        public int Row;
        public int Col;
        ComponentReportForm _componentReportForm;
        public BigMapShowView(int row,int col)
        {
            InitializeComponent();
            Row = row;
            Col = col;
        }

        public void InjectModule(IModule module)
        {
            Module = (InspectionModule)module;
        }

        public void ReadMap(InspectionModule module)
        {
            if (Directory.Exists($@"D:\Coordinator2.0\Products\{module.ProductName}\BigMap") == false)
            {
                Directory.CreateDirectory($@"D:\Coordinator2.0\Products\{module.ProductName}\BigMap");
            }
            MapImage?.Dispose();
            if (!File.Exists($@"D:\Coordinator2.0\Products\{module.ProductName}\BigMap\ProductMap.jpg"))
                return;
            MapImage = new HImage();
            MapImage.ReadImage($@"D:\Coordinator2.0\Products\{module.ProductName}\BigMap\ProductMap.jpg");
            hWindowControl1.SetFullImagePart(MapImage);
            //hWindowControl1.HalconWindow.ClearWindow();
            hWindowControl1.HalconWindow.DispObj(MapImage);

            var windowSize =  hWindowControl1.Size;
            double scaleX = (double)windowSize.Width / (double)module.ProductParam.BigProductMapSetting.OriginPixelImageSize.x;
            double scaleY = (double)windowSize.Height / (double)module.ProductParam.BigProductMapSetting.OriginPixelImageSize.y;


            for (int i = 0; i < module.ProductParam.BigProductMapSetting.MapList.Count; i++)
            {
                double centerX = scaleX*module.ProductParam.BigProductMapSetting.MapList[i].PixelLeftDown.x;
                double centerY = scaleY*module.ProductParam.BigProductMapSetting.MapList[i].PixelRightUp.y;
                double sizeX = scaleX*Math.Abs(module.ProductParam.BigProductMapSetting.MapList[i].PixelRightUp.x - module.ProductParam.BigProductMapSetting.MapList[i].PixelLeftDown.x);
                double sizeY = scaleY*Math.Abs(module.ProductParam.BigProductMapSetting.MapList[i].PixelRightUp.y - module.ProductParam.BigProductMapSetting.MapList[i].PixelLeftDown.y);
                Color color = Color.Gray;

                if (module.CurrentTrayCarrier.ProductInfos[Row][Col].BigMapResults[i].InspectResult == "Fail")
                {
                    color = Color.Red;
                }
                else if(module.CurrentTrayCarrier.ProductInfos[Row][Col].BigMapResults[i].InspectResult == "Invalid")
                {
                    color = Color.LightBlue;
                }
                else if(module.CurrentTrayCarrier.ProductInfos[Row][Col].BigMapResults[i].InspectResult == "Pass")
                {
                    color = Color.LightGreen;
                }
                Button button = new Button()
                {
                    Name = Guid.NewGuid().ToString(),
                    Location = new Point((int)centerX, (int)centerY),
                    Text = i.ToString() + $"({module.CurrentTrayCarrier.ProductInfos[Row][Col].BigMapResults[i].InspectResult})",
                    Size= new Size((int)sizeX, (int)sizeY),
                    TabIndex = 1,
                    UseVisualStyleBackColor = true,
                    BackColor = color,
                };
                button.Click += MapButtonClick;
                MapButton.Add(button);
                this.Controls.Add(button);
                button.BringToFront();
            }

        }

        public void MapButtonClick(object sender,EventArgs e)
        {
            var btn = (Button)sender;
            var name = btn.Name;
            var target = MapButton.FirstOrDefault(x => x.Name == name);
            var index = MapButton.IndexOf(target);

            if (_componentReportForm == null)
            {
                _componentReportForm = (ComponentReportForm)Module._mdiService.ShowMdi(this, typeof(ComponentReportForm), new Point(0, 0), new Size(0, 0));
                _componentReportForm.FormClosed += (ss, ee) =>
                {
                    _componentReportForm = null;
                };
                _componentReportForm.TotalTrayInLot = Module.CurrentTrayCarrier.Count;
                //需要自己求出index(影像中第幾顆產品)，row跟col可能都要重算
                _componentReportForm.ShowComponentReportValue(
                    Module.VisionController.CommonInfo.ComponentSaveSetting.path,
                    Row + 1,
                    Col + 1,
                    0,
                    Module.VisionController.CommonInfo.ResultImageSaveSetting.path,
                    Module.VisionController.CommonInfo.RowImageSaveSetting.path,
                    Module._curLotName,
                    $"MapIndex_{index}");
            }
            else
            {
                _componentReportForm.TotalTrayInLot = Module.CurrentTrayCarrier.Count;
                _componentReportForm.ShowComponentReportValue(
                    Module.VisionController.CommonInfo.ComponentSaveSetting.path,
                    Row + 1,
                    Col + 1,
                    0,
                    Module.VisionController.CommonInfo.ResultImageSaveSetting.path,
                    Module.VisionController.CommonInfo.RowImageSaveSetting.path,
                    Module._curLotName,
                    $"MapIndex_{index}");
            }
        }

        private void BigMapShowView_Shown(object sender, EventArgs e)
        {
            ReadMap(Module);
        }
    }
}
