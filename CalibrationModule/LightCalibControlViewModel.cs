using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using TA2000Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class LightCalibControlViewModel
    {
        /// <summary>
        /// 視覺橫移軸 ==>BX1_流道橫移軸
        /// </summary>
        public double X1PosLightCalib
        {
            get => Module.Param.X1PosLightCalib;
            set
            {
                Module.Param.X1PosLightCalib = value;
                this.RaisePropertyChanged(x => x.X1PosLightCalib);
            }
        }

        /// <summary>
        /// 視覺升降軸 ==>BZ1_流道頂升升降軸
        /// </summary>
        public double Z1PosLightCalib
        {
            get => Module.Param.Z1PosLightCalib;
            set
            {
                Module.Param.Z1PosLightCalib = value;
                this.RaisePropertyChanged(x => x.Z1PosLightCalib);
            }
        }

        /// <summary>
        /// AY1_視覺縱移軸
        /// </summary>
        public double Y1PosLightCalib
        {
            get => Module.Param.Y1PosLightCalib;
            set
            {
                Module.Param.Y1PosLightCalib = value;
                this.RaisePropertyChanged(x => x.Y1PosLightCalib);
            }
        }

        public double GrayCardY1Pos
        {
            get => Module.Param.GrayCardY1Pos;
            set
            {
                Module.Param.GrayCardY1Pos = value;
                this.RaisePropertyChanged(x => x.GrayCardY1Pos);
            }
        }


        public double GrayCardZ1Pos
        {
            get => Module.Param.GrayCardZ1Pos;
            set
            {
                Module.Param.GrayCardZ1Pos = value;
                this.RaisePropertyChanged(x => x.GrayCardZ1Pos);
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

        public void Cam_BX1_Move()
        {
            var velX = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            Module.BX1_流道橫移軸.AbsoluteMove(X1PosLightCalib, velX);
        }

        public void AY1_Move()
        {          
            var velY = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            Module.視覺縱移軸.AbsoluteMove(Y1PosLightCalib, velY);
        }
        public void BZ1_Move()
        {
            var velZ = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, Vel);
            Module.BZ1_流道頂升升降軸.AbsoluteMove(Z1PosLightCalib, velZ);
        }

        public void GrayAY1Move()
        {
            var velY = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            Module.視覺縱移軸.AbsoluteMove(GrayCardY1Pos, velY);
        }
        public void GrayAZ1Move()
        {
            var velZ = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, Vel);
            Module.BZ1_流道頂升升降軸.AbsoluteMove(GrayCardZ1Pos, velZ);
        }
    }
}
