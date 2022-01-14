using System.Text;


namespace SushiMarcet.MyLogger
{
    internal class FileService
    {
        private string _currentMessage;
        private long _sizeMessage;
        private byte _sizeNewlineCharacter = 2;

        private static long _fileSize;
        private static DateTime _currentTime = DateTime.Now;
        private long _maxLenght = 30720;

        private string _fileName;
        private string _currentPath = $@"D:\SushiLogs\log {_currentTime.ToString("yyyymmdd")}_[{_fileSize} byte].txt";
        private string _directory = @"D:\SushiLogs\";

        public FileService(string message)
        {
            _fileName = SearchFile();           

            if (_fileName is not null)
            {
                _currentMessage = message;
                _sizeMessage = message.Length;
                _fileSize = _sizeMessage + GetFileSize() + _sizeNewlineCharacter;

                if(_fileSize < _maxLenght)
                {
                    _currentPath = UpdateFileName(_fileName);
                }               
            }
            else
            {
                _currentMessage = message;
                _sizeMessage = message.Length;
                _fileSize = _sizeMessage + _sizeNewlineCharacter;
            }
            
        }

        public async Task WriteLog()
        {

            if (_fileSize >= _maxLenght)
            {
                _fileSize = 0;
                _fileSize = _sizeMessage + _sizeNewlineCharacter;


                using (StreamWriter sw = new StreamWriter(GetNameFile(_fileSize), true, Encoding.Default))
                {
                    await sw.WriteLineAsync(_currentMessage);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(GetNameFile(_fileSize), true, Encoding.Default))
                {
                    await sw.WriteLineAsync(_currentMessage);
                }
            }
        }

        private long GetFileSize()
        {
            FileInfo fileInfo = new FileInfo(_fileName);

            return fileInfo.Length;
        }

        private string UpdateFileName(string path)
        {

            File.Move(path, GetNameFile(_fileSize));
           
            return GetNameFile(_fileSize);   
        }

        private string SearchFile()
        {
            try
            {
                string path;

                var files = Directory.EnumerateFiles(_directory, "*.txt");

                path = files.FirstOrDefault(_=>_.Contains(_currentTime.ToString("yyyymmdd")));

                return path;
            }
            catch
            {
                return _currentPath;
            }
            
        }

        private string GetNameFile(long fileSize)
        {
            return $@"D:\SushiLogs\log {_currentTime.ToString("yyyymmdd")}_[{fileSize} byte].txt";
        }
    }
}
