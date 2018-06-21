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
            new MemoryStream((byte[])new ResourceManager(typeof(MainForm)).GetObject("Cross")));

        public ToolMultipleCircle()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            Point point = e.Location;
            AddNewObject(richPictureBox, new DrawMultipleCircle(richPictureBox, new PointF(point.X, point.Y)));
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (richPictureBox.CreatingDrawObject)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point point = e.Location;
                    richPictureBox.GraphicsList[0].MoveHandleTo(richPictureBox, point, 2);
                    richPictureBox.Refresh();
                }
            }
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            //base.OnMouseUp(richPictureBox, e);
        }
    }
}
