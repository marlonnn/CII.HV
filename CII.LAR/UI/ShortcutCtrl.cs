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
            RegisterKeyDownEvent();
        }

        private void RegisterKeyDownEvent()
        {
            this.txtSnap.KeyDown += TxtSnap_KeyDown; ;
            this.txtVideo.KeyDown += TextBox_KeyDown;
            this.txtZoomIn.KeyDown += TextBox_KeyDown;
            this.txtZoomOut.KeyDown += TextBox_KeyDown;
        }

        private void TxtSnap_KeyDown(object sender, KeyEventArgs e)
        {
            Program.SysConfig.ShortcutKeys.AddSnapShortKey(e.KeyCode);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;
            var keyControl = e.Control;
            var cd = keyCode.ToString();
            var kc = keyControl.ToString();
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            var keyCode = e.KeyCode;
            var keyControl = e.Control;
            var cd = keyCode.ToString();
            var kc = keyControl.ToString();
        }
    }
}
