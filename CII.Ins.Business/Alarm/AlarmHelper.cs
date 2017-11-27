//==================================================================================================
// Cell100报警码类操作接口类定义
// 创建人：刘海生
// 创建时间: 2015.11.13
//
// 修改人 修改时间 修改后版本 修改内容
//
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CII.Library.Alarm;

namespace CII.Ins.Business.Alarm
{
    /// <summary>
    /// Cell100报警码类操作接口类定义
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public class AlarmHelper
    {
        #region 报警码操作接口
        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="acList"></param>
        public static void AddAlarm(string source, List<string> acList)
        {
            try
            {
                //添加新报警
                for (int i = 0; i < acList.Count; ++i)
                {
                    //报警码全部为小写
                    acList[i] = acList[i].ToLower();

                    bool isValid = false;
                    //检查新报警是否合法
                    foreach (AlarmCode ac in AlarmManager.GetInstance().alarmCodes)
                    {
                        if (Convert.ToInt32(ac.id, 16) == Convert.ToInt32(acList[i], 16))
                        {
                            isValid = true;
                            break;
                        }
                    }
                    if (isValid)
                    {
                        AlarmManager.GetInstance().AddAlarm(source, acList[i]);
                    }
                    else
                    {
                        //这个一个非法报警码
                    }
                }

                //更新当前报警
                CII.Library.Alarm.AlarmInfo[] currentAlarms = CII.Library.Alarm.AlarmManager.GetInstance().GetCurrentAlarms();
                for (int i = 0; currentAlarms != null && i < currentAlarms.Length; ++i)
                {
                    if (!acList.Contains(currentAlarms[i].AlarmCode.id))
                    {
                        AlarmManager.GetInstance().RemoveAlarm(source, currentAlarms[i].AlarmCode.id);
                    }
                }
            }
            catch (Exception ex)
            {
                Entry.HV.Entry.LogException(ex);
            }
        }

        /// <summary>
        /// 删除当前报警（当前报警被删除时自动转化为历史报警）
        /// </summary>
        public static void RemoveAlarm(string source)
        {
            try
            {
                CII.Library.Alarm.AlarmInfo[] currentAlarms = CII.Library.Alarm.AlarmManager.GetInstance().GetCurrentAlarms();
                for (int i = 0; currentAlarms != null && i < currentAlarms.Length; ++i)
                {
                    AlarmManager.GetInstance().RemoveAlarm(source, currentAlarms[i].AlarmCode.id);
                }
            }
            catch (Exception ex)
            {
                Entry.HV.Entry.LogException(ex);
            }
        }
        #endregion
    }
}