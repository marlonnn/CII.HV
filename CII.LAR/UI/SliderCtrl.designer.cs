namespace CII.LAR.UI
{
    partial class SliderCtrl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.slider = new DevComponents.DotNetBar.Controls.Slider();
            this.PulseHoleWS = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // slider
            // 
            // 
            // 
            // 
            this.slider.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider.LabelVisible = false;
            this.slider.Location = new System.Drawing.Point(0, 1);
            this.slider.Maximum = 10;
            this.slider.Minimum = 1;
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(150, 18);
            this.slider.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider.TabIndex = 0;
            this.slider.Value = 1;
            this.slider.ValueChanged += new System.EventHandler(this.Slide_ValueChanged);
            // 
            // PulseHoleWS
            // 
            // 
            // 
            // 
            this.PulseHoleWS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.PulseHoleWS.Location = new System.Drawing.Point(154, 3);
            this.PulseHoleWS.Name = "PulseHoleWS";
            this.PulseHoleWS.Size = new System.Drawing.Size(130, 16);
            this.PulseHoleWS.TabIndex = 1;
            this.PulseHoleWS.Text = "0.200ms 5.0um";
            // 
            // SliderCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PulseHoleWS);
            this.Controls.Add(this.slider);
            this.Name = "SliderCtrl";
            this.Size = new System.Drawing.Size(284, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Slider slider;
        private DevComponents.DotNetBar.LabelX PulseHoleWS;
    }
}
