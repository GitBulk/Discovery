using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Queue.Logging;
using Tam.Queue.Serialization;

namespace Tam.Queue
{
    public class RabbitQueue : IDisposable
    {

        private readonly IModel channel;
        private readonly IConnection connection;
        private readonly string queueName;


        public RabbitQueue(string exchangeName, string queueName)
            : this("localhost", exchangeName, ExchangeType.Direct, queueName, true, true, true, false, "", null)
        {
        }


        public RabbitQueue(string host, string exchangeName, string exchangeType, string queueName) :
            this(host, exchangeName, exchangeType, queueName, true, true, true, false, "", null)
        {
        }

        public RabbitQueue(string host, string exchangeName, string exchangeType, string queueName, string routingKey) :
            this(host, exchangeName, exchangeType, queueName, true, true, true, false, routingKey, null)
        {
        }

        public RabbitQueue(string hostName, string exchangeName, string exchangeType, string queueName, bool durableExchange, bool autoDeleteExchange, bool durableQueue, bool autoDeleteQueue):
            this(hostName, exchangeName, exchangeType, queueName, durableExchange, autoDeleteExchange, durableQueue, autoDeleteQueue, string.Empty, null, null)
        {

        }

        public RabbitQueue(string hostName, string exchangeName, string exchangeType, string queueName, bool durableExchange, bool autoDeleteExchange, bool durableQueue, bool autoDeleteQueue, string routingKey,
                           IDictionary<string, object> headers) :
            this(hostName, exchangeName, exchangeType, queueName, durableExchange, autoDeleteExchange, durableQueue, autoDeleteQueue, routingKey, headers, null)
        {

        }

        public RabbitQueue(string hostName, string exchangeName, string exchangeType, string queueName, bool durableExchange, bool autoDeleteExchange, bool durableQueue, bool autoDeleteQueue, string routingKey, IDictionary<string, object> headers, IDictionary<string, object> queueProperties)
        {
            this.queueName = queueName;
            var factory = QueueManager.CreateConnectionFactory(hostName);
            this.connection =  factory.CreateConnection();
            this.channel = this.connection.CreateModel();
            if (exchangeName != string.Empty)
            {
                this.channel.ExchangeDeclare(exchangeName, exchangeType, durableExchange, autoDeleteExchange, null);
                this.channel.QueueDeclare(this.queueName, durableQueue, false, autoDeleteQueue, queueProperties);
                this.channel.QueueBind(queueName, exchangeName, routingKey, headers);
            }
            else
            {
                this.channel.QueueDeclare(this.queueName, durableQueue, false, autoDeleteQueue, null);
            }
        }

        public void GetMessage<TMessage>(BinarySerializationStrategy serializer, EventHandler<BasicDeliverEventArgs> callback) where TMessage : class
        {
            
            var consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += callback;
        }
        
        public void Dispose()
        {
            this.channel.Close();
            this.connection.Close();
        }
    }
}
