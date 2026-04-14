using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HalconDotNet;
using Hta.MotionBase;
using HTA.MainController;
using HTA.Utility.FormTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class FocusViewModel
    {
        private FocusParam _param = new FocusParam();
        private HTA.Utility.Structure.Rect1 _focusRoi = new HTA.Utility.Structure.Rect1();
        private IAxis _CurrentAxis;
        private ImageWindowAccessor _accessor;
        private HTA.IFramer.IStationFramer _useFramer;
        private HTA.LightServer.ILighter _useLighter;
        private HTA.TriggerServer.ITriggerChannel _useTrigger;
        private List<double> _groupInfo;
        private string _hintStr;
        private List<double> _focusValue = new List<double>();
        private int _capIndex;

        public System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        public List<ItemString> DirList = new List<ItemString>();
        public List<ItemString> StepList = new List<ItemString>();
        public double OldOffsetData;
        public double NewOffsetData;
        public double BaseDist;
        public int MoveTime = 1000;
        public int MoveLimit;
        public Func<string, bool> CheckIoStateNotify;
        public Action OnClose;
        public Action SendGrab;
        public Action<string, string> OnLog;
        public int CamIndex = 0;
        IMainController _controller;
        public List<double> NewOffsetDatas = new List<double>();
        public List<double> OldOffsetDatas = new List<double>();
        public bool IsModifyOffsetData = false;
        public List<int> GroupIndexes = new List<int>();
        public List<int> InGroupCaptureIndexes = new List<int>();
        public List<List<int>> CaptureIndexes = new List<List<int>>();
        public Action<int, int> ChangeCaptureIndex;
        public Action<int, int, int> SetLightDevice;
        public Action<double[]> OnUpdateLightForm;
        private int _selectGroupIndex = 0;
        public int SelectGroupIndex
        {
            get => _selectGroupIndex;
            set
            {
                if (_selectGroupIndex == value)
                    return;
                _selectGroupIndex = value;
                InGroupCaptureIndexes.Clear();
                for (int i = 0; i < _controller.ProductSetting.RoundSettings2[0].Groups[SelectGroupIndex].Captures.Count; i++)
                {
                    InGroupCaptureIndexes.Add(i);
                }
                NewOffsetData = NewOffsetDatas[SelectGroupIndex];
                Offset = NewOffsetData.ToString("#0.00");
                MoveToOtherOffset();
                SelectCaptureIndex = 0;
                this.RaisePropertyChanged(x => x.SelectGroupIndex);
            }
        }
        private int _selectCaptureIndex = 0;
        public int SelectCaptureIndex
        {
            get => _selectCaptureIndex;
            set
            {
                //if (_selectCaptureIndex == value)
                //    return;
                _selectCaptureIndex = value;
                OnUpdateLightForm?.Invoke(_controller.ProductSetting.RoundSettings2[0].Groups[SelectGroupIndex].Captures[_selectCaptureIndex].Light.LightingPersentage.ToArray());
                this.RaisePropertyChanged(x => x.SelectCaptureIndex);
            }
        }
        public void Initial(IAxis axis,
            double baseDist,
            double offsetDist,
            int moveLimit = 2,
            int camIndex = 0,
            ImageWindowAccessor accessor = null,
            HTA.IFramer.IStationFramer useCam = null,
            HTA.LightServer.ILighter useLighter = null,
            HTA.TriggerServer.ITriggerChannel useTrigger = null,
            List<double> groupInfo = null,
            string hintStr = null,
            IMainController mainController = null,
            int targetGroup = 0,
            List<double> offsetList = null)
        {
            _CurrentAxis = axis;
            _accessor = accessor;
            _useFramer = useCam;
            _useLighter = useLighter;
            _useTrigger = useTrigger;
            _groupInfo = groupInfo;
            _hintStr = hintStr;
            MoveLimit = moveLimit;
            CamIndex = camIndex;
            _controller = mainController;

            BaseDist = baseDist;
            OldOffsetData = offsetDist;
            NewOffsetData = offsetDist;
            Offset = offsetDist.ToString();

            if (offsetList.Count == 0)
            {
                offsetList.Add(offsetDist);
            }

            if (_controller.ProductSetting.RoundSettings2[0].Groups.Count != offsetList.Count)
            {
                List<double> tmp = new List<double>();
                if (offsetList.Count > _controller.ProductSetting.RoundSettings2[0].Groups.Count)
                {
                    for (int i = 0; i < offsetList.Count; i++)
                    {
                        tmp.Add(offsetList[i]);
                    }
                    offsetList = tmp;
                }
                else if (offsetList.Count < _controller.ProductSetting.RoundSettings2[0].Groups.Count)
                {
                    for (int i = 0; i < _controller.ProductSetting.RoundSettings2[0].Groups.Count; i++)
                    {
                        if (i < offsetList.Count)
                        {
                            tmp.Add(offsetList[i]);
                        }
                        else
                        {
                            tmp.Add(offsetList[0]);
                        }
                    }
                    offsetList = tmp;
                }
            }

            OldOffsetDatas = HTA.Utility.HTATool.DeepClone(offsetList);
            NewOffsetDatas = HTA.Utility.HTATool.DeepClone(offsetList);

            DirList.Add(new ItemString() { Name = "+" });
            DirList.Add(new ItemString() { Name = "-" });
            StepList.Add(new ItemString() { Name = "0.01" });
            StepList.Add(new ItemString() { Name = "0.1" });
            StepList.Add(new ItemString() { Name = "1" });
            _useFramer.OnGroupAllCaptured += ImageIn;

            for (int i = 0; i < _controller.ProductSetting.RoundSettings2[0].Groups.Count; i++)
            {
                GroupIndexes.Add(i);
                CaptureIndexes.Add(new List<int>());
                for (int j = 0; j < _controller.ProductSetting.RoundSettings2[0].Groups[i].Captures.Count; j++)
                {
                    CaptureIndexes[i].Add(j);
                }
            }

            for (int i = 0; i < CaptureIndexes[0].Count; i++)
            {
                InGroupCaptureIndexes.Add(CaptureIndexes[0][i]);
            }
            var target = GroupIndexes.FirstOrDefault(x => x == targetGroup);
            SelectGroupIndex = target;
            OnUpdateLightForm?.Invoke(_controller.ProductSetting.RoundSettings2[0].Groups[SelectGroupIndex].Captures[SelectCaptureIndex].Light.LightingPersentage.ToArray());
        }

        public void SetChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            Chart1 = chart;
        }

        public void Move()
        {
            OnLog?.Invoke("VisionModule","-FocusTool Move Start");
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
                default:
                    MessageBox.Show("單步距離判別錯誤", "Warning", MessageBoxButtons.OK);
                    return;
            }

            if (MoveLimit != 0)
            {
                if ((NewOffsetData + dir * step > (MoveLimit * 0.5)) || (NewOffsetData + dir * step < (MoveLimit * -0.5)))
                {
                    MessageBox.Show($"移動超出最達移動極限{MoveLimit * 0.5} ~ {MoveLimit * -0.5}");
                    return;
                }
            }

            WaitForm frm = new WaitForm(() => Move(), "移動中...");
            frm.ShowDialog();
            void Move()
            {
                double dist =Math.Round (_CurrentAxis.ActualPos + dir * step,5);
                _CurrentAxis.AbsoluteMove(dist, MoveTime);
                while (true)
                {
                    if (_CurrentAxis.IsMotionDone())
                    {
                        break;
                    }
                    SpinWait.SpinUntil(() => false, 100);
                }
            }
            if (_useTrigger != null)
                _useTrigger.ManualTrigger();
            NewOffsetData = NewOffsetData + dir * step;
            NewOffsetDatas[SelectGroupIndex] = NewOffsetData;
            Offset = NewOffsetData.ToString("#0.00000");
            OnLog?.Invoke("VisionModule", $"-FocusTool Move dist={NewOffsetData} End");
        }
        public void MoveBasePos()
        {
            OnLog?.Invoke("VisionModule", "-FocusTool MoveBasePos Start");
            MoveToBaseDist();
            NewOffsetData = 0;
            Offset = NewOffsetData.ToString("#0.00000");
            OnLog?.Invoke("VisionModule", "-FocusTool MoveBasePos End");
        }

        public void MoveToFocusDist()
        {
            OnLog?.Invoke("VisionModule", $"-FocusTool MoveToFocusDist BaseDist:{BaseDist}, OldOffsetData={OldOffsetData} Start");
            _CurrentAxis.AbsoluteMove(BaseDist + OldOffsetData, MoveTime);
            while (true)
            {
                if (_CurrentAxis.IsMotionDone())
                {
                    break;
                }
                SpinWait.SpinUntil(() => false, 100);
            }
            OnLog?.Invoke("VisionModule", "-FocusTool MoveToFocusDist End");
        }

        private void MoveToBaseDist()
        {
            _CurrentAxis.AbsoluteMove(BaseDist, MoveTime);
            while (true)
            {
                if (_CurrentAxis.IsMotionDone())
                {
                    break;
                }
                SpinWait.SpinUntil(() => false, 100);
            }
        }

        public void SelectRegion()
        {
            OnLog?.Invoke("VisionModule", "-FocusTool SelectRegion Start");
            if (_accessor == null)
                return;

            if (_hintStr != null)
                MessageBox.Show("" + _hintStr, "提示(Hint)");

            using (var mlock = _accessor.OnLockCursor())
            {
                _focusRoi = _accessor.OnDrawRect1(_focusRoi);
            }


            HRegion focusRect = new HRegion();
            focusRect.GenRectangle1(_focusRoi.r1, _focusRoi.c1, _focusRoi.r2, _focusRoi.c2);

            _accessor.AutoUpdate(false);
            _accessor.OnClearObject();

            _accessor.OnAddReg(focusRect, "Focus", "green");

            _accessor.AutoUpdate(true);
            _accessor.OnUpdate();
            OnLog?.Invoke("VisionModule", "-FocusTool SelectRegion End");

        }
        public void AutoFocus()
        {
            OnLog?.Invoke("VisionModule", "-FocusTool AutoFocus Start");
            //var frm = new WaitForm(DoAutoFocus);
            //frm.ShowDialog();
            DoAutoFocus();
            OnLog?.Invoke("VisionModule", "-FocusTool AutoFocus End");
        }

        public void ImageIn(object sender,HTA.IFramer.StationCaptureArgs e)
        {
            OnLog?.Invoke("VisionModule", "-FocusTool ImageIn Start");
            int groupidx = e.GroupIndex;
            HImage catcherImg = e.imgs[CamIndex];
            if (catcherImg == null)
                throw new Exception("Image is NULL");
            HRegion focusRoi = new HRegion();
            focusRoi.GenRectangle1(_focusRoi.r1, _focusRoi.c1, _focusRoi.r2, _focusRoi.c2);
            HImage sobel = catcherImg.SobelAmp("sum_abs", 3);
            sobel.MinMaxGray(focusRoi, 0.0, out double minValue, out double maxValue, out double rangerValue);
            _focusValue.Add(maxValue);
            OnLog?.Invoke("VisionModule", "-FocusTool ImageIn End");
        }

        public void DoAutoFocus()
        {
            //if (InvokeRequired)
            //{
            //    Invoke(new Action(DoAutoFocus));
            //}
            //else
            //{
            if (_focusRoi.Height == 0 || _focusRoi.Width == 0)
            {
                MessageBox.Show("請框選對焦區域");
                return;
            }

            //確認當前軸位置是否超出極限設定位置
            if (CheckAxisOutLimit(_CurrentAxis.ActualPos))
            {
                MessageBox.Show("當前軸超出極限設定位置");
                return;
            }

            //拍攝多張決定亮度

            double startDist = BaseDist;
            //if (!double.TryParse(MoveRange, out double range))
            //{
            //    MessageBox.Show("請輸入數字");
            //    return;
            //}


            //if (!int.TryParse(CaptureTimes, out int times))
            //{
            //    MessageBox.Show("請輸入數字");
            //    return;
            //}

            //NUD_CapTimes.Maximum = 30;

            double stepDist = MoveRange / CaptureTimes;

            MoveAndCalFocus(startDist - (MoveRange * 0.5), startDist + (MoveRange * 0.5), CaptureTimes);
            //step 0.5
            //if (!MoveAndCalFocus(startDist - (MoveRange * 0.5), startDist + (MoveRange * 0.5), CaptureTimes))
            //{//grab fail
            //    MessageBox.Show("Grab Fail!!");
            //    return;
            //}

            Thread t = new Thread(new ThreadStart(CalculateFocus));
            t.Start();

            //var focusInTuple = new HTuple(_focusValue.ToArray());
            //ImageExtendInterFace.Tools.Mean1DArr(focusInTuple.DArr, 7);

            //HTA.Utility.PeakFinder.FindPeak(
            //    focusInTuple,
            //    new HTA.Utility.MeasureCommon.CommonMeasureParam()
            //    {
            //        Refine = HTA.Utility.MeasureCommon.MeasureRefineEm.None,
            //        Polor = HTA.Utility.MeasureCommon.MeasurePolorEm.All,
            //        Select = HTA.Utility.MeasureCommon.MeasureSelectEm.Max,
            //        MeasureThreshold = 0
            //    },
            //    out var selectIdx, out var offset, out var amp);

            //var bestFocusDist = startDist - (MoveRange * 0.5) + (selectIdx[0] + offset[0]) * stepDist;


            ////對焦失敗
            //if (selectIdx[0] == -1 && offset[0] == -1)
            //{
            //    var maxFocus = focusInTuple[0];
            //    var maxFocusIndex = 0;
            //    for (int i = 1; i < focusInTuple.Length; i++)
            //    {
            //        if (maxFocus < focusInTuple[i])
            //        {
            //            maxFocus = focusInTuple[i];
            //            maxFocusIndex = i;
            //        }
            //    }
            //    bestFocusDist = startDist - (MoveRange * 0.5) + (maxFocusIndex) * stepDist;
            //}

            //Chart1.Series.Clear();
            //Chart1.Series.Add("focus value");
            //var s = Chart1.Series[0];
            //for (int i = 0; i < focusInTuple.Length; i++)
            //{
            //    s.Points.AddXY(i, focusInTuple[i]);
            //}


            ////MessageBox.Show(bestFocusValue.ToString());

            //////step 0.1
            ////double tmp = (GetMaxIndexPos(startDist, 0.5, ret, 1) +
            ////              GetMaxIndexPos(startDist, 0.5, ret, 2)) / 2;
            ////
            ////ret = MoveAndCalFocus(tmp, tmp + 0.5, 5);
            ////
            //////step 0.01
            ////tmp = (GetMaxIndexPos(tmp, 0.1, ret, 1) +
            ////       GetMaxIndexPos(tmp, 0.1, ret, 2)) / 2;
            ////
            ////ret = MoveAndCalFocus(tmp, 0.05, 5);
            ////
            ////double maxPos = GetMaxIndexPos(tmp, 0.01, ret, 1);
            ////
            //NewOffsetData = bestFocusDist - startDist;
            //Offset = NewOffsetData.ToString("#0.00");

            //MoveToFocusDist(bestFocusDist);
            //}
        }

        private void CalculateFocus()
        {
            if (SpinWait.SpinUntil(() => _focusValue.Count == CaptureTimes, 10000))
            {
                double stepDist = MoveRange / CaptureTimes;
                double startDist = BaseDist;
                var focusInTuple = new HTuple(_focusValue.ToArray());
                ImageExtendInterFace.Tools.Mean1DArr(focusInTuple.DArr, 7);

                HTA.Utility.PeakFinder.FindPeak(
                    focusInTuple,
                    new HTA.Utility.MeasureCommon.CommonMeasureParam()
                    {
                        Refine = HTA.Utility.MeasureCommon.MeasureRefineEm.None,
                        Polor = HTA.Utility.MeasureCommon.MeasurePolorEm.All,
                        Select = HTA.Utility.MeasureCommon.MeasureSelectEm.Max,
                        MeasureThreshold = 0
                    },
                    out var selectIdx, out var offset, out var amp);

                var bestFocusDist = startDist - (MoveRange * 0.5) + (selectIdx[0] + offset[0]) * stepDist;


                //對焦失敗
                if (selectIdx[0] == -1 && offset[0] == -1)
                {
                    var maxFocus = focusInTuple[0];
                    var maxFocusIndex = 0;
                    for (int i = 1; i < focusInTuple.Length; i++)
                    {
                        if (maxFocus < focusInTuple[i])
                        {
                            maxFocus = focusInTuple[i];
                            maxFocusIndex = i;
                        }
                    }
                    bestFocusDist = startDist - (MoveRange * 0.5) + (maxFocusIndex) * stepDist;
                }

                //NewBestFocusDist = bestFocusDist;


                Chart1.Invoke(new EventHandler(delegate
                {
                    Chart1.Series.Clear();
                    Chart1.Series.Add("focus value");
                    var s = Chart1.Series[0];
                    for (int i = 0; i < focusInTuple.Length; i++)
                    {
                        s.Points.AddXY(i, focusInTuple[i]);
                    }
                }));

                //MessageBox.Show(bestFocusValue.ToString());

                ////step 0.1
                //double tmp = (GetMaxIndexPos(startDist, 0.5, ret, 1) +
                //              GetMaxIndexPos(startDist, 0.5, ret, 2)) / 2;
                //
                //ret = MoveAndCalFocus(tmp, tmp + 0.5, 5);
                //
                ////step 0.01
                //tmp = (GetMaxIndexPos(tmp, 0.1, ret, 1) +
                //       GetMaxIndexPos(tmp, 0.1, ret, 2)) / 2;
                //
                //ret = MoveAndCalFocus(tmp, 0.05, 5);
                //
                //double maxPos = GetMaxIndexPos(tmp, 0.01, ret, 1);
                //
                NewOffsetData = bestFocusDist - startDist;
                if (NewOffsetDatas.Count <= SelectGroupIndex)
                {
                    NewOffsetDatas.Add(NewOffsetData);
                }
                else
                {
                    NewOffsetDatas[SelectGroupIndex] = NewOffsetData;
                }
                Offset = NewOffsetData.ToString("#0.00");

                MoveToFocusDist(bestFocusDist);
                //}
            }
            else
            {
                MessageBox.Show("Grab Fail!!");
            }
        }


        public bool CheckAxisOutLimit(double pos)
        {
            double baseDist = BaseDist;

            if (pos > (baseDist + MoveLimit) ||
                pos < (baseDist - MoveLimit))
            {
                return true;
            }

            return false;
        }

        private void MoveToFocusDist(double dist)
        {
            _CurrentAxis.AbsoluteMove(dist, MoveTime);
            while (true)
            {
                if (_CurrentAxis.IsMotionDone())
                {
                    break;
                }
                SpinWait.SpinUntil(() => false, 100);
            }

            _useTrigger.ManualTrigger();
        }

        private bool MoveAndCalFocus(double startDist, double endDist, int stepTimes)
        {
            _focusValue.Clear();
            _capIndex = 0;
            var gap = (endDist - startDist) / stepTimes;
            HOperatorSet.TupleGenSequence(startDist, endDist + gap, gap, out var axisPos);

            int totalGrabTime = stepTimes;

            for (int i = 0; i < stepTimes; i++)
            {
                _CurrentAxis.AbsoluteMove(axisPos[i], MoveTime);
                while (true)
                {
                    if (_CurrentAxis.IsMotionDone())
                    {
                        break;
                    }
                    SpinWait.SpinUntil(() => false, 100);
                }

                _useTrigger.ManualTrigger();
                SpinWait.SpinUntil(() => false, 20);
            }

            return true;
            //if (SpinWait.SpinUntil(() => _focusValue.Count == totalGrabTime, 10000))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

        }

        public double GetOffsetData()
        {
            //if (OldOffsetData != NewOffsetData)
            //{
            //    if (MessageBox.Show("OffsetData 有調整是否儲存?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        return NewOffsetData;
            //    }
            //}

            //return OldOffsetData;

            for (int i = 0; i < NewOffsetDatas.Count; i++)
            {
                if (OldOffsetDatas[i] != NewOffsetDatas[i])
                {
                    if (MessageBox.Show("OffsetData 有調整是否儲存?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        IsModifyOffsetData = true;
                        return NewOffsetDatas[0];
                    }
                }
            }

            return OldOffsetDatas[0];
        }

        public void DisposeAction()
        {
            if (_useFramer != null)
                _useFramer.OnGroupAllCaptured -= ImageIn;
        }

        public void MoveToOtherOffset()
        {
            OnLog?.Invoke("InspectionModule", "-FocusTool MoveToOtherOffset Start");

            WaitForm frm = new WaitForm(() => Move(), "移動中...");
            frm.ShowDialog();
            void Move()
            {
                double dist = BaseDist + NewOffsetData;
                _CurrentAxis.AbsoluteMove(dist, MoveTime);
                while (true)
                {
                    if (_CurrentAxis.IsMotionDone())
                    {
                        break;
                    }
                    SpinWait.SpinUntil(() => false, 100);
                }
            }
            var capIndex = CalcCaptureNum();
            ChangeCaptureIndex?.Invoke(CamIndex, capIndex);
            SetLightDevice?.Invoke(CamIndex, SelectCaptureIndex, SelectGroupIndex);
            SpinWait.SpinUntil(() => false, 150);
            if (_useTrigger != null)
                _useTrigger.ManualTrigger();

            OnLog?.Invoke("InspectionModule", $"-FocusTool MoveToOtherOffset dist={NewOffsetData} End");
        }

        public int CalcCaptureNum()
        {
            int num = 0;
            for (int i = 0; i < SelectGroupIndex; i++)
            {
                num += _controller.ProductSetting.RoundSettings2[0].Groups[i].Captures.Count;
            }
            num += SelectCaptureIndex;
            return num;
        }

        public int CaptureTimes
        {
            get => _param.CaptureTimes;
            set
            {
                if (_param.CaptureTimes == value)
                    return;
                _param.CaptureTimes = value;
            }
        }
        public double MoveRange
        {
            get => _param.MoveRange;
            set
            {
                if (_param.MoveRange == value)
                    return;
                _param.MoveRange = value;
            }
        }
        public int MoveDir
        {
            get => _param.MoveDir;
            set
            {
                if (_param.MoveDir == value)
                    return;
                _param.MoveDir = value;
            }
        }
        public int Step
        {
            get => _param.Step;
            set
            {
                if (_param.Step == value)
                    return;
                _param.Step = value;
            }
        }

        public string Offset
        {
            get =>_param.Offset;
            set
            {
                if (_param.Offset == value)
                    return;
                _param.Offset = value;
                //this.RaisePropertyChanged(x => x.Offset);
                this.GetService<IDispatcherService>()?.BeginInvoke(() =>
                {
                    this.RaisePropertyChanged(x => x.Offset);
                });
            }
        }

        public string CurrentPos
        {
            get => _param.CurrentPos;
            set
            {
                if (_param.CurrentPos == value)
                    return;
                _param.CurrentPos = value;
            }
        }

    }

    public class FocusParam
    {
        public int CaptureTimes { get; set; } = 20;
        public double MoveRange { get; set; } = 4;
        public int MoveDir { get; set; } = 0;//0正 1逆
        public int Step { get; set; } = 0;
        public string Offset { get; set; } = "0.00000";
        public string CurrentPos { get; set; } = "0.00000";
    }
    public class ItemString
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
