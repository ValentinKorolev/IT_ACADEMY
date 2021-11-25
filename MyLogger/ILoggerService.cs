using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet
{
    internal interface ILoggerService
    {
        public string Info(string infoMessage);

        public string Debug(string debugMessage);

        public string Error(string errorMessage, Exception ex);
    }
}
