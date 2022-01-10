using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace SushiMarcet.MyLogger
{
    public class Logger : IMyLogger
    {       
        private string _nameSpace;
        private readonly string _currentTime = DateTime.Now.ToString();
        private FileService? _fileService;

        private string DesignerString(LogLevel logLevel, string message, string nameSpace, string nameMethod, Exception exception)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            return exception != null
                ? $"{_currentTime}|{logLevel}|{nameSpace}|{nameMethod}|Thread - {Environment.CurrentManagedThreadId}|{message}|{exception}"
                : $"{_currentTime}|{logLevel}|{nameSpace}|{nameMethod}|Thread - {Environment.CurrentManagedThreadId}|{message}";
        }

        public string Log(LogLevel logLevel, string message, string nameSpace, string nameMethod,Exception ex = null)
        {
            string logMessage = DesignerString(logLevel, message, nameSpace, nameMethod, ex);

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

        private string GetNameSpase()
        {
            var mth = new StackTrace().GetFrame(5).GetMethod();
            var cls = mth.ReflectedType.Name;
            return cls;
        }

        public async void Info(string message)
        {
            _nameSpace = GetNameSpase();
            string methodName = GetMethodName();          
            string logMessage = Log(LogLevel.INFO, message, _nameSpace,  methodName);
            _fileService = new FileService(logMessage);
            await _fileService.WriteLog();
        }

        public async void Debug(string message)
        {
            _nameSpace = GetNameSpase();
            string methodName = GetMethodName();
            string logMessage = Log(LogLevel.DEBUG, message, _nameSpace, methodName);
            _fileService = new FileService(logMessage);
            await _fileService.WriteLog();
        }

        public async void Warning(string message)
        {
            _nameSpace = GetNameSpase();
            string methodName = GetMethodName();
            string logMessage = Log(LogLevel.WARNING, message, _nameSpace, methodName);
            _fileService = new FileService(logMessage);
            await _fileService.WriteLog();
        }

        public async void Error(string message, Exception ex)
        {
            _nameSpace = GetNameSpase();
            string methodName = GetMethodName();
            string logMessage = Log(LogLevel.ERROR, message, _nameSpace, methodName, ex);
            _fileService = new FileService(logMessage);
            await _fileService.WriteLog();
        }
    }
}
