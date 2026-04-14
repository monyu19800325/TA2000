using DevExpress.Mvvm.POCO;
using Hta.MotionBase;
using HTA.IFramer;
using HyperInspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionController2.MosaicController;
using HTA.Utility.Structure;
using ObjectDraw;

namespace TA2000Modules
{
    public class MosaicControlViewModel
    {
        public MosaicFormViewModel MosaicService;
        public CalibrationModule Module;
        public string MoveVel { get; set; }
        public List<string> VelList { get; set; } = new List<string>();
        public int SelectX { get; set; }
        public int SelectY { get; set; }
        public List<int> XList { get; set; } = new List<int>();
        public List<int> YList { get; set; } = new List<int>();

        public MosaicForm MosaicForm;

        private int _mCamIdx = 0;//目前此站別僅有一台相機 
        private int _mDirIdx = 0;// 0 : forWard、1 : backWard 
        private int _mCapIdx;//單一相機之光源張數Index


        /// <summary> ForwardCaptureNum
        /// 將使用事件透過 teachProcess、ＦｌｏｗＦｏｒｍ取得ｆｏｒｗａｒｄ取像總張數
        /// </summary> 為 flowSetting 內的FlowHandle.ProductSetting.RunningSettings[0].CaptureNum
        public int ForwardCaptureNum = 1;
        public Func<int, int, List<double>> GetLightingPercentage;
        public Action<string, string> OnLog;
        public HTA.IFramer.IStationFramer Framer;
        public HTA.TriggerServer.ITriggerChannel Trigger;
        public IAxis Axis_Z;
        public IAxis Axis_X;
        public IAxis Axis_Y;
        public HTA.LightServer.ILighter UseLighter;
        public List<double> GroupInfo;
        public string HintStr;
        public Action OnClose;
        public Action<int, int> ChangeCamAndCaptureIndexTrigger;
        public Action OnRefreshBuffer;
        public Action<double[]> SetLighting;

        public double AZ1Pos
        {
            get => Module.Param.MosaicAZ1Pos;
            set
            {
                Module.Param.MosaicAZ1Pos = value;
                this.RaisePropertyChanged(x => x.AZ1Pos);
            }
        }


        public void Init(CalibrationModule module)
        {
            MosaicService = MosaicFormViewModel.MosaicFormViewModelService;
            MosaicService.OnImageGridChange += MosaicService_OnImageGridChange;
            Module = module;
            VelList = Enum.GetNames(typeof(MoveVelEm)).ToList();
            MoveVel = VelList[0];
            XList.Clear();
            if(MosaicService.ImageView != null)
            {
                for (int i = 0; i < MosaicService.ImageView.GetLength(0); i++)
                {
                    XList.Add(i);
                }
                YList.Clear();
                for (int i = 0; i < MosaicService.ImageView.GetLength(1); i++)
                {
                    YList.Add(i);
                }
                SelectX = XList[0];
                SelectY = YList[0];
            }
            Trigger = module.VisionController.Trigger1;
        }

        private void MosaicService_OnImageGridChange(object sender, SmallImage[,] e)
        {
            XList.Clear();
            for (int i = 0; i < e.GetLength(0); i++)
            {
                XList.Add(i);
            }
            YList.Clear();
            for (int i = 0; i < e.GetLength(1); i++)
            {
                YList.Add(i);
            }
            SelectX = XList[0];
            SelectY = YList[0];
        }

        public void CaptureSelect()
        {
            if (MosaicService.ImageView != null)
            {
                var vely = TATool.SelectVelDef(Module.視覺縱移軸, Module.BX1VelList, MoveVel.ToString());
                var velx = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.AY1VelList, MoveVel.ToString());

                //Module.視覺縱移軸.AbsoluteMove(MosaicService.ImageView[SelectX, SelectY].MotionX, velx);
                //Module.BX1_流道橫移軸.AbsoluteMove(MosaicService.ImageView[SelectX, SelectY].MotionY, vely);


                Module.BX1_流道橫移軸.AbsoluteMove(MosaicService.SelectedMosaicParam.CalibrationControlParam.CapturePoints[(SelectX * YList.Count + SelectY)].x, velx);
                Module.視覺縱移軸.AbsoluteMove(MosaicService.SelectedMosaicParam.CalibrationControlParam.CapturePoints[(SelectX * YList.Count + SelectY)].y, vely);

                MosaicService.OnSetSelect(MosaicService.ImageView[SelectX, SelectY]);

                SendGrab();
            } 
        }

        public void CaptureAll()
        {
            Task.Factory.StartNew(() => 
            {
                var vely = TATool.SelectVelDef(Module.視覺縱移軸, Module.BX1VelList, MoveVel.ToString());
                var velx = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.AY1VelList, MoveVel.ToString());
                for (int i = 0; i < MosaicService.ImageView.GetLength(1); i++)
                {
                    for (int j = 0; j < MosaicService.ImageView.GetLength(0); j++)
                    {
                  
                        //Module.BX1_流道橫移軸.AbsoluteMove(MosaicService.SelectedMosaicParam.CalibrationControlParam.CapturePoints[(j * YList.Count + i)].x, velx);
                        //Module.視覺縱移軸.AbsoluteMove(MosaicService.SelectedMosaicParam.CalibrationControlParam.CapturePoints[(j * YList.Count + i)].y, vely);

                        MosaicService.OnSetSelect(MosaicService.ImageView[j, i]);

                        //取像動作
                        //UpdateSelectIndex();
                        //SpinWait.SpinUntil(() => false, 300);

                        SendGrab();
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                }
            });
            
        }


        //待測試
        public void MulitProductMosaicCalib()
        {
            Task.Factory.StartNew(() =>
            {
                for (int k = 0; k < MosaicService.SelectedMosaicParam.CalibrationControlParam.AllProductPosition.Count; k++)
                {
                    var selectProduct = MosaicService.SelectedMosaicParam.CalibrationControlParam.AllProductPosition[k];

                    var vely = TATool.SelectVelDef(Module.視覺縱移軸, Module.BX1VelList, MoveVel.ToString());
                    var velx = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.AY1VelList, MoveVel.ToString());

                    MosaicService.SelectedProductPositionIndex = k ;

                    for (int i = 0; i < MosaicService.ImageView.GetLength(1); i++)
                    {
                        for (int j = 0; j < MosaicService.ImageView.GetLength(0); j++)
                        {
                            Module.BX1_流道橫移軸.AbsoluteMove(MosaicService.SelectedMosaicParam.CalibrationControlParam.CapturePoints[(j * YList.Count + i)].x, velx);
                            Module.視覺縱移軸.AbsoluteMove(MosaicService.SelectedMosaicParam.CalibrationControlParam.CapturePoints[(j * YList.Count + i)].y, vely);


                            MosaicService.OnSetSelect(MosaicService.ImageView[j, i]);

                            SendGrab();
                            SpinWait.SpinUntil(() => false, 1000);
                        }
                    }
                    var result = MosaicService.SetSelectAllProductPosition(selectProduct.Name);

                    if (result == false)
                    {
                        MessageBox.Show("設定dot點位置異常");
                    }
                }

                MessageBox.Show("全產品組圖校正完畢!!!");

            });

            

        }




        public void Save()
        {
            //Module.Param.MosaicCalibrationLights = Module.VisionController.Lighter.GetLight().ToList();
            Module.OnSaveGlobalParam(this, Module);
        }

        public void AZ1_Move()
        {
            var velz = TATool.SelectVelDef(Module.BZ1_流道頂升升降軸, Module.BZ1VelList, MoveVel.ToString());
            Module.BZ1_流道頂升升降軸.AbsoluteMove(AZ1Pos, velz);            
        }

        private void UpdateSelectIndex()
        {
            OnLog?.Invoke("VisionModule", "-Teach UpdateSelectIndex Start");
            var camIdx = Math.Min(Framer.Count - 1, Math.Max(0, _mCamIdx));

            int capIdx = _mDirIdx == 0 ? _mCapIdx : _mCapIdx + ForwardCaptureNum;
            //capIdx = Math.Max(0, capIdx);
            ChangeCamAndCapFunc(camIdx, capIdx);

            SetCurrentLightToDevice(_mDirIdx, capIdx);
            OnLog?.Invoke("VisionModule", "-Teach UpdateSelectIndex End");
        }

        private void ChangeCamAndCapFunc(int camIdx, int captureIdx)
        {
            OnLog?.Invoke("VisionModule", $"-Teach ChangeCamAndCapFunc camIdx={camIdx},captureIdx={captureIdx}  Start");
            ChangeCamAndCaptureIndexTrigger?.Invoke(camIdx, captureIdx);
            OnLog?.Invoke("VisionModule", $"-Teach ChangeCamAndCapFunc camIdx={camIdx},captureIdx={captureIdx} End");
        }

        private void SetCurrentLightToDevice(int dirIdx, int capIdx)
        {
            OnLog?.Invoke("VisionModule", $"-Teach SetCurrentLightToDevice dirIdx={dirIdx},capIdx={capIdx}  Start");
            if (capIdx == -1)
                return;

            //設定到光源上
            var groupInfo = Module.VisionController.ProductSetting.RoundSettings[dirIdx].LightInfo[capIdx].LightingPersentage;

            //var groupInfo = GetLightingPercentage.Invoke(dirIdx, capIdx);
            double[] groupInfoArr = new double[8];
            for (int i = 0; i < groupInfo.Count; i++)
            {
                groupInfoArr[i] = groupInfo[i];
            }
            SetLighting?.Invoke(groupInfoArr);
            OnLog?.Invoke("VisionModule", $"-Teach SetCurrentLightToDevice dirIdx={dirIdx},capIdx={capIdx}  End");
        }


        public void SetProductPos()
        {

            double product_x = Module.Carrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.X;
            double product_y = Module.Carrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.Y;

            Point2d[] ProductCenter = new Point2d[Module.Carrier.InspectData.InspectionPostion.XMaxStepCount * Module.Carrier.InspectData.InspectionPostion.YMaxStepCount];
            for (int index = 0; index < ProductCenter.Length; index++)
            {

                int x_index = index / Module.Carrier.InspectData.InspectionPostion.YMaxStepCount;
                int y_index = index % Module.Carrier.InspectData.InspectionPostion.YMaxStepCount;

                ProductCenter[ProductCenter.Length-1-index] = new Point2d(Module.InspMotorOffsetParam.InspStandBy_X-Module.Carrier.InspectData.InspectionPostion._blockStepX[x_index], Module.InspMotorOffsetParam.InspStandBy_Y+ Module.Carrier.InspectData.InspectionPostion._blockStepY[y_index] );

            }

            int xCount = Module.Carrier.InspectData.InspectionPostion._blockStepX.Length;
            int yCount = Module.Carrier.InspectData.InspectionPostion._blockStepY.Length;

            MosaicForm.SetProductPosition(Module.CurrentProductName,
                new HTA.Utility.Structure.Point2d(product_x, product_y),
                ProductCenter,
                false, false,new Point2d(xCount, yCount), out var productCapturePoints);  //設定產品檔資訊與計算組圖位置(多產品切換)

            List<Point2d> point2Ds = new List<Point2d>();
            if (productCapturePoints.Length > 0)
            {
                for (int index = 0; index < productCapturePoints.Length; index++)
                {
                    point2Ds.AddRange(productCapturePoints[index]);
                  
                }
                Module.NotifyMosaicPos(point2Ds);
            }

            //double maxXY = Math.Max(Module.Carrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.X, Module.Carrier.InspectData.InspectionPostion.Container.IcLayout[0, 0].ContainerSize.Y);
            //double productCenter = Module.Carrier.InspectData.InspectionPostion._blockStepY[0];
            //MosaicForm.SetProductPosition(Module.CurrentProductName, new HTA.Utility.Structure.Point2d(maxXY, 10),
            //    new HTA.Utility.Structure.Point2d[] { new Point2d(productCenter, 10) }, false, false, out var productCapturePoints);

            //List<Point2d> point2Ds = new List<Point2d>();
            //if (productCapturePoints.Length > 0)
            //{
            //    point2Ds.AddRange(productCapturePoints[0]);
            //    Module.NotifyMosaicPos(point2Ds);
            //}
        }

        private void SendGrab()
        {
            Trigger.ManualTrigger();
        }
    }
}
