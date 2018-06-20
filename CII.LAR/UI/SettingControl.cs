﻿using AForge.Video.DirectShow;
using CII.LAR.MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class SettingControl : BaseCtrl
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

        public SettingControl (RichPictureBox richPictureBox) : base()
        {
            this.richPictureBox = richPictureBox;
            InitializeComponent();
            this.CtrlType = CtrlType.SettingCtrl;
            resources = new ComponentResourceManager(typeof(SettingControl));
            this.textBoxItemStoragePath.Text = Program.SysConfig.StorePath;
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
            for (int i = 1; i < 17; i++)
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
            string language = CultureToLanguage(Program.SysConfig.UICulture);
            for (int i = 0; i < comboBoxItemLanguage.Items.Count; i++)
            {
                if ((comboBoxItemLanguage.Items[i]).ToString() == language)
                {
                    comboBoxItemLanguage.SelectedIndex = i;
                    break;
                }
            }
        }

        private void InitializeLaserType()
        {
            cmbLaser.SelectedIndex = 0;
        }

        private void ComboBoxItemLanguage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string language = this.comboBoxItemLanguage.SelectedItem.ToString();
            //var culture = GetCulture(language);
            Program.SysConfig.UICulture = LanguageToCulture(language);
        }

        private string LanguageToCulture(string language)
        {
            string culture = "zh-CN";
            switch (language)
            {
                case "English":
                    culture = "en-US";
                    break;
                case "简体中文":
                    culture = "zh-CN";
                    break;
            }
            return culture;
        }

        private string CultureToLanguage(string culture)
        {
            string language = "简体中文";
            switch (culture)
            {
                case "English":
                    language = "en-US";
                    break;
                case "简体中文":
                    language = "zh-CN";
                    break;
            }
            return language;
        }

        public override void RefreshUI()
        {

            this.Title = global::CII.LAR.Properties.Resources.StrSetting;
            resources.ApplyResources(this.groupBoxLanguage, groupBoxLanguage.Name);
            resources.ApplyResources(this.groupBoxStoragePath, groupBoxStoragePath.Name);
            resources.ApplyResources(this.groupBoxObjectLense, groupBoxObjectLense.Name);
            //resources.ApplyResources(this.labelItem1, labelItem1.Name);
            resources.ApplyResources(this.lblCamera, lblCamera.Name);
            //resources.ApplyResources(this.conne, lblConnectedInfo.Name);
            //resources.ApplyResources(this.lblLaser, lblLaser.Name);
            //resources.ApplyResources(this.lblSimulator, lblSimulator.Name);
            //resources.ApplyResources(this.lense, lense.Name);
            //resources.ApplyResources(this.labelItemScale, labelItemScale.Name);
            //resources.ApplyResources(this.labelItem2, labelItem2.Name);
            resources.ApplyResources(this.btnShortcuts, btnShortcuts.Name);
            //resources.ApplyResources(this.labelItem3, labelItem3.Name);
            resources.ApplyResources(this.btnScaleAppearance, btnScaleAppearance.Name);
            //resources.ApplyResources(this.comboItem5, "comboItem5");
            //resources.ApplyResources(this.comboItem6, "comboItem6");
            //this.cmbLaser.Refresh();
            ////this.itemContainer2.Refresh();
            //foreach (var ctrl in this.Controls)
            //{
            //    Button btnX = ctrl as Button;
            //    if (btnX != null)
            //    {
            //        resources.ApplyResources(btnX, btnX.Name);
            //    }

            //    ItemPanel itemPanel = ctrl as ItemPanel;
            //    if (itemPanel != null)
            //    {
            //        foreach (var itemCtrl in itemPanel.Controls)
            //        {
            //            Button subBtnX = itemCtrl as Button;
            //            if (subBtnX != null)
            //            {
            //                resources.ApplyResources(subBtnX, subBtnX.Name);
            //            }
            //        }
            //    }
            //}
            this.Invalidate();
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

        public void SettingControl_Load(object sender, EventArgs e)
        {
            UpdateComboLanguage();
        }

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

        private void materialRoundButton1_Click(object sender, EventArgs e)
        {
            ShowObjectLenseManagerHandler?.Invoke();
        }

        private void btnShortcuts_Click(object sender, EventArgs e)
        {
            ShowShortcutManagerHandler?.Invoke();
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

        private void btnScaleAppearance_Click(object sender, EventArgs e)
        {
            ShowScaleAppearanceCtrlHandler?.Invoke();
        }
    }
}
