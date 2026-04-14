using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HTA.MotionBase.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    [POCOViewModel]
    public class LaserTestViewModel
    {
        private double _cX1Pos;
        public double CX1Pos 
        { 
            get=>_cX1Pos;
            set
            {
                _cX1Pos = value;
                this.RaisePropertyChanged(x => x.CX1Pos);
            }
        }

        double _aY1Pos;
        public double AY1Pos
        {
            get => _aY1Pos;
            set
            {
                _aY1Pos = value;
                this.RaisePropertyChanged(x => x.AY1Pos);
            }
        }

        double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                this.RaisePropertyChanged(x => x.Value);
            }
        }

        string _vel;
        public string Vel
        {
            get => _vel;
            set
            {
                _vel = value;
                this.RaisePropertyChanged(x => x.Vel);
            }
        }
        public List<string> VelList { get; set; } = new List<string>();

        public InspectionModule Module;

        public void Init(InspectionModule module)
        {
            Module = module;
            VelList = new List<string>() { "Very High", "High", "Medium", "Slow", "Very Slow" };
        }

        public void Move()
        {
            //var velAY1 = TATool.SelectVelDef(Module.視覺縱移軸, Module.AY1VelList, Vel);
            //Module.視覺縱移軸.AbsoluteMove(AY1Pos, velAY1);

            //var velCX1 = TATool.SelectVelDef(Module.BX1_流道橫移軸, Module.CX1VelList, Vel);
            //Module.BX1_流道橫移軸.AbsoluteMove(CX1Pos, velCX1);
        }

        public void Measure()
        {
            if(Module.LaserReader is ILaserDistanceFinder laserDistanceFinder)
            {
                var success =  laserDistanceFinder.ReadHeight(2000);
                if (!success)
                {
                    MessageBox.Show("Measure Fail");
                }
                else
                {
                    Value = laserDistanceFinder.Height;
                }
            }
        }

        public void doTest()
        {
            string ipAddress = "192.168.20.2";
            int port = 502;

            try
            {
                using (TcpClient client = new TcpClient(ipAddress, port))
                using (NetworkStream stream = client.GetStream())
                {
                    // 1. 組建發送報文 (Read Holding Registers: Addr 0, Qty 2)
                    byte[] request = {
                    0x00, 0x01,             // Transaction ID
                    0x00, 0x00,             // Protocol ID (Modbus)
                    0x00, 0x06,             // Length (6 bytes to follow)
                    0x01,                   // Unit ID
                    0x03,                   // Function Code (Read)
                    0x00, 0x00,             // Start Address High/Low
                    0x00, 0x02              // Quantity High/Low
                };

                    // 2. 發送請求
                    stream.Write(request, 0, request.Length);

                    // 3. 接收回傳 (標頭 7 bytes + 數據 5 bytes = 12 bytes)
                    byte[] response = new byte[12];
                    int bytesRead = stream.Read(response, 0, response.Length);

                    if (bytesRead >= 12 && response[7] == 0x03) // 檢查功能碼
                    {
                        // Modbus TCP 回傳數據從 index 9 開始 (前 9 bytes 為標頭與長度)
                        // HL-G2 通常是 Big-Endian (高位在前)
                        byte[] rawData = new byte[4];
                        rawData[0] = response[12]; // 低位 (視手冊順序可能需調整)
                        rawData[1] = response[11];
                        rawData[2] = response[10];
                        rawData[3] = response[9];

                        // 將 Byte 陣列轉為 32-bit 整數
                        int value = BitConverter.ToInt32(response.Skip(9).Take(4).Reverse().ToArray(), 0);

                        Console.WriteLine($"原始整數值: {value}");
                        Console.WriteLine($"測量結果: {value / 10000.0} mm");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("連線錯誤: " + ex.Message);
            }
        }

    }
}
