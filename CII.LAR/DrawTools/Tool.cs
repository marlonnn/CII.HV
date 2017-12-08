using CII.LAR.UI;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Left nous button is pressed
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="e"></param>
        public virtual void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
        }


        /// <summary>
        /// Mouse is moved, left mouse button is pressed or none button is pressed
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="e"></param>
        public virtual void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
        }
        public virtual void OnMouseMoveZoom(ZWPictureBox pictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Left mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
        }
        public virtual void OnMouseUpZoom(ZWPictureBox pictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Mouse leave draw area
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseLeave(ZWPictureBox pictureBox, EventArgs e)
        {

        }

        /// <summary>
        /// Mouse button is double clicked
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnDoubleClick(ZWPictureBox pictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// call when press "Escape" key
        /// </summary>
        public virtual void OnCancel(ZWPictureBox pictureBox, bool cancelSelection)
        {
        }

        public virtual void OnKeyMove(ZWPictureBox pictureBox, Keys keyData, bool isPressCtrl)
        {

        }

        public virtual void OnKeyUp(ZWPictureBox pictureBox, Keys keyData, bool isPressCtrl)
        {

        }
    }
}
