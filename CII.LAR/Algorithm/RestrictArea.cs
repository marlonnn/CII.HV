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

        public RestrictArea()
        {
            InitializeOriginalMotorPoints();

            videoPath = new GraphicsPath();
            motorPath = new GraphicsPath();

            VideoRegion = new Region();
            MotorRegion = new Region();
        }

        private void InitializeOriginalMotorPoints()
        {
            originalMotorPoints = new List<Point>();

            originalMotorPoints.Add(new Point(MinLimit, MinLimit));
            originalMotorPoints.Add(new Point(0, MaxLimit));
            originalMotorPoints.Add(new Point(MaxLimit, MaxLimit));
            originalMotorPoints.Add(new Point(MaxLimit, 0));
        }

        public void TransformMotorOriginalPoints()
        {
            transformedMotorPoints = new List<Point>();
            Matrix<double> finalMatrix = Coordinate.GetCoordinate().FinalMatrix;
            Matrix<double> Inverse = finalMatrix.Inverse();
            foreach (var originalPoint in originalMotorPoints)
            {
                transformedMotorPoints.Add(Calculate(originalPoint, Inverse));
            }
        }

        private Point Calculate(Point originalPoint, Matrix<double> Inverse)
        {
            Point newPoint = Point.Empty;
            double[,] mps = { { originalPoint.X }, { originalPoint.Y }, { 1 } };
            var motorArray = mb.DenseOfArray(mps);

            Matrix<double> transformed = motorArray * Inverse;
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
