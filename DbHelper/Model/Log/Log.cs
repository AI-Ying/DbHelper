using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Configuration;

//注意下面的语句一定要加上，指定log4net使用.config文件来读取配置信息  
//如果是WinForm（假定程序为MyDemo.exe，则需要一个MyDemo.exe.config文件）  
//如果是WebForm，则从web.config中读取相关信息  
[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile=@"")]
namespace DataBaseHelper
{  
    public static class Log
    { 
        public static void Debug(object message)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Debug(message);
        }
        public static void Debug(object message, Exception ex)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Debug(message, ex);
        }
        public static void Error(object message)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Error(message);
        }
        public static void Error(object message, Exception exception)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Error(message, exception);
        }
        public static void Info(object message)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Info(message);
        }
        public static void Info(object message, Exception ex)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Info(message, ex);
        }
        public static void Warn(object message)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Warn(message);
        }
        public static void Warn(object message, Exception ex)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Warn(message, ex);
        }
        /// <summary>
        /// 通过堆栈跟踪，获取当前调用方法名
        /// </summary>
        /// <returns>返回当前调用的方法名</returns>
        private static string GetCurrentMethodFullName()
        {
            StackFrame frame;
            string str;
            string str1;
            bool flag;
            try
            {
                int num = 2;
                StackTrace stackTrace = new StackTrace();
                int length = stackTrace.GetFrames().Length;
                do
                {
                    int num1 = num;
                    num = num1 + 1;
                    frame = stackTrace.GetFrame(num1);
                    str = frame.GetMethod().DeclaringType.ToString();
                    flag = (!str.EndsWith("Exception") ? false : num < length);
                }
                while (flag);
                string name = frame.GetMethod().Name;
                str1 = string.Concat(str, ".", name);
            }
            catch
            {
                str1 = null;
            }
            return str1;
        }

    }
}
