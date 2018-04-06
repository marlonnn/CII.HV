namespace CII.LAR.UI
{
    partial class LaserDebugCtrl
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(306, 3);
            // 
            // slider
            // 
            // 
            // 
            // 
            this.slider.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider.Location = new System.Drawing.Point(71, 42);
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(251, 23);
            this.slider.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider.TabIndex = 1;
            this.slider.Text = "0.0";
            this.slider.Value = 0;
            this.slider.ValueChanged += new System.EventHandler(this.slider_ValueChanged);
            this.slider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slider_MouseUp);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 42);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "Red Laser";
            // 
            // LaserDebugCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slider);
            this.Controls.Add(this.labelX1);
            this.Name = "LaserDebugCtrl";
            this.Size = new System.Drawing.Size(325, 212);
            this.Title = "Laser Debug Form";
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.slider, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Slider slider;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}
