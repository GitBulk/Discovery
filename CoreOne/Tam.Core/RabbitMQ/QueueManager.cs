using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Tam.Core.Utilities;

namespace Tam.Core.RabbitMQ
{
    public class QueueManager
    {
        private readonly string hostName;
        public QueueManager(string hostName)
        {
            this.hostName = hostName;
        }

        public void Send(string queueName = "hello", string message = "I am Sieu Nhan Gao")
        {
            var factory = CreateConnectionFactory(hostName);
            using (var connection = factory.CreateConnection())
            {
                // create channel in the TCP connection
                using (var channel = connection.CreateModel())
                {
                    // declare a queue with a given name
                    channel.QueueDeclare(queue: queueName,
                        durable: false, exclusive: false, autoDelete: false, arguments: null);

                    // prepare data to publish
                    var body = Encoding.UTF8.GetBytes(message);

                    // publish
                    channel.BasicPublish(exchange: "", routingKey: queueName,
                        basicProperties: null, body: body);
                }
            }
        }

        public static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(hostName).AbsoluteUri
            };
            return factory;
        }

    }
}
