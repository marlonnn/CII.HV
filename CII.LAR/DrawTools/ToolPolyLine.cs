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
    public class ToolPolyLine : ToolObject
    {
        private DrawPolyLine newPolyLine;

        private static Cursor s_cursor = new Cursor(
            new MemoryStream((byte[])new ResourceManager(typeof(MainForm)).GetObject("Pencil")));

        /// <summary>
        /// used for double click to end drawing polygon gate when in Continuous mode
        /// </summary>
        private bool cancelNewFlag = false;
        public ToolPolyLine()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            // operations are done in OnMouseUp
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (cancelNewFlag)
            {
                cancelNewFlag = false;
                return;
            }

            // if new object creation is canceled
            if (!richPictureBox.CreatingDrawObject && newPolyLine != null)
            {
                return;
            }

            if (newPolyLine == null)
            {
                Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
                newPolyLine = new DrawPolyLine(richPictureBox, point.X, point.Y, point.X + 1, point.Y + 1);
                AddNewObject(richPictureBox, newPolyLine);
            }
            else
            {
                // polygon gate should have at least 3 points
                if (newPolyLine.CloseToFirstPoint(e.Location) && newPolyLine.PointCount > 3)
                {
                    newPolyLine.RemovePointAt(newPolyLine.PointCount - 1); // remove the last added point, it is closed to first
                    EndCreating(richPictureBox);
                    return;
                }
                // Drawing is in process, so simply add a new point
                Point point = e.Location;
                newPolyLine.AddPoint(richPictureBox, point, true);
            }
            richPictureBox.Capture = false;
            richPictureBox.Invalidate();
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            richPictureBox.Cursor = Cursor;
            // if new object creation is canceled
            if (!richPictureBox.CreatingDrawObject) return;

            if (newPolyLine == null)
                return; // precaution

            Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));

            // move last point
            base.OnMouseMove(richPictureBox, e);
            newPolyLine.MoveLastHandleTo(richPictureBox, point);
            richPictureBox.Invalidate();
        }

        public override void OnMouseLeave(RichPictureBox richPictureBox, System.EventArgs e)
        {
        }

        public override void OnCancel(RichPictureBox richPictureBox, bool cancelSelection)
        {
            base.OnCancel(richPictureBox, cancelSelection);

            newPolyLine = null;
        }

        /// <summary>
        /// Mouse button is double clicked
        /// New Polyline is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnDoubleClick(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (newPolyLine == null)
                return;

            newPolyLine.RemovePointAt(newPolyLine.PointCount - 1); // remove the last added point, it is duplicated
            // polygon gate should have at least 3 points
            if (newPolyLine.PointCount < 3)
                return;

            EndCreating(richPictureBox);

            // there is a mouse up event after double click event when in Continuous mode
            cancelNewFlag = true;

            richPictureBox.GraphicsList[0].UpdateStatisticsInformation();
            richPictureBox.ActiveTool = DrawToolType.PolyLine;
        }

        private void EndCreating(RichPictureBox richPictureBox)
        {
            newPolyLine.Creating = false;
            newPolyLine = null;
        }
    }
}
