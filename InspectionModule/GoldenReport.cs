using Calibration.Halcon;
using DevExpress.XtraEditors.SyntaxEditor;
using HTA.MainController;
using HTA.TriggerServer.Triggers.ADLink;
//using InspectionModule.ToolForm;
using Newtonsoft.Json.Linq;
using Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TA2100Modules
{
    public partial class GoldenReport
    {
        public string WritePath { get; set; }
        public string mCurrentLot { get; set; }
        public int InspectTimes { get; set; }
        public string DataUnits { get; set; } = "mm";
        public string CmpPath { get; set; }
        public DateTime Time { get; set; }


        public List<double> CmpHeight_1_1, CmpHeight_1_2, CmpHeight_1_3, CmpHeight_1_4, CmpHeight_1_5,
            CmpHeight_2_1, CmpHeight_2_2, CmpHeight_2_3, CmpHeight_2_4, CmpHeight_2_5,
            CmpHeight_3_1, CmpHeight_3_2, CmpHeight_3_3, CmpHeight_3_4, CmpHeight_3_5,
            CmpHeight_4_1, CmpHeight_4_2, CmpHeight_4_3, CmpHeight_4_4, CmpHeight_4_5;

        public List<double> CirclePitch_1_1, CirclePitch_1_2, CirclePitch_1_3, CirclePitch_1_4, CirclePitch_1_5,
            CirclePitch_2_1, CirclePitch_2_2, CirclePitch_2_3, CirclePitch_2_4, CirclePitch_2_5;

        /// <summary>
        /// 一組裡面又有兩顆球要算Diameter
        /// </summary>
        public List<List<double>> CircleDiameter_1_1, CircleDiameter_1_2, CircleDiameter_1_3, CircleDiameter_1_4, CircleDiameter_1_5,
            CircleDiameter_2_1, CircleDiameter_2_2, CircleDiameter_2_3, CircleDiameter_2_4, CircleDiameter_2_5;

        public double pixelSize = 0;  //取得一個pixel實際大小

        public void InitialParam(IMainController MainController)
        {
            pixelSize = MainController.GetHardware().CalibrationLists[0].Pix2MMAvr; //取得pixelSize (double)

            CmpHeight_1_1 = new List<double>();
            CmpHeight_2_1 = new List<double>();
            CmpHeight_3_1 = new List<double>();
            CmpHeight_4_1 = new List<double>();
            CmpHeight_1_2 = new List<double>();
            CmpHeight_2_2 = new List<double>();
            CmpHeight_3_2 = new List<double>();
            CmpHeight_4_2 = new List<double>();
            CmpHeight_1_3 = new List<double>();
            CmpHeight_2_3 = new List<double>();
            CmpHeight_3_3 = new List<double>();
            CmpHeight_4_3 = new List<double>();
            CmpHeight_1_4 = new List<double>();
            CmpHeight_2_4 = new List<double>();
            CmpHeight_3_4 = new List<double>();
            CmpHeight_4_4 = new List<double>();
            CmpHeight_1_5 = new List<double>();
            CmpHeight_2_5 = new List<double>();
            CmpHeight_3_5 = new List<double>();
            CmpHeight_4_5 = new List<double>();

            CirclePitch_1_1 = new List<double>();
            CirclePitch_1_2 = new List<double>();
            CirclePitch_1_3 = new List<double>();
            CirclePitch_1_4 = new List<double>();
            CirclePitch_1_5 = new List<double>();
            CirclePitch_2_1 = new List<double>();
            CirclePitch_2_2 = new List<double>();
            CirclePitch_2_3 = new List<double>();
            CirclePitch_2_4 = new List<double>();
            CirclePitch_2_5 = new List<double>();


            CircleDiameter_1_1 = new List<List<double>>();
            CircleDiameter_1_2 = new List<List<double>>();
            CircleDiameter_1_3 = new List<List<double>>();
            CircleDiameter_1_4 = new List<List<double>>();
            CircleDiameter_1_5 = new List<List<double>>();
            CircleDiameter_2_1 = new List<List<double>>();
            CircleDiameter_2_2 = new List<List<double>>();
            CircleDiameter_2_3 = new List<List<double>>();
            CircleDiameter_2_4 = new List<List<double>>();
            CircleDiameter_2_5 = new List<List<double>>();

            for (int i = 0; i < 2; i++)
            {
                CircleDiameter_1_1.Add(new List<double>());
                CircleDiameter_1_2.Add(new List<double>());
                CircleDiameter_1_3.Add(new List<double>());
                CircleDiameter_1_4.Add(new List<double>());
                CircleDiameter_1_5.Add(new List<double>());
                CircleDiameter_2_1.Add(new List<double>());
                CircleDiameter_2_2.Add(new List<double>());
                CircleDiameter_2_3.Add(new List<double>());
                CircleDiameter_2_4.Add(new List<double>());
                CircleDiameter_2_5.Add(new List<double>());
            }
        }

        #region Circle Pitch

        public void CirclePitchRawData(string fileName)
        {
            if (Directory.Exists(WritePath + "\\" + mCurrentLot) == false)
                Directory.CreateDirectory(WritePath + "\\" + mCurrentLot);
            //InitialParam();
            using (StreamWriter w = new StreamWriter(WritePath + "\\" + mCurrentLot + "\\" + fileName + ".txt"))
            {
                CirclePitchWriteInspectData(w);
                w.Close();
            }
        }

        public void CirclePitchWriteInspectData(StreamWriter w)
        {
            w.WriteLine("---------------------------------Specification Value-------------------------------------------------");
            w.WriteLine("INDEX" +
                            "Circle Pitch Value".PadLeft(20));

            for (int i = 0; i < InspectTimes; i++)
            {
                w.WriteLine($"------------------------------------{i + 1}-------------------------------------------------");
                w.WriteLine("-----------------------------------Measure Data Shift------------------------------------");
                w.WriteLine("INDEX" +
                            "Circle Pitch Value".PadLeft(20));
                w.WriteLine("---------------------------------------------------------------------------------------");
                WriteSingleData_CirclePitch(w, i + 1);
            }
        }

        public void WriteSingleData_CirclePitch(StreamWriter w, int inspectTimes)
        {
            for (int i = 1; i <= 1; i++)
            {
                GoldenSingleCirclePitch(inspectTimes, i, 1, w);
            }
        }

        public void GoldenSingleCirclePitch(int trayIndex, int row, int col, StreamWriter w)
        {
            List<double> values = new List<double>() { -99, -99, -99, -99, -99, -99, -99, -99, -99, -99 };

            AfterSpecData spec = FetchFeatures(trayIndex, row, col);
            int index = 1;
            for (int spekcIndex = 0; spekcIndex < spec.Data.Count; spekcIndex++)
            {
                for (int i = 0; i < spec.Data[spekcIndex].Value.Length; i++)
                {
                    if (spec.Data[spekcIndex].ComponentName == "CircleDistance2" && spec.Data[spekcIndex].SpecName == "Distance" && spec.Data[spekcIndex].UserName == $"CircleDistance{index}")
                    {
                        values[index - 1] = spec.Data[spekcIndex].Value[i];
                        index++;
                    }
                }
            }

            switch (col)
            {
                case 1:
                    switch (row)
                    {
                        case 1:
                            double value = -99;
                            for (int i = 0; i < values.Count; i++)
                            {
                                value = values[i];
                                string valStr = values[i].ToString("0.000");
                                switch (i)
                                {
                                    case 0:
                                        CirclePitch_1_1.Add(value);
                                        w.WriteLine($"R{1}_C{1}" + valStr.PadLeft(20));
                                        break;
                                    case 1:
                                        CirclePitch_1_2.Add(value);
                                        w.WriteLine($"R{1}_C{2}" + valStr.PadLeft(20));
                                        break;
                                    case 2:
                                        CirclePitch_1_3.Add(value);
                                        w.WriteLine($"R{1}_C{3}" + valStr.PadLeft(20));
                                        break;
                                    case 3:
                                        CirclePitch_1_4.Add(value);
                                        w.WriteLine($"R{1}_C{4}" + valStr.PadLeft(20));
                                        break;
                                    case 4:
                                        CirclePitch_1_5.Add(value);
                                        w.WriteLine($"R{1}_C{5}" + valStr.PadLeft(20));
                                        break;
                                    case 5:
                                        CirclePitch_2_1.Add(value);
                                        w.WriteLine($"R{2}_C{1}" + valStr.PadLeft(20));
                                        break;
                                    case 6:
                                        CirclePitch_2_2.Add(value);
                                        w.WriteLine($"R{2}_C{2}" + valStr.PadLeft(20));
                                        break;
                                    case 7:
                                        CirclePitch_2_3.Add(value);
                                        w.WriteLine($"R{2}_C{3}" + valStr.PadLeft(20));
                                        break;
                                    case 8:
                                        CirclePitch_2_4.Add(value);
                                        w.WriteLine($"R{2}_C{4}" + valStr.PadLeft(20));
                                        break;
                                    case 9:
                                        CirclePitch_2_5.Add(value);
                                        w.WriteLine($"R{2}_C{5}" + valStr.PadLeft(20));
                                        break;
                                }

                            }
                            break;
                    }
                    break;
            }

        }

        public void CirclePitchAccuracyReport(string fileName)
        {
            if (Directory.Exists(WritePath + "\\" + mCurrentLot) == false)
                Directory.CreateDirectory(WritePath + "\\" + mCurrentLot);
            var GoldenReportFilePath = WritePath + "\\" + mCurrentLot + "\\" + fileName;
            using (StreamWriter w = new StreamWriter(WritePath + "\\" + mCurrentLot + "\\" + fileName + ".txt"))
            {

                w.WriteLine("Circle Pitch Inspect Golden Target Report");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                w.WriteLine($"Machine NO. : BA3000");
                w.WriteLine("Component : CircleDistance2");
                w.WriteLine($"Inspect Counts :{InspectTimes}");
                w.WriteLine($"Check Time : {Time.ToString("yyyy_MM_dd  HH:mm:ss")}");
                w.WriteLine($"Measure Unit : {DataUnits.ToString()}");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");


                w.WriteLine("CRITERION" +
                           //"Center Shift Mean".PadLeft(20) +
                           "Circle Pitch P - P".PadLeft(20) +
                           "Circle Pitch STD".PadLeft(20) +
                           "SPECIFIED".PadLeft(20) +
                           "STATUS".PadLeft(20));

                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                CalcCirclePitchWrite(w, CirclePitch_1_1, "Standard", 0.000);
                CalcCirclePitchWrite(w, CirclePitch_1_2, "+20", -0.020);
                CalcCirclePitchWrite(w, CirclePitch_1_3, "+40", -0.040);
                CalcCirclePitchWrite(w, CirclePitch_1_4, "+60", -0.060);
                CalcCirclePitchWrite(w, CirclePitch_1_5, "+80", -0.080);
                CalcCirclePitchWrite(w, CirclePitch_2_1, "-20", 0.020);
                CalcCirclePitchWrite(w, CirclePitch_2_2, "-40", 0.040);
                CalcCirclePitchWrite(w, CirclePitch_2_3, "-60", 0.060);
                CalcCirclePitchWrite(w, CirclePitch_2_4, "-80", 0.080);
                CalcCirclePitchWrite(w, CirclePitch_2_5, "-100", 0.100);
                w.Close();
            }
        }

        public void CalcCirclePitchWrite(StreamWriter w, List<double> rawData, string dataName, double diff)
        {
            double stdValueCenter = GetStandardDeviation(rawData);
            double maxSubMinCenter = GetMaxSubMin(rawData.Max(), rawData.Min());
            double specValue = pixelSize;//10um(0.01mm)

            string passFail = (maxSubMinCenter > MM2Unit(specValue)) ? "FAIL" : "PASS";


            w.WriteLine(dataName +
                            //rawDataCenter.Average().ToString("0.000").PadLeft(20) +
                            maxSubMinCenter.ToString("0.000").PadLeft(20) +
                            stdValueCenter.ToString("0.000").PadLeft(20) +
                            ("<" + MM2Unit(specValue).ToString("0.000")).PadLeft(20) +
                            passFail.PadLeft(20));
        }

        #endregion

        #region Circle Diameter

        public void CircleDiameterRawData(string fileName)
        {
            if (Directory.Exists(WritePath + "\\" + mCurrentLot) == false)
                Directory.CreateDirectory(WritePath + "\\" + mCurrentLot);
            //InitialParam();
            using (StreamWriter w = new StreamWriter(WritePath + "\\" + mCurrentLot + "\\" + fileName + ".txt"))
            {
                CircleDiameterWriteInspectData(w);
                w.Close();
            }
        }

        public void CircleDiameterWriteInspectData(StreamWriter w)
        {
            w.WriteLine("---------------------------------Specification Value-------------------------------------------------");
            w.WriteLine("INDEX" +
                            "Circle Diameter Value".PadLeft(20));

            for (int i = 0; i < InspectTimes; i++)
            {
                w.WriteLine($"------------------------------------{i + 1}-------------------------------------------------");
                w.WriteLine("-----------------------------------Measure Data Shift------------------------------------");
                w.WriteLine("INDEX" +
                            "Circle Diameter Value".PadLeft(20));
                w.WriteLine("---------------------------------------------------------------------------------------");
                WriteSingleData_CircleDiameter(w, i + 1);
            }
        }

        public void WriteSingleData_CircleDiameter(StreamWriter w, int inspectTimes)
        {
            for (int i = 1; i <= 1; i++)
            {
                GoldenSingleCircleDiameter(inspectTimes, i, 1, w);
            }
        }

        public void GoldenSingleCircleDiameter(int trayIndex, int row, int col, StreamWriter w)
        {
            List<double> values = new List<double>() { -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99 };

            AfterSpecData spec = FetchFeatures(trayIndex, row, col);
            for (int spekcIndex = 0; spekcIndex < spec.Data.Count; spekcIndex++)
            {
                for (int i = 0; i < spec.Data[spekcIndex].Value.Length; i++)
                {
                    if (spec.Data[spekcIndex].ComponentName == "CircleMeasure2" && spec.Data[spekcIndex].SpecName == "Radius")
                    {
                        values[i] = spec.Data[spekcIndex].Value[i] * 2;
                    }
                }
            }

            switch (col)
            {
                case 1:
                    switch (row)
                    {
                        case 1:
                            double value = -99;
                            for (int i = 0; i < values.Count; i++)
                            {
                                value = values[i];
                                string valStr = values[i].ToString("0.000");
                                switch (i / 2)
                                {
                                    case 0:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_1_1[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_1_1[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 1:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_1_2[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_1_2[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 2:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_1_3[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_1_3[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 3:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_1_4[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_1_4[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 4:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_1_5[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_1_5[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 5:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_2_1[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_2_1[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 6:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_2_2[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_2_2[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 7:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_2_3[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_2_3[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 8:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_2_4[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_2_4[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                    case 9:
                                        if (i % 2 == 0)
                                        {
                                            CircleDiameter_2_5[0].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{1}" + valStr.PadLeft(20));
                                        }
                                        else
                                        {
                                            CircleDiameter_2_5[1].Add(value);
                                            w.WriteLine($"R{1}_C{1}_{2}" + valStr.PadLeft(20));
                                        }
                                        break;
                                }

                            }
                            break;
                    }
                    break;
            }

        }


        public void CircleDiameterAccuracyReport(string fileName)
        {
            if (Directory.Exists(WritePath + "\\" + mCurrentLot) == false)
                Directory.CreateDirectory(WritePath + "\\" + mCurrentLot);
            var GoldenReportFilePath = WritePath + "\\" + mCurrentLot + "\\" + fileName;
            using (StreamWriter w = new StreamWriter(WritePath + "\\" + mCurrentLot + "\\" + fileName + ".txt"))
            {

                w.WriteLine("Circle Diameter Inspect Golden Target Report");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                w.WriteLine($"Machine NO. : BA3000");
                w.WriteLine("Component : CircleMeasure2");
                w.WriteLine($"Inspect Counts :{InspectTimes}");
                w.WriteLine($"Check Time : {Time.ToString("yyyy_MM_dd  HH:mm:ss")}");
                w.WriteLine($"Measure Unit : {DataUnits.ToString()}");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");


                w.WriteLine("CRITERION" +
                           //"Center Shift Mean".PadLeft(20) +
                           "Circle Diameter P - P".PadLeft(20) +
                           "Circle Diameter STD".PadLeft(20) +
                           "SPECIFIED".PadLeft(20) +
                           "STATUS".PadLeft(20));

                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                CalcCircleDiameterWrite(w, CircleDiameter_1_1[0], "Standard_1", 0.000);
                CalcCircleDiameterWrite(w, CircleDiameter_1_1[1], "Standard_2", 0.000);
                CalcCircleDiameterWrite(w, CircleDiameter_1_2[0], "+20_1", -0.020);
                CalcCircleDiameterWrite(w, CircleDiameter_1_2[1], "+20_2", -0.020);
                CalcCircleDiameterWrite(w, CircleDiameter_1_3[0], "+40_1", -0.040);
                CalcCircleDiameterWrite(w, CircleDiameter_1_3[1], "+40_2", -0.040);
                CalcCircleDiameterWrite(w, CircleDiameter_1_4[0], "+60_1", -0.060);
                CalcCircleDiameterWrite(w, CircleDiameter_1_4[1], "+60_2", -0.060);
                CalcCircleDiameterWrite(w, CircleDiameter_1_5[0], "+80_1", -0.080);
                CalcCircleDiameterWrite(w, CircleDiameter_1_5[1], "+80_2", -0.080);
                CalcCircleDiameterWrite(w, CircleDiameter_2_1[0], "-20_1", 0.020);
                CalcCircleDiameterWrite(w, CircleDiameter_2_1[1], "-20_2", 0.020);
                CalcCircleDiameterWrite(w, CircleDiameter_2_2[0], "-40_1", 0.040);
                CalcCircleDiameterWrite(w, CircleDiameter_2_2[1], "-40_2", 0.040);
                CalcCircleDiameterWrite(w, CircleDiameter_2_3[0], "-60_1", 0.060);
                CalcCircleDiameterWrite(w, CircleDiameter_2_3[1], "-60_2", 0.060);
                CalcCircleDiameterWrite(w, CircleDiameter_2_4[0], "-80_1", 0.080);
                CalcCircleDiameterWrite(w, CircleDiameter_2_4[1], "-80_2", 0.080);
                CalcCircleDiameterWrite(w, CircleDiameter_2_5[0], "-100_1", 0.100);
                CalcCircleDiameterWrite(w, CircleDiameter_2_5[1], "-100_2", 0.100);
                w.Close();
            }
        }

        public void CalcCircleDiameterWrite(StreamWriter w, List<double> rawData, string dataName, double diff)
        {
            double stdValueCenter = GetStandardDeviation(rawData);
            double maxSubMinCenter = GetMaxSubMin(rawData.Max(), rawData.Min());
            double specValue = pixelSize;//10um(0.01mm)

            string passFail = (maxSubMinCenter > MM2Unit(specValue)) ? "FAIL" : "PASS";


            w.WriteLine(dataName +
                            //rawDataCenter.Average().ToString("0.000").PadLeft(20) +
                            maxSubMinCenter.ToString("0.000").PadLeft(20) +
                            stdValueCenter.ToString("0.000").PadLeft(20) +
                            ("<" + MM2Unit(specValue).ToString("0.000")).PadLeft(20) +
                            passFail.PadLeft(20));
        }

        #endregion

        #region Component Height
        public void ComponentHeightRawData(string fileName)
        {
            if (Directory.Exists(WritePath + "\\" + mCurrentLot) == false)
                Directory.CreateDirectory(WritePath + "\\" + mCurrentLot);
            //InitialParam();
            using (StreamWriter w = new StreamWriter(WritePath + "\\" + mCurrentLot + "\\" + fileName + ".txt"))
            {
                ComponentHeightWriteInspectData(w);
                w.Close();
            }
        }

        public void ComponentHeightWriteInspectData(StreamWriter w)
        {
            w.WriteLine("---------------------------------Specification Value-------------------------------------------------");
            w.WriteLine("INDEX" +
                            "Component Height Value".PadLeft(20));

            for (int i = 0; i < InspectTimes; i++)
            {
                w.WriteLine($"------------------------------------{i + 1}-------------------------------------------------");
                w.WriteLine("-----------------------------------Measure Data Shift------------------------------------");
                w.WriteLine("INDEX" +
                            "Component Height Value".PadLeft(20));
                w.WriteLine("---------------------------------------------------------------------------------------");
                WriteSingleData_ComponentHeight(w, i + 1);
            }
        }

        public void WriteSingleData_ComponentHeight(StreamWriter w, int inspectTimes)
        {
            //只有一個row,一個col，然後有5顆x4顆
            for (int i = 1; i <= 1; i++)
            {
                GoldenSingleComponentHeight(inspectTimes, i, 1, w);
            }
        }

        public void GoldenSingleComponentHeight(int trayIndex, int row, int col, StreamWriter w)
        {
            List<double> values = new List<double>() { -99,-99,-99,-99,-99,-99,-99,-99,-99,
            -99,-99,-99,-99,-99,-99,-99,-99,-99,-99,-99};
            AfterSpecData spec = FetchFeatures(trayIndex, row, col);
            for (int spekcIndex = 0; spekcIndex < spec.Data.Count; spekcIndex++)
            {
                if (spec.Data[spekcIndex].ComponentName == "StructureLight_Component" && spec.Data[spekcIndex].SpecName == "ComponentHeight")
                {
                    for (int i = 0; i < spec.Data[spekcIndex].Value.Length; i++)
                    {
                        values[i] = spec.Data[spekcIndex].Value[i];
                    }
                }
            }

            for (int i = 0; i < values.Count; i++)
            {
                string valStr = values[i].ToString("0.000");

                switch (i)
                {
                    case 0:
                        CmpHeight_1_1.Add(values[i]);
                        w.WriteLine($"300um_1" +
                            valStr.PadLeft(20));
                        break;
                    case 1:
                        CmpHeight_2_1.Add(values[i]);
                        w.WriteLine($"300um_2" +
                            valStr.PadLeft(20));
                        break;
                    case 2:
                        CmpHeight_3_1.Add(values[i]);
                        w.WriteLine($"300um_3" +
                            valStr.PadLeft(20));
                        break;
                    case 3:
                        CmpHeight_4_1.Add(values[i]);
                        w.WriteLine($"300um_4" +
                            valStr.PadLeft(20));
                        break;
                    case 4:
                        CmpHeight_1_2.Add(values[i]);
                        w.WriteLine($"400um_1" +
                            valStr.PadLeft(20));
                        break;
                    case 5:
                        CmpHeight_2_2.Add(values[i]);
                        w.WriteLine($"400um_2" +
                            valStr.PadLeft(20));
                        break;
                    case 6:
                        CmpHeight_3_2.Add(values[i]);
                        w.WriteLine($"400um_3" +
                            valStr.PadLeft(20));
                        break;
                    case 7:
                        CmpHeight_4_2.Add(values[i]);
                        w.WriteLine($"400um_4" +
                            valStr.PadLeft(20));
                        break;
                    case 8:
                        CmpHeight_1_3.Add(values[i]);
                        w.WriteLine($"500um_1" +
                            valStr.PadLeft(20));
                        break;
                    case 9:
                        CmpHeight_2_3.Add(values[i]);
                        w.WriteLine($"500um_2" +
                            valStr.PadLeft(20));
                        break;
                    case 10:
                        CmpHeight_3_3.Add(values[i]);
                        w.WriteLine($"500um_3" +
                            valStr.PadLeft(20));
                        break;
                    case 11:
                        CmpHeight_4_3.Add(values[i]);
                        w.WriteLine($"500um_4" +
                            valStr.PadLeft(20));
                        break;
                    case 12:
                        CmpHeight_1_4.Add(values[i]);
                        w.WriteLine($"600um_1" +
                            valStr.PadLeft(20));
                        break;
                    case 13:
                        CmpHeight_2_4.Add(values[i]);
                        w.WriteLine($"600um_2" +
                            valStr.PadLeft(20));
                        break;
                    case 14:
                        CmpHeight_3_4.Add(values[i]);
                        w.WriteLine($"600um_3" +
                            valStr.PadLeft(20));
                        break;
                    case 15:
                        CmpHeight_4_4.Add(values[i]);
                        w.WriteLine($"600um_4" +
                            valStr.PadLeft(20));
                        break;
                    case 16:
                        CmpHeight_1_5.Add(values[i]);
                        w.WriteLine($"700um_1" +
                            valStr.PadLeft(20));
                        break;
                    case 17:
                        CmpHeight_2_5.Add(values[i]);
                        w.WriteLine($"700um_2" +
                            valStr.PadLeft(20));
                        break;
                    case 18:
                        CmpHeight_3_5.Add(values[i]);
                        w.WriteLine($"700um_3" +
                            valStr.PadLeft(20));
                        break;
                    case 19:
                        CmpHeight_4_5.Add(values[i]);
                        w.WriteLine($"700um_4" +
                            valStr.PadLeft(20));
                        break;
                }
            }
        }

        public void ComponentHeightAccuracyReport(string fileName)
        {
            if (Directory.Exists(WritePath + "\\" + mCurrentLot) == false)
                Directory.CreateDirectory(WritePath + "\\" + mCurrentLot);
            var GoldenReportFilePath = WritePath + "\\" + mCurrentLot + "\\" + fileName;
            using (StreamWriter w = new StreamWriter(WritePath + "\\" + mCurrentLot + "\\" + fileName + ".txt"))
            {

                w.WriteLine("Component Height Inspect Golden Target Report");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                w.WriteLine($"Machine NO. : BA3000");
                w.WriteLine("Component : StructureLight_Component");
                w.WriteLine($"Inspect Counts :{InspectTimes}");
                w.WriteLine($"Check Time : {Time.ToString("yyyy_MM_dd  HH:mm:ss")}");
                w.WriteLine($"Measure Unit : {DataUnits.ToString()}");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");


                w.WriteLine("CRITERION" +
                           //"Center Shift Mean".PadLeft(20) +
                           "Component Height P - P".PadLeft(20) +
                           "Component Height STD".PadLeft(20) +
                           "SPECIFIED".PadLeft(20) +
                           "STATUS".PadLeft(20));

                w.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                CalcComponentHeightWrite(w, CmpHeight_1_1, "300um_1", 0.300);
                CalcComponentHeightWrite(w, CmpHeight_2_1, "300um_2", 0.300);
                CalcComponentHeightWrite(w, CmpHeight_3_1, "300um_3", 0.300);
                CalcComponentHeightWrite(w, CmpHeight_4_1, "300um_4", 0.300);
                CalcComponentHeightWrite(w, CmpHeight_1_2, "400um_1", 0.400);
                CalcComponentHeightWrite(w, CmpHeight_2_2, "400um_2", 0.400);
                CalcComponentHeightWrite(w, CmpHeight_3_2, "400um_3", 0.400);
                CalcComponentHeightWrite(w, CmpHeight_4_2, "400um_4", 0.400);
                CalcComponentHeightWrite(w, CmpHeight_1_3, "500um_1", 0.500);
                CalcComponentHeightWrite(w, CmpHeight_2_3, "500um_2", 0.500);
                CalcComponentHeightWrite(w, CmpHeight_3_3, "500um_3", 0.500);
                CalcComponentHeightWrite(w, CmpHeight_4_3, "500um_4", 0.500);
                CalcComponentHeightWrite(w, CmpHeight_1_4, "600um_1", 0.600);
                CalcComponentHeightWrite(w, CmpHeight_2_4, "600um_2", 0.600);
                CalcComponentHeightWrite(w, CmpHeight_3_4, "600um_3", 0.600);
                CalcComponentHeightWrite(w, CmpHeight_4_4, "600um_4", 0.600);
                CalcComponentHeightWrite(w, CmpHeight_1_5, "700um_1", 0.700);
                CalcComponentHeightWrite(w, CmpHeight_2_5, "700um_2", 0.700);
                CalcComponentHeightWrite(w, CmpHeight_3_5, "700um_3", 0.700);
                CalcComponentHeightWrite(w, CmpHeight_4_5, "700um_4", 0.700);
                w.Close();
            }
        }

        public void CalcComponentHeightWrite(StreamWriter w, List<double> rawData, string dataName, double diff)
        {
            double stdValueCenter = GetStandardDeviation(rawData);
            double maxSubMinCenter = GetMaxSubMin(rawData.Max(), rawData.Min());
            double specValue = pixelSize;//這台10um(0.01mm)

            string passFail = (maxSubMinCenter > MM2Unit(specValue)) ? "FAIL" : "PASS";


            w.WriteLine(dataName +
                            //rawDataCenter.Average().ToString("0.000").PadLeft(20) +
                            maxSubMinCenter.ToString("0.000").PadLeft(20) +
                            stdValueCenter.ToString("0.000").PadLeft(20) +
                            ("<" + MM2Unit(specValue).ToString("0.000")).PadLeft(20) +
                            passFail.PadLeft(20));
        }


        public void CompoentHeightCalcData()
        {
            for (int i = 0; i < InspectTimes; i++)
            {
                for (int j = 1; j <= 1; j++)
                {
                    GoldenSingleCalcCompoentHeight(i + 1, j, 1);
                }
            }
        }

        public void GoldenSingleCalcCompoentHeight(int trayIndex, int row, int col)
        {
            List<double> values = new List<double>() { -99,-99,-99,-99,-99,-99,-99,-99,-99,
            -99,-99,-99,-99,-99,-99,-99,-99,-99,-99,-99};
            AfterSpecData spec = FetchFeatures(trayIndex, row, col);
            for (int spekcIndex = 0; spekcIndex < spec.Data.Count; spekcIndex++)
            {
                if (spec.Data[spekcIndex].ComponentName == "StructureLight_Component" && spec.Data[spekcIndex].SpecName == "ComponentHeight")
                {
                    for (int i = 0; i < spec.Data[spekcIndex].Value.Length; i++)
                    {
                        values[i] = spec.Data[spekcIndex].Value[i];
                    }
                }
            }

            for (int i = 0; i < values.Count; i++)
            {
                string valStr = values[i].ToString("0.000");

                switch (i)
                {
                    case 0:
                        CmpHeight_1_1.Add(values[i]);
                        break;
                    case 1:
                        CmpHeight_2_1.Add(values[i]);
                        break;
                    case 2:
                        CmpHeight_3_1.Add(values[i]);
                        break;
                    case 3:
                        CmpHeight_4_1.Add(values[i]);
                        break;
                    case 4:
                        CmpHeight_1_2.Add(values[i]);
                        break;
                    case 5:
                        CmpHeight_2_2.Add(values[i]);
                        break;
                    case 6:
                        CmpHeight_3_2.Add(values[i]);
                        break;
                    case 7:
                        CmpHeight_4_2.Add(values[i]);
                        break;
                    case 8:
                        CmpHeight_1_3.Add(values[i]);
                        break;
                    case 9:
                        CmpHeight_2_3.Add(values[i]);
                        break;
                    case 10:
                        CmpHeight_3_3.Add(values[i]);
                        break;
                    case 11:
                        CmpHeight_4_3.Add(values[i]);
                        break;
                    case 12:
                        CmpHeight_1_4.Add(values[i]);
                        break;
                    case 13:
                        CmpHeight_2_4.Add(values[i]);
                        break;
                    case 14:
                        CmpHeight_3_4.Add(values[i]);
                        break;
                    case 15:
                        CmpHeight_4_4.Add(values[i]);
                        break;
                    case 16:
                        CmpHeight_1_5.Add(values[i]);
                        break;
                    case 17:
                        CmpHeight_2_5.Add(values[i]);
                        break;
                    case 18:
                        CmpHeight_3_5.Add(values[i]);
                        break;
                    case 19:
                        CmpHeight_4_5.Add(values[i]);
                        break;
                }
            }
        }

        #endregion

        #region function
        private AfterSpecData FetchFeatures(int InspecTimes, int row, int col)
        {
            string CheckPath = $@"{CmpPath}\{mCurrentLot}\{InspecTimes.ToString("000")}\{row}_{col}.csv";
            //AddLog("Golden Report", $"CheckPath : {CheckPath}");
            ComponentReportTool.ReportContent content = ComponentReportTool.GetReportContent(CheckPath);
            AfterSpecData specs = content.SpecItems;

            return specs;
        }

        /// <summary>
        /// 計算輸入數據之標準差
        /// </summary>
        /// <param name="doubleList">輸入數據</param>
        /// <returns></returns>
        private double GetStandardDeviation(List<double> doubleList)
        {
            double average = doubleList.Sum() / doubleList.Count;
            double sumOfDerivation = 0;
            foreach (double value in doubleList)
            {
                sumOfDerivation += (value) * (value);
            }
            double sumOfDerivationAverage = sumOfDerivation / doubleList.Count;
            double vaule = Math.Abs(sumOfDerivationAverage - (average * average));

            return vaule == 0 ? 0 : Math.Sqrt(vaule);
        }

        /// <summary>
        /// 計算輸入數據相減最大誤差
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        private double GetMaxSubMin(double max, double min)
        {
            return Math.Abs(max - min);
        }



        protected double MM2Unit(double value)
        {
            double result = value;
            switch (DataUnits)
            {
                case "MM":
                    break;
                case "UM":
                    result = value / 0.001;
                    break;
            }
            return result;
        }

        #endregion


        #region html Circle Pitch


        public void CirclePitchCalcData()
        {
            for (int i = 0; i < InspectTimes; i++)
            {
                for (int j = 1; j <= 1; j++)
                {
                    GoldenSingleCalcCirclePitch(i + 1, j, 1);
                }
            }
        }

        public void GoldenSingleCalcCirclePitch(int trayIndex, int row, int col)
        {
            List<double> values = new List<double>() { -99, -99, -99, -99, -99, -99, -99, -99, -99, -99 };
            AfterSpecData spec = FetchFeatures(trayIndex, row, col);
            int index = 1;
            for (int spekcIndex = 0; spekcIndex < spec.Data.Count; spekcIndex++)
            {
                for (int i = 0; i < spec.Data[spekcIndex].Value.Length; i++)
                {
                    if (spec.Data[spekcIndex].ComponentName != "CircleDistance2")
                    {
                        break;
                    }

                    if (spec.Data[spekcIndex].ComponentName == "CircleDistance2" && spec.Data[spekcIndex].SpecName == "Distance")
                    {
                        values[index - 1] = spec.Data[spekcIndex].Value[i];
                        index++;
                    }

                }
            }

            switch (col)
            {
                case 1:
                    switch (row)
                    {
                        case 1:
                            double value = -99;
                            for (int i = 0; i < values.Count; i++)
                            {
                                value = values[i];
                                string valStr = values[i].ToString("0.000");
                                switch (i)
                                {
                                    case 0:

                                        CirclePitch_1_1.Add(value);

                                        break;
                                    case 1:

                                        CirclePitch_1_2.Add(value);

                                        break;
                                    case 2:

                                        CirclePitch_1_3.Add(value);

                                        break;
                                    case 3:

                                        CirclePitch_1_4.Add(value);

                                        break;
                                    case 4:

                                        CirclePitch_1_5.Add(value);

                                        break;
                                    case 5:

                                        CirclePitch_2_1.Add(value);

                                        break;
                                    case 6:

                                        CirclePitch_2_2.Add(value);

                                        break;
                                    case 7:

                                        CirclePitch_2_3.Add(value);

                                        break;
                                    case 8:

                                        CirclePitch_2_4.Add(value);

                                        break;
                                    case 9:

                                        CirclePitch_2_5.Add(value);
                                        break;
                                }
                            }
                            break;
                    }
                    break;
            }
        }


        private string BuildCirclePitchRawHtml()
        {
            // Left / Right 的資料來源
            List<double>[] src = new List<double>[] { CirclePitch_1_1, CirclePitch_1_2, CirclePitch_1_3, CirclePitch_1_4, CirclePitch_1_5,
                               CirclePitch_2_1, CirclePitch_2_2, CirclePitch_2_3, CirclePitch_2_4, CirclePitch_2_5 };

            string[] keys = new string[]
            {
        "Standard(R1_C1)","+20(R1_C2)","+40(R1_C3)","+60(R1_C4)","+80(R1_C5)",
        "-20(R2_C1)","-40(R2_C2)","-60(R2_C3)","-80(R2_C4)","-100(R2_C5)"
            };

            var sb = new StringBuilder();

            for (int t = 0; t < InspectTimes; t++)
            {
                sb.AppendLine("<details open>");
                sb.AppendLine("  <summary>Run #" + (t + 1) + "</summary>");
                sb.AppendLine("  <table class='table'>");
                sb.AppendLine("    <thead><tr><th>Index</th><th class='num'>Value</th></tr></thead>");
                sb.AppendLine("    <tbody>");

                for (int k = 0; k < keys.Length; k++)
                {
                    string valueText = "-";
                    if (src[k] != null && src[k].Count > t)
                        valueText = HtmlUtil.F3(src[k][t]);

                    sb.AppendLine("      <tr><td>" + HtmlUtil.H(keys[k]) + "</td><td class='num'>" + valueText + "</td></tr>");
                }

                sb.AppendLine("    </tbody>");
                sb.AppendLine("  </table>");
                sb.AppendLine("</details>");
            }

            return sb.ToString();
        }


        private string CalcCirclePitchRowHtml(List<double> rawData, string dataName)
        {
            // 避免 rawData 空的時候 Max/Min 會爆
            if (rawData == null || rawData.Count == 0)
            {
                return "<tr><td>" + HtmlUtil.H(dataName) + "</td><td class='num'>-</td><td class='num'>-</td><td class='num'>-</td><td class='num'>-</td></tr>";
            }

            double stdValue = GetStandardDeviation(rawData);
            double p2p = GetMaxSubMin(rawData.Max(), rawData.Min());

            double specValue = 0.0087;
            double limit = MM2Unit(specValue);

            bool fail = p2p > limit;
            string status = fail ? "<span class='badge-ng'>FAIL</span>" : "<span class='badge-ok'>PASS</span>";

            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<td>" + HtmlUtil.H(dataName) + "</td>");
            sb.Append("<td class='num'>" + HtmlUtil.F3(p2p) + "</td>");
            sb.Append("<td class='num'>" + HtmlUtil.F3(stdValue) + "</td>");
            sb.Append("<td class='num'>&lt;" + HtmlUtil.F3(limit) + "</td>");
            sb.Append("<td class='num'>" + status + "</td>");
            sb.Append("</tr>");

            return sb.ToString();
        }

        #endregion

        #region html Circle Diameter

        public void CircleDiameterCalcData()
        {
            for (int i = 0; i < InspectTimes; i++)
            {
                for (int j = 1; j <= 1; j++)
                {
                    GoldenSingleCalcCircleDiameter(i + 1, j, 1);
                }
            }
        }

        public void GoldenSingleCalcCircleDiameter(int trayIndex, int row, int col)
        {
            List<double> values = new List<double>() { -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99, -99 };

            AfterSpecData spec = FetchFeatures(trayIndex, row, col);
            for (int spekcIndex = 0; spekcIndex < spec.Data.Count; spekcIndex++)
            {
                for (int i = 0; i < spec.Data[spekcIndex].Value.Length; i++)
                {
                    if (spec.Data[spekcIndex].ComponentName == "CircleMeasure2" && spec.Data[spekcIndex].SpecName == "Radius")
                    {
                        values[i] = spec.Data[spekcIndex].Value[i] * 2;
                    }
                }
            }

            switch (col)
            {
                case 1:
                    switch (row)
                    {
                        case 1:
                            double value = -99;
                            for (int i = 0; i < values.Count; i++)
                            {
                                value = values[i];
                                string valStr = values[i].ToString("0.000");
                                switch (i / 2)
                                {
                                    case 0:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_1_1[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_1_1[1].Add(value);

                                        }

                                        break;
                                    case 1:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_1_2[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_1_2[1].Add(value);

                                        }

                                        break;
                                    case 2:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_1_3[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_1_3[1].Add(value);

                                        }

                                        break;
                                    case 3:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_1_4[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_1_4[1].Add(value);

                                        }

                                        break;
                                    case 4:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_1_5[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_1_5[1].Add(value);

                                        }

                                        break;
                                    case 5:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_2_1[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_2_1[1].Add(value);

                                        }

                                        break;
                                    case 6:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_2_2[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_2_2[1].Add(value);

                                        }

                                        break;
                                    case 7:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_2_3[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_2_3[1].Add(value);

                                        }
                                        break;
                                    case 8:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_2_4[0].Add(value);

                                        }
                                        else
                                        {

                                            CircleDiameter_2_4[1].Add(value);

                                        }

                                        break;
                                    case 9:
                                        if (i % 2 == 0)
                                        {

                                            CircleDiameter_2_5[0].Add(value);
                                        }
                                        else
                                        {

                                            CircleDiameter_2_5[1].Add(value);
                                        }
                                        break;
                                }

                            }
                            break;
                    }
                    break;
            }

        }

        private string BuildCircleDiameterRawHtml()
        {
            // 10 個位置，每個位置 2 顆球 => 20 個序列
            // order: R1_C1_1, R1_C1_2, R1_C2_1, R1_C2_2, ... R2_C5_2
            List<List<double>>[] src = new List<List<double>>[]
                {
            CircleDiameter_1_1, CircleDiameter_1_2, CircleDiameter_1_3, CircleDiameter_1_4, CircleDiameter_1_5,
            CircleDiameter_2_1, CircleDiameter_2_2, CircleDiameter_2_3, CircleDiameter_2_4, CircleDiameter_2_5
                };

            string[] baseKeys = new string[]
            {
        "Standard(R1_C1)","+20(R1_C2)","+40(R1_C3)","+60(R1_C4)","+80(R1_C5)",
        "-20(R2_C1)","-40(R2_C2)","-60(R2_C3)","-80(R2_C4)","-100(R2_C5)"
            };

            var sb = new StringBuilder();

            for (int t = 0; t < InspectTimes; t++)
            {
                sb.AppendLine("<details open>");
                sb.AppendLine("  <summary>Run #" + (t + 1) + "</summary>");
                sb.AppendLine("  <table class='table'>");
                sb.AppendLine("    <thead><tr><th>Index</th><th class='num'>Ball</th><th class='num'>Value</th></tr></thead>");
                sb.AppendLine("    <tbody>");

                for (int pos = 0; pos < baseKeys.Length; pos++)
                {
                    // 每個位置 src[pos] 裡面應該有兩個 list: [0] ball1, [1] ball2
                    for (int ball = 0; ball < 2; ball++)
                    {
                        string valueText = "-";
                        if (src[pos] != null && src[pos].Count > ball && src[pos][ball] != null && src[pos][ball].Count > t)
                            valueText = HtmlUtil.F3(src[pos][ball][t]);

                        sb.AppendLine("      <tr><td>" + HtmlUtil.H(baseKeys[pos]) + "</td><td class='num'>" + (ball + 1) + "</td><td class='num'>" + valueText + "</td></tr>");
                    }
                }

                sb.AppendLine("    </tbody>");
                sb.AppendLine("  </table>");
                sb.AppendLine("</details>");
            }

            return sb.ToString();
        }



        private string CalcCircleDiameterRowHtml(List<double> rawData, string dataName, string ballNum)
        {
            if (rawData == null || rawData.Count == 0)
                return "<tr><td>" + HtmlUtil.H(dataName) + "</td><td class='num'>-</td><td class='num'>-</td><td class='num'>-</td><td class='num'>-</td></tr>";

            double stdValue = GetStandardDeviation(rawData);
            double p2p = GetMaxSubMin(rawData.Max(), rawData.Min());

            // 你原本寫死 0.0087，我保留一致（可改成 SpecValue）
            double specValue = 0.0087;
            double limit = MM2Unit(specValue);

            bool fail = p2p > limit;
            string status = fail ? "<span class='badge-ng'>FAIL</span>" : "<span class='badge-ok'>PASS</span>";

            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<td>" + HtmlUtil.H(dataName) + "</td>");
            sb.Append("<td>" + HtmlUtil.H(ballNum) + "</td>");
            sb.Append("<td class='num'>" + HtmlUtil.F3(p2p) + "</td>");
            sb.Append("<td class='num'>" + HtmlUtil.F3(stdValue) + "</td>");
            sb.Append("<td class='num'>&lt;" + HtmlUtil.F3(limit) + "</td>");
            sb.Append("<td class='num'>" + status + "</td>");
            sb.Append("</tr>");
            return sb.ToString();
        }


        #endregion


        #region html Export

        public void ExportAllInOneHtml(string fileName)
        {
            string lotDir = Path.Combine(WritePath, mCurrentLot);
            if (!Directory.Exists(lotDir))
                Directory.CreateDirectory(lotDir);



            string outPath = Path.Combine(lotDir, fileName + ".html");

            var sb = new StringBuilder(256 * 1024);

            sb.AppendLine("<!doctype html>");
            sb.AppendLine("<html lang='zh-Hant'>");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset='utf-8'>");
            sb.AppendLine("  <meta name='viewport' content='width=device-width,initial-scale=1'>");
            sb.AppendLine("  <title>Circle Golden Report</title>");
            sb.AppendLine("  <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>");
            sb.AppendLine(HtmlUtil.Css());
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='container'>");

            // Header
            sb.AppendLine("<div class='top'>");
            sb.AppendLine("  <div>");
            sb.AppendLine("    <h1>Golden Report</h1>");
            sb.AppendLine("    <div class='sub'>Lot: " + HtmlUtil.H(mCurrentLot)
                + " | InspectTimes: " + InspectTimes
                + " | Time: " + Time.ToString("yyyy-MM-dd HH:mm:ss")
                + " | Unit: " + HtmlUtil.H(DataUnits)
                + "</div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            // Tabs
            sb.AppendLine("<div class='tabs'>");

            sb.AppendLine("  <div class='tab-buttons'>");
            sb.AppendLine("    <button type='button' class='tab-btn active' onclick=\"openTab('tabCharts', this)\">Charts</button>");
            sb.AppendLine("    <button type='button' class='tab-btn' onclick=\"openTab('tabPitchRaw', this)\">Pitch RawData</button>");
            sb.AppendLine("    <button type='button' class='tab-btn' onclick=\"openTab('tabPitchAccuracy', this)\">Pitch Accuracy</button>");
            sb.AppendLine("    <button type='button' class='tab-btn' onclick=\"openTab('tabDiameterRaw', this)\">Diameter RawData</button>");
            sb.AppendLine("    <button type='button' class='tab-btn' onclick=\"openTab('tabDiameterAccuracy', this)\">Diameter Accuracy</button>");
            sb.AppendLine("  </div>");

            //抓資料
            CirclePitchCalcData();
            CircleDiameterCalcData();

            // Charts
            sb.AppendLine("  <div id='tabCharts' class='tab-panel active'>");
            sb.AppendLine(BuildSectionCard(
                "Analysis Charts",
                "Pitch / Diameter 的平均值與 P-P 分析圖",
                BuildPitchChartsHtml() + BuildDiameterChartsHtml()
            ));
            sb.AppendLine("  </div>");

            // Pitch Raw
            sb.AppendLine("  <div id='tabPitchRaw' class='tab-panel'>");
            sb.AppendLine(BuildSectionCard(
                "Circle Pitch Raw Data",
                "每次 Inspect 的 Circle Pitch 原始量測值",
                BuildCirclePitchRawSectionHtml()
            ));
            sb.AppendLine("  </div>");

            // Pitch Accuracy
            sb.AppendLine("  <div id='tabPitchAccuracy' class='tab-panel'>");
            sb.AppendLine(BuildSectionCard(
                "Circle Pitch Accuracy Report",
                "Circle Pitch P-P / STD / SPEC / STATUS",
                BuildCirclePitchAccuracySectionHtml()
            ));
            sb.AppendLine("  </div>");

            // Diameter Raw
            sb.AppendLine("  <div id='tabDiameterRaw' class='tab-panel'>");
            sb.AppendLine(BuildSectionCard(
                "Circle Diameter Raw Data",
                "每次 Inspect 的 Circle Diameter 原始量測值（每位置兩顆球）",
                BuildCircleDiameterRawSectionHtml()
            ));
            sb.AppendLine("  </div>");

            // Diameter Accuracy
            sb.AppendLine("  <div id='tabDiameterAccuracy' class='tab-panel'>");
            sb.AppendLine(BuildSectionCard(
                "Circle Diameter Accuracy Report",
                "Circle Diameter P-P / STD / SPEC / STATUS",
                BuildCircleDiameterAccuracySectionHtml()
            ));
            sb.AppendLine("  </div>");

            sb.AppendLine("</div>"); // tabs

            sb.AppendLine(BuildTabScript());

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(outPath, sb.ToString(), Encoding.UTF8);
        }


        public void Export3DComponentHtml(string fileName)
        {
            string lotDir = Path.Combine(WritePath, mCurrentLot);
            if (!Directory.Exists(lotDir))
                Directory.CreateDirectory(lotDir);

            string outPath = Path.Combine(lotDir, fileName + ".html");

            var sb = new StringBuilder(256 * 1024);

            sb.AppendLine("<!doctype html>");
            sb.AppendLine("<html lang='zh-Hant'>");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset='utf-8'>");
            sb.AppendLine("  <meta name='viewport' content='width=device-width,initial-scale=1'>");
            sb.AppendLine("  <title>Golden Report</title>");
            sb.AppendLine("  <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>");
            sb.AppendLine(HtmlUtil.Css());
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='container'>");

            // Header
            sb.AppendLine("<div class='top'>");
            sb.AppendLine("  <div>");
            sb.AppendLine("    <h1>Component Height Golden Report</h1>");
            sb.AppendLine("    <div class='sub'>Lot: " + HtmlUtil.H(mCurrentLot)
                + " | InspectTimes: " + InspectTimes
                + " | Time: " + Time.ToString("yyyy-MM-dd HH:mm:ss")
                + " | Unit: " + HtmlUtil.H(DataUnits)
                + "</div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            // Tabs
            sb.AppendLine("<div class='tabs'>");

            sb.AppendLine("  <div class='tab-buttons'>");
            sb.AppendLine("    <button type='button' class='tab-btn active' onclick=\"openTab('tabCharts', this)\">Charts</button>");
            sb.AppendLine("    <button type='button' class='tab-btn' onclick=\"openTab('tabComponentRaw', this)\">Pitch RawData</button>");
            sb.AppendLine("    <button type='button' class='tab-btn' onclick=\"openTab('tabComponentAccuracy', this)\">Pitch Accuracy</button>");
            sb.AppendLine("  </div>");

            //抓資料
            CompoentHeightCalcData();

            // Charts
            sb.AppendLine("  <div id='tabCharts' class='tab-panel active'>");
            sb.AppendLine(BuildSectionCard(
                "Analysis Charts",
                "Component Height 的平均值與 P-P 分析圖",
                BuildComponentChartsHtml()
            ));
            sb.AppendLine("  </div>");

            // Component Raw
            sb.AppendLine("  <div id='tabComponentRaw' class='tab-panel'>");
            sb.AppendLine(BuildSectionCard(
                "Component Raw Data",
                "每次 Inspect 的 Component Height 原始量測值",
                BuildComponentRawSectionHtml()
            ));
            sb.AppendLine("  </div>");

            // Component Accuracy
            sb.AppendLine("  <div id='tabComponentAccuracy' class='tab-panel'>");
            sb.AppendLine(BuildSectionCard(
                "Component Height Accuracy Report",
                "Component Height P-P / STD / SPEC / STATUS",
                BuildComponentAccuracySectionHtml()
            ));
            sb.AppendLine("  </div>");

            sb.AppendLine("</div>"); // tabs

            sb.AppendLine(BuildTabScript());

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(outPath, sb.ToString(), Encoding.UTF8);
        }


        #endregion

    }
}
