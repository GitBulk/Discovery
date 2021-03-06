﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Core.Utilities;

namespace Tam.Core.RabbitMQ
{
    public class Publisher
    {
        public string HostName { get; set; }

        public Publisher(string hostName)
        {
            this.HostName = hostName;
        }

        public void Fanout(string exchangeName, string message)
        {
            QueueManager.ExchangeMessage(this.HostName, exchangeName, ExchangeType.Fanout, routingKey: "", message: message, properties: null);
        }

        public void Direct(string exchangeName, string message, string routingKey)
        {   
            QueueManager.ExchangeMessage(this.HostName, exchangeName, ExchangeType.Direct, routingKey, message, properties: null);
        }

        public void Topic(string exchangeName, string message, string routingKey)
        {
            QueueManager.ExchangeMessage(this.HostName, exchangeName, ExchangeType.Topic, routingKey, message, null);
        }

        public void Send(string queueName, string message, bool durable = false, bool exclusive = false, bool autoDelete = false, string exchange = "",
            bool persistent = false)
        {
            Guard.ThrowIfNullOrWhiteSpace(message);

            var senderInfo = new SenderInfo()
            {
                AutoDelete = autoDelete,
                Durable = durable,
                Exchange = exchange,
                Exclusive = exclusive,
                Message = message,
                //Properties = properties,
                Persistent = persistent,
                QueueName = queueName
            };
            
            Send(senderInfo);
        }

        public void Send(SenderInfo sender)
        {
            Guard.ThrowIfNull(sender);
            var factory = QueueManager.CreateConnectionFactory(this.HostName);
            using (var connection = factory.CreateConnection())
            {
                // create channel in the TCP connection
                using (var channel = connection.CreateModel())
                {
                    // declare a queue with a given name
                    channel.QueueDeclare(queue: sender.QueueName,
                        durable: sender.Durable,
                        exclusive: sender.Exclusive,
                        autoDelete: sender.AutoDelete,
                        arguments: null);

                    IBasicProperties properties = null;
                    if (sender.Persistent)
                    {
                        properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                    }

                    // prepare data to publish
                    var body = Encoding.UTF8.GetBytes(sender.Message);

                    // publish
                    channel.BasicPublish(exchange: sender.Exchange, routingKey: sender.QueueName,
                        basicProperties: properties, body: body);
                }
            }
        }
    }
}
