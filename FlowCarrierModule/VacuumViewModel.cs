using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTA.Com.TCPIP2;
using System.Net;
using System.Drawing;
using DevExpress.Utils.Automation;
using System.Windows.Forms;
using DevExpress.Mvvm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using DevExpress.UIAutomation;
using Hta.MotionBase;
using HTAMachine.Machine;
using DevExpress.Utils.MVVM.Services;
using System.Threading;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class VacuumViewModel
    {


        public IDispatcherService DispatcherService => this.GetService<IDispatcherService>();
        // MessageBox 服務 (顯示訊息框)
        public IMessageBoxService MessageBoxService => this.GetService<IMessageBoxService>();
        public FlowCarrierGlobalParam Param { get; set; }
        public FlowCarrierModule Module { get; set; }
        private bool _btnVCStateFlg = false;//真空旗標
        private bool _btnSVStateFlg = false;//頂升旗標



        public void IOChanged()
        {
            IOChangeEvent(true);

            Module.AI128000_頂升治具真空流量計.OnIOChange += ((sender, args) =>
            {
                if (Module.AI128000_頂升治具真空流量計.GetValue().ToString("0.00") != CVBoatVacuumCurrentValue)
                {
                    CVBoatVacuumCurrentValue = Module.AI128000_頂升治具真空流量計.GetValue().ToString("0.00");
                    EstablishedLight =
                        Module.AI128000_頂升治具真空流量計.GetValue() > Param.CVBoatVacuum_EstablishedValue ? Color.Green : Color.Red;
                }
            });

            Module.Y067002_傳送流道_頂升真空電磁閥.OnIOChange += ((sender, args) =>
            {
                StateLight = Module.Y067002_傳送流道_頂升真空電磁閥.CheckIO() ? Color.Green : Color.Red;
            });

        }

        /// <summary>
        /// 視窗關閉後，取消委派
        /// </summary>
        public void FormClosed()
        {

            IOChangeEvent(false);
            Module.AI128000_頂升治具真空流量計.OnIOChange -= ((sender, args) =>
            {
                if (Module.AI128000_頂升治具真空流量計.GetValue().ToString("0.00") != CVBoatVacuumCurrentValue)
                {
                    CVBoatVacuumCurrentValue = Module.AI128000_頂升治具真空流量計.GetValue().ToString("0.00");
                    EstablishedLight =
                        Module.AI128000_頂升治具真空流量計.GetValue() > Param.CVBoatVacuum_EstablishedValue ? Color.Green : Color.Red;
                }
            });

            Module.Y067002_傳送流道_頂升真空電磁閥.OnIOChange -= ((sender, args) =>
            {
                StateLight = Module.Y067002_傳送流道_頂升真空電磁閥.CheckIO() ? Color.Green : Color.Red;
            });

        }
        /// <summary>
        /// 委派事件 _state : true = 委派 / false = 取消委派
        /// </summary>
        /// <param name="_state"></param>
        private void IOChangeEvent(bool _state)
        {
            if (_state)
            {
                Module.AI128000_頂升治具真空流量計.OnIOChange += AIIOChanged;

                Module.Y067002_傳送流道_頂升真空電磁閥.OnIOChange += IOStateChanged_coler;
            }
            else
            {
                Module.AI128000_頂升治具真空流量計.OnIOChange -= AIIOChanged;

                Module.Y067002_傳送流道_頂升真空電磁閥.OnIOChange -= IOStateChanged_coler;
            }
        }

        public void AIIOChanged(object sender,AnalogIOChangeEvent args)
        {
            if (Module.AI128000_頂升治具真空流量計.GetValue().ToString("0.00") != CVBoatVacuumCurrentValue)
            {
                CVBoatVacuumCurrentValue = Module.AI128000_頂升治具真空流量計.GetValue().ToString("0.00");
                EstablishedLight =
                    Module.AI128000_頂升治具真空流量計.GetValue() > Param.CVBoatVacuum_EstablishedValue ? Color.Green : Color.Red;
            }
        }

        public void IOStateChanged_coler(object sender, IOChangeEvent args)
        {
            StateLight = Module.Y067002_傳送流道_頂升真空電磁閥.CheckIO() ? Color.Green : Color.Red;
        }


        //if (_StateLight != value)
        //        {
        //            _StateLight = value;
        //            //利用DispatcherService避免跨執行續介面問題
        //            DispatcherService?.Invoke(() =>
        //            {
        //    this.RaisePropertyChanged(x => x.StateLight);
        //});
        //        }

        public string CVBoatVacuumEstablishedValue
        {

            get => (Param.CVBoatVacuum_EstablishedValue).ToString("0.00");
            set
            {
                if (value == "") return;
                double _val = Math.Round(Convert.ToDouble(value), 2);
                if (Param.CVBoatVacuum_EstablishedValue == _val)
                    return;
                Param.CVBoatVacuum_EstablishedValue = _val;
                //利用DispatcherService避免跨執行續介面問題
                DispatcherService?.Invoke(() =>
                {
                    this.RaisePropertyChanged(x => x.CVBoatVacuumEstablishedValue);
                });
            }
        }
        private string _VacuumCurrentValue;
        public string CVBoatVacuumCurrentValue
        {
            get => (Module.AI128000_頂升治具真空流量計.GetValue()).ToString("0.00");
            set 
            {
                if (_VacuumCurrentValue != value)
                {
                    _VacuumCurrentValue = value;
                    //利用DispatcherService避免跨執行續介面問題
                    DispatcherService?.Invoke(() =>
                    {
                        this.RaisePropertyChanged(x => x.CVBoatVacuumCurrentValue);
                    });
                }

            }
        }

        private bool _cVBoatVacuumEstablished;
        /// <summary>
        /// 狀態判斷
        /// </summary>
        public bool CVBoatVacuumEstablished
        {
            get => _cVBoatVacuumEstablished =
                Module.AI128000_頂升治具真空流量計.GetValue() > Convert.ToDouble(CVBoatVacuumEstablishedValue) ? true : false;
            private set { }
            
        }
        private Color _StateLight;
        public virtual Color StateLight
        {
            get
            {
                return Module.Y067002_傳送流道_頂升真空電磁閥.GetIO() ? Color.Green : Color.Red;
            }
            set 
            {
                if (_StateLight != value)
                {
                    _StateLight = value;
                    //利用DispatcherService避免跨執行續介面問題
                    DispatcherService?.Invoke(() =>
                    {
                        this.RaisePropertyChanged(x => x.StateLight);
                    });
                }
            }
        }

        private Color _EstablishedLight;
        public virtual Color EstablishedLight
        {
            get{return CVBoatVacuumEstablished ? Color.Green : Color.Red;}
            set
            {
                if (_EstablishedLight != value)
                {
                    _EstablishedLight = value;
                    //利用DispatcherService避免跨執行續介面問題
                    DispatcherService?.Invoke(() =>
                    {
                        this.RaisePropertyChanged(x => x.EstablishedLight);
                    });
                }
            }
        }
        private Color _btnVCLight; // 私有欄位儲存屬性值

        public Color btn_VCLight
        {
            get { return _btnVCStateFlg ? Color.Green : Color.Red; }
            set 
            {
                if (_btnVCLight != value)
                {
                    _btnVCLight = value;
                    this.RaisePropertyChanged(x => x.btn_VCLight);
                }
            }
        }
        private Color _btnSVLight;
        public virtual Color btn_SVLight    
        {
            get { return _btnSVStateFlg ? Color.Green : Color.Red; }
            set
            {
                if (_btnSVLight != value)
                {
                    _btnSVLight = value;
                    this.RaisePropertyChanged(x => x.btn_SVLight);
                }
            }
        }

        private string _btn_VCText;
        public string btn_VCText
        {
            get { return _btnVCStateFlg ? "VC_Open" : "VC_Closed"; }
            set {
                if (_btn_VCText != value)
                {
                    _btn_VCText = value;
                    this.RaisePropertyChanged(x => x.btn_VCText); // 通知 UI 更新
                }
            }
        }
        private string _btn_SVText;
        public string btn_SVText
        {
            get { return _btnSVStateFlg ? "SV_Open" : "SV_Closed"; }
            set {
                if (_btn_SVText != value)
                {
                    _btn_SVText = value;
                    this.RaisePropertyChanged(x => x.btn_SVText); // 通知 UI 更新
                }
            }
        }


        public void Init(FlowCarrierGlobalParam param, FlowCarrierModule module)
        {
            Param = param;
            Module = module;
            IOChanged();
        }

        public void VC_Trigger()
        {
            _btnVCStateFlg = !_btnVCStateFlg; // 每次切換狀態
            Module.Y067002_傳送流道_頂升真空電磁閥.SetIO(_btnVCStateFlg);
            btn_VCLight = _btnVCStateFlg ? Color.Green : Color.Red;
            btn_VCText = _btnVCStateFlg ? "VC_Open" : "VC_Closed";

        }


        public void SV_Trigger()
        {
            _btnSVStateFlg = !_btnSVStateFlg; // 每次切換狀態
            Module.Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(_btnSVStateFlg);
            if (Module.Y067000_傳送流道_靠邊氣缸電磁閥.GetIO() && SpinWait.SpinUntil(() => false, 100))
            {
                Module.Y067002_傳送流道_頂升真空電磁閥.SetIO(true);
            }
            else { Module.Y067002_傳送流道_頂升真空電磁閥.SetIO(false); }
            btn_SVLight = _btnSVStateFlg ? Color.Green : Color.Red;
            btn_SVText = _btnSVStateFlg ? "SV_Open" : "SV_Closed";

        }

       

        public void SaveData()
        {
            Module.Param.CVBoatVacuum_EstablishedValue = Convert.ToDouble(CVBoatVacuumEstablishedValue);
            Module.OnSaveGlobalParam(this, Module);
            // 確保 UI 操作在 UI 執行緒執行
            DispatcherService.BeginInvoke(() =>
            {
                // 顯示 DevExpress 訊息框
                MessageBoxService.ShowMessage("數據已成功寫入！", "確認", MessageButton.OK, MessageIcon.Information);
            });

        }

        public void OnFormClose(object sender, FormClosingEventArgs e)
        {
            Module.Y067002_傳送流道_頂升真空電磁閥.SetIO(false);
            Module.Y067002_傳送流道_頂升真空電磁閥.SetIO(false);

            FormClosed(); //取消委派
        }
    }
}
