using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Base class for all drawing tools
    /// Author:Zhong Wen 2017/07/25
    /// </summary>
    public class Tool
    {
        protected Point endPoint = new Point(0, 0);
        protected Point lastPoint = new Point(0, 0);
        protected Point startPoint = new Point(0, 0);

        /// <summary>
        /// Left nous button is pressed
        /// </summary>
        /// <param name="richPictureBox"></param>
        /// <param name="e"></param>
        public virtual void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            startPoint = new Point(e.X, e.Y);
        }


        /// <summary>
        /// Mouse is moved, left mouse button is pressed or none button is pressed
        /// </summary>
        /// <param name="richPictureBox"></param>
        /// <param name="e"></param>
        public virtual void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            DelegateClass.GetDelegate().ChangeSysFunctionHandler?.Invoke();
        }
        public virtual void OnMouseMoveZoom(RichPictureBox richPictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Left mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            endPoint = new Point(e.X, e.Y);
        }
        public virtual void OnMouseUpZoom(RichPictureBox richPictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Mouse leave draw area
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseLeave(RichPictureBox richPictureBox, EventArgs e)
        {

        }

        /// <summary>
        /// Mouse button is double clicked
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnDoubleClick(RichPictureBox richPictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// call when press "Escape" key
        /// </summary>
        public virtual void OnCancel(RichPictureBox richPictureBox, bool cancelSelection)
        {
        }

        public virtual void OnKeyMove(RichPictureBox richPictureBox, Keys keyData, bool isPressCtrl)
        {

        }

        public virtual void OnKeyUp(RichPictureBox richPictureBox, Keys keyData, bool isPressCtrl)
        {

        }
    }
}
