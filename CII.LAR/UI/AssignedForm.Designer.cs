using System;
using Manina.Windows.Forms;

namespace CII.LAR.UI
{
    partial class AssignedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignedForm));
            this.imageListView = new Manina.Windows.Forms.ImageListView();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRestore = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDetail = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelSelectPatient = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSelect = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListView
            // 
            this.imageListView.DefaultImage = ((System.Drawing.Image)(resources.GetObject("imageListView.DefaultImage")));
            resources.ApplyResources(this.imageListView, "imageListView");
            this.imageListView.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageListView.ErrorImage")));
            this.imageListView.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.imageListView.Name = "imageListView";
            this.imageListView.ThumbnailSize = new System.Drawing.Size(120, 200);
            this.imageListView.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView_ItemDoubleClick);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            resources.ApplyResources(this.listView, "listView");
            this.listView.FullRowSelect = true;
            this.listView.Name = "listView";
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDelete,
            this.toolStripButtonRestore,
            this.toolStripButtonCopy,
            this.toolStripButtonDetail,
            this.toolStripButtonPrint,
            this.toolStripSeparator1,
            this.toolStripLabelSelectPatient,
            this.toolStripTextBoxSelect});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::CII.LAR.Properties.Resources.delete;
            resources.ApplyResources(this.toolStripButtonDelete, "toolStripButtonDelete");
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonRestore
            // 
            this.toolStripButtonRestore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRestore.Image = global::CII.LAR.Properties.Resources.undo;
            resources.ApplyResources(this.toolStripButtonRestore, "toolStripButtonRestore");
            this.toolStripButtonRestore.Name = "toolStripButtonRestore";
            this.toolStripButtonRestore.Click += new System.EventHandler(this.toolStripButtonRestore_Click);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopy.Image = global::CII.LAR.Properties.Resources.copy;
            resources.ApplyResources(this.toolStripButtonCopy, "toolStripButtonCopy");
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripButtonDetail
            // 
            this.toolStripButtonDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDetail.Image = global::CII.LAR.Properties.Resources.detail;
            resources.ApplyResources(this.toolStripButtonDetail, "toolStripButtonDetail");
            this.toolStripButtonDetail.Name = "toolStripButtonDetail";
            this.toolStripButtonDetail.Click += new System.EventHandler(this.toolStripButtonDetail_Click);
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::CII.LAR.Properties.Resources.print;
            resources.ApplyResources(this.toolStripButtonPrint, "toolStripButtonPrint");
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripLabelSelectPatient
            // 
            this.toolStripLabelSelectPatient.Name = "toolStripLabelSelectPatient";
            resources.ApplyResources(this.toolStripLabelSelectPatient, "toolStripLabelSelectPatient");
            // 
            // toolStripTextBoxSelect
            // 
            this.toolStripTextBoxSelect.Name = "toolStripTextBoxSelect";
            resources.ApplyResources(this.toolStripTextBoxSelect, "toolStripTextBoxSelect");
            // 
            // AssignedForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageListView);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.MinimizeBox = false;
            this.Name = "AssignedForm";
            this.ShowIcon = false;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Manina.Windows.Forms.ImageListView imageListView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonRestore;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrint;
        private System.Windows.Forms.ToolStripButton toolStripButtonDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelectPatient;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSelect;
    }
}