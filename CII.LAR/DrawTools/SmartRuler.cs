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
    /// Samrt ruler
    /// Zhong Wen 2018/03/02
    /// </summary>
    public class SmartRuler
    {
        private const int minorInterval = 2;

        private const double D254 = 2.54d;

        private RichPictureBox pictureBox;

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

        private double micronPixels;
        public SmartRuler(RichPictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            this.showRulers = false;
        }

        public void Draw(Graphics g)
        {
            if (ShowRulers)
            {
                GraphicsProperties gp = Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Ruler");
                using (Pen pen = new Pen(Color.FromArgb(gp.Alpha, gp.Color), gp.PenWidth))
                {
                    //以下代码阅读可能带来不适
                    var baseMicron = BaseMajorValue * pictureBox.DigitalMagnification;
                    micronPixels = pictureBox.MicronToPixel(baseMicron) * Program.SysConfig.Lense.FineAdjustment / 100f;
                    PaintXAxis(g, pen, gp);
                    PaintYAxis(g, pen, gp);
                }
            }
        }

        /// <summary>
        /// Major ticks跨度
        /// </summary>
        private double baseMajorValue;
        public double BaseMajorValue
        {
            get { return baseMajorValue = CalculateMajorValue() / RoundFactor; }
            set { baseMajorValue = value; }
        }

        public double CalculateMajorValue()
        {
            double value = 0;
            if (100 % RoundFactor != 0)
            {
                var temp = 100 / RoundFactor;
                value = temp;
            }
            else
            {
                value = 100 / RoundFactor;
            }
            return value * RoundFactor;
        }

        public int RoundFactor
        {
            get { return CalculateRoundFactor(); }
        }

        private int CalculateRoundFactor()
        {
            int factor = (int)Program.SysConfig.Lense.Factor;
            int roundFactor = 1;
            if (factor > 0 && factor < 10)
            {
                roundFactor = 1;
            }
            else if (factor >= 10 && factor < 100)
            {
                roundFactor = (int)(factor / 10) * 10;
            }
            else if (factor >= 100 && factor <1000)
            {
                roundFactor = (int)(factor / 100) * 100;
            }
            return roundFactor;
        }

        /// <summary>
        /// 正 Y 轴细分
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="font"></param>
        /// <param name="gp"></param>
        /// <param name="index"></param>
        /// <param name="startX"></param>
        /// <param name="endX"></param>
        /// <param name="startY"></param>
        /// <param name="endY"></param>
        private void PaintYDividePositiveAxis(Graphics g, Pen pen, Font font, GraphicsProperties gp, int index, int startX, int endX, float startY, float endY)
        {
            var count = micronPixels / (40 * Program.SysConfig.Lense.FineAdjustment / 100f);
            count = GetCount((int)count);
            if (count > 1)
            {
                double positiveStartNumber = (index - 1) * baseMajorValue;

                double positiveEndNumber = index * baseMajorValue;

                int j = 0;
                for (double i = startY; i < endY; i += (micronPixels / count))
                {
                    if (i != startY)
                    {

                        g.DrawLine(pen, startX, (float)i, endX, (float)i);
                        j++;
                        double positiveNumber = positiveStartNumber + (positiveEndNumber - positiveStartNumber) / count * j;

                        string pns = positiveNumber.ToString("F2");
                        var pSize = g.MeasureString(pns, font);
                        if (positiveNumber > 0)
                        {
                            using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                                g.DrawString(pns, font, sb, endX - pSize.Width, (float)(i - pSize.Height / 2));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 负 Y轴 细分
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="font"></param>
        /// <param name="gp"></param>
        /// <param name="index"></param>
        /// <param name="startX"></param>
        /// <param name="endX"></param>
        /// <param name="startYNegative"></param>
        /// <param name="endY"></param>
        private void PaintYDivideNegativeAxis(Graphics g, Pen pen, Font font, GraphicsProperties gp, int index, int startX, int endX, float startYNegative, float endY)
        {
            var count = micronPixels / (40 * Program.SysConfig.Lense.FineAdjustment / 100f);
            count = GetCount((int)count);
            if (count > 1)
            {
                double positiveStartNumber = (index + 1) * baseMajorValue;

                double positiveEndNumber = index * baseMajorValue;
                int j = 0;
                for (double i = startYNegative; i < endX; i += (micronPixels / count))
                {
                    if (i != startYNegative)
                    {
                        g.DrawLine(pen, startX, (float)i, endX, (float)i);

                        j++;
                        double positiveNumber = positiveStartNumber + (positiveEndNumber - positiveStartNumber) / count * j;


                        var negativeNumber = -1 * positiveNumber;
                        var nns = negativeNumber.ToString("F2");
                        var nSize = g.MeasureString(nns, font);
                        if (negativeNumber < 0)
                        {
                            using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                                g.DrawString(nns, font, sb, endX - nSize.Width, (float)(i - nSize.Height / 2));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Y轴
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="gp"></param>
        private void PaintYAxis(Graphics g, Pen pen, GraphicsProperties gp)
        {
            var startYPositive = pictureBox.Height / 2;
            var endYPositive = pictureBox.Height;
            var startX = pictureBox.Width / 2;
            var endX = pictureBox.Width / 2 - 10;

            double startYNegative = pictureBox.Height / 2;
            //micronPixels = CentimeterToPixel(1) * Program.SysConfig.Lense.FineAdjustment / 100f;
            int count = 0;

            for (double i = startYPositive; i < endYPositive + micronPixels; i+=micronPixels)
            {
                var positiveNumber = count * baseMajorValue/* / (Program.SysConfig.Lense.Factor * pictureBox.Zoom)*/;
                //draw positive major ticks
                g.DrawLine(pen, startX, (float)i, endX, (float)i);
                Font font = new Font("Microsoft Sans Serif", 8.25f + gp.PenWidth / 10f);
                if (i != startYPositive)
                    PaintYDividePositiveAxis(g, pen, font, gp, count, startX, endX, (float)(i - micronPixels), (float)i);
                //draw positive minor ticks
                //g.DrawLine(pen, startX, (float)(i + micronPixels / 2), startX - 5, (float)(i + micronPixels / 2));

                string pns = positiveNumber.ToString("F2");
                var pSize = g.MeasureString(pns, font);
                if (positiveNumber > 0)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                        g.DrawString(pns, font, sb, endX - pSize.Width, (float)(i - pSize.Height / 2));
                }

                //draw negative major ticks
                g.DrawLine(pen, startX, (float)startYNegative, endX, (float)startYNegative);
                //draw negative minor ticks
                //g.DrawLine(pen, startX, (float)(startYNegative - micronPixels / 2), startX - 5, (float)(startYNegative - micronPixels / 2));

                var negativeNumber = -1 * positiveNumber;
                var nns = negativeNumber.ToString("F2");
                var nSize = g.MeasureString(nns, font);
                if (negativeNumber < 0)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                        g.DrawString(nns, font, sb, endX - nSize.Width, (float)(startYNegative - nSize.Height / 2));
                }
                startYNegative -= micronPixels;
                PaintYDivideNegativeAxis(g, pen, font, gp, count, startX, endX, (float)startYNegative, (float)(startYNegative + micronPixels));
                count++;
                font.Dispose();
            }

            g.DrawLine(pen, pictureBox.Width / 2, 0, pictureBox.Width / 2, pictureBox.Height);
        }

        /// <summary>
        /// 正X轴自动细分
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="gp"></param>
        /// <param name="index">用以计算细分的起点和终点数值</param>
        /// <param name="startX">X开始坐标</param>
        /// <param name="endX">X终点坐标</param>
        /// <param name="startY"></param>
        /// <param name="endY"></param>
        private void PaintXDividePositiveAxis(Graphics g, Pen pen, Font font, GraphicsProperties gp, int index, double startX, double endX, float startY, float endY)
        {
            var count = micronPixels / (40 * Program.SysConfig.Lense.FineAdjustment / 100f);
            count = GetCount((int)count);
            if (count > 1)
            {
                double positiveStartNumber = (index - 1) * baseMajorValue;

                double positiveEndNumber = index * baseMajorValue;
                var v = baseMajorValue / count;
                int j = 0;
                for (double i= startX; i < endX; i+= (micronPixels / count))
                {
                    if (i != startX)
                    {
                        g.DrawLine(pen, (float)i, startY, (float)i, endY);
                        j++;
                        double positiveNumber = positiveStartNumber + (positiveEndNumber - positiveStartNumber) / count * j;
                        string pns = positiveNumber.ToString("F2");
                        var pSize = g.MeasureString(pns, font);
                        if (positiveNumber > 0)
                        {
                            using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                                g.DrawString(pns, font, sb, (float)(i - pSize.Width / 2), endY - pSize.Height);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 负 X 轴细分
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="font"></param>
        /// <param name="gp"></param>
        /// <param name="index"></param>
        /// <param name="startXNegative"></param>
        /// <param name="endX"></param>
        /// <param name="startY"></param>
        /// <param name="endY"></param>
        private void PaintXDivideNegativeAxis(Graphics g, Pen pen, Font font, GraphicsProperties gp, int index, double startXNegative, double endX, float startY, float endY)
        {
            var count = micronPixels / (40 * Program.SysConfig.Lense.FineAdjustment / 100f);
            count = GetCount((int)count);
            if (count > 1)
            {
                double positiveStartNumber = (index + 1) * baseMajorValue;

                double positiveEndNumber = index * baseMajorValue;
                int j = 0;
                for (double i = startXNegative; i < endX; i += (micronPixels / count))
                {
                    if (i != startXNegative)
                    {
                        g.DrawLine(pen, (float)i, startY, (float)i, endY);

                        j++;
                        double positiveNumber = positiveStartNumber + (positiveEndNumber - positiveStartNumber) / count * j;


                        var negativeNumber = -1 * positiveNumber;
                        var nns = negativeNumber.ToString("F2");
                        var nSize = g.MeasureString(nns, font);
                        if (negativeNumber < 0)
                        {
                            using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                                g.DrawString(nns, font, sb, (float)(i - nSize.Width / 2), endY - nSize.Height);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 整除或者小数点后一位
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private int GetCount(int count)
        {
            var value = baseMajorValue / count;
            string number = value.ToString();
            string[] a = number.Split(new char[] { '.' });
            int decimals = a.Length > 1 ? a[1].Length : 0;
            if (decimals == 1 || baseMajorValue % count == 0)
            {
                return count;
            }
            else
            {
                if (count> 0 && count-1 > 0)
                {
                    count--;
                    return GetCount(count);
                }
            }
            return count;
        }

        /// <summary>
        /// X轴绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="gp"></param>
        private void PaintXAxis(Graphics g, Pen pen, GraphicsProperties gp)
        {
            var startXPositive = pictureBox.Width / 2;
            var endXPositive = pictureBox.Width;
            var startY = pictureBox.Height / 2;
            var endY = pictureBox.Height / 2 - 10;

            double startXNegative = pictureBox.Width / 2;
            //micronPixels = CentimeterToPixel(1) * Program.SysConfig.Lense.FineAdjustment / 100f;
            int count = 0;
            for (double i= startXPositive; i < endXPositive + micronPixels; i+= micronPixels)
            {

                var positiveNumber = count * baseMajorValue/* / (Program.SysConfig.Lense.Factor * pictureBox.Zoom)*/;
                //draw positive major ticks
                g.DrawLine(pen, (float)i, startY, (float)i, endY);

                Font font = new Font("Microsoft Sans Serif", 8.25f + gp.PenWidth / 10f);
                if (i != startXPositive)
                    PaintXDividePositiveAxis(g, pen, font, gp, count, i - micronPixels, i, startY, endY);
                //draw positive minor ticks
                //g.DrawLine(pen, (float)(i + micronPixels / 2), startY, (float)(i + micronPixels / 2), startY - 5);
                string pns = positiveNumber.ToString("F2");
                var pSize = g.MeasureString(pns, font);
                if (positiveNumber > 0)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                        g.DrawString(pns, font, sb, (float)(i - pSize.Width / 2), endY - pSize.Height);
                }

                //draw negative major ticks
                g.DrawLine(pen, (float)startXNegative, startY, (float)startXNegative, endY);
                //draw negative minor ticks
                //g.DrawLine(pen, (float)(startXNegative - micronPixels / 2), startY, (float)(startXNegative - micronPixels / 2), startY - 5);

                var negativeNumber = -1 * positiveNumber;
                var nns = negativeNumber.ToString("F2");
                var nSize = g.MeasureString(nns, font);
                if (negativeNumber < 0)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(gp.Alpha, gp.Color)))
                        g.DrawString(nns, font, sb, (float)(startXNegative - nSize.Width / 2), endY - nSize.Height);
                }
                //if (i != startXNegative)
                startXNegative -= micronPixels;
                PaintXDivideNegativeAxis(g, pen, font, gp, count, startXNegative, startXNegative + micronPixels, startY, endY);
                count++;
                font.Dispose();
            }

            g.DrawLine(pen, 0, pictureBox.Height / 2, pictureBox.Width, pictureBox.Height / 2);
        }
    }
}
