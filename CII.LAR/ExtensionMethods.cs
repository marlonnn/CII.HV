using CII.LAR.MaterialSkin;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    public static class ExtensionMethods
    {
        private static double D254 = 2.54d;

        public static double MicronToPixel(this RichPictureBox picture, double micron)
        {
            return MillimeterToPixel(picture, micron / 1000);
        }

        public static double MillimeterToPixel(this RichPictureBox picture, double millimeter)
        {
            return CentimeterToPixel(picture, millimeter / 10);
        }

        public static double CentimeterToPixel(this RichPictureBox picture, double centimeter)
        {
            return centimeter * Program.DpiX / D254;
        }

        public static double PixelToCentimeter(this RichPictureBox picture, double pixel)
        {
            return pixel * D254 / Program.DpiX;
        }

        public static double PixelToMillimeter(this RichPictureBox picture, double pixel)
        {
            return PixelToCentimeter(picture, pixel) * 10;
        }

        public static double PixelToMicron(this RichPictureBox picture, double pixel)
        {
            return PixelToMillimeter(picture, pixel) * 1000;
        }

        public static double MicronToMicroscope(this RichPictureBox pictureBox, double micron)
        {
            return micron / pictureBox.DigitalMagnification;
        }

        public static double PixelToMicroscope(this RichPictureBox pictureBox, double pixel)
        {
            return PixelToMicron(pictureBox, pixel) / pictureBox.DigitalMagnification;
        }

        /// <summary>
        /// Deserialize object from file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T SerializeFromFile<T>(this T source, string fileName)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// serialize object to file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static void SerializeToFile<T>(this T source, string fileName)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return;
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, source);
            }
        }

        public static bool SerializeEqual<T>(this T source, T target)
        {
            return source.Serialize().SequenceEqual(target.Serialize());
        }

        /// <summary>
        /// get serialize bytes array
        /// </summary>
        /// <returns></returns>
        public static byte[] Serialize<T>(this T source)
        {
            if (source == null) return new byte[] { };

            IFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream, source);
                    return stream.ToArray();
                }
                catch (System.Exception)
                {

                }
                return new byte[] { };
            }
        }

        public static void InitializeThumbCustomShape(this BaseCtrl baseCtrl)
        {
            foreach (var ctrl in baseCtrl.Controls)
            {
                MaterialSlider ms = ctrl as MaterialSlider;
                if (ms != null)
                {
                    GraphicsPath gp2 = new GraphicsPath();
                    gp2.AddEllipse(new RectangleF((0 + ms.Height) / 2f, (0 + ms.Height) / 2f, 10, 10));
                    ms.ThumbCustomShape = gp2;
                }
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(this Control parent, Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void ToHighQuality(this Control parent, Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        public static void ToLowQuality(this Control parent, Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
        }
    }
}
