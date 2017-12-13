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
    public class ToolLine: ToolObject
    {
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(ZWPictureBox)).GetObject("Line")));

        public ToolLine()
        {
            Cursor = s_cursor;
            clickCount = 0;
        }

        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            clickCount++;
            if (clickCount % 2 == 1)
            {
                base.OnMouseDown(pictureBox, e);
                drawObject = new DrawLine(pictureBox, startPoint.X, startPoint.Y, startPoint.X + 1, startPoint.Y + 1);
                AddNewObject(pictureBox, drawObject);
            }
        }

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            pictureBox.Cursor = Cursor;

            if (clickCount % 2 == 1)
            {
                Point point = Point.Empty;
                if (Program.ExpManager.MachineStatus == MachineStatus.LiveVideo)
                {
                    point = new Point(e.X, e.Y);
                    pictureBox.GraphicsList[0].MoveHandleTo(pictureBox, point, 2);
                }
                else if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                {
                    point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
                    pictureBox.GraphicsList[0].MoveHandleTo(pictureBox, point, 2);
                    pictureBox.Refresh();
                }
            }
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            if (clickCount % 2 == 0)
            {
                if (Program.ExpManager.MachineStatus == MachineStatus.LiveVideo)
                {
                    endPoint = new Point(e.X, e.Y);
                }
                else if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                {
                    endPoint = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
                }
                Rectangle rectangle = new Rectangle(new Point(startPoint.X - 1, startPoint.Y - 1), new Size(2, 2));
                if (rectangle.Contains(endPoint))
                {
                    pictureBox.GraphicsList.DeleteDrawObject(drawObject);
                    if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                        pictureBox.Invalidate();
                }
                else
                {
                    pictureBox.GraphicsList[0].UpdateStatisticsInformation();
                    pictureBox.ActiveTool = DrawToolType.Line;
                }
            }
        }
    }
}
