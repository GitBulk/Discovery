using System.Configuration;

namespace QueueReceiver
{
    public class QueueSettings
    {
        public static string HostName = ConfigurationManager.AppSettings["QueueConnection"];
        public const string QueueName = "hello";
    }
}
