using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    /// <summary>
    /// Toast Notification
    /// Zhong Wen 2018/08/24
    /// </summary>
    public class ToastNotification : MaterialToast
    {
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // NewToast
            // 
            this.ClientSize = new System.Drawing.Size(129, 56);
            this.Name = "NewToast";
            this.ResumeLayout(false);

        }

        public ToastNotification()
        {
            InitializeComponent();
            DrawImage = true;
            timeOutInteral = 1500;
            sf = new StringFormat { LineAlignment = StringAlignment.Center };
        }

        public static ToastNotification Instance()
        {
            return new ToastNotification();
        }
        private StringFormat sf;
        private int timeOutInteral = 0;
        public int TimeOut
        {
            get { return this.timeOutInteral; }
            set { this.timeOutInteral = value; }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return this.recordCount; }
            set { this.recordCount = value; }
        }

        private string msg;
        public string Msg
        {
            get { return this.msg; }
            set
            {
                if (value != this.msg)
                {
                    this.msg = value;
                }
            }
        }

        private bool drawImage;
        private Timer timer;
        public Timer TostTimer
        {
            get { return this.timer; }
            set { this.timer = value; }
        }
        private System.ComponentModel.IContainer components;

        private bool DrawImage
        {
            get { return this.drawImage; }
            set { this.drawImage = value; }
        }

        private Image image;
        public Image ToastImage
        {
            get { return this.image; }
            set
            {
                if (value != this.image)
                {
                    this.image = value;
                }
            }
        }


        public void ShowToast(string messasge, Image toastImage, int timeOutInteral = 1000)
        {
            recordCount = 0;
            this.Msg = messasge;
            this.ToastImage = toastImage;
            this.timeOutInteral = timeOutInteral;

            Graphics g = this.CreateGraphics();
            SizeF msgSize = g.MeasureString(Msg, this.Font);
            this.Size = new Size((int)(65+ msgSize.Width), 58);
            this.Location = new Point(Program.EntryForm.Width / 2, Program.EntryForm.Height / 2);
            this.Invalidate();
            this.Show();
            this.timer.Enabled = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(boardPen, new System.Drawing.Rectangle(1, 1, this.Width - 2, this.Height - 2));
            if (DrawImage && ToastImage != null)
            {
                var iconRect = new Rectangle(4, 4, 50, 50);
                e.Graphics.DrawImage(ToastImage, iconRect);
            }

            if (!string.IsNullOrEmpty(this.Msg))
            {
                SizeF msgSize = e.Graphics.MeasureString(Msg, this.Font);
                e.Graphics.DrawString(this.Msg, this.Font, Brushes.White, new PointF(60, this.Height / 2f ), sf);
            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            recordCount += 300;
            if(recordCount > timeOutInteral)
            {
                this.timer.Enabled = false;
                this.Close();
            }

        }
    }
}
