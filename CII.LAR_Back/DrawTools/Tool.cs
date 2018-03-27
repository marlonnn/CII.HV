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
        /// <param name="videoControl"></param>
        /// <param name="e"></param>
        public virtual void OnMouseDown(VideoControl videoControl, MouseEventArgs e)
        {
            startPoint = new Point(e.X, e.Y);
        }


        /// <summary>
        /// Mouse is moved, left mouse button is pressed or none button is pressed
        /// </summary>
        /// <param name="videoControl"></param>
        /// <param name="e"></param>
        public virtual void OnMouseMove(VideoControl videoControl, MouseEventArgs e)
        {
        }
        public virtual void OnMouseMoveZoom(VideoControl videoControl, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Left mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseUp(VideoControl videoControl, MouseEventArgs e)
        {
            endPoint = new Point(e.X, e.Y);
        }
        public virtual void OnMouseUpZoom(VideoControl videoControl, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Mouse leave draw area
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseLeave(VideoControl videoControl, EventArgs e)
        {

        }

        /// <summary>
        /// Mouse button is double clicked
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnDoubleClick(VideoControl videoControl, MouseEventArgs e)
        {
        }

        /// <summary>
        /// call when press "Escape" key
        /// </summary>
        public virtual void OnCancel(VideoControl videoControl, bool cancelSelection)
        {
        }

        public virtual void OnKeyMove(VideoControl videoControl, Keys keyData, bool isPressCtrl)
        {

        }

        public virtual void OnKeyUp(VideoControl videoControl, Keys keyData, bool isPressCtrl)
        {

        }
    }
}
