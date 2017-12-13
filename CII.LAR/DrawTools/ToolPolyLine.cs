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
            new MemoryStream((byte[])new ResourceManager(typeof(ZWPictureBox)).GetObject("Pencil")));

        /// <summary>
        /// used for double click to end drawing polygon gate when in Continuous mode
        /// </summary>
        private bool cancelNewFlag = false;
        public ToolPolyLine()
        {
            Cursor = s_cursor;
        }

        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            // operations are done in OnMouseUp
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            if (cancelNewFlag)
            {
                cancelNewFlag = false;
                return;
            }

            // if new object creation is canceled
            if (!pictureBox.CreatingDrawObject && newPolyLine != null)
            {
                return;
            }

            if (newPolyLine == null)
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
                newPolyLine = new DrawPolyLine(pictureBox, point.X, point.Y, point.X + 1, point.Y + 1);
                AddNewObject(pictureBox, newPolyLine);
            }
            else
            {
                // polygon gate should have at least 3 points
                if (newPolyLine.CloseToFirstPoint(e.Location) && newPolyLine.PointCount > 3)
                {
                    newPolyLine.RemovePointAt(newPolyLine.PointCount - 1); // remove the last added point, it is closed to first
                    EndCreating(pictureBox);
                    return;
                }
                // Drawing is in process, so simply add a new point
                Point point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
                newPolyLine.AddPoint(pictureBox, point, true);
            }
            pictureBox.Capture = false;
            if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
            {
                pictureBox.Refresh();
            }
        }

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            pictureBox.Cursor = Cursor;
            // if new object creation is canceled
            if (!pictureBox.CreatingDrawObject) return;

            if (newPolyLine == null)
                return; // precaution

            Point point = Point.Empty;
            if (Program.ExpManager.MachineStatus == MachineStatus.LiveVideo)
            {
                point = e.Location;
            }
            else if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
            {
                point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));
            }

            // move last point
            newPolyLine.MoveLastHandleTo(pictureBox, point);
            if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
            {
                pictureBox.Refresh();
            }
        }

        public override void OnMouseLeave(ZWPictureBox pictureBox, System.EventArgs e)
        {
        }

        public override void OnCancel(ZWPictureBox pictureBox, bool cancelSelection)
        {
            base.OnCancel(pictureBox, cancelSelection);

            newPolyLine = null;
        }

        /// <summary>
        /// Mouse button is double clicked
        /// New Polyline is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnDoubleClick(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            if (newPolyLine == null)
                return;

            newPolyLine.RemovePointAt(newPolyLine.PointCount - 1); // remove the last added point, it is duplicated
            // polygon gate should have at least 3 points
            if (newPolyLine.PointCount < 3)
                return;

            EndCreating(pictureBox);

            // there is a mouse up event after double click event when in Continuous mode
            cancelNewFlag = true;

            pictureBox.GraphicsList[0].UpdateStatisticsInformation();
            pictureBox.ActiveTool = DrawToolType.PolyLine;
        }

        private void EndCreating(ZWPictureBox pictureBox)
        {
            newPolyLine.Creating = false;
            newPolyLine = null;
        }
    }
}
