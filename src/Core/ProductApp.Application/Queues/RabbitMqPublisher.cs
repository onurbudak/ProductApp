using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Queues;
using RabbitMQ.Client;
using System.Text;

namespace ProductApp.Application.Queues;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IRabbitMqConnection _connectionFactory;
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly AppSettings? _settings;

    public RabbitMqPublisher(
        IRabbitMqConnection connectionFactory,
        ILogger<RabbitMqPublisher> logger,
        IOptions<AppSettings> options)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        _settings = options.Value;
    }

    public async Task PublishAsync(string queueName, string message)
    {
        try
        {
            using var channel = await _connectionFactory.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);

            var props = new BasicProperties
            {
                Persistent = true 
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: true,
                basicProperties: props,
                body: body);

            _logger.LogInformation("Mesaj başarıyla gönderildi → Queue: {QueueName}", queueName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RabbitMQ publish işlemi başarısız oldu. Queue: {QueueName}", queueName);
            throw; 
        }
    }
}
