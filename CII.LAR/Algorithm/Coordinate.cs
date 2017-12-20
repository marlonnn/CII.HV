using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Algorithm
{
    public class Coordinate
    {
        private MatrixBuilder<double> mb = Matrix<double>.Build;

        public static Coordinate coordinate;
        public Coordinate() { }

        public static Coordinate GetCoordinate()
        {
            if (coordinate == null)
            {
                coordinate = new Coordinate();
            }
            return coordinate;
        }

        public Matrix<double> TestMatrix(List<Point> points)
        {
            double[,] p =  { { points[0].X, points[1].X, points[2].X }, { points[0].Y, points[1].Y, points[2].Y }, { 1, 1, 1} };
            var fromArray = mb.DenseOfArray(p);
            Console.WriteLine("fromArray: " + fromArray.ToString());
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
