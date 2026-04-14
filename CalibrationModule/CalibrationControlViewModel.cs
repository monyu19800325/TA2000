using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HTAMachine.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class CalibrationControlViewModel
    {
        public double AX1Offset //視覺橫移軸
        {
            get => Module.Param.Cam_AX1Offset;
            set
            {
                Module.Param.Cam_AX1Offset = value;
                this.RaisePropertyChanged(x => x.AX1Offset);
            }
        }
        public double AZ1Offset //視覺升降軸  
        {
            get => Module.Param.Cam_AZ1Offset;
            set
            {
                Module.Param.Cam_AZ1Offset = value;
                this.RaisePropertyChanged(x => x.AZ1Offset);
            }
        }

        public double BY1Offset //流道縱移軸  
        {
            get => Module.Param.BY1Offset;
            set
            {
                Module.Param.BY1Offset = value;
                this.RaisePropertyChanged(x => x.BY1Offset);
            }
        }  

        private bool _product_IsJacking= false;
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
            Module.Param.CalibrationLights = Module.VisionController.Lighter.GetLight().ToList();        

            Module.Param.CalibrationGain = Module.VisionController.Framer.Grabbers[0].Gain;

            Module.OnSaveGlobalParam(this, Module);
        }

        public void Cam_AX1_Move()
        {
            var velAX1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            Module.視覺縱移軸.AbsoluteMove(AX1Offset, velAX1);         
        }
        public void Cam_AZ1_Move()
        {
            var velAZ1 = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, Vel);
            Module.BZ1_流道頂升升降軸.AbsoluteMove(AZ1Offset, velAZ1);
        }
        public void Carrier_Move()
        {
            var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            Module.BX1_流道橫移軸.AbsoluteMove(BY1Offset, velBY1);
        }
       
    }
}
