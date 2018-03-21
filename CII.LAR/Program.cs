using CII.Ins.Business.Entry.LAR;
using CII.Ins.Model.Command.LAR;
using CII.Ins.Model.GlobalConfig;
using CII.LAR.SysClass;
using CII.Library.CIINet.Commands;
using CII.Library.CIINet.Manager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CII.LAR
{
    static class Program
    {
        private static EntryForm entryForm;

        /// <summary>
        /// The reference of program main form
        /// </summary>
        public static EntryForm EntryForm
        {
            get { return entryForm; }
        }

        private static ExpManager expManager;

        /// <summary>
        /// The exp document manager
        /// </summary>
        public static ExpManager ExpManager
        {
            get { return expManager; }
        }

        private static SysConfig _sysConfig;

        /// <summary>
        /// The configure information about software and hardware
        /// </summary>
        public static SysConfig SysConfig
        {
            get { return _sysConfig; }
        }

        private static SysConfig _sysConfigOrigin;

        /// <summary>
        /// The original configuration about software and hardware
        /// </summary>
        public static SysConfig SysConfigOrigin
        {
            get { return _sysConfigOrigin; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //CII.LAR.Algorithm.Coordinate.GetCoordinate().TestMatrix(new List<System.Drawing.Point>() {
                //new System.Drawing.Point(1500, 1500),
                //new System.Drawing.Point(1600, 1500),
                //new System.Drawing.Point(1500, 1600)});

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                _sysConfig = SysConfig.Load();
                _sysConfigOrigin = SysConfig.Load();

                CII.Library.LoadingForm frm = new Library.LoadingForm();
                Bitmap bitmap = new Bitmap("test.png");
                frm.SetLoadingImage((Image)bitmap);
                frm.SetLoadingName("Welcome");
                frm.ShowDialog();

                //SendCommand sendCmd40 = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
                //RecvCommand recvCmd40 = (RecvCommand)PortManager.GetInstance().Send("LAR", sendCmd40);
                //var v40 = recvCmd40.GetBytes();
                //var m1Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Status);
                //var m1Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Result);
                //var m1CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1CompleteSteps);
                //var m1SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1SumSteps);

                //var m2Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Status);
                //var m2Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Result);
                //var m2CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2CompleteSteps);
                //var m2SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2SumSteps);

                //byte[] temp = new byte[] { 0x01, 0x21, 0x01, 0xFE, 0x60, 0x66, 0x00, 0x07, 0x61, 0x01, 0x00, 0x00, 0x00, 0x00, 0x32};
                ////var v = BitConverter.GetBytes(Protocol.CRC16.Compute(temp));
                //ComTestForm view = new ComTestForm();
                //view.StartPosition = FormStartPosition.CenterScreen;
                //IController controller = new IController(view);

                //Application.Run(view);

                //string pipeName = GlobalConfig.PortManagerPipeName;
                //string busName = GlobalConfig.PortManagerCOMBusName;
                //string busPort = GlobalConfig.PortManagerCOMBusPort;
                //string busBaud = GlobalConfig.PortManagerCOMBusBaud;
                //string busDataBit = GlobalConfig.PortManagerCOMBusDataBit;
                //string busStopBit = GlobalConfig.PortManagerCOMBusStopBit;
                //string busProtocolName = GlobalConfig.PortManagerProtocolName;
                //string busProtocolRouterPort = GlobalConfig.PortManagerRouterPort;
                //string pcAddress = GlobalConfig.PortManagerPCAddress;
                //if (CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName] != null &&
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName) != null &&
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busPort) != null)
                //{
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busPort).value = "COM3";
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busBaud).value = "115200";
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busDataBit).value = "8";
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busName).GetProperty(busStopBit).value = "1";
                //}
                //var v1 = CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName).GetProperty(busProtocolRouterPort);
                //var V2 = v1.GetProperty(pcAddress);
                ////PC
                //if (CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName] != null &&
                //     CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName) != null &&
                //     CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName).GetProperty(busProtocolRouterPort) != null &&
                //     CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName).GetProperty(busProtocolRouterPort).GetProperty(pcAddress) != null)
                //{
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().pipes[pipeName].GetProperty(busProtocolName).GetProperty(busProtocolRouterPort).GetProperty(pcAddress).value = "0xFE";
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().Save();
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().Reset();
                //    CII.Library.CIINet.Manager.PortManager.GetInstance().Open();
                //}

                entryForm = new EntryForm();

                expManager = new ExpManager();

                Application.Run(entryForm);
            }
            catch (Exception ex)
            {
                Entry.LogException(ex);
            }
            finally
            {
                GC.Collect();
                //环境退出
                Environment.Exit(0);
            }
        }
    }
}
