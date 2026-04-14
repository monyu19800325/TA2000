using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using HTAMachine.Machine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Forms;
using HTAMachine.Machine;
using HTAMachine.Module;
using DevExpress.Mvvm.DataAnnotations;

namespace TA2000Modules
{
    [POCOViewModel]
    public class TeachTestViewModel
    {
        public virtual bool GetProductEnable { get; set; } = true;
        public virtual bool PutProductEnable { get; set; } = true;
        public virtual bool CaptureEnable { get; set; } = true;
        public virtual bool FocusEnable { get; set; } = true;
        public virtual bool InspectBtnEnable { get; set; } = true;
        public virtual bool InspectStopBtnEnable { get; set; }
        public virtual bool FinishTeachEnable { get; set; } = true;
        public virtual bool TE_InspectCountEnable { get; set; } = true;
        public virtual bool BurningEnable { get; set; } = true;
        public virtual bool LUE_X_Enable { get; set; } = true;
        public virtual bool LUE_Y_Enable { get; set; } = true;
        public virtual bool LUEMoasicEnable { get; set; } = true;
        public virtual bool BtnMoveMosaicEnable { get; set; } = true;

        public Action OnStartInspect;
        public List<int> MapList { get; set; } = new List<int>();
        public virtual int MapIndex { get; set; }
        public int InspectCounts
        {
            get => Vision.InspectCounts;
            set
            {
                if (Vision.InspectCounts == value)
                    return;
                Vision.InspectCounts = value;
                this.RaisePropertyChanged(x => x.InspectCounts);
            }
        }

        public bool IsBurning
        {
            get => Vision.IsBurning;
            set
            {
                if (Vision.IsBurning == value)
                    return;
                Vision.IsBurning = value;
                this.RaisePropertyChanged(x => x.IsBurning);
            }
        }

        public InspectionModule Vision { get; set; }
        public Action<string, string> OnLog;
        public event EventHandler<IModule> SaveParam;
        public InspectionProductParam ProductParam;
        public Action OnClose;
        public void Init()
        {
            for (int i = 0; i < Vision.ProductParam.BigProductMapSetting.MapList.Count; i++)
            {
                MapList.Add(i);
            }
            MapIndex = MapList.FirstOrDefault();
        }

        public void Inspect()
        {
            OnLog?.Invoke("InspectionModule", "-Teach Inspect Start");
            Vision.GoInspect = true;
            Vision.GoInsepctQueue.Add(Vision.GoInspect);
            SaveParam?.Invoke(this, Vision);
            InspectEnabledAllComponent(false);
            OnStartInspect?.Invoke();
            //OnClose?.Invoke();
            OnLog?.Invoke("InspectionModule", "-Teach Inspect End");
        }
        

        /// <summary>
        /// 結束教讀流程
        /// </summary>
        public void FinishTeach()
        {
            OnLog?.Invoke("InspectionModule", "-Teach FinishTeach Start");
            Vision.GoInspect = false;
            Vision.GoInsepctQueue.Add(Vision.GoInspect);
            SaveParam?.Invoke(this, Vision);
            Vision.SaveVisionProductParam(this, Vision);
            OnClose?.Invoke();
            OnLog?.Invoke("InspectionModule", "-Teach FinishTeach End");
        }

        /// <summary>
        /// 臨時暫停檢測(會等待目前整盤檢測結束後再暫停)
        /// </summary>
        public void StopInspect()
        {
            OnLog?.Invoke("InspectionModule", "-Teach StopInspect Start");
            Vision.TeachImmediatelyStop = true;
            //這邊要用Task，介面才不會卡死
            Task.Factory.StartNew(() =>
            {
                var dispatcher = this.GetService<IDispatcherService>();
                dispatcher.Invoke(() =>
                {
                    InspectStopBtnEnable = false;
                });
                var service = Vision.GetHtaService<HTAMachine.Machine.Services.IDialogService>();
                var show = service.ShowDialog(this, new ShowDialogArgs()
                {
                    Button = MessageBoxButtons.OK,
                    Caption = "提示",
                    Message = "等待檢測暫停"
                });
                Vision.TeachBackQueue.Take();
                dispatcher.Invoke(() =>
                {
                    InspectEnabledAllComponent(true);
                });
            });

            OnLog?.Invoke("InspectionModule", "-Teach StopInspect End");
        }
        public void BindFailCode()
        {
            //FailCodeForm failCodeForm;
            //if (Vision.ProductParam.BigProductMapSetting.MapList[MapIndex].UseType == "Mosaic")
            //{
            //    failCodeForm = new FailCodeForm(Vision, Vision.VisionController_Mosaic, MapIndex);
            //}
            //else
            //{
            //    failCodeForm = new FailCodeForm(Vision, Vision.VisionController, MapIndex);
            //}

            //failCodeForm.Show();

            FailLinkForm failLinkForm;
            failLinkForm = new FailLinkForm(Vision);
            failLinkForm.Show();
        }
        public void InspectEnabledAllComponent(bool value)
        {
            GetProductEnable = value;
            PutProductEnable = value;
            CaptureEnable = value;
            FocusEnable = value;
            InspectBtnEnable = value;
            InspectStopBtnEnable = !value;
            FinishTeachEnable = value;
            TE_InspectCountEnable = value;
            BurningEnable = value;

            LUE_X_Enable= value;
            LUE_Y_Enable = value;
            BtnMoveMosaicEnable = value;
            LUEMoasicEnable = value;
        }
    }
}
