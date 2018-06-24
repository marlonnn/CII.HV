using System;
using Manina.Windows.Forms;
using CII.LAR.MaterialSkin;
using System.Windows.Forms;

namespace CII.LAR
{
    partial class FilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilesForm));
            this.toolStrip = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolStripButtonDelete = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripButtonAssign = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripButtonCopy = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripButtonPrint = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.tsbAssigned = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolStripLabelSelectByID = new CII.LAR.MaterialSkin.MaterialToolStripLabel();
            this.toolStripTextBox1 = new CII.LAR.MaterialSkin.MaterialToolStripTextBox();
            this.toolStripButtonSearch = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.imageListView = new Manina.Windows.Forms.ImageListView();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(42)))));
            this.toolStrip.Depth = 0;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDelete,
            this.toolStripButtonAssign,
            this.toolStripButtonCopy,
            this.toolStripButtonPrint,
            this.tsbAssigned,
            this.toolStripLabelSelectByID,
            this.toolStripTextBox1,
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
            // toolStripButtonAssign
            // 
            resources.ApplyResources(this.toolStripButtonAssign, "toolStripButtonAssign");
            this.toolStripButtonAssign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAssign.Image = global::CII.LAR.Properties.Resources.assign;
            this.toolStripButtonAssign.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolStripButtonAssign.Name = "toolStripButtonAssign";
            this.toolStripButtonAssign.Click += new System.EventHandler(this.toolStripButtonAssign_Click);
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
            // toolStripButtonPrint
            // 
            resources.ApplyResources(this.toolStripButtonPrint, "toolStripButtonPrint");
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::CII.LAR.Properties.Resources.print;
            this.toolStripButtonPrint.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // tsbAssigned
            // 
            resources.ApplyResources(this.tsbAssigned, "tsbAssigned");
            this.tsbAssigned.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAssigned.Image = global::CII.LAR.Properties.Resources.assigned;
            this.tsbAssigned.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.tsbAssigned.Name = "tsbAssigned";
            this.tsbAssigned.Click += new System.EventHandler(this.tsbAssigned_Click);
            // 
            // toolStripLabelSelectByID
            // 
            resources.ApplyResources(this.toolStripLabelSelectByID, "toolStripLabelSelectByID");
            this.toolStripLabelSelectByID.Depth = 0;
            this.toolStripLabelSelectByID.ForeColor = System.Drawing.Color.White;
            this.toolStripLabelSelectByID.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.toolStripLabelSelectByID.Name = "toolStripLabelSelectByID";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.CustomAutoSize = true;
            this.toolStripTextBox1.Depth = 0;
            this.toolStripTextBox1.EmptyTextTip = null;
            this.toolStripTextBox1.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.toolStripTextBox1, "toolStripTextBox1");
            this.toolStripTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.toolStripTextBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Radius = 3;
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
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
            // timerStatus
            // 
            this.timerStatus.Interval = 2000;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // imageListView
            // 
            resources.ApplyResources(this.imageListView, "imageListView");
            this.imageListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(223)))), ((int)(((byte)(238)))));
            this.imageListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imageListView.DefaultImage = global::CII.LAR.Properties.Resources.video;
            this.imageListView.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageListView.ErrorImage")));
            this.imageListView.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.imageListView.Name = "imageListView";
            this.imageListView.ThumbnailSize = new System.Drawing.Size(120, 200);
            this.imageListView.View = Manina.Windows.Forms.View.Pane;
            this.imageListView.ItemDoubleClick += imageListView_ItemDoubleClick;
            // 
            // FilesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageListView);
            this.Controls.Add(this.toolStrip);
            this.MinimizeBox = false;
            this.Name = "FilesForm";
            this.ShowIcon = false;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialToolStrip toolStrip;
        private MaterialToolStripButton toolStripButtonDelete;
        private MaterialToolStripButton toolStripButtonAssign;
        private MaterialToolStripButton toolStripButtonCopy;
        private MaterialToolStripButton toolStripButtonPrint;
        private MaterialToolStripLabel toolStripLabelSelectByID;
        private System.Windows.Forms.Timer timerStatus;
        private MaterialToolStripTextBox toolStripTextBox1;
        private MaterialToolStripButton tsbAssigned;
        private MaterialToolStripButton toolStripButtonSearch;
        private ImageListView imageListView;
    }
}

