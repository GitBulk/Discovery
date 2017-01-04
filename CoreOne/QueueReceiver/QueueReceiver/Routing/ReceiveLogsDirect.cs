using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Core.RabbitMQ;

namespace QueueReceiver.Routing
{
    public class ReceiveLogsDirect
    {
        public static void Process(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                        Environment.GetCommandLineArgs()[0]);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }

            //var binds = new List<QueueBindInfo>();

            //foreach (var level in args)
            //{
            //    binds.Add(new QueueBindInfo
            //    {
            //        ExchangeName = "direct_logs",
            //        RoutingKey = level
            //    });
            //}

            var binds = new List<string>();

            foreach (var level in args)
            {
                binds.Add(level);
            }

            QueueManager.Consumer(QueueSettings.HostName).Direct("direct_logs", binds, (sender, ea) =>
            {
                var body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                Console.WriteLine(" [x] Received '{0}':'{1}'",
                                  routingKey, message);
            });
            
            Console.WriteLine(" Press [enter] to exit");
            Console.ReadLine();
        }
    }
}
