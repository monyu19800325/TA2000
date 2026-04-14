using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM;
using HTA.Com.TCPIP2;
using HTAMachine.Machine;
using HTAMachine.Module;
using HTAMachine.Module.DockPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    /// <summary>
    /// 顯示在 CDI 2.0
    /// </summary>
    [RegisterCdiDocker]
    public partial class ConnectionStateForm : DevExpress.XtraEditors.XtraUserControl, IInjectModule, IMultilingual
    {
        FlowCarrierModule _module; //宣告Flow Carrier Module
        /// <summary>
        /// 建構子
        /// </summary>
        public ConnectionStateForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加入 FlowCarrier Module  **觸發機制待詢問**
        /// </summary>
        /// <param name="module"></param>
        public void InjectModule(IModule module)
        {
            _module = (FlowCarrierModule)module;
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        /// <summary>
        /// 初始化Bindings
        /// </summary>
        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ConnectionStateViewModel>();
            fluent.ViewModel.Init(_module);

            fluent.SetBinding(LB_UpperX, x => x.BackColor, m => m.UpperX);
            fluent.SetBinding(LB_UpperY, x => x.BackColor, m => m.UpperY);
            fluent.SetBinding(LB_LowerX, x => x.BackColor, m => m.LowerX);
            fluent.SetBinding(LB_LowerY, x => x.BackColor, m => m.LowerY);
            fluent.SetBinding(LB_ServerState, x => x.BackColor, m => m.ServerState);
            fluent.SetBinding(lbServerNumber, x => x.Text, m => m.ServerStateText);
            fluent.SetBinding(LB_ClientState, x => x.BackColor, m => m.ClientState);

            Disposed += fluent.ViewModel.OnClose;
        }

        public void SetLanguage(ComponentResourceManager resources)
        {
            System.ComponentModel.ComponentResourceManager resources2 = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionStateForm));
            resources2.ApplyResources(this, "$this");
            SetLang(this.Controls, resources2);
            this.Refresh();
        }

        public void SetLang(Control.ControlCollection ctrls, ComponentResourceManager resource)
        {
            foreach (Control ctrl in ctrls)
            {
                resource.ApplyResources(ctrl, ctrl.Name);
                if (ctrl.HasChildren)
                    SetLang(ctrl.Controls, resource);
            }
        }
    }

    [POCOViewModel()]
    public class ConnectionStateViewModel
    {
        public IDispatcherService DispatcherService => this.GetService<IDispatcherService>();
        public virtual Color UpperX { get; set; }
        public virtual Color LowerX { get; set; }
        public virtual Color UpperY { get; set; }
        public virtual Color LowerY { get; set; }
        public virtual Color ServerState { get; set; }
        public virtual string ServerStateText { get; set; } = "0";
        public virtual Color ClientState { get; set; }
        private FlowCarrierModule _module { get; set; }
        private bool _isClose { get; set; }

        public void Init(FlowCarrierModule module)
        {
            _module = module;
            UpperX = _module.X064012_SMEMA_上位1_通知已放料信號.CheckIO() ? Color.Green : Color.Red;
            UpperY = _module.Y065012_SMEMA_上位1_通知上位_Ready信號.CheckIO() ? Color.Green : Color.Red;
            LowerX = _module.X064013_SMEMA_下位1_通知Ready信號.CheckIO() ? Color.Green : Color.Red;
            LowerY = _module.Y065013_SMEMA_下位1_通知已放料信號.CheckIO() ? Color.Green : Color.Red;
            ServerState = TCPIPTool.TcpServer.IsOnline ? Color.Orange : Color.Red;
            if (TCPIPTool.TcpServer.IsOnline && TCPIPTool.TcpServer.ListClient().Count() > 0)
            {
                ServerState = Color.Green;
                ServerStateText = TCPIPTool.TcpServer.ListClient().Count().ToString();
            }
            ClientState = TCPIPTool.TcpClient.IsConnect ? Color.Green : Color.Red;
            IOChanged();
        }

        public void IOChanged()
        {
            _module.X064012_SMEMA_上位1_通知已放料信號.OnIOChange += (s, e) =>
            {
                DispatcherService.Invoke(() =>
                {
                    UpperX = _module.X064012_SMEMA_上位1_通知已放料信號.CheckIO() ? Color.Green : Color.Red;
                });
            };
            _module.Y065012_SMEMA_上位1_通知上位_Ready信號.OnIOChange += (s, e) =>
            {
                DispatcherService.Invoke(() =>
                {
                    UpperY = _module.Y065012_SMEMA_上位1_通知上位_Ready信號.CheckIO() ? Color.Green : Color.Red;
                });
            };
            _module.X064013_SMEMA_下位1_通知Ready信號.OnIOChange += (s, e) =>
            {
                DispatcherService.Invoke(() =>
                {
                    LowerX = _module.X064013_SMEMA_下位1_通知Ready信號.CheckIO() ? Color.Green : Color.Red;
                });
            };
            _module.Y065013_SMEMA_下位1_通知已放料信號.OnIOChange += (s, e) =>
            {
                DispatcherService.Invoke(() =>
                {
                    LowerY = _module.Y065013_SMEMA_下位1_通知已放料信號.CheckIO() ? Color.Green : Color.Red;
                });
            };

            Thread threadServer = new Thread(CheckServerState);
            Thread threadClient = new Thread(CheckClientState);

            threadServer.Start();
            threadClient.Start();
        }


        public void CheckServerState()
        {
            while (!_isClose)
            {
                DispatcherService.Invoke(() =>
                {
                    ServerState = TCPIPTool.TcpServer.IsOnline ? Color.Orange : Color.Red;
                    if (TCPIPTool.TcpServer.IsOnline)
                    {
                        if (TCPIPTool.TcpServer.ListClient().Count() > 0)
                            ServerState = Color.Green;
                        ServerStateText = TCPIPTool.TcpServer.ListClient().Count().ToString();
                    }
                    else
                    {
                        ServerStateText = "0";
                    }
                });
                SpinWait.SpinUntil(() => false, 1000);
            }
        }

        public void CheckClientState()
        {
            while (!_isClose)
            {
                DispatcherService.Invoke(() =>
                {
                    ClientState = TCPIPTool.TcpClient.IsConnect ? Color.Green : Color.Red;
                });
                SpinWait.SpinUntil(() => false, 1000);
            }
        }


        public void OnClose(object sender,EventArgs e)
        {
            _isClose = true;
        }
    }
}
