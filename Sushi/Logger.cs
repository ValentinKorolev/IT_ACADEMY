using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SushiMarcet
{
    public class Logger : ILoggerService
    {
        static DateTime _time = DateTime.Now;
        static string _fileName = $"log {_time}";
  
        public string Info(string infoMessage)
        {
            
            return $"{_time} [INF]: {infoMessage}";
        }

        public string Debug(string debugMessage)
        {
            return $"{_time} [DEB]: {debugMessage}";
        }

        public string Error(string errorMessage, Exception ex = null)
        {
            return $"{_time} [ERR]: {errorMessage} {ex}";
        }

        public void CreateLogsFile(string _pathFile)
        {
            DateTime _time;

            //30720b = 30 kb
            File.WriteAllBytes("logs.txt", new byte[30720]);
        } 
    }
}
