using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Draw multiple circle shape tool
    /// Author:Zhong Wen 2017/10/10
    /// </summary>
    public class ToolMultipleCircle : ToolObject
    {
        private static Cursor s_cursor = new Cursor(
            new MemoryStream((byte[])new ResourceManager(typeof(ZWPictureBox)).GetObject("Cross")));

        public ToolMultipleCircle()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            Point point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
            AddNewObject(pictureBox, new DrawMultipleCircle(pictureBox, new PointF(point.X, point.Y)));
        }

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            if (pictureBox.CreatingDrawObject)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX),
                        (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
                    pictureBox.GraphicsList[0].MoveHandleTo(pictureBox, point, 2);
                    pictureBox.Refresh();
                }
            }
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            //base.OnMouseUp(pictureBox, e);
        }
    }
}
