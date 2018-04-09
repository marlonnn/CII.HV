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

        public ToolPointer()
        {

        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            wasMove = false;
            Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
            selectMode = SelectionMode.None;
            dragBoxFromMouseDown = Rectangle.Empty;

            // Test for moving or resizing (only if control is selected, cursor is on the handle)
            foreach (DrawObject o in richPictureBox.GraphicsList)
            {
                if (o is DrawCircle)
                {
                    continue;
                }
                DrawObject.HitTestResult htr = o.HitTest(richPictureBox, point, false);
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

                        richPictureBox.GraphicsList.UnselectAll();
                        o.Selected = true;
                    }
                }
                else if (htr.ElementType == DrawObject.ElementType.Gate)
                {
                    selectMode = SelectionMode.Move;
                    richPictureBox.Cursor = Cursors.SizeAll;
                    break;
                }
            }

            // Test for selection (cursor is on the object or label)
            if (selectMode == SelectionMode.None || selectMode == SelectionMode.Drag)
            {
                DrawObject o = null;
                int gateIndex = 0;
                for (int i = 0; i < richPictureBox.GraphicsList.Count; i++)
                {
                    if (richPictureBox.GraphicsList[i] is DrawCircle)
                    {
                        continue;
                    }
                    DrawObject.HitTestResult htr = richPictureBox.GraphicsList[i].HitTest(richPictureBox, point, true);
                    if (htr.ElementType == DrawObject.ElementType.Gate)
                    {
                        o = richPictureBox.GraphicsList[i];
                        gateIndex = htr.Index;
                        selectMode = o.Selected ? SelectionMode.Move : SelectionMode.Select;
                        break;
                    }
                }
                if (o != null)
                {
                    if ((Control.ModifierKeys & Keys.Control) == 0 && !o.Selected)
                    {
                        richPictureBox.GraphicsList.UnselectAll();
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
                    richPictureBox.GraphicsList.UnselectAll();
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

            richPictureBox.Capture = true;
            richPictureBox.Refresh();
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            Point point = new Point((int)(e.X / richPictureBox.Zoom - richPictureBox.OffsetX), (int)(e.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
            Point oldPoint = lastPoint;

            // set cursor when mouse button is not pressed
            if (e.Button == MouseButtons.None)
            {
                Cursor cursor = null;

                for (int i = 0; i < richPictureBox.GraphicsList.Count; i++)
                {
                    if (!richPictureBox.GraphicsList[i].Selected || (richPictureBox.GraphicsList[i] is DrawCircle)) continue;

                    DrawTools.DrawObject.HitTestResult result = richPictureBox.GraphicsList[i].HitTest(richPictureBox, point, false);

                    if (result.ElementType == DrawTools.DrawObject.ElementType.Handle && result.Index > 0)
                    {
                        cursor = richPictureBox.GraphicsList[i].GetHandleCursor(result.Index);
                        break;
                    }
                    else if (result.ElementType == DrawTools.DrawObject.ElementType.Label &&
                                richPictureBox.GraphicsList[i].Selected)
                    {
                        cursor = Cursors.SizeAll;
                    }
                }

                if (cursor == null)
                    cursor = Cursors.Default;

                richPictureBox.Cursor = cursor;
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
                    resizedObject.MoveHandleTo(richPictureBox, point, resizedObjectHandle);
                    richPictureBox.Refresh();
                }
            }

            // move
            if (selectMode == SelectionMode.Move)
            {
                foreach (DrawObject o in richPictureBox.GraphicsList.Selection)
                {
                    o.MovingOffset = new Point(o.MovingOffset.X + dx, o.MovingOffset.Y + dy);
                    // start drag and drop
                    if (!richPictureBox.ClientRectangle.Contains(e.Location))
                    {
                        o.MovingOffset = Point.Empty;
                        richPictureBox.Refresh();
                        richPictureBox.DoDragDrop(o, DragDropEffects.Copy | DragDropEffects.Link);
                    }
                }

                richPictureBox.Cursor = Cursors.SizeAll;
                richPictureBox.Refresh();
            }

            //if (selectMode == SelectionMode.NetSelection)
            //{
            //    richPictureBox.RectNetSelection = new Rectangle(startPoint.X, startPoint.Y, point.X - startPoint.X, point.Y - startPoint.Y);
            //    richPictureBox.Refresh();
            //    return;
            //}
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            if (selectMode == SelectionMode.NetSelection)
            {
                //richPictureBox.RectNetSelection = Rectangle.Empty;

                //if (Math.Abs(startPoint.X - lastPoint.X) > 3 || Math.Abs(startPoint.Y - lastPoint.Y) > 3)
                //{
                //    // Make group selection
                //    richPictureBox.GraphicsList.SelectInRectangle(richPictureBox,
                //        DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint));
                //}
                selectMode = SelectionMode.None;
            }

            if (selectMode == SelectionMode.Move && wasMove)
            {
                foreach (DrawObject o in richPictureBox.GraphicsList.Selection)
                {
                    o.Move(richPictureBox, o.MovingOffset.X, o.MovingOffset.Y);
                    o.MovingOffset = Point.Empty;
                }

                // gate could be moved, but lost selection
                foreach (DrawObject o in richPictureBox.GraphicsList)
                {
                    o.MovingOffset = Point.Empty;
                }
                selectMode = SelectionMode.None;
                richPictureBox.Refresh();
            }
            wasMove = false;
        }

        public override void OnCancel(RichPictureBox richPictureBox, bool cancelSelection)
        {

        }
    }
}
