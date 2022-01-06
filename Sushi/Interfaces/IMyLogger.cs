using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Interfaces
{
    public interface IMyLogger
    {
        string Log(LogLevel logLevel, string message, string nameMethod, Exception ex = null);
        public void Info(string message);

        public void Debug(string message);

        public void Warning(string message);

        public void Error(string message, Exception ex);
    }
}
