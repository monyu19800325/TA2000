using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Hta.MotionBase;
using HTA.MotionBase.Utility;
using HTA.TriggerServer;
using HTA.Utility.Halcon;
using HTAMachine.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TA2000Modules;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
//using static TA2000Modules.APS_TestFormViewModel;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class LaserCalibControlViewModel
    {
        public CalibrationModule Module { get; set; }
        public List<ItemString> DirList = new List<ItemString>();
        public List<ItemString> StepList = new List<ItemString>();
        public double AX1Offset
        {
            get
            {
                return Module.Param.LaserCenterAX1Pos;
            }
            set
            {
                Module.Param.LaserCenterAX1Pos = value;
                this.RaisePropertyChanged(x => x.AX1Offset);
            }
        }
        public double AZ1Offset
        {
            get
            {
                return Module.Param.LaserCenterAZ1Pos;
            }
            set
            {
                Module.Param.LaserCenterAZ1Pos = value;
                this.RaisePropertyChanged(x => x.AZ1Offset);
            }
        }

        public double BY1Offset
        {
            get
            {
                return Module.Param.LaserCenterBY1Pos;
            }
            set
            {
                Module.Param.LaserCenterBY1Pos = value;
                this.RaisePropertyChanged(x => x.BY1Offset);
            }
        }


        public double LaserCenterX
        {
            get
            {
                return Module.Param.LaserCenterX;
            }
            set
            {
                Module.Param.LaserCenterX = value;
                this.RaisePropertyChanged(x => x.LaserCenterX);
            }
        }
        public double LaserCenterY
        {
            get
            {
                return Module.Param.LaserCenterY;
            }
            set
            {
                Module.Param.LaserCenterY = value;
                this.RaisePropertyChanged(x => x.LaserCenterY);
            }
        }

        public double LaserOffsetX
        {
            get
            {
                return Module.Param.LaserOffsetX;
            }
            set
            {
                Module.Param.LaserOffsetX = value;
                this.RaisePropertyChanged(x => x.LaserOffsetX);
            }
        }

        public double LaserOffsetY
        {
            get
            {
                return Module.Param.LaserOffsetY;
            }
            set
            {
                Module.Param.LaserOffsetY = value;
                this.RaisePropertyChanged(x => x.LaserOffsetY);
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
        public int MoveDir
        {
            get => Module.Param.MoveDir;
            set
            {
                if (Module.Param.MoveDir == value)
                    return;
                Module.Param.MoveDir = value;
            }
        }
        public int Step
        {
            get => Module.Param.Step;
            set
            {
                if (Module.Param.Step == value)
                    return;
                Module.Param.Step = value;
            }
        }

        public List<string> VelList { get; set; } = new List<string>();

        public void Initial(CalibrationModule module)
        {
            Module = module;
            VelList = Enum.GetNames(typeof(MoveVelEm)).ToList();

            DirList.Add(new ItemString() { Name = "+" });
            DirList.Add(new ItemString() { Name = "-" });
            StepList.Add(new ItemString() { Name = "0.01" });
            StepList.Add(new ItemString() { Name = "0.1" });
            StepList.Add(new ItemString() { Name = "1" });
            StepList.Add(new ItemString() { Name = "10" });     
        }

        public void Save()
        {
            Module.OnSaveGlobalParam(this, Module);
        }

        public void MoveAX1()
        {
            Module.OnAddTrace("CalibrationModule", "MoveAX1 Start");

            var velAX1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            Module.視覺縱移軸.AbsoluteMove(AX1Offset, velAX1);

            Module.OnAddTrace("CalibrationModule", "MoveAX1 End");
        }
            
        public void MoveBY1()
        {
            Module.OnAddTrace("CalibrationModule", "MoveBY1 Start");

            var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            Module.BX1_流道橫移軸.AbsoluteMove(BY1Offset, velBY1);

            Module.OnAddTrace("CalibrationModule", "MoveBY1 End");
        }
        
        
        public void MoveAZ1()
        {
            Module.OnAddTrace("CalibrationModule", "MoveAZ1 Start");

            var velAZ1 = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, Vel);
            Module.BZ1_流道頂升升降軸.AbsoluteMove(AZ1Offset, velAZ1);

            Module.OnAddTrace("CalibrationModule", "MoveAZ1 End");
        }

        public void ZeroCalibration()
        {
            //aaa.SetZero(10000);
        }

        private class PositionInfo
        {
            public double Position;
            public double Height;
        }

        public void CenterSearch()
        {
            if (Module.LaserReader is ILaserDistanceFinder _laser)
            {

                #region X方向Search

                var velAX1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);        
                List<PositionInfo> Pos_X_List = CenterScan(Module.視覺縱移軸, velAX1);
                
                double x_Center = CalculateCenter(Pos_X_List);

                //移動至塊規中心
                MoveAxis(Module.視覺縱移軸, x_Center, velAX1);

                LaserCenterX = x_Center;

                #endregion



                #region Y方向Search

                var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);

                List<PositionInfo> Pos_Y_List = CenterScan(Module.BX1_流道橫移軸, velBY1);

                double y_Center = CalculateCenter(Pos_Y_List);

                //移動至塊規中心
                MoveAxis(Module.BX1_流道橫移軸, y_Center, velBY1);

                LaserCenterY = y_Center;


                //Offset紀錄
                LaserOffsetX = LaserCenterX - AX1Offset;
                LaserOffsetY = LaserCenterY - BY1Offset;

                #endregion
            }
        }
        public void MoveToCameraCenter()
        {
            var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            MoveAxis(Module.BX1_流道橫移軸, LaserCenterY + 189.5, velBY1); //雷射中心與相機中心距離189.5 mm
        }

        public void LaserCenterMove()
        {
            var velAX1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            MoveAxis(Module.視覺縱移軸, LaserCenterX, velAX1); 

            var velBY1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.BX1VelList, Vel);
            MoveAxis(Module.BX1_流道橫移軸, LaserCenterY , velBY1); //雷射中心與相機中心距離189.5

            //歸0
            //LaserOffsetX = 0;
            //LaserOffsetY = 0;
        }
        public void Offset_X_Move()
        {
            int dir = MoveDir == 0 ? 1 : -1;
            double step;
            switch (Step)
            {
                case 0:
                    step = 0.01;
                    break;
                case 1:
                    step = 0.1;
                    break;
                case 2:
                    step = 1;
                    break;
                case 3:
                    step = 10;
                    break;
                default:
                    MessageBox.Show("單步距離判別錯誤", "Warning", MessageBoxButtons.OK);
                    return;
            }

            double dist = Module.視覺縱移軸.ActualPos + dir * step;
            Module.視覺縱移軸.AbsoluteMove(dist, 3000);
            while (true)
            {
                if (Module.視覺縱移軸.IsMotionDone())
                {
                    break;
                }
                SpinWait.SpinUntil(() => false, 100);
            }

            LaserOffsetX= LaserOffsetX+ dir * step;
        }
        public void Offset_Y_Move()
        {
            int dir = MoveDir == 0 ? 1 : -1;
            double step;
            switch (Step)
            {
                case 0:
                    step = 0.01;
                    break;
                case 1:
                    step = 0.1;
                    break;
                case 2:
                    step = 1;
                    break;
                case 3:
                    step = 10;
                    break;
                default:
                    MessageBox.Show("單步距離判別錯誤", "Warning", MessageBoxButtons.OK);
                    return;
            }

            double dist = Module.BX1_流道橫移軸.ActualPos + dir * step;
            Module.BX1_流道橫移軸.AbsoluteMove(dist, 3000);
            while (true)
            {
                if (Module.BX1_流道橫移軸.IsMotionDone())
                {
                    break;
                }
                SpinWait.SpinUntil(() => false, 100);
            }

            LaserOffsetY = LaserOffsetY + dir * step;        
        }

        public bool MoveAxis(IAxis axis, double dist, Velocity vel = null, int moveTimeout = 0, int waitTimeout = 0)
        {
            Velocity axisVel = new Velocity();

            if (vel == null)
                axisVel = axis.MoveVelocity;
            else
                axisVel = vel;

            bool moveResult = axis.AbsoluteMove(dist, axisVel, moveTimeout);
            bool waitResult = axis.WaitMotionDone(waitTimeout);
            bool isMoveSuccess = moveResult && waitResult && Math.Abs(axis.ActualPos - dist) < 2;
            //LogTrace($"MoveAxis - Axis:{axis.Name}, dist:{dist}, ActualPos:{axis.ActualPos}, moveTimeout:{moveTimeout}, waitTimeout:{waitTimeout}, IsMoveSuccess:{isMoveSuccess}");

            if (isMoveSuccess == false)
            {
                //LogTrace($"MoveAxisFailed - Axis:{axis.Name}, dist:{dist}, ActualPos:{axis.ActualPos}, ALM:{axis.CurrentStatus.ALM}, INP:{axis.CurrentStatus.INP}, SVON:{axis.CurrentStatus.SVON}, EMG:{axis.CurrentStatus.EMG}, NSTP:{axis.CurrentMoveStatus.NSTP}, CSTP:{axis.CurrentMoveStatus.CSTP}");
                //LogTrace($"MoveAxisFailed - ErrorCode:{axis.ErrorCode.ToString()}, Message{axis.ErrorString}");
            }
            return isMoveSuccess;
        }

        private List<PositionInfo> CenterScan(IAxis axis, Velocity vel)
        {
            double position = 0;

            if (axis.Name =="AX1") position = AX1Offset;
            if (axis.Name == "BY1") position = BY1Offset;
            var _vel = vel.DeepClone();
            _vel.MaxVel = 5;

            if (Module.LaserReader is ILaserDistanceFinder _laser)
            {              
                //移動至啟動位置           
                MoveAxis(axis, position - 7.5, _vel, 10000,10000); //往前移動7.5mm (一半塊規寬+5mm)

                //開始移動
                //MoveAxis(axis, axis.ActualPos + 15, _vel);
                bool moveResult = axis.AbsoluteMove(position + 7.5, _vel, 0);
           

                List<PositionInfo> Pos_List = new List<PositionInfo>();

                while (Pos_List.Count<300)
                {
                    var laserSucces = _laser.ReadHeight(1000);
                    if (laserSucces)
                    {
                        PositionInfo Pos_Info = new PositionInfo();

                        Pos_Info.Position = axis.ActualPos;
                       
                        double hetghtvalue = _laser.GetHeight();
                        Pos_Info.Height = hetghtvalue;

                        if (hetghtvalue < 30)
                            Pos_List.Add(Pos_Info);
                    }
                    SpinWait.SpinUntil(() => false, 10);
                }
                
                axis.WaitMotionDone(10000);

                return Pos_List;
            }

            return null;
        }


        private double CalculateCenter(List<PositionInfo> Pos_List)
        {
            double minHeight = Pos_List.OrderBy(p => p.Height).First().Height;
            double maxHeight = Pos_List.OrderBy(p => p.Height).Last().Height;

            var meanHeight = (minHeight + maxHeight) / 2.0;

            var result = Pos_List.Where(p => p.Height > meanHeight).ToList();

            double minPosit = result.OrderBy(p => p.Position).First().Position;
            double maxPosit = result.OrderBy(p => p.Position).Last().Position;

            var cneterPosit = (minPosit + maxPosit) / 2.0;

            return cneterPosit;
        }

    }
}
