﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CII.LAR
{
    public class SerialPortModel
    {
        private SerialPort laserSerialPort = new SerialPort();
        private SerialPort motorSerialPort = new SerialPort();

        public event SerialPortEventHandler laserComReceiveDataEvent = null;
        public event SerialPortEventHandler laserComOpenEvent = null;
        public event SerialPortEventHandler laserComCloseEvent = null;

        public event SerialPortEventHandler motorComReceiveDataEvent = null;
        public event SerialPortEventHandler motorComOpenEvent = null;
        public event SerialPortEventHandler motorComCloseEvent = null;

        private Object thisLock = new Object();

        /// <summary>
        /// When serial received data, will call this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaserDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (laserSerialPort.BytesToRead <= 0)
            {
                return;
            }
            //Thread Safety explain in MSDN:
            // Any public static (Shared in Visual Basic) members of this type are thread safe. 
            // Any instance members are not guaranteed to be thread safe.
            // So, we need to synchronize I/O
            lock (thisLock)
            {
                int len = laserSerialPort.BytesToRead;
                Byte[] data = new Byte[len];
                try
                {
                    laserSerialPort.Read(data, 0, len);
                }
                catch (System.Exception)
                {
                    //catch read exception
                }
                SerialPortEventArgs args = new SerialPortEventArgs();
                args.receivedBytes = data;
                if (laserComReceiveDataEvent != null)
                {
                    laserComReceiveDataEvent.Invoke(this, args);
                }
            }
        }

        /// <summary>
        /// LaserSendData bytes to device
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public bool LaserSendData(Byte[] bytes)
        {
            if (!laserSerialPort.IsOpen)
            {
                return false;
            }

            try
            {
                laserSerialPort.Write(bytes, 0, bytes.Length);
            }
            catch (System.Exception)
            {
                return false;   //write failed
            }
            return true;        //write successfully
        }

        /// <summary>
        /// Open Serial port
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        /// <param name="handshake"></param>
        public void LaserSerialPortOpen(string portName, String baudRate,
            string dataBits, string stopBits, string parity,
            string handshake)
        {
            if (laserSerialPort.IsOpen)
            {
                Close();
            }
            laserSerialPort.PortName = portName;
            laserSerialPort.BaudRate = Convert.ToInt32(baudRate);
            laserSerialPort.DataBits = Convert.ToInt16(dataBits);

            /**
             *  If the Handshake property is set to None the DTR and RTS pins 
             *  are then freed up for the common use of Power, the PC on which
             *  this is being typed gives +10.99 volts on the DTR pin & +10.99
             *  volts again on the RTS pin if set to true. If set to false 
             *  it gives -9.95 volts on the DTR, -9.94 volts on the RTS. 
             *  These values are between +3 to +25 and -3 to -25 volts this 
             *  give a dead zone to allow for noise immunity.
             *  http://www.codeproject.com/Articles/678025/Serial-Comms-in-Csharp-for-Beginners
             */
            if (handshake == "None")
            {
                //Never delete this property
                laserSerialPort.RtsEnable = true;
                laserSerialPort.DtrEnable = true;
            }

            SerialPortEventArgs args = new SerialPortEventArgs();
            try
            {
                laserSerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                laserSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                laserSerialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), handshake);
                laserSerialPort.WriteTimeout = 1000; /*Write time out*/
                laserSerialPort.Open();
                laserSerialPort.DataReceived += new SerialDataReceivedEventHandler(LaserDataReceived);
                args.isOpend = true;
            }
            catch (System.Exception)
            {
                args.isOpend = false;
            }
            if (laserComOpenEvent != null)
            {
                laserComOpenEvent.Invoke(this, args);
            }

        }

        /**
         *  Take care to avoid deadlock when calling Close on the SerialPort 
         *  in response to a GUI event.
         *   An app involving the UI and the SerialPort freezes up when closing the SerialPort
         *   Deadlock can occur if Control.Invoke() is used in serial port event handlers
         * 
         *  The typical scenario we encounter is occasional deadlock in an app 
         *  that has a data received handler trying to update the GUI at the 
         *  same time the GUI thread is trying to close the SerialPort (for 
         *  example, in response to the user clicking a Close button).
         * 
         *  The reason deadlock happens is that Close() waits for events to 
         *  finish executing before it closes the port. You can address this 
         *  problem in your apps in two ways:
         * 
         *  (1)In your event handlers, replace every Control.Invoke call with 
         *  Control.BeginInvoke, which executes asynchronously and avoids 
         *  the deadlock condition. This is commonly used for deadlock avoidance 
         *  when working with GUIs.
         *  
         *  (2)Call serialPort.Close() on a separate thread. You may prefer this
         *  because this is less invasive than updating your Invoke calls.
         */
        /// <summary>
        /// Close serial port
        /// </summary>
        public void Close()
        {
            Thread closeThread = new Thread(new ThreadStart(CloseSpThread));
            closeThread.Start();
        }

        /// <summary>
        /// Close serial port thread
        /// </summary>
        private void CloseSpThread()
        {
            SerialPortEventArgs args = new SerialPortEventArgs();
            args.isOpend = false;
            try
            {
                laserSerialPort.Close(); //close the serial port
                laserSerialPort.DataReceived -= new SerialDataReceivedEventHandler(LaserDataReceived);
            }
            catch (Exception)
            {
                args.isOpend = true;
            }
            if (laserComCloseEvent != null)
            {
                laserComCloseEvent.Invoke(this, args);
            }

        }
    }
}
