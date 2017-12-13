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
            new MemoryStream((byte[])new ResourceManager(typeof(ZWPictureBox)).GetObject("Pencil")));
        public ToolPolygon()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            Point point = Point.Empty;
            if (Program.ExpManager.MachineStatus == MachineStatus.LiveVideo)
            {
                point = e.Location;
            }
            else if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
            {
                point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
            }
            newPolygon = new DrawPolygon(pictureBox, point.X, point.Y, point.X + 1, point.Y + 1);
            AddNewObject(pictureBox, newPolygon);

            lastX = point.X;
            lastY = point.Y;
        }

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            pictureBox.Cursor = Cursor;

            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (newPolygon == null)
            {
                return;
            }

            Point point = Point.Empty;
            if (Program.ExpManager.MachineStatus == MachineStatus.LiveVideo)
            {
                point = e.Location;
            }
            else if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
            {
                point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
            }

            int distance = (point.X - lastX) * (point.X - lastX) + (point.Y - lastY) * (point.Y - lastY);

            if (distance < minDistance)
            {
                // Distance between last two points is less than minimum -
                // move last point
                newPolygon.MoveHandleTo(pictureBox, point, newPolygon.PointCount);
            }
            else
            {
                // Add new point
                newPolygon.AddPoint(pictureBox, point, false);
                lastX = point.X;
                lastY = point.Y;
            }
            pictureBox.Refresh();
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            newPolygon.Creating = false;
            newPolygon = null;

            base.OnMouseUp(pictureBox, e);
        }
    }
}
