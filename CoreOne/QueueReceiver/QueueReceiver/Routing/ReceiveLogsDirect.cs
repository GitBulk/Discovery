using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
            //if (args.Length < 1)
            //{
            //    Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
            //                            Environment.GetCommandLineArgs()[0]);
            //    Console.WriteLine(" Press [enter] to exit.");
            //    Console.ReadLine();
            //    Environment.ExitCode = 1;
            //    return;
            //}

            ////var binds = new List<QueueBindInfo>();

            //foreach (var level in args)
            //{
            //    binds.Add(new QueueBindInfo
            //    {
            //        ExchangeName = "direct_logs",
            //        RoutingKey = level
            //    });
            //}

            //var binds = new List<string>();

            //foreach (var level in args)
            //{
            //    binds.Add(level);
            //}

            //QueueManager.Consumer(QueueSettings.HostName).Direct("direct_logs", binds, (sender, ea) =>
            //{
            //    var body = ea.Body;
            //    string message = Encoding.UTF8.GetString(body);
            //    var routingKey = ea.RoutingKey;
            //    Console.WriteLine(" [x] Received '{0}':'{1}'",
            //                      routingKey, message);
            //});

            //Console.WriteLine(" Press [enter] to exit");
            //Console.ReadLine();

            // or

            var factory = QueueManager.CreateConnectionFactory(QueueSettings.HostName);
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            {
                channel.ExchangeDeclare(exchange: "direct_logs",
                                        type: "direct");
                var queueName = channel.QueueDeclare().QueueName;

                if (args.Length < 1)
                {
                    Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                            Environment.GetCommandLineArgs()[0]);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                    Environment.ExitCode = 1;
                    return;
                }

                foreach (var severity in args)
                {
                    channel.QueueBind(queue: queueName,
                                      exchange: "direct_logs",
                                      routingKey: severity);
                }

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" [x] Received '{0}':'{1}'",
                                      routingKey, message);
                };
                channel.BasicConsume(queue: queueName,
                                     noAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
