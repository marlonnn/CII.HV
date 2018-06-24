using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    partial class ReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.toolStrip = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonZoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmi500 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi150 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tslPages = new System.Windows.Forms.ToolStripLabel();
            this.reportLayout = new CII.LAR.UI.ReportLayout(this.components);
            this.pnlSpace = new System.Windows.Forms.Panel();
            this.toolStrip.SuspendLayout();
            this.reportLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Depth = 0;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrint,
            this.toolStripButtonPreview,
            this.toolStripSeparator1,
            this.toolStripDropDownButtonZoom,
            this.tslPages});
            this.toolStrip.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripButtonPrint
            // 
            resources.ApplyResources(this.toolStripButtonPrint, "toolStripButtonPrint");
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::CII.LAR.Properties.Resources.print;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripButtonPreview
            // 
            resources.ApplyResources(this.toolStripButtonPreview, "toolStripButtonPreview");
            this.toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreview.Image = global::CII.LAR.Properties.Resources.print_preview;
            this.toolStripButtonPreview.Name = "toolStripButtonPreview";
            this.toolStripButtonPreview.Click += new System.EventHandler(this.toolStripButtonPreview_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripDropDownButtonZoom
            // 
            resources.ApplyResources(this.toolStripDropDownButtonZoom, "toolStripDropDownButtonZoom");
            this.toolStripDropDownButtonZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi500,
            this.tsmi200,
            this.tsmi150,
            this.tsmi100});
            this.toolStripDropDownButtonZoom.Image = global::CII.LAR.Properties.Resources.zoom;
            this.toolStripDropDownButtonZoom.Name = "toolStripDropDownButtonZoom";
            // 
            // tsmi500
            // 
            this.tsmi500.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.tsmi500.Name = "tsmi500";
            resources.ApplyResources(this.tsmi500, "tsmi500");
            this.tsmi500.Tag = "500";
            this.tsmi500.Click += new System.EventHandler(this.tssbZoom_Click);
            // 
            // tsmi200
            // 
            this.tsmi200.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.tsmi200.Name = "tsmi200";
            resources.ApplyResources(this.tsmi200, "tsmi200");
            this.tsmi200.Tag = "200";
            this.tsmi200.Click += new System.EventHandler(this.tssbZoom_Click);
            // 
            // tsmi150
            // 
            this.tsmi150.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.tsmi150.Name = "tsmi150";
            resources.ApplyResources(this.tsmi150, "tsmi150");
            this.tsmi150.Tag = "150";
            this.tsmi150.Click += new System.EventHandler(this.tssbZoom_Click);
            // 
            // tsmi100
            // 
            this.tsmi100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.tsmi100.Name = "tsmi100";
            resources.ApplyResources(this.tsmi100, "tsmi100");
            this.tsmi100.Tag = "100";
            this.tsmi100.Click += new System.EventHandler(this.tssbZoom_Click);
            // 
            // tslPages
            // 
            this.tslPages.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslPages.Name = "tslPages";
            resources.ApplyResources(this.tslPages, "tslPages");
            // 
            // reportLayout
            // 
            resources.ApplyResources(this.reportLayout, "reportLayout");
            this.reportLayout.CanvasColor = System.Drawing.Color.LightSlateGray;
            this.reportLayout.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.reportLayout.Controls.Add(this.pnlSpace);
            this.reportLayout.Name = "reportLayout";
            this.reportLayout.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.reportLayout.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.reportLayout.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.reportLayout.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.reportLayout.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.reportLayout.Style.GradientAngle = 90;
            // 
            // pnlSpace
            // 
            resources.ApplyResources(this.pnlSpace, "pnlSpace");
            this.pnlSpace.Name = "pnlSpace";
            // 
            // ReportForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportLayout);
            this.Controls.Add(this.toolStrip);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "ReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.reportLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonZoom;
        private System.Windows.Forms.ToolStripMenuItem tsmi500;
        private System.Windows.Forms.ToolStripMenuItem tsmi200;
        private System.Windows.Forms.ToolStripMenuItem tsmi150;
        private System.Windows.Forms.ToolStripMenuItem tsmi100;
        private ReportLayout reportLayout;
        private System.Windows.Forms.Panel pnlSpace;
        private System.Windows.Forms.ToolStripLabel tslPages;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreview;
    }
}

