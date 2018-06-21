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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.materialFlatButton2 = new CII.LAR.MaterialSkin.MaterialFlatButton();
            this.materialTextBox1 = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.materialComboBox1 = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 201);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 21);
            this.textBox1.TabIndex = 3;
            // 
            // materialFlatButton2
            // 
            this.materialFlatButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton2.Depth = 0;
            this.materialFlatButton2.Icon = null;
            this.materialFlatButton2.Location = new System.Drawing.Point(190, 153);
            this.materialFlatButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialFlatButton2.Name = "materialFlatButton2";
            this.materialFlatButton2.Primary = false;
            this.materialFlatButton2.Size = new System.Drawing.Size(38, 19);
            this.materialFlatButton2.TabIndex = 4;
            this.materialFlatButton2.Text = "···";
            this.materialFlatButton2.UseVisualStyleBackColor = true;
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.materialTextBox1.CustomAutoSize = true;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.EmptyTextTip = null;
            this.materialTextBox1.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.materialTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialTextBox1.ForeColor = System.Drawing.Color.White;
            this.materialTextBox1.Location = new System.Drawing.Point(48, 152);
            this.materialTextBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.Radius = 3;
            this.materialTextBox1.Size = new System.Drawing.Size(181, 21);
            this.materialTextBox1.TabIndex = 2;
            // 
            // materialComboBox1
            // 
            this.materialComboBox1.Depth = 0;
            this.materialComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.materialComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialComboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialComboBox1.ForeColor = System.Drawing.Color.White;
            this.materialComboBox1.FormattingEnabled = true;
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
            this.Controls.Add(this.materialFlatButton2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.materialTextBox1);
            this.Controls.Add(this.materialComboBox1);
            this.Name = "TestForm";
            this.Text = "···";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.MaterialComboBox materialComboBox1;
        private MaterialSkin.MaterialTextBox materialTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private MaterialSkin.MaterialFlatButton materialFlatButton2;
    }
}