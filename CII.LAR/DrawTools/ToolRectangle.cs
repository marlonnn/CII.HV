﻿using CII.LAR.UI;
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
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(ZWPictureBox)).GetObject("Rectangle")));

        public ToolRectangle()
        {
            Cursor = s_cursor;
        }


        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            clickCount++;
            if (clickCount % 2 == 1)
            {
                startPoint = new Point(e.X, e.Y);
                drawObject = new DrawRectangle(pictureBox, startPoint.X, startPoint.Y, 1, 1);
                AddNewObject(pictureBox, drawObject);
            }
        }

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            pictureBox.Cursor = Cursor;

            if (pictureBox.CreatingDrawObject)
            {
                if (clickCount % 2 == 1)
                {
                    Point point = new Point(e.X, e.Y);
                    pictureBox.GraphicsList[0].MoveHandleTo(pictureBox, point, 5);
                    //pictureBox.Refresh();
                }
            }
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            if (clickCount % 2 == 0)
            {
                endPoint = new Point(e.X, e.Y);
                Rectangle rectangle = new Rectangle(new Point(startPoint.X - 1, startPoint.Y - 1), new Size(2, 2));
                if (rectangle.Contains(endPoint))
                {
                    pictureBox.GraphicsList.DeleteDrawObject(drawObject);
                    //pictureBox.Invalidate();
                }
                else
                {
                    pictureBox.GraphicsList[0].UpdateStatisticsInformation();
                    pictureBox.ActiveTool = DrawToolType.Rectangle;
                }
            }
        }
    }
}