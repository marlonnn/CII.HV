namespace CII.LAR.MaterialSkin
{
    partial class MaterialSliderControl
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
            this.lblValue = new CII.LAR.MaterialSkin.MaterialLabel();
            this.btnAdd = new CII.LAR.MaterialSkin.RoundButton();
            this.btnSub = new CII.LAR.MaterialSkin.RoundButton();
            this.slider = new CII.LAR.MaterialSkin.MaterialSlider();
            this.SuspendLayout();
            // 
            // lblValue
            // 
            this.lblValue.Depth = 0;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblValue.Location = new System.Drawing.Point(218, 6);
            this.lblValue.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(100, 23);
            this.lblValue.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.ColorGradient = ((byte)(2));
            this.btnAdd.ColorStepGradient = ((byte)(2));
            this.btnAdd.FadeOut = false;
            this.btnAdd.HoverColor = System.Drawing.SystemColors.ControlDark;
            this.btnAdd.Image = global::CII.LAR.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(187, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 25);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.TextStartPoint = new System.Drawing.Point(0, 0);
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAdd_MouseDown);
            this.btnAdd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAdd_MouseUp);
            // 
            // btnSub
            // 
            this.btnSub.ColorGradient = ((byte)(2));
            this.btnSub.ColorStepGradient = ((byte)(2));
            this.btnSub.FadeOut = false;
            this.btnSub.HoverColor = System.Drawing.SystemColors.ControlDark;
            this.btnSub.Image = global::CII.LAR.Properties.Resources.sub;
            this.btnSub.Location = new System.Drawing.Point(3, 3);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(25, 25);
            this.btnSub.TabIndex = 1;
            this.btnSub.TextStartPoint = new System.Drawing.Point(0, 0);
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            this.btnSub.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSub_MouseDown);
            this.btnSub.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSub_MouseUp);
            // 
            // slider
            // 
            this.slider.BackColor = System.Drawing.Color.Transparent;
            this.slider.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.slider.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.slider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.slider.Depth = 0;
            this.slider.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.slider.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.slider.LargeChange = ((uint)(5u));
            this.slider.Location = new System.Drawing.Point(31, 7);
            this.slider.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(150, 15);
            this.slider.SmallChange = ((uint)(1u));
            this.slider.TabIndex = 0;
            this.slider.Text = "materialSlider1";
            this.slider.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.slider.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.slider.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.slider.ThumbSize = 10;
            this.slider.ValueChanged += new System.EventHandler(this.slider_ValueChanged);
            // 
            // MaterialSliderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.slider);
            this.Name = "MaterialSliderControl";
            this.Size = new System.Drawing.Size(323, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSlider slider;
        private RoundButton btnSub;
        private RoundButton btnAdd;
        private MaterialLabel lblValue;
    }
}
