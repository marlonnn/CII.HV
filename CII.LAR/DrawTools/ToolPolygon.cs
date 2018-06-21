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
    /// draw polygon tool
    /// Author:Zhong Wen 2017/07/27
    /// </summary>
    public class ToolPolygon : ToolObject
    {
        private int lastX;
        private int lastY;
        private DrawPolygon newPolygon;
        private const int minDistance = 15 * 15;

        private static Cursor s_cursor = new Cursor(
            new MemoryStream((byte[])new ResourceManager(typeof(MainForm)).GetObject("Pencil")));
        public ToolPolygon()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
            newPolygon = new DrawPolygon(richPictureBox, point.X, point.Y, point.X + 1, point.Y + 1);
            AddNewObject(richPictureBox, newPolygon);

            lastX = point.X;
            lastY = point.Y;
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            richPictureBox.Cursor = Cursor;

            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (newPolygon == null)
            {
                return;
            }

            Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
            int distance = (point.X - lastX) * (point.X - lastX) + (point.Y - lastY) * (point.Y - lastY);

            if (distance < minDistance)
            {
                // Distance between last two points is less than minimum -
                // move last point
                newPolygon.MoveHandleTo(richPictureBox, point, newPolygon.PointCount);
            }
            else
            {
                // Add new point
                newPolygon.AddPoint(richPictureBox, point, false);
                lastX = point.X;
                lastY = point.Y;
            }
            richPictureBox.Invalidate();
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            newPolygon.Creating = false;
            newPolygon = null;

            base.OnMouseUp(richPictureBox, e);
        }
    }
}
