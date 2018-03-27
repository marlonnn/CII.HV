using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CII.LAR.UI;

namespace CII.LAR.DrawTools
{
    public class DrawPolyLine : DrawPolygon
    {
        private bool needReverseX = false;
        public bool NeedReverseX
        {
            get { return needReverseX; }
            set { needReverseX = value; }
        }
        private bool needReverseY = false;
        public bool NeedReverseY
        {
            get { return needReverseY; }
            set { needReverseY = value; }
        }
        private bool setProportion = true;
        public bool SetProportion
        {
            get { return setProportion; }
            set { setProportion = value; }
        }

        public DrawPolyLine()
        {
            this.ObjectType = ObjectType.Polygon;
        }

        public DrawPolyLine(VideoControl videoControl, int x1, int y1, int x2, int y2)
            : base(videoControl, x1, y1, x2, y2)
        {
            InitializeGraphicsProperties();
            this.RegisterUpdateStatisticsHandler();
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += videoControl.GraphicsPropertiesChangedHandler;

        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Polygon");
            this.GraphicsProperties.DrawObject = this;
            //this.GraphicsProperties.Color = Color.LawnGreen;
            this.GraphicsProperties.Alpha = (this.GraphicsProperties.Alpha == 0xFF || this.GraphicsProperties.Alpha == 0) ? 0xFF
                : this.GraphicsProperties.Alpha;
        }

        public override string Prefix
        {
            get
            {
                return "P";
            }
        }

        public override RectangleF GetTextF(string name, Graphics g, int index)
        {
            SizeF sizeF = g.MeasureString(name, this.Font);
            return new RectangleF(pointArray[0].X - sizeF.Width, pointArray[0].Y - sizeF.Height,
                sizeF.Width, sizeF.Height);
        }

        public bool CloseToFirstPoint(Point point)
        {
            if (PointCount <= 0) return false;

            Point first = Point.Ceiling(pointArray[0]);

            return CloseToPoint(first, point);
        }

        public void RemovePointAt(int nIndex)
        {
            if (nIndex > 0 && nIndex < pointArray.Count)
            {
                pointArray.RemoveAt(nIndex);
            }

        }
    }
}
