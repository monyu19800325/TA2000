using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TA2000Modules
{
    [POCOViewModel()]
    public class FlowCarrierProductSettingViewModel
    {
        public FlowCarrierProductParam FlowCarrierProductParam { get; set; } = new FlowCarrierProductParam();
        public List<string> VelList { get; set; } = new List<string>();

        public int LoadTimeout
        {
            get => FlowCarrierProductParam.LoadTimeout;
            set
            {
                FlowCarrierProductParam.LoadTimeout = value;
                this.RaisePropertyChanged(x => x.LoadTimeout);
            }
        }

        public int UnloadTimeout
        {
            get => FlowCarrierProductParam.UnloadTimeout;
            set
            {
                FlowCarrierProductParam.UnloadTimeout = value;
                this.RaisePropertyChanged(x => x.UnloadTimeout);
            }
        }

        public int CylinderTimeout
        {
            get => FlowCarrierProductParam.CylinderTimeout;
            set
            {
                FlowCarrierProductParam.CylinderTimeout = value;
                this.RaisePropertyChanged(x => x.CylinderTimeout);
            }
        }

        public int DecTimeout
        {
            get => FlowCarrierProductParam.DecTimeout;
            set
            {
                FlowCarrierProductParam.DecTimeout = value;
                this.RaisePropertyChanged(x => x.DecTimeout);
            }
        }

        public int StopTimeout
        {
            get => FlowCarrierProductParam.StopTimeout;
            set
            {
                FlowCarrierProductParam.StopTimeout = value;
                this.RaisePropertyChanged(x => x.StopTimeout);
            }
        }

        public string Vel
        {
            get => FlowCarrierProductParam.FlowVel.ToString();
            set
            {
                FlowCarrierProductParam.FlowVel = (MoveVelEm)Enum.Parse(typeof(MoveVelEm), value);
                this.RaisePropertyChanged(x => x.Vel);
            }
        }


        //public bool IsPick
        //{
        //    get => Module.ProductParam.IsPick;
        //    set
        //    {
        //        Module.ProductParam.IsPick = value;
        //        this.RaisePropertyChanged(x => x.IsPick);
        //    }
        //}

        #region Bypass SV/VC for Auto Modle 
        public bool TopSVPick
        {
            get => FlowCarrierProductParam.TopSV_Bypass_Pick;
            set
            {
                FlowCarrierProductParam.TopSV_Bypass_Pick = value;
                this.RaisePropertyChanged(x => x.TopSVPick);
            }
        }

        public bool GuideSVPick
        {
            get => FlowCarrierProductParam.GuideSV_Bypass_Pick;
            set
            {
                FlowCarrierProductParam.GuideSV_Bypass_Pick = value;
                this.RaisePropertyChanged(x => x.GuideSVPick);
            }
        }

        public bool StopSVPick
        {
            get => FlowCarrierProductParam.StopSV_Bypass_Pick;
            set
            {
                FlowCarrierProductParam.StopSV_Bypass_Pick = value;
                this.RaisePropertyChanged(x => x.StopSVPick);
            }
        }

        public bool VCPick
        {
            get => FlowCarrierProductParam.VC_Bypass_Pick;
            set
            {
                FlowCarrierProductParam.VC_Bypass_Pick = value;
                this.RaisePropertyChanged(x => x.VCPick);
            }
        }

        public bool VCCheckPick
        {
            get => FlowCarrierProductParam.VCCheck_Bypass_Pick;
            set
            {
                FlowCarrierProductParam.VCCheck_Bypass_Pick = value;
                this.RaisePropertyChanged(x => x.VCCheckPick);
            }
        }


        #endregion



        public void Init(FlowCarrierProductParam param)
        {
            FlowCarrierProductParam = param;
            VelList.Clear();
            foreach (var item in Enum.GetNames(typeof(MoveVelEm)))
            {
                VelList.Add(item);
            }
        }


        public void BoatBypass()
        {
            TopSVPick = true;
            //GuideSVPick = true;
            //StopSVPick = true;
            VCPick = true;
            VCCheckPick = true;
        }

    }
}
