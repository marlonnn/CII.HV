using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    public partial class ImageTracker : UserControl, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        /// <summary>
        /// indicate if is the highlight rectangle dragging
        /// </summary>
        private bool isDragging = false;

        /// <summary>
        /// last mouse position in dragging highlight rectangle
        /// </summary>
        private Point lastMousePosOfDragging = new Point(0, 0);

        /// <summary>
        /// Scroll picture event handler
        /// </summary>
        /// <param name="xMovementRate">horizontal scroll movement rate which may be nagtive value</param>
        /// <param name="yMovementRate">vertical scroll movement rate which may be nagtive value</param>
        public delegate void ScrollPictureEventHandler(float xMovementRate, float yMovementRate);

        /// <summary>
        /// Scroll picture event to ask ScalablePictureBox to scroll picture
        /// </summary>
        public event ScrollPictureEventHandler ScrollPictureEvent;

        /// <summary>
        /// image thumbnail for tracking image.
        /// We make thumbnail of original picture for performance consideration
        /// instead of using original picture for tracking.
        /// </summary>
        private Image thumbnail = null;

        /// <summary>
        /// rectangle area where to draw the thumbnail picture
        /// </summary>
        private Rectangle pictureDestRect;

        /// <summary>
        /// rectangle area where to draw part of visible picture
        /// </summary>
        private Rectangle highlightingRect;

        /// <summary>
        /// current zoom rate
        /// </summary>
        private float zoomRate = 100;

        /// <summary>
        /// zoom rate of current image
        /// </summary>
        public float ScalePercent
        {
            get { return this.zoomRate; }
            set
            {
                if (value != zoomRate)
                {
                    zoomRate = value;
                    this.Visible = zoomRate != 100;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Font for drawing zoom rate text
        /// </summary>
        private Font zoomRateFont = new System.Drawing.Font("Times New Roman", 9.75F, 
            ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), 
            System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        /// <summary>
        /// a transperent brush for shadowing invisible part of picture.
        /// It uses sliver color for shadowing picture
        /// </summary>
        private Brush tranparentBrush = new SolidBrush(Color.FromArgb(180, 0xc0, 0xc0, 0xc0));

        /// <summary>
        /// image for tracking
        /// </summary>
        public Image Picture
        {
            get { return this.thumbnail; }
            set
            {
                //if (thumbnail != null)
                //{
                //    thumbnail.Dispose();
                //}
                if (value != null)
                {
                    Rectangle srcRect = this.picturePanel.ClientRectangle;
                    srcRect.X += 1;
                    srcRect.Y += 1;
                    srcRect.Width -= 2;
                    srcRect.Height -= 2;
                    thumbnail = Util.CreateThumbnail(value, srcRect.Height);
                    pictureDestRect = Util.ScaleToFit(thumbnail, srcRect, false);
                    highlightingRect = new Rectangle(0, 0, 0, 0);
                }
            }
        }

        private RichPictureBox richPictureBox;
        public ImageTracker(RichPictureBox richPictureBox)
        {
            InitializeComponent();
            this.richPictureBox = richPictureBox;
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
        }

        public void OnPicturePainted(Rectangle showingRect, Rectangle pictureBoxRect)
        {
            Region regionToInvalidate;
            if (highlightingRect.IsEmpty)
            {
                //After start or picture change redraw the entire thumbnail.
                regionToInvalidate = new Region(picturePanel.ClientRectangle);
            }
            else
            {
                // Redraw the thumbnail part covered till now.
                regionToInvalidate = new Region(highlightingRect);
            }
            float widthScale = (float)showingRect.Width / (float)pictureBoxRect.Width;
            float xPosScale = (float)showingRect.X / (float)pictureBoxRect.Width;
            float heightScale = (float)showingRect.Height / (float)pictureBoxRect.Height;
            float yPosScale = (float)showingRect.Y / (float)pictureBoxRect.Height;
            highlightingRect = new Rectangle((int)(this.pictureDestRect.X + this.pictureDestRect.Width * xPosScale),
            (int)(this.pictureDestRect.Y + this.pictureDestRect.Height * yPosScale),
            (int)(this.pictureDestRect.Width * widthScale),
            (int)(this.pictureDestRect.Height * heightScale));

            regionToInvalidate.Union(highlightingRect); // Also redraw the part now highlighted.

            // Redraw only old and new highlighted rectangles
            picturePanel.Invalidate(regionToInvalidate);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // draw control border
            //Rectangle borderRect = this.ClientRectangle;
            //borderRect.Width -= 1;
            //borderRect.Height -= 1;
            //e.Graphics.DrawRectangle(Pens.Navy, borderRect);

            using (SolidBrush sb = new SolidBrush(Color.FromArgb(0x1A, 0x1E, 0x25)))
            {
                e.Graphics.FillRectangle(sb, new Rectangle(0, 0, Width, 20));
                using (Pen pen = new Pen(Color.FromArgb(0x1A, 0x1E, 0x25), 1.0f))
                    e.Graphics.DrawRectangle(pen, new Rectangle(1, 1, this.Width - 2, this.Height - 2));
            }

            // draw zoom rate text

            using (SolidBrush sb = new SolidBrush(Color.FromArgb(0xAD, 0xB8, 0xD0)))
                e.Graphics.DrawString("Zoom rate:" + (int)ScalePercent + "%", this.Font, sb, 3, 3);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (DelegateClass.GetDelegate().VideoKeyDownHandler != null)
            {
                DelegateClass.GetDelegate().VideoKeyDownHandler(e);
            }
        }

        /// <summary>
        /// begin to drag highlight rectangle if mouse is down within the highlight rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picturePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.highlightingRect.Contains(e.X, e.Y))
            {
                isDragging = true;
                lastMousePosOfDragging = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// fire scroll picture event when highlight rectangle is dragged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picturePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (ScrollPictureEvent != null && isDragging &&
                (lastMousePosOfDragging.X != e.X || lastMousePosOfDragging.Y != e.Y))
            {
                int offsetX = e.X - lastMousePosOfDragging.X;
                int offsetY = e.Y - lastMousePosOfDragging.Y;
                lastMousePosOfDragging = new Point(e.X, e.Y);

                // 1.Calculate horizontal and vertical mouse movement rates relative to the pictureDestRect
                //   the mouse movement rates may be nagtive value if mouse moved to left or up
                // 2.Raise ScrollPictureEvent to scroll actual picture in the ScalablePictureBox
                float xMovementRate = (float)offsetX / (float)pictureDestRect.Width;
                float yMovementRate = (float)offsetY / (float)pictureDestRect.Height;
                ScrollPictureEvent(xMovementRate, yMovementRate);
            }
        }

        /// <summary>
        /// end dragging highlight rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picturePanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void picturePanel_Paint(object sender, PaintEventArgs e)
        {
            if (thumbnail != null)
            {
                // draw thumbnail image
                e.Graphics.DrawImage(this.thumbnail, this.pictureDestRect);

                // adjust highlighting region of visible picture area
                Region highlightRegion = new Region(this.pictureDestRect);
                if (highlightingRect.Width > 0 && highlightingRect.Height > 0)
                {
                    highlightRegion.Exclude(highlightingRect);
                }
                e.Graphics.FillRegion(tranparentBrush, highlightRegion);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Rectangle borderRect = this.ClientRectangle;

            const int MSG_HEIGHT = 18;
            const int OFFSET = 5;
            this.picturePanel.Location = new Point(OFFSET, MSG_HEIGHT);
            this.picturePanel.Width = this.ClientRectangle.Width - OFFSET * 2;
            this.picturePanel.Height = this.ClientRectangle.Height - (MSG_HEIGHT + OFFSET);
        }
    }
}
