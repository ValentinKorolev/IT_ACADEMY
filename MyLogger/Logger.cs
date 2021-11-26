using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MyLogger;

namespace SushiMarcet
{
    public static class Logger 
    {
        private static FileService _fileService;
        private readonly static string _nameSpace;
        private readonly static string _currentTime = DateTime.Now.ToLongTimeString();
      
        private static string DesignerString( LogLevel logLevel, string message, Exception exception)
        {
            return exception !=null 
                ? $"{_currentTime}|{_nameSpace}|{logLevel.ToString()}|{message}|{exception}"
                : $"{_currentTime}|{_nameSpace}|{logLevel.ToString()}|{message}";
        }
        public static void Log(LogLevel logLevel, string message, Exception exception = null)
        {
            string answer = DesignerString(logLevel, message, exception);

            switch (logLevel)
            {
                case LogLevel.INF:
                    Info(answer);
                    break;
                case LogLevel.DEB:
                    Debug(answer);
                    break;
                case LogLevel.ERR:
                    Error(answer,exception);
                    break;
            }
        }
        public static void Info(string message)
        {            
             _fileService.WriteLog(message);
        }

        public static void Debug(string message)
        {
            _fileService.WriteLog(message);
        }

        public static void Error(string message, Exception ex)
        {
            _fileService.WriteLog(message, ex);
        }
        
    }
}
