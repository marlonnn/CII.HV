using System;
using Manina.Windows.Forms;
using CII.LAR.MaterialSkin;

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
            this.toolStrip = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolStripButtonDelete = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripButtonRestore = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripButtonCopy = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripButtonDetail = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripButtonPrint = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelSelectPatient = new CII.LAR.MaterialSkin.MaterialToolStripLabel();
            this.toolStripTextBoxSelect = new CII.LAR.MaterialSkin.MaterialToolStripTextBox();
            this.toolStripButtonSearch = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListView
            // 
            resources.ApplyResources(this.imageListView, "imageListView");
            this.imageListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(223)))), ((int)(((byte)(238)))));
            this.imageListView.DefaultImage = global::CII.LAR.Properties.Resources.video;
            this.imageListView.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageListView.ErrorImage")));
            this.imageListView.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.imageListView.Name = "imageListView";
            this.imageListView.ThumbnailSize = new System.Drawing.Size(120, 200);
            this.imageListView.ItemClick += new Manina.Windows.Forms.ItemClickEventHandler(this.imageListView_ItemClick);
            this.imageListView.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView_ItemDoubleClick);
            // 
            // listView
            // 
            resources.ApplyResources(this.listView, "listView");
            this.listView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(116)))), ((int)(((byte)(131)))));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
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
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Depth = 0;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDelete,
            this.toolStripButtonRestore,
            this.toolStripButtonCopy,
            this.toolStripButtonDetail,
            this.toolStripButtonPrint,
            this.toolStripSeparator1,
            this.toolStripLabelSelectPatient,
            this.toolStripTextBoxSelect,
            this.toolStripButtonSearch});
            this.toolStrip.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripButtonDelete
            // 
            resources.ApplyResources(this.toolStripButtonDelete, "toolStripButtonDelete");
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::CII.LAR.Properties.Resources.delete;
            this.toolStripButtonDelete.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonRestore
            // 
            resources.ApplyResources(this.toolStripButtonRestore, "toolStripButtonRestore");
            this.toolStripButtonRestore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRestore.Image = global::CII.LAR.Properties.Resources.undo;
            this.toolStripButtonRestore.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolStripButtonRestore.Name = "toolStripButtonRestore";
            this.toolStripButtonRestore.Click += new System.EventHandler(this.toolStripButtonRestore_Click);
            // 
            // toolStripButtonCopy
            // 
            resources.ApplyResources(this.toolStripButtonCopy, "toolStripButtonCopy");
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopy.Image = global::CII.LAR.Properties.Resources.copy;
            this.toolStripButtonCopy.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripButtonDetail
            // 
            resources.ApplyResources(this.toolStripButtonDetail, "toolStripButtonDetail");
            this.toolStripButtonDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDetail.Image = global::CII.LAR.Properties.Resources.detail;
            this.toolStripButtonDetail.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolStripButtonDetail.Name = "toolStripButtonDetail";
            this.toolStripButtonDetail.Click += new System.EventHandler(this.toolStripButtonDetail_Click);
            // 
            // toolStripButtonPrint
            // 
            resources.ApplyResources(this.toolStripButtonPrint, "toolStripButtonPrint");
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::CII.LAR.Properties.Resources.print;
            this.toolStripButtonPrint.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
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
            this.toolStripLabelSelectPatient.Depth = 0;
            resources.ApplyResources(this.toolStripLabelSelectPatient, "toolStripLabelSelectPatient");
            this.toolStripLabelSelectPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.toolStripLabelSelectPatient.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.toolStripLabelSelectPatient.Name = "toolStripLabelSelectPatient";
            // 
            // toolStripTextBoxSelect
            // 
            this.toolStripTextBoxSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.toolStripTextBoxSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBoxSelect.CustomAutoSize = true;
            this.toolStripTextBoxSelect.Depth = 0;
            this.toolStripTextBoxSelect.EmptyTextTip = null;
            this.toolStripTextBoxSelect.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.toolStripTextBoxSelect, "toolStripTextBoxSelect");
            this.toolStripTextBoxSelect.ForeColor = System.Drawing.Color.White;
            this.toolStripTextBoxSelect.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.toolStripTextBoxSelect.Name = "toolStripTextBoxSelect";
            this.toolStripTextBoxSelect.Radius = 3;
            // 
            // toolStripButtonSearch
            // 
            resources.ApplyResources(this.toolStripButtonSearch, "toolStripButtonSearch");
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearch.Image = global::CII.LAR.Properties.Resources.search;
            this.toolStripButtonSearch.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
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

        }

        #endregion

        private Manina.Windows.Forms.ImageListView imageListView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialToolStrip toolStrip;
        private MaterialToolStripButton toolStripButtonDelete;
        private MaterialToolStripButton toolStripButtonRestore;
        private MaterialToolStripButton toolStripButtonCopy;
        private MaterialToolStripButton toolStripButtonPrint;
        private MaterialToolStripButton toolStripButtonDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MaterialToolStripLabel toolStripLabelSelectPatient;
        private MaterialToolStripTextBox toolStripTextBoxSelect;
        private MaterialToolStripButton toolStripButtonSearch;
    }
}