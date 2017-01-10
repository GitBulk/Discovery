using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tam.Queue.Logging
{
    public static class Logger
    {
        static ILogger CurrentLogger;
        public static ILogger Current
        {
            get
            {
                return (CurrentLogger ?? (CurrentLogger = new  NullLogger()));
            }
            set
            {
                CurrentLogger = value;
            }
        }

        public static void UseLogger(ILogger logger)
        {
            CurrentLogger = logger;
        }

        class NullLogger : ILogger
        {
            public void Write(LogEntry logEntry)
            {
            }
        }
    }
}
