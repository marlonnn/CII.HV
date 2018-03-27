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
            this.btnLaserCtrl = new System.Windows.Forms.Button();
            this.lblAdjustPulse = new DevComponents.DotNetBar.LabelX();
            this.lblAdjustHole = new DevComponents.DotNetBar.LabelX();
            this.sliderPulse = new CII.LAR.UI.SliderCtrl();
            this.btnFire = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
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
            series1.MarkerSize = 8;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            // 
            // btnLaserCtrl
            // 
            this.btnLaserCtrl.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnLaserCtrl, "btnLaserCtrl");
            this.btnLaserCtrl.Name = "btnLaserCtrl";
            this.btnLaserCtrl.Click += new System.EventHandler(this.btnLaserCtrl_Click);
            // 
            // lblAdjustPulse
            // 
            // 
            // 
            // 
            this.lblAdjustPulse.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblAdjustPulse, "lblAdjustPulse");
            this.lblAdjustPulse.Name = "lblAdjustPulse";
            // 
            // lblAdjustHole
            // 
            // 
            // 
            // 
            this.lblAdjustHole.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblAdjustHole, "lblAdjustHole");
            this.lblAdjustHole.Name = "lblAdjustHole";
            // 
            // sliderPulse
            // 
            resources.ApplyResources(this.sliderPulse, "sliderPulse");
            this.sliderPulse.Name = "sliderPulse";
            // 
            // btnFire
            // 
            this.btnFire.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnFire, "btnFire");
            this.btnFire.Name = "btnFire";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // holeSizeCtrl
            // 
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
        private System.Windows.Forms.Button btnLaserCtrl;
        private DevComponents.DotNetBar.LabelX lblAdjustPulse;
        private DevComponents.DotNetBar.LabelX lblAdjustHole;
        private SliderCtrl sliderPulse;
        private System.Windows.Forms.Button btnFire;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private HoleSizeCtrl holeSizeCtrl;
    }
}
