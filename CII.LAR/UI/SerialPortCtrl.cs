using CII.Ins.Model.GlobalConfig;
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
    public partial class SerialPortCtrl : BaseCtrl, IView
    {
        private Timer timer;
        private IController controller;

        public SerialPortCtrl()
        {
            resources = new ComponentResourceManager(typeof(SerialPortCtrl));
            this.ShowIndex = 1;
            this.CtrlType = CtrlType.SerialPort;
            InitializeComponent();
            InitializeLaserCOMCombox();
            InitializeMotorCOMCombox();

            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //动态获取激光器和电机串口
            //Com Ports
            string[] ArrayComPortsNames = SerialPortHelper.GetHelper().GetPorts();
            if (ArrayComPortsNames != null && ArrayComPortsNames.Length > 0)
            {
                Array.Sort(ArrayComPortsNames);
                for (int i = 0; i < ArrayComPortsNames.Length; i++)
                {
                    if (!CheckHasSameData(motorComListCbx, ArrayComPortsNames[i])) motorComListCbx.Items.Add(ArrayComPortsNames[i]);
                    if (!CheckHasSameData(laserComListCbx, ArrayComPortsNames[i])) laserComListCbx.Items.Add(ArrayComPortsNames[i]);
                }
            }
        }

        private bool CheckHasSameData(ComboBox combox, string portName)
        {
            bool hasPort = false;
            if (combox != null && combox.Items != null && combox.Items.Count > 0)
            {
                foreach (var item in combox.Items)
                {
                    if (item.ToString() == portName)
                    {
                        hasPort = true;
                        break;
                    }
                }
            }
            return hasPort;
        }

        private void InitializeLaserCOMCombox()
        {
            //laserBaudRateCbx
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
                laserStatus.Text = "No COM found !";
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
            //get 115200 print in text
            motorBaudRateCbx.Text = motorBaudRateCbx.Items[5].ToString();

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
            string[] ArrayComPortsNames = SerialPortHelper.GetHelper().GetPorts();
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

        protected override void OnVisibleChanged(EventArgs e)
        {
            this.timer.Enabled = this.Visible;
        }

        public delegate void OpenBtnClick(string btnName);
        public OpenBtnClick OpenBtnClickHandler;

        private void laserOpenCloseSpbtn_Click(object sender, EventArgs e)
        {
            if (laserOpenCloseSpbtn.Text == "Open")
            {
                if (controller != null)
                {
                    controller.OpenLaserSerialPort(laserComListCbx.Text, laserBaudRateCbx.Text,
                        laserDataBitsCbx.Text, laserStopBitsCbx.Text, laserParityCbx.Text, laserHandshakingcbx.Text);
                    OpenBtnClickHandler?.Invoke("laser");
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
                string pipeName = GlobalConfig.PortManagerPipeName;
                string busName = GlobalConfig.PortManagerCOMBusName;
                string busPort = GlobalConfig.PortManagerCOMBusPort;
                string busBaud = GlobalConfig.PortManagerCOMBusBaud;
                string busDataBit = GlobalConfig.PortManagerCOMBusDataBit;
                string busStopBit = GlobalConfig.PortManagerCOMBusStopBit;
                string busProtocolName = GlobalConfig.PortManagerProtocolName;
                string busProtocolRouterPort = GlobalConfig.PortManagerRouterPort;
                string pcAddress = GlobalConfig.PortManagerPCAddress;
                if (CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName] != null &&
                    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName) != null &&
                    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busPort) != null)
                {
                    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busPort).value = motorComListCbx.Text;
                    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busBaud).value = "115200";
                    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busDataBit).value = motorDataBitsCbx.Text;
                    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busStopBit).value = "1";
                }
                //PC
                if (CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName] != null &&
                     CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName) != null &&
                     CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName).GetProperty(busProtocolRouterPort) != null && 
                     CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName).GetProperty(busProtocolRouterPort).GetProperty(pcAddress) != null)
                {
                    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName).GetProperty(busProtocolRouterPort).GetProperty(pcAddress).value = "0xFE";
                }
                CII.Library.CIINet.Manager.PortManager.GetInstance().Save();
                CII.Library.CIINet.Manager.PortManager.GetInstance().Reset();
                //CII.Library.CIINet.Manager.PortManager.GetInstance().Open();
                motorOpenCloseSpbtn.Text = "Close";
                motorStatus.Text = motorComListCbx.Text + " Opend";
                OpenBtnClickHandler?.Invoke("motor");
            }
            else
            {
                CII.Library.CIINet.Manager.PortManager.GetInstance().Close();
                motorOpenCloseSpbtn.Text = "Open";
                motorStatus.Text = motorComListCbx.Text + " Closed";
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
            if (e.isOpend)
            {
                laserStatus.Text = laserComListCbx.Text + " Opend";
                laserOpenCloseSpbtn.Text = "Close";

                laserComListCbx.Enabled = false;
                laserBaudRateCbx.Enabled = false;
                laserDataBitsCbx.Enabled = false;
                laserStopBitsCbx.Enabled = false;
                laserParityCbx.Enabled = false;
                laserHandshakingcbx.Enabled = false;

            }
            else
            {
                laserStatus.Text = "Open failed !";
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

        public override void RefreshUI()
        {
            base.RefreshUI();
        }

        protected override void closeButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Enabled = false;
            OpenBtnClickHandler?.Invoke("close");
        }
    }
}
