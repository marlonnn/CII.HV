using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public class DelegateClass
    {
        private static DelegateClass dc;
        public static DelegateClass GetDelegate()
        {
            if (dc == null)
            {
                dc = new DelegateClass();
            }
            return dc;
        }

        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="e"></param>
        public delegate void VideoKeyDown(KeyEventArgs e);
        public VideoKeyDown VideoKeyDownHandler;


        /// <summary>
        /// button click delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name"></param>
        public delegate void ClickDelegate(object sender, CtrlType type);
        public ClickDelegate ClickDelegateHandler;
    }
}
