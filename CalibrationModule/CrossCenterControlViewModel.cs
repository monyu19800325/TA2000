using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using TA2000Modules;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using System.Threading;
using HTAMachine.Module;
using DevExpress.Mvvm;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class CrossCenterControlViewModel
    {      
        public double AY1Offset
        {
            get
            {
                return Module.Param.CrossCenterAY1Pos;
            }
            set
            {
                Module.Param.CrossCenterAY1Pos = value;
                this.RaisePropertyChanged(x => x.AY1Offset);
            }
        }

        public double BZ1Offset
        {
            get
            {
                return Module.Param.CrossCenterAZ1Pos;
            }
            set
            {
                Module.Param.CrossCenterAZ1Pos = value;
                this.RaisePropertyChanged(x => x.BZ1Offset);
            }
        }

        public double BX1Offset
        {
            get
            {
                return Module.Param.CrossCenterBY1Pos;
            }
            set
            {
                Module.Param.CrossCenterBY1Pos = value;
                this.RaisePropertyChanged(x => x.BX1Offset);
            }
        }

        private bool _product_IsJacking = false;
        public bool Product_IsJacking //產品頂升
        {
            get => _product_IsJacking;
            set
            {
                _product_IsJacking = value;
                //if (value == true)
                //{
                //    Module.Y067002_治具頂升氣缸.SetIO(true);
                //}
                //else
                //{
                //    Module.Y067002_治具頂升氣缸.SetIO(false);
                //}
            }
        }

        private bool _product_IsAside = false;
        public bool Product_IsAside //產品靠邊
        {
            get => _product_IsAside;
            set
            {
                _product_IsAside = value;
                if (value == true)
                {
                    Module.Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
                }
                else
                {
                    Module.Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);
                }
            }
        }

        public string Vel
        {
            get => Module.Param.MovingVel.ToString();
            set
            {
                Module.Param.MovingVel = (MoveVelEm)Enum.Parse(typeof(MoveVelEm), value);
                this.RaisePropertyChanged(x => x.Vel);
            }
        }
        public List<string> VelList { get; set; } = new List<string>();

        public CalibrationModule Module { get; set; }

        public void Initial(CalibrationModule module)
        {
            Module = module;
            VelList = Enum.GetNames(typeof(MoveVelEm)).ToList();          
        }
                
        public void Save()
        {
            Module.OnSaveGlobalParam(this, Module);
        }

        public void MoveAY1()
        {
            Module.OnAddTrace("CalibrationModule", "MoveAY1 Start");

            var velAY1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            Module.視覺縱移軸.AbsoluteMove(AY1Offset, velAY1);

            Module.OnAddTrace("CalibrationModule", "MoveAY1 End");
        }

        public void MoveBZ1()
        {
            Module.OnAddTrace("CalibrationModule", "MoveAZ1 Start");

            var velBZ1 = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, Vel);
            Module.BZ1_流道頂升升降軸.AbsoluteMove(BZ1Offset, velBZ1);

            Module.OnAddTrace("CalibrationModule", "MoveBZ1 End");
        }

        public void MoveBX1()
        {
            Module.OnAddTrace("CalibrationModule", "MoveBX1 Start");

            var velBX1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            Module.BX1_流道橫移軸.AbsoluteMove(BX1Offset, velBX1);

            Module.OnAddTrace("CalibrationModule", "MoveBX1 End");
        }
    }
}
