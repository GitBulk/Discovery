using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

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
            var factory = QueueManager.CreateConnectionFactory(this.HostName);
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchangeName,
                        type: ExchangeType.Fanout);

                    // random a queue name
                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += callback;
                    channel.BasicConsume(queue: queueName, noAck: noAck, consumer: consumer);
                }
            }
        }
    }
}
