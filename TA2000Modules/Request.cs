using Hta.MotionBase;
using HTA.MainController;
using HTA.Utility;
using HTA.Utility.Calibration;
using HTA.Utility.Structure;
using ModuleTemplate;
using ModuleTemplate.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HTAMachine.Machine.HtaMachineController;

namespace TA2000Modules
{
    public static class TATool
    {
        

        public static Velocity SelectVelDef(IAxis axis, List<VelocityData> velocityDatas, string targetName)
        {
            var velDef = velocityDatas.FirstOrDefault(v => v.VelocityName == targetName);
            if (velDef == null)
                return axis.MoveVelocity;
            return velDef.Velocity;
        }


        public static bool LoadMosaic(out Calibration.Halcon.MosaicTool.MotionPoseGrid MotionGrid)
        {
            MotionGrid = HTATool.ReadXml<Calibration.Halcon.MosaicTool.MotionPoseGrid>("D:\\Coordinator2.0\\Mosaic\\TA2000Vision\\cam0_grid.xml");

            bool loadsuccess = true;
            if (MotionGrid.Poses.Count == 0)
                loadsuccess = false;

            return loadsuccess;
        }

        public static List<MosaicInfo> SortMosaicInfo(Calibration.Halcon.MosaicTool.MotionPoseGrid MotionGrid)
        {
            List<MosaicInfo> _mosaicInfo_List = new List<MosaicInfo>();
            MosaicInfo _mosaicInfo = null;
            string _tempProductName = "";
            for (int index = 0; index < MotionGrid.Poses.Count; index++)
            {
                if (index == 0)
                {
                    _tempProductName = MotionGrid.Poses[index].Name;

                    _mosaicInfo = new MosaicInfo();
                    _mosaicInfo.ProductName = _tempProductName;
                }

                if (MotionGrid.Poses[index].Name == _tempProductName) //同一批資料
                {
                    _mosaicInfo.Position.Add(new Point2d(MotionGrid.Poses[index].MotionX, MotionGrid.Poses[index].MotionY));
                }
                else //不同批資料
                {
                    _mosaicInfo_List.Add(_mosaicInfo);  //儲存上批資料                       
                }

                if (index == MotionGrid.Poses.Count - 1) //最後一筆資料時
                {
                    if (MotionGrid.Poses[index].Name == _tempProductName) //全部只有一批資料 → 儲存
                        _mosaicInfo_List.Add(_mosaicInfo);
                }
            }

            return _mosaicInfo_List;
        }

        public static string BoatBarcode { get; set; } = "defalut";

        public static IMainController MainController;
        public static double InspecttPosition_AY1 { get; set; } = 0;
        public static double InspecttPosition_BX1 { get; set; } = 0;

        //針對校正設計的參數
        public static double GrayCard_Y1 { get; set; } = 0;
        public static double GrayCard_Z1 { get; set; } = 0;
        public static double LightCalib_Y1 { get; set; } = 0;
        public static double LightCalib_Z1 { get; set; } = 0;
        public static double LightCalib_X1 { get; set; } = 0;
    }

    /* //LI3000
    [Serializable]
    public class VersionController
    {
        /// <summary>
        /// 1=跟DS3070S串機(M01)，0=進日月光機台(M00)
        /// </summary>
        public int Version { get; set; } = 1;
    }
    */

    public class MachineStateArgs
    {
        public MachineStateEm machineState { get; set; }
        public AxisState _AxisState {  get; set; }
    }

    public class RedLightOnArgs
    {
        public bool IsOn { get; set; }
    }
    public class OrangeLightOnArgs
    {
        public bool IsOn { get; set; }
    }
    public class GreenLightOnArgs
    {
        public bool IsOn { get; set; }
    }

    public class SafetyArgs
    {
        /// <summary>
        /// True:安全模式，False:維修模式
        /// </summary>
        public bool KeyState { get; set; }

        /// <summary>
        /// True:上鎖，False:解鎖
        /// </summary>
        public bool DoorLockState { get; set; }

        public override string ToString()
        {
            var msg = KeyState ? "安全模式" : "維修模式";
            msg += DoorLockState ? "上鎖" : "解鎖";
            return msg;
        }
    }

    public class ModelArgs
    {
        public ModelEm ModelName { get; set; } = ModelEm.安全模式;
    }

    public class VisionControllerArgs
    {
        public IMainController VisionController { get; set; }
    }

    public class VirtualArgs
    {
        public bool IsVirtual { get; set; }
    }

    public class VisionByPassArgs
    {
        public bool IsByPass { get; set; }
    }

    public class GoldenModelArgs
    {
        public bool IsGolden { get; set; }
    }

    public class MotorSTArgs
    {
        public bool IsST { get; set; }
    }

    public class ReConnectTriggerArgs
    {

    }

    /// <summary>
    /// Motion重連時，通知擁有ScanThread的模組停止/開始掃描
    /// </summary>
    public class NotifyResetMotionRequest
    {
        public bool ScanEnable = true;

        public NotifyResetMotionRequest(bool scanEnable)
        {
            ScanEnable = scanEnable;
        }
    }

    public class CrossCenterOffsetArgs
    {
        public double LeftCrossCenteroffset { get; set; } = 76.5;
        public double RightCrossCenteroffset { get; set; } = 76.5;
    }

    [Serializable]
    public class VersionController
    {
        /// <summary>
        /// 1=跟DS3070S串機(M01)，0=進日月光機台(M00)
        /// </summary>
        public int Version { get; set; } = 1;
    }

    public class CalibNotify
    {

    }

    public class MosaicPosArgs
    {
        public List<Point2d> MosaicPos { get; set; } = new List<Point2d>();
    }


    public class MosaicInfo
    {
        public string ProductName;
        public List<Point2d> Position = new List<Point2d>();
    }
    public class SpendTimeArgs
    {
        public TimeSpan Time { get; set; } = new TimeSpan(0, 0, 0, 0, 0);
        public ModelSpendEm ModelSpend { get; set; } = ModelSpendEm.Load;
        public int TotalCount { get; set; } = 0;
    }

    public enum ModelSpendEm
    {
        Load = 0,
        Unload = 1,
    }
    public class InitalArgs
    {
        public bool IsInitialAxis { get; set; } = false;

    }

}
