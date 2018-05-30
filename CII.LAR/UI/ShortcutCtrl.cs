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
        public ShortcutCtrl(HotKeyManager hotKeyManager)
        {
            InitializeComponent();
            this.hotKeyManager = hotKeyManager;
            resources = new ComponentResourceManager(typeof(ShortcutCtrl));
            this.CtrlType = CtrlType.ShortCut;
            this.Load += ShortcutCtrl_Load;
        }

        private void ShortcutCtrl_Load(object sender, EventArgs e)
        {
            //hotKeyManager = new HotKeyManager(Program.EntryForm);
            RegisterKeyDownEvent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                InitializeShortcutKeys();
            }
        }
        private void InitializeShortcutKeys()
        {
            foreach (var shortcutKey in Program.SysConfig.LocalHotKeyContainer)
            {
                if (shortcutKey != null)
                {
                    switch (shortcutKey.Name)
                    {
                        case "takePicture":
                            this.txtTakePicture.Text = HotKeyShared.CombineShortcut(shortcutKey.Modifier, shortcutKey.Key);
                            break;
                        case "zoomIn":
                            this.txtZoomIn.Text = HotKeyShared.CombineShortcut(shortcutKey.Modifier, shortcutKey.Key);
                            break;
                        case "zoomOut":
                            this.txtZoomOut.Text = HotKeyShared.CombineShortcut(shortcutKey.Modifier, shortcutKey.Key);
                            break;
                        case "startRecord":
                            this.txtStart.Text = HotKeyShared.CombineShortcut(shortcutKey.Modifier, shortcutKey.Key);
                            break;
                    }
                }
            }
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
            if (hotKeyManager.HotKeyExists(e.Shortcut, HotKeyManager.CheckKey.LocalHotKey) || CheckHotKeyExist())
            {
                e.Cancel = true;
                MessageBox.Show("This HotKey has already been registered");
            }
        }

        /// <summary>
        /// 检查界面上输入的快捷键是否重复
        /// </summary>
        /// <returns></returns>
        private bool CheckHotKeyExist()
        {
            List<string> keys = new List<string>();
            for (int i=0; i< this.Controls.Count; i++)
            {
                var hotkeyCtrl = this.Controls[i] as HotKeyControl;
                if (hotkeyCtrl != null)
                {
                    if (!string.IsNullOrEmpty(hotkeyCtrl.Text))
                    {
                        keys.Add(hotkeyCtrl.Text);
                    }
                }
            }
            return keys.GroupBy(n => n).Any(c => c.Count() > 1);
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            UnRegister();
            Register();
            this.Visible = false;
        }

        private void UnRegister()
        {
            var keys = Program.SysConfig.LocalHotKeyContainer.ToList();
            foreach (var shortkey in keys)
            {
                hotKeyManager.RemoveHotKey(shortkey.Name);
            }
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
