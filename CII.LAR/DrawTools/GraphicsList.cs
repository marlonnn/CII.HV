using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    using System.Drawing;
    using UI;
    using DrawList = List<DrawObject>;

    public class GraphicsList
    {
        private DrawList graphicsList;

        public event EventHandler<ArrayChangedEventArgs<DrawObject>> DrawObjsChanged;

        public GraphicsList()
        {
            graphicsList = new DrawList();
        }

        private void OnDrawObjsChanged(ArrayChangedEventArgs<DrawObject> e)
        {
            DrawObjsChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Count and this [nIndex] allow to read all graphics objects
        /// from GraphicsList in the loop.
        /// </summary>
        public int Count
        {
            get
            {
                return graphicsList.Count;
            }
        }

        public DrawObject this[int index]
        {
            get
            {
                if (index < 0 || index >= graphicsList.Count)
                    return null;

                return graphicsList[index];
            }
        }

        public IEnumerator<DrawObject> GetEnumerator()
        {
            return graphicsList.GetEnumerator();
        }

        public void Draw(Graphics g, RichPictureBox richPictureBox)
        {
            int n = graphicsList.Count;
            DrawObject o;

            // Enumerate list in reverse order to get first
            // object on the top of Z-order.
            for (int i = n - 1; i >= 0; i--)
            {
                if (richPictureBox == null)
                {
                    break;
                }
                o = graphicsList[i];
                g.ScaleTransform(richPictureBox.Zoom, richPictureBox.Zoom);
                g.TranslateTransform(richPictureBox.OffsetX, richPictureBox.OffsetY);
                o.Draw(g, richPictureBox);
                o.DrawTest(g, richPictureBox);
                g.ResetTransform();
                //if (o.Selected)
                //{
                //    o.DrawTracker(g, richPictureBox);
                //}
            }
        }

        /// <summary>
        /// Returns INumerable object which may be used for enumeration
        /// of selected objects.
        /// 
        /// Note: returning IEnumerable<DrawObject> breaks CLS-compliance
        /// (assembly CLSCompliant = true is removed from AssemblyInfo.cs).
        /// To make this program CLS-compliant, replace 
        /// IEnumerable<DrawObject> with IEnumerable. This requires
        /// casting to object at runtime.
        /// </summary>
        /// <value></value>
        public IEnumerable<DrawObject> Selection
        {
            get
            {
                return graphicsList.Where(o => o.Selected).ToList();
            }
        }

        public void Add(DrawObject obj, bool refreshWhenAdded = false)
        {
            graphicsList.Insert(0, obj);

            obj.ID = GetNextDrawObjectID();
            obj.Name = obj.Prefix + obj.ID.ToString();

            OnDrawObjsChanged(new ArrayChangedEventArgs<DrawObject>(obj, ArrayChangedType.ItemAdded, refreshWhenAdded));
        }

        public int GetNextDrawObjectID()
        {
            List<int> objectIDs = new List<int>();
            foreach (DrawObject o in graphicsList)
            {
                if (o is DrawCircle)
                {
                    continue;
                }
                objectIDs.Add(o.ID);
            }
            objectIDs.Sort();
            // find the id that larger than previous id plus one
            for (int i = 1; i < objectIDs.Count; i++)
                if (objectIDs[i] > objectIDs[i - 1] + 1) return objectIDs[i - 1] + 1;

            return objectIDs.LastOrDefault() + 1;
        }

        /// <summary>
        /// Remove object by index.
        /// Used for Undo.
        /// </summary>
        public void RemoveAt(int index)
        {
            DrawObject o = graphicsList[index];
            //             if (o.DrawArea != null) o.DrawArea.EndTextBoxEdit(false);            
            graphicsList.RemoveAt(index);

            //onGateRemoved();
            OnDrawObjsChanged(new ArrayChangedEventArgs<DrawObject>(o, ArrayChangedType.ItemRemoved));
        }

        /// <summary>
        /// Delete last added object from the list
        /// (used for Undo operation).
        /// </summary>
        public void DeleteLastAddedObject()
        {
            if (graphicsList.Count > 0)
            {
                RemoveAt(0);
            }
        }

        /// <summary>
        /// delete draw objcet
        /// </summary>
        /// <param name="drawObject"></param>
        public void DeleteDrawObject(DrawObject drawObject)
        {
            graphicsList.Remove(drawObject);
        }

        public void DeleteAll()
        {
            if (graphicsList != null && graphicsList.Count > 0)
            {
                graphicsList.Clear();
            }
        }

        public void UnselectAll()
        {
            foreach (DrawObject o in graphicsList)
            {
                o.Selected = false;
            }
        }
    }
}
