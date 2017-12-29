using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Algorithm
{
    /// <summary>
    /// 电机坐标系和屏幕坐标系转换算法
    /// 钟文 2017/12/20
    /// </summary>
    public class Coordinate
    {
        //屏幕坐标系坐标
        private Dictionary<int, Point> clickPointsDic;

        private MatrixBuilder<double> mb = Matrix<double>.Build;

        private Dictionary<int, Matrix<double>> transformMatrix;

        public Matrix<double> FistMatrix
        {
            get { return this.transformMatrix[0]; }
        }

        //电机坐标系坐标
        private Dictionary<int, Point> motorPoints;

        private List<Point> boundPoints;
        public static Coordinate coordinate;

        private Matrix<double> finalMatrix;
        public Matrix<double> FinalMatrix
        {
            get { return this.finalMatrix; }
            private set { this.finalMatrix = value; }
        }

        public Coordinate()
        {
            clickPointsDic = new Dictionary<int, Point>();
            transformMatrix = new Dictionary<int, Matrix<double>>();
            motorPoints = new Dictionary<int, Point>();
            motorPoints.Add(0, new Point(1500, 1500));
            motorPoints.Add(1, new Point(1600, 1500));
            motorPoints.Add(2, new Point(1500, 1600));

            boundPoints = new List<Point>() { new Point(0, 0), new Point(0, 3000), new Point(3000, 3000), new Point(3000, 0)};

            finalMatrix = mb.Dense(3, 3);
        }

        /// <summary>
        /// 添加电机坐标
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pictureBoxSize"></param>
        private void CreatePresetMotorPoint(int index, Size pictureBoxSize)
        {
            //屏幕坐标
            Point psp = CreatePresetScreenPoint(index, pictureBoxSize);
            //转换矩阵变化为电机坐标
            Point pmp = ChangeScreenPointToMotorPoint(psp);
            AddMotorPoint(index, pmp);
        }

        /// <summary>
        /// 创建预设屏幕4点坐标
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pictureBoxSize"></param>
        /// <returns></returns>
        private Point CreatePresetScreenPoint(int index, Size pictureBoxSize)
        {
            Point psp = Point.Empty;
            switch (index)
            {
                case 3:
                    psp = new Point(pictureBoxSize.Width - 100, pictureBoxSize.Height / 2);
                    break;
                case 4:
                    psp = new Point(pictureBoxSize.Width / 2, pictureBoxSize.Height);
                    break;
                case 5:
                    psp = new Point(100, pictureBoxSize.Height / 2);
                    break;
                case 6:
                    psp = new Point(pictureBoxSize.Width / 2, 100);
                    break;
            }
            //check point in legal region
            return psp;
        }

        /// <summary>
        /// 屏幕预设坐标转换为电机坐标系坐标
        /// </summary>
        /// <param name="psp"></param>
        /// <returns></returns>
        private Point ChangeScreenPointToMotorPoint(Point psp)
        {
            Point p = Point.Empty;
            var screenArray = mb.DenseOfArray(new double[,] { { psp.X }, { psp.Y }, { 1 } });
            if (transformMatrix.Count > 0)
            {
                var temp = transformMatrix[0] * screenArray;
                p = new Point((int)temp[0, 0], (int)temp[1, 0]);
            }
            return p;
        }

        /// <summary>
        /// 求最终转换矩阵，算平均值
        /// </summary>
        /// <returns></returns>
        public Matrix<double> GetFinalMatrix()
        {
            foreach (var values in transformMatrix.Values)
            {
                FinalMatrix += values;
            }
            return FinalMatrix.Divide(transformMatrix.Values.Count);
        }

        /// <summary>
        /// 根据电机坐标边界和转换矩阵的逆矩阵，求解映射到屏幕上的点
        /// </summary>
        /// <returns></returns>
        private List<Point> CalculateScreenBoundPoints()
        {
            List<Point> points = new List<Point>();
            foreach (var p in boundPoints)
            {
                var motorArray = mb.DenseOfArray(new double[,] { { p.X }, { p.Y }, { 1 } } );
                if (transformMatrix.Count > 0)
                {
                    var temp = transformMatrix[0].Determinant() * motorArray;
                    points.Add(new Point((int)temp[0, 0], (int)temp[1, 0]));
                }
            }
            return points;
        }

        private bool CheckPointInScreenRegion(Point motorPoint)
        {
            Point screenPoint = Point.Empty;
            var motorArray = mb.DenseOfArray(new double[,] { { motorPoint.X }, { motorPoint.Y }, { 1 } });
            if (transformMatrix.Count > 0)
            {
                var temp = transformMatrix[0].Determinant() * motorArray;
                screenPoint = new Point((int)temp[0, 0], (int)temp[1, 0]);
            }


            return true;
        }

        public static Coordinate GetCoordinate()
        {
            if (coordinate == null)
            {
                coordinate = new Coordinate();
            }
            return coordinate;
        }

        /// <summary>
        /// 添加电机坐标系的点到集合中
        /// </summary>
        /// <param name="index"></param>
        /// <param name="point"></param>
        public void AddMotorPoint(int index, Point point)
        {
            if (motorPoints.ContainsKey(index))
            {
                //update
                motorPoints[index] = point;
            }
            else
            {
                //add new point
                motorPoints.Add(index, point);
            }
        }

        /// <summary>
        /// 添加屏幕校准的点到集合中
        /// </summary>
        /// <param name="index"></param>
        /// <param name="point"></param>
        public void AddPoint(int index, Point point)
        {
            if (clickPointsDic.ContainsKey(index))
            {
                //update
                clickPointsDic[index] = point;
            }
            else
            {
                //add new point
                clickPointsDic.Add(index, point);
            }
        }

        /// <summary>
        /// 根据前面三点计算第一个转换矩阵
        /// </summary>
        public void CalculateFirstMatrix()
        {
            List<Point> sps = new List<Point>();
            if (clickPointsDic.Count > 2)
            {
                foreach (var value in clickPointsDic.Values)
                {
                    sps.Add(value);
                }
            }
            List<Point> firstThreeMotorPoints = new List<Point>();
            if (motorPoints.Count > 2)
            {
                foreach (var value in motorPoints.Values)
                {
                    firstThreeMotorPoints.Add(value);
                }
            }

            Matrix<double> firstMatrix = CalculateTransformMatrix(firstThreeMotorPoints, sps);
            AddMatrix(0, firstMatrix);
        }

        /// <summary>
        /// 创建其他精度转换矩阵
        /// </summary>
        /// <param name="indexs"></param>
        /// <param name="index"></param>
        public void CalculateOtherMatix(List<int> indexs, int index)
        {
            List<Point> sps = new List<Point>();
            List<Point> mps = new List<Point>();
            foreach (int i in indexs)
            {
                sps.Add(clickPointsDic[i]);
                mps.Add(motorPoints[i]);
            }
            Matrix<double> m = CalculateTransformMatrix(mps, sps);
            AddMatrix(index, m);
        }

        /// <summary>
        /// 创建其他三个转换矩阵
        /// </summary>
        public void CalculateOtherMatix()
        {
            List<int> indexs = new List<int>() { 3, 4, 5 };
            CalculateOtherMatix(indexs, 1);
            indexs = new List<int>() { 3, 4, 6 };
            CalculateOtherMatix(indexs, 2);
            indexs = new List<int>() { 4, 5, 6 };
            CalculateOtherMatix(indexs, 3);
        }

        /// <summary>
        /// 将转换矩阵添加到转换矩阵集合中
        /// </summary>
        /// <param name="index"></param>
        /// <param name="matrix"></param>
        public void AddMatrix(int index, Matrix<double> matrix)
        {
            if (transformMatrix.ContainsKey(index))
            {
                transformMatrix[index] = matrix;
            }
            else
            {
                transformMatrix.Add(index, matrix);
            }
        }

        /// <summary>
        /// 由屏幕上三点和电机坐标系上的三点计算出转换矩阵
        /// </summary>
        /// <param name="motorPoints">电机坐标系上的点</param>
        /// <param name="screenPoints">屏幕上的点</param>
        /// <returns></returns>
        public Matrix<double> CalculateTransformMatrix(List<Point> motorPoints ,List<Point> screenPoints)
        {
            double[,] mps = { { motorPoints[0].X, motorPoints[1].X, motorPoints[2].X }, { motorPoints[0].Y, motorPoints[1].Y, motorPoints[2].Y }, { 1, 1, 1 } };
            var motorArray = mb.DenseOfArray(mps);

            double[,] sps = { { screenPoints[0].X, screenPoints[1].X, screenPoints[2].X }, { screenPoints[0].Y, screenPoints[1].Y, screenPoints[2].Y }, { 1, 1, 1 } };
            var screenArray = mb.DenseOfArray(sps);
            var Determinant = screenArray.Determinant();
            var Inverse = screenArray.Inverse();
            return motorArray * Inverse;
        }

        public Matrix<double> TestMatrix(List<Point> points)
        {
            double[,] p =  { { points[0].X, points[1].X, points[2].X }, { points[0].Y, points[1].Y, points[2].Y }, { 1, 1, 1} };
            var fromArray = mb.DenseOfArray(p);
            Console.WriteLine("fromArray: " + fromArray.ToString());
            Console.WriteLine("fromArray divide: " + fromArray.Divide(3).ToString());
            var Determinant = fromArray.Determinant();
            Console.WriteLine("Determinant: " + Determinant.ToString());
            var Inverse = fromArray.Inverse();
            Console.WriteLine("Inverse: " + Inverse.ToString());

            double[,] p1 = { { 1 }, { 2 }, { 1} };
            var fromArray1 = mb.DenseOfArray(p1);
            Console.WriteLine("fromArray1: " + fromArray1.ToString());
            var v = Inverse * fromArray1;
            Console.WriteLine("v: " + v.ToString());
            var v1 = v[0, 0];
            var v2 = v[1, 0];
            var v3 = v[2, 0];
            Console.WriteLine("v1: " + v1.ToString());
            Console.WriteLine("v2: " + v2.ToString());
            Console.WriteLine("v3: " + v3.ToString());

            return fromArray;
        }
    }
}
