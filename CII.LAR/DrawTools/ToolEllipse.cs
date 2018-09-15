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
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(MainForm)).GetObject("Ellipse")));

        public ToolEllipse()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
            richPictureBox.DrawObject = null;
            clickCount++;
            if (clickCount % 2 == 1)
            {
                //base.OnMouseDown(richPictureBox, e);
                startPoint = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));

                drawObject = new DrawEllipse(richPictureBox, startPoint.X, startPoint.Y, startPoint.X, startPoint.Y, 0.6);

                AddNewObject(richPictureBox, drawObject);
            }
            else
            {
                endPoint = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
                Rectangle rectangle = new Rectangle(new Point(startPoint.X - 1, startPoint.Y - 1), new Size(2, 2));
                if (rectangle.Contains(endPoint))
                {
                    richPictureBox.GraphicsList.DeleteDrawObject(drawObject);
                    richPictureBox.Invalidate();
                }
            }
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
                if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
                richPictureBox.Cursor = Cursor;

                if (clickCount % 2 == 1)
                {
                    var p = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
                    Rectangle rectangle = new Rectangle(new Point(startPoint.X - 1, startPoint.Y - 1), new Size(2, 2));
                    if (rectangle.Contains(p)) return;

                    base.OnMouseMove(richPictureBox, e);
                    Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
                    if (richPictureBox.GraphicsList != null && richPictureBox.GraphicsList.Count > 0)
                    {
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
                    if (richPictureBox.GraphicsList[0] != null) richPictureBox.GraphicsList[0].Creating = false;
                    //richPictureBox.ActiveTool = DrawToolType.Pointer;
                }
            }
        }
    }
}
