using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Queues;
using Quartz;
using System.Text;

namespace ProductApp.Application.Jobs;

public class UpdateProductJob : IJob
{
    private readonly IRabbitMqConnection _connectionFactory;
    private readonly IRabbitMqPublisher _publisher;
    private readonly ILogger<UpdateProductJob> _logger;
    private readonly AppSettings _settings;

    public UpdateProductJob(
        IRabbitMqConnection connectionFactory,
        IRabbitMqPublisher publisher,
        ILogger<UpdateProductJob> logger,
        IOptions<AppSettings> options)
    {
        _connectionFactory = connectionFactory;
        _publisher = publisher;
        _logger = logger;
        _settings = options.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("UpdateProductJob started at {Time}", DateTime.UtcNow);

        var productQueue = _settings?.RabbitMq?.ProductQueueName;
        var productQueueError = _settings?.RabbitMq?.ProductQueueErrorName;

        if (string.IsNullOrEmpty(productQueue) || string.IsNullOrEmpty(productQueueError))
        {
            _logger.LogWarning("UpdateProductJob queue names missing.");
            return;
        }

        var channel = await _connectionFactory.CreateChannelAsync();

        while (true)
        {
            // Hata kuyruğundan 1 adet mesaj çekilir
            var result = await channel.BasicGetAsync(productQueueError, autoAck: false);

            // Kuyruk boşsa dur
            if (result == null) break;

            var message = Encoding.UTF8.GetString(result.Body.ToArray());
            _logger.LogInformation("Retrying message: {Message}", message);

            try
            {
                // Mesajı tekrar normal kuyruğa bas
                await _publisher.PublishAsync(productQueue, message);

                // Error kuyruğundaki mesajı sil
                await channel.BasicAckAsync(result.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Retry failed for message: {Message}", message);

                // Silme, ileride yeniden retry edilsin
                await channel.BasicNackAsync(result.DeliveryTag, false, true);
                break;
            }
        }

        _logger.LogInformation("UpdateProductJob finished at {Time}", DateTime.UtcNow);
    }
}
