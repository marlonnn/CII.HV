﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.MaterialSkin
{
    public class MaterialSlider : ColorSlider, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public MaterialSlider()
        {

            this.ElapsedInnerColor = SkinManager.SliderBarColor;
            this.ElapsedOuterColor = SkinManager.SliderBarColor;
            this.BarInnerColor = SkinManager.SliderBarColor;
            this.BarOuterColor = SkinManager.SliderBarColor;
            this.ThumbInnerColor = SkinManager.ThumbColor;
            this.ThumbOuterColor = SkinManager.ThumbColor;
            this.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.Size = new Size(150, 15);
            this.ThumbSize = 6;
            this.Invalidate();
        }
    }
}
