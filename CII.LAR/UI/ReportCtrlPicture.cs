using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.ExpClass;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CII.LAR.UI
{
    public partial class ReportCtrlPicture : ReportCtrl
    {
        public PictureBox SubCtrl
        {
            get { return subCtrl as PictureBox; }
            set { subCtrl = value; }
        }

        public ReportPictureItem PictureItem
        {
            get { return ReportItem as ReportPictureItem; }
            set { ReportItem = value; }
        }

        public Image Picture
        {
            get
            {
                return PictureItem.Picture;
            }
            set
            {
                PictureItem.Picture = value;
                SubCtrl.Image = value;

                Rectangle rect = PictureItem.Bounds;
                rect.Width = PictureItem.Picture.Width + 12;
                rect.Height = PictureItem.Picture.Height + 12;
                PictureItem.Bounds = rect;
                this.Width = rect.Width;
                this.Height = rect.Height;
                SubCtrl.Width = PictureItem.Picture.Width;
                SubCtrl.Height = PictureItem.Picture.Height;
            }
        }

        public ReportCtrlPicture(ReportPictureItem reportItem) :base(reportItem)
        {
            InitializeComponent();
            subCtrl = new PictureBox();
            SubCtrl.Image = ScaleFitPage((Bitmap)PictureItem.Picture, ReportForm.PAGE_HEIGHT, ReportForm.PAGE_WIDTH);
        }

        protected override void ReportCtrl_Load(object sender, EventArgs e)
        {
            base.ReportCtrl_Load(sender, e);
            SetSubCtrl();
        }

        public override void Draw(Graphics g, Rectangle bounds)
        {
            Point p = bounds.Location;
            p.Offset(SubCtrl.Location);

            if (oldBounds.Size.Width == 0 || oldBounds.Size.Height == 0)
            {
                oldBounds.Size = this.Size;
            }

            Rectangle rectSrc = new Rectangle(new Point(0, 0), PictureItem.OldImageSize);

            Rectangle rectDec = new Rectangle(p, SubCtrl.Size);

            g.DrawImage(SubCtrl.Image, rectDec, rectSrc, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// scale image to fit current page
        /// </summary>
        /// <param name="b">source image</param>
        /// <param name="destHeight">page height</param>
        /// <param name="destWidth">page width</param>
        /// <returns></returns>
        public Bitmap ScaleFitPage(Bitmap b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // equal scale          
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);      
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();   
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }
    }
}
