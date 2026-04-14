using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using TA2100Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA2100Modules
{
    [POCOViewModel()]
    public class LightCalibControlViewModel
    {
        public double Cam_XOffsetLightCalib
        {
            get
            {
                return Module.Param.Cam_XOffsetLightCalib;
            }
            set
            {
                Module.Param.Cam_XOffsetLightCalib = value;
                this.RaisePropertyChanged(x => x.Cam_XOffsetLightCalib);
            }
        }

        public double Cam_ZOffsetLightCalib
        {
            get
            {
                return Module.Param.Cam_ZOffsetLightCalib;
            }
            set
            {
                Module.Param.Cam_ZOffsetLightCalib = value;
                this.RaisePropertyChanged(x => x.Cam_ZOffsetLightCalib);
            }
        }
        public double Carrier_YOffsetLightCalib
        {
            get => Module.Param.Carrier_YOffsetLightCalib;
            set
            {
                Module.Param.Carrier_YOffsetLightCalib = value;
                this.RaisePropertyChanged(x => x.Carrier_YOffsetLightCalib);
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

        public void AX1_Move()
        {
            var velAX1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            Module.視覺縱移軸.AbsoluteMove(Cam_XOffsetLightCalib, velAX1);    
        }
        public void AZ1_Move()
        {
            var velAZ1 = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, Vel);
            Module.BZ1_流道頂升升降軸.AbsoluteMove(Cam_ZOffsetLightCalib, velAZ1);
        }


        public void Carrier_Move()
        {
            var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            Module.BX1_流道橫移軸.AbsoluteMove(Carrier_YOffsetLightCalib, velBY1);         
        }
    }
}

