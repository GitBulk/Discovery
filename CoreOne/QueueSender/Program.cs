﻿using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Text;
using Tam.Core.RabbitMQ;

namespace QueueSender
{
    class Program
    {


        private static ConnectionFactory CreateConnectionFactory(string hostName)
        {
            string uri = new Uri(hostName).AbsoluteUri;
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
            var factory = CreateConnectionFactory(hostName);
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }


    }
}
