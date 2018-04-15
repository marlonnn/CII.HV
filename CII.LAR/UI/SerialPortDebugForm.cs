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
            InitializeMotorCOMCombox();
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
            MotorProtocolFactory = MotorProtocolFactory.GetInstance();
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
        private void InitializeMotorCOMCombox()
        {
            //laserBaudRateCbx
            //BaudRate
            motorBaudRateCbx.Items.Add(4800);
            motorBaudRateCbx.Items.Add(9600);
            motorBaudRateCbx.Items.Add(19200);
            motorBaudRateCbx.Items.Add(38400);
            motorBaudRateCbx.Items.Add(57600);
            motorBaudRateCbx.Items.Add(115200);
            motorBaudRateCbx.Items.ToString();
            //get 9600 print in text
            motorBaudRateCbx.Text = motorBaudRateCbx.Items[1].ToString();

            //Data bits
            motorDataBitsCbx.Items.Add(7);
            motorDataBitsCbx.Items.Add(8);
            //get the 8bit item print it in the text 
            motorDataBitsCbx.Text = motorDataBitsCbx.Items[1].ToString();

            //Stop bits
            motorStopBitsCbx.Items.Add("One");
            motorStopBitsCbx.Items.Add("OnePointFive");
            motorStopBitsCbx.Items.Add("Two");
            //get the One item print in the text
            motorStopBitsCbx.Text = motorStopBitsCbx.Items[0].ToString();

            //Parity
            motorParityCbx.Items.Add("None");
            motorParityCbx.Items.Add("Even");
            motorParityCbx.Items.Add("Mark");
            motorParityCbx.Items.Add("Odd");
            motorParityCbx.Items.Add("Space");
            //get the first item print in the text
            motorParityCbx.Text = motorParityCbx.Items[0].ToString();

            //Handshaking
            motorHandshakingcbx.Items.Add("None");
            motorHandshakingcbx.Items.Add("XOnXOff");
            motorHandshakingcbx.Items.Add("RequestToSend");
            motorHandshakingcbx.Items.Add("RequestToSendXOnXOff");
            motorHandshakingcbx.Text = motorHandshakingcbx.Items[0].ToString();

            //Com Ports
            string[] ArrayComPortsNames = SerialPort.GetPortNames();
            if (ArrayComPortsNames.Length == 0)
            {
                motorStatus.Text = "No COM found !";
                motorOpenCloseSpbtn.Enabled = false;
            }
            else
            {
                Array.Sort(ArrayComPortsNames);
                for (int i = 0; i < ArrayComPortsNames.Length; i++)
                {
                    motorComListCbx.Items.Add(ArrayComPortsNames[i]);
                }
                motorComListCbx.Text = ArrayComPortsNames[0];
                motorOpenCloseSpbtn.Enabled = true;
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

        public void MotorOpenComEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(MotorOpenComEvent), sender, e);
                return;
            }
            if (e.isOpend)
            {
                motorStatus.Text = motorComListCbx.Text + " Opend";
                motorOpenCloseSpbtn.Text = "Close";

                motorComListCbx.Enabled = false;
                motorBaudRateCbx.Enabled = false;
                motorDataBitsCbx.Enabled = false;
                motorStopBitsCbx.Enabled = false;
                motorParityCbx.Enabled = false;
                motorHandshakingcbx.Enabled = false;

            }
            else
            {
                motorStatus.Text = "Open failed !";
            }
        }

        public void MotorCloseComEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(MotorCloseComEvent), sender, e);
                return;
            }
            if (!e.isOpend) //close successfully
            {
                motorStatus.Text = laserComListCbx.Text + " Closed";
                motorOpenCloseSpbtn.Text = "Open";

                motorComListCbx.Enabled = true;
                motorBaudRateCbx.Enabled = true;
                motorDataBitsCbx.Enabled = true;
                motorStopBitsCbx.Enabled = true;
                motorParityCbx.Enabled = true;
                motorHandshakingcbx.Enabled = true;
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

        private void motorOpenCloseSpbtn_Click(object sender, EventArgs e)
        {
            if (motorOpenCloseSpbtn.Text == "Open")
            {
                if (controller != null)
                {
                    //controller.OpenMotorSerialPort(motorComListCbx.Text, motorBaudRateCbx.Text, motorDataBitsCbx.Text, motorStopBitsCbx.Text, motorParityCbx.Text, motorHandshakingcbx.Text);
                    //motorOpenCloseSpbtn.Text = "Close";
                    //this.motorStatus.Text = "Conected";
                }
            }
            else
            {
                if (controller != null)
                {
                    //controller.CloseMotorSerialPort();
                    //motorOpenCloseSpbtn.Text = "Open";
                    //this.motorStatus.Text = "Not Conected";
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

        private void btnC60_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x66);
            request.ControlSelection = 0x61;
            request.ControlMode61 = 0x01;
            request.Direction61 = 0x00;
            request.TotalSteps61 = 50;
            MotorProtocolFactory.SendMessage(request);
        }

        private void btn6062_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x66);
            request.ControlSelection = 0x62;
            request.ControlMode62 = 0x01;
            request.Direction62 = 0x00;
            request.TotalSteps62 = 100;
            MotorProtocolFactory.SendMessage(request);
        }

        private void btn6060_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x66);
            request.ControlSelection = 0x60;
            request.ControlMode61 = 0x01;
            request.Direction61 = 0x00;
            request.TotalSteps61 = 50;

            request.ControlMode62 = 0x01;
            request.Direction62 = 0x00;
            request.TotalSteps62 = 100;
            MotorProtocolFactory.SendMessage(request);
        }

        private void btn6055_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x60;
            MotorProtocolFactory.SendMessage(request);
        }

        private void btn6055A0_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0xA0;
            MotorProtocolFactory.SendMessage(request);
        }

        private void btn605500_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x00;
            MotorProtocolFactory.SendMessage(request);
        }

        private void btn605562_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x62;
            MotorProtocolFactory.SendMessage(request);
        }

        private void btn605561_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x61;
            MotorProtocolFactory.SendMessage(request);
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
