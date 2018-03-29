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
    /// Base class for all tools which create new graphic object
    /// Author:Zhong Wen 2017/07/25
    /// </summary>
    public abstract class ToolObject : Tool
    {
        protected int clickCount;

        protected DrawObject drawObject;

        private Cursor cursor;

        [System.ComponentModel.Localizable(false)]

        /// <summary>
        /// Tool cursor.
        /// </summary>
        public Cursor Cursor
        {
            get
            {
                return cursor;
            }
            set
            {
                cursor = value;
            }
        }

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {

            if (richPictureBox.CreatingDrawObject)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //richPictureBox.GraphicsList[0].UpdateStatisticsInformation();
                    richPictureBox.ActiveTool = DrawToolType.Pointer;
                }
            }
        }

        /// <summary>
        /// left mouse is leave
        /// </summary>
        /// <param name="richPictureBox"></param>
        /// <param name="e"></param>
        public override void OnMouseLeave(RichPictureBox richPictureBox, EventArgs e)
        {
            OnMouseUp(richPictureBox, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
        }

        /// <summary>
        /// call when press "Escape" key
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="cancelSelection"></param>
        public override void OnCancel(RichPictureBox richPictureBox, bool cancelSelection)
        {
            // cancel adding 
            if (richPictureBox.GraphicsList.Count > 0 && richPictureBox.GraphicsList[0].Creating)
            {
                richPictureBox.GraphicsList.DeleteLastAddedObject();
            }
        }

        /// <summary>
        /// add new object to picturebox
        /// </summary>
        /// <param name="richPictureBox"></param>
        /// <param name="o"></param>
        protected void AddNewObject(RichPictureBox richPictureBox, DrawObject o)
        {
            richPictureBox.GraphicsList.UnselectAll();

            o.Selected = true;
            o.Creating = true;

            richPictureBox.GraphicsList.Add(o);
        }
    }
}
