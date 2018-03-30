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

        public delegate void UpdateLense(Lense lense);
        public UpdateLense UpdateLenseHandler;

        public delegate void UpdateSimulatorImage(int selectIndex);
        public UpdateSimulatorImage UpdateSimulatorImageHandler;
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

        protected override void RefreshUI()
        {
            this.Title = global::CII.LAR.Properties.Resources.StrSetting;
            resources.ApplyResources(this.labelItemLanguage, labelItemLanguage.Name);
            resources.ApplyResources(this.labelItemStoragePath, labelItemStoragePath.Name);
            resources.ApplyResources(this.labelItemCamera, labelItemCamera.Name);
            resources.ApplyResources(this.lblConnectedInfo, lblConnectedInfo.Name);
            resources.ApplyResources(this.lblLaser, lblLaser.Name);
            resources.ApplyResources(this.lblSimulator, lblSimulator.Name);
            resources.ApplyResources(this.lense, lense.Name);
            resources.ApplyResources(this.btnDelete, btnDelete.Name);
            resources.ApplyResources(this.labelItemScale, labelItemScale.Text);
            this.itemContainer2.Refresh();
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

        /// <summary>
        /// Create a new object lense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// delete existing lense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var lense = comboBoxItemLense.SelectedItem;
            if (lense != null)
            {
                int index = Program.SysConfig.Lenses.FindIndex(l => (l.ToString() == lense.ToString()));
                Program.SysConfig.DeleteLense(lense.ToString());
                comboBoxItemLense.Items.Clear();
                comboBoxItemLense.Items.AddRange(Program.SysConfig.Lenses.ToArray());
                comboBoxItemLense.SelectedIndex = index - 1;

            }
        }

        private void textBoxLense_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int factor = 0;
                Int32.TryParse(textBoxLense.Text, out factor);
                if (factor != 0)
                {
                    Lense lense = new Lense(factor);
                    if (Program.SysConfig.AddLense(lense))
                    {
                        UpdateComBoxItemLense(lense);
                        UpdateLenseHandler?.Invoke(lense);
                    }

                }
                else
                {
                    //should input correct lense factor
                    //TO DO
                }
            }
        }

        private void UpdateComBoxItemLense(Lense lense)
        {
            comboBoxItemLense.Items.Clear();
            comboBoxItemLense.Items.AddRange(Program.SysConfig.Lenses.ToArray());
            int index = Program.SysConfig.Lenses.FindIndex(l => (l.ToString() == lense.ToString()));
            comboBoxItemLense.SelectedIndex = index;
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

        private void cmbLaser_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnSimulator_Click(object sender, EventArgs e)
        {
            richPictureBox.LoadImage(string.Format("{0}\\Resources\\Simulator\\Embryo.bmp", System.Environment.CurrentDirectory));
            Program.SysConfig.LiveMode = false;
        }
    }
}