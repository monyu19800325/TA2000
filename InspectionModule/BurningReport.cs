using ControlFlow.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TA2000Modules
{
    public class BurningReport
    {
        public static BurningData Data = new BurningData();

        public static void GenBurningReport(string fileName,string mCurrentLot)
        {
            if (Directory.Exists("D:\\Burning" + "\\" + mCurrentLot) == false)
                Directory.CreateDirectory("D:\\Burning" + "\\" + mCurrentLot);

            

            using (StreamWriter w = new StreamWriter("D:\\Burning" + "\\" + mCurrentLot + "\\" + fileName))
            {

                Data.duration = Data.EndTime - Data.StartTime;
                double totalHours = Data.duration.TotalHours;

                w.WriteLine("---------------------------------------------------------------------------------------");
                w.WriteLine("Vision AutoRunning Report");
                w.WriteLine("---------------------------------------------------------------------------------------");
                w.WriteLine($"Test Product   : TA1000 ---{Data.ProductName}");
                w.WriteLine($"StartTime      : {Data.StartTime.ToString()}");
                w.WriteLine($"EndTime      : {Data.EndTime.ToString()}");

                if (Data.TestResult)
                {
                    w.WriteLine($"Test Result       : PASS");
                }
                else
                {
                    w.WriteLine($"Test Result       : Fail");
                }


                w.WriteLine("---------------------------------------------------------------------------------------");
                w.WriteLine(" Cycle Report");
                w.WriteLine("---------------------------------------------------------------------------------------");

                w.WriteLine($"Total Test Cycle   : {Data.TestCycle.ToString()}");
                w.WriteLine($"Total Fail Cycle   : {Data.FailCycle.ToString()}");
                w.WriteLine($"Cycle Fail Rate   : {Data.FailCycleRate.ToString()}");
                w.WriteLine($"Averange Cycle Time   : {Data.Average_Cycle_Time.ToString()}");
                //w.WriteLine($"UPH    : {Data.UPH}");
                w.WriteLine($"UPH    : {Data.Expect_Product_Count/ totalHours}");

                w.WriteLine("---------------------------------------------------------------------------------------");
                w.WriteLine(" Product Report");
                w.WriteLine("---------------------------------------------------------------------------------------");

                w.WriteLine($"Expect Product Count   : {Data.Expect_Product_Count.ToString()}");
                w.WriteLine($"Total Test Product   : {Data.Total_TestProduct.ToString()}");
                w.WriteLine($"Trigger Lose Rate   : {Data.Trigger_Lose_Rate.ToString()}");

                w.WriteLine("---------------------------------------------------------------------------------------");
                w.WriteLine(" Trigger Report");
                w.WriteLine("---------------------------------------------------------------------------------------");

                w.WriteLine($"Total Send Trigger   : {Data.Send_Trigger.ToString()}");
                w.WriteLine($"Cam 0 Trigger Count   : {Data.Cam0Event.ToString()}");


                w.Close();

            }

        }
    }

    public class BurningData
    {

        //                    Vision AutoRunning Report
        //LotName
        public string LotName { get; set; } = "";
        //紀錄時間
        public DateTime StartTime { get; set; } = new DateTime();

        public DateTime EndTime { get; set; } = new DateTime();
        //燒機結果
        public bool TestResult { get; set; } = false;

        //                   Cycle Report



        public TimeSpan duration { get; set; }


        public int TestCycle { get; set; } = 0;

        public int FailCycle { get; set; } = 0;

        public double FailCycleRate { get; set; } = 0.0;

        public double Average_Cycle_Time { get; set; } = 0.0;

        public double UPH { get; set; } = 0.0;

        public int Expect_Product_Count { get; set; } = 0;

        public int Total_TestProduct { get; set; } = 0;

        public double Trigger_Lose_Rate { get; set; } = 0.0;

        public int Send_Trigger { get; set; } = 0;

        public int Cam0Event { get; set; } = 0;
        public int Cam1Event { get; set; } = 0;

        public string ProductName { get; set; } = "";
    }
}
