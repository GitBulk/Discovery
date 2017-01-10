using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Queue.Serialization;

namespace Tam.Queue
{
    public class RabbitExchange : IDisposable
    {
        private readonly IModel channel;
        private readonly IConnection connection;
        private readonly string exchangeName;
        private static DateTime DefaultDate = new DateTime(1970, 1, 1, 0, 0, 0);

        public RabbitExchange(string exchangeName)
            : this("localhost", exchangeName, ExchangeType.Direct)
        {

        }

        public RabbitExchange(string hostName, string exchangeName, string exchangeType)
            : this(hostName, exchangeName, exchangeName, true, false)
        {

        }

        public RabbitExchange(string hostName, string exchangeName, string exchangeType, bool durable, bool autoDelete)
        {
            this.exchangeName = exchangeName;
            var factory = QueueManager.CreateConnectionFactory(hostName);
            this.connection = factory.CreateConnection();
            this.channel = this.connection.CreateModel();
            this.channel.ExchangeDeclare(this.exchangeName, exchangeType, durable, autoDelete, null);
        }

        private static AmqpTimestamp GetTimestamp()
        {
            return new AmqpTimestamp((long)(DateTime.Now - DefaultDate).TotalSeconds);
        }

        public void Pushlish<TMessage>(TMessage message, string routingKey)
        {
            Publish<TMessage>(message, routingKey, null, new BinarySerializationStrategy());
        }

        public void Publish<TMessage>(TMessage message, int ttl)
        {
            Publish<TMessage>(message, string.Empty, ttl, new BinarySerializationStrategy());
        }

        public void Publish<TMessage>(TMessage message, string routingKey, int? ttl, ISerializationStrategy serializer)
        {
            Console.WriteLine(
                string.Format("Publishing message to exchange:\'{0}\' routing key:\'{1}\'", this.exchangeName, routingKey),
                TraceEventType.Information);
            byte[] body = serializer.Serialize(message);
            var props = new BasicProperties();
            if (ttl.HasValue)
            {
                props.Expiration = ttl.Value.ToString();
            }
            props.Timestamp = GetTimestamp();
            this.channel.BasicPublish(this.exchangeName, routingKey, props, body);
        }

        public void Publish<TMessage>(TMessage message, IDictionary<string, object> headers)
        {
            var props = new BasicProperties();
            props.Headers = headers;
            props.Timestamp = GetTimestamp();
            var serializer = new BinarySerializationStrategy();
            byte[] body = serializer.Serialize(message);
            this.channel.BasicPublish(this.exchangeName, string.Empty, props, body);
        }

        public void Dispose()
        {
            this.channel.Close();
            this.connection.Close();
        }

        public RabbitExchange Delete()
        {
            return Delete(true);
        }

        public RabbitExchange Delete(bool ifUnused)
        {
            try
            {
                Console.WriteLine("Deleting exchange: " + this.exchangeName);
                this.channel.ExchangeDelete(this.exchangeName, ifUnused);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return this;
        }
    }
}
