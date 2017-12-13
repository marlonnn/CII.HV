using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    static class Program
    {
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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //byte[] temp = new byte[] { 0x01, 0x21, 0x01, 0xFE, 0x60, 0x66, 0x00, 0x07, 0x61, 0x01, 0x00, 0x00, 0x00, 0x00, 0x32};
                ////var v = BitConverter.GetBytes(Protocol.CRC16.Compute(temp));
                //ComTestForm view = new ComTestForm();
                //view.StartPosition = FormStartPosition.CenterScreen;
                //IController controller = new IController(view);

                //Application.Run(view);

                expManager = new ExpManager();

                Application.Run(new EntryForm());
            }
            catch (Exception ex)
            {
            }
        }
    }
}
