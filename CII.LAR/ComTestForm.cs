﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows;
using System.Windows.Forms;
using CII.LAR.Protocol;
using CII.LAR.Commond;

namespace CII.LAR
{
    public partial class ComTestForm : Form, IView
    {
        private IController controller;
        private int sendBytesCount = 0;
        private int receiveBytesCount = 0;
        private LaserProtocolFactory laserProtocolFactory;

        private MotorProtocolFactory motorProtocolFactory;

        public ComTestForm()
        {
            InitializeComponent();
            InitializeCOMCombox();
            this.statusTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            this.toolStripStatusTx.Text = "Sent: 0";
            this.toolStripStatusRx.Text = "Received: 0";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeLaserProtocolFactory();
            this.Load += ComTestForm_Load;
            this.FormClosing += ComTestForm_FormClosing;
        }

        private void ComTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //laserProtocolFactory.DestroyDecodeThread();
            //laserProtocolFactory.DestroyEncodeThread();

            motorProtocolFactory.DestroyDecodeThread();
            motorProtocolFactory.DestroyEncodeThread();
        }

        private void ComTestForm_Load(object sender, EventArgs e)
        {
            //laserProtocolFactory.StartDecodeThread();
            //laserProtocolFactory.StartEncodeThread();

            motorProtocolFactory.StartDecodeThread();
            motorProtocolFactory.StartEncodeThread();
        }

        private void InitializeLaserProtocolFactory()
        {
            laserProtocolFactory = LaserProtocolFactory.GetInstance();

            motorProtocolFactory = MotorProtocolFactory.GetInstance();
        }

        /// <summary>
        /// Set controller
        /// </summary>
        /// <param name="controller"></param>
        public void SetController(IController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Initialize serial port information
        /// </summary>
        private void InitializeCOMCombox()
        {
            //BaudRate
            baudRateCbx.Items.Add(4800);
            baudRateCbx.Items.Add(9600);
            baudRateCbx.Items.Add(19200);
            baudRateCbx.Items.Add(38400);
            baudRateCbx.Items.Add(57600);
            baudRateCbx.Items.Add(115200);
            baudRateCbx.Items.ToString();
            //get 9600 print in text
            baudRateCbx.Text = baudRateCbx.Items[1].ToString();

            //Data bits
            dataBitsCbx.Items.Add(7);
            dataBitsCbx.Items.Add(8);
            //get the 8bit item print it in the text 
            dataBitsCbx.Text = dataBitsCbx.Items[1].ToString();

            //Stop bits
            stopBitsCbx.Items.Add("One");
            stopBitsCbx.Items.Add("OnePointFive");
            stopBitsCbx.Items.Add("Two");
            //get the One item print in the text
            stopBitsCbx.Text = stopBitsCbx.Items[0].ToString();

            //Parity
            parityCbx.Items.Add("None");
            parityCbx.Items.Add("Even");
            parityCbx.Items.Add("Mark");
            parityCbx.Items.Add("Odd");
            parityCbx.Items.Add("Space");
            //get the first item print in the text
            parityCbx.Text = parityCbx.Items[0].ToString();

            //Handshaking
            handshakingcbx.Items.Add("None");
            handshakingcbx.Items.Add("XOnXOff");
            handshakingcbx.Items.Add("RequestToSend");
            handshakingcbx.Items.Add("RequestToSendXOnXOff");
            handshakingcbx.Text = handshakingcbx.Items[0].ToString();

            //Com Ports
            string[] ArrayComPortsNames = SerialPort.GetPortNames();
            if (ArrayComPortsNames.Length == 0)
            {
                statuslabel.Text = "No COM found !";
                openCloseSpbtn.Enabled = false;
            }
            else
            {
                Array.Sort(ArrayComPortsNames);
                for (int i = 0; i < ArrayComPortsNames.Length; i++)
                {
                    comListCbx.Items.Add(ArrayComPortsNames[i]);
                }
                comListCbx.Text = ArrayComPortsNames[0];
                openCloseSpbtn.Enabled = true;
            }
        }

        /// <summary>
        /// update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenComEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(OpenComEvent), sender, e);
                return;
            }

            if (e.isOpend)  //Open successfully
            {
                statuslabel.Text = comListCbx.Text + " Opend";
                openCloseSpbtn.Text = "Close";
                sendbtn.Enabled = true;
                autoSendcbx.Enabled = true;
                autoReplyCbx.Enabled = true;

                comListCbx.Enabled = false;
                baudRateCbx.Enabled = false;
                dataBitsCbx.Enabled = false;
                stopBitsCbx.Enabled = false;
                parityCbx.Enabled = false;
                handshakingcbx.Enabled = false;
                refreshbtn.Enabled = false;

                if (autoSendcbx.Checked)
                {
                    autoSendtimer.Start();
                    sendtbx.ReadOnly = true;
                }
            }
            else    //Open failed
            {
                statuslabel.Text = "Open failed !";
                sendbtn.Enabled = false;
                autoSendcbx.Enabled = false;
                autoReplyCbx.Enabled = false;
            }
        }

        /// <summary>
        /// update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CloseComEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(CloseComEvent), sender, e);
                return;
            }

            if (!e.isOpend) //close successfully
            {
                statuslabel.Text = comListCbx.Text + " Closed";
                openCloseSpbtn.Text = "Open";

                sendbtn.Enabled = false;
                sendtbx.ReadOnly = false;
                autoSendcbx.Enabled = false;
                autoSendtimer.Stop();

                comListCbx.Enabled = true;
                baudRateCbx.Enabled = true;
                dataBitsCbx.Enabled = true;
                stopBitsCbx.Enabled = true;
                parityCbx.Enabled = true;
                handshakingcbx.Enabled = true;
                refreshbtn.Enabled = true;
            }
        }

        /// <summary>
        /// Display received data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComReceiveDataEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs>(ComReceiveDataEvent), sender, e);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }
            var receivedBytes = e.receivedBytes;
            LogHelper.GetLogger<ComTestForm>().Debug(string.Format("接受的消息为： {0}", ByteHelper.Byte2ReadalbeXstring(receivedBytes)));
            laserProtocolFactory.RxQueue.Push(new OriginalBytes(DateTime.Now, receivedBytes));
            if (recStrRadiobtn.Checked) //display as string
            {
                receivetbx.AppendText(Encoding.Default.GetString(receivedBytes));
            }
            else //display as hex
            {
                if (receivetbx.Text.Length > 0)
                {
                    receivetbx.AppendText("-");
                }
                receivetbx.AppendText(IController.Bytes2Hex(e.receivedBytes));
            }
            //update status bar
            receiveBytesCount += e.receivedBytes.Length;
            toolStripStatusRx.Text = "Received: "+receiveBytesCount.ToString();

            //auto reply
            if (autoReplyCbx.Checked)
            {
                sendbtn_Click(this, new EventArgs());
            }

        }

        /// <summary>
        /// Auto scroll in receive textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receivetbx_TextChanged(object sender, EventArgs e)
        {
            receivetbx.SelectionStart = receivetbx.Text.Length;
            receivetbx.ScrollToCaret();
        }

        /// <summary>
        /// update time in status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statustimer_Tick(object sender, EventArgs e)
        {
            this.statusTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// open or close serial port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openCloseSpbtn_Click(object sender, EventArgs e)
        {
            if (openCloseSpbtn.Text == "Open")
            {
                controller.OpenSerialPort(comListCbx.Text, baudRateCbx.Text,
                    dataBitsCbx.Text, stopBitsCbx.Text, parityCbx.Text,
                    handshakingcbx.Text);
            } 
            else
            {
                controller.CloseSerialPort();
            }
        }

        /// <summary>
        /// Refresh soft to find Serial port device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshbtn_Click(object sender, EventArgs e)
        {
            comListCbx.Items.Clear();
            //Com Ports
            string[] ArrayComPortsNames = SerialPort.GetPortNames();
            if (ArrayComPortsNames.Length == 0)
            {
                statuslabel.Text = "No COM found !";
                openCloseSpbtn.Enabled = false;
            }
            else
            {
                Array.Sort(ArrayComPortsNames);
                for (int i = 0; i < ArrayComPortsNames.Length; i++)
                {
                    comListCbx.Items.Add(ArrayComPortsNames[i]);
                }
                comListCbx.Text = ArrayComPortsNames[0];
                openCloseSpbtn.Enabled = true;
                statuslabel.Text = "OK !";
            }
            
        }

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendbtn_Click(object sender, EventArgs e)
        {
            String sendText = sendtbx.Text;
            bool flag = false;
            if (sendText == null)
            {
                return;
            }
            //set select index to the end
            sendtbx.SelectionStart = sendtbx.TextLength; 
          
            if (sendHexRadiobtn.Checked)
            {
                //If hex radio checked
                //send bytes to serial port
                Byte[] bytes = IController.Hex2Bytes(sendText);
                sendbtn.Enabled = false;//wait return
                flag = controller.SendDataToCom(bytes);
                sendbtn.Enabled = true;
                sendBytesCount += bytes.Length;
            }
            else
            {
                //send String to serial port
                sendbtn.Enabled = false;//wait return
                flag = controller.SendDataToCom(sendText);
                sendbtn.Enabled = true;
                sendBytesCount += sendText.Length;
            }
            if (flag)
            {
                statuslabel.Text = "Send OK !";
            }
            else
            {
                statuslabel.Text = "Send failed !";
            }
            //update status bar
            toolStripStatusTx.Text = "Sent: " + sendBytesCount.ToString();
        }

        /// <summary>
        /// clear text in send area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearSendbtn_Click(object sender, EventArgs e)
        {
            sendtbx.Text = "";
            toolStripStatusTx.Text = "Sent: 0";
            sendBytesCount = 0;
            addCRCcbx.Checked = false;
        }

        /// <summary>
        /// clear receive text in receive area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearReceivebtn_Click(object sender, EventArgs e)
        {
            receivetbx.Text = "";
            toolStripStatusRx.Text = "Received: 0";
            receiveBytesCount = 0;
        }

        /// <summary>
        /// String to hex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recHexRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            if (recHexRadiobtn.Checked)
            {
                if (receivetbx.Text == null)
                {
                    return;
                }
                receivetbx.Text = IController.String2Hex(receivetbx.Text);
            }
        }

        /// <summary>
        /// Hex to string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recStrRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            if (recStrRadiobtn.Checked)
            {
                if (receivetbx.Text == null)
                {
                    return;
                }
                receivetbx.Text = IController.Hex2String(receivetbx.Text);
            }
        }

        /// <summary>
        /// String to Hex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendHexRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            if (sendHexRadiobtn.Checked)
            {
                if (sendtbx.Text == null)
                {
                    return;
                }
                sendtbx.Text = IController.String2Hex(sendtbx.Text);
                addCRCcbx.Enabled = true;
            }
        }

        /// <summary>
        /// Hex to string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendStrRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            if (sendStrRadiobtn.Checked)
            {
                if (sendtbx.Text == null)
                {
                    return;
                }
                sendtbx.Text = IController.Hex2String(sendtbx.Text);
                addCRCcbx.Enabled = false;
            }
        }

        /// <summary>
        /// Filter illegal input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Input Hex, should like: AF-1B-09
            if (sendHexRadiobtn.Checked)
            {
                e.Handled = true;
                int length = sendtbx.SelectionStart;
                switch (length % 3)
                {
                case 0:
                case 1:
                    if ((e.KeyChar >= 'a' && e.KeyChar <= 'f')
                        || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                        || char.IsDigit(e.KeyChar)
                        || (char.IsControl(e.KeyChar) && e.KeyChar != (char)13))
                    {
                        e.Handled = false;
                    }
                    break;
                case 2:
                    if (e.KeyChar == '-'
                        || (char.IsControl(e.KeyChar) && e.KeyChar != (char)13))
                    {
                        e.Handled = false;
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// Auto send data to serial port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoSendcbx_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSendcbx.Checked)
            {
                autoSendtimer.Enabled = true;
                autoSendtimer.Interval = int.Parse(sendIntervalTimetbx.Text);
                autoSendtimer.Start();

                //disable send botton and textbox
                sendIntervalTimetbx.Enabled = false;
                sendtbx.ReadOnly = true;
                sendbtn.Enabled = false;
            }
            else
            {
                autoSendtimer.Enabled = false;
                autoSendtimer.Stop();

                //enable send botton and textbox
                sendIntervalTimetbx.Enabled = true;
                sendtbx.ReadOnly = false;
                sendbtn.Enabled = true;
            }
        }

        private void autoSendtimer_Tick(object sender, EventArgs e)
        {
            //if (laserProtocolFactory.TxQueue != null && laserProtocolFactory.TxQueue.Count > 0)
            //{
            //    var list = laserProtocolFactory.TxQueue.PopAll();
            //    foreach (var o in list)
            //    {
            //        var originalByte = o as OriginalBytes;
            //        if (originalByte != null)
            //        {
            //            controller.SendDataToCom(originalByte.Data);
            //            LogHelper.GetLogger<ComTestForm>().Debug(string.Format("发送的消息为： {0}", ByteHelper.Byte2ReadalbeXstring(originalByte.Data)));
            //        }   
            //    }
            //}

            if (motorProtocolFactory.TxQueue != null && motorProtocolFactory.TxQueue.Count > 0)
            {
                var list = motorProtocolFactory.TxQueue.PopAll();
                foreach (var o in list)
                {
                    var originalByte = o as OriginalBytes;
                    if (originalByte != null)
                    {
                        controller.SendDataToCom(originalByte.Data);
                        LogHelper.GetLogger<ComTestForm>().Debug(string.Format("发送的消息为： {0}", ByteHelper.Byte2ReadalbeXstring(originalByte.Data)));
                    }
                }
            }
            //sendbtn_Click(sender, e);
        }

        /// <summary>
        /// filter illegal input of auto send interval time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendIntervalTimetbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Add CRC checkbox changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCRCcbx_CheckedChanged(object sender, EventArgs e)
        {
            //String sendText = sendtbx.Text;
            //if (sendText == null || sendText == "")
            //{
            //    addCRCcbx.Checked = false;
            //    return;
            //}
            //if (addCRCcbx.Checked)
            //{
            //    //Add 2 bytes CRC to the end of the data
            //    Byte[] senddata = IController.Hex2Bytes(sendText);
            //    Byte[] crcbytes = BitConverter.GetBytes(CRC16.Compute(senddata));
            //    sendText += "-" + BitConverter.ToString(crcbytes, 1, 1);
            //    sendText += "-" + BitConverter.ToString(crcbytes, 0, 1);
            //}
            //else
            //{
            //    //Delete 2 bytes CRC to the end of the data
            //    if (sendText.Length >= 6)
            //    {
            //        sendText = sendText.Substring(0, sendText.Length - 6);
            //    }
            //}
            //sendtbx.Text = sendText;
        }

        /// <summary>
        /// save received data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receivedDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "txt file|*.txt";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FileName = "received.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fName = saveFileDialog.FileName;
                System.IO.File.WriteAllText(fName, receivetbx.Text);
            }
        }

        /// <summary>
        /// save send data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "txt file|*.txt";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FileName = "send.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fName = saveFileDialog.FileName;
                System.IO.File.WriteAllText(fName, sendtbx.Text);
            }
        }

        /// <summary>
        /// Quit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// about me
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //AboutForm about = new AboutForm();
            //about.StartPosition = FormStartPosition.CenterParent;
            //about.Show();

            //if (about.StartPosition == FormStartPosition.CenterParent)
            //{
            //    var x = Location.X + (Width - about.Width) / 2;
            //    var y = Location.Y + (Height - about.Height) / 2;
            //    about.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
            //}
        }

        /// <summary>
        /// Help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HelpForm help = new HelpForm();
            //help.StartPosition = FormStartPosition.CenterParent;
            //help.Show();

            //if (help.StartPosition == FormStartPosition.CenterParent)
            //{
            //    var x = Location.X + (Width - help.Width) / 2;
            //    var y = Location.Y + (Height - help.Height) / 2;
            //    help.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
            //}
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC00Request());
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC01Request());
        }

        private void btn03_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC03Request());
        }

        private void btn04_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC04Request());
        }

        private void btn05_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC05Request());
        }

        private void btn06_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC06Request());
        }

        private void btn07_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC07Request());
        }

        private void btn08_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC08Request());
        }

        private void btn0B_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC0BRequest());
        }

        private void btn75_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC75Request(10));
        }

        private void btn09_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC09Request());
        }

        private void btn74_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC74Request(0x01));
        }

        private void btn0C_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC0CRequest());
        }

        private void btn71_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC71Request());
        }

        private void btn73_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC73Request(100));
        }

        private void btn72_Click(object sender, EventArgs e)
        {
            laserProtocolFactory.SendMessage(new LaserC72Request(20));
        }

        private void btnC60_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x66);
            request.ControlSelection = 0x61;
            request.ControlMode61 = 0x01;
            request.Direction61 = 0x00;
            request.TotalSteps61 = 50;
            motorProtocolFactory.SendMessage(request);
        }

        private void btn6062_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x66);
            request.ControlSelection = 0x62;
            request.ControlMode62 = 0x01;
            request.Direction62 = 0x00;
            request.TotalSteps62 = 100;
            motorProtocolFactory.SendMessage(request);
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
            motorProtocolFactory.SendMessage(request);
        }

        private void btn6055_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x60;
            motorProtocolFactory.SendMessage(request);
        }

        private void btn6055A0_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0xA0;
            motorProtocolFactory.SendMessage(request);
        }

        private void btn605500_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x00;
            motorProtocolFactory.SendMessage(request);
        }

        private void btn605562_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x62;
            motorProtocolFactory.SendMessage(request);
        }

        private void btn605561_Click(object sender, EventArgs e)
        {
            var request = new MotorC60Request(0x60, 0x55);
            request.ControlSelection = 0x61;
            motorProtocolFactory.SendMessage(request);
        }
    }
}
