using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    public sealed class FileService : IFileService
    {
        private const string Path = @"D:\SushiLogs\log.txt";

        public async Task WriteLog(string message, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(Path,true,Encoding.Default))
            {
               await sw.WriteLineAsync(message);
            }
        }
    }
}
