using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class ShortcutCtrl : BaseCtrl
    {
        public ShortcutCtrl()
        {
            InitializeComponent();
            resources = new ComponentResourceManager(typeof(ShortcutCtrl));
            this.CtrlType = CtrlType.ShortCut;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public override void RefreshUI()
        {
            this.Title = global::CII.LAR.Properties.Resources.StrShortcutTitle;
            resources.ApplyResources(this.buttonSave, buttonSave.Name);
            resources.ApplyResources(this.lblVideo, lblVideo.Name);
            resources.ApplyResources(this.lblZoomOut, lblZoomOut.Name);
            resources.ApplyResources(this.lblZoomIn, lblZoomIn.Name);
            resources.ApplyResources(this.lblSnapshoot, lblSnapshoot.Name);
            this.Invalidate();
        }
    }
}
