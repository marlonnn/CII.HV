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
        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {

            if (pictureBox.CreatingDrawObject)
            {
                if (e.Button == MouseButtons.Left)
                {
                    pictureBox.GraphicsList[0].UpdateStatisticsInformation();
                    pictureBox.ActiveTool = DrawToolType.Pointer;
                }
            }
        }

        /// <summary>
        /// left mouse is leave
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="e"></param>
        public override void OnMouseLeave(ZWPictureBox pictureBox, EventArgs e)
        {
            OnMouseUp(pictureBox, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
        }

        /// <summary>
        /// call when press "Escape" key
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="cancelSelection"></param>
        public override void OnCancel(ZWPictureBox pictureBox, bool cancelSelection)
        {
            // cancel adding 
            if (pictureBox.GraphicsList.Count > 0 && pictureBox.GraphicsList[0].Creating)
            {
                pictureBox.GraphicsList.DeleteLastAddedObject();
            }
        }

        /// <summary>
        /// add new object to picturebox
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="o"></param>
        protected void AddNewObject(ZWPictureBox pictureBox, DrawObject o)
        {
            pictureBox.GraphicsList.UnselectAll();

            o.Selected = true;
            o.Creating = true;

            pictureBox.GraphicsList.Add(o);
        }
    }
}
