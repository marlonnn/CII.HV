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
            this.panel.SuspendLayout();
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
            // StatisticsCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.Controls.Add(this.btnAppearance);
            this.Controls.Add(this.panel);
            this.Name = "StatisticsCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrStatisticsTitle;
            this.Controls.SetChildIndex(this.panel, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.btnAppearance, 0);
            this.panel.ResumeLayout(false);
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
    }
}

