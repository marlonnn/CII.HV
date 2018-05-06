using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.SysClass.Shortcuts;

namespace CII.LAR.UI
{
    public partial class ShortcutCtrl : BaseCtrl
    {
        private HotKeyManager hotKeyManager;
        public ShortcutCtrl()
        {
            InitializeComponent();
            resources = new ComponentResourceManager(typeof(ShortcutCtrl));
            this.CtrlType = CtrlType.ShortCut;
            this.Load += ShortcutCtrl_Load;
            //this.hotKeyManager = Program.EntryForm.hotKeyManager;
        }

        private void ShortcutCtrl_Load(object sender, EventArgs e)
        {
            this.hotKeyManager = Program.EntryForm.hotKeyManager;
            RegisterKeyDownEvent();
        }

        private void RegisterKeyDownEvent()
        {
            this.txtTakePicture.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyIsSet);
            this.txtZoomIn.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyIsSet);
            this.txtZoomOut.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyIsSet);
            this.txtStart.HotKeyIsSet += new HotKeyIsSetEventHandler(HotKeyIsSet);
        }

        private void HotKeyIsSet(object sender, HotKeyIsSetEventArgs e)
        {
            if (Program.EntryForm.hotKeyManager.HotKeyExists(e.Shortcut, HotKeyManager.CheckKey.LocalHotKey))
            {
                e.Cancel = true;
                MessageBox.Show("This HotKey has already been registered");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Register();
            this.Visible = false;
        }

        private void Register()
        {
            if (!string.IsNullOrEmpty(txtTakePicture.Text) &&  txtTakePicture.Text != Keys.None.ToString())
            {
                LocalHotKey NewLocalHotKey = new LocalHotKey("takePicture", txtTakePicture.UserModifier, txtTakePicture.UserKey);
                NewLocalHotKey.Tag = "takePicture";
                hotKeyManager.AddLocalHotKey(NewLocalHotKey);
            }
            if (!string.IsNullOrEmpty(txtZoomIn.Text) &&  txtZoomIn.Text != Keys.None.ToString())
            {
                LocalHotKey NewLocalHotKey = new LocalHotKey("zoomIn", txtZoomIn.UserModifier, txtZoomIn.UserKey);
                NewLocalHotKey.Tag = "zoomIn";
                hotKeyManager.AddLocalHotKey(NewLocalHotKey);
            }
            if (!string.IsNullOrEmpty(txtZoomOut.Text) && txtZoomOut.Text != Keys.None.ToString())
            {
                LocalHotKey NewLocalHotKey = new LocalHotKey("zoomOut", txtZoomOut.UserModifier, txtZoomOut.UserKey);
                NewLocalHotKey.Tag = "zoomOut";
                hotKeyManager.AddLocalHotKey(NewLocalHotKey);
            }
            if (!string.IsNullOrEmpty(txtStart.Text) && txtStart.Text != Keys.None.ToString())
            {
                LocalHotKey NewLocalHotKey = new LocalHotKey("startRecord", txtStart.UserModifier, txtStart.UserKey);
                NewLocalHotKey.Tag = "startRecord";
                hotKeyManager.AddLocalHotKey(NewLocalHotKey);
            }
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
