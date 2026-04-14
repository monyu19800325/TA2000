using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA2000Modules;

namespace TA2000Modules
{
    public class SummaryReport
    {
        public string Path { get; set; }
        public string MachineName { get; set; }
        public string LotName { get; set; }
        public string ProductName { get; set; }
        public int TrayCount { get; set; }
        public int TotalCount { get => PassCount+FailCount+InvalidCount; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public int InvalidCount { get; set; }
        public double Yield { get => PassCount/TotalCount; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public StatisticViewModel StatisticViewModel;

        public void GenSummaryReport(InspectionModule module)
        {

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            using (StreamWriter w = new StreamWriter(Path + $"\\SummaryReport_{EndTime.ToString("yyyy_MM_dd_HH_mm_ss")}.txt", false, UTF8Encoding.UTF8))
            {
                w.WriteLine($"Lot Report");
                w.WriteLine($"Machine Name  : {MachineName}");
                w.WriteLine($"StartTime     : {StartTime.ToString("yyyy/MM/dd HH:mm:ss")}" + $"EndTime     : {EndTime.ToString("yyyy/MM/dd HH:mm:ss")}".PadLeft(80 - $"StartTime     : {StartTime.ToString("yyyy/MM/dd HH:mm:ss")}".Length));
                w.WriteLine($"LotName       : {LotName}" + $"ProductName : {ProductName}".PadLeft(80 - $"LotName       : {LotName}".Length - 6));
                w.WriteLine("===================================================");
                w.WriteLine($"TrayCount   :   {TrayCount}");
                w.WriteLine($"TotalCount  :   {TotalCount}");
                w.WriteLine($"PassCount   :   {PassCount}");
                w.WriteLine($"FailCount   :   {FailCount}");
                w.WriteLine($"InvalidCount:   {InvalidCount}");
                w.WriteLine($"Yield       :   {Yield}");
                w.WriteLine("===================================================");
                w.WriteLine("Item".PadLeft(20) + "%Pass".PadLeft(20) + "Pass".PadLeft(20) + "Fail".PadLeft(20));
                w.WriteLine("===================================================");
                for (int i = 1; i < StatisticViewModel.AllCounts.ViewFormDatas.Count; i++)
                {
                    w.WriteLine($"{StatisticViewModel.AllCounts.ViewFormDatas[i].Label}" +
                        $"{((double)StatisticViewModel.AllCounts.ViewFormDatas[i].Pass / (double)TotalCount) * 100}".PadLeft(40 - StatisticViewModel.AllCounts.ViewFormDatas[i].Label.Length) +
                        $"{StatisticViewModel.AllCounts.ViewFormDatas[i].Pass}".PadLeft(20) +
                        $"{StatisticViewModel.AllCounts.ViewFormDatas[i].Fail}".PadLeft(20));
                }
                w.Close();
            }



            #region old
            //using (StreamWriter w = new StreamWriter(Path, false, UTF8Encoding.UTF8))
            //{
            //    w.WriteLine($"Lot Report");
            //    w.WriteLine($"Machine Name  : {MachineName}");
            //    w.WriteLine($"StartTime:{StartTime.ToString("yyyy-MM-dd-HH-mm-ss")} ~ EndTime:{EndTime.ToString("yyyy-MM-dd-HH-mm-ss")}");
            //    w.WriteLine($"LotName:{LotName}");
            //    w.WriteLine($"ProductName:{ProductName}");
            //    w.WriteLine($"TrayCount:{TrayCount}");
            //    w.WriteLine($"TotalCount:{TotalCount}");
            //    w.WriteLine($"PassCount:{PassCount}");
            //    w.WriteLine($"FailCount:{FailCount}");
            //    w.WriteLine($"InvalidCount:{InvalidCount}");
            //    w.WriteLine($"Yield:{Yield}");
            //    w.Close();
            //}
            #endregion
        }
    }
}
