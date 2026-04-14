using HTA.Utility;
using Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HTA.Utility.Common;

namespace TA2000Modules
{
    public class TrayReport
    {

        public Action<string, string> OnLog;
        //第幾個盤、lot、儲存路徑、Row、Col、FOV、barcodeID
        public void GenTrayReport(string currentCmpPath,string mCurrentLot,int mTrayNo,BoatCarrier boatCarrier,int fovCounts,string mTrayID,string writePath,List<string> cmpList)
        {
            OnLog?.Invoke("InspectionModule", "GenTrayReport start");
            try
            {
                //mTrayPassFail = "Pass";
                List<List<string>> Title_LC = new List<List<string>>();

                int rows = boatCarrier.Tray.BlockContainerDesc.SubDimSize.Y;
                int cols = boatCarrier.Tray.BlockContainerDesc.SubDimSize.X;

                //AddLog?.Invoke("Tray Report", $"Row : {rows}、 Col : {cols}、 Fov : {fovCounts}");

                string folderName = writePath + $"\\{mCurrentLot}\\";//
                HTATool.FileSystem.TryCreateFolder(folderName);

                string WriteTarget = folderName + $"Tray_{mTrayNo}-Report.csv";
                //AddLog?.Invoke("Tray Report", $"Tray Report save path : {WriteTarget}");

                StreamWriter w = new StreamWriter(WriteTarget, false, UTF8Encoding.UTF8);

                //寫欄位
                ComponentReportTool.ReportContent Inspection;
                int lcDataCount = 0;
                
                w.Write($"Tray No,Tray ID,Row,Col,IC-PassFail,");

                string inspectCmpPath = $"{currentCmpPath}\\{mCurrentLot}\\{mTrayNo.ToString("000")}\\{1}_{1}.csv";//

                if (File.Exists(inspectCmpPath))
                {
                    Inspection = FetchSingleData(inspectCmpPath);
                    lcDataCount = Inspection.SpecItems.Data.Count;
                    for (int i = 0; i < lcDataCount; i++)
                    {
                        w.Write($"{Inspection.SpecItems.Data[i].UserName}-{Inspection.SpecItems.Data[i].ComponentName}-{Inspection.SpecItems.Data[i].SpecName}," +
                            $"{Inspection.SpecItems.Data[i].ComponentName}-{Inspection.SpecItems.Data[i].SpecName}-SPEC," +
                            $"{Inspection.SpecItems.Data[i].SpecName}-PassFail,");
                        List<string> TitleTemp = new List<string>();
                        TitleTemp.Add(Inspection.SpecItems.Data[i].UserName);
                        TitleTemp.Add(Inspection.SpecItems.Data[i].SpecName);

                        Title_LC.Add(TitleTemp);
                    }

                    //AddLog?.Invoke("Tray Report", $"LCStation Component SpecItems Count : {lcDataCount}");

                }
                else
                {
                    //AddLog?.Invoke("Tray Report", $"LCStation Component Report not find");
                }
                w.WriteLine();

                string lastPath = $"{currentCmpPath}\\{mCurrentLot}\\{mTrayNo.ToString("000")}\\{rows}_{cols}.csv";
                string lastPath2 = $"{currentCmpPath}\\{mCurrentLot}\\{mTrayNo.ToString("000")}\\{rows}_{1}.csv";
                SpinWait.SpinUntil(() => false, 5000);//怕有上一次的資料，讀寫衝到，所以還是等5秒看看
                SpinWait.SpinUntil(() => File.Exists(lastPath) && File.Exists(lastPath2), 10000);
                int r_real = 0;
                int c_real = 0;
                int f_real = fovCounts;
                for (int r = 1; r <= rows; r++)
                {
                    for (int c = 1; c <= cols; c++)
                    {
                        for (int f = 0; f < fovCounts; f++)
                        {
                            string lcTar = $"{currentCmpPath}\\{mCurrentLot}\\{mTrayNo.ToString("000")}\\{r}_{c}.csv";//


                            string temp;

                            if (File.Exists(lcTar))
                            {
                                //AddLog?.Invoke("Tray Report", $"wirte component report");

                                Inspection = FetchSingleData(lcTar);
                                temp = $"{mTrayNo.ToString("000")},{mTrayID},{r},{c},{Inspection.PassFail},";

                                for (int i = 0; i < Title_LC.Count; i++)
                                {
                                    temp += SearchTitle(i, Inspection.SpecItems.Data, Title_LC);
                                }

                                w.WriteLine(temp);
                            }
                        }
                    }
                }
                w.Close();

                string WriteTargetNewName = folderName + $"Tray_{mTrayNo}-{mTrayID}-Report.csv";

                if (File.Exists(WriteTargetNewName))
                {
                    File.Delete(WriteTargetNewName);
                }
                System.IO.File.Move(WriteTarget, WriteTargetNewName);
                //AddLog?.Invoke("Tray Report", $"Save Path:{WriteTargetNewName}");
            }
            catch (Exception e)
            {
                OnLog?.Invoke("InspectionModule", $"TrayReport Exp:{e.Message}");
            }
            OnLog?.Invoke("InspectionModule", "GenTrayReport end");
        }

        public ComponentReportTool.ReportContent FetchSingleData(string fileTarget)
        {
            bool inUse = true;

            ComponentReportTool.ReportContent content = null;

            while (inUse)
            {
                try
                {
                    content = ComponentReportTool.GetReportContent(fileTarget);
                    AfterSpecData specs = content.SpecItems;
                    inUse = false;
                }
                catch
                {
                    SpinWait.SpinUntil(() => false, 500);
                    inUse = true;
                }
            }

            return content;
        }

        public string SearchTitle(int index, List<SingleAfterSpec> data, List<List<string>> Title)
        {
            string result = "";
            //UserName==使用者元件名稱 LC-BASE_Rect
            //SpecName==檢測欄位  Width

            var search = data.Find(x => x.UserName == Title[index][0] && x.SpecName == Title[index][1]);

            if (search != null)
            {
                if (search.SpecName.Contains("Defect"))
                {
                    result = $"{search.Value[0]},1,{PVIPassFail(search.Value[0])},";
                }
                else
                {
                    //若為chipping crack 則顯示Max_...
                    result = $"{search.Value[0]},{search.LowBound}~{search.HighBound},{ValueSpce(search.HighBound, search.LowBound, search.Value[0])},";
                }
            }
            else
            {
                result = "None,None,Invalid,";
            }


            return result;
        }

        private string PVIPassFail(double v)
        {
            string result = "";
            if (v == 1.0)
            {
                result = "Pass";
            }
            else
            {
                result = "Fail";
            }

            return result;
        }

        private string ValueSpce(double highBound, double lowBound, double v)
        {
            string result = "";

            if ((v <= highBound) && (v >= lowBound))
            {
                result = "Pass";
            }
            else
            {
                result = "Fail";
            }

            return result;
        }
    }
}
