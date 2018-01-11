using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Draw horizontal and vertical ruler
    /// Author: Zhong Wen 2017/08/31
    /// </summary>
    public class Rulers
    {
        private bool showRulers;
        public bool ShowRulers
        {
            get
            {
                return showRulers;
            }
            set
            {
                showRulers = value;
            }
        }

        private const int DigitWidth = 6;

        private GraphicsPropertiesManager graphicsPropertiesManager = GraphicsPropertiesManager.GraphicsManagerSingleInstance();
        public GraphicsPropertiesManager GraphicsPropertiesManager
        {
            get
            {
                return graphicsPropertiesManager;
            }
            set
            {
                graphicsPropertiesManager = value;
            }
        }

        private VideoControl videoControl;

        private float rulerStep = 100;

        public float RulerStep
        {
            get
            {
                return rulerStep * videoControl.Zoom;
            }
            set
            {
                if (value != rulerStep)
                {
                    rulerStep = value;
                }
            }
        }

        private Dictionary<char, Point[]> signsTable;

        public Rulers()
        {
            InitializeSignsTable();
        }

        public Rulers(VideoControl videoControl) : this()
        {
            this.videoControl = videoControl;
        }

        #region initialize signs table
        private void InitializeSignsTable()
        {
            signsTable = new Dictionary<char, Point[]>();
            //figure "1"
            Point[] one = {
                    new Point(0, 2),
                    new Point(2, 0),
                    new Point(2, 7)
                };
            // figure "2"
            Point[] two = {
                    new Point(0, 1),
                    new Point(1, 0),
                    new Point(3, 0),
                    new Point(4, 1),
                    new Point(4, 3),
                    new Point(0, 7),
                    new Point(4, 7)
                };

            // figure "3"
            Point[] three = {
                    new Point(0, 1),
                    new Point(1, 0),
                    new Point(3, 0),
                    new Point(4, 1),
                    new Point(4, 2),
                    new Point(3, 3),
                    new Point(2, 3),
                    new Point(3, 3),
                    new Point(4, 4),
                    new Point(4, 6),
                    new Point(3, 7),
                    new Point(1, 7),
                    new Point(0, 6)
                };

            // figure "4"
            Point[] four = {
                    new Point(4, 5),
                    new Point(0, 5),
                    new Point(0, 4),
                    new Point(2, 1),
                    new Point(3, 0),
                    new Point(3, 7)
                };

            // figure "5"
            Point[] five = {
                    new Point(4, 0),
                    new Point(1, 0),
                    new Point(1, 1),
                    new Point(0, 2),
                    new Point(0, 3),
                    new Point(3, 3),
                    new Point(4, 4),
                    new Point(4, 6),
                    new Point(3, 7),
                    new Point(1, 7),
                    new Point(0, 6)
                };

            // figure "6"
            Point[] six = {
                    new Point(3, 0),
                    new Point(1, 0),
                    new Point(0, 1),
                    new Point(0, 6),
                    new Point(1, 7),
                    new Point(3, 7),
                    new Point(4, 6),
                    new Point(4, 4),
                    new Point(3, 3),
                    new Point(0, 3)
                };

            // figure "7"
            Point[] seven = {
                    new Point(0, 0),
                    new Point(4, 0),
                    new Point(1, 7)
                };

            // figure "8"
            Point[] eight = {
                    new Point(3, 0),
                    new Point(1, 0),
                    new Point(0, 1),
                    new Point(0, 2),
                    new Point(1, 3),
                    new Point(3, 3),
                    new Point(4, 4),
                    new Point(4, 6),
                    new Point(3, 7),
                    new Point(1, 7),
                    new Point(0, 6),
                    new Point(0, 4),
                    new Point(1, 3),
                    new Point(3, 3),
                    new Point(4, 2),
                    new Point(4, 1),
                    new Point(3, 0)
                };

            // figure "9"
            Point[] nine = {
                    new Point(0, 6),
                    new Point(1, 7),
                    new Point(3, 7),
                    new Point(4, 6),
                    new Point(4, 1),
                    new Point(3, 0),
                    new Point(1, 0),
                    new Point(0, 1),
                    new Point(0, 3),
                    new Point(1, 4),
                    new Point(4, 4)
                };

            // figure "0"
            Point[] zero = {
                    new Point(1, 0),
                    new Point(3, 0),
                    new Point(4, 1),
                    new Point(4, 6),
                    new Point(3, 7),
                    new Point(1, 7),
                    new Point(0, 6),
                    new Point(0, 1),
                    new Point(1, 0)
                };

            // figure "-"
            Point[] minus = {
                    new Point(1, 3),
                    new Point(4, 3)
                };

            // figure "." e figure ","
            Point[] dot = {
                    new Point(2, 6),
                    new Point(3, 6),
                    new Point(2, 7),
                    new Point(3, 7)
                };

            signsTable.Add('1', one);
            signsTable.Add('2', two);
            signsTable.Add('3', three);
            signsTable.Add('4', four);
            signsTable.Add('5', five);
            signsTable.Add('6', six);
            signsTable.Add('7', seven);
            signsTable.Add('8', eight);
            signsTable.Add('9', nine);
            signsTable.Add('0', zero);
            signsTable.Add('-', minus);
            signsTable.Add('.', dot);
            signsTable.Add(',', dot);
        } 
        #endregion

        private int MaskWidth(double aValue)
        {
            return DigitWidth * ValueString(aValue).Length;
        }

        private string ValueString(double aValue)
        {
            return aValue.ToString("0.###");
        }

        private List<Point> pixelList;
        private List<Point> logicalList;
        public void DrawScaledNumber(Graphics g, double value, float xCoord, float yCoord, float ScaleFactor, bool Horizontal)
        {
            pixelList = new List<Point>();
            CreateSegmentsList(value, ref pixelList, Horizontal);

            logicalList = new List<Point>();

            Point tmpLogicPoint = default(Point);
            for (int iIter = 0; iIter <= pixelList.Count - 1; iIter++)
            {
                if (pixelList[iIter].X != int.MaxValue)
                {
                    tmpLogicPoint.X = (int)(pixelList[iIter].X / ScaleFactor + xCoord);
                    tmpLogicPoint.Y = (int)(pixelList[iIter].Y / ScaleFactor + yCoord);
                    logicalList.Add(tmpLogicPoint);
                }
                else
                {
                    using (Pen pen = new Pen(GraphicsPropertiesManager.GetPropertiesByName("Ruler").Color,
                        GraphicsPropertiesManager.GetPropertiesByName("Ruler").PenWidth))
                    {
                        g.DrawLines(pen, logicalList.ToArray());
                        logicalList.Clear();
                    }
                }
            }
        }

        private void CreateSegmentsList(double value, ref List<System.Drawing.Point> pointList, bool Horizontal)
        {
            pointList.Clear();

            string strValue = ValueString(value);
            int alignmentOffset = -MaskWidth(value) / 2;

            Point[] actualSign = null;
            char actualChar = '\0';

            for (int actualIndex = 0; actualIndex <= strValue.Length - 1; actualIndex++)
            {
                actualChar = strValue.ToCharArray()[actualIndex];
                actualSign = signsTable[actualChar];

                System.Drawing.Point newPoint = default(System.Drawing.Point);
                int xCoord = 0;
                int yCoord = 0;
                pointList.Capacity = pointList.Count + actualSign.Length + 1;
                for (int i = 0; i <= actualSign.Length - 1; i++)
                {
                    xCoord = (DigitWidth * actualIndex) + actualSign[i].X + alignmentOffset;
                    yCoord = actualSign[i].Y;
                    if (Horizontal)
                    {
                        newPoint = new System.Drawing.Point(xCoord, yCoord);
                    }
                    else
                    {
                        newPoint = new System.Drawing.Point(yCoord, -xCoord);
                    }
                    pointList.Add(newPoint);
                }
                pointList.Add(new System.Drawing.Point(int.MaxValue, int.MaxValue));
            }
        }

        public void Draw(Graphics g)
        {
            if (ShowRulers)
            {
                using (Pen pen = new Pen(GraphicsPropertiesManager.GetPropertiesByName("Ruler").Color, 
                    GraphicsPropertiesManager.GetPropertiesByName("Ruler").PenWidth))
                {
                    //g.ResetTransform();
                    DrawHorizontalRuler(g, pen);
                    DrawVerticalRuler(g, pen);
                }
            }
        }
        
        /// <summary>
        /// draw hotizontal ruler
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        public void DrawHorizontalRuler(Graphics g, Pen pen)
        {
            float x1Coord = videoControl.Width / 2;
            float x2Coord = videoControl.Width / 2;
            float x1 = 0;
            float x2 = 0;
            for ( ; x1Coord < videoControl.Width; x1Coord += RulerStep)
            {
                //1.X > 0
                g.DrawLine(pen, x1Coord, videoControl.Height / 2 - 10, x1Coord, videoControl.Height / 2);
                g.DrawLine(pen, x1Coord + RulerStep / 2, videoControl.Height / 2 - 5, x1Coord + RulerStep / 2, videoControl.Height / 2);

                if (x1 != 0)
                {
                    DrawScaledNumber(g, x1, x1Coord, videoControl.Height / 2 - 20, 1, true);
                }
                x1 += this.rulerStep;

                //2.X < 0
                g.DrawLine(pen, x2Coord, videoControl.Height / 2 - 10, x2Coord, videoControl.Height / 2);
                g.DrawLine(pen, x2Coord + RulerStep / 2, videoControl.Height / 2 - 5, x2Coord + RulerStep / 2, videoControl.Height / 2);

                if (x2 != 0)
                {
                    DrawScaledNumber(g, x2, x2Coord, videoControl.Height / 2 - 20, 1, true);
                }
                x2Coord -= RulerStep;
                x2 -= this.rulerStep;
            }

            g.DrawLine(pen, 0, videoControl.Height / 2, videoControl.Width, videoControl.Height / 2);
        }

        /// <summary>
        /// draw vertical ruler
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        public void DrawVerticalRuler(Graphics g, Pen pen)
        {
            float y1Coord = videoControl.Height / 2;
            float y2Coord = videoControl.Height / 2;
            float y1 = 0;
            float y2 = 0;
            for ( ; y1Coord < videoControl.Height; y1Coord += RulerStep)
            {
                //1.Y > 0
                g.DrawLine(pen, videoControl.Width / 2 - 10, y1Coord, videoControl.Width / 2, y1Coord);
                g.DrawLine(pen, videoControl.Width / 2 - 5, y1Coord + RulerStep / 2, videoControl.Width / 2, y1Coord + RulerStep / 2);

                if (y1 != 0)
                {
                    DrawScaledNumber(g, y1, videoControl.Width / 2 - 20, y1Coord - 2, 1, true);
                }
                y1 += this.rulerStep;

                g.DrawLine(pen, videoControl.Width / 2 - 10, y2Coord, videoControl.Width / 2, y2Coord);
                g.DrawLine(pen, videoControl.Width / 2 - 5, y2Coord + RulerStep / 2, videoControl.Width / 2, y2Coord + RulerStep / 2);

                if (y2 != 0)
                {
                    DrawScaledNumber(g, y2, videoControl.Width / 2 - 20, y2Coord - 2, 1, true);
                }
                y2Coord -= RulerStep;
                y2 -= this.rulerStep;
            }
            g.DrawLine(pen, videoControl.Width / 2, 0, videoControl.Width / 2, videoControl.Height);
        }
    }
}
