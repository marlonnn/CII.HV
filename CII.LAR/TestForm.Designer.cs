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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.materialTextBox1 = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(92, 175);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(129, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "21222";
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.materialTextBox1.CustomAutoSize = true;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.EmptyTextTip = null;
            this.materialTextBox1.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.materialTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialTextBox1.ForeColor = System.Drawing.Color.White;
            this.materialTextBox1.Location = new System.Drawing.Point(92, 125);
            this.materialTextBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.Radius = 3;
            this.materialTextBox1.Size = new System.Drawing.Size(100, 21);
            this.materialTextBox1.TabIndex = 2;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.materialTextBox1);
            this.Controls.Add(this.textBox1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private MaterialSkin.MaterialTextBox materialTextBox1;
    }
}