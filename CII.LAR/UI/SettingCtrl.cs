using AForge.Video.DirectShow;
using CII.LAR.SysClass;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class SettingCtrl : BaseCtrl
    {
        private RichPictureBox richPictureBox;

        private SystemInfoForm systemInfoForm;

        public delegate void UpdateTimerState(bool enable);
        public UpdateTimerState UpdateTimerStatesHandler;

        public delegate void UpdateSimulatorImage(int selectIndex);
        public UpdateSimulatorImage UpdateSimulatorImageHandler;

        public delegate void ShowObjectLenseManager();
        public ShowObjectLenseManager ShowObjectLenseManagerHandler;

        public delegate void ShowShortcutManager();
        public ShowShortcutManager ShowShortcutManagerHandler;

        public delegate void ShowScaleAppearanceCtrl();
        public ShowScaleAppearanceCtrl ShowScaleAppearanceCtrlHandler;

        public SettingCtrl(RichPictureBox richPictureBox) : base()
        {
            this.richPictureBox = richPictureBox;
            this.ShowIndex = 0;
            this.CtrlType = CtrlType.SettingCtrl;
            InitializeComponent();
            resources = new ComponentResourceManager(typeof(SettingCtrl));
            this.textBoxItemStoragePath.Text = Program.SysConfig.StorePath;
            UpdateComboLanguage();
            InitializeLaserType();
            InitializeScaleCoefficient();
            this.cmbLaser.SelectedIndexChanged += new System.EventHandler(this.cmbLaser_SelectedIndexChanged);
            this.cbxScale.SelectedIndexChanged += CbxScale_SelectedIndexChanged;
            InitializeCmbTime();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                updateCmbLaser = false;
                cmbLaser.SelectedIndex = Program.EntryForm.LaserType == LaserType.SaturnFixed ? 0 : 1;
                updateCmbLaser = true;
            }
        }

        private void CbxScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.SysConfig.DefaultScaleCoefficient = (int)this.cbxScale.SelectedItem;
        }

        private void InitializeScaleCoefficient()
        {
            for(int i =1; i < 17; i++)
            {
                this.cbxScale.Items.Add(i);
            }
            this.cbxScale.SelectedIndex = Program.SysConfig.DefaultScaleCoefficient - 1;
        }

        /// <summary>
        /// update combo language item when load or make a change
        /// </summary>
        private void UpdateComboLanguage()
        {
            foreach (ComboItem item in comboBoxItemLanguage.Items)
            {
                if (item.Value.ToString() == Program.SysConfig.UICulture)
                {
                    comboBoxItemLanguage.SelectedItem = item;
                    break;
                }
            }
        }

        private void InitializeLaserType()
        {
            cmbLaser.SelectedIndex = 0;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && 
                    !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    Program.SysConfig.StorePath = fbd.SelectedPath;
                    this.textBoxItemStoragePath.Text = Program.SysConfig.StorePath;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonSysInfo_Click(object sender, EventArgs e)
        {
            systemInfoForm = new SystemInfoForm();
            systemInfoForm.ShowDialog();
        }

        private void ComboBoxItemLanguage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string language = this.comboBoxItemLanguage.SelectedItem.ToString();
            var culture = ((ComboItem)comboBoxItemLanguage.SelectedItem).Value.ToString();
            Program.SysConfig.UICulture = culture;
        }

        private void comboBoxItemLanguage_ExpandChange(object sender, EventArgs e)
        {
            var v = this.comboBoxItemLanguage.Expanded;
            UpdateTimerStatesHandler?.Invoke(!this.comboBoxItemLanguage.Expanded);
        }

        public override void RefreshUI()
        {
            this.Title = global::CII.LAR.Properties.Resources.StrSetting;
            resources.ApplyResources(this.labelItemLanguage, labelItemLanguage.Name);
            resources.ApplyResources(this.labelItemStoragePath, labelItemStoragePath.Name);
            resources.ApplyResources(this.lense, lense.Name);
            resources.ApplyResources(this.labelItem1, labelItem1.Name);
            resources.ApplyResources(this.labelItemCamera, labelItemCamera.Name);
            resources.ApplyResources(this.lblConnectedInfo, lblConnectedInfo.Name);
            resources.ApplyResources(this.lblLaser, lblLaser.Name);
            resources.ApplyResources(this.lblSimulator, lblSimulator.Name);
            resources.ApplyResources(this.lense, lense.Name);
            resources.ApplyResources(this.labelItemScale, labelItemScale.Name);
            resources.ApplyResources(this.labelItem2, labelItem2.Name);
            resources.ApplyResources(this.btnShortcuts, btnShortcuts.Name);
            resources.ApplyResources(this.labelItem3, labelItem3.Name);
            resources.ApplyResources(this.btnScaleAppearance, btnScaleAppearance.Name);
            resources.ApplyResources(this.comboItem5, "comboItem5");
            resources.ApplyResources(this.comboItem6, "comboItem6");
            this.cmbLaser.Refresh();
            //this.itemContainer2.Refresh();
            foreach (var ctrl in this.Controls)
            {
                Button btnX = ctrl as Button;
                if (btnX != null)
                {
                    resources.ApplyResources(btnX, btnX.Name);
                }

                ItemPanel itemPanel = ctrl as ItemPanel;
                if (itemPanel != null)
                {
                    foreach (var itemCtrl in itemPanel.Controls)
                    {
                        Button subBtnX = itemCtrl as Button;
                        if (subBtnX != null)
                        {
                            resources.ApplyResources(subBtnX, subBtnX.Name);
                        }
                    }
                }
            }
            this.Invalidate();
        }

        private void SettingCtrl_Load(object sender, EventArgs e)
        {

        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //change simulator image
            if (UpdateSimulatorImageHandler != null)
            {
                UpdateSimulatorImageHandler(cmbImage.SelectedIndex);
                Program.SysConfig.LiveMode = false;
            }
        }

        private bool updateCmbLaser = true;

        private void cmbLaser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updateCmbLaser)
            {
                switch (cmbLaser.SelectedIndex)
                {
                    case 0:
                        Program.EntryForm.LaserType = LaserType.SaturnFixed;
                        break;
                    case 1:
                        Program.EntryForm.LaserType = LaserType.SaturnActive;
                        break;
                }
                Program.EntryForm.HolesNumberSlider(Program.EntryForm.LaserType == LaserType.SaturnActive);
            }
        }

        private void btnSimulator_Click(object sender, EventArgs e)
        {
            if (Program.SysConfig.LiveMode)
            {
                //Turn on simulator
                Program.EntryForm.StopVideoDevice();
                richPictureBox.LoadImage(string.Format("{0}\\Resources\\Simulator\\Embryo.bmp", System.Environment.CurrentDirectory));
                this.btnSimulator.Text = CII.LAR.Properties.Resources.StrCloseSimulator;
                Program.SysConfig.LiveMode = false;
            }
            else
            {
                //Close simulator
                if (this.richPictureBox != null)
                {
                    this.richPictureBox.Picture = null;
                    this.richPictureBox.GraphicsList.DeleteAll();
                    CtrlFactory.GetCtrlFactory().GetCtrlByType<StatisticsCtrl>(CtrlType.StatisticsCtrl).StatisticsListView.Items.Clear();
                }
                if (!string.IsNullOrEmpty(Program.SysConfig.DeviceName))
                {
                    FilterInfo fileInfo = Program.SysConfig.EnumerateVideoDevices();
                    DelegateClass.GetDelegate().CaptureDeviceHandler(fileInfo.MonikerString);

                }
                this.btnSimulator.Text = CII.LAR.Properties.Resources.StrOpenSimulator;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowObjectLenseManagerHandler?.Invoke();
        }

        private void cmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.SysConfig.RecordTime = Int32.Parse(this.cmbTime.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
            }
        }

        private void InitializeCmbTime()
        {
            foreach (var item in cmbTime.Items)
            {
                if (Int32.Parse(item.ToString()) == Program.SysConfig.RecordTime)
                {
                    this.cmbTime.SelectedItem = item;
                }
            }
        }

        private void btnShortcuts_Click(object sender, EventArgs e)
        {
            ShowShortcutManagerHandler?.Invoke();
            //ShortcutsForm sf = new ShortcutsForm();
            //sf.ShowDialog();
        }

        private void btnScaleAppearance_Click(object sender, EventArgs e)
        {
            ShowScaleAppearanceCtrlHandler?.Invoke();
        }
    }
}