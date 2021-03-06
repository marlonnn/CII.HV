﻿using CII.LAR.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialSkinManager
    {
        //Singleton instance
        private static MaterialSkinManager _instance;

        //Forms to control
        private readonly List<MaterialForm> _formsToManage = new List<MaterialForm>();

        //Theme
        private Themes _theme;
        public Themes Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                UpdateBackgrounds();
            }
        }

        private ColorScheme _colorScheme;
        public ColorScheme ColorScheme
        {
            get { return _colorScheme; }
            set
            {
                _colorScheme = value;
                UpdateBackgrounds();
            }
        }

        public enum Themes : byte
        {
            LIGHT,
            DARK
        }

        private Color waringColor = Color.FromArgb(0xFF, 0x66, 0x00);
        public Color WaringColor
        {
            get { return this.waringColor; }
        }
        //Constant color values
        private Color fontColor = Color.FromArgb(0xDB, 0xE2, 0xF1);
        //默认字体颜色
        public Color FontColor
        {
            get { return this.fontColor; }
        }

        //默认ComboBox选中字体颜色
        private Color comboBoxItemSelectFontColor = Color.FromArgb(0x1A, 0x1E, 0x25);
        public Color ComboBoxItemSelectFontColor
        {
            get { return this.comboBoxItemSelectFontColor; }
        }

        //默认ComboBox选中颜色
        private Color comboBoxHighlightColor = Color.FromArgb(0xCA, 0xD1, 0xE0);
        public Color ComboBoxHighlightColor
        {
            get { return this.comboBoxHighlightColor; }
        }

        //默认Material slider bar颜色
        private Color sliderBarColor = Color.FromArgb(0x49, 0x50, 0x5F);
        public Color SliderBarColor
        {
            get { return this.sliderBarColor; }
        }

        //默认Material slider thumb颜色
        private Color thumbColor = Color.FromArgb(0xCA, 0xD1, 0xE0);
        public Color ThumbColor
        {
            get { return this.thumbColor; }
        }

        //默认BaseCtrl标题栏颜色
        private Color baseCtrlTitleColor = Color.FromArgb(0x1A, 0x1E, 0x25);
        public Color BaseCtrlTitleColor
        {
            get { return this.baseCtrlTitleColor; }
        }

        //默认BaseCtrl标题栏字体颜色
        private Color baseCtrlTitleTextColor = Color.FromArgb(0xAD, 0xB8, 0xD0);
        public Color BaseCtrlTitleTextColor
        {
            get { return this.baseCtrlTitleTextColor; }
        }

        //默认Material GroupBox边框颜色
        private Color groupBoxBorderColor = Color.FromArgb(0x1C, 0x1F, 0x26);
        public Color GroupBoxBorderColor
        {
            get { return this.groupBoxBorderColor; }
        }


        //默认Material Round Button边框颜色
        private Color roundButtonBorderColor = Color.FromArgb(0xCA, 0xD1, 0xE0);
        public Color RoundButtonBorderColor
        {
            get { return this.roundButtonBorderColor; }
        }
        private static readonly Color PRIMARY_TEXT_BLACK = Color.FromArgb(222, 0, 0, 0);
        private static readonly Brush PRIMARY_TEXT_BLACK_BRUSH = new SolidBrush(PRIMARY_TEXT_BLACK);
        public static Color SECONDARY_TEXT_BLACK = Color.FromArgb(138, 0, 0, 0);
        public static Brush SECONDARY_TEXT_BLACK_BRUSH = new SolidBrush(SECONDARY_TEXT_BLACK);
        private static readonly Color DISABLED_OR_HINT_TEXT_BLACK = Color.FromArgb(66, 0, 0, 0);
        private static readonly Brush DISABLED_OR_HINT_TEXT_BLACK_BRUSH = new SolidBrush(DISABLED_OR_HINT_TEXT_BLACK);
        private static readonly Color DIVIDERS_BLACK = Color.FromArgb(31, 0, 0, 0);
        private static readonly Brush DIVIDERS_BLACK_BRUSH = new SolidBrush(DIVIDERS_BLACK);

        private static readonly Color PRIMARY_TEXT_WHITE = Color.FromArgb(255, 255, 255, 255);
        private static readonly Brush PRIMARY_TEXT_WHITE_BRUSH = new SolidBrush(PRIMARY_TEXT_WHITE);
        public static Color SECONDARY_TEXT_WHITE = Color.FromArgb(179, 255, 255, 255);
        public static Brush SECONDARY_TEXT_WHITE_BRUSH = new SolidBrush(SECONDARY_TEXT_WHITE);
        private static readonly Color DISABLED_OR_HINT_TEXT_WHITE = Color.FromArgb(77, 255, 255, 255);
        private static readonly Brush DISABLED_OR_HINT_TEXT_WHITE_BRUSH = new SolidBrush(DISABLED_OR_HINT_TEXT_WHITE);
        private static readonly Color DIVIDERS_WHITE = Color.FromArgb(31, 255, 255, 255);
        private static readonly Brush DIVIDERS_WHITE_BRUSH = new SolidBrush(DIVIDERS_WHITE);

        //// Checkbox colors
        //private static readonly Color CHECKBOX_OFF_LIGHT = Color.FromArgb(138, 0, 0, 0);
        //private static readonly Brush CHECKBOX_OFF_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_LIGHT);
        //private static readonly Color CHECKBOX_OFF_DISABLED_LIGHT = Color.FromArgb(66, 0, 0, 0);
        //private static readonly Brush CHECKBOX_OFF_DISABLED_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_LIGHT);

        //private static readonly Color CHECKBOX_OFF_DARK = Color.FromArgb(179, 255, 255, 255);
        //private static readonly Brush CHECKBOX_OFF_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DARK);
        //private static readonly Color CHECKBOX_OFF_DISABLED_DARK = Color.FromArgb(77, 255, 255, 255);
        //private static readonly Brush CHECKBOX_OFF_DISABLED_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_DARK);

        ////Raised button
        //private static readonly Color RAISED_BUTTON_BACKGROUND = Color.FromArgb(255, 255, 255, 255);
        //private static readonly Brush RAISED_BUTTON_BACKGROUND_BRUSH = new SolidBrush(RAISED_BUTTON_BACKGROUND);
        //private static readonly Color RAISED_BUTTON_TEXT_LIGHT = PRIMARY_TEXT_WHITE;
        //private static readonly Brush RAISED_BUTTON_TEXT_LIGHT_BRUSH = new SolidBrush(RAISED_BUTTON_TEXT_LIGHT);
        //private static readonly Color RAISED_BUTTON_TEXT_DARK = PRIMARY_TEXT_BLACK;
        //private static readonly Brush RAISED_BUTTON_TEXT_DARK_BRUSH = new SolidBrush(RAISED_BUTTON_TEXT_DARK);

        //Flat button
        private static readonly Color FLAT_BUTTON_BACKGROUND_HOVER_LIGHT = Color.FromArgb(20.PercentageToColorComponent(), 0x999999.ToColor());
        private static readonly Brush FLAT_BUTTON_BACKGROUND_HOVER_LIGHT_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_HOVER_LIGHT);
        private static readonly Color FLAT_BUTTON_BACKGROUND_PRESSED_LIGHT = Color.FromArgb(40.PercentageToColorComponent(), 0x999999.ToColor());
        private static readonly Brush FLAT_BUTTON_BACKGROUND_PRESSED_LIGHT_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_PRESSED_LIGHT);
        private static readonly Color FLAT_BUTTON_DISABLEDTEXT_LIGHT = Color.FromArgb(26.PercentageToColorComponent(), 0x000000.ToColor());
        private static readonly Brush FLAT_BUTTON_DISABLEDTEXT_LIGHT_BRUSH = new SolidBrush(FLAT_BUTTON_DISABLEDTEXT_LIGHT);

        private static readonly Color FLAT_BUTTON_BACKGROUND_HOVER_DARK = Color.FromArgb(0x24,0x28, 0x30);
        //private static readonly Color FLAT_BUTTON_BACKGROUND_HOVER_DARK = Color.FromArgb(15.PercentageToColorComponent(), 0xCCCCCC.ToColor());
        private static readonly Brush FLAT_BUTTON_BACKGROUND_HOVER_DARK_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_HOVER_DARK);
        private static readonly Color FLAT_BUTTON_BACKGROUND_PRESSED_DARK = Color.FromArgb(25.PercentageToColorComponent(), 0xCCCCCC.ToColor());
        private static readonly Brush FLAT_BUTTON_BACKGROUND_PRESSED_DARK_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_PRESSED_DARK);
        private static readonly Color FLAT_BUTTON_DISABLEDTEXT_DARK = Color.FromArgb(30.PercentageToColorComponent(), 0xFFFFFF.ToColor());
        private static readonly Brush FLAT_BUTTON_DISABLEDTEXT_DARK_BRUSH = new SolidBrush(FLAT_BUTTON_DISABLEDTEXT_DARK);

        ////ContextMenuStrip
        //private static readonly Color CMS_BACKGROUND_LIGHT_HOVER = Color.FromArgb(255, 238, 238, 238);
        //private static readonly Brush CMS_BACKGROUND_HOVER_LIGHT_BRUSH = new SolidBrush(CMS_BACKGROUND_LIGHT_HOVER);

        //private static readonly Color CMS_BACKGROUND_DARK_HOVER = Color.FromArgb(38, 204, 204, 204);
        //private static readonly Brush CMS_BACKGROUND_HOVER_DARK_BRUSH = new SolidBrush(CMS_BACKGROUND_DARK_HOVER);

        //Application background
        private static readonly Color BACKGROUND_LIGHT = Color.FromArgb(255, 255, 255, 255);
        private static Brush BACKGROUND_LIGHT_BRUSH = new SolidBrush(BACKGROUND_LIGHT);

        //1F242A
        private static readonly Color BACKGROUND_DARK = Color.FromArgb(255, 0x1F, 0x24, 0x2A);
        private static Brush BACKGROUND_DARK_BRUSH = new SolidBrush(BACKGROUND_DARK);

        //Application action bar
        public readonly Color ACTION_BAR_TEXT = Color.FromArgb(255, 255, 255, 255);
        public readonly Brush ACTION_BAR_TEXT_BRUSH = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        public readonly Color ACTION_BAR_TEXT_SECONDARY = Color.FromArgb(153, 255, 255, 255);
        public readonly Brush ACTION_BAR_TEXT_SECONDARY_BRUSH = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

        public Color GetLabelTextColor()
        {
            //ADB8D0
            return Color.FromArgb(0xAD, 0xB8, 0xD0);
        }

        public Color GetDividersColor()
        {
            return Theme == Themes.LIGHT ? DIVIDERS_BLACK : DIVIDERS_WHITE;
        }

        public Color GetFlatButtonHoverBackgroundColor()
        {
            return Theme == Themes.LIGHT ? FLAT_BUTTON_BACKGROUND_HOVER_LIGHT : FLAT_BUTTON_BACKGROUND_HOVER_DARK;
        }

        public Brush GetFlatButtonHoverBackgroundBrush()
        {
            return Theme == Themes.LIGHT ? FLAT_BUTTON_BACKGROUND_HOVER_LIGHT_BRUSH : FLAT_BUTTON_BACKGROUND_HOVER_DARK_BRUSH;
        }

        public Brush GetFlatButtonPressedBackgroundBrush()
        {
            return Theme == Themes.LIGHT ? FLAT_BUTTON_BACKGROUND_PRESSED_LIGHT_BRUSH : FLAT_BUTTON_BACKGROUND_PRESSED_DARK_BRUSH;
        }

        public Color GetApplicationBackgroundColor()
        {
            return Theme == Themes.LIGHT ? BACKGROUND_LIGHT : BACKGROUND_DARK;
        }

        //Roboto font
        public Font ROBOTO_MEDIUM_12;
        public Font ROBOTO_REGULAR_11;
        public Font ROBOTO_MEDIUM_11;
        public Font ROBOTO_MEDIUM_10;

        public Font PINGFANG_MEDIUM_9;
        public Font PINGFANG_MEDIUM_10;
        public Font PINGFANG_MEDIUM_12;
        public Font PINGFANG_MEDIUM_16;
        //Other constants
        public int FORM_PADDING = 10;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);

        private MaterialSkinManager()
        {
            ROBOTO_MEDIUM_12 = new Font(LoadFont(Resources.PingFang_Medium), 12f);
            ROBOTO_MEDIUM_10 = new Font(LoadFont(Resources.PingFang_Medium), 10f);
            ROBOTO_REGULAR_11 = new Font(LoadFont(Resources.PingFang_Medium), 11f);
            ROBOTO_MEDIUM_11 = new Font(LoadFont(Resources.PingFang_Medium), 11f);

            PINGFANG_MEDIUM_9 = new Font(LoadFont(Resources.PingFang_Medium), 9f);
            PINGFANG_MEDIUM_10 = new Font(LoadFont(Resources.PingFang_Medium), 10f);
            PINGFANG_MEDIUM_12 = new Font(LoadFont(Resources.PingFang_Medium), 12f);
            PINGFANG_MEDIUM_16 = new Font(LoadFont(Resources.PingFang_Medium), 16f);
            Theme = Themes.DARK;
            ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        public static MaterialSkinManager Instance => _instance ?? (_instance = new MaterialSkinManager());

        public void AddFormToManage(MaterialForm materialForm)
        {
            _formsToManage.Add(materialForm);
            UpdateBackgrounds();
        }

        public void RemoveFormToManage(MaterialForm materialForm)
        {
            _formsToManage.Remove(materialForm);
        }

        private readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        private FontFamily LoadFont(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);

            return privateFontCollection.Families.Last();
        }

        private void UpdateBackgrounds()
        {
            var newBackColor = GetApplicationBackgroundColor();
            foreach (var materialForm in _formsToManage)
            {
                materialForm.BackColor = newBackColor;
                UpdateControl(materialForm, newBackColor);
            }
        }

        private void UpdateToolStrip(ToolStrip toolStrip, Color newBackColor)
        {
            if (toolStrip == null) return;

            toolStrip.BackColor = newBackColor;
            //foreach (ToolStripItem control in toolStrip.Items)
            //{
            //    control.BackColor = newBackColor;
            //    if (control is MaterialToolStripMenuItem && (control as MaterialToolStripMenuItem).HasDropDown)
            //    {

            //        //recursive call
            //        UpdateToolStrip((control as MaterialToolStripMenuItem).DropDown, newBackColor);
            //    }
            //}
        }

        private void UpdateControl(Control controlToUpdate, Color newBackColor)
        {
            if (controlToUpdate == null) return;

            if (controlToUpdate.ContextMenuStrip != null)
            {
                UpdateToolStrip(controlToUpdate.ContextMenuStrip, newBackColor);
            }
            //var tabControl = controlToUpdate as MaterialTabControl;
            //if (tabControl != null)
            //{
            //    foreach (TabPage tabPage in tabControl.TabPages)
            //    {
            //        tabPage.BackColor = newBackColor;
            //    }
            //}

            //if (controlToUpdate is MaterialDivider)
            //{
            //    controlToUpdate.BackColor = GetDividersColor();
            //}

            //if (controlToUpdate is MaterialListView)
            //{
            //    controlToUpdate.BackColor = newBackColor;

            //}

            ////recursive call
            //foreach (Control control in controlToUpdate.Controls)
            //{
            //    UpdateControl(control, newBackColor);
            //}

            controlToUpdate.Invalidate();
        }
    }
}
