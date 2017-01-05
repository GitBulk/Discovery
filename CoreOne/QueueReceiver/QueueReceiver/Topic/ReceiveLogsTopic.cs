using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Core.RabbitMQ;
using Tam.Core.Utilities;

namespace QueueReceiver.Topic
{
    class ReceiveLogsTopic
    {
        public static void Process(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Usage: {0} [binding_key...]",
                                Environment.GetCommandLineArgs()[0]);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }

            var factory = QueueManager.CreateConnectionFactory(QueueSettings.HostName);
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
                    var queueName = channel.QueueDeclare().QueueName;

                    foreach (var bindingKey in args)
                    {
                        channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: bindingKey);
                    }

                    Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");
                    var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += (model, ea) =>
                    //{

                    //};
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            //var message = Encoding.UTF8.GetString(body);
            string message = body.GetStringUtf8();
            string routingKey = e.RoutingKey;
            Console.WriteLine(" [x] Received '{0}':'{1}'",
                                  routingKey,
                                  message);
        }
    }
}
