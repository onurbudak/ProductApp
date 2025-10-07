using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Queues;
using RabbitMQ.Client;

namespace ProductApp.Application.Queues;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IRabbitMqConnection _connectionFactory;
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly AppSettings? _settings;

    public RabbitMqPublisher(IRabbitMqConnection connectionFactory, ILogger<RabbitMqPublisher> logger, IOptions<AppSettings> options)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        _settings = options.Value;

        // Exponential backoff ile retry policy oluşturuluyor
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: _settings?.RabbitMq?.RetryCount ?? 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // 2, 4, 8, 16, 32 sn
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogWarning(
                        exception,
                        "RabbitMQ publish denemesi {RetryAttempt} başarısız oldu. {Delay} saniye sonra tekrar denenecek.",
                        retryCount,
                        timeSpan.TotalSeconds);
                });
    }

    public async Task PublishAsync(string queueName, string message)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            using var channel = await _connectionFactory.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);
            var props = new BasicProperties { Persistent = true };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: true,
                basicProperties: props,
                body: body);

            _logger.LogInformation("Mesaj başarıyla kuyruğa gönderildi: {QueueName}", queueName);
        });
    }
}
