using System.Configuration;

namespace QueueSender
{
    public class QueueSettings
    {
        public static string HostName = ConfigurationManager.AppSettings["QueueConnection"];
        public const string QueueName = "hello";
    }
}
