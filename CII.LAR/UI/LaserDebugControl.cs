using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.Protocol;
using CII.LAR.Commond;

namespace CII.LAR.UI
{
    public partial class LaserDebugControl : Form
    {
        private SerialPortCommunication serialPortCom = SerialPortCommunication.GetInstance();

        public LaserDebugControl()
        {
            InitializeComponent();
            serialPortCom.SerialDataReceivedHandler += SerialDataReceivedHandler;
        }
        private bool redLaserOpen = false;
        private void SerialDataReceivedHandler(LaserBaseResponse baseResponse)
        {
            if (baseResponse != null)
            {
                LaserC01Response c01r = baseResponse as LaserC01Response;
                if (c01r != null)
                {
                    if (c01r.Flag == 1920)
                    {
                        //红光关闭，则强制开启
                        this.btn70.Text = "Open";
                        redLaserOpen = false;
                    }
                    else if (c01r.Flag == 1664)
                    {
                        this.btn70.Text = "Closed";
                        redLaserOpen = true;
                    }
                }
                LaserC09Response c09r = baseResponse as LaserC09Response;
                if (c09r != null)
                {
                    this.slider.Value = (int)c09r.Current;
                }
            }
        }

        private void LaserDebugControl_Load(object sender, EventArgs e)
        {
            if (Program.SysConfig.LaserPort != null)
                this.lblComName.Text = Program.SysConfig.LaserPort;
            this.laserStatus.Text = serialPortCom.SerialPort.IsOpen ? "Connected" : "Not connected";
            if (serialPortCom.SerialPort.IsOpen)  CheckLaserStatus();
            CheckRedLaserCurrent();
        }

        private void CheckRedLaserCurrent()
        {
            var c09 = new LaserC09Request();
            var bytes = serialPortCom.Encode(c09);
            serialPortCom.SendData(bytes);
        }
        private void CheckLaserStatus()
        {
            LaserC01Request c01 = new LaserC01Request();
            var bytes = serialPortCom.Encode(c01);
            serialPortCom.SendData(bytes);
        }

        private void SendEnableLaserData()
        {
            LaserC70Request c70 = new LaserC70Request();
            var bytes = serialPortCom.Encode(c70);
            serialPortCom.SendData(bytes);
        }

        private void btn70_Click(object sender, EventArgs e)
        {
            redLaserOpen = !redLaserOpen;
            SendEnableLaserData();
            this.btn70.Text = redLaserOpen ?  "Closed" : "Open";
            //CheckLaserStatus();
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            this.slider.Text = slider.Value.ToString();
            if (serialPortCom.SerialPort.IsOpen) SetRedLaserCurrent();
        }

        /// <summary>
        /// 设置红光电流
        /// </summary>
        private void SetRedLaserCurrent()
        {
            try
            {
                var c75 = new LaserC75Request(this.slider.Value);
                var bps = c75.Encode();
                List<byte[]> bytes = new List<byte[]>();
                foreach (var b in bps)
                {
                    var data = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(b);
                    bytes.Add(data);
                }
                serialPortCom.SendData(bytes);
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger<LaserDebugControl>().Error(ex.Message);
                LogHelper.GetLogger<LaserDebugControl>().Error(ex.StackTrace);
            }
        }
        private void slider_MouseUp(object sender, MouseEventArgs e)
        {
            //LaserProtocolFactory.GetInstance().SendMessage(new LaserC75Request(this.slider.Value));
        }
    }
}
