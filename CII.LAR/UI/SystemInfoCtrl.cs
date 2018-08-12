using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.Commond;
using CII.LAR.Protocol;

namespace CII.LAR.UI
{
    public partial class SystemInfoCtrl : BaseCtrl
    {
        private SerialPortManager serialPortCom;
        public SystemInfoCtrl()
        {
            resources = new ComponentResourceManager(typeof(SystemInfoCtrl));
            this.CtrlType = CtrlType.SystemInoCtrl;
            InitializeComponent();

            serialPortCom = SerialPortManager.GetInstance();

        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrSystemInfoTitle;
            this.Invalidate();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Program.EntryForm.ShowBaseCtrl(true, CtrlType.SettingCtrl);
        }

        private void CheckLaserInfo()
        {
            LaserC00Request c00 = new LaserC00Request();
            var bytes = serialPortCom.Encode(c00);
            byte[] recData = serialPortCom.SendData(bytes);
            if (recData != null)
            {
                LaserBaseResponse baseResponse = serialPortCom.LaserBaseResponse(bytes, recData);
                if (baseResponse != null)
                {
                    LaserC00Response c00r = baseResponse as LaserC00Response;
                    if (c00r != null)
                    {
                        lblLaserVersion.Text = c00r.VersionNumber.ToString();
                        lblWorkingHour.Text = string.Format("{0} : {1}", c00r.Hour, c00r.Month);
                    }
                }
            }
        }

        public void UpdateStatus()
        {
            this.lblLaserStatus.Text = Program.SysConfig.LaserPortConected ? Properties.Resources.StrConnected : Properties.Resources.StrDisconnect;
            this.lblMotorStatus.Text = Program.SysConfig.MotorPortConected ? Properties.Resources.StrConnected : Properties.Resources.StrDisconnect;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                CheckLaserInfo();
            }
        }
    }
}
