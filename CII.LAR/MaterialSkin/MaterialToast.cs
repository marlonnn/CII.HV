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
    /// Base Material Toast Notification form
    /// Zhong Wen 2018/08/24
    /// </summary>
    public class MaterialToast : Form
    {
        protected Pen boardPen;
        public MaterialToast()
        {
            FormBorderStyle = FormBorderStyle.None;
            boardPen = new Pen(Color.WhiteSmoke, 2f);
        }

        protected override void Dispose(bool disposing)
        {
            if (boardPen != null) boardPen.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.Clear(MaterialSkinManager.Instance.GetApplicationBackgroundColor());
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ToastNotification
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToastNotification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
