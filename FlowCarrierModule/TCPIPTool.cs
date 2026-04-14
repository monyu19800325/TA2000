using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using System.Xml;
using HTA.Com.TCPIP2;
using HTAMachine.Machine;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using static HTAMachine.Machine.HtaMachineController;
using System.Runtime.CompilerServices;
using System.IO;
using DevExpress.Office.Crypto;

namespace TA2000Modules
{
    public static class TCPIPTool
    {
        public static TcpServer2 OnlyDataTcpServer = new TcpServer2();
        public static TcpClient2 OnlyDataTcpClient = new TcpClient2();
        public static event EventHandler<SocketInfoArgs> OnlyDataServerOnClientDisConnected;
        public static event EventHandler OnlyDataClientDisConnected;
        public static string BoatBarcode = "default";

        public static TcpServer2 TcpServer = new TcpServer2();
        public static TcpClient2 TcpClient = new TcpClient2();
        private static FlowCarrierModule _flowCarrierModule;
        public static List<byte[]> DeliveryBoats = new List<byte[]>();
        public static List<DelphiBoatInfo> DelphiBoatInfos = new List<DelphiBoatInfo>();
        public static int CurrentID1 = 0;
        public static int CurrentID2 = 0;
        public static event EventHandler<SocketInfoArgs> ServerOnClientDisConnected;
        public static event EventHandler ClientDisConnected;

        #region 只給CDI2.0機台使用，目前只傳遞資料，不做交握
        //通常資料是Client傳給Server，Server頂多回復而已

        public static void OnlyDataServerOnline(int port)
        {
            if (!OnlyDataTcpServer.IsOnline)
            {
                OnlyDataTcpServer.DataIn -= OnlyDataTcpServer_DataIn;
                OnlyDataTcpServer.ClientDisConnected -= OnlyDataServerOnClientDisConnect;

                OnlyDataTcpServer.Port = port;
                OnlyDataTcpServer.ReceiveBufferSize = 10000;
                OnlyDataTcpServer.DataIn += OnlyDataTcpServer_DataIn;
                OnlyDataTcpServer.ClientDisConnected += OnlyDataServerOnClientDisConnect;
                OnlyDataTcpServer.OnLine();
            }
        }

        private static void OnlyDataTcpServer_DataIn(object sender, DataInArgs e)
        {
            var datas = Encoding.ASCII.GetString(e.Data);
            var splits = datas.Split(',');
            for (int i = 0; i < splits.Length; i++)
            {
                var obj = splits[i].Split(':');
                if (obj.Length > 1)
                {
                    string name = obj[0];
                    string value = obj[1];
                    switch (name)
                    {
                        case "BoatBarcode":
                            BoatBarcode = value;
                            TATool.BoatBarcode = BoatBarcode;
                            break;
                    }

                    OnlyDataServerSendData($"OK {name}");
                }
            }
        }

        public static void OnlyDataServerSendData(string data)
        {
            var bytes = Encoding.ASCII.GetBytes(data);
            var clients = OnlyDataTcpServer.ListClient();
            if (clients.Length > 0)
            {
                OnlyDataTcpServer.Send(clients[0], bytes);
            }
        }

        public static void OnlyDataServerOffline()
        {
            OnlyDataTcpServer.OffLine();
        }

        public static void OnlyDataServerDispose()
        {
            OnlyDataTcpServer.Dispose();
        }
        public static void OnlyDataServerOnClientDisConnect(object sender, SocketInfoArgs e)
        {
            OnlyDataServerOnClientDisConnected?.Invoke(sender, e);
        }

        public static void OnlyDataClientConnect(string ip, int port)
        {
            if (!OnlyDataTcpClient.IsConnect)
            {
                OnlyDataTcpClient.Disconnected -= OnlyDataGetClientDisconnect;
                OnlyDataTcpClient.DataIn -= OnlyDataTcpClient_DataIn;
                OnlyDataTcpClient.Ip = IPAddress.Parse(ip);
                OnlyDataTcpClient.Port = port;
                OnlyDataTcpClient.ReadBufferSize = 10000;
                OnlyDataTcpClient.Disconnected += OnlyDataGetClientDisconnect;
                OnlyDataTcpClient.DataIn += OnlyDataTcpClient_DataIn;
                OnlyDataTcpClient.Connect();
            }
        }

        private static void OnlyDataTcpClient_DataIn(object sender, DataInArgs e)
        {
            var datas = Encoding.ASCII.GetString(e.Data);
            if (datas.Contains("OK"))
            {
                _flowCarrierModule.LogTrace($"資料傳遞OK :{datas}");
            }
        }

        public static void OnlyDataClientSendData(string data)
        {
            //data格式為 Name:Value,Name:Value
            var bytes = Encoding.ASCII.GetBytes(data);
            OnlyDataTcpClient.Send(bytes);
        }
        public static void OnlyDataClientDisconnect()
        {
            OnlyDataTcpClient.Disconnect();
        }

        public static void OnlyDataClientDispose()
        {
            OnlyDataTcpClient.Dispose();
        }
        public static void OnlyDataGetClientDisconnect(object sender, EventArgs e)
        {
            OnlyDataClientDisConnected?.Invoke(sender, e);
        }

        #endregion

        public static void ClearList()
        {
            DeliveryBoats.Clear();
            DelphiBoatInfos.Clear();
        }

        public static void UpdateModule(FlowCarrierModule flowCarrierModule)
        {
            _flowCarrierModule = flowCarrierModule;
        }

        public static void ServerOnline(int port)
        {
            if (!TcpServer.IsOnline)
            {
                TcpServer.DataIn -=TcpServer_DataIn;
                TcpServer.ClientDisConnected -= ServerOnClientDisConnect;

                TcpServer.Port = port;
                TcpServer.ReceiveBufferSize = 2048;
                TcpServer.DataIn +=TcpServer_DataIn;
                TcpServer.ClientDisConnected += ServerOnClientDisConnect;
                TcpServer.OnLine();
            }
        }


        private static void TcpServer_DataIn(object sender, DataInArgs e)
        {
            //8碼Header， ID 2 byte, Cmd 1 byte, Type 1 byte, Length 4 byte
            //8碼後是Data，總長度為8+Length

            //分析矩陣內部資訊Switch進行回覆
            CmdEm Cmd = (CmdEm)(int)e.Data[2];
            TypeEm Type = (TypeEm)(int)e.Data[3];


            //連動Run 
            if (Cmd == CmdEm.cmdRun && Type == TypeEm.cmdRequest)
            {
                byte[] sendData = new byte[9];
                e.Data.CopyTo(sendData, 0);
                _flowCarrierModule.LogTrace("上游請求連動Run");
                if (_flowCarrierModule.Param.UseInterLock)
                {
                    if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.UnInitial)
                    {
                        _flowCarrierModule.LogTrace("Server連動Run");
                        //避免Run事件觸發
                        _flowCarrierModule.Rcv_Server_flag = true;

                        //機台啟動(連動)
                        if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.Running)
                            _flowCarrierModule.MachineSimpleController.Run();

                        sendData[8] = 1;//true
                                        //回饋連動Run
                        ServerSendMessage(CmdEm.cmdRun, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                    else
                    {
                        _flowCarrierModule.LogTrace("Server連動Run異常: "+ _flowCarrierModule.MachineSimpleController.State.ToString());
                        sendData[8] = 0;//false
                                        //回饋連動Run
                        ServerSendMessage(CmdEm.cmdRun, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                }
                else
                {
                    _flowCarrierModule.LogTrace("Run不啟動連動設定，UseInterLock=false");
                }
            }
            //接收Run回復
            else if (Cmd == CmdEm.cmdRun && Type == TypeEm.cmdReply)
            {
                _flowCarrierModule.LogTrace($"要求上游啟動 回復 : {e.Data[8]}");
            }
            //連動Stop
            else if (Cmd == CmdEm.cmdStop && Type == TypeEm.cmdRequest)
            {
                byte[] sendData = new byte[9];
                e.Data.CopyTo(sendData, 0);
                _flowCarrierModule.LogTrace("上游請求連動Stop");
                if (_flowCarrierModule.Param.UseInterLock)
                {
                    if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.UnInitial)
                    {
                        _flowCarrierModule.LogTrace("Server連動Stop");
                        //避免Stop事件觸發
                        _flowCarrierModule.Rcv_Server_flag = true;

                        //機台停止(連動)
                        if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.Idle)
                            _flowCarrierModule.MachineSimpleController.Stop();

                        sendData[8] = 1;//true
                        ServerSendMessage(CmdEm.cmdStop, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                    else
                    {
                        _flowCarrierModule.LogTrace("Server連動Stop異常: " + _flowCarrierModule.MachineSimpleController.State.ToString());
                        sendData[8] = 0;//false
                        ServerSendMessage(CmdEm.cmdStop, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                }
                else
                {
                    _flowCarrierModule.LogTrace("Stop不啟動連動設定，UseInterLock=false");
                }
            }
            //接收Stop回復
            else if (Cmd == CmdEm.cmdStop && Type == TypeEm.cmdReply)
            {
                _flowCarrierModule.LogTrace($";要求上游停止 回復 : {e.Data[8]}");
            }
            //DeliveBoat Rcv
            else if (Cmd == CmdEm.cmdDeliverBoat && Type == TypeEm.cmdRequest)
            {
                if (_flowCarrierModule.MachineSimpleController.State == HtaMachineController.MachineStateEm.Running)
                {
                    //流程已走到等待入料
                    if (_flowCarrierModule.CanPutProduct)
                    {
                        _flowCarrierModule.LogTrace("接收到DeliveBoat資訊，流程已到等待入料點");

                        DeliveryBoats.Add(e.Data);

                        //重置，確認Flow已到等待入料
                        //_flowCarrierModule.CanPutProduct = false;     //改由入料銜接IO觸發才修改

                        byte[] sendData = new byte[9];
                        for (int i = 0; i < 9; i++)
                        {
                            sendData[i] = e.Data[i];
                        }

                        //可入料Ready
                        sendData[8] = 1;//true

                        //傳送
                        ServerSendMessage(CmdEm.cmdDeliverBoat, TypeEm.cmdReply, sendData, 1, sendData[0], sendData[1], 9);//回復回9碼                                                                                           

                        WriteByteData(e.Data);
                        if (_flowCarrierModule.Param.TCPIPUseXml)
                        {
                            try
                            {
                                var boatInfo = DecodeDelphiBoatInfoXml(e.Data);//DS3070S是用這個解析
                                DelphiBoatInfos.Add(boatInfo);
                                _flowCarrierModule.MapData.BoatID = boatInfo.BoatId;
                                _flowCarrierModule.MapData.LotID = boatInfo.LotId;
                                _flowCarrierModule.MapData.Row = boatInfo.RowCount;
                                _flowCarrierModule.MapData.Col = boatInfo.ColCount;

                                _flowCarrierModule.MapData.List_HSID.Clear();
                                _flowCarrierModule.MapData.List_SubID.Clear();
                                for (int y = 0; y < boatInfo.RowCount; y++)
                                {
                                    for (int x = 0; x < boatInfo.ColCount; x++)
                                    {
                                        _flowCarrierModule.MapData.List_HSID.Add(boatInfo.PartInfo[x][y].HsId);
                                        _flowCarrierModule.MapData.List_SubID.Add(boatInfo.PartInfo[x][y].SubId);
                                    }
                                }
                                var mapData = _flowCarrierModule.MapData;
                                CreateMapDataReport(ref mapData);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        _flowCarrierModule.UpperPutQueue.Add(true);

                    }
                    else //無法入料
                    {
                        _flowCarrierModule.LogTrace("接收到DeliveBoat資訊，流程未到等待入料點");
                        byte[] sendData = new byte[9];
                        for (int i = 0; i < 9; i++)
                        {
                            sendData[i] = e.Data[i];
                        }
                        //不可入料Ready
                        sendData[8] = 0;
                        //傳送
                        ServerSendMessage(CmdEm.cmdDeliverBoat, TypeEm.cmdReply, sendData, 1, sendData[0], sendData[1], 9);
                    }
                }
                else
                {
                    _flowCarrierModule.LogTrace("接收到DeliveBoat資訊，機台暫停中");
                    byte[] sendData = new byte[9];
                    for (int i = 0; i < 9; i++)
                    {
                        sendData[i] = e.Data[i];
                    }
                    //不可入料Ready
                    sendData[8] = 0;
                    //傳送
                    ServerSendMessage(CmdEm.cmdDeliverBoat, TypeEm.cmdReply, sendData, 1, sendData[0], sendData[1], 9);
                }
            }
            else if (Cmd == CmdEm.cmdGotBoat && Type == TypeEm.cmdReply)
            {
                _flowCarrierModule.LogTrace("上游回復收到我得GotBoat");
            }
        }

        public static void ServerSendMessage(CmdEm cmd, TypeEm type, byte[] array, int ilength, byte iID, byte iID2, int iSendSize)
        {
            try
            {
                CurrentID1 = iID;
                CurrentID2 = iID2;
                _flowCarrierModule.LogTrace($"Server Send : ID : {(byte)iID} , ID2:{(byte)iID2}, Cmd : {(byte)cmd} , Type : {(byte)type} , Length : {(byte)ilength}");
                //Server訊息傳送
                if (iID != 0)
                    array[0] = (byte)iID;

                if (iID2 != 0)
                    array[1] = (byte)iID2;

                array[2] = (byte)cmd;
                array[3] = (byte)type;
                //array[4] = (byte)ilength;
                //server_networkstream.Write(array, 0, iSendSize);
                var lengthArray = BitConverter.GetBytes(ilength);
                Buffer.BlockCopy(lengthArray, 0, array, 4, lengthArray.Length);


                var clients = TcpServer.ListClient();
                if (clients.Length > 0)
                {
                    TcpServer.Send(clients[0], array);
                    _flowCarrierModule.LogTrace($"IP:{clients[0].Address.ToString()},Port{clients[0].Port}");
                }
            }
            catch (Exception ex)
            {
                _flowCarrierModule.LogTrace("Server傳送資訊錯誤");
            }
        }

        public static void ServerOffline()
        {
            TcpServer.OffLine();
        }

        public static void ServerDispose()
        {
            TcpServer.Dispose();
        }

        public static void ServerOnClientDisConnect(object sender, SocketInfoArgs e)
        {
            ServerOnClientDisConnected?.Invoke(sender, e);
        }


        public static void ClientConnect(string ip, int port)
        {
            if (!TcpClient.IsConnect)
            {
                TcpClient.Disconnected -= GetClientDisconnect;
                TcpClient.DataIn -= TcpClient_DataIn;
                TcpClient.Ip = IPAddress.Parse(ip);
                TcpClient.Port = port;
                TcpClient.ReadBufferSize = 10000;
                TcpClient.Disconnected += GetClientDisconnect;
                TcpClient.DataIn += TcpClient_DataIn;
                TcpClient.Connect();
            }
        }

        private static void TcpClient_DataIn(object sender, DataInArgs e)
        {

            //分析矩陣內部資訊Switch進行回覆
            CmdEm Cmd = (CmdEm)(int)e.Data[2];
            TypeEm Type = (TypeEm)(int)e.Data[3];

            //連動Run 
            if (Cmd == CmdEm.cmdRun && Type == TypeEm.cmdRequest)
            {
                byte[] sendData = new byte[9];
                e.Data.CopyTo(sendData, 0);
                _flowCarrierModule.LogTrace("下游請求連動Run");

                if (_flowCarrierModule.Param.UseInterLock)
                {
                    if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.UnInitial)
                    {
                        _flowCarrierModule.LogTrace("Client連動Run");
                        _flowCarrierModule.Rcv_Client_flag = true;//避免Run事件觸發

                        if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.Running)
                            _flowCarrierModule.MachineSimpleController.Run();//機台啟動(連動)

                        sendData[8] = 1;//true
                                        //回復Run
                        ClientSendMessage(CmdEm.cmdRun, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                    else
                    {
                        _flowCarrierModule.LogTrace("Client連動Run異常: " + _flowCarrierModule.MachineSimpleController.State.ToString());
                        sendData[8] = 0;//false
                                        //回復Run
                        ClientSendMessage(CmdEm.cmdRun, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                }
                else
                {
                    _flowCarrierModule.LogTrace("Run不啟動連動設定，UseInterLock=false");
                }
            }
            //接收Run回復
            else if (Cmd == CmdEm.cmdRun && Type == TypeEm.cmdReply)
            {
                _flowCarrierModule.LogTrace($"要求下游啟動 回復 : {e.Data[8]}");
            }
            //連動Stop
            else if (Cmd == CmdEm.cmdStop && Type == TypeEm.cmdRequest)
            {
                byte[] sendData = new byte[9];
                e.Data.CopyTo(sendData, 0);
                _flowCarrierModule.LogTrace("下游請求連動Stop");
                if (_flowCarrierModule.Param.UseInterLock)
                {
                    if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.UnInitial)
                    {
                        _flowCarrierModule.LogTrace("Client連動Stop");

                        _flowCarrierModule.Rcv_Client_flag = true;//避免Stop事件觸發

                        if (_flowCarrierModule.MachineSimpleController.State != HtaMachineController.MachineStateEm.Idle)
                            _flowCarrierModule.MachineSimpleController.Stop();//機台停止(連動)

                        sendData[8] = 1;//true
                                        //回復Stop
                        ClientSendMessage(CmdEm.cmdStop, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                    else
                    {
                        _flowCarrierModule.LogTrace("Client連動Stop異常: "  + _flowCarrierModule.MachineSimpleController.State.ToString());
                        sendData[8] = 0;//false
                                        //回復Stop
                        ClientSendMessage(CmdEm.cmdStop, TypeEm.cmdReply, sendData, 1, 0, 0, 9);
                    }
                }
                else
                {
                    _flowCarrierModule.LogTrace("Stop不啟動連動設定，UseInterLock=false");
                }
            }
            //接收Stop回復
            else if (Cmd == CmdEm.cmdStop && Type == TypeEm.cmdReply)
            {
                _flowCarrierModule.LogTrace($"要求下游停止 回復 : {e.Data[8]}");
            }
            //接收下游是否可接料
            else if (Cmd == CmdEm.cmdDeliverBoat && Type == TypeEm.cmdReply)
            {
                if (_flowCarrierModule.MachineSimpleController.State == HtaMachineController.MachineStateEm.Running)
                {
                    if (e.Data[0] == _flowCarrierModule.IndexCount)
                    {
                        if (e.Data[8] == 1)
                        {
                            //可出料
                            _flowCarrierModule.LogTrace("可出料至下游");
                            _flowCarrierModule.LowerReadyQueue.Add(true);
                        }
                        else if (e.Data[8] == 0)
                        {
                            //不可出料
                            _flowCarrierModule.LogTrace("不可出料至下游");
                        }
                    }
                    else
                    {
                        _flowCarrierModule.LogTrace("ID比對錯誤!!");
                    }
                }
                else
                {
                    _flowCarrierModule.LogTrace("不再Running，不作事");
                }
            }
            else if (Cmd == CmdEm.cmdGotBoat && Type == TypeEm.cmdRequest)
            {
                _flowCarrierModule.LogTrace("下游已接到料，已回GotBoat");
                //收到訊息，不用回復
            }
        }

        public static void ClientSendMessage(CmdEm cmd, TypeEm type, byte[] array, int ilength, byte iID, byte iID2, int iSendSize)
        {
            //Clinet訊息傳送
            try
            {
                if (cmd == CmdEm.cmdDeliverBoat)
                {
                    //傳送DeliberBoat資訊給下游
                    _flowCarrierModule.LogTrace($"Client Send : cmdDeliverBoat ID : {(byte)iID} , {(byte)iID2}");
                    array[0] = (byte)iID;
                    array[1] = (byte)iID2;
                    TcpClient.Send(array);
                }
                else
                {
                    _flowCarrierModule.LogTrace($"Client Send : ID : {(byte)iID} {(byte)iID2} , Cmd : {(byte)cmd} , Type : {(byte)type} , Length : {(byte)ilength}");
                    //Clinet訊息傳送
                    if (iID != 0)
                        array[0] = (byte)iID;
                    if (iID2 != 0)
                        array[1] = (byte)iID2;

                    array[2] = (byte)cmd;
                    array[3] = (byte)type;
                    array[4] = (byte)ilength;

                    TcpClient.Send(array);
                }
            }
            catch (Exception ex) { _flowCarrierModule.LogTrace("Clinet傳送資訊錯誤"); }
        }

        public static void ClientDisconnect()
        {
            TcpClient.Disconnect();
        }

        public static void ClientDispose()
        {
            TcpClient.Dispose();
        }

        public static void GetClientDisconnect(object sender, EventArgs e)
        {
            ClientDisConnected?.Invoke(sender, e);
        }

        public static void WriteByteData(byte[] byteData)
        {
            string path = @"D:\Coordinator2.0\TADatas\DeliveryBoats";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            string byteStr = "";
            for (int i = 0; i < byteData.Length; i++)
            {
                byteStr += byteData[i] + ",";
            }
            using (StreamWriter w = new StreamWriter(path + "\\" + "BoatData_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"))
            {
                w.WriteLine(byteStr);
                w.Close();
            }
        }

        public static byte[] ReadByteData()
        {
            string path = @"D:\Coordinator2.0\TADatas\DeliveryBoats";
            var files = Directory.GetFiles(path, "BoatData_*.txt");
            string byteStr = "";
            using (StreamReader w = new StreamReader(files[files.Length-1]))
            {
                byteStr = w.ReadToEnd();
                w.Close();
            }
            var splits = byteStr.Split(',');
            byte[] bytes = new byte[splits.Length-1];
            for (int i = 0; i < splits.Length-1; i++)
            {
                bytes[i] = Convert.ToByte(splits[i].Trim());
            }
            return bytes;
        }

        /// <summary>
        /// 解析Delphi傳來的xml byte 轉換成xml資料給內部使用，從LD50X0搬來。Xml解析是用在DS3070S的部分，跟DS3070格式不一樣
        /// </summary>
        /// <param name="byteData"></param>
        /// <returns></returns>
        public static DelphiBoatInfo DecodeDelphiBoatInfoXml(byte[] byteData)
        {
            //獲取Boat Info 長度，Skip ID 2 byte, Cmd 1 byte, Type 1 byte，Take Length 4 byte
            var lengthArray = byteData.Skip(4).Take(4).ToArray();

            //透過BitConverter轉換byte[] to int
            var length = BitConverter.ToInt32(lengthArray, 0);
            var headSize = 8;

            var str = Encoding.ASCII.GetString(byteData, headSize, length);

            var boatInfo = new DelphiBoatInfo();

            //因<Boat>前有一些不明字元，所以先移除
            var startStr = "<BOAT>";
            var xmlStart = str.IndexOf(startStr);

            var endStr = "</BOAT>";
            var xmlEnd = str.IndexOf(endStr) + endStr.Length;

            str = str.Substring(xmlStart, xmlEnd - xmlStart);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);

            var versionStr = xmlDoc.SelectSingleNode("//VERSION")?.InnerText;
            boatInfo.Version = Convert.ToInt32(versionStr);

            var rowCountStr = xmlDoc.SelectSingleNode("//ROWS")?.InnerText;
            boatInfo.RowCount = Convert.ToInt32(rowCountStr);

            var colCountStr = xmlDoc.SelectSingleNode("//COlS")?.InnerText;
            boatInfo.ColCount = Convert.ToInt32(colCountStr);

            #region Part State Decode

            boatInfo.PartInfo = new PartInfo[boatInfo.RowCount][];

            var rowList = xmlDoc.SelectNodes("//ROWS");

            if (rowList != null)
            {
                //因為第一個是RowCount但命名相同所以跳過才能找到正確PartState的Row,Col
                var startIndex = 1;
                for (int i = startIndex; i < rowList.Count; i++)
                {
                    XmlNode row = rowList[i];
                    var colList = row.ChildNodes;

                    boatInfo.PartInfo[i - startIndex] = new PartInfo[colList.Count];

                    for (int j = 0; j < colList.Count; j++)
                    {
                        var col = colList[j];

                        var partInfo = new PartInfo();

                        var statesStr = col["STATES"]?.InnerText;
                        var setOfState = GetIndicesOfOnes(Convert.ToInt32(statesStr));

                        foreach (var setOf in setOfState)
                        {
                            partInfo.State.Add((PartStateEm)setOf);
                        }

                        var subIDStr = col["SUBID"]?.InnerText;
                        partInfo.SubId = subIDStr;

                        var hSIDStr = col["HSID"]?.InnerText;
                        partInfo.HsId = hSIDStr;

                        var colDispSetStr = col["DispSet"]?.InnerText;
                        var setOfColDispSet = GetIndicesOfOnes(Convert.ToInt32(colDispSetStr));

                        foreach (var setOf in setOfColDispSet)
                        {
                            partInfo.DispenseTypeSet.Add((DispenseTypeSetEm)setOf);
                        }

                        var colTimeStampStr = col["DISPTIMESTAMP"]?.InnerText;
                        partInfo.DispenseTimeStamp = colTimeStampStr;

                        boatInfo.PartInfo[i - startIndex][j] = partInfo;
                    }
                }
            }

            #endregion

            var trackStr = xmlDoc.SelectSingleNode("//TRACK")?.InnerText;
            boatInfo.Track = (TrackEm)Convert.ToInt32(trackStr);

            var autoStr = xmlDoc.SelectSingleNode("//AUTO")?.InnerText;
            boatInfo.Auto = Convert.ToBoolean(autoStr);

            var modeStr = xmlDoc.SelectSingleNode("//MODE")?.InnerText;
            boatInfo.Mode = (ModeEm)Convert.ToInt32(modeStr);

            var dispSetStr = xmlDoc.SelectSingleNode("//DispSet")?.InnerText;
            //需轉換Int 為Bit 並確認集合Add到List中
            var setOfDispSet = GetIndicesOfOnes(Convert.ToInt32(dispSetStr));

            foreach (var setOf in setOfDispSet)
            {
                boatInfo.DispenseTypeSet.Add((DispenseTypeSetEm)setOf);
            }

            var timeStampStr = xmlDoc.SelectSingleNode("//TIMESTAMP")?.InnerText;
            boatInfo.TimeStamp = timeStampStr;

            var magazineNoStr = xmlDoc.SelectSingleNode("//MAGNO")?.InnerText;
            boatInfo.MagazineNo = Convert.ToInt32(magazineNoStr);

            var endLotStr = xmlDoc.SelectSingleNode("//ENDLOT")?.InnerText;
            boatInfo.EndLot = Convert.ToBoolean(endLotStr);

            var boatIdStr = xmlDoc.SelectSingleNode("//BOATID")?.InnerText;
            boatInfo.BoatId = boatIdStr;

            var magazineIdStr = xmlDoc.SelectSingleNode("//MAGID")?.InnerText;
            boatInfo.MagazineId = magazineIdStr;

            var magazineSlotStr = xmlDoc.SelectSingleNode("//MAGSLOT")?.InnerText;
            boatInfo.MagazineSlot = Convert.ToInt32(magazineSlotStr);

            var magazineBoatCountStr = xmlDoc.SelectSingleNode("//MAGBOATCOUNT")?.InnerText;
            boatInfo.MagazineBoatCount = Convert.ToInt32(magazineBoatCountStr);

            var lotIdStr = xmlDoc.SelectSingleNode("//LOTID")?.InnerText;
            boatInfo.LotId = lotIdStr;

            var idStr = xmlDoc.SelectSingleNode("//ID")?.InnerText;
            boatInfo.ID = idStr;

            var upLoadStr = xmlDoc.SelectSingleNode("//Upload")?.InnerText;
            boatInfo.UpLoad = Convert.ToBoolean(upLoadStr);

            return boatInfo;
        }

        public static List<int> GetIndicesOfOnes(int number)
        {
            List<int> indices = new List<int>();
            int position = 0;

            while (number > 0)
            {
                // 檢查最低位元是否為 1
                if ((number & 1) == 1)
                {
                    indices.Add(position);
                }
                number >>= 1; // 右移一位
                position++;
            }

            return indices;
        }

        public static bool CreateMapDataReport(ref MapDataInfo mapData)
        {
            try
            {
                //確認資料是否符合
                int _count = mapData.Row * mapData.Col;
                if (mapData.List_SubID.Count != _count && mapData.List_HSID.Count != _count)
                    return false;

                DateTime utcNow = DateTime.UtcNow;
                string formattedDate = utcNow.ToString("yyyy/MM/dd HH:mm:ss");

                // 創建XmlDocument對象
                XmlDocument _doc = new XmlDocument();

                // 創建根節點 <MapData>
                XmlElement _mapData = _doc.CreateElement("MapData");
                _doc.AppendChild(_mapData);

                // 創建 <SubstrateMaps> 節點並添加命名空間
                XmlElement _substrateMaps = _doc.CreateElement("SubstrateMaps");
                _substrateMaps.SetAttribute("xmlns:xsi", "urn:semi-org:xsd.E142-1.V1005.SubstrateMap");
                _mapData.AppendChild(_substrateMaps);

                // 創建 <SubstrateMap> 節點並設置屬性
                XmlElement substrateMap = _doc.CreateElement("SubstrateMap");
                substrateMap.SetAttribute("SubstrateId", mapData.LotID + "-" + mapData.BoatID);
                substrateMap.SetAttribute("AxisDirection", "4");
                substrateMap.SetAttribute("SubstrateSide", "TopSide");
                substrateMap.SetAttribute("OriginLocation", "2");
                substrateMap.SetAttribute("Orientation", "Downward");
                substrateMap.SetAttribute("SubstrateType", "Wafer");
                substrateMap.SetAttribute("FinishTime", formattedDate);
                substrateMap.SetAttribute("Schedule", mapData.LotID);
                substrateMap.SetAttribute("EQP_PickTime", formattedDate);
                substrateMap.SetAttribute("EQP_PlaceTime", formattedDate);
                _substrateMaps.AppendChild(substrateMap);

                // 創建 <Overlay> 節點並設置屬性
                XmlElement _overlay = _doc.CreateElement("Overlay");
                _overlay.SetAttribute("MapVersion", "1");
                _overlay.SetAttribute("MapName", "2D Matrix");
                substrateMap.AppendChild(_overlay);

                // 創建 <DeviceIdMap> 節點
                XmlElement _deviceIdMap = _doc.CreateElement("DeviceIdMap");
                _overlay.AppendChild(_deviceIdMap);

                int _listCount = 0;

                // 添加多個 <Id> 節點
                for (int j = 0; j < mapData.Row; j++)
                {
                    for (int i = 0; i < mapData.Col; i++)
                    {
                        AddIdNode(_doc, _deviceIdMap, i.ToString(), j.ToString(), mapData.List_SubID[_listCount], mapData.List_HSID[_listCount]);
                        _listCount++;
                    }
                }

                //// 添加多個 <Id> 節點
                //AddIdNode(_doc, _deviceIdMap, "0", "0", "469602020U230204", "Je310724JR04626450");
                //AddIdNode(_doc, _deviceIdMap, "1", "0", "469602020U210308", "Je310724JR04624091");
                //AddIdNode(_doc, _deviceIdMap, "2", "0", "469602020U220404", "Je050824JR04626785");
                //AddIdNode(_doc, _deviceIdMap, "0", "1", "469602020U340207", "Je310724JR04624670");
                //AddIdNode(_doc, _deviceIdMap, "1", "1", "469602020U240308", "Je310724JR04624076");
                //AddIdNode(_doc, _deviceIdMap, "2", "1", "469602020U210408", "Je050824JR04627720");

                mapData.MapDataString = _doc.OuterXml;

                var folderPath = "D:\\Coordinator2.0\\TADatas\\";
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // 保存XML到文件或控制台輸出
                _doc.Save($"{folderPath}{mapData.BoatID}.xml");

                return true;
            }
            catch (Exception ex)
            {
                _flowCarrierModule.LogTrace(ex.ToString());
                return false;
            }
        }
        static void AddIdNode(XmlDocument doc, XmlElement parent, string x, string y, string mapUnit2D, string idValue)
        {
            XmlElement idNode = doc.CreateElement("Id");
            idNode.SetAttribute("X", x);
            idNode.SetAttribute("Y", y);
            idNode.SetAttribute("MapUnit2D", mapUnit2D);
            idNode.InnerText = idValue;
            parent.AppendChild(idNode);
        }

    }

    #region Decode 物件

    [Serializable]
    public class DelphiBoatInfo
    {
        public int Version { get; set; }

        public int RowCount { get; set; }

        public int ColCount { get; set; }

        /// <summary>
        /// 每一顆產品內製程相關狀態
        /// </summary>
        public PartInfo[][] PartInfo { get; set; }

        /// <summary>
        /// 此Boat從哪個流道進入
        /// </summary>
        public TrackEm Track { get; set; }

        /// <summary>
        /// 此Boat是否為自動流程作業的
        /// </summary>
        public bool Auto { get; set; }

        /// <summary>
        /// 此Boat作業模式
        /// </summary>
        public ModeEm Mode { get; set; }

        /// <summary>
        /// 此Boat經歷過哪個點膠製程
        /// </summary>
        public List<DispenseTypeSetEm> DispenseTypeSet { get; set; }

        /// <summary>
        /// 作業時間戳記
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// Magazine 編號
        /// </summary>
        public int MagazineNo { get; set; }

        /// <summary>
        /// 是否為最後一片
        /// </summary>
        public bool EndLot { get; set; }

        /// <summary>
        /// Boat ID 通常為Barcode 讀取獲得
        /// </summary>
        public string BoatId { get; set; }

        /// <summary>
        /// 此Boat 隸屬的 Magazine ID 通常為Barcode 讀取獲得
        /// </summary>
        public string MagazineId { get; set; }

        /// <summary>
        /// 此Boat為該Magazine的第幾個Slot數量
        /// </summary>
        public int MagazineSlot { get; set; }

        /// <summary>
        /// 此Boat隸屬的Magazine總共有多少個Slot數量
        /// </summary>
        public int MagazineBoatCount { get; set; }

        /// <summary>
        /// 此Boat作業的Lot ID
        /// </summary>
        public string LotId { get; set; }

        /// <summary>
        /// 持續作業的ID，尚不確定使用時機
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 尚不確定使用時機與用途
        /// </summary>
        public bool UpLoad { get; set; }
    }

    [Serializable]
    public class MapDataInfo
    {
        public int Col { get; set; } = 0;
        public int Row { get; set; } = 0;
        public string BoatID { get; set; } = "";
        public string LotID { get; set; } = "";
        public List<string> List_SubID { get; set; } = new List<string>();
        public List<string> List_HSID { get; set; } = new List<string>();
        public string MapDataString { get; set; } = "";
    }

    [Serializable]
    public class PartInfo
    {
        /// <summary>
        /// 產品經歷狀態，集合表示後續獲取的狀態使用Bit的方式獲取 0000 0011 -> 表示經歷了pstPart、pstDispense1狀態
        /// </summary>
        public List<PartStateEm> State { get; set; } = new List<PartStateEm>();

        /// <summary>
        /// 產品經歷過多少個點膠製成，貼銦片製程使用
        /// </summary>
        public List<DispenseTypeSetEm> DispenseTypeSet { get; set; } = new List<DispenseTypeSetEm>();

        /// <summary>
        /// 紀錄產品何時進行點膠
        /// </summary>
        public string DispenseTimeStamp { get; set; }

        /// <summary>
        /// 基板 ID
        /// </summary>
        public string SubId { get; set; }

        /// <summary>
        /// 散熱片 ID
        /// </summary>
        public string HsId { get; set; }
    }

    #endregion

    public enum CmdEm
    {
        cmdRun, cmdStop, cmdEmg, cmdInterrupt, cmdQryDeliverBoat,
        cmdDeliverBoat, cmdRequireBoat, cmdGotBoat, cmdQryDispStatus,
        cmdTrackSafe, cmdMagInfo, cmdRemoveBoat, cmdMagProceedData,
        cmdQueryBoat, cmdUpdateBoat, cmdHeartBeat
    }
    public enum TypeEm
    {
        cmdRequest, cmdReply
    }

    /// <summary>
    /// 產品經過哪些製程
    /// </summary>
    public enum PartStateEm
    {
        /// <summary>
        /// 表示有產品
        /// </summary>
        pstPart = 0,

        /// <summary>
        /// 經歷過點膠1
        /// </summary>
        pstDispense1,

        /// <summary>
        /// 經歷過點膠2
        /// </summary>
        pstDispense2,

        /// <summary>
        /// 經歷過植片1
        /// </summary>
        pstMount1,

        /// <summary>
        /// 經歷過植片2
        /// </summary>
        psMount2,

        /// <summary>
        /// 經歷過噴膠1
        /// </summary>
        pstSpray1,

        /// <summary>
        /// 經歷過噴膠2
        /// </summary>
        pstSpray2
    }

    /// <summary>
    /// 表示經歷過多少個點膠製程
    /// </summary>
    public enum DispenseTypeSetEm
    {
        dispA = 0,

        dispB,

        dispC,

        dispD,
    }

    /// <summary>
    /// 紀錄產品經過的流道
    /// </summary>
    public enum TrackEm
    {
        /// <summary>
        /// 前流道
        /// </summary>
        trkFront = 0,

        /// <summary>
        /// 後流道
        /// </summary>
        trkRead
    }

    /// <summary>
    /// Delphi DS 作業模式
    /// </summary>
    public enum ModeEm
    {
        pmD1M1D2M2 = 0,

        pmD1M2
    }
}
