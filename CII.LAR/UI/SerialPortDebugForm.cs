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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    /// <summary>
    /// 串口调试窗口
    /// </summary>
    public partial class SerialPortDebugForm : Form
    {
        private SerialPortCommunication serialPortCom;

        public SerialPortDebugForm()
        {
            InitializeComponent();
            InitializeLaserCOMCombox();
            this.Load += SerialPortDebugForm_Load;
            this.FormClosing += SerialPortDebugForm_FormClosing;
        }

        private void SerialPortDebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPortCom.Close();
        }

        private void SerialPortDebugForm_Load(object sender, EventArgs e)
        {
            serialPortCom = SerialPortCommunication.GetInstance();
            serialPortCom.ComOpenEvent += LaserOpenComEvent;
            serialPortCom.ComCloseEvent += LaserCloseComEvent;
            serialPortCom.SerialDataReceivedHandler += SerialDataReceivedHandler;
        }

        private void SerialDataReceivedHandler(LaserBaseResponse baseResponse)
        {
            if (baseResponse != null)
                textBox.AppendText(baseResponse.ToString());
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

                laserComListCbx.Enabled = false;
                laserBaudRateCbx.Enabled = false;
                laserDataBitsCbx.Enabled = false;
                laserStopBitsCbx.Enabled = false;
                laserParityCbx.Enabled = false;
                laserHandshakingcbx.Enabled = false;

            }
            else    //Open failed
            {

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
                if (serialPortCom != null)
                {
                    serialPortCom.SerialPortOpen(laserComListCbx.Text, laserBaudRateCbx.Text,
                        laserDataBitsCbx.Text, laserStopBitsCbx.Text, laserParityCbx.Text, laserHandshakingcbx.Text);
                }
            }
            else
            {
                if (serialPortCom != null)
                {
                    serialPortCom.Close();
                }
            }
        }

        private void Send(LaserBaseRequest br)
        {
            var bytes = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(br.Encode()[0]);
            SendData(bytes);
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            var c00 = new LaserC00Request();
            Send(c00);
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            var c01 = new LaserC01Request();
            Send(c01);
        }

        private void btn03_Click(object sender, EventArgs e)
        {
            var c03 = new LaserC03Request();
            Send(c03);
        }

        private void btn04_Click(object sender, EventArgs e)
        {
            var c04 = new LaserC04Request();
            Send(c04);
        }

        private void btn05_Click(object sender, EventArgs e)
        {
            var c05 = new LaserC05Request();
            Send(c05);
        }

        private void btn06_Click(object sender, EventArgs e)
        {
            var c06 = new LaserC06Request();
            Send(c06);
        }

        private void btn07_Click(object sender, EventArgs e)
        {
            var c07 = new LaserC07Request();
            Send(c07);
        }

        private void btn08_Click(object sender, EventArgs e)
        {
            var c08 = new LaserC08Request();
            Send(c08);
        }

        private void btn0B_Click(object sender, EventArgs e)
        {
             var c0b = new LaserC0BRequest();
            Send(c0b);
        }

        private void btn75_Click(object sender, EventArgs e)
        {
            var c75 = new LaserC75Request(3);
            Send(c75);
        }

        private void btn09_Click(object sender, EventArgs e)
        {
            var c09 = new LaserC09Request();
            Send(c09);
        }

        private void btn0C_Click(object sender, EventArgs e)
        {
            var c0c = new LaserC0CRequest();
            Send(c0c);
        }
        private void btn70_Click(object sender, EventArgs e)
        {
            var c70 = new LaserC70Request();
            var bytes = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(c70.Encode()[0]);
            SendData(bytes);
        }

        private void btn71_Click(object sender, EventArgs e)
        {
            var c71 = new LaserC71Request();
            Send(c71);
        }

        private void btn72_Click(object sender, EventArgs e)
        {
            var c72 = new LaserC72Request(20);
        }

        private void btn73_Click(object sender, EventArgs e)
        {
            var c73 = new LaserC73Request(100);
        }

        private void btn74_Click(object sender, EventArgs e)
        {
            var c74 = new LaserC74Request(0x01);
            var bps = c74.Encode();
            List<byte[]> bytes = new List<byte[]>();
            foreach (var b in bps)
            {
                var data = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(b);
                bytes.Add(data);
            }
            SendData(bytes);
        }

        private void clearReceivebtn_Click(object sender, EventArgs e)
        {
            receivetbx.Clear();
        }

        private void slider_MouseUp(object sender, MouseEventArgs e)
        {
            var c75 = new LaserC75Request(this.slider.Value);
            var bps = c75.Encode();
            List<byte[]> bytes = new List<byte[]>();
            foreach (var b in bps)
            {
                var data = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(b);
                bytes.Add(data);
            }
            SendData(bytes);
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            this.slider.Text = slider.Value.ToString();
        }

        private void SendData(byte[] data)
        {
            if (sendtbx.Text.Length > 0)
            {
                sendtbx.AppendText("\n");
            }
            if (data != null)
            {
                sendtbx.AppendText(IController.Bytes2Hex(data));
            }
            serialPortCom.SendData(data);
            Thread.Sleep(100);
            if (serialPortCom.FinalData != null)
            {
                if (receivetbx.Text.Length > 0)
                {
                    receivetbx.AppendText("\n");
                }
                if (serialPortCom.FinalData != null)
                {
                    receivetbx.AppendText(IController.Bytes2Hex(serialPortCom.FinalData));
                }
            }
        }

        private void SendData(List<byte[]> dataList)
        {
            if (sendtbx.Text.Length > 0)
            {
                sendtbx.AppendText("\n");
            }
            if (dataList != null)
            {
                foreach (var d in dataList)
                {
                    sendtbx.AppendText(IController.Bytes2Hex(d));
                    if (sendtbx.Text.Length > 0)
                    {
                        sendtbx.AppendText("-");
                    }
                }
            }
            serialPortCom.SendData(dataList);
            Thread.Sleep(100);
            if (serialPortCom.FinalData != null)
            {
                if (receivetbx.Text.Length > 0)
                {
                    receivetbx.AppendText("\n");
                }
                if (serialPortCom.FinalData != null)
                {
                    receivetbx.AppendText(IController.Bytes2Hex(serialPortCom.FinalData));
                }
            }
        }

        private void clearSendbtn_Click(object sender, EventArgs e)
        {
            sendtbx.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox.Clear();
        }
    }
}
