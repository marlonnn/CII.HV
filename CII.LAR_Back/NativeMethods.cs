using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Security.Permissions;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms.VisualStyles;
using System.Globalization;

namespace CII.LAR
{

    internal static class NativeMethods
    {
        // Constants.
        // Copied from winuser.h

        public const int WM_CLOSE = 0x10;
        public const int WM_COMMAND = 0x111;
        public const int WM_SETREDRAW = 0x000B;

        public const string CLS_BUTTON = "BUTTON";
        public const string CLS_STATIC = "STATIC";

        public const int SS_ICON = 3;

        public const int GWL_STYLE = -16;
        public const int GWL_ID = -12;

        public const int PM_NOREMOVE = 0x0000;

        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOREDRAW = 0x0008;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_FRAMECHANGED = 0x0020; /* The frame changed: send WM_NCCALCSIZE */
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_HIDEWINDOW = 0x0080;
        public const int SWP_NOCOPYBITS = 0x0100;
        public const int SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        public const int SWP_NOSENDCHANGING = 0x0400; /* Don't send WM_WINDOWPOSCHANGING */

        //  Dialog Box Command IDs
        public const int IDOK = 1;
        public const int IDCANCEL = 2;
        public const int IDABORT = 3;
        public const int IDRETRY = 4;
        public const int IDIGNORE = 5;
        public const int IDYES = 6;
        public const int IDNO = 7;
        public const int IDCLOSE = 8;
        public const int IDHELP = 9;
        public const int IDTRYAGAIN = 10;
        public const int IDCONTINUE = 11;

        // Button notification code
        public const int BN_CLICKED = 0;

        public const int SM_CXMENUCHECK = 71;
        public const int SM_CYMENUCHECK = 72;
        public const int SM_CXEDGE = 45;
        public const int SM_CYEDGE = 46;
        public const int SM_CXVSCROLL = 2;
        public const int SM_CYHSCROLL = 3;

        // Window Styles
        public const int WS_HSCROLL = 0x00100000;
        public const int WS_VSCROLL = 0x00200000;

        //Clipboard
        public const int WM_CLIPBOARDUPDATE = 0x031D;

        public static IntPtr HWND_MESSAGE = new IntPtr(-3);

        // Methods
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hWnd, string text);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr FindWindow(string className, string caption);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string caption);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int GetWindowLong(IntPtr hWnd, int index);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SetWindowLong(IntPtr hWnd, int index, IntPtr newLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc callback, IntPtr param);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder className, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref NativeMethods.RECT rect);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref NativeMethods.RECT rect);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ScreenToClient(IntPtr hWnd, [In, Out] ref NativeMethods.POINT point);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int nIndex);
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);
        [DllImport("user32.dll")]
        public static extern bool MessageBeep(uint uType);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr handle, uint filterMin, uint filterMax, uint flags);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [DllImport("user32.dll")]
        public extern static bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

        [DllImport("user32.dll")]
        public extern static bool ShutdownBlockReasonDestroy(IntPtr hWnd);

        // See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        public static IntPtr MakeWParam(int lowWord, int highWord)
        {
            int wparam = highWord << 16;
            wparam |= (lowWord & 0xffff);

            return new IntPtr(wparam);
        }

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        // structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rectangle r)
            {
                this.left = r.Left;
                this.top = r.Top;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public static NativeMethods.RECT FromXYWH(int x, int y, int width, int height)
            {
                return new NativeMethods.RECT(x, y, x + width, y + height);
            }

            public Size Size
            {
                get
                {
                    return new Size(this.right - this.left, this.bottom - this.top);
                }
            }

            public override string ToString()
            {
                return string.Format("{0},{1},{2},{3}", this.left, this.top, this.right - this.left, this.bottom - this.top);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int x;
            public int y;
        }

        // delegates

        internal delegate bool EnumChildProc(IntPtr hWnd, IntPtr param);

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }

        /// <summary>
        /// get non XP style scaling (bitmap stretching) factor
        /// </summary>
        /// <returns></returns>
        public static float GetNonXpStyleScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }

        // Win32 Constants
        public const int SB_HORZ = 0;
        public const int SB_VERT = 1;
        public const int SB_CTL = 2;
        public const int SB_BOTH = 3;

        // Win32 Functions
        [DllImport("user32.dll")]
        public static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

    }

}