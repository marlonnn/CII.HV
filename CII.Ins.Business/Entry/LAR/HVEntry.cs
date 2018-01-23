//==================================================================================================
// Cell100统一接口操作类定义
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
using CII.Library.Xml;
using CII.Ins.Model.GlobalConfig;

namespace CII.Ins.Business.Entry.LAR
{
    /// <summary>
    /// Cell100统一接口操作类定义（如写数据到文件到数据库，从文件或数据库读数据，与仪器通信操作等）
    /// 先统一放到这里，稍后视情况再定什么哪种方式
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public class Entry
    {
        /// <summary>
        /// 根据名称记录一个值，带保存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetValue(string name, string value)
        {
            try
            {
                VarConfig.SetValue(name, value);
                VarConfig.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// 根据名称记录一个值，带保存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetValue(string name, int value)
        {
            try
            {
                SetValue(name, value.ToString());
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// 根据名称记录一个值，带保存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetValue(string name, float value)
        {
            try
            {
                SetValue(name, value.ToString());
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// 根据名称记录一个值，带保存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetValue(string name, bool value)
        {
            try
            {
                SetValue(name, value.ToString());
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// 根据名字读取一个值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValueString(string name)
        {
            try
            {
                string value = VarConfig.GetValue(name);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据名字读取一个值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetValueInt(string name)
        {
            try
            {
                int result = 0;
                string value = GetValueString(name);
                if (int.TryParse(value, out result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return 0;
        }

        /// <summary>
        /// 根据名字读取一个值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static float GetValueFloat(string name)
        {
            try
            {
                float result = 0;
                string value = GetValueString(name);
                if (float.TryParse(value, out result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return 0;
        }

        /// <summary>
        /// 根据名字读取一个值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool GetValueBool(string name)
        {
            try
            {
                bool result = false;
                string value = GetValueString(name);
                if (bool.TryParse(value, out result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return false;
        }

        /// <summary>
        /// 一般性日志统一入口
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static void Log(string text)
        {
            CII.Library.Log.LogUtil.SysLog(string.Format("{0}", text));
        }

        /// <summary>
        /// 异常日志，仅供开发人员查看
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static void LogException(Exception ex)
        {
            CII.Library.Log.LogUtil.TraceException(ex);
        }

        /// <summary>
        /// 调试日志，仅供开发人员查看
        /// </summary>
        /// <param name="text"></param>
        public static void LogDebug(string text)
        {
            Common.Common.Log(text);
        }

        /// <summary>
        /// 记录监控数据
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool RecordMonitor(string fileName, string line)
        {
            return Common.Common.SaveMonitorData(fileName, line);
        }

        /// <summary>
        /// 记录监控数据
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool RecordMonitor(string fileName, string[] lines)
        {
            return Common.Common.SaveMonitorData(fileName, lines);
        }

    }
}