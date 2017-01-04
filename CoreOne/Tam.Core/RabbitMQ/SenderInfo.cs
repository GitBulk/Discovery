using RabbitMQ.Client;

namespace Tam.Core.RabbitMQ
{

    public class SenderInfo
    {
        public string QueueName { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public string Message { get; set; }
        //public IBasicProperties Properties { get; set; }
        public string Exchange { get; set; } = "";
        public bool Persistent { get; set; }
    }
}
