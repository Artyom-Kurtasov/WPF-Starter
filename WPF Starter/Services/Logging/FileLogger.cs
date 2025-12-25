using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace WPF_Starter.Services.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath = "log.txt";

        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel >= LogLevel.Warning) return true;
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            string message = $"{DateTime.Now} [{logLevel}] {formatter(state, exception)}";
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
    }
}
