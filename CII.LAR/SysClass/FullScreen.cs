using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.SysClass
{
    /// <summary>
    /// This class will help you to hide or show 
    /// a winform in full screen mode. 
    /// Any one can use this source code without restriction.
    /// I'm not responsible for any error.
    /// Author: Zhong Wen 2017/10/11
    /// </summary>
    public class FullScreen
    {
        private Form form;
        private FormWindowState windowState;
        private FormBorderStyle borderStyle;
        private Rectangle bounds;
        private bool fullScreen;
        public bool IsFullScreen
        {
            get { return this.fullScreen; }
        }

        /// <summary>
        /// Full screen constructor.
        /// </summary>
        /// <param name="form">The WinForm to be show or hide as full screen</param>
        public FullScreen(Form form)
        {
            this.form = form;
            fullScreen = false;
        }

        /// <summary>
        /// Show or hide full screen mode
        /// </summary>
        public void ShowFullScreen()
        {
            // set full screen
            if (!fullScreen)
            {
                // Get the WinForm properties
                borderStyle = form.FormBorderStyle;
                bounds = form.Bounds;
                windowState = form.WindowState;

                // set to false to avoid site effect
                form.Visible = false;

                HandleTaskBar.hideTaskBar();

                // set new properties
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;

                form.Visible = true;
                fullScreen = true;
            }
        }

        public void ResetFullScreen()
        {
            if (fullScreen)
            {
                // reset full screen
                // reset the normal WinForm properties
                // always set WinForm.Visible to false to avoid site effect
                form.Visible = false;
                form.WindowState = windowState;
                form.FormBorderStyle = borderStyle;
                form.Bounds = bounds;

                HandleTaskBar.showTaskBar();

                form.Visible = true;

                // Not in full screen mode
                fullScreen = false;
            }
        }
        /// <summary>
        /// You can use this to reset the Taskbar in case of error.
        /// I don't want to handle exception in this class.
        /// You can change it if you like!
        /// </summary>
        public void ResetTaskBar()
        {
            HandleTaskBar.showTaskBar();
        }
    }
}
