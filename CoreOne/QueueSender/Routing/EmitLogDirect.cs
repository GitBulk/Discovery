using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Core.RabbitMQ;

namespace QueueSender.Routing
{
    public class EmitLogDirect
    {
        public static void Process(string[] args)
        {
            var factory = QueueManager.CreateConnectionFactory(QueueSettings.HostName);

            string message = GetMessage(args);
            string routingKey = (args.Length > 0 ? args[0] : "info");

            QueueManager.Publisher(QueueSettings.HostName).Direct(exchangeName: "direct_logs",
                message: message, routingKey: routingKey);

            Console.WriteLine(" [x] sent '{0}': '{1}'", routingKey, message);
            Console.WriteLine(" Press enter to exit");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return (args.Length > 1)
                          ? string.Join(" ", args.Skip(1).ToArray())
                          : "Hello World!";
        }
    }
}
