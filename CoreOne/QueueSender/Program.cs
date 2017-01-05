using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Text;
using Tam.Core.RabbitMQ;

namespace QueueSender
{
    class Program
    {


        private static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            string uri = new Uri(hostName).AbsoluteUri;
            var factory = new ConnectionFactory()
            {
                //HostName = hostName
                Uri = new Uri(hostName).AbsoluteUri
            };
            return factory;
        }

        static void Main(string[] args)
        {
            //WorkQueueEx2(args);
            //FanoutExample.EmitLog.Process(args);
            //Routing.EmitLogDirect.Process(args);
            Topic.EmitLogsTopic.Process(args);
        }

        private static void WorkQueueEx2(string[] args)
        {
            string hostName = ConfigurationManager.AppSettings["QueueConnection"];
            var factory = CreateConnectionFactory(hostName);
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: properties,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }

    }
}
