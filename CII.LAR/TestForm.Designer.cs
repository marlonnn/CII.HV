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
            this.materialRoundButton1 = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.materialRoundButton2 = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.SuspendLayout();
            // 
            // materialRoundButton1
            // 
            this.materialRoundButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRoundButton1.Depth = 0;
            this.materialRoundButton1.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.materialRoundButton1.Icon = null;
            this.materialRoundButton1.Location = new System.Drawing.Point(124, 67);
            this.materialRoundButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialRoundButton1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialRoundButton1.Name = "materialRoundButton1";
            this.materialRoundButton1.Primary = false;
            this.materialRoundButton1.Size = new System.Drawing.Size(75, 23);
            this.materialRoundButton1.TabIndex = 0;
            this.materialRoundButton1.Text = "materialRoundButton1";
            this.materialRoundButton1.UseVisualStyleBackColor = true;
            this.materialRoundButton1.Warning = false;
            // 
            // materialRoundButton2
            // 
            this.materialRoundButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRoundButton2.Depth = 0;
            this.materialRoundButton2.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.materialRoundButton2.Icon = null;
            this.materialRoundButton2.Location = new System.Drawing.Point(139, 143);
            this.materialRoundButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialRoundButton2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialRoundButton2.Name = "materialRoundButton2";
            this.materialRoundButton2.Primary = false;
            this.materialRoundButton2.Size = new System.Drawing.Size(75, 23);
            this.materialRoundButton2.TabIndex = 1;
            this.materialRoundButton2.Text = "materialRoundButton2";
            this.materialRoundButton2.UseVisualStyleBackColor = true;
            this.materialRoundButton2.Warning = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.materialRoundButton2);
            this.Controls.Add(this.materialRoundButton1);
            this.Name = "TestForm";
            this.Text = "···";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialRoundButton materialRoundButton1;
        private MaterialRoundButton materialRoundButton2;
    }
}