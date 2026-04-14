using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class FlowCarrierGlobalSettingViewModel
    {
        FlowCarrierGlobalParam _flowCarrierGlobalParam;
        public int ServerPort
        {
            get => _flowCarrierGlobalParam.TCPIPServerPort;
            set
            {
                if (_flowCarrierGlobalParam.TCPIPServerPort == value)
                    return;
                _flowCarrierGlobalParam.TCPIPServerPort = value;
                this.RaisePropertyChanged(x => x.ServerPort);
            }
        }
        public string ClientIP
        {
            get => _flowCarrierGlobalParam.TCPIPClientIP;
            set
            {
                if (_flowCarrierGlobalParam.TCPIPClientIP == value)
                    return;
                _flowCarrierGlobalParam.TCPIPClientIP = value;
                this.RaisePropertyChanged(x => x.ClientIP);
            }
        }

        public int ClientPort
        {
            get => _flowCarrierGlobalParam.TCPIPClientPort;
            set
            {
                if (_flowCarrierGlobalParam.TCPIPClientPort == value)
                    return;
                _flowCarrierGlobalParam.TCPIPClientPort = value;
                this.RaisePropertyChanged(x => x.ClientPort);
            }
        }

        public int OnlyDataServerPort
        {
            get => _flowCarrierGlobalParam.OnlyDataTCPIPServerPort;
            set
            {
                if (_flowCarrierGlobalParam.OnlyDataTCPIPServerPort == value)
                    return;
                _flowCarrierGlobalParam.OnlyDataTCPIPServerPort = value;
                this.RaisePropertyChanged(x => x.OnlyDataServerPort);
            }
        }

        public int OnlyDataClientPort
        {
            get => _flowCarrierGlobalParam.OnlyDataTCPIPClientPort;
            set
            {
                if (_flowCarrierGlobalParam.OnlyDataTCPIPClientPort == value)
                    return;
                _flowCarrierGlobalParam.OnlyDataTCPIPClientPort = value;
                this.RaisePropertyChanged(x => x.OnlyDataClientPort);
            }
        }


        public int UpperType
        {
            get => (int)_flowCarrierGlobalParam.UpperDeliveryType;
            set
            {
                if ((int)_flowCarrierGlobalParam.UpperDeliveryType == value)
                    return;
                _flowCarrierGlobalParam.UpperDeliveryType = (MachineDeliveryTypeEm)value;
                this.RaisePropertyChanged(x => x.UpperType);
                this.RaisePropertyChanged(x => x.TCPIPGroupVisible);
            }
        }

        public int LowerType
        {
            get => (int)_flowCarrierGlobalParam.LowerDeliveryType;
            set
            {
                if ((int)_flowCarrierGlobalParam.LowerDeliveryType == value)
                    return;
                _flowCarrierGlobalParam.LowerDeliveryType = (MachineDeliveryTypeEm)value;
                this.RaisePropertyChanged(x => x.LowerType);
                this.RaisePropertyChanged(x => x.TCPIPGroupVisible);
            }
        }


        public bool TCPIPGroupVisible
        {
            get
            {
                if (_flowCarrierGlobalParam.UpperDeliveryType != MachineDeliveryTypeEm.TCPIP &&
                   _flowCarrierGlobalParam.LowerDeliveryType != MachineDeliveryTypeEm.TCPIP)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool TCPIPUseXml
        {
            get => _flowCarrierGlobalParam.TCPIPUseXml;
            set
            {
                _flowCarrierGlobalParam.TCPIPUseXml = value;
                this.RaisePropertyChanged(x => x.TCPIPUseXml);
            }
        }

        public bool TCPIPUseInterLock
        {
            get => _flowCarrierGlobalParam.UseInterLock;
            set
            {
                _flowCarrierGlobalParam.UseInterLock = value;
                this.RaisePropertyChanged(x => x.TCPIPUseInterLock);
            }
        }

        public bool EnableOnlyDataTCPIPServer
        {
            get => _flowCarrierGlobalParam.EnableOnlyDataTCPIPServer;
            set
            {
                _flowCarrierGlobalParam.EnableOnlyDataTCPIPServer = value;
                this.RaisePropertyChanged(x => x.EnableOnlyDataTCPIPServer);
            }
        }

        public bool EnableOnlyDataTCPIPClient
        {
            get => _flowCarrierGlobalParam.EnableOnlyDataTCPIPClient;
            set
            {
                _flowCarrierGlobalParam.EnableOnlyDataTCPIPClient = value;
                this.RaisePropertyChanged(x => x.EnableOnlyDataTCPIPClient);
            }
        }


        public void Init(FlowCarrierGlobalParam param)
        {
            _flowCarrierGlobalParam = param;
        }
    }
}
