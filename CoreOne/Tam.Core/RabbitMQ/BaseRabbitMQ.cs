using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.RabbitMQ
{
    public class BaseRabbitMQ : IDisposable
    {
        protected readonly IModel channel;
        public string HostName { get; set; }
        public BaseRabbitMQ(string hostName)
        {
            this.HostName = hostName;
            var factory = QueueManager.CreateConnectionFactory(hostName);
            var connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void SendAck(ulong deliveryTag)
        {
            this.channel.BasicAck(deliveryTag, multiple: false);
        }

        public void Dispose()
        {
            if (!this.channel.IsClosed)
            {
                this.channel.Close();
                this.channel.Dispose();
            }
        }
    }
}
