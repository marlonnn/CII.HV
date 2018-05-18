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
    public class ToolRectangle : ToolObject
    {
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(EntryForm)).GetObject("Rectangle")));

        public ToolRectangle()
        {
            Cursor = s_cursor;
        }


        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
            clickCount++;
            if (clickCount % 2 == 1)
            {
                base.OnMouseMove(richPictureBox, e);
                startPoint = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
                drawObject = new DrawRectangle(richPictureBox, startPoint.X, startPoint.Y, 1, 1);
                AddNewObject(richPictureBox, drawObject);
            }
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
            richPictureBox.Cursor = Cursor;

            if (richPictureBox.CreatingDrawObject)
            {
                if (clickCount % 2 == 1)
                {
                    Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
                    richPictureBox.GraphicsList[0].MoveHandleTo(richPictureBox, point, 5);
                    richPictureBox.Invalidate();
                }
            }
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
            if (clickCount % 2 == 0)
            {
                endPoint = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
                Rectangle rectangle = new Rectangle(new Point(startPoint.X - 1, startPoint.Y - 1), new Size(2, 2));
                if (rectangle.Contains(endPoint))
                {
                    richPictureBox.GraphicsList.DeleteDrawObject(drawObject);
                    richPictureBox.Invalidate();
                }
                else
                {
                    //richPictureBox.GraphicsList[0].UpdateStatisticsInformation();
                    richPictureBox.GraphicsList[0].Creating = false;
                    //richPictureBox.ActiveTool = DrawToolType.Pointer;
                }
            }
        }
    }
}
