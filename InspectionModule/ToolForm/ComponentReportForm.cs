using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HTA.InspectionFlow;
using Reporter;
using HalconDotNet;
using HTA.Utility.Structure;

namespace TA2000Modules
{
    public partial class ComponentReportForm : Form
    {
        //Lot路徑
        public string LotPath;
        //Spec 項目
        public string SpecItem;
        public string SpecItemCompName;
        int _X_Index = 1;
        int _Y_Index = 1;
        int _Index = 0;
        public string[] CompSpecName;
        public string[] DataValue;
        public string[] CompPassFail;
        //window parameter
        Point2d mPrevImageSize = new Point2d(-1, -1);
        Point2d LeftTop = new Point2d(), RightBot = new Point2d();
        Point ImgSize;
        HImage ErrorImage = new HImage();
        string CurrentCsvPath;
        double Currentscale = 1;
        /// <summary>
        /// 滑鼠當下按的位置
        /// </summary>
        private Point DragPoint;
        /// <summary>
        /// 是否被滑鼠按住
        /// </summary>
        private bool IsDrag = false;
        public int TotalTrayInLot { get; set; } = 1;

        public ComponentReportForm()
        {
            InitializeComponent();
        }

        private void ComponentReportForm_Shown(object sender, EventArgs e)
        {

        }


        public bool ShowComponentReportValue(string newPath, int Y_Index, int X_Index, int Index, string failImagePath, string rowImagePath, string lotPath, string otherClassify = "")
        {
            //先複製UA
            this.Left = 950;
            this.Top = 110;
            //issue UA: Row:Y、Col:X，與LC不同後續排查
            _X_Index = Y_Index;

            _Y_Index = X_Index;

            _Index = Index;


            LotPath = lotPath;
            //讀取CSV內的字串
            var fileContent = string.Empty;
            //存取目標資料
            var data = string.Empty;
            var dataCompName = string.Empty;
            string[] DataArray;
            string[] DataArrayCompName;
            bool GetData = false;
            var filePath = string.Empty;
            //COB_SpectItem.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "d:\\";

            filePath = newPath + "\\" + LotPath + "\\" +
                       TotalTrayInLot.ToString("000") + "\\" +
                       (_X_Index) + "_" + (_Y_Index) + ".csv";
            openFileDialog.FileName = filePath;


            if (File.Exists(openFileDialog.FileName) == false)
            {
                MessageBox.Show("csv不存在!");
                BtnClose_Click(null, null);
                return false;
            }
            var fileStream = openFileDialog.OpenFile();

            using (StreamReader reader = new StreamReader(fileStream))
            {
                while ((fileContent = reader.ReadLine()) != null)
                {
                    if (fileContent.Contains(" - "))
                    {
                        DataArrayCompName = fileContent.Split('-');
                        dataCompName = DataArrayCompName[0];
                    }

                    if (fileContent.Contains("Data Name"))
                    {
                        GetData = true;
                        continue;
                    }
                    if (fileContent.Contains("==================================================="))
                    {
                        GetData = false;
                    }
                    if (GetData)
                    {
                        DataArray = fileContent.Split(',');
                        if (DataArray[4] == "False")
                        {
                            data = dataCompName + "-" + DataArray[0];
                            //COB_SpectItem.Items.Add(data);
                        }
                    }
                }
            }
            CompSpecName = new string[200];
            DataValue = new string[200];
            CompPassFail = new string[200];

            LV_CustomShow.Items.Clear();
            System.IO.StreamReader mysr = new System.IO.StreamReader(filePath);
            Reporter.ComponentReportTool.ReportContent content = Reporter.ComponentReportTool.GetReportContent(filePath);

            this.Text = filePath;
            LV_CustomShow.Items.Clear();
            bool HasFail = false;
            for (int i = 0; i < content.SpecItems.Data.Count; i++)
            {
                HasFail = false;
                var target = content.SpecItems.UserDefineNames.FirstOrDefault(x => x == content.SpecItems.Data[i].UserName);
                if (target != null)
                {
                    var targetIndex = content.SpecItems.UserDefineNames.IndexOf(target);
                    if(false)// (content.SpecItems.FolderNames[targetIndex] == "" || content.SpecItems.FolderNames[targetIndex].StartsWith(otherClassify))
                    {
                        CompSpecName[i] = content.SpecItems.Data[i].UserName + "-" + content.SpecItems.Data[i].SpecName;
                        if (content.SpecItems.Data[i].GetPassFail() == true)
                        {
                            CompPassFail[i + 1] = "Pass";
                        }
                        else
                        {
                            CompPassFail[i + 1] = "Fail";
                        }
                        //CompPassFail[i+1] = content.SpecItems.Data[i].GetPassFail().ToString();
                        //value.SubItems.Add(content.SpecItems.Data[i].);
                        double maxDiffValue = -999999;
                        int maxIndex = 0;
                        for (int j = 0; j < content.SpecItems.Data[i].Value.Length; j++)
                        {
                            if (Double.IsNaN(content.SpecItems.Data[i].Value[j]))
                            {
                                maxIndex = j;
                                DataValue[i + 1] = (-999999).ToString();
                                HasFail = true;
                                break;
                            }
                            if (content.SpecItems.Data[i].Value[j] > content.SpecItems.Data[i].HighBound ||
                                content.SpecItems.Data[i].Value[j] < content.SpecItems.Data[i].LowBound)
                            {
                                if (content.SpecItems.Data[i].Value[j] > content.SpecItems.Data[i].HighBound)
                                {
                                    var diff = content.SpecItems.Data[i].Value[j] - content.SpecItems.Data[i].HighBound;
                                    if (diff > maxDiffValue)
                                    {
                                        maxDiffValue = diff;
                                        maxIndex = j;
                                    }

                                }
                                else if (content.SpecItems.Data[i].Value[j] < content.SpecItems.Data[i].LowBound)
                                {
                                    var diff = content.SpecItems.Data[i].LowBound - content.SpecItems.Data[i].Value[j];
                                    if (diff > maxDiffValue)
                                    {
                                        maxDiffValue = diff;
                                        maxIndex = j;
                                    }
                                }

                                DataValue[i + 1] = content.SpecItems.Data[i].Value[maxIndex].ToString();
                                HasFail = true;
                            }

                        }

                        if (HasFail == false)
                        {
                            DataValue[i + 1] = content.SpecItems.Data[i].Value[0].ToString();
                        }
                    }
                }


            }

            var newDataValue = DataValue.Where(x => x != null).ToList();
            newDataValue.Insert(0, null);
            var newDataValueArr = newDataValue.ToArray();
            var newCompPassFail = CompPassFail.Where(x => x != null).ToList();
            newCompPassFail.Insert(0, null);
            var newCompPassFailArr = newCompPassFail.ToArray();

            ListViewItem value = new ListViewItem(newDataValueArr);
            ListViewItem specPassFail = new ListViewItem(newCompPassFailArr);

            LV_CustomShow.Items.Add(value);
            LV_CustomShow.Items.Add(specPassFail);
            LV_CustomShow.Items[0].UseItemStyleForSubItems = false;

            for (int i = 0; i < content.SpecItems.Data.Count; i++)
            {
                if (CompPassFail[i + 1] == "Fail")
                {
                    LV_CustomShow.Items[0].SubItems[i + 1].BackColor = Color.OrangeRed;
                }
            }

            LV_CustomShow.Scrollable = true;

            LV_CustomShow.Refresh();

            var newCompSpecName = CompSpecName.Where(x => x != null).ToArray();
            CreateGridListName(newCompSpecName);
            if (content.PassFail == HTA.Utility.Common.ProductResultEm.Pass)
                ShowErrorImg(rowImagePath, content);
            else
                ShowErrorImg(failImagePath, content);

            this.Visible = true;
            return true;
        }


        private void ShowErrorImg(string newPath, Reporter.ComponentReportTool.ReportContent content)
        {
            COB_ImageSelect.Items.Clear();
            COB_ImageSelect.SelectedIndex = -1;

            var dirs = Directory.GetDirectories(newPath);

            var targets = dirs.Where(x => x.Contains(LotPath)).ToList();

            string filePath = "";
            bool noImage = true;
            //現在存圖改用LotPath_時間，所以最新的時間會在最下面
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                filePath = targets[i] + "\\" +
                        TotalTrayInLot.ToString("000") + "\\" +
                        (_X_Index) + "_" + (_Y_Index);
                if (Directory.Exists(filePath))
                {
                    CurrentCsvPath = filePath;
                    foreach (string fname in System.IO.Directory.GetFileSystemEntries(filePath))
                    {
                        string[] sArray = fname.Split('\\');
                        string FileName = sArray[sArray.Length - 1];
                        COB_ImageSelect.Items.Add(FileName);
                    }

                    if (COB_ImageSelect.Items.Count > 0)
                    {
                        COB_ImageSelect.SelectedIndex = 0;
                        string ErrorImagePath = filePath + "\\" + COB_ImageSelect.Items[0].ToString();
                        ErrorImage.ReadImage(ErrorImagePath);
                        hWindowControl.SetFullImagePart(ErrorImage);
                        hWindowControl.HalconWindow.DispObj(ErrorImage);
                        int width, height;
                        ErrorImage.GetImageSize(out width, out height);

                        ImgSize.X = width;
                        ImgSize.Y = height;
                    }
                    else
                    {
                        COB_ImageSelect.Text = "";
                        COB_ImageSelect.Refresh();
                        ErrorImage.Dispose();
                        HImage hImage = new HImage("byte", 5120, 5120);
                        ErrorImage = hImage;
                        TabP_Image.Refresh();
                        this.Refresh();
                        hWindowControl.HalconWindow.ClearWindow();
                    }
                    noImage = false;
                    break;
                }
            }

            if (noImage)
            {
                COB_ImageSelect.Text = "";
                COB_ImageSelect.Refresh();
                ErrorImage.Dispose();
                HImage hImage = new HImage("byte", 5120, 5120);
                ErrorImage = hImage;
                TabP_Image.Refresh();
                this.Refresh();
                hWindowControl.HalconWindow.ClearWindow();
            }

        }

        List<double> data = new List<double>();
        private void COB_SpectItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SpecItem = COB_SpectItem.SelectedItem.ToString();
            //string[] tmpSpecItem = SpecItem.Split('-');
            //SpecItem = tmpSpecItem[1];
            //SpecItemCompName = tmpSpecItem[0];
            //data = SearchResultData();

            ////MessageBox.Show(String.Join(", ", data));
            //BtnTest_Click(null, null);
        }


        public string GetReportPath(int ReportIndex)
        {
            //string InputPath = "D:\\MyTest\\20210805\\";
            //string InputPath1 = "";

            //if (ReportIndex < 10)
            //{
            //    InputPath1 = InputPath + "00" + ReportIndex.ToString() + "\\1_1_0.csv";
            //}
            //else if (ReportIndex < 100)
            //{
            //    InputPath1 = InputPath + "0" + ReportIndex.ToString() + "\\1_1_0.csv";
            //}
            //else
            //{
            //    InputPath1 = InputPath + ReportIndex.ToString() + "\\1_1_0.csv";
            //}

            string InputPath1 = LotPath +
                               ReportIndex.ToString("000") + "\\" +
                              (_X_Index) + "_" + (_Y_Index) + ".csv";
            //InputPath1 = "D:\\test.csv";

            return InputPath1;
        }

        public bool ReadReport(string Path, string SelectComp, string SelectSpect, ref List<double> ShowData)
        {
            //讀取CSV內的字串
            var fileContent = string.Empty;
            //存取目標資料
            var data = string.Empty;
            string[] DataArray;
            var CheckCompName = string.Empty;
            bool GetData = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.FileName = Path;
            if (File.Exists(openFileDialog.FileName) == false)
            {
                return false;
            }
            var fileStream = openFileDialog.OpenFile();

            using (StreamReader reader = new StreamReader(fileStream))
            {
                while ((fileContent = reader.ReadLine()) != null)
                {
                    //if (fileContent.Contains(SelectSpect))
                    //{
                    //    DataArray = fileContent.Split(',');
                    //    data = DataArray[5];
                    //    ShowData.Add(Convert.ToDouble(data));
                    //    return true;
                    //}

                    if (fileContent.Contains(" - "))
                    {
                        DataArray = fileContent.Split('-');
                        CheckCompName = DataArray[0];
                    }

                    if (fileContent.Contains("Data Name") && CheckCompName == SelectComp)
                    {
                        GetData = true;
                        continue;
                    }
                    if (fileContent.Contains("==================================================="))
                    {
                        GetData = false;
                    }
                    if (GetData)
                    {
                        DataArray = fileContent.Split(',');
                        if (DataArray[0] == SelectSpect)
                        {
                            //data = DataArray[3];//舊的Componentreport
                            data = DataArray[7];//新的Componentreport
                            ShowData.Add(Convert.ToDouble(data));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool UpdateShowdata(int NewTrayID)
        {
            List<double> NewData = new List<double>();
            string InputPath1 = GetReportPath(NewTrayID);

            //測試先讀取
            ReadReport(InputPath1, SpecItemCompName, SpecItem, ref NewData);

            // NewData= GlobalObj.GlobalObjs.gLot.CurrentContainer().GetTrayInspectionSpecifyResult(0,"GlueInspection", COB_SpectItem.SelectedItem.ToString());

            data.RemoveAt(0);
            data.Add(NewData[0]);
            //MessageBox.Show(String.Join(", ", data));
            Draw();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //UpdateShowdata(Convert.ToInt16(numericUpDown1.Value));
        }

        public void Draw()

        {
            //標題 最大數值
            Series series1 = new Series(SpecItem, 1000);

            //設定線條顏色
            series1.Color = Color.Blue;

            //設定字型
            series1.Font = new System.Drawing.Font("標楷體", 14);

            //折線圖
            series1.ChartType = SeriesChartType.Line;

            //將數值顯示在線上
            series1.IsValueShownAsLabel = false;


            //將數值新增至序列
            for (int index = 0; index < data.Count; index++)
            {
                series1.Points.AddXY(index, data[index]);
            }

            //將序列新增到圖上
            //this.chart1 = new Chart();
            //this.chart1.Series.Clear();
            //this.chart1.Series.Add(series1);
            //this.chart1.ChartAreas[0].AxisY.Minimum = data.Min() - 0.5;
            //this.chart1.ChartAreas[0].AxisY.Maximum = data.Max() + 0.5;
            //this.chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(0);
            //this.chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(NUDDataSize.Value + 1);
            ////標題
            //this.chart1.Titles.Clear();
            //this.chart1.Titles.Add(SpecItemCompName);
            ////this.chart1.Show();
            //this.chart1.Refresh();
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            Draw();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //UpdateShowdata(Convert.ToInt16(numericUpDown1.Value));
            //numericUpDown1.Value = numericUpDown1.Value + 1;
            //BtnTest_Click(null, null);
        }

        private void NUDDataSize_ValueChanged(object sender, EventArgs e)
        {
            //numericUpDown1.Value = NUDDataSize.Value + 1;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btn_Advance_Click(object sender, EventArgs e)
        {
            if (this.Height == 180)
            {
                this.Height = 770;
                BtnAdvance.Text = "Close Data";
            }
            else
            {
                this.Height = 180;
                BtnAdvance.Text = "Show Data";
            }
        }

        private void CreateGridListName(string[] t_CompSpecName)
        {
            this.LV_CustomShow.Columns.Clear();
            this.LV_CustomShow.Columns.Add("", 43, HorizontalAlignment.Center);
            for (int c = 0; c < t_CompSpecName.Count(); c++)
            {
                if (t_CompSpecName[c] != null)
                {
                    this.LV_CustomShow.Columns.Add(t_CompSpecName[c], 85, HorizontalAlignment.Center);
                }
            }

            this.LV_CustomShow.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void hWindowControl_HMouseWheel(object sender, HMouseEventArgs e)
        {

            //var relativePoint = this.PointToClient(System.Windows.Forms.Cursor.Position);

            Point relativePoint = new Point();
            //relativePoint.X = (int)e.X;
            //relativePoint.Y = (int)e.Y;
            relativePoint = hWindowControl.PointToClient(System.Windows.Forms.Cursor.Position);


            if (e.Delta >= 0)
                zoom(relativePoint, -0.1);
            else
                zoom(relativePoint, 0.1);
            hWindowControl.HalconWindow.DispObj(ErrorImage);

        }

        private void hWindowControl_HMouseMove(object sender, HMouseEventArgs e)
        {
            if (IsDrag)
            {
                UpdateInfoBaseOnMouse(e);
                hWindowControl.HalconWindow.DispObj(ErrorImage);
                int[] pts = new int[4];
                hWindowControl.HalconWindow.GetPart(out pts[0], out pts[1], out pts[2], out pts[3]);
                LeftTop.y = pts[0];
                LeftTop.x = pts[1];
                RightBot.y = pts[2];
                RightBot.x = pts[3];
            }
            IsDrag = false;

        }
        private void UpdateInfoBaseOnMouse(HMouseEventArgs e)
        {
            double xDiff = -e.X + DragPoint.X;
            double yDiff = -e.Y + DragPoint.Y;

            int[] pts = new int[4];
            hWindowControl.HalconWindow.GetPart(out pts[0], out pts[1], out pts[2], out pts[3]);
            LeftTop.y = pts[0];
            LeftTop.x = pts[1];
            RightBot.y = pts[2];
            RightBot.x = pts[3];

            double r1 = LeftTop.y;
            double c1 = LeftTop.x;
            double r2 = RightBot.y;
            double c2 = RightBot.x;
            double focusWidth = c2 - c1;
            double focusHeight = r2 - r1;
            double currentScaleX = hWindowControl.Width / focusWidth;
            double currentScaleY = hWindowControl.Height / focusHeight;


            {
                r1 = r1 + yDiff / currentScaleY;
                c1 = c1 + xDiff / currentScaleX;
                if (r1 < 0)
                {
                    r1 = 0;
                }

                if (c1 < 0)
                {
                    c1 = 0;
                }

                r2 = r1 + focusHeight;
                c2 = c1 + focusWidth;
                if (r2 >= ImgSize.Y)
                {
                    r2 = ImgSize.Y - 1;
                    r1 = r2 - focusHeight;
                }
                if (c2 >= ImgSize.X)
                {
                    c2 = ImgSize.X - 1;
                    c1 = c2 - focusWidth;
                }
            }

            hWindowControl.HalconWindow.SetPart((int)r1, (int)c1, (int)r2, (int)c2);
        }

        private void hWindowControl_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void hWindowControl_HMouseUp(object sender, HMouseEventArgs e)
        {
            IsDrag = true;

        }

        private void hWindowControl_HMouseDown(object sender, HMouseEventArgs e)
        {
            DragPoint.X = (int)e.X;
            DragPoint.Y = (int)e.Y;
            IsDrag = false;
        }

        private void hWindowControl_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void hWindowControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void COB_ImageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectIndex = 0;
            if (Directory.Exists(CurrentCsvPath))
            {

                if (COB_ImageSelect.Items.Count > 0)
                {
                    SelectIndex = COB_ImageSelect.SelectedIndex;
                    string ErrorImagePath = CurrentCsvPath + "\\" + COB_ImageSelect.Items[SelectIndex].ToString();
                    ErrorImage.ReadImage(ErrorImagePath);
                    hWindowControl.SetFullImagePart(ErrorImage);
                    hWindowControl.HalconWindow.DispObj(ErrorImage);
                    int width, height;
                    ErrorImage.GetImageSize(out width, out height);

                    ImgSize.X = width;
                    ImgSize.Y = height;
                }
            }
        }
        /// <summary>
        /// 影像縮放功能
        /// </summary>
        /// <param name="center">縮放中心</param>
        /// <param name="factor">縮放比例</param>
        private void zoom(Point center, double factor)
        {
            double width = RightBot.x - LeftTop.x;
            double height = RightBot.y - LeftTop.y;
            double centerX = (RightBot.x + LeftTop.x) / 2;
            double centerY = (RightBot.y + LeftTop.y) / 2;
            double currentScale = width / ImgSize.X;
            double newScaleX = Math.Max(Math.Min(currentScale + factor, 1.0), 0.03);
            double newScaleY = Math.Max(Math.Min(currentScale * 1 + factor, 1.0), 0.03);


            width = ImgSize.X * newScaleX * 0.5;
            height = ImgSize.Y * newScaleY * 0.5;
            LeftTop.x = Math.Max((int)(centerX - width), 0);
            LeftTop.y = Math.Max((int)(centerY - height), 0);
            RightBot.x = Math.Min((int)(centerX + width), ImgSize.X - 1);
            RightBot.y = Math.Min((int)(centerY + height), ImgSize.Y - 1);
            hWindowControl.HalconWindow.SetPart(Math.Max(0, (int)LeftTop.y),
                Math.Max(0, (int)LeftTop.x),
                Math.Min(ImgSize.Y - 1, (int)RightBot.y),
                Math.Min(ImgSize.X - 1, (int)RightBot.x));

        }


    }
}
