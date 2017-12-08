using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    static class Program
    {
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
                //ComTestForm view = new ComTestForm();
                //view.StartPosition = FormStartPosition.CenterScreen;
                //IController controller = new IController(view);

                //Application.Run(view);

                Application.Run(new EntryForm());
            }
            catch (Exception ex)
            {
            }
        }
    }
}
