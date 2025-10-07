using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Queues;
using Quartz;

namespace ProductApp.Application.Jobs;

public class UpdateProductJob : IJob
{
    private readonly IRabbitMqConsumer _rabbitMqConsumer;
    private readonly ILogger<UpdateProductJob> _logger;
    private readonly AppSettings _settings;

    public UpdateProductJob(
        IRabbitMqConsumer rabbitMqConsumer,
        ILogger<UpdateProductJob> logger,
        IOptions<AppSettings> options)
    {
        _rabbitMqConsumer = rabbitMqConsumer;
        _logger = logger;
        _settings = options.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("UpdateProductJob started at {StartTime}", DateTime.UtcNow);

        var queueName = _settings?.RabbitMq?.ProductQueueName;
        var queueErrorName = _settings?.RabbitMq?.ProductQueueErrorName;

        if (string.IsNullOrWhiteSpace(queueName) || string.IsNullOrWhiteSpace(queueErrorName))
        {
            _logger.LogWarning("RabbitMQ queue name is missing in configuration.");
            return;
        }

        try
        {
            await _rabbitMqConsumer.ConsumeAsync(queueName, queueErrorName);
            _logger.LogInformation("UpdateProductJob completed successfully at {EndTime}", DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateProductJob failed at {ErrorTime}", DateTime.UtcNow);
            throw;
        }
    }
}
