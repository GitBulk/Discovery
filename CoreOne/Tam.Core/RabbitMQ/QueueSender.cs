using RabbitMQ.Client;

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

        public void Process(string queueName, string message)
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
