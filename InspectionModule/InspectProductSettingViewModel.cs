using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class InspectProductSettingViewModel
    {
        public InspectionModule Module { get; set; } = new InspectionModule();
        public DataGridView Pick_dataGrid;
        BoatCarrier Carrier = new BoatCarrier();
        public List<string> VelList { get; set; } = new List<string>();
        private Dictionary<int, List<int>> RowInfo = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> ColInfo = new Dictionary<int, List<int>>();

        public bool IsPick 
        { 
            get=>Module.ProductParam.IsPick; 
            set
            {
                Module.ProductParam.IsPick = value;
                this.RaisePropertyChanged(x => x.IsPick);
            }
        }

        public bool IsReinspect
        {
            get => Module.ProductParam.IsReinspect;
            set
            {
                Module.ProductParam.IsReinspect = value;
                if (Module.ProductParam.IsReinspect)
                {
                    ReinspectVisible = true;
                }
                else
                {
                    ReinspectVisible = false;
                }
                this.RaisePropertyChanged(x => x.IsReinspect);
            }
        }
              
        public bool IsFly
        {
            get => Module.ProductParam.IsSetFly;
            set
            {
                Module.ProductParam.IsSetFly = value;
                this.RaisePropertyChanged(x => x.IsFly);
            }
        }

        public bool UseBoatBarcodeReader
        {
            get => Module.ProductParam.UseBoatBarcodeReader;
            set
            {
                Module.ProductParam.UseBoatBarcodeReader = value;
                this.RaisePropertyChanged(x => x.UseBoatBarcodeReader);
            }
        }

        public bool UseLaserMeasure
        {
            get => Module.ProductParam.UseLaserMeasure;
            set
            {
                Module.ProductParam.UseLaserMeasure = value;
                this.RaisePropertyChanged(x => x.UseLaserMeasure);
            }
        }

        public bool InspectByPass
        {
            get => Module.ProductParam.InspectByPass;
            set
            {
                Module.ProductParam.InspectByPass = value;
                this.RaisePropertyChanged(x => x.InspectByPass);
            }
        }

        public string Vel
        {
            get => Module.ProductParam.InspectVel.ToString();
            set
            {
                Module.ProductParam.InspectVel = (MoveVelEm)Enum.Parse(typeof(MoveVelEm), value);
                this.RaisePropertyChanged(x => x.Vel);
            }
        }

        public string FlyVel
        {
            get => Module.ProductParam.FlyVel.ToString();
            set
            {
                Module.ProductParam.FlyVel = (MoveVelEm)Enum.Parse(typeof(MoveVelEm), value);
                this.RaisePropertyChanged(x => x.FlyVel);
            }
        }

        public double FlyVelPercent
        {
            get => Module.ProductParam.FlyPercent;
            set
            {
                Module.ProductParam.FlyPercent = value;
                this.RaisePropertyChanged(x => x.FlyVelPercent);
            }
        }

        public int ReinspectCount
        {
            get => Module.ProductParam.ReinspectCount;
            set
            {
                Module.ProductParam.ReinspectCount = (int)value;
                this.RaisePropertyChanged(x => x.ReinspectCount);
            }
        }

        public bool _reinspectVisible;
        public bool ReinspectVisible
        {
            get
            {
                if (Module.ProductParam.IsReinspect)
                {
                    _reinspectVisible = true;
                }
                else
                {
                    _reinspectVisible = false;
                }
                return _reinspectVisible;
            }
            set
            {
                _reinspectVisible = value;
                if(ReinspectCount == 0 && _reinspectVisible)
                {
                    ReinspectCount = 1;
                }
                this.RaisePropertyChanged(x => x.ReinspectVisible);
            }
        }

        public int FailAlarmCount
        {
            get => Module.ProductParam.FailAlarmCount;
            set
            {
                Module.ProductParam.FailAlarmCount = (int)value;
                this.RaisePropertyChanged(x => x.FailAlarmCount);
            }
        }

        public int LotRejectAlarmCount
        {
            get => Module.ProductParam.LotRejectAlarmCount;
            set
            {
                Module.ProductParam.LotRejectAlarmCount = (int)value;
                this.RaisePropertyChanged(x => x.LotRejectAlarmCount);
            }
        }


        public InspectProductSettingViewModel() 
        {
            
        }

        public void Init(InspectionModule module, DataGridView dataGridView)
        {
            Module = module;
            Pick_dataGrid = dataGridView;
            VelList.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(MoveVelEm)).Count(); i++)
            {
                VelList.Add(((MoveVelEm)i).ToString());
            }

            Carrier.IsPreUse = true;
            Module.PreGetTray(Carrier, "", true);
            var totalX = Carrier.Tray.BlockContainerDesc.SubDimSize.X;
            var totalY = Carrier.Tray.BlockContainerDesc.SubDimSize.Y;
            SetupResultGridview(totalY, totalX, true);
            BuildDictionary();
            DataGridViewCellStyle redStyle = new DataGridViewCellStyle();
            redStyle.BackColor = Color.Red;

            //if(Pick_dataGrid.Rows.Count > 0 && Module.ProductParam.PickPos.Count > 0)
            //{
            //    if (Pick_dataGrid.Rows.Count != Module.ProductParam.PickPos[0].x || Pick_dataGrid.Rows[0].Cells.Count != Module.ProductParam.PickPos[0].y)
            //    {
            //        return;
            //    }
            //}
            
            for (int i = 0; i < Module.ProductParam.PickPos.Count; i++)
            {
                if(Pick_dataGrid.Rows.Count <= (int)Module.ProductParam.PickPos[i].x)
                {
                    continue;
                }
                if(Pick_dataGrid.Rows[(int)Module.ProductParam.PickPos[i].x].Cells.Count <= totalX - (int)Module.ProductParam.PickPos[i].y - 1)
                {
                    continue;
                }
                Pick_dataGrid.Rows[(int)Module.ProductParam.PickPos[i].x].Cells[totalX - (int)Module.ProductParam.PickPos[i].y - 1].Style = redStyle;
            }
            //TODO 目前第一次進來，會點擊第一格，但是被點擊的如果是紅色的，顏色會不顯示，目前有的bug
        }

        public void SetupResultGridview(int rows, int cols, bool reset)
        {
            //產品之row col與畫面GridView 定義相反

            if (reset)
            {
                Pick_dataGrid.DataSource = null;
                Pick_dataGrid.ColumnCount = 0;
            }

            Pick_dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Pick_dataGrid.ColumnCount = cols;
            Pick_dataGrid.RowCount = rows;
            Pick_dataGrid.DefaultCellStyle.SelectionBackColor = Color.White;
            Pick_dataGrid.DefaultCellStyle.SelectionForeColor = Color.Black;
            Pick_dataGrid.ColumnHeadersVisible = true;
            Pick_dataGrid.RowHeadersVisible = true;
            Pick_dataGrid.ClearSelection();
            Pick_dataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Pick_dataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Pick_dataGrid.ReadOnly = true;

            for (int i = 0; i < rows; i++)
            {
                Pick_dataGrid.Rows[i].Resizable = DataGridViewTriState.False;

                for (int j = 0; j < cols; j++)
                {
                    Pick_dataGrid.Columns[j].HeaderText = (cols - j).ToString();
                    Pick_dataGrid.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Pick_dataGrid.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                Pick_dataGrid.Rows[i].HeaderCell.Value = String.Format("{0}", i + 1);
            }
        }

        //自己計算坐標系
        private void BuildDictionary()
        {
            ColInfo.Clear();
            RowInfo.Clear();
            int xStep = 0;
            int yStep = 0;
            int colNum, rowNum, blockColNum, blockRowNum;
            double pitchX, pitchY;

                pitchX = Carrier.Tray.BlockContainerDesc.Pitch.X;
                pitchY = Carrier.Tray.BlockContainerDesc.Pitch.Y;
                colNum = Carrier.Tray.BlockContainerDesc.SubDimSize.X;
                rowNum = Carrier.Tray.BlockContainerDesc.SubDimSize.Y;
                blockColNum = 1;
                blockRowNum = 1;


            var lcFovSize = 60;

            int pitchXCount = 0;
            int pitchYCount = 0;

            if (pitchX != 0)
                pitchXCount = Convert.ToInt16(Math.Floor((lcFovSize - Carrier.Tray.BlockContainerDesc.ContainerSize.X) /
                                                pitchX));
            if (pitchY != 0)
                pitchYCount = Convert.ToInt16(Math.Floor((lcFovSize - Carrier.Tray.BlockContainerDesc.ContainerSize.Y) /
                                             pitchY));


            if (Carrier.InspectData.InspectionPostion.IsFovSingle == true)
            {
                pitchXCount = 0;
                pitchYCount = 0;
            }

            // 一個 FOV多少產品
            int xStepCount = pitchXCount + 1;
            int yStepCount = pitchYCount + 1;

            int xMaxStepCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(colNum) /
                                                          Convert.ToDouble(xStepCount))); //每排最多Step
            int yMaxStepCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(rowNum) /
                                                           Convert.ToDouble(yStepCount)));
            for (int i = 0; i < yMaxStepCount * blockRowNum; i++)//blockcolNum
            {
                List<int> rows = new List<int>();
                if ((i + 1) % yMaxStepCount == 0)
                {
                    int notFullY = rowNum % yStepCount;
                    if (notFullY == 0)
                    {
                        for (int j = 0; j < yStepCount; j++)
                        {
                            rows.Add(yStep);
                            yStep++;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < notFullY; j++)
                        {
                            rows.Add(yStep);
                            yStep++;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < yStepCount; j++)
                    {
                        rows.Add(yStep);
                        yStep++;
                    }
                }

                RowInfo.Add(i, rows);
            }

            for (int i = 0; i < xMaxStepCount * blockColNum; i++)
            {
                List<int> cols = new List<int>();
                if ((i + 1) % xMaxStepCount == 0)
                {
                    int notFullX = colNum % xStepCount;
                    if (notFullX == 0)
                    {
                        for (int j = 0; j < xStepCount; j++)
                        {
                            cols.Add(xStep);
                            xStep++;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < notFullX; j++)
                        {
                            cols.Add(xStep);
                            xStep++;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < xStepCount; j++)
                    {
                        cols.Add(xStep);
                        xStep++;
                    }
                }

                ColInfo.Add(i, cols);
            }
        }

        public void Pick_dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            Pick_dataGrid.ClearSelection();
            DataGridViewCellStyle redStyle = new DataGridViewCellStyle();
            redStyle.BackColor = Color.Red;

            DataGridViewCellStyle whiteStyle = new DataGridViewCellStyle();
            whiteStyle.BackColor = Color.White;

            double LC_FOV_SIZE = 60;


            bool isStitchImg = Math.Max(Carrier.Tray.Blocks[0, 0].Ic[0, 0].ContainerSize.X, Carrier.Tray.Blocks[0, 0].Ic[0, 0].ContainerSize.Y) > LC_FOV_SIZE;

            if (isStitchImg)
            {
                if (Pick_dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.Red)
                {
                    Pick_dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = whiteStyle;
                }
                else
                {
                    Pick_dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = redStyle;
                }
            }
            else
            {
                int colKey = -1;
                int rowKey = -1;
                int tColNum;
                tColNum = Carrier.Tray.BlockContainerDesc.SubDimSize.X;


                //RowKey = eRow;
                //ColKey = eCol;
                foreach (var rowItem in RowInfo)
                {
                    if (rowItem.Value.Contains(e.RowIndex))//
                    {
                        rowKey = rowItem.Key;
                    }
                }

                foreach (var colItem in ColInfo)
                {
                    if (colItem.Value.Contains(tColNum - 1 - e.ColumnIndex))//
                    {
                        colKey = colItem.Key;
                    }
                }

                
                for (int i = 0; i < RowInfo[rowKey].Count; i++)
                {
                    for (int j = 0; j < ColInfo[colKey].Count; j++)
                    {

                        if (RowInfo[rowKey][i] > Pick_dataGrid.Rows.Count - 1)
                        {
                            continue;
                        }

                        if (Pick_dataGrid.Rows[RowInfo[rowKey][i]].Cells[tColNum - 1 - ColInfo[colKey][j]].Style.BackColor == Color.Red)
                        {
                            Pick_dataGrid.Rows[RowInfo[rowKey][i]].Cells[tColNum - 1 - ColInfo[colKey][j]].Style = whiteStyle;
                        }
                        else
                        {
                            Pick_dataGrid.Rows[RowInfo[rowKey][i]].Cells[tColNum - 1 - ColInfo[colKey][j]].Style = redStyle;
                        }

                    }
                }
            }

            Module.ProductParam.PickPos.Clear();
            var totalY = Carrier.Tray.BlockContainerDesc.SubDimSize.Y;
            var totalX = Carrier.Tray.BlockContainerDesc.SubDimSize.X;
            for (int i = 0; i < totalY; i++)
            {
                for (int j = 0; j < totalX; j++)
                {
                    if(Pick_dataGrid.Rows[i].Cells[j].Style.BackColor == Color.Red)
                    {
                        Module.ProductParam.PickPos.Add(new HTA.Utility.Structure.Point2d(i, totalX-j-1));//真實的row col
                    }
                }
            }
        }

        private void PaintPickIndex()
        {
            Pick_dataGrid.ClearSelection();
            DataGridViewCellStyle redStyle = new DataGridViewCellStyle();
            redStyle.BackColor = Color.Red;
            DataGridViewCellStyle whiteStyle = new DataGridViewCellStyle();
            whiteStyle.BackColor = Color.White;
            List<int> pickRows = new List<int>();
            List<int> pickCols = new List<int>();
            for (int i = 0; i < Module.ProductParam.PickPos.Count; i++)
            {
                pickRows.Add((int)Module.ProductParam.PickPos[i].y);
                pickCols.Add((int)Module.ProductParam.PickPos[i].y);
            }

            var lcFovSize = 60;

            bool isStitchImg = Math.Max(Carrier.Tray.BlockContainerDesc.ContainerSize.X, Carrier.Tray.BlockContainerDesc.ContainerSize.Y) > lcFovSize;
            if (isStitchImg)
            {
                for (int i = 0; i < pickRows.Count; i++)
                {
                    Pick_dataGrid.Rows[pickRows[i]].Cells[(int)Carrier.Tray.BlockContainerDesc.ContainerSize.X - 1 - pickCols[i]].Style = redStyle;
                }
            }
            else
            {
                int coltran;

                coltran = (int)Carrier.Tray.BlockContainerDesc.ContainerSize.X - 1;


                for (int a = 0; a < pickRows.Count; a++)
                {
                    int rowKey = pickRows[a];
                    int colKey = pickCols[a];


                    for (int i = 0; i < RowInfo[rowKey].Count; i++)
                    {
                        for (int j = 0; j < ColInfo[colKey].Count; j++)
                        {
                            if (RowInfo[rowKey][i] > Pick_dataGrid.Rows.Count - 1)
                            {
                                continue;
                            }


                            if (Pick_dataGrid.Rows[RowInfo[rowKey][i]].Cells[coltran - ColInfo[colKey][j]].Style.BackColor == Color.Red)
                            {
                                Pick_dataGrid.Rows[RowInfo[rowKey][i]].Cells[coltran - ColInfo[colKey][j]].Style = whiteStyle;
                            }
                            else
                            {
                                Pick_dataGrid.Rows[RowInfo[rowKey][i]].Cells[coltran - ColInfo[colKey][j]].Style = redStyle;
                            }
                        }
                    }
                }
            }
        }
    }
}
