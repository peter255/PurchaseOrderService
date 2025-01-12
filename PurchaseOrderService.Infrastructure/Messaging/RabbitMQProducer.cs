using System.Text;
using Newtonsoft.Json;
using PurchaseOrderService.Domain.Interfaces;
using RabbitMQ.Client;

namespace PurchaseOrderService.Infrastructure.Messaging
{
    public class RabbitMQProducer : IMessageProducer
    {
        public async Task SendMessageAsync<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            var queue = await channel.QueueDeclareAsync("shipingOrders");

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: "", routingKey: "shipingOrders", body: body);
        }
    }
}
