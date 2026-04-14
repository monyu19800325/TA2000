using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
    public enum MoveVelEm
    {
        VeryHigh = 0,
        High,
        Medium,
        Slow,
        VerySlow
    }

    public enum ModelEm
    {
        //一般模式,
        安全模式,
        維修模式
    }

    public enum BarcodeCatchModeEm
    {
        Barcode機模式,
        相機模式,
    }

    public enum AxisEm
    {
        視覺縱移軸,
        BZ1_流道頂升升降軸,
        BX1_流道橫移軸
    }

    public enum AxisState
    {
        Virtual=0,
        Real
    }

}
