﻿using System;
using System.ComponentModel;
using CII.LAR.DrawTools;

namespace CII.LAR.UI
{
    public partial class RulerAppearanceCtrl : BaseCtrl
    {
        private GraphicsPropertiesManager graphicsPropertiesManager = Program.SysConfig.GraphicsPropertiesManager;

        private GraphicsProperties graphicsProperties;
        private RichPictureBox pictureBox;
        public RulerAppearanceCtrl(RichPictureBox pictureBox) : base()
        {
            this.pictureBox = pictureBox;
            resources = new ComponentResourceManager(typeof(RulerAppearanceCtrl));
            this.ShowIndex = 4;
            this.CtrlType = CtrlType.RulerAppearanceCtrl;
            InitializeComponent();
            this.InitializeThumbCustomShape();
        }

        private void SetSliderValue()
        {
            invokeColorChange = false;
            this.sliderTargetSize.Value = (int)graphicsProperties.TextSize;
            this.sliderThickness.Value = graphicsProperties.PenWidth;
            this.sliderTransparency.Value = (int)(graphicsProperties.Alpha * 100 / 255f);
            //this.sliderTickLength.Value = graphicsProperties.TargetSize;
            this.sliderColour.Value = graphicsProperties.ColorIndex() * 10;
            invokeColorChange = true;
        }

        private void btnLaserCtrl_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.StatisticsCtrl);
        }

        private void cmboxRuler_DropDown(object sender, EventArgs e)
        {
            UpdateTimerStatesHandler?.Invoke(false);
        }

        private void cmboxRuler_DropDownClosed(object sender, EventArgs e)
        {
            UpdateTimerStatesHandler?.Invoke(true);
        }

        public delegate void UpdateTimerState(bool enable);
        public UpdateTimerState UpdateTimerStatesHandler;

        private void cmboxRuler_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = this.cmboxRuler.SelectedItem.ToString();
            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName(name);
            SetSliderValue();

        }

        private void sliderTransparency_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderTransparency.Value;
            if (graphicsProperties != null)
            {
                graphicsProperties.Alpha = (int)(0xFF * value / 100f);
            }
        }

        private void Slider_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (DelegateClass.GetDelegate().VideoKeyDownHandler != null)
            {
                DelegateClass.GetDelegate().VideoKeyDownHandler(new System.Windows.Forms.KeyEventArgs(e.KeyCode));
            }
        }

        private void sliderThickness_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderThickness.Value;
            if (graphicsProperties != null)
            {
                graphicsProperties.PenWidth = value;
            }
        }

        private void sliderTargetSize_ValueChanged(object sender, EventArgs e)
        {
            if (invokeColorChange)
            {
                var value = this.sliderTargetSize.Value;
                if (graphicsProperties != null)
                {
                    graphicsProperties.TextSize = value;
                }
            }
        }

        private bool invokeColorChange = true;
        private void sliderColour_ValueChanged(object sender, EventArgs e)
        {
            if (invokeColorChange)
            {
                var value = (int)(this.sliderColour.Value / 10f);
                if (graphicsProperties != null)
                {
                    graphicsProperties.ChangeColor(value);
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                if (pictureBox.GraphicsList != null && pictureBox.GraphicsList.Count > 0)
                {
                    for (int i = 0; i < this.cmboxRuler.Items.Count; i++)
                    {
                        if (this.cmboxRuler.Items[i].ToString() == pictureBox.GraphicsList[0].ObjectType.ToString())
                        {
                            this.cmboxRuler.SelectedItem = this.cmboxRuler.Items[i];
                            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName(this.cmboxRuler.Items[i].ToString());
                            SetSliderValue();
                            break;
                        }
                    }
                }
            }
            this.pictureBox.InvokeMouseWheel = !this.Visible;
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrRulerAppearanceTitle;
            resources.ApplyResources(this.btnLaserCtrl, btnLaserCtrl.Name);

            //resources.ApplyResources(cmboxRuler.Items, "cmboxRuler.Items");
            //resources.ApplyResources(this.comboItem2, "comboItem2");
            //resources.ApplyResources(this.comboItem3, "comboItem3");
            this.cmboxRuler.Items.Clear();
            this.cmboxRuler.Items.AddRange(new object[] {
            resources.GetString("cmboxRuler.Items"),
            resources.GetString("cmboxRuler.Items1"),
            resources.GetString("cmboxRuler.Items2")});
            this.Invalidate();
        }
    }
}
