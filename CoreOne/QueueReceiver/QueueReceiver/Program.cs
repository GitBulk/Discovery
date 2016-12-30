using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using Tam.Core.RabbitMQ;

namespace QueueReceiver
{
    class Program
    {
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
            string hostName = ConfigurationManager.AppSettings["QueueConnection"];
            string queueName = "hello";

            Console.WriteLine("I am listening");
            var factory = CreateConnectionFactory(hostName);
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello",
                                     noAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

    }

}
