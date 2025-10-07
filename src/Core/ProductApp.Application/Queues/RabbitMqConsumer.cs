using System.Text;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Queues;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductApp.Application.Queues;

public class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly IRabbitMqConnection _connectionFactory;
    private readonly IRabbitMqPublisher _rabbitMqPublisher;
    private readonly ILogger<RabbitMqConsumer> _logger;

    public RabbitMqConsumer(IRabbitMqConnection connectionFactory, IRabbitMqPublisher rabbitMqPublisher, ILogger<RabbitMqConsumer> logger)
    {
        _connectionFactory = connectionFactory;
        _rabbitMqPublisher = rabbitMqPublisher;
        _logger = logger;
    }

    public async Task ConsumeAsync(string queueName, string queueErrorName)
    {
        var channel = await _connectionFactory.CreateChannelAsync();
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation("Received message from queue {QueueName}: {Message}", queueErrorName, message);

            // TODO: İş mantığını burada çalıştır

            await _rabbitMqPublisher.PublishAsync(queueName, message);

            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync(queue: queueErrorName, autoAck: false, consumer: consumer);
        _logger.LogInformation("RabbitMQ consumer started for queue: {QueueName}", queueErrorName);
    }
}
