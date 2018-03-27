using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;
using log4net.Appender;
using System.IO;
using log4net.Repository.Hierarchy;
using System.Reflection;

namespace CII.LAR
{
    /// <summary>
    /// 日志处理类，必须在Configuration目录下包含log4net.xml文件，此文件用于描述日志的输出方式。
    /// 非常重要:
    /// 使用此类时必须将项目属性的目标框架改成 Net Framework 4.0，否则编译不通过
    /// </summary>
    /// <remark>
    /// 思路：封装log4net库
    /// 公司：CASCO
    /// 作者：张立鹏
    /// 创建日期：2013-4-20
    /// </remark>
    public class LogHelper
    {
        private static int _i = Init();

        private const string FileName = @"/Config/log4net.xml";

        private static int Init()
        {
            FileInfo finfo = null;

            try
            {
                string path = global::System.AppDomain.CurrentDomain.BaseDirectory;
                //    string path= Environment.CurrentDirectory;
                //优先尝试从EXE所在目录查找配置文件
                finfo = new FileInfo(path + FileName);
            }
            catch (Exception)
            {
                finfo = null;
            }

            try
            {
                if (finfo == null)
                {
                    string path = Assembly.GetExecutingAssembly().Location;
                    //优先尝试从EXE所在目录查找配置文件
                    finfo = new FileInfo(path + FileName);
                }
            }
            catch (Exception)
            {
                finfo = null;
            }

            try
            {
                if (finfo == null)
                {
                    //如果没有找到，则使用相对路径查找配置文件
                    finfo = new FileInfo("." + FileName);
                }
            }
            catch (Exception)
            {
                finfo = null;
            }

            try
            {
                //配置文件修改后会立即生效，无需重启应用
                XmlConfigurator.ConfigureAndWatch(finfo);
            }
            catch (Exception ee)
            {
                //如果没有找到此文件，系统默认使用控制台方式输出
                BasicConfigurator.Configure();
                GetLogger<LogHelper>().Error("LogHelper初始化失败，使用默认的控制台方式输出。");
                GetLogger<LogHelper>().Error(ee.StackTrace);
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// 得到logger日志对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Logger GetLogger<T>()
        {
            ILog log = LogManager.GetLogger(typeof(T));
            Logger logger = new Logger(log);
            return logger;
        }

        /// <summary>
        /// 得到logger日志对象
        /// </summary>
        /// <returns></returns>
        public static Logger GetLogger(string logname)
        {
            //System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            ILog log = LogManager.GetLogger(logname);
            Logger logger = new Logger(log);
            return logger;
        }

        /// <summary>
        /// 根据配置文件的配置信息得到当前配置的输出器实例
        /// </summary>
        /// <param name="appenderName"></param>
        /// <returns></returns>
        public static IAppender GetAppender(string appenderName)
        {
            try
            {
                IAppender[] appenders = LogManager.GetAllRepositories()[0].GetAppenders();
                foreach (IAppender appender in appenders)
                {
                    if (appender.Name == appenderName)
                    {
                        return appender;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 程序在运行时可以动态指定根日志（配置文件中Root的部分）的输出级别
        /// </summary>
        /// <param name="level"></param>
        public static void SetRootLevel(Level level)
        {
            log4net.Core.Level log4netLevel = null;

            switch (level)
            {
                case Level.All:
                    log4netLevel = log4net.Core.Level.All;
                    break;
                case Level.Debug:
                    log4netLevel = log4net.Core.Level.Debug;
                    break;
                case Level.Info:
                    log4netLevel = log4net.Core.Level.Info;
                    break;
                case Level.Warn:
                    log4netLevel = log4net.Core.Level.Warn;
                    break;
                case Level.Error:
                    log4netLevel = log4net.Core.Level.Error;
                    break;
                case Level.Off:
                    log4netLevel = log4net.Core.Level.Off;
                    break;
            }

            Hierarchy hierarchy = LogManager.GetRepository() as Hierarchy;
            if (hierarchy != null)
            {
                hierarchy.Root.Level = log4netLevel;
                hierarchy.Configured = true;
                hierarchy.RaiseConfigurationChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// 日志输出级别枚举，off > fatal > error > warn > info > debug > all
    /// </summary>
    public enum Level
    {
        All = 0,
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5,
        Off = 6
    }

    /// <summary>
    /// 日志实体类，用于输出日志
    /// 日志输出级别从低到高依次是：
    /// （1）Debug：调试信息；
    /// （2）Info：信息；
    /// （3）Warn：警告；
    /// （4）Error：错误；
    /// （5）Fatal：致命；
    /// </summary>
    public class Logger
    {
        protected ILog Log;

        public Logger(ILog log)
        {
            Log = log;
        }
        /// <summary>
        /// 打印调试日志
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message)
        {
            Log.Debug(message);
        }
        /// <summary>
        /// 打印错误日志
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message)
        {
            Log.Error(message);
        }
        /// <summary>
        /// 打印致命错误日志
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(object message)
        {
            Log.Fatal(message);
        }
        /// <summary>
        /// 打印信息日至
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message)
        {
            Log.Info(message);
        }
        /// <summary>
        /// 打印警告日志
        /// </summary>
        /// <param name="message"></param>
        public void Warn(object message)
        {
            Log.Warn(message);
        }
    }
}
