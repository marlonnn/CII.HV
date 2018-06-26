using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    partial class LaserHoleSize
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserHoleSize));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnLaserCtrl = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.lblAdjustPulse = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblAdjustHole = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderPulse = new CII.LAR.UI.SliderCtrl();
            this.btnFire = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnSave = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnDelete = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.holeSizeCtrl = new CII.LAR.UI.HoleSizeCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // chart1
            // 
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            resources.ApplyResources(this.chart1, "chart1");
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.MarkerColor = System.Drawing.Color.DarkGreen;
            series1.MarkerSize = 10;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // btnLaserCtrl
            // 
            this.btnLaserCtrl.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnLaserCtrl, "btnLaserCtrl");
            this.btnLaserCtrl.Depth = 0;
            this.btnLaserCtrl.Icon = null;
            this.btnLaserCtrl.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnLaserCtrl.Name = "btnLaserCtrl";
            this.btnLaserCtrl.Primary = false;
            this.btnLaserCtrl.Click += new System.EventHandler(this.btnLaserCtrl_Click);
            // 
            // lblAdjustPulse
            // 
            this.lblAdjustPulse.Depth = 0;
            resources.ApplyResources(this.lblAdjustPulse, "lblAdjustPulse");
            this.lblAdjustPulse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblAdjustPulse.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblAdjustPulse.Name = "lblAdjustPulse";
            // 
            // lblAdjustHole
            // 
            this.lblAdjustHole.Depth = 0;
            resources.ApplyResources(this.lblAdjustHole, "lblAdjustHole");
            this.lblAdjustHole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblAdjustHole.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblAdjustHole.Name = "lblAdjustHole";
            // 
            // sliderPulse
            // 
            resources.ApplyResources(this.sliderPulse, "sliderPulse");
            this.sliderPulse.Name = "sliderPulse";
            this.sliderPulse.Update = true;
            // 
            // btnFire
            // 
            this.btnFire.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnFire, "btnFire");
            this.btnFire.Depth = 0;
            this.btnFire.Icon = null;
            this.btnFire.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnFire.Name = "btnFire";
            this.btnFire.Primary = false;
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Depth = 0;
            this.btnSave.Icon = null;
            this.btnSave.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.Primary = false;
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Depth = 0;
            this.btnDelete.Icon = null;
            this.btnDelete.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Primary = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // holeSizeCtrl
            // 
            this.holeSizeCtrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.holeSizeCtrl.HoleSize = 0D;
            resources.ApplyResources(this.holeSizeCtrl, "holeSizeCtrl");
            this.holeSizeCtrl.Name = "holeSizeCtrl";
            // 
            // LaserHoleSize
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.holeSizeCtrl);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFire);
            this.Controls.Add(this.sliderPulse);
            this.Controls.Add(this.lblAdjustHole);
            this.Controls.Add(this.lblAdjustPulse);
            this.Controls.Add(this.btnLaserCtrl);
            this.Controls.Add(this.chart1);
            this.Name = "LaserHoleSize";
            this.Title = global::CII.LAR.Properties.Resources.StrLaserHoleSizeCalibration;
            this.Controls.SetChildIndex(this.chart1, 0);
            this.Controls.SetChildIndex(this.btnLaserCtrl, 0);
            this.Controls.SetChildIndex(this.lblAdjustPulse, 0);
            this.Controls.SetChildIndex(this.lblAdjustHole, 0);
            this.Controls.SetChildIndex(this.sliderPulse, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.btnFire, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.holeSizeCtrl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private MaterialRoundButton btnLaserCtrl;
        private MaterialLabel lblAdjustPulse;
        private MaterialLabel lblAdjustHole;
        private SliderCtrl sliderPulse;
        private MaterialRoundButton btnFire;
        private MaterialRoundButton btnSave;
        private MaterialRoundButton btnDelete;
        private HoleSizeCtrl holeSizeCtrl;
    }
}
