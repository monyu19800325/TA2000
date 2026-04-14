using HTA.Utility.Structure;
using HTAMachine.Machine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VisionController2.VisionController.Common.ComponentSpecMap;

namespace TA2000Modules
{
    /// <summary>
    /// 視覺系統參數
    /// </summary>
    [Serializable]
    public class InspectionModuleParam
    {
        /// <summary>
        /// WCF Port
        /// </summary>
        public int Port { get; set; } = 50003;
        /// <summary>
        /// WCF連線Timeout時間，單位:毫秒
        /// </summary>
        public int Timeout { get; set; } = 60000;
        /// <summary>
        /// WCF_Mosaic Port
        /// </summary>
        public int PortMosaic { get; set; } = 50003;
        /// <summary>
        /// WCF連線Timeout時間，單位:毫秒
        /// </summary>
        public int TimeoutMosaic { get; set; } = 60000;

        /// <summary>
        /// 視覺上線等待時間，單位:秒
        /// </summary>
        public int InspectOnlineTimeout { get; set; } = 30;
        /// <summary>
        /// 檢測資料等待時間，單位:秒
        /// </summary>
        public int DataTimeout { get; set; } = 10;
        /// <summary>
        /// 取像Timeout時間(等待影像收齊)，單位:秒
        /// </summary>
        public int CaptureTimeout { get; set; } = 15;
        public string TrayReportPath { get; set; } = @"D:\TA2000\TrayReport\";
        public string GoldenReportPath { get; set; } = @"D:\TA2000\GoldenReport\";
        public string SummaryReportPath { get; set; } = @"D:\TA2000\SummaryReport\";

        public BarcodeCatchModeEm BarcodeCatchMode { get; set; } = BarcodeCatchModeEm.Barcode機模式;
        public bool Barcode_TeachFinish = false;


        /// <summary>
        /// 相機橫移軸(ByTest)
        /// </summary>
        public double AX1_Test0_Offset { get; set; } = 0;
        public double AX1_Test1_Offset { get; set; } = 0;

        /// <summary>
        /// 相機升降軸(ByTest)
        /// </summary>
        public double AZ1_Test0_Offset { get; set; } = 0;
        public double AZ1_Test1_Offset { get; set; } = 0;
        public double AZ1_TestFocus_Offset { get; set; } = 0;

        /// <summary>
        /// 流道縱移軸(ByTest)
        /// </summary>
        public double BY1_Test0_Offset { get; set; } = 0;
        public double BY1_Test1_Offset { get; set; } = 0;


        /// <summary>
        /// 是否使用此批號的舊資料
        /// </summary>
        public bool IsUseLotOldData { get; set; } = false;

        //針對多區塊的大產新增的參數品
        public BigProductMapSetting BigProductMapSetting { get; set; } = new BigProductMapSetting();
        public LaserSetting LaserSetting { get; set; } = new LaserSetting();
        public List<string> ComponentUserDefinedNamesMosaic { get; set; } = new List<string>();
        public List<string> ComponentUserDefinedNames { get; set; } = new List<string>();
    }

    [Serializable]
    public class InspectionProductParam
    {
        public MoveVelEm InspectVel { get; set; } = MoveVelEm.Medium;
        public AxisEm AxisName { get; set; } = AxisEm.BX1_流道橫移軸;
        /// <summary>
        /// 是否抽測
        /// </summary>
        public bool IsPick { get; set; }
        /// <summary>
        /// 是否重測
        /// </summary>
        public bool IsReinspect { get; set; }
        /// <summary>
        /// 是否飛拍
        /// </summary>
        public bool IsSetFly { get; set; }
        /// <summary>
        /// Laser是否ByPass(如果沒有BarcodeReader就不要顯示在介面上)
        /// </summary>
        public bool UseLaserMeasure { get; set; }
        /// <summary>
        /// BarcodeReader是否ByPass(如果沒有BarcodeReader就不要顯示在介面上)
        /// </summary>
        public bool UseBoatBarcodeReader { get; set; }
        /// <summary>
        /// 視覺是否ByPass
        /// </summary>
        public bool InspectByPass { get; set; }
        // <summary>
        /// 是否開啟光衰流程
        /// </summary>
        public bool OpenDecayFlow { get; set; }
        /// <summary>
        /// 抽測位置Index
        /// </summary>
        public List<Point2d> PickPos { get; set; } = new List<Point2d>();
        public double FocusLocation { get; set; } = 0;
        public List<List<double>> FocusLocations { get; set; } = new List<List<double>>();



        public double FocusLocation_Mon { get; set; } = 0;
        public List<double> FocusLocations_Mon { get; set; } = new List<double>();

        //public double FocusLeft0Deg { get; set; } = 0;
        //public double FocusRight0Deg { get; set; } = 0;
        //public double FocusLeft90Deg { get; set; } = 0;
        //public double FocusRight90Deg { get; set; } = 0;

        /// <summary>
        /// 確認氣缸位置的Timeout時間，單位:秒
        /// </summary>
        public int CheckCylinderTimeout { get; set; } = 10;
        public MoveVelEm FlyVel { get; set; } = MoveVelEm.Medium;
        public double FlyPercent { get; set; } = 1;
        public int ReinspectCount { get; set; } = 0;
        /// <summary>
        /// 當下產品的Fail數量大於此Alarm數量時，執行FailAlarm流程 (此參數設定為0時，為ByPass功能)
        /// </summary>
        public int FailAlarmCount { get; set; } = 0;
        /// <summary>
        /// 累計紀錄FailAlarm的數量，達到後也跟FailAlarm一樣
        /// </summary>
        public int AccumulateEachTrayFailAlarmCount { get; set; } = 0;
        /// <summary>
        /// 當下產品Lot的Fail數量大於此Alarm數量時，執行FailAlarm流程 (此參數設定為0時，為ByPass功能)
        /// </summary>
        public int LotRejectAlarmCount { get; set; } = 0;

        /// <summary>
        /// 分區檢測X位置
        /// </summary>
        public List<double> PartionXPosList { get; set; } = new List<double>();
        public List<double> PartionYPosList { get; set; } = new List<double>();
        public List<double> PartionZPosList { get; set; } = new List<double>();
        public bool UseMosaic { get; set; } = true;

       
        #region Laser
        /// <summary>
        /// Laser相關參數
        /// </summary>
        public int LaserImageIndex { get; set; } = 0;       

        public double Laser_AX1 { get; set; } = 0;
        public double Laser_BY1 { get; set; } = 0;
        public double Laser_AZ1 { get; set; } = 0;

        public double CircleRadius { get; set; } = 0;

        public int ThresholdMax = 255;
        public int ThresholdMin = 0;

        public int SearchScore = 60;
        public int HeightMax = 999;
        public int HeightMin = 0;

        public List<double> Lights { get; set; } = new List<double>();

        //public List<PointF> BlockPosition { get; set; } = new List<PointF>();
        #endregion

        public BigProductMapSetting BigProductMapSetting { get; set; } = new BigProductMapSetting();

        public LaserSetting LaserSetting { get; set; } = new LaserSetting();
    
        public List<string> ComponentUserDefinedNamesMosaic { get; set; } = new List<string>();
        public List<string> ComponentUserDefinedNames { get; set; } = new List<string>();
        public List<FailTableLink> FailTableLinks { get; set; } = new List<FailTableLink>();


        /// <summary>
        /// 暫存上次在視覺儲存存起來的Map
        /// </summary>
        public List<SingleUnitMap> LastTempMosaicMap { get; set; } = new List<SingleUnitMap>();
        /// <summary>
        /// 暫存上次在視覺儲存存起來的Map
        /// </summary>
        public List<SingleUnitMap> LastTempPartMap { get; set; } = new List<SingleUnitMap>();
    }


    #region Mon新增 針對多區塊的大產新增的參數品
    /*
     * Mon新增 針對多區塊的大產新增的參數品
     */
    public class BigProductMapSetting
    {
        public int MapCount { get => MapList.Count; set { } }
        public List<SingleUnitMap> MapList { get; set; } = new List<SingleUnitMap>();
        public Point2d ProductRightUp { get; set; } = new Point2d();
        public Point2d ProductLeftDown { get; set; } = new Point2d();
        public string MapImagePath { get; set; } = "";
        public double MosaicPitchX { get; set; } = 45;
        public double MosaicPitchY { get; set; } = 45;
        /// <summary>
        /// 原始影像尺寸大小
        /// </summary>
        public Point2d OriginPixelImageSize { get; set; } = new Point2d(0, 0);
    }
    
    [Serializable]
    public class LaserSetting
    {
        public List<LaserPoint> LaserPoints { get; set; } = new List<LaserPoint>();
        public double BestZValue { get; set; }

        public void CalcBestZValue()
        {
            if (LaserPoints.Count > 0)
            {
                BestZValue = LaserPoints.Average(x => x.ZValue);
            }
        }
    }

    [Serializable]
    public class LaserPoint
    {
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double ZValue { get; set; }
    }
    [Serializable]
    public class SingleUnitMap
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int MapIndex { get; set; }
        /// <summary>
        /// 區域實際軸位置的右上角座標
        /// </summary>
        public Point2d PositionRightUp { get; set; } = new Point2d(0, 0);
        /// <summary>
        /// 區域實際軸位置的左下角座標
        /// </summary>
        public Point2d PositionLeftDown { get; set; } = new Point2d(0, 0);
        /// <summary>
        /// 區域影像Pixel的右上角座標
        /// </summary>
        public Point2d PixelRightUp { get; set; } = new Point2d(0, 0);
        /// <summary>
        /// 區域影像Pixel的左下角座標
        /// </summary>
        public Point2d PixelLeftDown { get; set; } = new Point2d(0, 0);
        /// <summary>
        /// 區域延伸的區域寬度(左右各延伸)
        /// </summary>
        public double ExtendedWidth { get; set; }
        /// <summary>
        /// 區域延伸的區域高度(上下各延伸)
        /// </summary>
        public double ExtendedHeight { get; set; }
        /// <summary>
        /// 組圖或分區
        /// </summary>
        public string UseType { get; set; } = "Mosaic";
        /// <summary>
        /// 組圖和分區一開始都會以產品檔大小自動分區，那只有分區可以設Offset
        /// </summary>
        public List<Point2d> Offsets { get; set; } = new List<Point2d>();
        /// <summary>
        /// 紀錄影像Group的Guid
        /// </summary>
        public List<Guid> GroupIndexes { get; set; } = new List<Guid>();
        /// <summary>
        /// 組圖位置
        /// </summary>
        public List<Point2d> MosaicPosition { get; set; } = new List<Point2d>();
        /// <summary>
        /// 這組Map使用那些影像編號(從0開始)
        /// </summary>
        public List<int> CaptureImageIndexes { get; set; } = new List<int>();
        /// <summary>
        /// 雷射打到的實際值
        /// </summary>
        public double LaserZFocusOffset { get; set; } = 0;
        /// <summary>
        /// 組圖X、Y方向的數量
        /// </summary>
        public Point2d MosaicXYCount { get; set; } = new Point2d();
    }

    /// <summary>
    /// End
    /// </summary>
    /// 
    //public class InspectFailCode
    //{
    //    public List<MapFailCode> MapFailCodes { get; set; } = new List<MapFailCode>();
    //}
    //[Serializable]
    //public class MapFailCode
    //{
    //    public int MapIndex { get; set; }
    //    public List<FailCode> FailCodes { get; set; } = new List<FailCode>();
    //}
    [Serializable]
    public class SingleSpec
    {
        public string Name { get; set; } = "";

        public bool Active { get; set; } = false;

        public double LowBound { get; set; } = -99999.0;

        public double HighBound { get; set; } = 99999.0;

    }

    /// <summary>
    /// 紀錄是哪個元件的哪個Spec
    /// </summary>
    [Serializable]
    public class ComponentBindSpec
    {
        public int ComponentIndex { get; set; }
        public SingleSpec Spec { get; set; }

        public ComponentBindSpec() { }

        public ComponentBindSpec(int componentIndex, SingleSpec spec)
        {
            ComponentIndex = componentIndex;
            Spec = spec;
        }
    }


    #endregion Mon新增 針對多區塊的大產新增的參數品

    public class InspectionParamArgs
    {
        public MotorOffsetParam Param { get; set; }
    }


    [Serializable]
    public class MotorOffsetParam
    {
        #region 基準位置 (Start)

        [MotorOffset("BX1_流道橫移軸")]
        [Description("檢測Standby位置BX1")]
        public double Start_X1 { get; set; } = 0;

        [MotorOffset("視覺縱移軸")]
        [Description("啟動位置AY1")]
        public double Start_AY1 { get; set; } = 0;

        [MotorOffset("BZ1_流道頂升升降軸")]
        [Description("啟動位置AZ1")]
        public double Start_Z1 { get; set; } = 0;

        #endregion 基準位置 (End)


        #region 準備檢測位置

        [MotorOffset("BX1_流道橫移軸")]
        [Description("檢測Standby位置BX1 (鏡頭下方)")]
        public double InspStandBy_X { get; set; } = 0;


        [MotorOffset("BZ1_流道頂升升降軸")]
        [Description("檢測Standby位置BZ1 (準備對焦位置)")]
        public double InspStandBy_Z { get; set; } = 0;

        [MotorOffset("視覺縱移軸")]
        [Description("檢測前Standby位置AY1 (對準第一排產品之位置)")]
        public double InspStandBy_Y { get; set; } = 0;

        [MotorOffset("BX1_流道橫移軸")]
        [Description("準備取放產品之位置BX1 (治具定位位置)")]
        public double TakeProduct_X { get; set; } = 0;

        [MotorOffset("BZ1_流道頂升升降軸")]
        [Description("檢測時，頂升產品高度")]
        public double InspectLiftPos_Z { get; set; } = 0;


        #endregion

        #region Barcode位置 (Start)
        [MotorOffset("BX1_流道橫移軸")]
        [Description("TrayBarcode拍攝位置X(設定TrayBarcode拍攝位置X)")]
        public double TrayBarcode_BX1 { get; set; } = 0;

        [MotorOffset("視覺縱移軸")]
        [Description("TrayBarcode拍攝位置Y(設定TrayBarcode拍攝位置Y)")]
        public double TrayBarcode_AY1 { get; set; } = 0;

        [MotorOffset("BZ1_流道頂升升降軸")]
        [Description("TrayBarcode對焦位置Z(設定TrayBarcode對焦位置Z)")]
        public double TrayBarcode_BZ1 { get; set; } = 0;
        #endregion Barcode位置 (End)

        #region Fail Alarm (Start)
        [MotorOffset("BX1_流道橫移軸")]
        [Description("FailAlarm發生時，將Boat盤移動至人員可確認位置")]
        public double FailAlarm_Y { get; set; } = 0;
        #endregion Fail Alarm (End)


        [MotorOffset("BX1_流道橫移軸")]
        [Description("視覺中心跟雷射中心，X方向的Offset")]
        public double VisionCenterChangeToLaser_BX1 { get; set; } = 0;

        [MotorOffset("視覺縱移軸")]
        [Description("視覺中心跟雷射中心，Y方向的Offset")]
        public double VisionCenterChangeToLaser_AY1 { get; set; } = 0;

        [MotorOffset("BX1_流道橫移軸")]
        [Description("3D Golden板子中心到右上角X距離")]
        public double Golden3DCenterToCornerBX1 { get; set; } = 0;
        [MotorOffset("視覺縱移軸")]
        [Description("3D Golden板子中心到右上角Y距離")]
        public double Golden3DCenterToCornerAY1 { get; set; } = 0;

    }
}
