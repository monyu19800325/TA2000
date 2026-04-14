using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    [POCOViewModel]
    public class AdjustMapRegionViewModel
    {
        public List<int> MapIndexList { get; set; } = new List<int>();
        int _mapIndex = 0;
        public int MapIndex 
        {
            get => _mapIndex;
            set
            {
                _mapIndex = value;
                ExtendedHeight = _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].ExtendedHeight;
                ExtendedWidth = _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].ExtendedWidth;
                DisplayRegion();
                this.RaisePropertiesChanged();
            }
        }

        double _extendedHeight = 0;
        public double ExtendedHeight 
        {
            get => _extendedHeight;
            set
            {
                _extendedHeight = value;
                DisplayRegion();
                this.RaisePropertiesChanged();
            }
        }
        double _extendedWidth = 0;
        public double ExtendedWidth 
        {
            get => _extendedWidth;
            set
            {
                _extendedWidth = value;
                DisplayRegion();
                this.RaisePropertiesChanged();
            }
        }

        private ProductSettingToolViewModel _productSettingToolViewModel;
        public HWindowControl HWindowControlMap;
        public void Init(ProductSettingToolViewModel productSettingToolViewModel)
        {
            _productSettingToolViewModel = productSettingToolViewModel;

            for (int i = 0; i < _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList.Count; i++)
            {
                MapIndexList.Add(i);
            }
            MapIndex = MapIndexList.FirstOrDefault();

            DisplayRegion();
        }

        public void ShowOriginIndexRegion()
        {
            _productSettingToolViewModel.RealPosToPixel(_productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionRightUp.x, _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionRightUp.y,
                   out var pixelX, out var pixelY);
            _productSettingToolViewModel.RealPosToPixel(_productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionLeftDown.x, _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionLeftDown.y,
                out var pixelX2, out var pixelY2);

            HOperatorSet.GenRectangle1(out HObject rectangle,
                pixelY,
                pixelX2,
                pixelY2,
                pixelX);
            HWindowControlMap.SetFullImagePart(_productSettingToolViewModel.MapImage);
            HWindowControlMap.HalconWindow.DispObj(_productSettingToolViewModel.MapImage);
            HWindowControlMap.HalconWindow.SetColor("red");
            HWindowControlMap.HalconWindow.SetDraw("margin");
            HWindowControlMap.HalconWindow.DispObj(rectangle);
            double centerX = (pixelX+pixelX2)/2;
            double centerY = (pixelY+pixelY2)/2;
            HWindowControlMap.HalconWindow.SetTposition((int)centerY, (int)centerX);
            HWindowControlMap.HalconWindow.WriteString(MapIndex.ToString());
        }

        public void DisplayRegion()
        {
            HWindowControlMap.HalconWindow.ClearWindow();
            ShowOriginIndexRegion();
            _productSettingToolViewModel.RealPosToPixel(_productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionRightUp.x, _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionRightUp.y,
                   out var pixelX, out var pixelY);
            _productSettingToolViewModel.RealPosToPixel(_productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionLeftDown.x, _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].PositionLeftDown.y,
                out var pixelX2, out var pixelY2);

            HOperatorSet.GenRectangle1(out HObject rectangle,
                pixelY-ExtendedHeight,
                pixelX2-ExtendedWidth,
                pixelY2+ExtendedHeight,
                pixelX+ExtendedWidth);
            //HWindowControlMap.SetFullImagePart(_productSettingToolViewModel.MapImage);
            //HWindowControlMap.HalconWindow.DispObj(_productSettingToolViewModel.MapImage);
            HWindowControlMap.HalconWindow.SetColor("green");
            HWindowControlMap.HalconWindow.SetDraw("margin");
            HWindowControlMap.HalconWindow.DispObj(rectangle);
        }

        public void Set()
        {
            _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].ExtendedWidth = ExtendedWidth;
            _productSettingToolViewModel.Module.ProductParam.BigProductMapSetting.MapList[MapIndex].ExtendedHeight = ExtendedHeight;
        }

        public void FormClosing(object sender,FormClosingEventArgs e)
        {
            _productSettingToolViewModel.Module.SaveVisionProductParam(this, _productSettingToolViewModel.Module);
        }
    }
}
