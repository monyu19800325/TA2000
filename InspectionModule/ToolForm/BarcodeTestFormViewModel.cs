using CamBarcodeH20;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HTA.MainController;
using HTA.MotionBase.Utility;
using HTAMachine.Module.AxisConfigModule;
using LVDATA;
using ModuleTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class BarcodeTestFormViewModel
    {
       // [DefineVisionModule("TA1000Vision")] public IMainController BarcodeController;  //呼叫Barcode視覺站

        private InspectionModule _param;
        public Action OnClose;

        private IDispatcherService DispatcherService => this.GetService<IDispatcherService>();

        public object Parameter
        {
            get => _param;
            set
            {
                if (value is InspectionModule p)
                {
                    _param = p;
                    //_isHomed = _param._isInitialized;
                    //_buttonEnabled = _param._isInitialized;
                }
            }
        }

        //private bool _isHomed;

        //private bool _buttonEnabled;

        //public bool ButtonEnabled => _buttonEnabled && _isHomed;

        string _currentBarcode = "Null";
        public string BarcodeId
        {
            get => _currentBarcode;
            set
            {
                if (_currentBarcode == value)
                    return;
                _currentBarcode = value;
                this.RaisePropertyChanged(x => x.BarcodeId);
            }
        }
               

        public double AX1Offset_X //視覺橫移軸
        {
            get => _param.MotorOffset.TrayBarcode_BX1;
            set
            {
                _param.MotorOffset.TrayBarcode_BX1 = value;
                this.RaisePropertyChanged(x => x.AX1Offset_X);
            }
        }
        public double AX1Offset_Z //視覺縱移軸
        {
            get => _param.MotorOffset.TrayBarcode_BZ1;
            set
            {
                _param.MotorOffset.TrayBarcode_BZ1 = value;
                this.RaisePropertyChanged(x => x.AX1Offset_Z);
            }
        }

        public double BY1Offset //流道縱移軸  
        {
            get => _param.MotorOffset.TrayBarcode_AY1;
            set
            {
                _param.MotorOffset.TrayBarcode_AY1 = value;
                this.RaisePropertyChanged(x => x.BY1Offset);
            }
        }
        public string Vel
        {
            get => _param.ProductParam.InspectVel.ToString();
            set
            {
                _param.ProductParam.InspectVel = (MoveVelEm)Enum.Parse(typeof(MoveVelEm), value);
                this.RaisePropertyChanged(x => x.Vel);
            }
        }

        public string BarcodeCatchMode
        {
            get => _param.Param.BarcodeCatchMode.ToString();
            set
            {
                _param.Param.BarcodeCatchMode = (BarcodeCatchModeEm)Enum.Parse(typeof(BarcodeCatchModeEm), value);

                if (_param.Param.BarcodeCatchMode == BarcodeCatchModeEm.Barcode機模式)
                {
                    _labelControl6_Visible = false;
                    _labelControl9_Visible = false;
                    _te_Cam_ZOffset_Visible = false;
                    _btn_Cam_Z_Move_Visible = false;
                }
                else if (_param.Param.BarcodeCatchMode == BarcodeCatchModeEm.相機模式)
                {
                    _labelControl6_Visible = true;
                    _labelControl9_Visible = true;
                    _te_Cam_ZOffset_Visible = true;
                    _btn_Cam_Z_Move_Visible = true;
                }

                this.RaisePropertyChanged(x => x.BarcodeCatchMode);
                this.RaisePropertyChanged(x => x.LabelControl6_Visible);
                this.RaisePropertyChanged(x => x.LabelControl9_Visible);
                this.RaisePropertyChanged(x => x.TE_Cam_ZOffset_Visible);
                this.RaisePropertyChanged(x => x.Btn_Cam_Z_Move_Visible);
            }
        }
        
        private bool _labelControl6_Visible;
        public bool LabelControl6_Visible
        {
            get => _labelControl6_Visible;
            set
            {               
                if (_labelControl6_Visible != value)
                {
                    _labelControl6_Visible = value;
                }
                this.RaisePropertyChanged(x => x.LabelControl6_Visible);
            }
        }
        private bool _labelControl9_Visible;
        public bool LabelControl9_Visible
        {
            get => _labelControl9_Visible;
            set
            {
                if (_labelControl9_Visible != value)
                {
                    _labelControl9_Visible = value;
                }
                this.RaisePropertyChanged(x => x.LabelControl9_Visible);
            }
        }
        private bool _te_Cam_ZOffset_Visible;
        public bool TE_Cam_ZOffset_Visible
        {
            get => _te_Cam_ZOffset_Visible;
            set
            {
                if (_te_Cam_ZOffset_Visible != value)
                {
                    _te_Cam_ZOffset_Visible = value;
                }
                this.RaisePropertyChanged(x => x.TE_Cam_ZOffset_Visible);
            }
        }

        private bool _btn_Cam_Z_Move_Visible;
        public bool Btn_Cam_Z_Move_Visible
        {
            get => _btn_Cam_Z_Move_Visible;
            set
            {
                if (_btn_Cam_Z_Move_Visible != value)
                {
                    _btn_Cam_Z_Move_Visible = value;
                }
                this.RaisePropertyChanged(x => x.Btn_Cam_Z_Move_Visible);
            }
        }

        private bool _product_IsPosition = false;
        public bool Product_IsPosition //產品頂升
        {
            get => _product_IsPosition;
            set
            {
                _product_IsPosition = value;
                if (value == true)
                {
                    _param.Y067001_傳送流道_到位氣缸電磁閥.SetIO(true);
                }
                else
                {
                    _param.Y067001_傳送流道_到位氣缸電磁閥.SetIO(false);
                }
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
                    _param.Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(true);
                }
                else
                {
                    _param.Y067000_傳送流道_靠邊氣缸電磁閥.SetIO(false);
                }
            }
        }


        //public InspectionModule Module { get; set; }
        public List<string> VelList { get; set; } = Enum.GetNames(typeof(MoveVelEm)).ToList();
        public List<string> BarcodeCatahModeList { get; set; } = Enum.GetNames(typeof(BarcodeCatchModeEm)).ToList();

        //    InspectionModule Module  { get; set; }
        public void Initial()
        {
            //Module = module;
            //VelList = Enum.GetNames(typeof(MoveVelEm)).ToList();

            if (_param.Param.BarcodeCatchMode == BarcodeCatchModeEm.Barcode機模式)
            {
                _labelControl6_Visible = false;
                _labelControl9_Visible = false;
                _te_Cam_ZOffset_Visible = false;
                _btn_Cam_Z_Move_Visible = false;
            }
            else if (_param.Param.BarcodeCatchMode == BarcodeCatchModeEm.相機模式)
            {
                _labelControl6_Visible = true;
                _labelControl9_Visible = true;
                _te_Cam_ZOffset_Visible = true;
                _btn_Cam_Z_Move_Visible = true;
            }      
            this.RaisePropertyChanged(x => x.LabelControl6_Visible);
            this.RaisePropertyChanged(x => x.LabelControl9_Visible);
            this.RaisePropertyChanged(x => x.TE_Cam_ZOffset_Visible);
            this.RaisePropertyChanged(x => x.Btn_Cam_Z_Move_Visible);
        }

        public async Task BarcodeTest()
        {
            if (_param.Param.BarcodeCatchMode == BarcodeCatchModeEm.Barcode機模式)
            {
                try
                {
                    //await UpdateUI(false);
                    //_param._currentBarcode = "";

                    var workTask = Task<bool>.Factory.StartNew(() =>
                   {
                       bool ret = false;

                       if (_param.BarcodeReader is IKeyenceBarcodeReader keyenceBarcode)
                       {
                           DateTime timeStamp = DateTime.Now;

                           var doScanTime = DateTime.Now;

                           ret = keyenceBarcode.ScanBarcode(1000);

                           if (ret == false)
                               ret = keyenceBarcode.CloseScanBarcode(1000);

                           _currentBarcode = keyenceBarcode.Barcode;
                       }
                       else
                       {
                           MessageBox.Show("Barcode裝置未連線.", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                       }
                       return ret;
                   });

                    await workTask;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    throw;
                }
                finally
                {
                    this.RaisePropertiesChanged();
                    //await UpdateUI(true);
                }
            }
            else if(_param.Param.BarcodeCatchMode==BarcodeCatchModeEm.相機模式)
            {
                try
                {                    
                    _currentBarcode= _param.BarcodeCamera_Catch();           
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    throw;
                }
                finally
                {
                    this.RaisePropertiesChanged();
                    //await UpdateUI(true);
                }
            }
        }

        //async Task UpdateUI(bool buttonEnabled)
        //{
        //    await DispatcherService.BeginInvoke(() =>
        //    {
        //        _buttonEnabled = buttonEnabled;
        //        this.RaisePropertyChanged(x => x.ButtonEnabled);
        //    });
        //}

        public void Save()
        {
            _param.SaveParam(this, _param);
            _param.SaveVisionProductParam(this, _param);
        }
        public void Cam_X_Move()
        {
            var velAX1 = TATool.SelectVelDef(_param.視覺縱移軸, _param.AY1VelList, Vel);
            _param.視覺縱移軸.AbsoluteMove(AX1Offset_X, velAX1);          
        }
        public void Cam_Z_Move()
        {
            var velAX1 = TATool.SelectVelDef(_param.BZ1_流道頂升升降軸, _param.BZ1VelList, Vel);
            _param.BZ1_流道頂升升降軸.AbsoluteMove(AX1Offset_Z, velAX1);
        }
        public void Carrier_Move()
        {
            var velBY1 = TATool.SelectVelDef(_param.BX1_流道橫移軸, _param.BX1VelList, Vel);
            _param.BX1_流道橫移軸.AbsoluteMove(BY1Offset, velBY1);
        }

       
        public void Finish()
        {
            _param.Param.Barcode_TeachFinish = true;

            _param.SaveParam(this, _param);

            OnClose?.Invoke();
        }
    }
}
