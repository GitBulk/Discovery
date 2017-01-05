﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Tam.Core.RabbitMQ;

namespace QueueReceiver.FanoutExample
{
    public class Receiver
    {
        public static void Process()
        {
            var factory = QueueManager.CreateConnectionFactory(QueueSettings.HostName);
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "toan_fanout",
                        type: ExchangeType.Fanout);
                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName,
                        exchange: "toan_fanout", routingKey: "");
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

            // or

            //Console.WriteLine(" [*] waiting for logs");

            //QueueManager.Consumer(QueueSettings.HostName).Fanout("toan_fanout", (sender, ea) =>
            //{
            //    var body = ea.Body;
            //    var message = Encoding.UTF8.GetString(body);
            //    Console.WriteLine(" [x] {0}", message);
            //});
            //Console.WriteLine(" Press [enter] to exit");
            //Console.ReadLine();
        }
    }
}
