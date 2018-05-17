using CII.LAR.UI;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Algorithm
{
    public class RestrictArea
    {
        /// <summary>
        /// 最小限位
        /// </summary>
        private int minLimit = 1000;
        public int MinLimit
        {
            get { return this.minLimit; }
        }

        /// <summary>
        /// 最大限位
        /// </summary>
        private int maxLimit = 1500;
        public int MaxLimit
        {
            get { return this.maxLimit; }
        }

        private List<Point> originalMotorPoints;
        private List<Point> transformedMotorPoints;
        public List<Point> TransformedMotorPoints
        {
            get { return this.transformedMotorPoints; }
        }
        /// <summary>
        /// 视频区域
        /// </summary>
        private Rectangle videoBounds;
        public Rectangle VideoBounds
        {
            get { return this.videoBounds; }
            set { this.videoBounds = value; }
        }

        private GraphicsPath videoPath;
        private GraphicsPath motorPath;

        private Region videoRegion;
        public Region VideoRegion
        {
            get { return this.videoRegion; }
            private set { this.videoRegion = value; }
        }

        private Region motorRegion;
        public Region MotorRegion
        {
            get { return this.motorRegion; }
            set { this.motorRegion = value; }
        }

        private MatrixBuilder<double> mb = Matrix<double>.Build;

        private RichPictureBox picturebox;

        public RestrictArea(RichPictureBox picturebox)
        {
            this.picturebox = picturebox;

            InitializeOriginalMotorPoints();

            drawRestrict = false;
        }

        private void InitializeOriginalMotorPoints()
        {
            originalMotorPoints = new List<Point>();

            originalMotorPoints.Add(new Point(MinLimit, MinLimit));
            originalMotorPoints.Add(new Point(MinLimit, MaxLimit));
            originalMotorPoints.Add(new Point(MaxLimit, MaxLimit));
            originalMotorPoints.Add(new Point(MaxLimit, MinLimit));
        }

        public List<Point> TransformMotorOriginalPoints()
        {
            transformedMotorPoints = new List<Point>();
            Matrix<double> finalMatrix = Program.SysConfig.LaserConfig.FinalMatrix;
            Matrix<double> Inverse = finalMatrix.Inverse();
            foreach (var originalPoint in originalMotorPoints)
            {
                transformedMotorPoints.Add(Calculate(originalPoint, Inverse));
            }
            CalculateRegion();
            DrawRestrict = true;
            return transformedMotorPoints;
        }

        private bool drawRestrict;
        public bool DrawRestrict
        {
            get { return this.drawRestrict; }
            set { this.drawRestrict = value; }
        }

        public void TestDrawMotorRectangle(Graphics g, Pen pen)
        {
            if (DrawRestrict && this.picturebox.Zoom == 1)
            {
                if (transformedMotorPoints != null && transformedMotorPoints.Count > 0)
                {
                    g.DrawLine(pen, transformedMotorPoints[0].X, transformedMotorPoints[0].Y, transformedMotorPoints[1].X, transformedMotorPoints[1].Y);
                    g.DrawLine(pen, transformedMotorPoints[1].X, transformedMotorPoints[1].Y, transformedMotorPoints[2].X, transformedMotorPoints[2].Y);
                    g.DrawLine(pen, transformedMotorPoints[2].X, transformedMotorPoints[2].Y, transformedMotorPoints[3].X, transformedMotorPoints[3].Y);
                    g.DrawLine(pen, transformedMotorPoints[3].X, transformedMotorPoints[3].Y, transformedMotorPoints[0].X, transformedMotorPoints[0].Y);

                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(0xC8, 0x80, 0x80, 0x80)))
                        g.FillRegion(sb, VideoRegion);
                }
            }
        }

        public void CalculateRegion()
        {
            videoPath = new GraphicsPath();
            motorPath = new GraphicsPath();

            videoPath.AddRectangle(new RectangleF(this.picturebox.OffsetX, this.picturebox.OffsetY, this.picturebox.RealSize.Width, this.picturebox.RealSize.Height));
            VideoRegion = new Region(videoPath);

            motorPath.AddPolygon(transformedMotorPoints.ToArray());
            MotorRegion = new Region(motorPath);

            VideoRegion.Xor(MotorRegion);
        }

        /// <summary>
        /// 检查鼠标是否在合法区域外
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool CheckPointInRegion(Point point)
        {
            if (DrawRestrict && this.picturebox.Zoom == 1)
            {
                return VideoRegion.IsVisible(point);
            }
            else
            {
                return false;
            }
        }

        public bool CheckPointInRegion(PointF point)
        {
            return VideoRegion.IsVisible(point);
        }

        private Point Calculate(Point originalPoint, Matrix<double> Inverse)
        {
            Point newPoint = Point.Empty;
            double[,] mps = { { originalPoint.X }, { originalPoint.Y }, { 1 } };
            var motorArray = mb.DenseOfArray(mps);

            Matrix<double> transformed = Inverse * motorArray;
            newPoint = new Point((int)transformed[0, 0], (int)transformed[1, 0]);
            return newPoint;
        }

        public void VideoBoundsToRegion()
        {
            videoPath.AddRectangle(videoBounds);
        }

        public void MotorBoundsToRegion()
        {
            motorPath.AddPolygon(transformedMotorPoints.ToArray());
        }
    }
}
