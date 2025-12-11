using System.Text;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Interfaces.Queues;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductApp.Application.Queues;

public class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly IRabbitMqConnection _connectionFactory;
    private readonly IRabbitMqPublisher _publisher;
    private readonly ILogger<RabbitMqConsumer> _logger;

    public RabbitMqConsumer(
        IRabbitMqConnection connectionFactory,
        IRabbitMqPublisher publisher,
        ILogger<RabbitMqConsumer> logger)
    {
        _connectionFactory = connectionFactory;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task ConsumeAsync(string queueName, string errorQueueName)
    {
        var channel = await _connectionFactory.CreateChannelAsync();

        // Consumer oluştur
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());

            _logger.LogInformation("Received message → Queue: {Queue}, Message: {Message}", queueName, message);

            try
            {
                // TODO: İş mantığı burada
                await _publisher.PublishAsync(queueName, message);

                // Ack → Mesaj başarıyla işlendi
                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);

                _logger.LogInformation("Message processed successfully → Queue: {Queue}", queueName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Message processing failed. Moving to error queue. Queue: {Queue}, Message: {Message}",
                    errorQueueName, message);
                throw;
            }
        };

        await channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer);

        _logger.LogInformation("RabbitMQ consumer started → Queue: {Queue}", queueName);
    }
}
