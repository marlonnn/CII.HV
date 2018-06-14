using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialToolStripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
            using (var b = new SolidBrush(((int)Primary.BlueGrey900).ToColor()))
            {
                var rect = new Rectangle(0, e.ToolStrip.Height, e.ToolStrip.Width, e.ToolStrip.Height);
                e.Graphics.FillRectangle(b, e.AffectedBounds);
                //e.Graphics.FillRectangle(shadow, rect);
            }
        }

                // Render button selected and pressed state
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
            base.OnRenderButtonBackground(e);
            //var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            //var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);

            //if (e.Item.Selected == true || (e.Item as ToolStripButton).Checked) {
            //    using (var b = new LinearGradientBrush(rect, clsColor.clrToolstripBtnGrad_White,
            //        clsColor.clrToolstripBtnGrad_Blue, LinearGradientMode.Vertical)) {
            //        using (var b2 = new SolidBrush(clsColor.clrToolstripBtn_Border)) {
            //            e.Graphics.FillRectangle(b2, rectBorder);
            //            e.Graphics.FillRectangle(b, rect);
            //        }
            //    }
            //}
            //if (e.Item.Pressed) {
            //    using (var b = new LinearGradientBrush(rect, clsColor.clrToolstripBtnGrad_White_Pressed,
            //        clsColor.clrToolstripBtnGrad_Blue_Pressed, LinearGradientMode.Vertical)) {
            //        using (var b2 = new SolidBrush(clsColor.clrToolstripBtn_Border)) {
            //            e.Graphics.FillRectangle(b2, rectBorder);
            //            e.Graphics.FillRectangle(b, rect);
            //        }
            //    }
            //}
        }
    }
}
