using Hta.MotionBase;
using Hta.MotionBase.Interface;
using HTAMachine.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
   
    public partial class CalibrationModule
    {
        
        [DefineHardware] public ISensor LaserReader;

        [DefineHardware] public IGantry 視覺縱移軸;
        //[DefineHardware] public IAxis AY2_視覺縱移軸_右;
        [DefineHardware] public IAxis BX1_流道橫移軸; // 含干涉確認機制
        [DefineHardware] public IAxis BX2_流道傳送軸; //皮帶傳送
        [DefineHardware] public IAxis BZ1_流道頂升升降軸;


        [DefineHardware] public IOutputIO Y067000_傳送流道_靠邊氣缸電磁閥;
        [DefineHardware] public IOutputIO Y067001_傳送流道_到位氣缸電磁閥;
        [DefineHardware] public IOutputIO Y067002_傳送流道_頂升真空電磁閥;
        //[DefineHardware] public IOutputIO Y067003_灰卡模組_上升氣缸電磁閥;
        //[DefineHardware] public IOutputIO Y067004_SP;
        //[DefineHardware] public IOutputIO Y067005_SP;
        //[DefineHardware] public IOutputIO Y067006_SP;
        //[DefineHardware] public IOutputIO Y067007_SP;
        //[DefineHardware] public IOutputIO Y067008_SP;
        //[DefineHardware] public IOutputIO Y067009_SP;
        //[DefineHardware] public IOutputIO Y067010_SP;
        //[DefineHardware] public IOutputIO Y067011_SP;
        //[DefineHardware] public IOutputIO Y067012_SP;
        //[DefineHardware] public IOutputIO Y067013_靜電消除器_放電啟動;
        //[DefineHardware] public IOutputIO Y067014_靜電風扇_1_放電停止;
        //[DefineHardware] public IOutputIO Y067015_靜電風扇_2_放電停止;
    }
}
