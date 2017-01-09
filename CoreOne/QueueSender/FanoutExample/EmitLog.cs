using System;
using Tam.Core.RabbitMQ;

namespace QueueSender.FanoutExample
{
    public static class EmitLog
    {
        public static void Process(string[] args)
        {
            Console.WriteLine("Fanout");
            var factory = QueueManager.CreateConnectionFactory(QueueSettings.HostName);
            string message = GetMessage(args);
            QueueManager.Publisher(QueueSettings.HostName).Fanout(exchangeName: "toan_fanout_ex", message: message);
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
