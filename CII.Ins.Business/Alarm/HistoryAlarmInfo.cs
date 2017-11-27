//==================================================================================================
// Cell100报警码类操作类定义
// 创建人：刘海生
// 创建时间: 2015.11.13
//
// 修改人 修改时间 修改后版本 修改内容
//
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using CII.Library.Xml;
using CII.Library.Alarm;
using CII.Ins.Model.GlobalConfig;

namespace CII.Ins.Business.Alarm
{
    /// <summary>
    /// Cell100报警码类操作类定义
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public class HistoryAlarmInfo : BaseNode
    {
        #region 字段
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 报警源
        /// </summary>
        public string alarmSource;
        /// <summary>
        /// 报警等级
        /// </summary>
        public string alarmGrade;
        /// <summary>
        /// 报警码
        /// </summary>
        public string alarmCode;
        /// <summary>
        /// 报警描述
        /// </summary>
        public string alarmDescription;
        /// <summary>
        /// 首次报警时间
        /// </summary>
        public string alarmFirstTime;
        /// <summary>
        /// 最近报警时间
        /// </summary>
        public string alarmUpdateTime;
        /// <summary>
        /// 消除报警时间
        /// </summary>
        public string alarmRemoveTime;
        #endregion

        #region 构造函数
        /// <summary>
        ///
        /// </summary>
        public HistoryAlarmInfo()
        {

        }
        /// <summary>
        ///
        /// </summary>
        public HistoryAlarmInfo(AlarmInfo alarmInfo)
        : this()
        {
            if (alarmInfo != null && alarmInfo.AlarmCode != null)
            {
                this.alarmSource = alarmInfo.AlarmSource.id;
                this.alarmGrade = alarmInfo.AlarmCode.GetGrade().id;
                this.alarmCode = alarmInfo.AlarmCode.id;
                this.alarmDescription = alarmInfo.AlarmCode.description;
                this.alarmFirstTime = alarmInfo.FirstTime.ToString(DATE_FORMAT);
                this.alarmUpdateTime = alarmInfo.UpdateTime.ToString(DATE_FORMAT);
                this.alarmRemoveTime = alarmInfo.RemoveTime.ToString(DATE_FORMAT);
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 报警源
        /// </summary>
        public string AlarmSource
        {
            get { return alarmSource; }
            set { alarmSource = value; }
        }
        /// <summary>
        /// 报警等级
        /// </summary>
        public string AlarmGrade
        {
            get { return alarmGrade; }
            set { alarmGrade = value; }
        }
        /// <summary>
        /// 报警码
        /// </summary>
        public string AlarmCode
        {
            get { return alarmCode; }
            set { alarmCode = value; }
        }
        /// <summary>
        /// 报警描述
        /// </summary>
        public string AlarmDescription
        {
            get { return alarmDescription; }
            set { alarmDescription = value; }
        }
        /// <summary>
        /// 首次报警时间
        /// </summary>
        public string AlarmFirstTime
        {
            get { return alarmFirstTime; }
            set { alarmFirstTime = value; }
        }
        /// <summary>
        /// 最近报警时间
        /// </summary>
        public string AlarmUpdateTime
        {
            get { return alarmUpdateTime; }
            set { alarmUpdateTime = value; }
        }
        /// <summary>
        /// 消除报警时间
        /// </summary>
        public string AlarmRemoveTime
        {
            get { return alarmRemoveTime; }
            set { alarmRemoveTime = value; }
        }
        #endregion
    }
}