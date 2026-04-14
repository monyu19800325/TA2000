using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTA.Com.TCPIP2;
using System.Net;
using HTA.Com.TCPIP;
using System.Drawing;
using DevExpress.Utils.Automation;
using System.Windows.Forms;
using DevExpress.Mvvm;
using System.Threading;
namespace TA2000Modules
{
    [POCOViewModel()]
    public class TCPIPComViewModel
    {
        public IDispatcherService DispatcherService => this.GetService<IDispatcherService>();
        public FlowCarrierGlobalParam Param { get; set; }
        public byte[] tmpData;
        bool isDelegate = false;
        bool _isClose = false;
        public string TCPIPClientIP
        {
            get => Param.TCPIPClientIP;
            set
            {
                if (Param.TCPIPClientIP == value)
                    return;
                Param.TCPIPClientIP = value;
                this.RaisePropertyChanged(x => x.TCPIPClientIP);
            }
        }
        public int TCPIPClientPort
        {
            get => Param.TCPIPClientPort;
            set
            {
                if (Param.TCPIPClientPort == value)
                    return;
                Param.TCPIPClientPort = value;
                this.RaisePropertyChanged(x => x.TCPIPClientPort);
            }
        }

        public int TCPIPServerPort
        {
            get => Param.TCPIPServerPort;
            set
            {
                if (Param.TCPIPServerPort == value)
                    return;
                Param.TCPIPServerPort = value;
                this.RaisePropertyChanged(x => x.TCPIPServerPort);
            }
        }

        public virtual Color ServerLight
        {
            get
            {
                if (TCPIPTool.TcpServer == null)
                    return Color.Gray;
                if (ServerStateText > 0)
                {
                    return Color.Green;
                }
                return TCPIPTool.TcpServer.IsOnline ? Color.Orange : Color.Red;
            }
        }
        public virtual Color ClientLight
        {
            get
            {
                if (TCPIPTool.TcpClient == null)
                    return Color.Gray;
                return TCPIPTool.TcpClient.IsConnect ? Color.Green : Color.Red;
            }
        }

        public virtual int ServerStateText { get; set; } = 0;

        public void Init(FlowCarrierGlobalParam param)
        {
            Param = param;
            TCPIPTool.ServerOnClientDisConnected += ServerOnClientDisconnect;
            TCPIPTool.ClientDisConnected += OnClientDisconnect;

            Thread threadUpdate = new Thread(UpdateState);

            threadUpdate.Start();
        }

        public void UpdateState()
        {
            while (!_isClose)
            {
                DispatcherService.Invoke(() =>
                {
                    if (TCPIPTool.TcpServer.IsOnline)
                    {
                        ServerStateText = TCPIPTool.TcpServer.ListClient().Count();
                    }
                    this.RaisePropertiesChanged();
                });
                SpinWait.SpinUntil(() => false, 1000);
            }
        }

        public void ServerOnline()
        {
            TCPIPTool.ServerOnline(TCPIPServerPort);
            DispatcherService?.Invoke(() =>
            {
                this.RaisePropertiesChanged();
            });
        }

        public void ServerOffline()
        {
            TCPIPTool.ServerOffline();
            ServerStateText = 0;
            DispatcherService?.Invoke(() =>
            {
                this.RaisePropertiesChanged();
            });
        }


        public void Connect()
        {
            TCPIPTool.ClientConnect(TCPIPClientIP, TCPIPClientPort);
            DispatcherService?.Invoke(() =>
            {
                this.RaisePropertiesChanged();
            });
        }

        public void Disconnect()
        {
            TCPIPTool.ClientDisconnect();
            DispatcherService?.Invoke(() =>
            {
                this.RaisePropertiesChanged();
            });
        }

        public void ServerReplyDeliveryBoatOK()
        {
            byte[] bytes = new byte[9];
            bytes[0] = (byte)TCPIPTool.CurrentID1;
            bytes[1] = (byte)TCPIPTool.CurrentID2;
            bytes[2] = (byte)CmdEm.cmdDeliverBoat;
            bytes[3] = (byte)TypeEm.cmdReply;
            bytes[4] = 1;
            bytes[8] = 1;
            TCPIPTool.ServerSendMessage(CmdEm.cmdDeliverBoat, TypeEm.cmdReply, bytes, 1, bytes[0], bytes[1], 9);
        }

        public void ServerReplyGotBoat()
        {
            byte[] bytes = new byte[10];
            bytes[0] = (byte)TCPIPTool.CurrentID1;
            bytes[1] = (byte)TCPIPTool.CurrentID2;
            bytes[2] = (byte)CmdEm.cmdGotBoat;
            bytes[3] = (byte)TypeEm.cmdRequest;
            bytes[4] = 2;
            TCPIPTool.ServerSendMessage(CmdEm.cmdGotBoat, TypeEm.cmdRequest, bytes, 2, bytes[0], bytes[1], 10);
        }

        public void ServerSendRun()
        {
            byte[] bytes = new byte[8];
            bytes[0] = (byte)TCPIPTool.CurrentID1;
            bytes[1] = (byte)TCPIPTool.CurrentID2;
            bytes[2] = (byte)CmdEm.cmdRun;
            bytes[3] = (byte)TypeEm.cmdRequest;
            bytes[4] = 0;
            TCPIPTool.ServerSendMessage(CmdEm.cmdRun, TypeEm.cmdRequest, bytes, 0, bytes[0], bytes[1], 8);
        }

        public void ServerSendStop()
        {
            byte[] bytes = new byte[8];
            bytes[0] = (byte)TCPIPTool.CurrentID1;
            bytes[1] = (byte)TCPIPTool.CurrentID2;
            bytes[2] = (byte)CmdEm.cmdStop;
            bytes[3] = (byte)TypeEm.cmdRequest;
            bytes[4] = 0;
            TCPIPTool.ServerSendMessage(CmdEm.cmdStop, TypeEm.cmdRequest, bytes, 0, bytes[0], bytes[1], 8);
        }

        public void ServerOnClientDisconnect(object sender, SocketInfoArgs e)
        {
            DispatcherService?.Invoke(() =>
            {
                ServerStateText = 0;
                this.RaisePropertiesChanged();
            });
        }

        public void ClientDeliveryBoat()
        {
            if (tmpData == null)
            {
                byte[] bytes = new byte[145];//test 464
                bytes[0] = (byte)TCPIPTool.CurrentID1;
                bytes[1] = (byte)TCPIPTool.CurrentID2;
                bytes[2] = (byte)CmdEm.cmdDeliverBoat;
                bytes[3] = (byte)TypeEm.cmdRequest;
                bytes[4] = (byte)(bytes.Length-8);
                tmpData = bytes;

                //test data
                //var readByte = File.ReadAllBytes(@"D:\\LI3000\DS3070SXML格式.txt");
                //for (int i = 0; i < readByte.Length; i++)
                //{
                //    tmpData[8+i] = readByte[i];
                //}

                //var lenByte = BitConverter.GetBytes(tmpData.Length-8);
                //var length = (lenByte[3] << 24) + (lenByte[2] << 16) + (lenByte[1] << 8) + lenByte[0];
                //for (int i = 0; i < lenByte.Length; i++)
                //{
                //    tmpData[4 + i] = lenByte[i];
                //}
                //var str = Encoding.ASCII.GetString(tmpData, 8, tmpData.Length-8);
            }
            TCPIPTool.ClientSendMessage(CmdEm.cmdDeliverBoat, TypeEm.cmdRequest, tmpData, tmpData.Length-8, tmpData[0], tmpData[1], tmpData.Length);
        }

        public void ClientSendRun()
        {
            byte[] bytes = new byte[8];
            bytes[0] = (byte)TCPIPTool.CurrentID1;
            bytes[1] = (byte)TCPIPTool.CurrentID2;
            bytes[2] = (byte)CmdEm.cmdRun;
            bytes[3] = (byte)TypeEm.cmdRequest;
            bytes[4] = 0;
            TCPIPTool.ClientSendMessage(CmdEm.cmdRun, TypeEm.cmdRequest, bytes, 0, bytes[0], bytes[1], 8);
        }

        public void ClientSendStop()
        {
            byte[] bytes = new byte[8];
            bytes[0] = (byte)TCPIPTool.CurrentID1;
            bytes[1] = (byte)TCPIPTool.CurrentID2;
            bytes[2] = (byte)CmdEm.cmdStop;
            bytes[3] = (byte)TypeEm.cmdRequest;
            bytes[4] = 0;
            TCPIPTool.ClientSendMessage(CmdEm.cmdStop, TypeEm.cmdRequest, bytes, 0, bytes[0], bytes[1], 8);
        }

        public void OnClientDisconnect(object sender, EventArgs e)
        {
            DispatcherService?.Invoke(() =>
            {
                this.RaisePropertiesChanged();
            });
        }

        public void OnFormClose(object sender, FormClosingEventArgs e)
        {
            TCPIPTool.ServerOnClientDisConnected -= ServerOnClientDisconnect;
            TCPIPTool.ClientDisConnected -= OnClientDisconnect;
            _isClose = true;
        }

        public void ReadNewDeliveryBoat()
        {
            var bytes = TCPIPTool.ReadByteData();
            tmpData = bytes;
        }
    }

}
