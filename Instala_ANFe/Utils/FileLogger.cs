using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Utils
{
    public class FileLogger : ILogger
    {
        private readonly string _logPath;
        
        public FileLogger()
        {
            _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Logs", $"log_{DateTime.Now:ddMMyyyyHHmmss}.txt");
            
            var dir = Path.GetDirectoryName(_logPath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        public void Error(string message)
        {
            Write("ERROR", message);
        }

        public void Error(string message, Exception ex)
        {
            Write("ERROR", $"{message} | Exception: {ex}");
        }

        public void Info(string message)
        {
            Write("INFO", message);
        }

        private void Write(string level, string message)
        {
            string log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            File.AppendAllText(_logPath, log + Environment.NewLine);
        }
    }
}
