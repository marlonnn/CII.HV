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
            new MemoryStream((byte[])new ResourceManager(typeof(EntryForm)).GetObject("Pencil")));

        /// <summary>
        /// used for double click to end drawing polygon gate when in Continuous mode
        /// </summary>
        private bool cancelNewFlag = false;
        public ToolPolyLine()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(VideoControl videoControl, MouseEventArgs e)
        {
            // operations are done in OnMouseUp
        }

        public override void OnMouseUp(VideoControl videoControl, MouseEventArgs e)
        {
            if (cancelNewFlag)
            {
                cancelNewFlag = false;
                return;
            }

            // if new object creation is canceled
            if (!videoControl.CreatingDrawObject && newPolyLine != null)
            {
                return;
            }

            if (newPolyLine == null)
            {
                Point point = e.Location;
                newPolyLine = new DrawPolyLine(videoControl, point.X, point.Y, point.X + 1, point.Y + 1);
                AddNewObject(videoControl, newPolyLine);
            }
            else
            {
                // polygon gate should have at least 3 points
                if (newPolyLine.CloseToFirstPoint(e.Location) && newPolyLine.PointCount > 3)
                {
                    newPolyLine.RemovePointAt(newPolyLine.PointCount - 1); // remove the last added point, it is closed to first
                    EndCreating(videoControl);
                    return;
                }
                // Drawing is in process, so simply add a new point
                Point point = e.Location;
                newPolyLine.AddPoint(videoControl, point, true);
            }
            videoControl.Capture = false;
            videoControl.Invalidate();
        }

        public override void OnMouseMove(VideoControl videoControl, MouseEventArgs e)
        {
            videoControl.Cursor = Cursor;
            // if new object creation is canceled
            if (!videoControl.CreatingDrawObject) return;

            if (newPolyLine == null)
                return; // precaution

            Point point = e.Location;

            // move last point
            newPolyLine.MoveLastHandleTo(videoControl, point);
            videoControl.Invalidate();
        }

        public override void OnMouseLeave(VideoControl videoControl, System.EventArgs e)
        {
        }

        public override void OnCancel(VideoControl videoControl, bool cancelSelection)
        {
            base.OnCancel(videoControl, cancelSelection);

            newPolyLine = null;
        }

        /// <summary>
        /// Mouse button is double clicked
        /// New Polyline is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnDoubleClick(VideoControl videoControl, MouseEventArgs e)
        {
            if (newPolyLine == null)
                return;

            newPolyLine.RemovePointAt(newPolyLine.PointCount - 1); // remove the last added point, it is duplicated
            // polygon gate should have at least 3 points
            if (newPolyLine.PointCount < 3)
                return;

            EndCreating(videoControl);

            // there is a mouse up event after double click event when in Continuous mode
            cancelNewFlag = true;

            videoControl.GraphicsList[0].UpdateStatisticsInformation();
            videoControl.ActiveTool = DrawToolType.PolyLine;
        }

        private void EndCreating(VideoControl videoControl)
        {
            newPolyLine.Creating = false;
            newPolyLine = null;
        }
    }
}
