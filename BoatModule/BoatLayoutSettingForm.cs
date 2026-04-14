using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base.ProductMeasureTool;
//using DevExpress.Mvvm.POCO;
using DisplayForm;
using Hta.Container;
using HTAMachine.Annotations;
using HTAMachine.Machine;
using HTAMachine.Machine.Services;
using ObjectDraw;


namespace TA2000Modules
{
    public partial class BoatLayoutSettingForm : UserControl, IDisposable
    {

        private ViewModel _viewModel;
        private BoatModule _boatModule;
      

        public BoatLayoutSettingForm(BoatLayoutSetting setting,BoatModule boatModule)
        {
            InitializeComponent();
            _viewModel = new ViewModel(setting);
            SetupProductBinding();
            SetupTrayBinding();
            SetupICBinding();
            SetupBlockBinding();
            SetupFOVProductBinding();

            setting.TrayInfo.TrayContainerDesc.CenterOffset = new Point2D(0, 0);
            setting.TrayInfo.BlockContainerDesc.CenterOffset = new Point2D(0, 0);
            
            if (_viewModel.useSingleIC)
            {
                setting.TrayInfo.TrayContainerDesc.FirstGap = new Point2D(0, 0);
                setting.TrayInfo.TrayContainerDesc.SubDimSize = new Point(1, 1);
            }
            //setting.TrayInfo.TrayContainerDesc.FirstGap = new Point2D(0, 0);
            //setting.TrayInfo.TrayContainerDesc.SubDimSize = new Point(1, 1);


            _boatModule = boatModule;
        }






        private void SetupProductBinding()
        {
            Stripe_radioButton.DataBindings.Add(nameof(Stripe_radioButton.Checked), _viewModel,
                nameof(_viewModel.useStripe),
                true, DataSourceUpdateMode.OnPropertyChanged);
            SingleIC_radioButton.DataBindings.Add(nameof(SingleIC_radioButton.Checked), _viewModel,
                nameof(_viewModel.useSingleIC),
                true, DataSourceUpdateMode.OnPropertyChanged);
            

            //顯示或關閉BlockInfo設定視窗
            gb_BlockInfo.Visible = !_viewModel.useSingleIC;
            
        }

        private void SetupFOVProductBinding()
        {
            //SignalProduct_radioButton.DataBindings.Add(nameof(SignalProduct_radioButton.Checked), _viewModel,
            //    nameof(_viewModel.useFOVSingleProduct),
            //    true, DataSourceUpdateMode.OnPropertyChanged);
            //MultiProduct_radioButton.DataBindings.Add(nameof(MultiProduct_radioButton.Checked), _viewModel,
            //    nameof(_viewModel.useFOVMultiProduct),
            //    true, DataSourceUpdateMode.OnPropertyChanged);
            //MultiProductMAX_radioButton.DataBindings.Add(nameof(MultiProductMAX_radioButton.Checked), _viewModel,
            //    nameof(_viewModel.useFOVMultiProductMAX),
            //    true, DataSourceUpdateMode.OnPropertyChanged);

            SignalProduct_radioButton.CheckedChanged += (s, e) =>
            {
                if (SignalProduct_radioButton.Checked)
                    _viewModel.SelectedFOVMode = ViewModel.FOVMode.Single;
            };

            MultiProduct_radioButton.CheckedChanged += (s, e) =>
            {
                if (MultiProduct_radioButton.Checked)
                    _viewModel.SelectedFOVMode = ViewModel.FOVMode.Multi;
            };

            MultiProductMAX_radioButton.CheckedChanged += (s, e) =>
            {
                if (MultiProductMAX_radioButton.Checked)
                    _viewModel.SelectedFOVMode = ViewModel.FOVMode.MultiMax;
            };




            FOVXNum.DataBindings.Add(nameof(FOVXNum.Value), _viewModel,
                nameof(_viewModel.FOVProductXNum),
                true, DataSourceUpdateMode.OnPropertyChanged);
            FOVYNum.DataBindings.Add(nameof(FOVYNum.Value), _viewModel,
                nameof(_viewModel.FOVProductYNum),
                true, DataSourceUpdateMode.OnPropertyChanged);


            //CB_FOVSingle.DataBindings.Add("Checked", _viewModel,
            //        nameof(ViewModel.IsFovSingle),
            //        true, DataSourceUpdateMode.OnPropertyChanged);


            //_viewModel.PropertyChanged += (s, e) =>
            //{
            //    if (e.PropertyName == nameof(_viewModel.SelectedFOVMode))
            //    {
            //        SignalProduct_radioButton.Checked = _viewModel.useFOVSingleProduct;
            //        MultiProduct_radioButton.Checked = _viewModel.useFOVMultiProduct;
            //        MultiProductMAX_radioButton.Checked = _viewModel.useFOVMultiProductMAX;
            //        gb_FOVXYInfo.Visible = _viewModel.useFOVMultiProduct;
            //    }

            //    //TraySizeXNum.Value = (decimal)(_viewModel.TraySizeX);
            //    TraySizeXNum.Value = 500;
            //    TraySizeXNum.Update();
            //    TraySizeXNum.Refresh();
            //};

            // 初始載入時，手動刷新 UI 顯示狀態（這就是你的三行）
            SignalProduct_radioButton.Checked = _viewModel.useFOVSingleProduct;
            MultiProduct_radioButton.Checked = _viewModel.useFOVMultiProduct;
            MultiProductMAX_radioButton.Checked = _viewModel.useFOVMultiProductMAX;



            gb_FOVXYInfo.Visible = _viewModel.useFOVMultiProduct ;

        }




        private void SetupICBinding()
        {
            IcSizeXNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.IcSizeX),
                true, DataSourceUpdateMode.OnPropertyChanged);
            IcSizeYNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.IcSizeY),
                true, DataSourceUpdateMode.OnPropertyChanged);
            ProductThinkness.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.ProductThinkness),
                true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetupBlockBinding()
        {
            BlockSizeXNum.DataBindings.Add(nameof(BlockSizeXNum.Value), _viewModel,
                nameof(_viewModel.BlockSizeX),
                true, DataSourceUpdateMode.OnPropertyChanged);
            BlockSizeYNum.DataBindings.Add(nameof(BlockSizeYNum.Value), _viewModel,
                nameof(_viewModel.BlockSizeY),
               true, DataSourceUpdateMode.OnPropertyChanged);
            BlockFirstGapXNum.DataBindings.Add(nameof(BlockFirstGapXNum.Value), _viewModel,
                nameof(_viewModel.BlockFistGapX),
                  true, DataSourceUpdateMode.OnPropertyChanged);
            BlockFirstGapYNum.DataBindings.Add(nameof(BlockFirstGapYNum.Value), _viewModel,
                nameof(_viewModel.BlockFistGapY),
                 true, DataSourceUpdateMode.OnPropertyChanged);
            BlockDimXNum.DataBindings.Add(nameof(BlockDimXNum.Value), _viewModel,
                nameof(_viewModel.BlockDimNumX),
                true, DataSourceUpdateMode.OnPropertyChanged);
            BlockDimYNum.DataBindings.Add(nameof(BlockDimYNum.Value), _viewModel,
                 nameof(_viewModel.BlockDimNumY),
                true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetupTrayBinding()
        {
            TraySizeXNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.TraySizeX),
                true, DataSourceUpdateMode.OnPropertyChanged);

            TraySizeYNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.TraySizeY),
                true, DataSourceUpdateMode.OnPropertyChanged);

            ICFirstGapXNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.IcFirstGapX),
                true, DataSourceUpdateMode.OnPropertyChanged);
            ICFirstGapYNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.IcFirstGapY),
                true, DataSourceUpdateMode.OnPropertyChanged);
            ICDimXNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.IcDimX),
                true, DataSourceUpdateMode.OnPropertyChanged);
            ICDimYNum.DataBindings.Add("Value", _viewModel,
                nameof(_viewModel.IcDimY),
                true, DataSourceUpdateMode.OnPropertyChanged);
           


        }

              
        public void UpdateBoatLayoutSettingView( BoatLayoutSetting setting)
        {
            //_viewModel = new ViewModel(setting);

            _viewModel.BlockSizeX = setting.TrayInfo.BlockContainerDesc.ContainerSize.X;
            _viewModel.BlockSizeY= setting.TrayInfo.BlockContainerDesc.ContainerSize.Y;
            _viewModel.BlockFistGapX= setting.TrayInfo.TrayContainerDesc.FirstGap.X;
            _viewModel.BlockFistGapY = setting.TrayInfo.TrayContainerDesc.FirstGap.Y;
            _viewModel.BlockDimNumX = setting.TrayInfo.TrayContainerDesc.SubDimSize.X;
            _viewModel.BlockDimNumY = setting.TrayInfo.TrayContainerDesc.SubDimSize.Y;

            _viewModel.IcSizeX = setting.TrayInfo.IcDesc.ContainerSize.X;
            _viewModel.IcSizeY = setting.TrayInfo.IcDesc.ContainerSize.Y;
            _viewModel.IcFirstGapX= setting.TrayInfo.BlockContainerDesc.FirstGap.X;
            _viewModel.IcFirstGapY = setting.TrayInfo.BlockContainerDesc.FirstGap.Y;
            _viewModel.IcDimX = setting.TrayInfo.BlockContainerDesc.SubDimSize.X;
            _viewModel.IcDimY = setting.TrayInfo.BlockContainerDesc.SubDimSize.Y;

            _viewModel.ProductThinkness = setting.TrayInfo.IcDesc.ContainerThickness;
        }


        private TrayViewForm _trayViewForm;
        private void button1_Click(object sender, EventArgs e)
        {
            if (_trayViewForm == null)
                _trayViewForm = new TrayViewForm();

            //_trayViewForm.SetupTray(_viewModel.TrayDesc.GenerateTray());
            //設定產品繪製資訊
            _trayViewForm.SetupTray(_viewModel.Desc.TrayInfo.GenerateTray());


            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;


            _trayViewForm.TopMost = true;

            var leftPos = new Point(this.Right, this.Top);
            var posOnScreen = this.PointToScreen(leftPos);
            _trayViewForm.Left = posOnScreen.X + 20;
            _trayViewForm.Top = posOnScreen.Y;
            _trayViewForm.Show();

            _trayViewForm.Disposed += (o, args) =>
            {
                _viewModel.PropertyChanged -= ViewModelOnPropertyChanged;
                _trayViewForm.Close();
                _trayViewForm = null;
            };
        }
        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _trayViewForm.SetupTray(_viewModel.TrayDesc.GenerateTray());
        }

        private void Stripe_radioButton_Click(object sender, EventArgs e)
        {
            gb_BlockInfo.Visible = true;
            Stripe_radioButton.Checked = true;
        }

        private void SingleIC_radioButton_Click(object sender, EventArgs e)
        {
            gb_BlockInfo.Visible = false;
            SingleIC_radioButton.Checked = true;
        }
              
       
        private void BoatSettingByVision_Click(object sender, EventArgs e)
        {
            _boatModule.ProductMeasureToolInfo._BaseProductSettingData.BlockRowNum = _boatModule.Settings.TrayInfo.TrayContainerDesc.SubDimSize.Y;
            _boatModule.ProductMeasureToolInfo._BaseProductSettingData.BlockColNum = _boatModule.Settings.TrayInfo.TrayContainerDesc.SubDimSize.X;

            _boatModule.ProductMeasureToolInfo._BaseProductSettingData.ProductThickenss = _boatModule.Settings.TrayInfo.IcDesc.ContainerThickness;
        
            #region For Test
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.BoatOriginPosition_X = 327.55;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.BoatOriginPosition_Y = 552.86;

            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_RightUpProduct_RightUpCorner_X = 286.05;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_RightUpProduct_RightUpCorner_Y = 590.96;

            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_RightUpProduct_LeftDownCorner_X = 277.55;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_RightUpProduct_LeftDownCorner_Y = 599.36;           

            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_LeftDownProduct_RightUpCorner_X = 186.35;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_LeftDownProduct_RightUpCorner_Y = 666.06;

            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_LeftDownProduct_LeftDownCorner_X = 177.95;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_LeftDownProduct_LeftDownCorner_Y = 674.36;

            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.LeftDownBlock_LeftDownProduct_RightUpCorner_X = 68.15;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.LeftDownBlock_LeftDownProduct_RightUpCorner_Y = 666.36;

            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.LeftDownBlock_LeftDownProduct_LeftDownCorner_X = 59.85;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.LeftDownBlock_LeftDownProduct_LeftDownCorner_Y = 674.66;

            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_RightUpCorner_X = 290.05;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_RightUpCorner_Y = 587.96;
            // _boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_LeftDownCorner_X = 174.05;
            //_boatModule.ProductMeasureToolInfo._BaseProductSettingData.RightUpBlock_LeftDownCorner_Y = 677.16;
            #endregion

            ProductMeasureTool _productMeasureTool = new ProductMeasureTool(0, _boatModule, "TA1000");
            _productMeasureTool.ShowDialog();

            _productMeasureTool.FormClosed += (ss, ee) =>
            {
                _productMeasureTool = null;
            };
        }
    }

    class ViewModel : INotifyPropertyChanged
    {
        public TrayDesc TrayDesc;
        public BoatLayoutSetting Desc;

        public ViewModel(BoatLayoutSetting desc)
        {
            Desc = desc;
            TrayDesc = desc.TrayInfo;

           

        }

        #region  Tray

        public double TraySizeX
        {
            get => TrayDesc.TrayContainerDesc.ContainerSize.X;
            set
            {
                //if (Math.Abs(value - TrayDesc.TrayContainerDesc.ContainerSize.X) < 0.001) return;
                TrayDesc.TrayContainerDesc.ContainerSize = new Point2D(value, TrayDesc.TrayContainerDesc.ContainerSize.Y);
                TrayDesc.BlockContainerDesc.ContainerSize = new Point2D(value, TrayDesc.TrayContainerDesc.ContainerSize.Y);
                this.OnPropertyChanged(TraySizeX.ToString());       
            }
        }
        public double TraySizeY
        {
            get => TrayDesc.TrayContainerDesc.ContainerSize.Y;
            set
            {
                //if (Math.Abs(value - TrayDesc.TrayContainerDesc.ContainerSize.Y) < 0.001) return;
                TrayDesc.TrayContainerDesc.ContainerSize = new Point2D(TrayDesc.TrayContainerDesc.ContainerSize.X, value);
                TrayDesc.BlockContainerDesc.ContainerSize = new Point2D(TrayDesc.TrayContainerDesc.ContainerSize.X, value);
                this.OnPropertyChanged(TraySizeY.ToString());
            }
        }
        public double IcFirstGapX
        {
            get => TrayDesc.BlockContainerDesc.FirstGap.X;
            set
            {
                //if (Math.Abs(value - TrayDesc.BlockContainerDesc.FirstGap.X) < 0.001) return;
                TrayDesc.BlockContainerDesc.FirstGap = new Point2D(value, TrayDesc.BlockContainerDesc.FirstGap.Y);
                this.OnPropertyChanged();
            }
        }
        public double IcFirstGapY
        {
            get => TrayDesc.BlockContainerDesc.FirstGap.Y;
            set
            {
                //if (Math.Abs(value - TrayDesc.BlockContainerDesc.FirstGap.Y) < 0.001) return;
                TrayDesc.BlockContainerDesc.FirstGap = new Point2D(TrayDesc.BlockContainerDesc.FirstGap.X, value);
                this.OnPropertyChanged();
            }
        }


        public int IcDimX
        {
            get => TrayDesc.BlockContainerDesc.SubDimSize.X;
            set
            {
                //if (Math.Abs(value - TrayDesc.BlockContainerDesc.SubDimSize.X) < 0.001) return;
                TrayDesc.BlockContainerDesc.SubDimSize = new Point(value, TrayDesc.BlockContainerDesc.SubDimSize.Y);
                this.OnPropertyChanged(IcDimX.ToString());
            }
        }
        public int IcDimY
        {
            get => TrayDesc.BlockContainerDesc.SubDimSize.Y;
            set
            {
                //if (Math.Abs(value - TrayDesc.BlockContainerDesc.SubDimSize.Y) < 0.001) return;
                TrayDesc.BlockContainerDesc.SubDimSize = new Point(TrayDesc.BlockContainerDesc.SubDimSize.X, value);
                this.OnPropertyChanged(IcDimX.ToString());
            }
        }

        #endregion



        #region Block

        //Block SizeX
        public double BlockSizeX
        {
            get => TrayDesc.BlockContainerDesc.ContainerSize.X;
            set
            {
                //if (Math.Abs(value - TrayDesc.BlockContainerDesc.ContainerSize.X) < 0.001) return;
                TrayDesc.BlockContainerDesc.ContainerSize = new Point2D(value, TrayDesc.BlockContainerDesc.ContainerSize.Y);
                this.OnPropertyChanged();
            }
        }

        //Block SizeY
        public double BlockSizeY
        {
            get => TrayDesc.BlockContainerDesc.ContainerSize.Y;
            set
            {
                //if (Math.Abs(value - TrayDesc.BlockContainerDesc.ContainerSize.Y) < 0.001) return;
                TrayDesc.BlockContainerDesc.ContainerSize = new Point2D(TrayDesc.BlockContainerDesc.ContainerSize.X, value);
                this.OnPropertyChanged();
            }
        }

        //Block到Tray的GapX (必須定義在大容器 TrayContainerDesc)
        public double BlockFistGapX
        {
            get => TrayDesc.TrayContainerDesc.FirstGap.X;
            set
            {
                //if (Math.Abs(value - TrayDesc.TrayContainerDesc.FirstGap.X) < 0.001) return;
                TrayDesc.TrayContainerDesc.FirstGap = new Point2D(value, TrayDesc.TrayContainerDesc.FirstGap.Y);
                this.OnPropertyChanged();
            }
        }

        //Block到Tray的GapY (必須定義在大容器 TrayContainerDesc)
        public double BlockFistGapY
        {
            get => TrayDesc.TrayContainerDesc.FirstGap.Y;
            set
            {
                //if (Math.Abs(value - TrayDesc.TrayContainerDesc.FirstGap.Y) < 0.001) return;
                TrayDesc.TrayContainerDesc.FirstGap = new Point2D(TrayDesc.TrayContainerDesc.FirstGap.X, value);
                this.OnPropertyChanged();
            }
        }

        //Block數量X (必須定義在大容器 TrayContainerDesc)
        public int BlockDimNumX
        {
            get => TrayDesc.TrayContainerDesc.SubDimSize.X;
            set
            {
                //if (TrayDesc.TrayContainerDesc.SubDimSize.X == value)return;
                TrayDesc.TrayContainerDesc.SubDimSize = new Point(value, TrayDesc.TrayContainerDesc.SubDimSize.Y);
                this.OnPropertyChanged();
            }
        }

        //Block數量Y (必須定義在大容器 TrayContainerDesc)
        public int BlockDimNumY
        {
            get => TrayDesc.TrayContainerDesc.SubDimSize.Y;
            set
            {
                //if (TrayDesc.TrayContainerDesc.SubDimSize.Y == value)return;
                TrayDesc.TrayContainerDesc.SubDimSize = new Point(TrayDesc.TrayContainerDesc.SubDimSize.X, value);
                this.OnPropertyChanged();
            }
        }


        #endregion Block

        public bool useSingleIC
        {
            get => Desc.bUseSingleIC;
            set
            {
                //if (Desc.bUseSingleIC == value)return;
                Desc.bUseSingleIC = value;
                this.OnPropertyChanged();
            }
        }

        public bool useStripe
        {
            get => Desc.bUseStripe;
            set
            {
                //if (Desc.bUseStripe == value)return;
                Desc.bUseStripe = value;
                this.OnPropertyChanged();
            }
        }

        #region FOV Setting

        public bool useFOVSingleProduct
        {
            get => SelectedFOVMode == FOVMode.Single;
            set
            {
                if (value)
                    SelectedFOVMode = FOVMode.Single;
            }
        }

        public bool useFOVMultiProduct
        {
            get => SelectedFOVMode == FOVMode.Multi;
            set
            {
                if (value)
                    SelectedFOVMode = FOVMode.Multi;
            }
        }

        public bool useFOVMultiProductMAX
        {
            get => SelectedFOVMode == FOVMode.MultiMax;
            set
            {
                if (value)
                    SelectedFOVMode = FOVMode.MultiMax;
            }
        }




        //public bool useFOVSingleProduct
        //{
        //    get => Desc.bUseFOVSingleProduct;
        //    set
        //    {
        //        if (Desc.bUseFOVSingleProduct == value)
        //            return;
        //        Desc.bUseFOVSingleProduct = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        //public bool useFOVMultiProductMAX
        //{
        //    get => Desc.bUseFOVMultiProductMax;
        //    set
        //    {
        //        if (Desc.bUseFOVMultiProductMax == value)
        //            return;
        //        Desc.bUseFOVMultiProductMax = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        //public bool useFOVMultiProduct
        //{
        //    get => Desc.bUseFOVMultieProduct;
        //    set
        //    {
        //        if (Desc.bUseFOVMultiProductMax == value)
        //            return;
        //        Desc.bUseFOVMultiProductMax = value;
        //        this.OnPropertyChanged();
        //    }
        //}


        public int FOVProductXNum
        {
            get => Desc.bFOVProductXNum;
            set
            {
                if (Desc.bFOVProductXNum < 1 )
                {
                    MessageBox.Show("Value Error");
                    return;
                }
                Desc.bFOVProductXNum = value;
                this.OnPropertyChanged();
            }
        }

        public int FOVProductYNum
        {
            get => Desc.bFOVProductYNum;
            set
            {
                if (Desc.bFOVProductYNum < 1)
                {
                    MessageBox.Show("Value Error");
                    return;
                }
                Desc.bFOVProductYNum = value;
                this.OnPropertyChanged();
            }
        }

        #endregion FOV Setting



        #region Product



        #endregion Product





        #region IC
        public double IcSizeX
        {
            get => TrayDesc.IcDesc.ContainerSize.X;
            set
            {
                //if (TrayDesc.IcDesc.ContainerSize.X == value) return;
                TrayDesc.IcDesc.ContainerSize = new Point2D(value, TrayDesc.IcDesc.ContainerSize.Y);
                this.OnPropertyChanged(IcSizeX.ToString());
            }
        }
        public double IcSizeY
        {
            get => TrayDesc.IcDesc.ContainerSize.Y;
            set
            {
                //if (Math.Abs(value - TrayDesc.IcDesc.ContainerSize.Y) < 0.001) return;
                TrayDesc.IcDesc.ContainerSize = new Point2D(TrayDesc.IcDesc.ContainerSize.X, value);
                this.OnPropertyChanged(IcSizeY.ToString());
            }
        }
        #endregion



        public double ProductThinkness
        {
            get => TrayDesc.IcDesc.ContainerThickness;
            set
            {
                //if (Math.Abs(value - TrayDesc.IcDesc.ContainerThickness) < 0.001) return;
                TrayDesc.IcDesc.ContainerThickness = value;
                this.OnPropertyChanged(ProductThinkness.ToString());
            }
        }

        public bool IsFovSingle
        {
            get => Desc.IsFovSingle;
            set
            {
                if (Desc.IsFovSingle == value)
                    return;
                Desc.IsFovSingle = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));           
        }


        public FOVMode SelectedFOVMode
        {
            get
            {
                if (Desc.bUseFOVSingleProduct)
                    return FOVMode.Single;
                else if (Desc.bUseFOVMultieProduct)
                    return FOVMode.Multi;
                else if (Desc.bUseFOVMultiProductMax)
                    return FOVMode.MultiMax;
                else
                    return FOVMode.Single; // 預設值，可依需求更改
            }
            set
            {
                if (SelectedFOVMode == value)
                    return;

                // 互斥設置
                Desc.bUseFOVSingleProduct = (value == FOVMode.Single);
                Desc.bUseFOVMultieProduct = (value == FOVMode.Multi);
                Desc.bUseFOVMultiProductMax = (value == FOVMode.MultiMax);

                // 通知三個屬性與自己改變
                OnPropertyChanged(nameof(SelectedFOVMode));
                OnPropertyChanged(nameof(useFOVSingleProduct));
                OnPropertyChanged(nameof(useFOVMultiProduct));
                OnPropertyChanged(nameof(useFOVMultiProductMAX));
            }
        }

        public enum FOVMode
        {
            Single,
            Multi,
            MultiMax
        }

    }
    
}
