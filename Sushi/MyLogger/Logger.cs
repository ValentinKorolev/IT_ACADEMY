using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MyLogger;

namespace SushiMarcet
{
    public static class Logger<T> 
    {
        private static string _nameSpace;
        private readonly static string _currentTime = DateTime.Now.ToString();
        private static string Path = $@"D:\SushiLogs\log.txt";

        public static async Task WriteLog(string message, Exception ex = null)
        {
            using (StreamWriter sw = new StreamWriter(Path, true, Encoding.Default))
            {
                await sw.WriteLineAsync(message);
            }
        }

        private static string DesignerString( LogLevel logLevel, string message, Exception exception)
        {
            return exception !=null 
                ? $"{_currentTime}|{_nameSpace = typeof(T).Name}|{logLevel.ToString()}|{message}|{exception}"
                : $"{_currentTime}|{_nameSpace = typeof(T).Name}|{logLevel.ToString()}|{message}";
        }

        private static string Log(LogLevel logLevel, string message, Exception exception = null)
        {
            string answer = DesignerString(logLevel, message, exception);

            switch (logLevel)
            {
                case LogLevel.INF:
                    return answer;
                    break;
                case LogLevel.DEB:
                    return answer;
                    break;
                case LogLevel.ERR:
                    return answer;
                    break;
                default: return answer;
            }
        }
        public static void Info(string message)
        {    
            
             WriteLog(Log(LogLevel.INF, message));
        }

        public static void Debug(string message)
        {
            WriteLog(Log(LogLevel.DEB, message));
        }

        public static void Error(string message, Exception ex)
        {
            WriteLog(Log(LogLevel.ERR, message,ex));
        }  
    }
}
