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
    public partial class AxisControlModule
    {
        [DefineHardware] public IGantry 視覺縱移軸;
        //[DefineHardware] public IAxis AY2_視覺縱移軸_右;
        [DefineHardware][DefineInterference(nameof(BX1軸干涉))] public IAxis BX1_流道橫移軸; // 含干涉確認機制
        [DefineHardware] public IAxis BX2_流道傳送軸; //皮帶傳送
        //[DefineHardware][DefineInterference("BZ1流道頂升升降軸干涉")] public IAxis BZ1_流道頂升升降軸;
        [DefineHardware][DefineInterference(nameof(BZ1流道頂升升降軸干涉))] public IAxis BZ1_流道頂升升降軸;
        
        [DefineHardware] public IInputIO X064000_設備安全系統正常;
        [DefineHardware] public IInputIO X064001_設備安全門系統正常;
        //[DefineHardware] public IInputIO X064002_SP;
        //[DefineHardware] public IInputIO X064003_SP;
        [DefineHardware] public IInputIO X064004_電磁鎖鎖定檢知;
        //[DefineHardware] public IInputIO X064005_SP;
        [DefineHardware] public IInputIO X064006_設備安全門關閉;
        //[DefineHardware] public IInputIO X064007_SP;
        [DefineHardware] public IInputIO X064008_啟動按鈕開關;
        [DefineHardware] public IInputIO X064009_停止按鈕開關;
        [DefineHardware] public IInputIO X064010_正壓源檢知;
        [DefineHardware] public IInputIO X064011_負壓源檢知;
        [DefineHardware] public IInputIO X064012_SMEMA_上位1_通知已放料信號;
        [DefineHardware] public IInputIO X064013_SMEMA_下位1_通知Ready信號;
        //[DefineHardware] public IInputIO X064014_SMEMA_S_P;
        //[DefineHardware] public IInputIO X064015_SMEMA_S_P;

        [DefineHardware] public IInputIO X066000_傳送流道_入料銜接檢知_上下對照;
        [DefineHardware] public IInputIO X066001_傳送流道_入料銜接檢知_側邊反射;
        [DefineHardware] public IInputIO X066002_傳送流道_產品位置檢知;
        [DefineHardware] public IInputIO X066003_傳送流道_產品減速檢知;
        [DefineHardware] public IInputIO X066004_傳送流道_產品到位檢知;
        [DefineHardware] public IInputIO X066005_傳送流道_到位氣缸_上升;
        [DefineHardware] public IInputIO X066006_傳送流道_到位氣缸_下降;
        [DefineHardware] public IInputIO X066007_傳送流道_出料銜接檢知_側邊反射;
        [DefineHardware] public IInputIO X066008_傳送流道_出料銜接檢知_上下對照;
        [DefineHardware] public IInputIO X066009_灰卡模組_上升檢知;
        [DefineHardware] public IInputIO X066010_灰卡模組_下降檢知;
        //[DefineHardware] public IInputIO X066011_SP;
        //[DefineHardware] public IInputIO X066012_SP;
        [DefineHardware] public IInputIO X066013_靜電消除器_異常_Alarm;
        [DefineHardware] public IInputIO X066014_靜電風扇_1_異常;
        [DefineHardware] public IInputIO X066015_靜電風扇_2_異常;


        [DefineHardware] public IOutputIO Y065000_設備安全門電磁鎖;
        [DefineHardware] public IOutputIO Y065001_設備安全門系統_Reset_SP;
        //[DefineHardware] public IOutputIO Y065002_SP;
        [DefineHardware] public IOutputIO Y065003_正壓源電磁閥;
        //[DefineHardware] public IOutputIO Y065004_SP;
        [DefineHardware] public IOutputIO Y065005_警示燈_紅	;
        [DefineHardware] public IOutputIO Y065006_警示燈_橙	;
        [DefineHardware] public IOutputIO Y065007_警示燈_綠	;
        [DefineHardware] public IOutputIO Y065008_警示燈_蜂鳴器	;
        //[DefineHardware] public IOutputIO Y065009_SP;
        //[DefineHardware] public IOutputIO Y065010_SP;
        //[DefineHardware] public IOutputIO Y065011_SP;
        [DefineHardware] public IOutputIO Y065012_SMEMA_上位1_通知上位_Ready信號	;
        [DefineHardware] public IOutputIO Y065013_SMEMA_下位1_通知已放料信號;
        //[DefineHardware] public IOutputIO Y065014_SMEMA_S_P;
        //[DefineHardware] public IOutputIO Y065015_SMEMA_S_P;


       
        [DefineHardware] public IOutputIO Y067000_傳送流道_靠邊氣缸電磁閥;
        [DefineHardware] public IOutputIO Y067001_傳送流道_到位氣缸電磁閥;
        [DefineHardware] public IOutputIO Y067002_傳送流道_頂升真空電磁閥;
        [DefineHardware] public IOutputIO Y067003_灰卡模組_上升氣缸電磁閥;
        //[DefineHardware] public IOutputIO Y067004_SP;
        //[DefineHardware] public IOutputIO Y067005_SP;
        //[DefineHardware] public IOutputIO Y067006_SP;
        //[DefineHardware] public IOutputIO Y067007_SP;
        //[DefineHardware] public IOutputIO Y067008_SP;
        //[DefineHardware] public IOutputIO Y067009_SP;
        //[DefineHardware] public IOutputIO Y067010_SP;
        //[DefineHardware] public IOutputIO Y067011_SP;
        //[DefineHardware] public IOutputIO Y067012_SP;
        [DefineHardware] public IOutputIO Y067013_靜電消除器_放電啟動;
        [DefineHardware] public IOutputIO Y067014_靜電風扇_1_放電停止;
        [DefineHardware] public IOutputIO Y067015_靜電風扇_2_放電停止;

    }
}
