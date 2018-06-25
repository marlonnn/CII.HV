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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.materialToolStrip1 = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.materialToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialToolStrip1
            // 
            this.materialToolStrip1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.materialToolStrip1.AutoSize = false;
            this.materialToolStrip1.Depth = 0;
            this.materialToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.materialToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.materialToolStrip1.Location = new System.Drawing.Point(9, 40);
            this.materialToolStrip1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip1.Name = "materialToolStrip1";
            this.materialToolStrip1.Size = new System.Drawing.Size(111, 47);
            this.materialToolStrip1.TabIndex = 0;
            this.materialToolStrip1.Text = "materialToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(35, 35);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(35, 35);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.materialToolStrip1);
            this.Name = "TestForm";
            this.Text = "···";
            this.materialToolStrip1.ResumeLayout(false);
            this.materialToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialToolStrip materialToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}