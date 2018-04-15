using CII.Ins.Model.GlobalConfig;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public class SerialPortHelper
    {
        private string pipeName;
        private string busName;
        private string busPort;
        private string busBaud;
        private string busDataBit;
        private string busStopBit;
        private string busProtocolName;
        private string busProtocolRouterPort;
        private string pcAddress;

        private static SerialPortHelper helper;

        private int index = 0;

        public SerialPortHelper()
        {
            pipeName = GlobalConfig.PortManagerPipeName;
            busName = GlobalConfig.PortManagerCOMBusName;
            busPort = GlobalConfig.PortManagerCOMBusPort;
            busBaud = GlobalConfig.PortManagerCOMBusBaud;
            busDataBit = GlobalConfig.PortManagerCOMBusDataBit;
            busStopBit = GlobalConfig.PortManagerCOMBusStopBit;
            busProtocolName = GlobalConfig.PortManagerProtocolName;
            busProtocolRouterPort = GlobalConfig.PortManagerRouterPort;
            pcAddress = GlobalConfig.PortManagerPCAddress;
        }
        public static  SerialPortHelper GetHelper()
        {
            if (helper == null)
            {
                helper = new SerialPortHelper();
            }
            return helper;
        }

        public string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        public bool HasPorts
        {
            get
            {
                if (GetPorts() != null && GetPorts().Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void CheckPort()
        {
            string[] ports = GetPorts();
            if (ports != null && ports.Length > 0)
            {
                if (index >= 0 && index < ports.Length)
                {
                    Save(ports[index]);
                }
                if (index + 1 <ports.Length)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
        }

        public bool WaringCheckSystem()
        {
            return index == GetPorts().Length;
        }

        public void ResetIndex()
        {
            index = 0;
        }

        public void Save(string comPort)
        {
            if (CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName] != null &&
                CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName) != null &&
                CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busPort) != null)
            {
                CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busPort).value = comPort;
                Program.SysConfig.MotorPort = comPort;
                CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busBaud).value = "115200";
                CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busDataBit).value = "8";
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
        }
    }
}
