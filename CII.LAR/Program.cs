using CII.Ins.Business.Entry.LAR;
using CII.Ins.Model.Command.LAR;
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

                CII.Library.LoadingForm frm = new Library.LoadingForm();
                Bitmap bitmap = new Bitmap("test.png");
                frm.SetLoadingImage((Image)bitmap);
                frm.SetLoadingName("Welcome");
                frm.ShowDialog();

                SendCommand sendCmd40 = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
                RecvCommand recvCmd40 = (RecvCommand)PortManager.GetInstance().Send("LAR", sendCmd40);
                var v40 = recvCmd40.GetBytes();
                var m1Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Status);
                var m1Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Result);
                var m1CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1CompleteSteps);
                var m1SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1SumSteps);

                var m2Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Status);
                var m2Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Result);
                var m2CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2CompleteSteps);
                var m2SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2SumSteps);

                //byte[] temp = new byte[] { 0x01, 0x21, 0x01, 0xFE, 0x60, 0x66, 0x00, 0x07, 0x61, 0x01, 0x00, 0x00, 0x00, 0x00, 0x32};
                ////var v = BitConverter.GetBytes(Protocol.CRC16.Compute(temp));
                //ComTestForm view = new ComTestForm();
                //view.StartPosition = FormStartPosition.CenterScreen;
                //IController controller = new IController(view);

                //Application.Run(view);

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
