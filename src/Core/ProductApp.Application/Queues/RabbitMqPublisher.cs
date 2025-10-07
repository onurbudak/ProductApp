using System.Text;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Interfaces.Queues;
using RabbitMQ.Client;

namespace ProductApp.Application.Queues;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IRabbitMqConnection _connectionFactory;
    private readonly ILogger<RabbitMqPublisher> _logger;

    public RabbitMqPublisher(IRabbitMqConnection connectionFactory, ILogger<RabbitMqPublisher> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task PublishAsync(string queueName, string message)
    {
        using var channel = await _connectionFactory.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false);

        var body = Encoding.UTF8.GetBytes(message);
        var props = new BasicProperties { Persistent = true };

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            mandatory: true,
            basicProperties: props,
            body: body);

        _logger.LogInformation("Message published to queue {QueueName}: {Message}", queueName, message);
    }
}
