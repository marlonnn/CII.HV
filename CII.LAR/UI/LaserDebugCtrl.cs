﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using CII.LAR.Protocol;
using CII.LAR.Commond;

namespace CII.LAR.UI
{
    public partial class LaserDebugCtrl : Form
    {
        private SerialPortCommunication serialPortCom = SerialPortCommunication.GetInstance();
        public LaserDebugCtrl()
        {
            //this.CtrlType = CtrlType.LaserDebugCtrl;
            InitializeComponent();
            InitializeLaserCOMCombox();
            this.Load += LaserDebugCtrl_Load;
            this.FormClosing += LaserDebugCtrl_FormClosing;
            serialPortCom.SerialDataReceivedHandler += SerialDataReceivedHandler;
        }

        private void LaserDebugCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPortCom.SerialDataReceivedHandler -= SerialDataReceivedHandler;
        }

        private void LaserDebugCtrl_Load(object sender, EventArgs e)
        {
            IsOpened(serialPortCom.SerialPort.IsOpen);
            if (serialPortCom.SerialPort.IsOpen)
            {
                CheckLaserStatus();
            }
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
            }
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

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            this.slider.Text = slider.Value.ToString();
        }

        private void slider_MouseUp(object sender, MouseEventArgs e)
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

        private void laserOpenCloseSpbtn_Click(object sender, EventArgs e)
        {
            if (laserOpenCloseSpbtn.Text == "Open")
            {
                if (serialPortCom != null && !serialPortCom.SerialPort.IsOpen)
                {
                    serialPortCom.SerialPortOpen(laserComListCbx.Text, laserBaudRateCbx.Text,
                        laserDataBitsCbx.Text, laserStopBitsCbx.Text, laserParityCbx.Text, laserHandshakingcbx.Text);
                }
            }
            else
            {
                if (serialPortCom != null && serialPortCom.SerialPort.IsOpen)
                {
                    serialPortCom.Close();
                }
            }
        }

        private void EnableLaserCtrl(bool enable)
        {
            this.slider.Enabled = enable;
            this.btn70.Enabled = enable;
        }

        private void IsOpened(bool isOpened)
        {
            if (isOpened)
            {
                for (int i =0; i < laserComListCbx.Items.Count; i++)
                {
                    if (laserComListCbx.Items[i].ToString() == Program.SysConfig.LaserPort)
                    {
                        laserComListCbx.SelectedItem = Program.SysConfig.LaserPort;
                        laserComListCbx.Text = Program.SysConfig.LaserPort;
                    }
                }
                laserStatus.Text = Program.SysConfig.LaserPort + " Opened";
                laserOpenCloseSpbtn.Text = "Close";

                laserComListCbx.Enabled = false;
                laserBaudRateCbx.Enabled = false;
                laserDataBitsCbx.Enabled = false;
                laserStopBitsCbx.Enabled = false;
                laserParityCbx.Enabled = false;
                laserHandshakingcbx.Enabled = false;
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
                laserStatus.Text = laserComListCbx.Text + " Opened";
                laserOpenCloseSpbtn.Text = "Close";

                laserComListCbx.Enabled = false;
                laserBaudRateCbx.Enabled = false;
                laserDataBitsCbx.Enabled = false;
                laserStopBitsCbx.Enabled = false;
                laserParityCbx.Enabled = false;
                laserHandshakingcbx.Enabled = false;
                Program.SysConfig.LaserPort = laserComListCbx.Text;
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
            this.btn70.Text = redLaserOpen ? "Closed" : "Open";
        }
    }
}
