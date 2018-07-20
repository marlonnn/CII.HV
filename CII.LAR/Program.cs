﻿using CII.Ins.Business.Entry.LAR;
using CII.Ins.Model.Command.LAR;
using CII.Ins.Model.GlobalConfig;
using CII.LAR.SysClass;
using CII.Library.CIINet.Commands;
using CII.Library.CIINet.Manager;
using System;
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

                mutex = new Mutex(true, "OnlyRun");
                if (mutex.WaitOne(0, false))
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
