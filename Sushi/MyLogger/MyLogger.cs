using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.MyLogger
{
    public class MyLogger<T> /*: IMyLogger*/
    {
        private readonly string _nameSpace;
        private readonly string _nameMethod;
        private readonly string _currentTime = DateTime.Now.ToString();
        private FileService? _fileLogObserver;

        public MyLogger()
        {
            _nameSpace = typeof(T).Name;
            

            //_nameMethod = MethodBase.GetCurrentMethod().Name;
        }

        private string DesignerString(LogLevel logLevel, string message,  string nameMethod, Exception exception)
        {
            return exception != null
                ? $"{_currentTime}|{logLevel}|{_nameSpace}|{nameMethod}|Thread - {Environment.CurrentManagedThreadId}|{message}|{exception}"
                : $"{_currentTime}|{logLevel}|{_nameSpace}|{nameMethod}|Thread - {Environment.CurrentManagedThreadId}|{message}";
        }

        private string Log(LogLevel logLevel, string message, string nameMethod,Exception ex = null)
        {
            string logMessage = DesignerString(logLevel, message, nameMethod, ex);

            return logLevel switch
            {
                LogLevel.INFO => logMessage,
                LogLevel.DEBUG => logMessage,
                LogLevel.ERROR => logMessage,
                LogLevel.WARNING => logMessage,
                _ => throw new NotImplementedException(),
            };
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetMethodName()
        {
            var st = new StackTrace(new StackFrame(5));
            return st.GetFrame(0).GetMethod().Name;
        }

        public async void Info(string message)
        {
            string methodName = GetMethodName();          
            string logMessage = Log(LogLevel.INFO, message, methodName);
            _fileLogObserver = new FileService(logMessage);
            await _fileLogObserver.WriteLog();
        }

        public async void Debug(string message)
        {
            string methodName = GetMethodName();
            string logMessage = Log(LogLevel.DEBUG, message, methodName);
            _fileLogObserver = new FileService(logMessage);
            await _fileLogObserver.WriteLog();
        }

        public async void Warning(string message)
        {
            string methodName = GetMethodName();
            string logMessage = Log(LogLevel.WARNING, message, methodName);
            _fileLogObserver = new FileService(logMessage);
            await _fileLogObserver.WriteLog();
        }

        public async void Error(string message, Exception ex)
        {
            string methodName = GetMethodName();
            string logMessage = Log(LogLevel.ERROR, message, methodName, ex);
            _fileLogObserver = new FileService(logMessage);
            await _fileLogObserver.WriteLog();
        }
    }
}
