using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Tam.Core.Utilities;

namespace Tam.Core.RabbitMQ
{
    public static class QueueManager
    {        
        public static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(hostName).AbsoluteUri
            };
            return factory;
        }

        public static void ExchangeMessage(ConnectionFactory factory, string exchange, string type, string routingKey, string message, IBasicProperties properties)
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
                    channel.BasicPublish(exchange: exchange, routingKey: routingKey,
                        basicProperties: properties, body: body);
                }
            }
        }

        public static void ExchangeMessage(string hostName, string exchange, string type, string routingKey, string message, IBasicProperties properties)
        {
            Guard.ThrowIfNullOrWhiteSpace(hostName);
            Guard.ThrowIfNullOrWhiteSpace(message);
            var factory = CreateConnectionFactory(hostName);
            ExchangeMessage(factory, exchange, type, routingKey, message, properties);
        }

        public static void ReceiveExchangeMessage(string hostName, string exchangeName, string type, string routingKey, EventHandler<BasicDeliverEventArgs> callback, bool noAck = true)
        {
            Guard.ThrowIfNullOrWhiteSpace(hostName);
            Guard.ThrowIfNullOrWhiteSpace(type);

            var binds = new List<string>()
            {
                routingKey
            };
            ReceiveExchangeMessage(hostName, exchangeName, type, binds, callback, noAck);
        }
        
        public static void ReceiveExchangeMessage(string hostName, string exchangeName, string type, List<string> routingKeys, EventHandler<BasicDeliverEventArgs> callback, bool noAck = true)
        {
            Guard.ThrowIfNullOrWhiteSpace(hostName);
            Guard.ThrowIfNullOrWhiteSpace(type);
            //Guard.ThrowIfNullOrEmpty(routingKeys);

            var factory = CreateConnectionFactory(hostName);
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchangeName,
                        type: type);
                    var queueName = channel.QueueDeclare().QueueName;

                    //channel.QueueBind(queueName, exchange: exchangeName, routingKey: routingKey);
                    foreach (var item in routingKeys)
                    {
                        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: item);
                    }

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += callback;
                    channel.BasicConsume(queue: queueName, noAck: noAck,
                        consumer: consumer);

                }
            }
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
