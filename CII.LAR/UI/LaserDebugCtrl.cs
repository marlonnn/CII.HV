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
using System.Threading;

namespace CII.LAR.UI
{
    public partial class LaserDebugCtrl : Form
    {
        private SerialPortManager serialPortCom = SerialPortManager.GetInstance();
        public LaserDebugCtrl()
        {
            //this.CtrlType = CtrlType.LaserDebugCtrl;
            InitializeComponent();
            InitializeLaserCOMCombox();
            this.Load += LaserDebugCtrl_Load;
            this.FormClosing += LaserDebugCtrl_FormClosing;
            this.txtCompensationFactor.Text = Program.SysConfig.CompensationFactor.ToString();
            this.chbLocation.Checked = Program.SysConfig.ScreenshotWithLocation;
            this.chbLocation.CheckedChanged += new System.EventHandler(this.chbLocation_CheckedChanged);
        }

        private void LaserDebugCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void LaserDebugCtrl_Load(object sender, EventArgs e)
        {
            IsOpened(serialPortCom.SerialPort.IsOpen);
            if (serialPortCom.SerialPort.IsOpen)
            {
                CheckLaserStatus();
                Thread.Sleep(1000);
                //CheckRedLaserCurrent();
            }
        }

        private void CheckRedLaserCurrent()
        {
            var c09 = new LaserC09Request();
            var bytes = serialPortCom.Encode(c09);
            serialPortCom.SendData(bytes);
        }

        private bool redLaserOpen = false;
        private void SerialDataReceivedHandler(LaserBaseResponse baseResponse)
        {
            if (baseResponse != null)
            {
                LaserC01Response c01r = baseResponse as LaserC01Response;
                if (c01r != null)
                {
                    if (c01r.Flag == 1152)
                    {
                        //红光关闭，则强制开启
                        UpdateBtnText("Open");
                        redLaserOpen = false;
                    }
                    else if (c01r.Flag == 1408)
                    {
                        UpdateBtnText("Closed");
                        redLaserOpen = true;
                    }
                }
                //LaserC09Response c09r = baseResponse as LaserC09Response;
                //if (c09r != null)
                //{
                //    this.slider.Value = (int)c09r.Current;
                //}
            }
        }

        private void UpdateBtnText(string text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(UpdateBtnText), text);
            }
            else
            {
                this.btn70.Text = text;
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

        private void txtCompensationFactor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var value = float.Parse(this.txtCompensationFactor.Text);
                if (value <= 0) return;
                Program.SysConfig.CompensationFactor = value;
            }
            catch (Exception ex)
            {

            }
        }

        private void chbLocation_CheckedChanged(object sender, EventArgs e)
        {
            Program.SysConfig.ScreenshotWithLocation = this.chbLocation.Checked;
        }
    }
}
