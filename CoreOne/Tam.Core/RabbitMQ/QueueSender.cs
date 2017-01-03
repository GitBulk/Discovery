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
        private bool autoDelete;
        private bool exclusive;
        public string HostName { get; set; }
        private string exchange;
        public QueueSender(string hostName)
        {
            this.HostName = hostName;
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

        public QueueSender AutoDelete()
        {
            this.autoDelete = true;
            return this;
        }

        public QueueSender Exclusive()
        {
            this.exclusive = true;
            return this;
        }

        public QueueSender Exchange(string value)
        {
            this.exchange = value;
            return this;
        }

        public static void Fanout(string hostName, string exchange, string message)
        {
            QueueManager.ExchangeMessage(hostName, exchange, ExchangeType.Fanout, queueName: "", message: message, properties: null);
        }

        //public void Send(string queueName = "hello", string message = "I am Sieu Nhan Gao")
        //{
        //    using (var connection = this.factory.CreateConnection())
        //    {
        //        // create channel in the TCP connection
        //        using (this.channel = connection.CreateModel())
        //        {
        //            // declare a queue with a given name
        //            channel.QueueDeclare(queue: queueName,
        //                durable: this.durable,
        //                exclusive: this.exclusive,
        //                autoDelete: this.autoDelete,
        //                arguments: null);

        //            // prepare data to publish
        //            var body = Encoding.UTF8.GetBytes(message);

        //            // publish
        //            channel.BasicPublish(exchange: "", routingKey: queueName,
        //                basicProperties: this.properties, body: body);
        //        }
        //    }
        //}


        public void SendMessage(string queueName, string message)
        {
            QueueManager.SendMessage(this.HostName, new SenderInfo()
            {
                AutoDelete = this.autoDelete,
                Durable = this.durable,
                Exchange = this.exchange,
                Exclusive = this.exclusive,
                Message = message,
                Properties = this.properties,
                QueueName = queueName
            });
        }

    }

}
