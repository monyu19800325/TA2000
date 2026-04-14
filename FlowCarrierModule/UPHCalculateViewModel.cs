using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.DataAnnotations;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class UPHCalculateViewModel
    {

        public string UPH
        {
            get
            {
                if (TotalCount == 0)
                {
                    return "0";
                }
                if (SpendTime.TotalSeconds == 0)
                {
                    return "0";
                }
                var uph = TotalCount / SpendTime.TotalHours;
                string uphStr = uph.ToString("F2");
                return uphStr;
            }
        }
        private int _totalCount = 0;
        public int TotalCount
        {
            get => _totalCount;
            set
            {
                _totalCount = value;
                service?.Invoke(() => {
                    this.RaisePropertiesChanged();
                });
            }
        }
        private TimeSpan _spendTime = new TimeSpan();
        public TimeSpan SpendTime
        {
            get => _spendTime;
            set
            {
                _spendTime = value;
                ShowSpendTime = $"{value.TotalHours:F6}";
                //service?.Invoke(() => {
                //    this.RaisePropertiesChanged();
                //});
            }
        }
        private string _showSpendTime = "0.00000";
        public string ShowSpendTime
        {
            get => _showSpendTime;
            set
            {
                _showSpendTime = value;
                service?.Invoke(() => {
                    this.RaisePropertiesChanged();
                });
            }
        }

        private TimeSpan _inspectionSpendTime = new TimeSpan();
        public TimeSpan InspectionSpendTime
        {
            get => _inspectionSpendTime;
            set
            {
                _inspectionSpendTime = value;
                ShowInspectionSpendTime = $"{value.TotalHours:F6}";
                //service?.Invoke(() => {
                //    this.RaisePropertiesChanged();
                //});
            }
        }
        private string _showInspectionSpendTime = "0.00000";
        public string ShowInspectionSpendTime
        {
            get => _showInspectionSpendTime;
            set
            {
                _showInspectionSpendTime = value;
                service?.Invoke(() => {
                    this.RaisePropertiesChanged();
                });
            }
        }
        public string InspectionUPH
        {
            get
            {
                if (TotalCount == 0)
                {
                    return "0";
                }
                if (InspectionSpendTime.TotalSeconds == 0)
                {
                    return "0";
                }
                var uph = TotalCount / InspectionSpendTime.TotalHours;
                string uphStr = uph.ToString("F2");
                return uphStr;
            }
        }
        private FlowCarrierModule _module;
        IDispatcherService service;

        public void Init(FlowCarrierModule module)
        {
            _module = module;
            service = this.GetService<IDispatcherService>();
            _module.UPHClass1.UpdateView = OnUpdate;
        }

        public void UpdateSpendTime(TimeSpan timeSpan)
        {
            SpendTime = timeSpan;
        }

        public void OnUpdate()
        {
            TotalCount = _module.UPHClass1.TotalCount;
            SpendTime = _module.UPHClass1.SpendTime;
            InspectionSpendTime = _module.UPHClass1.InspectionSpendTime;
        }
    }

    public class UPHClass
    {
        public int TotalCount { get; set; }
        public TimeSpan SpendTime { get; set; } = new TimeSpan(0, 0, 0, 0);
        public TimeSpan InspectionSpendTime { get; set; } = new TimeSpan(0, 0, 0, 0);

        public Action UpdateView;
        public UPHClass()
        {
        }

        public void Update()
        {
            UpdateView?.Invoke();
        }

        public void ClearAll()
        {
            TotalCount = 0;
            SpendTime = new TimeSpan(0, 0, 0, 0);
            InspectionSpendTime = new TimeSpan(0, 0, 0, 0);
        }
    }

}
