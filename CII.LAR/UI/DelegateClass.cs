using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.SysClass;

namespace CII.LAR.UI
{
    public class DelegateClass
    {
        public delegate void CaptureDevice(string deviceMoniker);
        public CaptureDevice CaptureDeviceHandler;

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

        public delegate void VideoKeyUp(KeyEventArgs e);
        public VideoKeyUp VideoKeyUpHandler;

        /// <summary>
        /// button click delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name"></param>
        public delegate void ClickDelegate(object sender, CtrlType type);
        public ClickDelegate ClickDelegateHandler;

        /// <summary>
        /// 切换功能时判断当前子窗口是否为当前功能的窗口，
        /// 若不是，则切换到当前功能窗口
        /// </summary>
        public delegate void ChangeSysFunction();
        public ChangeSysFunction ChangeSysFunctionHandler;

        /// <summary>
        /// 在模拟模式下需要判断相机是否开启，开启则先要关闭相机
        /// </summary>
        public delegate void CheckCloseVideo();
        public CheckCloseVideo CheckCloseVideoHandler;

        public delegate void UpdateLense(Lense lense);
        public UpdateLense UpdateLenseHandler;
    }
}
