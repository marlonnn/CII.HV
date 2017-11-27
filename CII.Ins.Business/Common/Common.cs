//==================================================================================================
// 仪器Common类定义
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
using System.Net.Mail;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace CII.Ins.Business.Common
{
    /// <summary>
    /// 仪器Common类定义
    /// 创建人：刘海生
    /// 创建时间: 2017.04.25
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 读取CPU序列号
        /// </summary>
        /// <returns></returns>
        public static string GetSetupID()
        {
            string id = "SetupID";//cpu序列号
            try
            {
                //ManagementClass mc = new ManagementClass("Win32_Processor");
                //ManagementObjectCollection moc = mc.GetInstances();
                //foreach (ManagementObject mo in moc)
                //{
                // id += mo.Properties["ProcessorId"].Value.ToString();
                // break;
                //}

                //string strbNumber = string.Empty;
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
                //foreach (ManagementObject mo in mos.Get())
                //{
                //    id += CII.Library.Util.Sundry.StringUtil.GetMD5String(mo["SerialNumber"].ToString());
                //    break;
                //}
            }
            catch { }
            return id;
        }

        /// <summary>
        /// 邮件通知
        /// </summary>
        /// <param name="text"></param>
        public static void SendMail(string smtpServer, string fromEmailAddress, string fromEmailPassword, string toEmailAddress, string subject, string body)
        {
            SmtpClient client = new SmtpClient(smtpServer);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage message = new MailMessage();
            string fromMail = fromEmailAddress;
            MailAddress addressFrom = new MailAddress(fromMail, subject);
            message.From = addressFrom;
            string[] arrToEmailAddress = toEmailAddress.Split(';');
            for (int i = 0; i < arrToEmailAddress.Length; ++i)
            {
                if (!string.IsNullOrEmpty(arrToEmailAddress[i]))
                {
                    message.To.Add(arrToEmailAddress[i]);
                }
            }
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = false;
            message.Subject = subject;
            message.Body = body;
            client.Send(message);
        }

        /// <summary>
        /// Tab空格键
        /// </summary>
        public static string Tab = " ";
        /// <summary>
        /// 线程互斥锁
        /// </summary>
        private static Mutex Mutex = new Mutex();
        /// <summary>
        /// 输入日志，调试日志
        /// </summary>
        /// <param name="text"></param>
        public static void Log(string text)
        {
            StreamWriter sw = null;
            try
            {
                Mutex.WaitOne();
                string fileName = Path.Combine(CII.Library.Xml.ConstConfig.AppPath, string.Format("Log\\Debug [{0}].txt", DateTime.Now.ToString("yyyy-MM-dd")));
                string path = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                sw = System.IO.File.AppendText(fileName);
                sw.WriteLine(CII.Library.Util.Security.AesCryptHelper.GetInstance().Encrypt(string.Format("<{0}> {1} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), text)));
            }
            catch { }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// 保存监控数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="line"></param>
        public static bool SaveMonitorData(string fileName, string[] lines)
        {
            bool hResult = false;
            StreamWriter sw = null;
            try
            {
                Mutex.WaitOne();
                string path = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                sw = System.IO.File.AppendText(fileName);
                for (int i = 0; i < lines.Length; ++i)
                {
                    if (lines[i].IndexOf('<') == 0)
                    {
                        sw.WriteLine(CII.Library.Util.Security.AesCryptHelper.GetInstance().Encrypt(lines[i].Replace(",", Tab)));//第一列带有时间信息，必须“,”分隔
                    }
                    else
                    {
                        sw.WriteLine(CII.Library.Util.Security.AesCryptHelper.GetInstance().Encrypt(string.Format("<{0}>{1}{2} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tab, lines[i].Replace(" ", Tab).Replace(",", Tab))));
                    }
                }
                hResult = true;
            }
            catch { }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                Mutex.ReleaseMutex();
            }
            return hResult;
        }

        /// <summary>
        /// 保存监控数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="line"></param>
        public static bool SaveMonitorData(string fileName, string line)
        {
            bool hResult = false;
            StreamWriter sw = null;
            try
            {
                Mutex.WaitOne();
                string path = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                sw = System.IO.File.AppendText(fileName);
                if (line.IndexOf('<') == 0)
                {
                    sw.WriteLine(CII.Library.Util.Security.AesCryptHelper.GetInstance().Encrypt(line.Replace(",", Tab)));//第一列带有时间信息，必须“,”分隔
                }
                else
                {
                    sw.WriteLine(CII.Library.Util.Security.AesCryptHelper.GetInstance().Encrypt(string.Format("<{0}>{1}{2} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tab, line.Replace(" ", Tab).Replace(",", Tab))));
                }
                hResult = true;
            }
            catch { }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                Mutex.ReleaseMutex();
            }
            return hResult;
        }

    }

    /// <summary>
    /// 系统信息CPU、内存等
    /// </summary>
    /// 创建者：王家杰
    /// 创建时间：2017-3-10
    public class SysInfo
    {
        /// <summary>
        /// 获取CPU占有率
        /// </summary>
        /// <param name="processname">进程名</param>
        /// <returns></returns>
        public static float GetCpuInfo(string processname)
        {
            PerformanceCounter pcpu = new PerformanceCounter();
            pcpu.CategoryName = "Process";
            pcpu.CounterName = "% Processor Time";
            pcpu.InstanceName = processname;
            pcpu.MachineName = ".";
            return pcpu.NextValue();
        }

        /// <summary>
        /// 获取当前进程
        /// </summary>
        /// <returns></returns>
        public static Process GetCurrentProcessInfo()
        {
            return Process.GetCurrentProcess();
        }
        /// <summary>
        /// 获取进程根据ID
        /// </summary>
        /// <returns></returns>
        public static Process GetProcessInfoByID(int id)
        {
            return Process.GetProcessById(id);
        }
        /// <summary>
        /// 获取进程根据Name
        /// </summary>
        /// <returns></returns>
        public static Process[] GetProcessInfoByName(string name)
        {
            return Process.GetProcessesByName(name);
        }
        /// <summary>
        /// 获取所有进程
        /// </summary>
        /// <returns></returns>
        public static Process[] GetProcesses()
        {
            return Process.GetProcesses();
        }
    }
}