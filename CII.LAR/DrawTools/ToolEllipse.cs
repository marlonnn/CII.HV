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
    /// Draw ellipse shape tool
    /// Author:Zhong Wen 2017/07/27
    /// </summary>
    public class ToolEllipse : ToolObject
    {
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(EntryForm)).GetObject("Ellipse")));

        public ToolEllipse()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            clickCount++;
            if (clickCount % 2 == 1)
            {
                base.OnMouseDown(richPictureBox, e);

                drawObject = new DrawEllipse(richPictureBox, startPoint.X, startPoint.Y, startPoint.X, startPoint.Y, 0.6);

                AddNewObject(richPictureBox, drawObject);
            }
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            richPictureBox.Cursor = Cursor;

            if (clickCount % 2 == 1)
            {
                Point point = e.Location;
                richPictureBox.GraphicsList[0].MoveHandleTo(richPictureBox, point, 5);
                richPictureBox.Refresh();
            }
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (clickCount % 2 == 0)
            {
                endPoint = e.Location;
                Rectangle rectangle = new Rectangle(new Point(startPoint.X - 1, startPoint.Y - 1), new Size(2, 2));
                if (rectangle.Contains(endPoint))
                {
                    richPictureBox.GraphicsList.DeleteDrawObject(drawObject);
                    richPictureBox.Invalidate();
                }
                else
                {
                    //richPictureBox.GraphicsList[0].UpdateStatisticsInformation();
                    richPictureBox.ActiveTool = DrawToolType.Ellipse;
                }
            }
        }
    }
}
