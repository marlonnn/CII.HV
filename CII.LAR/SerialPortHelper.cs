using CII.Ins.Business.Command.LAR;
using CII.Ins.Model.GlobalConfig;
using CII.Library.CIINet;
using CII.Library.CIINet.Manager;
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

        private string laserPipeName;
        private string laserBusName;
        private string laserBusPort;
        private string laserBusBaud;
        private string laserBusDataBit;
        private string laserBusStopBit;
        private string laserBusProtocolName;
        private string laserBusProtocolRouterPort;

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

            laserPipeName = GlobalConfig.LaserPortManagerPipeName;
            laserBusName = GlobalConfig.LaserPortManagerCOMBusName;
            laserBusPort = GlobalConfig.LaserPortManagerCOMBusPort;
            laserBusBaud = GlobalConfig.LaserPortManagerCOMBusBaud;
            laserBusDataBit = GlobalConfig.LaserPortManagerCOMBusDataBit;
            laserBusStopBit = GlobalConfig.LaserPortManagerCOMBusStopBit;
            laserBusProtocolName = GlobalConfig.LaserPortManagerProtocolName;
            laserBusProtocolRouterPort = GlobalConfig.LaserPortManagerRouterPort;
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

        //发送函数
        public byte[] SendDirectCommand(byte[] data, string portName)
        {
            byte[] rev = null;
            try
            {
                PortManager.GetInstance().GetPipe(laserPipeName).GetBusProperty().GetProperty("port").value = portName;
                PortManager.GetInstance().Save();//不保存打开，当前串口设置失效
                PortManager.GetInstance().Reset();//解决配置中串口不存在时，后前无法open的bug
                PortManager.GetInstance().GetPipe(laserPipeName).Open();
                LogHelper.GetLogger<SerialPortHelper>().Error("Send Data: " + ByteHelper.Byte2ReadalbeXstring(data));
                SimpleProtocolData newData = new SimpleProtocolData(data);
                object recv = PortManager.GetInstance().Send(LARCommandHelper.InsName1, newData);
                if (recv != null)
                {
                    rev = ((ByteArrayWrap)recv).GetBytes();
                    LogHelper.GetLogger<SerialPortHelper>().Error("Reveived Data: " + ByteHelper.Byte2ReadalbeXstring(data));
                }
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger<SerialPortHelper>().Error("error message: " + ex.Message);
                LogHelper.GetLogger<SerialPortHelper>().Error("error stacktrace: " + ex.StackTrace);
            }
            return rev;
        }

        public void SaveLaserPort(string comPort)
        {
            if (PortManager.GetInstance().pipes[laserPipeName] != null &&
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusName) != null &&
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusName).GetProperty(laserBusPort) != null)
            {
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusName).GetProperty(laserBusPort).value = comPort;
                Program.SysConfig.MotorPort = comPort;
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusName).GetProperty(laserBusPort).value = "9600";
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusName).GetProperty(laserBusDataBit).value = "8";
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusName).GetProperty(laserBusStopBit).value = "1";
            }
            //PC
            if (PortManager.GetInstance().pipes[laserPipeName] != null &&
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusProtocolName) != null &&
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusProtocolName).GetProperty(laserBusProtocolRouterPort) != null &&
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusProtocolName).GetProperty(laserBusProtocolRouterPort).GetProperty(pcAddress) != null)
            {                                                                                                
                PortManager.GetInstance().pipes[laserPipeName].GetProperty(laserBusProtocolName).GetProperty(laserBusProtocolRouterPort).GetProperty(pcAddress).value = "0xFE";
            }
            PortManager.GetInstance().Save();
            PortManager.GetInstance().Reset();
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
