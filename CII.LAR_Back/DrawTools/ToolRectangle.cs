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


        public override void OnMouseDown(VideoControl videoControl, MouseEventArgs e)
        {
            clickCount++;
            if (clickCount % 2 == 1)
            {
                base.OnMouseDown(videoControl, e);
                drawObject = new DrawRectangle(videoControl, startPoint.X, startPoint.Y, 1, 1);
                AddNewObject(videoControl, drawObject);
            }
        }

        public override void OnMouseMove(VideoControl videoControl, MouseEventArgs e)
        {
            videoControl.Cursor = Cursor;

            if (videoControl.CreatingDrawObject)
            {
                if (clickCount % 2 == 1)
                {
                    Point point = e.Location;
                    videoControl.GraphicsList[0].MoveHandleTo(videoControl, point, 5);
                    videoControl.Refresh();
                }
            }
        }

        public override void OnMouseUp(VideoControl videoControl, MouseEventArgs e)
        {
            if (clickCount % 2 == 0)
            {
                endPoint = e.Location;
                Rectangle rectangle = new Rectangle(new Point(startPoint.X - 1, startPoint.Y - 1), new Size(2, 2));
                if (rectangle.Contains(endPoint))
                {
                    videoControl.GraphicsList.DeleteDrawObject(drawObject);
                    videoControl.Invalidate();
                }
                else
                {
                    videoControl.GraphicsList[0].UpdateStatisticsInformation();
                    videoControl.ActiveTool = DrawToolType.Rectangle;
                }
            }
        }
    }
}
