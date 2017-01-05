using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Core.RabbitMQ;

namespace QueueSender.RPC
{
    class RPCSender
    {
        private IConnection connection;
        private IModel channel;
        private string replyQueuName;
        private QueueingBasicConsumer consumer;

        public RPCSender()
        {
            var factory = QueueManager.CreateConnectionFactory(QueueSettings.HostName);
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            this.replyQueuName = this.channel.QueueDeclare().QueueName;
            this.consumer = new QueueingBasicConsumer(this.channel);
            this.channel.BasicConsume(queue: replyQueuName, noAck: true, consumer: this.consumer);
        }

        public  string Call(string message)
        {
            var corrId = Guid.NewGuid().ToString();
            var props = this.channel.CreateBasicProperties();
            props.ReplyTo = this.replyQueuName;
            props.CorrelationId = corrId;

            var messageBytes = Encoding.UTF8.GetBytes(message);
            this.channel.BasicPublish(exchange: "", routingKey: "rpc_queue", basicProperties: props, body: messageBytes);
            while (true)
            {
                var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
            }
        }
    }
}
