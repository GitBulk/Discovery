using RabbitMQ.Client;

namespace Tam.Queue
{
    public interface IConnectionFactory
    {
        ConnectionFactory Get(string uri);
    }
}
