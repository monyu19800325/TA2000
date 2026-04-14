using DevExpress.Data.Filtering.Helpers;
using HTAMachine.Machine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA2000Modules;

namespace TA2000Modules
{

    [Serializable]
    public class FlowCarrierProductParam
    {
        /// <summary>
        /// 入料逾時時間，單位:秒
        /// </summary>
        public int LoadTimeout { get; set; } = 10;
        /// <summary>
        /// 出料逾時時間，單位:秒
        /// </summary>
        public int UnloadTimeout { get; set; } = 10;
        /// <summary>
        /// 汽缸逾時時間，單位:秒
        /// </summary>
        public int CylinderTimeout { get; set; } = 10;
        /// <summary>
        /// 減速逾時時間，單位:秒
        /// </summary>
        public int DecTimeout { get; set; } = 10;
        /// <summary>
        /// 到位逾時時間，單位:秒
        /// </summary>
        public int StopTimeout { get; set; } = 10;
        /// <summary>
        /// 往回退Timeout時間
        /// </summary>
        public int ReturnTimeout { get; set; } = 10;
        /// <summary>
        /// Vacuum時間
        /// </summary>
        public int VacuumTimeout { get; set; } = 10;
        /// <summary>
        /// TopSV時間
        /// </summary>
        public int TopSVTimeout { get; set; } = 10;
        public MoveVelEm FlowVel { get; set; } = MoveVelEm.Medium;

        /// <summary>
        /// 是否Bypass頂升汽缸(自動流程)
        /// </summary>
        public bool TopSV_Bypass_Pick { get; set; }
        /// <summary>
        /// 是否Bypass定位汽缸(自動流程)
        /// </summary>
        public bool GuideSV_Bypass_Pick { get; set; }
        /// <summary>
        /// 是否Bypass停止汽缸(自動流程)
        /// </summary>
        public bool StopSV_Bypass_Pick { get; set; }
        /// <summary>
        /// 是否Bypass真空吸取(自動流程)
        /// </summary>
        public bool VC_Bypass_Pick { get; set; }
        /// <summary>
        /// 是否Bypass真空吸取判斷(自動流程)
        /// </summary>
        public bool VCCheck_Bypass_Pick { get; set; }

    }

    [Serializable]
    public class FlowCarrierGlobalParam
    {
        public string TCPIPClientIP { get; set; } = "192.168.70.86";
        public int TCPIPClientPort { get; set; } = 6667;
        public int TCPIPServerPort { get; set; } = 36000;
        public MachineDeliveryTypeEm UpperDeliveryType { get; set; } = MachineDeliveryTypeEm.SMEMA;
        public MachineDeliveryTypeEm LowerDeliveryType { get; set; } = MachineDeliveryTypeEm.SMEMA;
        //public Double CVBoatVacuum_CurrentValue { get; set; } //CV Boat Current Vacuum
        public Double CVBoatVacuum_EstablishedValue { get; set; } = 0.00; //CV Boat Vacuum
        public bool TCPIPUseXml { get; set; } = true;
        /// <summary>
        /// 使用TCPIP Run與Stop的連動
        /// </summary>
        public bool UseInterLock { get; set; } = false;
        //public bool CVBoatVacuum_State { get; set; } = false;

        public int OnlyDataTCPIPClientPort { get; set; } = 48000;
        public int OnlyDataTCPIPServerPort { get; set; } = 48001;
        public bool EnableOnlyDataTCPIPServer { get; set; } = false;
        public bool EnableOnlyDataTCPIPClient { get; set; } = false;
    }

    [Serializable]
    public class FlowMotorOffset
    {
        [MotorOffset("視覺縱移軸")]
        [Description("入料位置")]
        public double LoadPosition { get; set; } = 0;
        [MotorOffset("視覺縱移軸")]
        [Description("出料位置")]
        public double UnloadPosition { get; set; } = 0;
        [MotorOffset("視覺縱移軸")]
        [Description("檢測位置(吸嘴中心對到boat盤靠近Stopbar邊邊的位置)(跟視覺基礎位置X設定一樣)")]
        public double InspectPosition { get; set; } = 0;



    }

    public enum MachineDeliveryTypeEm
    {
        SMEMA,
        TCPIP,
        NoUse,
    }
}

