using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    public interface IFileService
    {
        Task WriteLog(string message,Exception exception = null);
    }
}
