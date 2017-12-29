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
            new MemoryStream((byte[])new ResourceManager(typeof(EntryForm)).GetObject("Cross")));

        public ToolMultipleCircle()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(VideoControl videoControl, MouseEventArgs e)
        {
            Point point = e.Location;
            AddNewObject(videoControl, new DrawMultipleCircle(videoControl, new PointF(point.X, point.Y)));
        }

        public override void OnMouseMove(VideoControl videoControl, MouseEventArgs e)
        {
            if (videoControl.CreatingDrawObject)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point point = e.Location;
                    videoControl.GraphicsList[0].MoveHandleTo(videoControl, point, 2);
                    videoControl.Refresh();
                }
            }
        }

        public override void OnMouseUp(VideoControl videoControl, MouseEventArgs e)
        {
            //base.OnMouseUp(videoControl, e);
        }
    }
}
