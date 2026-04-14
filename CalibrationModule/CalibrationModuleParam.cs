using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
    [Serializable]
    public class CalibrationModuleParam
    {
        #region 相機校正
        /// <summary>
        /// 相機橫移軸
        /// </summary>
        public double Cam_AX1Offset { get; set; } = 0;

        /// <summary>
        /// 相機升降軸
        /// </summary>
        public double Cam_AZ1Offset { get; set; } = 0;
        #endregion
        /// <summary>
        /// 流道縱移軸
        /// </summary>
        public double BY1Offset { get; set; } = 0;

        public MoveVelEm MovingVel { get; set; } = MoveVelEm.Medium;


        public double Cam_XOffsetLightCalib { get; set; } = 0;
        public double Cam_ZOffsetLightCalib { get; set; } = 0;
        public double Carrier_YOffsetLightCalib { get; set; } = 0;

        public List<double> CalibrationLights { get; set; } = new List<double>();
        public List<double> MosaicCalibrationLights { get; set; } = new List<double>();

        //Z軸對焦點
        public double MosaicAZ1Pos { get; set; } = 0;

        //相機Gain值
        public double CalibrationGain { get; set; } = 1;

        //Barcode拍攝位置
        public double Barcode_Capture_X { get; set; } = 0;
        public double Barcode_Capture_Y { get; set; } = 0;
        public double Barcode_Capture_Z { get; set; } = 0;

        #region 十字中心點校正
        public double CrossCenterAY1Pos { get; set; } = 0;
        public double CrossCenterAZ1Pos { get; set; } = 0;
        public double CrossCenterBY1Pos { get; set; } = 0;
        #endregion
        #region 光源校正
        /// <summary>
        /// 視覺橫移軸(光源校正用)
        /// </summary>
        public double X1PosLightCalib { get; set; } = 0;
        /// <summary>
        /// 視覺升降軸(光源校正用)
        /// </summary>
        public double Z1PosLightCalib { get; set; } = 0;

        /// <summary>
        /// 吸嘴縱移軸(光源校正用)
        /// </summary>
        public double Y1PosLightCalib { get; set; } = 0;

        public double GrayCardY1Pos { get; set; } = 0;
        public double GrayCardZ1Pos { get; set; } = 0;

        #endregion
        #region 3D校正


        public double Z1Pos3D { get; set; } = 0;
        public double X1Pos3D { get; set; } = 0;
        public double Y1Pos3D { get; set; } = 0;
        #endregion

        public double LaserCenterAX1Pos { get; set; } = 0;
        public double LaserCenterAZ1Pos { get; set; } = 0;
        public double LaserCenterBY1Pos { get; set; } = 0;

        /// <summary>
        /// 紀錄雷射偵測塊規中心X
        /// </summary>
        public double LaserCenterX { get; set; } = 0;

        /// <summary>
        /// 紀錄雷射偵測塊規中心Y
        /// </summary>
        public double LaserCenterY { get; set; } = 0;

        public double LaserOffsetX{ get; set; } = 0;
        public double LaserOffsetY { get; set; } = 0;

        public int MoveDir { get; set; } = 0;//0正 1逆
        public int Step { get; set; } = 0;


        public double GrayCardAY1Pos { get; set; } = 0;
        public double GrayCardBZ1Pos { get; set; } = 0;

        public double AY1PosLightCalib { get; set; } = 0;
        public double BZ1PosLightCalib { get; set; } = 0;
        public double BX1PosLightCalib { get; set; } = 0;

    }
}
