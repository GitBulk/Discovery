using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using Tam.Core.Utilities;

namespace Tam.Core.RabbitMQ
{
    public class Consumer
    {
        public string HostName { get; set; }

        public Consumer(string hostName)
        {
            this.HostName = hostName;
        }

        public void Fanout(string exchangeName,
            EventHandler<BasicDeliverEventArgs> callback, bool noAck = true)
        {
            QueueManager.ReceiveExchangeMessage(this.HostName,
                exchangeName, ExchangeType.Fanout, routingKey: "", callback: callback, noAck: noAck);
        }

        public void Direct(string exchangeName, List<string> listRoutingKey,
            EventHandler<BasicDeliverEventArgs> callback, bool noAck = true)
        {
            QueueManager.ReceiveExchangeMessage(this.HostName,
                exchangeName, ExchangeType.Direct, listRoutingKey, callback: callback, noAck: noAck);
        }

        public void Direct(string exchangeName, string routingKey,
            EventHandler<BasicDeliverEventArgs> callback, bool noAck = true)
        {
            Guard.ThrowIfNullOrWhiteSpace(routingKey);
            QueueManager.ReceiveExchangeMessage(this.HostName,
                exchangeName, ExchangeType.Direct, routingKey: routingKey, callback: callback, noAck: noAck);
        }

        //public void Receive(string queueName, string message, EventHandler<BasicDeliverEventArgs> callback,
        //    bool durable = false, bool exclusive = false, bool autoDelete = false)
        //{
        //    Guard.ThrowIfNullOrWhiteSpace(message);
        //    var factory = QueueManager.CreateConnectionFactory(this.HostName);
        //    using (var connection = factory.CreateConnection())
        //    {
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);

        //            var consumer = new EventingBasicConsumer(channel);
        //            consumer.Received += callback;
        //        }
        //    }
        //}
    }
}
