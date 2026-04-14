using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class Pxl3DCalibControlViewModel
    {
        /// <summary>
        /// 視覺橫移軸
        /// </summary>
        public double X1Pos3D
        {
            get => Module.Param.X1Pos3D;
            set
            {
                Module.Param.X1Pos3D = value;
                this.RaisePropertyChanged(x => x.X1Pos3D);
            }
        }
        /// <summary>
        /// 視覺升降軸
        /// </summary>
        public double Z1Pos3D
        {
            get => Module.Param.Z1Pos3D;
            set
            {
                Module.Param.Z1Pos3D = value;
                this.RaisePropertyChanged(x => x.Z1Pos3D);
            }
        }
        /// <summary>
        /// 吸嘴縱移軸
        /// </summary>
        public double Y1Pos3D
        {
            get => Module.Param.Y1Pos3D;
            set
            {
                Module.Param.Y1Pos3D = value;
                this.RaisePropertyChanged(x => x.Y1Pos3D);
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

        public void CX1_Move()
        {
            var velX = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            Module.BX1_流道橫移軸.AbsoluteMove(X1Pos3D, velX);
        }

        public void AY1_Move()
        {
            var velY = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            Module.視覺縱移軸.AbsoluteMove(Y1Pos3D, velY);
        }
        public void AZ1_Move()
        {
            var velZ = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, Vel);
            Module.BZ1_流道頂升升降軸.AbsoluteMove(Z1Pos3D, velZ);
        }
    }
}
