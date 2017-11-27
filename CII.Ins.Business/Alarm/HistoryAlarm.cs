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
using System.Collections;
using System.Linq;
using CII.Library.Xml;
using CII.Library.Alarm;

namespace CII.Ins.Business.Alarm
{
    /// <summary>
    /// Cell100报警码类操作类定义
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public class HistoryAlarm : BaseNode, ISaveAlarm
    {
        #region 字段
        /// <summary>
        /// 历史报警保存的数量
        /// </summary>
        private const int HISTORYALARMINFOMAXCOUNT = 100;
        /// <summary>
        /// 历史报警信息列表
        /// </summary>
        public ArrayList historyAlarmInfos = new ArrayList();
        #endregion

        #region 构造函数
        /// <summary>
        ///
        /// </summary>
        public HistoryAlarm()
        {
            this.loadXml();
        }
        #endregion

        #region ISaveAlarm 成员
        /// <summary>
        /// 查询历史报警
        /// </summary>
        /// <param name="alarmSource"></param>
        /// <param name="alarmGrade"></param>
        /// <param name="alarmCode"></param>
        /// <param name="firstTimeBegin"></param>
        /// <param name="firstTimeEnd"></param>
        /// <param name="updateTimeBegin"></param>
        /// <param name="updateTimeEnd"></param>
        /// <param name="removeTimeBegin"></param>
        /// <param name="removeTimeEnd"></param>
        /// <returns></returns>
        public AlarmInfo[] QueryAlarm(string alarmSource, string alarmGrade, string alarmCode
        , DateTime? firstTimeBegin, DateTime? firstTimeEnd, DateTime? updateTimeBegin, DateTime? updateTimeEnd, DateTime? removeTimeBegin, DateTime? removeTimeEnd)
        {
            List<AlarmInfo> alarmInfoList = new List<AlarmInfo>();
            ArrayList alarmInfos = this.ExtractHistoryAlarmInfos(alarmSource, alarmGrade, alarmCode, firstTimeBegin
            , firstTimeEnd, updateTimeBegin, updateTimeEnd, removeTimeBegin, removeTimeEnd);

            AlarmInfo alarmInfo = null;
            var alarmCodeList = CII.Library.Alarm.AlarmManager.GetInstance().alarmCodes.OfType<AlarmCode>().Select(t => Convert.ToUInt16(t.id, 16)).ToList();
            foreach (HistoryAlarmInfo historyAlarmInfo in alarmInfos)
            {
                if (!alarmCodeList.Contains(Convert.ToUInt16(historyAlarmInfo.AlarmCode, 16)))
                {
                    continue;
                }

                alarmInfo = this.CreateAlarmInfo(historyAlarmInfo);
                alarmInfoList.Add(alarmInfo);
            }

            return alarmInfoList.ToArray();
        }
        /// <summary>
        /// 保存报警
        /// </summary>
        /// <param name="alarmInfo"></param>
        public void SaveAlarm(AlarmInfo alarmInfo)
        {
            HistoryAlarmInfo historyAlarmInfo = new HistoryAlarmInfo(alarmInfo);
            if (this.historyAlarmInfos.Count >= HISTORYALARMINFOMAXCOUNT && this.historyAlarmInfos.Count > 0)
            {
                this.historyAlarmInfos.RemoveAt(0);
            }
            this.historyAlarmInfos.Add(historyAlarmInfo);
            this.Save();
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 筛选报警信息
        /// </summary>
        /// <param name="alarmSource"></param>
        /// <param name="alarmGrade"></param>
        /// <param name="alarmCode"></param>
        /// <param name="firstTimeBegin"></param>
        /// <param name="firstTimeEnd"></param>
        /// <param name="updateTimeBegin"></param>
        /// <param name="updateTimeEnd"></param>
        /// <param name="updateTimeBegin"></param>
        /// <param name="removeTimeEnd"></param>
        /// <returns></returns>
        private ArrayList ExtractHistoryAlarmInfos(string alarmSource, string alarmGrade, string alarmCode
        , DateTime? firstTimeBegin, DateTime? firstTimeEnd, DateTime? updateTimeBegin, DateTime? updateTimeEnd, DateTime? removeTimeBegin, DateTime? removeTimeEnd)
        {
            try
            {
                this.historyAlarmInfos.Clear();
                this.loadXml();
            }
            catch { }
            ArrayList alarmInfos = new ArrayList();
            List<HistoryAlarmInfo> alarmInfoList = new List<HistoryAlarmInfo>();
            var tmpHistoryAlarmInfos = this.historyAlarmInfos.OfType<HistoryAlarmInfo>().ToList();
            foreach (HistoryAlarmInfo alramInfo in tmpHistoryAlarmInfos)
            {
                if ((string.IsNullOrEmpty(alarmSource) || alramInfo.AlarmSource == alarmSource)
                && (string.IsNullOrEmpty(alarmGrade) || alramInfo.AlarmGrade == alarmGrade)
                && (string.IsNullOrEmpty(alarmCode) || alramInfo.AlarmCode == alarmCode)
                && (firstTimeBegin == null || firstTimeBegin.Value <= Convert.ToDateTime(alramInfo.AlarmFirstTime))
                && (firstTimeEnd == null || Convert.ToDateTime(alramInfo.AlarmFirstTime) <= firstTimeEnd.Value)
                && (updateTimeBegin == null || updateTimeBegin.Value <= Convert.ToDateTime(alramInfo.AlarmUpdateTime))
                && (updateTimeEnd == null || Convert.ToDateTime(alramInfo.AlarmUpdateTime) <= updateTimeEnd.Value)
                && (removeTimeBegin == null || removeTimeBegin.Value <= Convert.ToDateTime(alramInfo.AlarmRemoveTime))
                && (removeTimeEnd == null || Convert.ToDateTime(alramInfo.AlarmRemoveTime) <= removeTimeEnd.Value)
                )
                {
                    alarmInfoList.Add(alramInfo);
                }
            }
            tmpHistoryAlarmInfos.Clear();
            alarmInfoList.Sort(new Comparison<HistoryAlarmInfo>(CompareHistoryAlarmInfo));
            alarmInfos.AddRange(alarmInfoList);
            return alarmInfos;
        }

        /// <summary>
        /// 历史报警信息
        /// </summary>
        /// <param name="historyAlarmInfo"></param>
        /// <returns></returns>
        private AlarmInfo CreateAlarmInfo(HistoryAlarmInfo historyAlarmInfo)
        {
            AlarmInfo alarmInfo = AlarmInfo.Load(historyAlarmInfo.AlarmSource, historyAlarmInfo.AlarmCode, historyAlarmInfo.AlarmDescription
            , Convert.ToDateTime(historyAlarmInfo.AlarmFirstTime), Convert.ToDateTime(historyAlarmInfo.AlarmUpdateTime), Convert.ToDateTime(historyAlarmInfo.AlarmRemoveTime));

            return alarmInfo;
        }
        /// <summary>
        /// 报警信息比较器
        /// </summary>
        /// <param name="cp1"></param>
        /// <param name="cp2"></param>
        /// <returns></returns>
        private int CompareHistoryAlarmInfo(HistoryAlarmInfo leftAlarmInfo, HistoryAlarmInfo rightAlarmInfo)
        {
            return -leftAlarmInfo.AlarmUpdateTime.CompareTo(rightAlarmInfo.AlarmUpdateTime);
        }

        #endregion
    }
}