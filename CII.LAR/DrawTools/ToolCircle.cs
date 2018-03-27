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
    /// Draw circle shape tool
    /// Author:Zhong Wen 2017/07/27
    /// </summary>
    public class ToolCircle : ToolObject
    {
        private static Cursor s_cursor = new Cursor(
            new MemoryStream((byte[])new ResourceManager(typeof(EntryForm)).GetObject("Cross")));

        public ToolCircle()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            Point point = e.Location;
            AddNewObject(richPictureBox, new DrawCircle(richPictureBox, new PointF(point.X, point.Y)));
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            //richPictureBox.Cursor = Cursor;

            //if (e.Button == MouseButtons.Left)
            //{
            //    Point point = new Point(e.X, e.Y);
            //    richPictureBox.GraphicsList[0].MoveHandleTo(richPictureBox, point, 5);
            //    richPictureBox.Refresh();
            //}
        }
    }
}
