using RabbitMQ.Client;

namespace Tam.Queue
{
    public class ConnectionFactoryCreator : IConnectionFactory
    {
        public ConnectionFactory Get(string uri)
        {
            return new ConnectionFactory
            {
                Uri = uri
            };
        }
    }
}
