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
            this.materialComboBox1 = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.SuspendLayout();
            // 
            // materialComboBox1
            // 
            this.materialComboBox1.Depth = 0;
            this.materialComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.materialComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialComboBox1.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.materialComboBox1.ForeColor = System.Drawing.Color.White;
            this.materialComboBox1.FormattingEnabled = true;
            this.materialComboBox1.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.materialComboBox1.ItemHeight = 18;
            this.materialComboBox1.Items.AddRange(new object[] {
            "28.8x",
            "38.8x",
            "48.8x",
            "58.8x"});
            this.materialComboBox1.Location = new System.Drawing.Point(48, 61);
            this.materialComboBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialComboBox1.Name = "materialComboBox1";
            this.materialComboBox1.Size = new System.Drawing.Size(112, 24);
            this.materialComboBox1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.materialComboBox1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.MaterialComboBox materialComboBox1;
    }
}