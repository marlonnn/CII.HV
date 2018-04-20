using CII.LAR.Commond;
using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    /// <summary>
    /// 串口调试窗口
    /// </summary>
    public partial class SerialPortDebugForm : Form, IView
    {
        private IController controller;
        public IController Controller
        {
            get { return this.controller; }
            set { this.controller = value; }
        }

        private LaserProtocolFactory laserProtocolFactory;
        public LaserProtocolFactory LaserProtocolFactory
        {
            get { return this.laserProtocolFactory; }
            private set { this.laserProtocolFactory = value; }
        }

        private MotorProtocolFactory motorProtocolFactory;
        public MotorProtocolFactory MotorProtocolFactory
        {
            get { return this.motorProtocolFactory; }
            private set { this.motorProtocolFactory = value; }
        }

        public SerialPortDebugForm()
        {
            InitializeComponent();
            InitializeLaserCOMCombox();
            InitializeLaserProtocolFactory();
            this.Load += SerialPortDebugForm_Load;
            this.FormClosing += SerialPortDebugForm_FormClosing;
        }

        private void SerialPortDebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.autoReceiverTimer.Enabled = false;
            this.autoSendTimer.Enabled = false;
        }

        private void InitializeLaserProtocolFactory()
        {
            LaserProtocolFactory = LaserProtocolFactory.GetInstance();
        }

        private void SerialPortDebugForm_Load(object sender, EventArgs e)
        {
            this.autoReceiverTimer.Enabled = true;
        }

        /// <summary>
        /// Initialize serial port information
        /// </summary>
        private void InitializeLaserCOMCombox()
        {
            //BaudRate
            laserBaudRateCbx.Items.Add(4800);
            laserBaudRateCbx.Items.Add(9600);
            laserBaudRateCbx.Items.Add(19200);
            laserBaudRateCbx.Items.Add(38400);
            laserBaudRateCbx.Items.Add(57600);
            laserBaudRateCbx.Items.Add(115200);
            laserBaudRateCbx.Items.ToString();
            //get 9600 print in text
            laserBaudRateCbx.Text = laserBaudRateCbx.Items[1].ToString();

            //Data bits
            laserDataBitsCbx.Items.Add(7);
            laserDataBitsCbx.Items.Add(8);
            //get the 8bit item print it in the text 
            laserDataBitsCbx.Text = laserDataBitsCbx.Items[1].ToString();

            //Stop bits
            laserStopBitsCbx.Items.Add("One");
            laserStopBitsCbx.Items.Add("OnePointFive");
            laserStopBitsCbx.Items.Add("Two");
            //get the One item print in the text
            laserStopBitsCbx.Text = laserStopBitsCbx.Items[0].ToString();

            //Parity
            laserParityCbx.Items.Add("None");
            laserParityCbx.Items.Add("Even");
            laserParityCbx.Items.Add("Mark");
            laserParityCbx.Items.Add("Odd");
            laserParityCbx.Items.Add("Space");
            //get the first item print in the text
            laserParityCbx.Text = laserParityCbx.Items[0].ToString();

            //Handshaking
            laserHandshakingcbx.Items.Add("None");
            laserHandshakingcbx.Items.Add("XOnXOff");
            laserHandshakingcbx.Items.Add("RequestToSend");
            laserHandshakingcbx.Items.Add("RequestToSendXOnXOff");
            laserHandshakingcbx.Text = laserHandshakingcbx.Items[0].ToString();

            //Com Ports
            string[] ArrayComPortsNames = SerialPort.GetPortNames();
            if (ArrayComPortsNames.Length == 0)
            {
                laserOpenCloseSpbtn.Enabled = false;
            }
            else
            {
                Array.Sort(ArrayComPortsNames);
                for (int i = 0; i < ArrayComPortsNames.Length; i++)
                {
                    laserComListCbx.Items.Add(ArrayComPortsNames[i]);
                }
                laserComListCbx.Text = ArrayComPortsNames[0];
                laserOpenCloseSpbtn.Enabled = true;
            }
        }

        private void autoSendTimer_Tick(object sender, EventArgs e)
        {

        }

        private void autoSendcbx_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSendcbx.Checked)
            {
                autoSendTimer.Enabled = true;
                autoSendTimer.Interval = int.Parse(sendIntervalTimetbx.Text);
                autoSendTimer.Start();

                //disable send botton and textbox
                sendIntervalTimetbx.Enabled = false;
                sendtbx.ReadOnly = true;
                sendbtn.Enabled = false;
            }
            else
            {
                autoSendTimer.Enabled = false;
                autoSendTimer.Stop();

                //enable send botton and textbox
                sendIntervalTimetbx.Enabled = true;
                sendtbx.ReadOnly = false;
                sendbtn.Enabled = true;
            }
        }

        public void SetController(IController controller)
        {
            this.controller = controller;
        }

        public void LaserOpenComEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(LaserOpenComEvent), sender, e);
                return;
            }

            if (e.isOpend)  //Open successfully
            {
                laserOpenCloseSpbtn.Text = "Close";
                sendbtn.Enabled = true;
                autoSendcbx.Enabled = true;

                laserComListCbx.Enabled = false;
                laserBaudRateCbx.Enabled = false;
                laserDataBitsCbx.Enabled = false;
                laserStopBitsCbx.Enabled = false;
                laserParityCbx.Enabled = false;
                laserHandshakingcbx.Enabled = false;

                if (autoSendcbx.Checked)
                {
                    autoSendTimer.Start();
                    sendtbx.ReadOnly = true;
                }
            }
            else    //Open failed
            {
                sendbtn.Enabled = false;
                autoSendcbx.Enabled = false;
            }
        }

        public void LaserCloseComEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(LaserCloseComEvent), sender, e);
                return;
            }
            if (!e.isOpend) //close successfully
            {
                laserStatus.Text = laserComListCbx.Text + " Closed";
                laserOpenCloseSpbtn.Text = "Open";

                laserComListCbx.Enabled = true;
                laserBaudRateCbx.Enabled = true;
                laserDataBitsCbx.Enabled = true;
                laserStopBitsCbx.Enabled = true;
                laserParityCbx.Enabled = true;
                laserHandshakingcbx.Enabled = true;
            }
        }

        private void laserOpenCloseSpbtn_Click(object sender, EventArgs e)
        {
            if (laserOpenCloseSpbtn.Text == "Open")
            {
                if (controller != null)
                {
                    controller.OpenLaserSerialPort(laserComListCbx.Text, laserBaudRateCbx.Text,
                        laserDataBitsCbx.Text, laserStopBitsCbx.Text, laserParityCbx.Text, laserHandshakingcbx.Text);
                }
            }
            else
            {
                if (controller != null)
                {
                    controller.CloseLaserSerialPort();
                }
            }
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC00Request());
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC01Request());
        }

        private void btn03_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC03Request());
        }

        private void btn04_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC04Request());
        }

        private void btn05_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC05Request());
        }

        private void btn06_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC06Request());
        }

        private void btn07_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC07Request());
        }

        private void btn08_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC08Request());
        }

        private void btn0B_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC0BRequest());
        }

        private void btn75_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC75Request(3));
        }

        private void btn09_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC09Request());
        }

        private void btn74_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC74Request(0x01));
        }

        private void btn0C_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC0CRequest());
        }

        private void btn71_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC71Request());
        }

        private void btn73_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC73Request(100));
        }

        private void btn72_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC72Request(20));
        }

        private void autoReceiverTimer_Tick(object sender, EventArgs e)
        {
            if (LaserProtocolFactory.GetInstance().RxQueue != null)
            {
                List<Original> bytes = LaserProtocolFactory.GetInstance().RxQueue.PopAll();
                if (bytes != null && bytes.Count > 0)
                {
                    foreach (var o in bytes)
                    {
                        OriginalBytes originalBytes = o as OriginalBytes;
                        if (originalBytes != null)
                        {
                            if (receivetbx.Text.Length > 0)
                            {
                                receivetbx.AppendText("-");
                            }
                            receivetbx.AppendText(IController.Bytes2Hex(originalBytes.Data));
                        }
                    }
                }
            }

            if (MotorProtocolFactory.GetInstance().RxQueue != null)
            {
                List<Original> bytes = MotorProtocolFactory.GetInstance().RxQueue.PopAll();
                if (bytes != null && bytes.Count > 0)
                {
                    foreach (var o in bytes)
                    {
                        OriginalBytes originalBytes = o as OriginalBytes;
                        if (originalBytes != null)
                        {
                            if (receivetbx.Text.Length > 0)
                            {
                                receivetbx.AppendText("-");
                            }
                            receivetbx.AppendText(IController.Bytes2Hex(originalBytes.Data));
                        }
                    }
                }
            }
        }

        private void clearReceivebtn_Click(object sender, EventArgs e)
        {
            receivetbx.Clear();
        }

        private void btn70_Click(object sender, EventArgs e)
        {
            LaserProtocolFactory.SendMessage(new LaserC70Request());
        }
    }
}
