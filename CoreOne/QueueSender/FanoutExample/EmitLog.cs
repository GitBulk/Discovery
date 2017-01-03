using System;
using Tam.Core.RabbitMQ;

namespace QueueSender.FanoutExample
{
    public static class EmitLog
    {
        public static void Process(string[] args)
        {
            var factory = QueueManager.CreateConnectionFactory(QueueSettings.HostName);
            string message = GetMessage(args);
            QueueManager.Fanout(QueueSettings.HostName, exchangeName: "logs",
                message: message);
            Console.WriteLine(" [x] sent {0}", message);
            Console.WriteLine(" Press enter to exit");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0)
                   ? string.Join(" ", args)
                   : "info: Hello World!");
        }
    }
}
