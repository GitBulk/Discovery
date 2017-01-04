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
        
        public static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(hostName).AbsoluteUri
            };
            return factory;
        }

        public static void ExchangeMessage(ConnectionFactory factory, string exchange, string type, string queueName, string message, IBasicProperties properties)
        {
            using (var connection = factory.CreateConnection())
            {
                // create channel in the TCP connection
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchange, type: type.ToLower());

                    // prepare data to publish
                    var body = Encoding.UTF8.GetBytes(message);

                    // publish
                    channel.BasicPublish(exchange: exchange, routingKey: queueName,
                        basicProperties: properties, body: body);
                }
            }
        }

        public static void ExchangeMessage(string hostName, string exchange, string type, string queueName, string message, IBasicProperties properties)
        {
            var factory = CreateConnectionFactory(hostName);
            ExchangeMessage(factory, exchange, type, queueName, message, properties);
        }               

        public static Consumer Consumer(string hostName)
        {
            return new Consumer(hostName);
        }

        public static Publisher Publisher(string hostName)
        {
            return new Publisher(hostName);
        }

        public static QueueSender CreateSender(string hostName)
        {
            var sender = new QueueSender(hostName);
            return sender;
        }
    }

}
