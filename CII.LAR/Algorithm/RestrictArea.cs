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
        private int minLimit = 500;
        public int MinLimit
        {
            get { return this.minLimit; }
        }

        /// <summary>
        /// 最大限位
        /// </summary>
        private int maxLimit = 2500;
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

        //private GraphicsPath videoPath;
        private GraphicsPath motorPath;

        private Region videoRegion;
        public Region VideoRegion
        {
            get { return this.videoRegion; }
            private set { this.videoRegion = value; }
        }

        private Region finalValidRegion;
        public Region FinalValidRegion
        {
            get { return this.finalValidRegion; }
            private set { this.finalValidRegion = value; }
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
            if (Program.SysConfig.LaserConfig.FinalMatrix.Rank() != 0)
            {
                Matrix<double> finalMatrix = Program.SysConfig.LaserConfig.FinalMatrix;
                double[,] p = { 
                    { finalMatrix[0, 0], finalMatrix[0, 1], finalMatrix[0, 2] },
                    { finalMatrix[1, 0], finalMatrix[1, 1], finalMatrix[1, 2] },
                    { finalMatrix[2, 0], finalMatrix[2, 1], finalMatrix[2, 2] }};
                MatrixBuilder<double> mb = Matrix<double>.Build;
                var newMatrix = mb.DenseOfArray(p);
                Matrix<double> Inverse = newMatrix.Inverse();
                foreach (var originalPoint in originalMotorPoints)
                {
                    transformedMotorPoints.Add(Calculate(originalPoint, Inverse));
                }
                CalculateRegion();
                DrawRestrict = true;
            }
            this.picturebox.Invalidate();
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
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    //g.DrawLine(pen, transformedMotorPoints[0].X, transformedMotorPoints[0].Y, transformedMotorPoints[1].X, transformedMotorPoints[1].Y);
                    //g.DrawLine(pen, transformedMotorPoints[1].X, transformedMotorPoints[1].Y, transformedMotorPoints[2].X, transformedMotorPoints[2].Y);
                    //g.DrawLine(pen, transformedMotorPoints[2].X, transformedMotorPoints[2].Y, transformedMotorPoints[3].X, transformedMotorPoints[3].Y);
                    //g.DrawLine(pen, transformedMotorPoints[3].X, transformedMotorPoints[3].Y, transformedMotorPoints[0].X, transformedMotorPoints[0].Y);
                    //SolidBrush sb = new SolidBrush(Color.FromArgb(0xC8, 0x80, 0x80, 0x80));
                    SolidBrush sb = new SolidBrush(Color.Gray);
                    g.FillRegion(sb, FinalValidRegion);
                    sb.Dispose();
                }
            }
        }

        public void CalculateRegion()
        {
            //videoPath = new GraphicsPath();
            motorPath = new GraphicsPath();

            //videoPath.AddRectangle(new RectangleF(this.picturebox.OffsetX, this.picturebox.OffsetY, this.picturebox.RealSize.Width, this.picturebox.RealSize.Height));
            //VideoRegion = new Region(videoPath);

            motorPath.AddPolygon(transformedMotorPoints.ToArray());
            MotorRegion = new Region(motorPath);

            Region videoRegion = GetVideoRegion();
            videoRegion.Xor(MotorRegion);

            Region tempRegion = GetVideoRegion();
            tempRegion.Exclude(videoRegion);

            FinalValidRegion = GetVideoRegion();

            FinalValidRegion.Exclude(tempRegion);
            videoRegion.Dispose();
            tempRegion.Dispose();
            motorPath.Dispose();
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

        public Region GetVideoRegion()
        {
            var videoPath = new GraphicsPath();
            videoPath.AddRectangle(new RectangleF(this.picturebox.OffsetX, this.picturebox.OffsetY, this.picturebox.RealSize.Width, this.picturebox.RealSize.Height));
            VideoRegion = new Region(videoPath);
            videoPath.Dispose();
            return VideoRegion;
        }

        public void MotorBoundsToRegion()
        {
            motorPath.AddPolygon(transformedMotorPoints.ToArray());
        }
    }
}
