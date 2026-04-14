using TA2000Modules;
using DevExpress.Utils.MVVM;
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
    public partial class ProductSettingTool : DevExpress.XtraEditors.XtraForm
    {
        public ProductSettingTool(InspectionModule module)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module);
        }

        void InitializeBindings(InspectionModule module)
        {
            var fluent = mvvmContext1.OfType<ProductSettingToolViewModel>();

            hWindow_RealImage.HMouseUp += fluent.ViewModel.ImgWndReal_HMosueUp;

            hWindow_Map.MouseUp += fluent.ViewModel.ImgWndMap_MouseUp;
            hWindow_Map.HMouseUp += fluent.ViewModel.ImgWndMap_HMouseUp;
            hWindow_Map.HMouseWheel += fluent.ViewModel.ImgWndMap_HMouseWheel;
            hWindow_Map.HMouseMove += fluent.ViewModel.ImgWndMap_HMouseMove;
            hWindow_Map.HMouseDown += fluent.ViewModel.ImgWndMap_HMouseDown;

            fluent.ViewModel.LightPanel = LightPanel;
            fluent.ViewModel.HWindowControlMap = hWindow_Map;
            fluent.ViewModel.XAxis = module.BX1_流道橫移軸;
            fluent.ViewModel.YAxis = module.視覺縱移軸;
            fluent.ViewModel.ZAxis = module.BZ1_流道頂升升降軸;

            //fluent.ViewModel.YAxis.AbsoluteMove(0.0);

            fluent.ViewModel.Init(module);

            fluent.BindCommand(Btn_AddMapIndex, x => x.AddMapIndex());
            fluent.BindCommand(Btn_DeleteMapIndex, x => x.DeleteMapIndex());
            fluent.BindCommand(Btn_CaptureGenMap, x => x.CaptureGenMap());
            fluent.BindCommand(Btn_MoveXBackward, x => x.MoveXBackward());
            fluent.BindCommand(Btn_MoveXForward, x => x.MoveXForward());
            fluent.BindCommand(Btn_MoveYBackward, x => x.MoveYBackward());
            fluent.BindCommand(Btn_MoveYForward, x => x.MoveYForward());
            fluent.BindCommand(Btn_ZUp, x => x.ZUp());
            fluent.BindCommand(Btn_ZDown, x => x.ZDown());
            fluent.BindCommand(Btn_SaveMap, x => x.SaveMap());
            fluent.BindCommand(Btn_SignMap, x => x.SignMap());
            fluent.BindCommand(Btn_ProductRightUpSet, x => x.ProductRightUpSet());
            fluent.BindCommand(Btn_ProductRightUpMove, x => x.ProductRightUpMove());
            fluent.BindCommand(Btn_ProductLeftDownSet, x => x.ProductLeftDownSet());
            fluent.BindCommand(Btn_ProductLeftDownMove, x => x.ProductLeftDownMove());
            fluent.BindCommand(Btn_RightUpSet, x => x.RightUpSet());
            fluent.BindCommand(Btn_RightUpMove, x => x.RightUpMove());
            fluent.BindCommand(Btn_LeftDownSet, x => x.LeftDownSet());
            fluent.BindCommand(Btn_LeftDownMove, x => x.LeftDownMove());
            fluent.BindCommand(Btn_OpenMoveMap, x => x.ClickOpenMoveMap());
            fluent.BindCommand(Btn_OpenZoomMap, x => x.ClickOpenZoomMap());
            fluent.BindCommand(Btn_AdjustMapRegion, x => x.OpenAdjustMapRegion());

            LUE_MapIndex.Properties.DataSource = fluent.ViewModel.MapIndexList;
            LUE_Type.Properties.DataSource = fluent.ViewModel.TypeList;
            LUE_XYStep.Properties.DataSource = fluent.ViewModel.XYStepList;
            LUE_ZStep.Properties.DataSource = fluent.ViewModel.ZStepList;
            LUE_VelXY.Properties.DataSource = fluent.ViewModel.MoveVel;
            LUE_VelZ.Properties.DataSource = fluent.ViewModel.MoveVel;

            fluent.SetBinding(Btn_OpenMoveMap,x=>x.Text,x=>x.BtnMoveMapContent);
            fluent.SetBinding(Btn_OpenZoomMap, x => x.Text, x => x.BtnZoomMapContent);
            fluent.SetBinding(toggleSwitch1, x => x.IsOn, x => x.LiveOn);
            fluent.SetBinding(NUD_XPos, x => x.Value, x => x.XPos);
            fluent.SetBinding(NUD_YPos, x => x.Value, x => x.YPos);//有問題 待查
            fluent.SetBinding(NUD_ZPos, x => x.Value, x => x.ZPos);
            fluent.SetBinding(SE_Gain, x => x.Value, x => x.Gain);
            fluent.SetBinding(NUD_PitchX, x => x.Value, x => x.PitchX);
            fluent.SetBinding(NUD_PitchY, x => x.Value, x => x.PitchY);
            fluent.SetBinding(NUD_ProductRightUpX, x => x.Value, x => x.ProductRightUpX);
            fluent.SetBinding(NUD_ProductRightUpY, x => x.Value, x => x.ProductRightUpY);
            fluent.SetBinding(NUD_ProductLeftDownX, x => x.Value, x => x.ProductLeftDownX);
            fluent.SetBinding(NUD_ProductLeftDownY, x => x.Value, x => x.ProductLeftDownY);
            fluent.SetBinding(NUD_RightUpX, x => x.Value, x => x.CurrentRightUpX);
            fluent.SetBinding(NUD_RightUpY, x => x.Value, x => x.CurrentRightUpY);
            fluent.SetBinding(NUD_LeftDownX, x => x.Value, x => x.CurrentLeftDownX);
            fluent.SetBinding(NUD_LeftDownY, x => x.Value, x => x.CurrentLeftDownY);
            fluent.SetBinding(LUE_MapIndex, x => x.EditValue, x => x.SelectMapIndex);
            fluent.SetBinding(LUE_Type, x => x.EditValue, x => x.SelectType);
            fluent.SetBinding(LUE_XYStep, x => x.EditValue, x => x.SelectXYStep);
            fluent.SetBinding(LUE_ZStep, x => x.EditValue, x => x.SelectZStep);
            fluent.SetBinding(LUE_VelXY, x => x.EditValue, x => x.SelectVel);
            fluent.SetBinding(LUE_VelZ, x => x.EditValue, x => x.SelectVel);
        }
    }
}
