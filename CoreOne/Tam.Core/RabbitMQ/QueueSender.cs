using RabbitMQ.Client;
using System.Text;

namespace Tam.Core.RabbitMQ
{
    public class QueueSender
    {
        private readonly ConnectionFactory factory;
        private bool durable;
        private IModel channel;
        private IBasicProperties properties;
        public QueueSender(string hostName)
        {
            this.factory = QueueManager.CreateConnectionFactory(hostName);
        }

        public QueueSender Durable()
        {
            this.durable = true;
            return this;
        }

        public QueueSender SetDurable(bool value)
        {
            this.durable = value;
            return this;
        }

        public QueueSender Persitent()
        {
            Durable();
            this.properties = channel.CreateBasicProperties();
            this.properties.Persistent = true;
            return this;
        }

        public void Send(string queueName = "hello", string message = "I am Sieu Nhan Gao")
        {
            using (var connection = factory.CreateConnection())
            {
                // create channel in the TCP connection
                using (this.channel = connection.CreateModel())
                {
                    // declare a queue with a given name
                    channel.QueueDeclare(queue: queueName,
                        durable: this.durable, exclusive: false, autoDelete: false, arguments: null);

                    // prepare data to publish
                    var body = Encoding.UTF8.GetBytes(message);

                    // publish
                    channel.BasicPublish(exchange: "", routingKey: queueName,
                        basicProperties: this.properties, body: body);
                }
            }
        }

    }

}
