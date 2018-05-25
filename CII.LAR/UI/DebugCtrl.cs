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
using CII.LAR.Algorithm;

namespace CII.LAR.UI
{
    public partial class DebugCtrl : BaseCtrl
    {
        public DebugCtrl()
        {
            InitializeComponent();
        }

        public void UpdateSteps(int s1, int s2)
        {
            this.m1Steps.Text = s1.ToString();
            this.m2Steps.Text = s2.ToString();
        }

        public void UpdateResponseCode(string code)
        {
            this.responseCode.Text = code;
            this.lblMatrix.Text = Program.SysConfig.LaserConfig.FinalMatrix.ToString();
            this.lblMousePosition.Text = Program.SysConfig.Point.ToString();
        }

        public void UpdateMoveStep(int x, byte ox, int y, byte oy)
        {
            this.motor1Steps.Text = x.ToString();
            this.motor1Origination.Text = ox.ToString();
            this.motor2Steps.Text = y.ToString();
            this.motor2Origination.Text = oy.ToString();
        }

        public void UpdateLaserStatus()
        {
            this.lblLaserStatus.Text = Program.SysConfig.LaserPortConected ? "已连接" : "断开";
        }

        protected override void closeButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Enabled = false;
        }

    }
}
