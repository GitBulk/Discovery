using System.Diagnostics;

namespace Tam.Queue.Logging
{
    public static class LoggerExtensions
    {
        public static void Write(this ILogger logger, string message, TraceEventType severity)
        {
            var entry = new LogEntry
            {
                Message = message,
                Severity = severity
            };
            logger.Write(entry);
        }
    }
}
