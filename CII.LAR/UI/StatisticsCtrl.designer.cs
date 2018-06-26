using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    partial class StatisticsCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsCtrl));
            this.panel = new System.Windows.Forms.Panel();
            this.listViewEx = new CII.LAR.UI.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAppearance = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.materialGroupBox1 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.lblMinArea = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel3 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblMinCir = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblCircumference = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialGroupBox2 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.lblMaxArea = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel5 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblMaxCir = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel7 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialGroupBox3 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.lblAveArea = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel9 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblAveCir = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel11 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.panel.SuspendLayout();
            this.materialGroupBox1.SuspendLayout();
            this.materialGroupBox2.SuspendLayout();
            this.materialGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // panel
            // 
            this.panel.Controls.Add(this.listViewEx);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // listViewEx
            // 
            this.listViewEx.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            resources.ApplyResources(this.listViewEx, "listViewEx");
            this.listViewEx.Name = "listViewEx";
            this.listViewEx.UseCompatibleStateImageBehavior = false;
            this.listViewEx.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // btnAppearance
            // 
            this.btnAppearance.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnAppearance, "btnAppearance");
            this.btnAppearance.Depth = 0;
            this.btnAppearance.Icon = null;
            this.btnAppearance.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnAppearance.Name = "btnAppearance";
            this.btnAppearance.Primary = false;
            this.btnAppearance.Click += new System.EventHandler(this.btnAppearance_Click);
            // 
            // materialGroupBox1
            // 
            this.materialGroupBox1.Controls.Add(this.lblMinArea);
            this.materialGroupBox1.Controls.Add(this.materialLabel3);
            this.materialGroupBox1.Controls.Add(this.lblMinCir);
            this.materialGroupBox1.Controls.Add(this.lblCircumference);
            this.materialGroupBox1.Depth = 0;
            resources.ApplyResources(this.materialGroupBox1, "materialGroupBox1");
            this.materialGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox1.Name = "materialGroupBox1";
            this.materialGroupBox1.TabStop = false;
            // 
            // lblMinArea
            // 
            resources.ApplyResources(this.lblMinArea, "lblMinArea");
            this.lblMinArea.Depth = 0;
            this.lblMinArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblMinArea.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblMinArea.Name = "lblMinArea";
            // 
            // materialLabel3
            // 
            resources.ApplyResources(this.materialLabel3, "materialLabel3");
            this.materialLabel3.Depth = 0;
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            // 
            // lblMinCir
            // 
            resources.ApplyResources(this.lblMinCir, "lblMinCir");
            this.lblMinCir.Depth = 0;
            this.lblMinCir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblMinCir.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblMinCir.Name = "lblMinCir";
            // 
            // lblCircumference
            // 
            resources.ApplyResources(this.lblCircumference, "lblCircumference");
            this.lblCircumference.Depth = 0;
            this.lblCircumference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblCircumference.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblCircumference.Name = "lblCircumference";
            // 
            // materialGroupBox2
            // 
            this.materialGroupBox2.Controls.Add(this.lblMaxArea);
            this.materialGroupBox2.Controls.Add(this.materialLabel5);
            this.materialGroupBox2.Controls.Add(this.lblMaxCir);
            this.materialGroupBox2.Controls.Add(this.materialLabel7);
            this.materialGroupBox2.Depth = 0;
            resources.ApplyResources(this.materialGroupBox2, "materialGroupBox2");
            this.materialGroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox2.Name = "materialGroupBox2";
            this.materialGroupBox2.TabStop = false;
            // 
            // lblMaxArea
            // 
            resources.ApplyResources(this.lblMaxArea, "lblMaxArea");
            this.lblMaxArea.Depth = 0;
            this.lblMaxArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblMaxArea.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblMaxArea.Name = "lblMaxArea";
            // 
            // materialLabel5
            // 
            resources.ApplyResources(this.materialLabel5, "materialLabel5");
            this.materialLabel5.Depth = 0;
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel5.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            // 
            // lblMaxCir
            // 
            resources.ApplyResources(this.lblMaxCir, "lblMaxCir");
            this.lblMaxCir.Depth = 0;
            this.lblMaxCir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblMaxCir.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblMaxCir.Name = "lblMaxCir";
            // 
            // materialLabel7
            // 
            resources.ApplyResources(this.materialLabel7, "materialLabel7");
            this.materialLabel7.Depth = 0;
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel7.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            // 
            // materialGroupBox3
            // 
            this.materialGroupBox3.Controls.Add(this.lblAveArea);
            this.materialGroupBox3.Controls.Add(this.materialLabel9);
            this.materialGroupBox3.Controls.Add(this.lblAveCir);
            this.materialGroupBox3.Controls.Add(this.materialLabel11);
            this.materialGroupBox3.Depth = 0;
            resources.ApplyResources(this.materialGroupBox3, "materialGroupBox3");
            this.materialGroupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox3.Name = "materialGroupBox3";
            this.materialGroupBox3.TabStop = false;
            // 
            // lblAveArea
            // 
            resources.ApplyResources(this.lblAveArea, "lblAveArea");
            this.lblAveArea.Depth = 0;
            this.lblAveArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblAveArea.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblAveArea.Name = "lblAveArea";
            // 
            // materialLabel9
            // 
            resources.ApplyResources(this.materialLabel9, "materialLabel9");
            this.materialLabel9.Depth = 0;
            this.materialLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel9.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel9.Name = "materialLabel9";
            // 
            // lblAveCir
            // 
            resources.ApplyResources(this.lblAveCir, "lblAveCir");
            this.lblAveCir.Depth = 0;
            this.lblAveCir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblAveCir.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblAveCir.Name = "lblAveCir";
            // 
            // materialLabel11
            // 
            resources.ApplyResources(this.materialLabel11, "materialLabel11");
            this.materialLabel11.Depth = 0;
            this.materialLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel11.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel11.Name = "materialLabel11";
            // 
            // StatisticsCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.Controls.Add(this.materialGroupBox3);
            this.Controls.Add(this.materialGroupBox2);
            this.Controls.Add(this.materialGroupBox1);
            this.Controls.Add(this.btnAppearance);
            this.Controls.Add(this.panel);
            this.Name = "StatisticsCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrStatisticsTitle;
            this.Controls.SetChildIndex(this.panel, 0);
            this.Controls.SetChildIndex(this.btnAppearance, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.materialGroupBox1, 0);
            this.Controls.SetChildIndex(this.materialGroupBox2, 0);
            this.Controls.SetChildIndex(this.materialGroupBox3, 0);
            this.panel.ResumeLayout(false);
            this.materialGroupBox1.ResumeLayout(false);
            this.materialGroupBox1.PerformLayout();
            this.materialGroupBox2.ResumeLayout(false);
            this.materialGroupBox2.PerformLayout();
            this.materialGroupBox3.ResumeLayout(false);
            this.materialGroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private ListViewEx listViewEx;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private MaterialRoundButton btnAppearance;
        private MaterialGroupBox materialGroupBox1;
        private MaterialLabel lblCircumference;
        private MaterialLabel lblMinArea;
        private MaterialLabel materialLabel3;
        private MaterialLabel lblMinCir;
        private MaterialGroupBox materialGroupBox2;
        private MaterialLabel lblMaxArea;
        private MaterialLabel materialLabel5;
        private MaterialLabel lblMaxCir;
        private MaterialLabel materialLabel7;
        private MaterialGroupBox materialGroupBox3;
        private MaterialLabel lblAveArea;
        private MaterialLabel materialLabel9;
        private MaterialLabel lblAveCir;
        private MaterialLabel materialLabel11;
    }
}

