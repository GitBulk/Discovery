using RabbitMQ.Client;
using System;

namespace Tam.Queue
{
    public static class QueueManager
    {
        public static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(hostName).AbsoluteUri
            };
            return factory;
        }
    }
}
