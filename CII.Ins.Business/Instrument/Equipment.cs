//==================================================================================================
// 仪器设备类定义
// 创建人：刘海生
// 创建时间: 2017.04.25
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
using System.IO;
using CII.Library.Alarm;
using CII.Ins.Model.Data.HV;
using CII.Ins.Business.Command.Interface;
using CII.Ins.Business.Command.HV;
using CII.Ins.Business.Alarm;

namespace CII.Ins.Business.Instrument
{
    /// <summary>
    /// 仪器设备类定义
    /// 创建人：刘海生
    /// 创建时间: 2017.04.25
    /// </summary>
    [Serializable]
    public class Equipment : IdNameNode
    {
        #region -- 通用事件 --
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="tag"></param>
        public delegate void EventHandle();
        /// <summary>
        /// 监控数据刷新
        /// </summary>
        public event EventHandle MonitorEvent;
        #endregion

        /// <summary>
        /// 一台仪器控制参数名称
        /// </summary>
        protected static readonly string MethodName = "MethodData";
        /// <summary>
        /// 多台仪器列表
        /// </summary>
        protected static readonly string InsListName = "InsListData";
        /// <summary>
        /// 仪器通信失败次数
        /// </summary>
        private int EquipmentConnectFailedCount = GlobalConfig.EquipmentConnectFailedMaxCount;

        public Equipment()
        {
            try
            {
                //加载默认
                loadXml();
            }
            catch (Exception ex)
            {
                Entry.HV.Entry.LogException(ex);
            }
        }

        /// <summary>
        /// 刷新监控数据
        /// </summary>
        public void RefreshMonitorData()
        {
            if (MonitorEvent != null)
            {
                MonitorEvent();
            }
        }

        public NodeList insInfoListData = new NodeList();
        /// <summary>
        /// 仪器列表
        /// </summary>
        public InsInfo[] InsInfoList
        {
            get
            {
                List<InsInfo> infoList = new List<InsInfo>();
                if (insInfoListData == null || insInfoListData[InsListName] == null)
                {
                    for (int i = 0; i < insInfoListData.GetCount(); ++i)
                    {
                        if (insInfoListData[i] is InsInfo)
                        {
                            infoList.Add(insInfoListData[i] as InsInfo);
                        }
                    }
                }
                return infoList.ToArray();
            }
            set
            {
                insInfoListData.Clear();
                for (int i = 0; i < value.Length; ++i)
                {
                    value[i].id = value[i].name = i.ToString();
                    insInfoListData.Add(value[i]);
                }
            }
        }

        public NodeList mthData = new NodeList();
        /// <summary>
        /// 仪器控制参数
        /// </summary>
        public Method MthData
        {
            get
            {
                if (mthData == null || mthData[MethodName] == null)
                {
                    Method mth = new Method();
                    mth.id = mth.name = MethodName;
                    mthData.Add(mth);
                }
                return (mthData[MethodName] as Method);
            }
            set
            {
                mthData.Clear();
                value.id = value.name = MethodName;
                mthData.Add(value);
            }
        }

        private MonitorData monitorData = null;
        /// <summary>
        /// 监控数据
        /// </summary>
        public MonitorData MonitorData
        {
            get { return monitorData; }
            set { monitorData = value; }
        }

        /// <summary>
        /// 读取监控数据
        /// </summary>
        public void GetMonitorData()
        {
            List<byte> alarmCode = new List<byte>();
            try
            {
                MonitorData = this.MthData.GetMonitorData();
                this.EquipmentConnectFailedCount = 0;
                //alarmCode.Add(MonitorData.AlarmCode);
            }
            catch (Exception ex)
            {
                this.EquipmentConnectFailedCount++;
                if (this.EquipmentConnectFailedCount >= GlobalConfig.EquipmentConnectFailedMaxCount)
                {
                    MonitorData = null;
                    //网络通信故障
                    alarmCode.Insert(0, 0xff);
                }
                else
                {
                    Entry.HV.Entry.LogDebug(string.Format("Equipment connect failed, count={0}", this.EquipmentConnectFailedCount));
                }
            }
            CheckAlarm(alarmCode.ToArray());
        }

        /// <summary>
        /// 设置控制参数
        /// </summary>
        public void SetMethod()
        {
            this.MthData.SetMethod();
        }

        #region -- 仪器报警处理 --
        /// <summary>
        /// 报警处理
        /// </summary>
        /// <param name="mon"></param>
        protected void CheckAlarm(byte[] alarmCode)
        {
            try
            {
                List<string> acList = new List<string>();

                //未有报警
                if (alarmCode.Length == 0 || alarmCode[0] == 0x00)
                {
                    //删除当前报警（当前报警自动转化为历史报警）
                    AlarmHelper.RemoveAlarm(GlobalConfig.AlarmSource);
                }
                else
                {
                    //添加MCU报警
                    for (int i = 0; alarmCode != null && i < alarmCode.Length; i++)
                    {
                        if (alarmCode[i] > 0x00)
                        {
                            acList.Add(string.Format("0x{0}", CII.Library.Util.Sundry.StringUtil.ByteToString(alarmCode[i])));
                        }
                    }

                    AlarmHelper.AddAlarm(GlobalConfig.AlarmSource, acList);
                }
            }
            catch (Exception ex)
            {
                Entry.HV.Entry.LogException(ex);
            }
        }
        #endregion

        #region -- GetInstance --
        private static object SyncObject = new object();
        private static Equipment Instance = null;
        public static Equipment GetInstance()
        {
            lock (SyncObject)
            {
                if (Instance == null)
                {
                    Instance = new Equipment();
                }
            }
            return Instance;
        }
        #endregion
    }
}