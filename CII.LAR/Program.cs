using CII.Ins.Business.Entry.LAR;
using CII.Ins.Model.Command.LAR;
using CII.Ins.Model.GlobalConfig;
using CII.LAR.SysClass;
using CII.Library.CIINet.Commands;
using CII.Library.CIINet.Manager;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CII.LAR
{
    static class Program
    {
        private static double dpiFactor = 1;
        /// <summary>
        /// system dpi setting, default is 100%
        /// </summary>
        public static double DpiFactor
        {
            get { return dpiFactor; }
            set { dpiFactor = value; }
        }

        private static float dpiX = 96;
        /// <summary>
        /// system dpiX setting, default is 96
        /// </summary>
        public static float DpiX
        {
            get { return dpiX; }
            set { dpiX = value; }
        }

        private static MainForm entryForm;

        /// <summary>
        /// The reference of program main form
        /// </summary>
        public static MainForm EntryForm
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

        private static void Initialize()
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
            DpiX = g.DpiX;
            DpiFactor = g.DpiX / 96.0;
            g.Dispose();
        }
        public static bool AppInstance()
        {
            Process[] MyProcesses = Process.GetProcesses();
            int i = 0;
            foreach (Process MyProcess in MyProcesses)
            {
                if (MyProcess.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    i++;
                }
            }
            return (i > 1) ? true : false;
        }

        private static Mutex mutexGlobal = null; 
        static bool CreateMutex()
        {
            // mutex name
            string name = "b64b2340-84dc-454d-81e6-1123ccd445b2";

            // try to obtain the local mutex used before 1.2.5
            bool result = false;
            mutex = new Mutex(true, name, out result);

            // try to obatin the global mutex used start from 1.2.5
            if (result) mutexGlobal = new Mutex(true, "Global\\" + name, out result);

            return result;
        }

        private static Mutex mutex;
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

                //mutex = new Mutex(true, "OnlyRun");
                if (CreateMutex())
                {
                    Initialize();
                    _sysConfig = SysConfig.Load();
                    _sysConfigOrigin = SysConfig.Load();

                    entryForm = new MainForm();

                    expManager = new ExpManager();

                    Application.Run(entryForm);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.StrProgramExit, Properties.Resources.StrWarning, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }

                if (mutex != null)
                {
                    mutex.ReleaseMutex();
                }
                if (mutexGlobal != null)
                {
                    mutexGlobal.ReleaseMutex();
                }
            }
            catch (Exception ex)
            {
                Entry.LogException(ex);
                LogHelper.GetLogger<MainForm>().Error(ex.Message);
                LogHelper.GetLogger<MainForm>().Error(ex.StackTrace);
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
