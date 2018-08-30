namespace CII.LAR
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.materialSliderControl1 = new CII.LAR.MaterialSkin.MaterialSliderControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // materialSliderControl1
            // 
            this.materialSliderControl1.Location = new System.Drawing.Point(40, 111);
            this.materialSliderControl1.Name = "materialSliderControl1";
            this.materialSliderControl1.Size = new System.Drawing.Size(323, 30);
            this.materialSliderControl1.SliderValue = 50;
            this.materialSliderControl1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(445, 393);
            this.Controls.Add(this.materialSliderControl1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.MaterialSliderControl materialSliderControl1;
        private System.Windows.Forms.Timer timer1;
    }
}