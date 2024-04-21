using Contracts.Common.Interfaces;
using Contracts.Messages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messages
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ISerializeService _serializeService;

        public RabbitMQProducer(ISerializeService serializeService)
        {
            _serializeService = serializeService;
        }

        public void SendMessage<T>(T message)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost", // product: Ip host
            };
            
            var connection = connectionFactory.CreateConnection();

            using var channel = connection.CreateModel(); // create a channel
            channel.QueueDeclare("orders", exclusive:false); // create a queue
            var jsondata = _serializeService.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsondata);
            
            channel.BasicPublish(exchange:"", routingKey:"orders", body: body);

        }
    }
}
