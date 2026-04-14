using FlowCarrierModule;
using HTAMachine.Machine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA2000Modules
{
    public partial class TCPIPComForm : DevExpress.XtraEditors.XtraForm, IMultilingual
    {
        public TCPIPComForm(FlowCarrierModule module)
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings(module.Param);
        }

        void InitializeBindings(FlowCarrierGlobalParam param)
        {
            var fluent = mvvmContext1.OfType<TCPIPComViewModel>();

            fluent.ViewModel.Init(param);

            fluent.SetBinding(TE_IP, x => x.Text, x => x.TCPIPClientIP);
            fluent.SetBinding(TE_Port, x => x.Text, x => x.TCPIPClientPort);
            fluent.SetBinding(TE_ServerPort, x => x.Text, x => x.TCPIPServerPort);
            fluent.SetBinding(LB_ServerLight, x => x.BackColor, x => x.ServerLight);
            fluent.SetBinding(LB_ClientLight, x => x.BackColor, x => x.ClientLight);
            fluent.SetBinding(lbServerNumber, x => x.Text, m => m.ServerStateText);

            fluent.BindCommand(Btn_Connect, x => x.Connect());
            fluent.BindCommand(Btn_DisConnect, x => x.Disconnect());
            fluent.BindCommand(Btn_ServerOnline, x => x.ServerOnline());
            fluent.BindCommand(Btn_Offline, x => x.ServerOffline());
            fluent.BindCommand(Btn_ServerReplyGotBoat, x => x.ServerReplyGotBoat());
            fluent.BindCommand(Btn_ServerSendRun, x => x.ServerSendRun());
            fluent.BindCommand(Btn_ServerSendStop, x => x.ServerSendStop());
            fluent.BindCommand(Btn_ClientDeliveryBoat, x => x.ClientDeliveryBoat());
            fluent.BindCommand(Btn_ClientSendRun, x => x.ClientSendRun());
            fluent.BindCommand(Btn_ClientSendStop, x => x.ClientSendStop());
            fluent.BindCommand(Btn_ReplyDeliveryOK, x => x.ServerReplyDeliveryBoatOK);
            fluent.BindCommand(Btn_ReadNewDeliveryBoat, x => x.ReadNewDeliveryBoat());

            FormClosing += fluent.ViewModel.OnFormClose;

            TE_IP.Enabled = false;
            TE_Port.Enabled = false;
            TE_ServerPort.Enabled = false;
        }

        public void SetLanguage(ComponentResourceManager resources)
        {
            System.ComponentModel.ComponentResourceManager resources2 = new System.ComponentModel.ComponentResourceManager(typeof(TCPIPComForm));
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
}
