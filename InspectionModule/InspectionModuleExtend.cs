using DevExpress.Utils.Drawing;
using HalconDotNet;
using Hta.Container;
using Hta.MotionBase;
using HTA.Com.WCF;
using HTA.SecsBase;
using HTA.Utility.Common;
using HTA.Utility.Structure;
using HTAMachine.Machine;
using HTAMachine.Machine.Services;
using ModuleTemplate.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using TA2000Modules.ToolForm;
using VisionController2.Routines2.WCF;
using VisionController2.Routines2.WCF.Contract;

namespace TA2000Modules
{
    public partial class InspectionModule
    {
        #region 全域變數
        #region Mosaic param
        private WcfDuplexClient<IVisionServerWcf, IVisionServerCallback> _wcfClientMosaic;
        private VisionServerClient _clientMosaic;
        private Queue<GrabInfos> _sgGoCommandsMosaic = new Queue<GrabInfos>();
        private Queue<Point2d> _grabCommandsMosaic = new Queue<Point2d>();
        private BlockingCollection<bool> _allCaptureDoneMosaic = new BlockingCollection<bool>();
        private BlockingCollection<bool> _inspectionDoneMosaic = new BlockingCollection<bool>();
        /// <summary>
        /// 儲存最終結果用的(一盤)
        /// </summary>  
        public List<GrabInfos> FinalResultsMosaic = new List<GrabInfos>();
        private int _totalTriggerCountMosaic = 0;
        public int Camera0CaptureCountMosaic = 0;
        #endregion

        public BoatCarrier BoatCarrier1;
        private WcfDuplexClient<IVisionServerWcf, IVisionServerCallback> _wcfClient;
        private VisionServerClient _client;
        private Queue<GrabInfos> _sgGoCommands = new Queue<GrabInfos>(); //拿到Tray的Layout後產生好這個Tray的Garb指令集，去程
        //private Queue<GrabInfos> _sgBackCommands = new Queue<GrabInfos>(); //拿到Tray的Layout後產生好這個Tray的Garb指令集，回程
        private Queue<Point2d> _grabCommands = new Queue<Point2d>();
        //private Queue<Point2d> _grabBackCommands = new Queue<Point2d>();
        private Queue<Point2d> _grabLaserCommands = new Queue<Point2d>();
        private Point2d _grabIndex = new Point2d();
        private BlockingCollection<TrayContainer> _trayInQueue = new BlockingCollection<TrayContainer>();
        private BlockingCollection<bool> _allCaptureDone = new BlockingCollection<bool>();
        private BlockingCollection<bool> _inspectionDone = new BlockingCollection<bool>();
        private TrayContainer _trayInCommands;
        /// <summary>
        /// 儲存最終結果用的(一盤)
        /// </summary>
        public List<GrabInfos> FinalResults = new List<GrabInfos>();
        public int CurrentRow = 0;
        public int CurrentCol = 0;
        /// <summary>
        /// 全部檢測完成(資料都產出)
        /// </summary>
        public bool InspectionDone = false; //全部檢測完成(資料都產出)
        /// <summary>
        /// 相機收齊影像
        /// </summary>
        public bool AllCaptureDone = false; //相機收齊影像
        public BoatCarrier CurrentTrayCarrier = new BoatCarrier();
        /// <summary>
        /// 教讀設定的檢測次數計數
        /// </summary>
        public int TeachInspectTimes = 0;
        private bool IsFirstInspect = true;
        private DateTime _programStartTime;
        private int _totalTriggerCount = 0;
        public bool IsBurning { get; set; } = false;
        /// <summary>
        /// 資料取得Timeout時，要不要這盤重跑
        /// </summary>
        public bool DataTimeoutIsRetry = false;
        /// <summary>
        /// 視覺異常時，略過視覺問題流程繼續
        /// </summary>
        public bool IsPassVision = false;
        //public List<VelocityData> AX1VelList; //視覺橫移軸
        //public List<VelocityData> AZ1VelList; //視覺升降軸
        //public List<VelocityData> BY1VelList; //流到縱移軸
        private BoatCarrier _preBoatCarrier = new BoatCarrier();
        public int CaptureLightCount = 0;//計數拍幾張了
        /// <summary>
        /// 是否要旋轉90度
        /// </summary>
        private bool _isTurnNintyDegree = false;
        /// <summary>
        /// 是否是去程
        /// </summary>
        private bool _isGoFlow = true;
        //public SuckerModule SuckerModuleThis;
        /// <summary>
        /// 給抽測與重測一起使用的變數
        /// </summary>
        public List<Point2d> SingleInspectPos = new List<Point2d>();
        /// <summary>
        /// GoInspect = true代表開始教讀檢測，GoInspect = false代表教讀檢測停止(代表直接跑流程)
        /// </summary>
        public bool GoInspect;
        public int Camera0CaptureCount = 0;
        public int Camera1CaptureCount = 0;

        [SecsSV("SV_ProductName", true, false)]
        public string ProductName { get; set; } = "";

        HyperInspection.FlowForm flowForm;
        /// <summary>
        /// 等待教讀視窗關閉的Queue
        /// </summary>
        public BlockingCollection<bool> GoInsepctQueue = new BlockingCollection<bool>();
        /// <summary>
        /// 計數燒機的產品總數
        /// </summary>
        public int TotalProductCount = 0;
        public string _curLotName;
        public string PreviousLotName;
        public IMdiService _mdiService;
        private ComponentReportForm _componentReportForm;
        private string OfflineImagePath = @"D:\TA2000\測試大產品\";
        //= @"D:\LI3000\測試大產品";
        //= @"D:\LI3000\測試小產品";
        /// <summary>
        /// 組圖需要拍的張數
        /// </summary>
        public int MosaicImgCount = 0;
        public BlockingCollection<bool> AlarmQueue = new BlockingCollection<bool>();
        /// <summary>
        /// 教讀設定檢測次數
        /// </summary>
        public int InspectCounts = 1;
        public StatisticViewModel StatisticView;
        private StatisticForm _statisticForm;

        //StatisticTableForm _statisticTableForm;
        StatisticTableForm2 _statisticTableForm2;
        //public StatisticTableViewModel StatisticTableViewModel;
        public StatisticTableViewModel2 StatisticTableViewModel2;
        public bool IsGolden = false;

        /// <summary>
        /// 教讀臨時暫停旗標
        /// </summary>
        public bool TeachImmediatelyStop = false;
        public object TeachingForm = null;
        public BarcodeTestForm BarcodeTestForm = null;
        public BlockingCollection<bool> TeachBackQueue = new BlockingCollection<bool>();
        private IShowWorkLogService _showWorkLogService;
        private IMachineSimpleController _machineSimpleController;
        public IMachineSystemSettingService SettingService;
        private CurrentImageView _currentImageView;
        private List<string> _failCmpNames = new List<string>();
        private DateTime _startRunTime;
        private DateTime _endRunTime;
        /// <summary>
        /// 當前計數重測次數
        /// </summary>
        private int _currentReinspectCount = 0;
        /// <summary>
        /// 流程中的重測旗標
        /// </summary>
        private bool _isReinspectFlag = false;
        private string _errorMessage = "";
        public bool GoldenFinishFlag = false;
        //private VersionController _versionController = new VersionController();
        // 8124 encoder count
        public int _encoderPosition;

        /// <summary>
        /// 確定流道上是否有產品 (FailAlarm流程使用)
        /// </summary>
        private bool _recheckNoProduct = false;
        int _partionIndex = 0;
        int _partionGroupIndex = 0;
        public List<int> MapIndexOrder = new List<int>();
        public int CurrentMapIndexOrder = 0;
        public bool IsMosaicSG = false;
        public bool IsPartSG = false;
        public Stopwatch UPHStopwatch = new Stopwatch();
        private LaserTestForm _laserTestForm;
        public bool Is3DGolden = false;
        public int AccumulateEachTrayFailAlarmCount = 0;
        public IDialogService DialogService;
        public IShowDefaultWaitFormService ShowDefaultWaitFormService;
        public string ProcessJobId = "";
        public string ControlJobId = "";
        public int InpectPartCount = 1;

        public string BarcodeStation { get; set; } = "TA2000Vision_Barcode";
     

        public List<Point2d> LaserCaptureLocation = null; //雷射移動拍攝點位紀錄
        public List<CustomImage> LaserDetectImage_List = null;
        
        /// <summary>
        /// 雷射測高目標Block的Model
        /// </summary>
        public HTuple TargetModel;
        public double CircleRadius;

        public double LaserToCamera = 189.5;
        #endregion

        double PixeltoMM = 1;
    }
}
