using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Tam.Core.RabbitMQ;

namespace QueueReceiver
{
    class Program
    {
        static string hostName = ConfigurationManager.AppSettings["QueueConnection"];
        const string queueName = "hello";
        private static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            var factory = new ConnectionFactory()
            {
                //HostName = hostName
                Uri = new Uri(hostName).AbsoluteUri
            };
            return factory;
        }

        static void Main(string[] args)
        {
            //WorkQueueEx2();
            //FanoutExample.Receiver.Process();
            Routing.ReceiveLogsDirect.Process(args);
        }

        private static void WorkQueueEx2()
        {
            Console.WriteLine("I am listening");
            var factory = CreateConnectionFactory(hostName);
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    int dots = message.Split('.').Length - 1;
                    Thread.Sleep(dots * 1000);

                    Console.WriteLine(" [x] Done");
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "hello",
                                     noAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }

}
