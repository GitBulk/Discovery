using System.Diagnostics;

namespace Tam.Queue.Logging
{
    public class LogEntry
    {
        public string Message { get; set; }
        public TraceEventType Severity { get; set; }
        public LogEntry()
        {
            // default value
            this.Severity = TraceEventType.Information;
        }
    }
}
