using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.RabbitMQ
{
    public class BaseConsumer : DefaultBasicConsumer
    {
        private readonly BaseRabbitMQ rabbit;

        public BaseConsumer(BaseRabbitMQ rabbit)
        {
            this.rabbit = rabbit;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            base.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);
        }
    }
}
