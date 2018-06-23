using CII.LAR.MaterialSkin;

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
            this.colorSlider1 = new CII.LAR.MaterialSkin.ColorSlider();
            this.materialSlider2 = new CII.LAR.MaterialSkin.MaterialSlider();
            this.SuspendLayout();
            // 
            // colorSlider1
            // 
            this.colorSlider1.BackColor = System.Drawing.Color.Transparent;
            this.colorSlider1.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSlider1.LargeChange = ((uint)(5u));
            this.colorSlider1.Location = new System.Drawing.Point(67, 95);
            this.colorSlider1.Name = "colorSlider1";
            this.colorSlider1.Size = new System.Drawing.Size(200, 30);
            this.colorSlider1.SmallChange = ((uint)(1u));
            this.colorSlider1.TabIndex = 0;
            this.colorSlider1.Text = "colorSlider1";
            this.colorSlider1.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            // 
            // materialSlider2
            // 
            this.materialSlider2.BackColor = System.Drawing.Color.Transparent;
            this.materialSlider2.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.materialSlider2.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.materialSlider2.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.materialSlider2.Depth = 0;
            this.materialSlider2.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.materialSlider2.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.materialSlider2.LargeChange = ((uint)(5u));
            this.materialSlider2.Location = new System.Drawing.Point(87, 60);
            this.materialSlider2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialSlider2.Name = "materialSlider2";
            this.materialSlider2.Size = new System.Drawing.Size(150, 20);
            this.materialSlider2.SmallChange = ((uint)(1u));
            this.materialSlider2.TabIndex = 2;
            this.materialSlider2.Text = "materialSlider2";
            this.materialSlider2.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.materialSlider2.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.materialSlider2.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.materialSlider2.ThumbSize = 6;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.materialSlider2);
            this.Controls.Add(this.colorSlider1);
            this.Name = "TestForm";
            this.Text = "···";
            this.ResumeLayout(false);

        }

        #endregion

        private ColorSlider colorSlider1;
        private MaterialSlider materialSlider2;
    }
}