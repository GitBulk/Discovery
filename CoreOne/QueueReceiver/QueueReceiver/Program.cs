using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using Tam.Core.RabbitMQ;

namespace QueueReceiver
{
    class Program
    {
        private static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            var factory = new ConnectionFactory()
            {
                //HostName = hostName
                Uri = new Uri(hostName).AbsoluteUri
            };
            return factory;
        }

        static void Main(string[] args)
        {
            string hostName = ConfigurationManager.AppSettings["QueueConnection"];
            string queueName = "hello";

            Console.WriteLine("I am listening");
            var factory = CreateConnectionFactory(hostName);
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += (model, ea) =>
                    //{
                    //    var body = ea.Body;
                    //    var message = Encoding.UTF8.GetString(body);
                    //    Console.WriteLine(" [x] Received {0}", message);
                    //};
                    consumer.Received += Consumer_Received;

                    channel.BasicConsume(queueName,
                                         noAck: true,
                                         consumer: consumer);


                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }


            //var queueManager = new QueueManager(hostName);
            ////queueManager.Receive(queueName);
            //queueManager.Receive(queueName, Consumer_Received);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }


        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);

            // do some thing with message
            Console.WriteLine(message);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
        }


    }

}
