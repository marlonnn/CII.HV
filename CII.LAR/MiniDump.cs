using CII.LAR.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CII.LAR
{
    public static class MiniDump
    {
        [Flags]
        public enum Typ : uint
        {
            // From dbghelp.h:
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpFilterMemory = 0x00000008,
            MiniDumpScanMemory = 0x00000010,
            MiniDumpWithUnloadedModules = 0x00000020,
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            MiniDumpFilterModulePaths = 0x00000080,
            MiniDumpWithProcessThreadData = 0x00000100,
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            MiniDumpWithoutOptionalData = 0x00000400,
            MiniDumpWithFullMemoryInfo = 0x00000800,
            MiniDumpWithThreadInfo = 0x00001000,
            MiniDumpWithCodeSegs = 0x00002000,
            MiniDumpWithoutAuxiliaryState = 0x00004000,
            MiniDumpWithFullAuxiliaryState = 0x00008000,
            MiniDumpWithPrivateWriteCopyMemory = 0x00010000,
            MiniDumpIgnoreInaccessibleMemory = 0x00020000,
            MiniDumpValidTypeFlags = 0x0003ffff,
        };

        [StructLayout(LayoutKind.Sequential, Pack = 4)]  // Pack=4 is important! So it works also for x64!
        struct MiniDumpExceptionInformation
        {
            public uint ThreadId;
            public IntPtr ExceptioonPointers;
            [MarshalAs(UnmanagedType.Bool)]
            public bool ClientPointers;
        }

        [DllImport("dbghelp.dll",
            EntryPoint = "MiniDumpWriteDump",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess,
            uint processId,
            IntPtr hFile,
            uint dumpType,
            ref MiniDumpExceptionInformation expParam,
            IntPtr userStreamParam,
            IntPtr callbackParam);

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        static extern uint GetCurrentThreadId();

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentProcess", ExactSpelling = true)]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentProcessId", ExactSpelling = true)]
        static extern uint GetCurrentProcessId();

        [DllImport("User32")]
        static extern int GetGuiResources(IntPtr hProcess, int uiFlags);

        public static bool Write(string fileName)
        {
            return Write(fileName, Typ.MiniDumpWithFullMemory);
        }

        public static bool Write(string fileName, Typ dumpTyp)
        {
            using (var fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                MiniDumpExceptionInformation exp;
                exp.ThreadId = GetCurrentThreadId();
                exp.ClientPointers = false;
                exp.ExceptioonPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers();
                bool bRet = MiniDumpWriteDump(
                  GetCurrentProcess(),
                  GetCurrentProcessId(),
                  fs.SafeFileHandle.DangerousGetHandle(),
                  (uint)dumpTyp,
                  ref exp,
                  IntPtr.Zero,
                  IntPtr.Zero);
                return bRet;
            }
        }

        private static void LogStackTrace(string fileName, string trace)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                TextWriter writer = new StreamWriter(fs);
                writer.Write(trace);
                writer.Close();
            }
        }

        /// <summary>
        /// convert local time to China Standard Time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToCSTTime(this DateTime dateTime)
        {
            try
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, "China Standard Time");
            }
            catch
            {
                return dateTime;
            }
        }

        private static void App_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionHandler(e.Exception);
        }

        private static void CD_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler(e.ExceptionObject as Exception);
        }

        public static void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CD_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(App_ThreadException);
        }

        private static void CleanUp()
        {
            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CD_UnhandledException);
            Application.ThreadException -= new System.Threading.ThreadExceptionEventHandler(App_ThreadException);
        }

        private static void ExceptionHandler(Exception e)
        {
            if (e == null) return;

            try
            {
                string path = Path.Combine(Application.StartupPath, "Dumps\\");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                string msg = "An unhandled exception occurred. \r\n\r\nError message: " +
                         e.Message + "\r\n\r\nStack Trace: " + e.StackTrace;

                string runingBit = "Running as " + (IntPtr.Size == 4 ? "32-bit" : "64-bit");

                Process process = Process.GetCurrentProcess();
                string swVersion = Application.ProductVersion + " Build Date: " +
                    System.IO.File.GetLastWriteTime(Application.ExecutablePath).ToCSTTime().ToString("yyyy-MM-dd HH:mm:ss") + "    " + runingBit
                        + "\r\n\r\nMemory: " + process.PrivateMemorySize64 / 1024 / 1024
                        + "    GDI Objects: " + GetGuiResources(process.Handle, 0)
                        + "    User Objects: " + GetGuiResources(process.Handle, 1);

                if (e is AggregateException) //handle task wrapped exceptions
                {
                    foreach (var inner in (e as AggregateException).InnerExceptions) msg += inner.Message + "\r\n\r\n";
                }

                LogStackTrace(path + ".txt", swVersion + "    " + "\r\n\r\n" + msg);
                //Write(path + ".dmp"); do not write dmp file, just log stack trace  
                string showMessage = string.Format("{0} {1}", Properties.Resources.StrUnknownErrorMsg,
                    Properties.Resources.StrCreateReport);

                if (e is OutOfMemoryException)
                {
                    showMessage = Properties.Resources.StrOutOfMemory;
                }
                else
                {
                    MessageBoxManager.OK = Properties.Resources.StrReport;
                    MessageBoxManager.Cancel = Properties.Resources.StrOK;
                    MessageBoxManager.Register();
                    DialogResult result = MessageBox.Show(showMessage, Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
                    MessageBoxManager.Reset();
                    if (result == DialogResult.OK && result != DialogResult.Cancel)
                    {
                        ProblemReportForm form = new ProblemReportForm();
                        form.ShowDialog();
                        form.Dispose();
                    }
                }
            }
            catch
            {
                CleanUp();
            }
        }


    }
}
