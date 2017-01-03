using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tam.Core.RabbitMQ;

namespace QueueReceiver.FanoutExample
{
    public class Receiver
    {
        public static void Process()
        {
            var factory = QueueManager.CreateConnectionFactory (QueueSettings.HostName);
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "logs",
                        type: ExchangeType.Fanout);
                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName,
                        exchange: "logs", routingKey: "");
                    Console.WriteLine(" [*] waiting for logs");
                    var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += Consumer_Received;
                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] {0}", message);
                    };

                    channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);
                    Console.WriteLine(" Press [enter] to exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
