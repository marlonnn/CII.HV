using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestPictureBox
{
    /// <summary>
    /// This class will show or hide windows taskbar for full screen mode.
    /// Author: Zhong Wen 2017/10/11
    /// </summary>
    internal class HandleTaskBar
    {
        private const int SWP_HIDEWINDOW = 0x0080;
        private const int SWP_SHOWWINDOW = 0x0040;

        /// <summary>
        /// Default Constructor.
        /// </summary>
		public HandleTaskBar()
        {
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        private static extern int SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        /// <summary>
        /// Show the TaskBar.
        /// </summary>
        public static void showTaskBar()
        {
            int hWnd = FindWindow("Shell_TrayWnd", "");
            SetWindowPos(hWnd, 0, 0, 0, 0, 0, SWP_SHOWWINDOW);
        }

        /// <summary>
        /// Hide the TaskBar.
        /// </summary>
        public static void hideTaskBar()
        {
            int hWnd = FindWindow("Shell_TrayWnd", "");
            SetWindowPos(hWnd, 0, 0, 0, 0, 0, SWP_HIDEWINDOW);
        }
    }
}
