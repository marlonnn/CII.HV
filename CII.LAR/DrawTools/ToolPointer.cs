using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Normal mouse state 
    /// Author:Zhong Wen 2017/07/27
    /// </summary>
    public class ToolPointer : Tool
    {
        private enum SelectionMode
        {
            None,
            Select,
            NetSelection,   // group selection is active
            MoveLabel,
            Move,           // object(s) are moves
            Size,           // object is resized
            Drag,           // object is dragged
        }
        private bool wasMove;

        private SelectionMode selectMode = SelectionMode.None;

        private Rectangle dragBoxFromMouseDown;

        // Object which is currently resized:
        private DrawObject resizedObject;
        private int resizedObjectHandle;

        // Keep state about last and current point (used to move and resize objects)
        private Point lastPoint = new Point(0, 0);
        private Point startPoint = new Point(0, 0);
        public ToolPointer()
        {

        }

        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            wasMove = false;
            Point point = new Point(e.X, e.Y);
            selectMode = SelectionMode.None;
            dragBoxFromMouseDown = Rectangle.Empty;

            // Test for moving or resizing (only if control is selected, cursor is on the handle)
            foreach (DrawObject o in pictureBox.GraphicsList)
            {
                if (o is DrawCircle)
                {
                    continue;
                }
                DrawObject.HitTestResult htr = o.HitTest(pictureBox, point, false);
                if (!o.Selected)    // test for drag and drop
                {
                    if (htr.ElementType == DrawObject.ElementType.Gate)
                    {
                        Size dragSize = SystemInformation.DragSize;
                        dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                    e.Y - (dragSize.Height / 2)), dragSize);

                        selectMode = SelectionMode.Drag;
                    }
                }
                else if (htr.ElementType == DrawTools.DrawObject.ElementType.Handle)
                {
                    int handleNumber = htr.Index;
                    if (handleNumber > 0)
                    {
                        selectMode = SelectionMode.Size;

                        // keep resized object in class member
                        resizedObject = o;
                        resizedObjectHandle = handleNumber;

                        //Restore DrawPolygonFrame para
                        if (resizedObject is DrawPolyLine)
                        {
                            (resizedObject as DrawPolyLine).NeedReverseX = false;
                            (resizedObject as DrawPolyLine).NeedReverseY = false;
                            (resizedObject as DrawPolyLine).SetProportion = true;
                        }

                        pictureBox.GraphicsList.UnselectAll();
                        o.Selected = true;
                    }
                }
                else if (htr.ElementType == DrawObject.ElementType.Gate)
                {
                    selectMode = SelectionMode.Move;
                    pictureBox.Cursor = Cursors.SizeAll;
                    break;
                }
            }

            // Test for selection (cursor is on the object or label)
            if (selectMode == SelectionMode.None || selectMode == SelectionMode.Drag)
            {
                DrawObject o = null;
                int gateIndex = 0;
                for (int i = 0; i < pictureBox.GraphicsList.Count; i++)
                {
                    if (pictureBox.GraphicsList[i] is DrawCircle)
                    {
                        continue;
                    }
                    DrawObject.HitTestResult htr = pictureBox.GraphicsList[i].HitTest(pictureBox, point, true);
                    if (htr.ElementType == DrawObject.ElementType.Gate)
                    {
                        o = pictureBox.GraphicsList[i];
                        gateIndex = htr.Index;
                        selectMode = o.Selected ? SelectionMode.Move : SelectionMode.Select;
                        break;
                    }
                }
                if (o != null)
                {
                    if ((Control.ModifierKeys & Keys.Control) == 0 && !o.Selected)
                    {
                        pictureBox.GraphicsList.UnselectAll();
                    }
                    o.Selected = true;
                }

            }
            // un-select
            if (selectMode == SelectionMode.None || selectMode == SelectionMode.Drag)
            {
                // click on background
                if ((Control.ModifierKeys & Keys.Control) == 0)
                {
                    pictureBox.GraphicsList.UnselectAll();
                }
            }

            // Net selection
            if (selectMode == SelectionMode.None)
            {
                selectMode = SelectionMode.NetSelection;
            }
            lastPoint.X = e.X;
            lastPoint.Y = e.Y;
            startPoint.X = e.X;
            startPoint.Y = e.Y;

            pictureBox.Capture = true;
            pictureBox.Refresh();
        }

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            Point oldPoint = lastPoint;

            // set cursor when mouse button is not pressed
            if (e.Button == MouseButtons.None)
            {
                Cursor cursor = null;

                for (int i = 0; i < pictureBox.GraphicsList.Count; i++)
                {
                    if (!pictureBox.GraphicsList[i].Selected || (pictureBox.GraphicsList[i] is DrawCircle)) continue;

                    DrawTools.DrawObject.HitTestResult result = pictureBox.GraphicsList[i].HitTest(pictureBox, point, false);

                    if (result.ElementType == DrawTools.DrawObject.ElementType.Handle && result.Index > 0)
                    {
                        cursor = pictureBox.GraphicsList[i].GetHandleCursor(result.Index);
                        break;
                    }
                    else if (result.ElementType == DrawTools.DrawObject.ElementType.Label &&
                                pictureBox.GraphicsList[i].Selected)
                    {
                        cursor = Cursors.SizeAll;
                    }
                }

                if (cursor == null)
                    cursor = Cursors.Default;

                pictureBox.Cursor = cursor;
                return;
            }

            // Left button is not pressed
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            // Find difference between previous and current position
            int dx = e.X - lastPoint.X;
            int dy = e.Y - lastPoint.Y;

            lastPoint.X = e.X;
            lastPoint.Y = e.Y;

            if (dx == 0 && dy == 0)
            {
                return;
            }
            wasMove = true;

            if (selectMode == SelectionMode.Size)
            {
                if (resizedObject != null)
                {
                    resizedObject.MoveHandleTo(pictureBox, point, resizedObjectHandle);
                    pictureBox.Refresh();
                }
            }

            // move
            if (selectMode == SelectionMode.Move)
            {
                foreach (DrawObject o in pictureBox.GraphicsList.Selection)
                {
                    o.MovingOffset = new Point(o.MovingOffset.X + dx, o.MovingOffset.Y + dy);
                    // start drag and drop
                    if (!pictureBox.ClientRectangle.Contains(e.Location))
                    {
                        o.MovingOffset = Point.Empty;
                        pictureBox.Refresh();
                        pictureBox.DoDragDrop(o, DragDropEffects.Copy | DragDropEffects.Link);
                    }
                }

                pictureBox.Cursor = Cursors.SizeAll;
                pictureBox.Refresh();
            }

            //if (selectMode == SelectionMode.NetSelection)
            //{
            //    pictureBox.RectNetSelection = new Rectangle(startPoint.X, startPoint.Y, point.X - startPoint.X, point.Y - startPoint.Y);
            //    pictureBox.Refresh();
            //    return;
            //}
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            if (selectMode == SelectionMode.NetSelection)
            {
                //pictureBox.RectNetSelection = Rectangle.Empty;

                //if (Math.Abs(startPoint.X - lastPoint.X) > 3 || Math.Abs(startPoint.Y - lastPoint.Y) > 3)
                //{
                //    // Make group selection
                //    pictureBox.GraphicsList.SelectInRectangle(pictureBox,
                //        DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint));
                //}
                selectMode = SelectionMode.None;
            }

            if (selectMode == SelectionMode.Move && wasMove)
            {
                foreach (DrawObject o in pictureBox.GraphicsList.Selection)
                {
                    o.Move(pictureBox, o.MovingOffset.X, o.MovingOffset.Y);
                    o.MovingOffset = Point.Empty;
                }

                // gate could be moved, but lost selection
                foreach (DrawObject o in pictureBox.GraphicsList)
                {
                    o.MovingOffset = Point.Empty;
                }
                selectMode = SelectionMode.None;
                pictureBox.Refresh();
            }
            wasMove = false;
        }

        public override void OnCancel(ZWPictureBox pictureBox, bool cancelSelection)
        {

        }
    }
}
