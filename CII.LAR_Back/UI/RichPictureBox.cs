using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CII.LAR.UI
{
    public partial class RichPictureBox : PictureBox
    {
        public Image Frame { get; set; }
        public RichPictureBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (this.Image != null)
                {
                    // Get the current image.
                    //Image image = this.Image;

                    //// If the image is not null, then dispose.
                    //if (image != null)
                    //{
                    //    // Dispose.
                    //    image.Dispose();
                    //}

                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //e.Graphics.ScaleTransform(zoom, zoom);
                    //e.Graphics.TranslateTransform(OffsetX, OffsetY);
                    //this.Image = this.Frame;
                    e.Graphics.DrawImage(this.Image, 0, 0, this.Bounds.Width, this.Bounds.Height);
                    using (Pen pen = new Pen(Color.Blue, 2f))
                    {
                        e.Graphics.DrawLine(pen, 0, 10, 200, 10);
                    }
                    //e.Graphics.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
