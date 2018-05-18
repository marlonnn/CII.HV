using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
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

        private Font font;
        private double centimeterPixels;
        public SmartRuler(RichPictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            this.showRulers = false;
            font = new Font("Microsoft Sans Serif", 8.25f);
        }

        public double MillimeterToPixel(double millimeter)
        {
            return CentimeterToPixel(millimeter / 10);
        }

        public double CentimeterToPixel(double centimeter)
        {
            return centimeter * Program.DpiX / D254;
        }

        public double PixelToCentimeter(double pixel)
        {
            return pixel * D254 / Program.DpiX;
        }

        public double PixelToMillimeter(double pixel)
        {
            return PixelToCentimeter(pixel) * 10;
        }


        public void Draw(Graphics g)
        {
            if (ShowRulers)
            {
                GraphicsProperties gp = Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Ruler");
                using (Pen pen = new Pen(Color.FromArgb(gp.Alpha, gp.Color), gp.PenWidth))

                {
                    PaintXAxis(g, pen);
                    PaintYAxis(g, pen);
                }
            }
        }

        private void PaintYAxis(Graphics g, Pen pen)
        {
            var startYPositive = pictureBox.Height / 2;
            var endYPositive = pictureBox.Height;
            var startX = pictureBox.Width / 2;
            var endX = pictureBox.Width / 2 - 10;

            double startYNegative = pictureBox.Height / 2;
            centimeterPixels = CentimeterToPixel(1) * Program.SysConfig.Lense.FineAdjustment / 100f;
            int count = 0;
            for (double i = startYPositive; i < endYPositive; i+=centimeterPixels)
            {
                var positiveNumber = count * 10 / (Program.SysConfig.Lense.Factor * pictureBox.Zoom);
                //draw positive major ticks
                g.DrawLine(pen, startX, (float)i, endX, (float)i);

                //draw positive minor ticks
                g.DrawLine(pen, startX, (float)(i + centimeterPixels / 2), startX - 5, (float)(i + centimeterPixels / 2));

                string pns = positiveNumber.ToString("F2");
                var pSize = g.MeasureString(pns, font);
                if (positiveNumber > 0) g.DrawString(pns, font, Brushes.Black, endX - pSize.Width, (float)(i - pSize.Height / 2));

                //draw negative major ticks
                g.DrawLine(pen, startX, (float)startYNegative, endX, (float)startYNegative);
                //draw negative minor ticks
                g.DrawLine(pen, startX, (float)(startYNegative - centimeterPixels / 2), startX - 5, (float)(startYNegative - centimeterPixels / 2));

                var negativeNumber = -1 * positiveNumber;
                var nns = negativeNumber.ToString("F2");
                var nSize = g.MeasureString(nns, font);
                if (negativeNumber < 0) g.DrawString(nns, font, Brushes.Black, endX - nSize.Width, (float)(startYNegative - nSize.Height / 2));
                startYNegative -= centimeterPixels;
                count++;
            }

            g.DrawLine(pen, pictureBox.Width / 2, 0, pictureBox.Width / 2, pictureBox.Height);
        }

        private void PaintXAxis(Graphics g, Pen pen)
        {
            var startXPositive = pictureBox.Width / 2;
            var endXPositive = pictureBox.Width;
            var startY = pictureBox.Height / 2;
            var endY = pictureBox.Height / 2 - 10;

            double startXNegative = pictureBox.Width / 2;
            centimeterPixels = CentimeterToPixel(1) * Program.SysConfig.Lense.FineAdjustment / 100f;
            int count = 0;
            for (double i= startXPositive; i < endXPositive; i+= centimeterPixels)
            {

                var positiveNumber = count * 10 / (Program.SysConfig.Lense.Factor * pictureBox.Zoom);
                //draw positive major ticks
                g.DrawLine(pen, (float)i, startY, (float)i, endY);

                //draw positive minor ticks
                g.DrawLine(pen, (float)(i + centimeterPixels / 2), startY, (float)(i + centimeterPixels / 2), startY - 5);

                string pns = positiveNumber.ToString("F2");
                var pSize = g.MeasureString(pns, font);
                if (positiveNumber > 0) g.DrawString(pns, font, Brushes.Black, (float)(i - pSize.Width / 2), endY - pSize.Height);

                //draw negative major ticks
                g.DrawLine(pen, (float)startXNegative, startY, (float)startXNegative, endY);
                //draw negative minor ticks
                g.DrawLine(pen, (float)(startXNegative - centimeterPixels / 2), startY, (float)(startXNegative - centimeterPixels / 2), startY - 5);

                var negativeNumber = -1 * positiveNumber;
                var nns = negativeNumber.ToString("F2");
                var nSize = g.MeasureString(nns, font);
                if (negativeNumber < 0) g.DrawString(nns, font, Brushes.Black, (float)(startXNegative - nSize.Width / 2), endY - nSize.Height);
                startXNegative -= centimeterPixels;
                count++;
            }

            g.DrawLine(pen, 0, pictureBox.Height / 2, pictureBox.Width, pictureBox.Height / 2);
        }
    }
}
