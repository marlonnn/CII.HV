using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public enum PanDirection
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    /// <summary>
    /// 绘图类型
    /// </summary>
    public enum DrawToolType
    {
        None,
        Pointer,
        Line,
        Rectangle,
        Ellipse,
        Polygon,
        PolyLine,
        Circle,
        MultipleCircle,
        Move,
        NumberOfDrawTools
    }

    /// <summary>
    /// 当前系统使用的功能：
    /// 1.激光器
    /// 2.测量工具
    /// 3.无
    /// </summary>
    public enum SystemFunction
    {
        Laser,
        Measure,
        Empty
    }

    /// <summary>
    /// 绘制图形类型
    /// </summary>
    public enum ObjectType
    {
        Line,
        Rectangle,
        Ellipse,
        Polygon,
        Circle,
        Text
    }

    /// <summary>
    /// 测量单位
    /// </summary>
    public enum enUniMis
    {
        um = 0,
        dmm = 1,
        mm = 2,
        cm = 3,
        inches = 4,
        meters = 5
    }

    /// <summary>
    /// 激光器类型
    /// </summary>
    public enum LaserType
    {
        SaturnFixed,
        SaturnActive,
        Alignment
    }

    /// <summary>
    /// 活检模式下，鼠标在圆弧的起点、终点、还是中点
    /// </summary>
    public enum InHoleType
    {
        StartHole,
        CenterHole,
        EndHole,
        Empty
    }

    /// <summary>
    /// 激光器，活检模式下圆弧类型
    /// Line表示直线型连续的圆环
    /// Arc表示拖动了直线型圆环的起点、终点、中点，成圆弧形
    /// </summary>
    public enum LaserShape
    {
        Line,
        Arc
    }

    /// <summary>
    /// 所有子窗口控件类型
    /// </summary>
    public enum CtrlType
    {
        SettingCtrl,
        SerialPort,
        StatisticsCtrl,
        LaserAlignment,
        LaserAppreance,
        LaserCtrl,
        VideoChooseCtrl,
        LaserHoleSize,
        RulerAppearanceCtrl,
        DebugCtrl
    }

    /// <summary>
    /// 相机水平或者垂直翻转
    /// </summary>
    public enum FlipType
    {
        Horizontal,
        Vertical,
        Empty
    }
}
