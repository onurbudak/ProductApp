using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductApp.Application.Common;
using ProductApp.Application.Queues;
using Quartz;

namespace ProductApp.Application.Jobs;

public class ProductMessageJob : IJob
{
    private readonly IRabbitMqFactory _rabbitMqFactory;
    private readonly ILogger<ProductMessageJob> _logger;
    private readonly AppSettings _settings;


    public ProductMessageJob(IRabbitMqFactory rabbitMqFactory, ILogger<ProductMessageJob> logger, IOptions<AppSettings> options)
    {
        _rabbitMqFactory = rabbitMqFactory;
        _logger = logger;
        _settings = options.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            Console.WriteLine($"ProductMessageJob Started");
            _logger.LogInformation("ProductMessageJob Started");
            await _rabbitMqFactory.ConsumeAsync(_settings?.RabbitMq?.Host ?? string.Empty, _settings?.RabbitMq?.ProductQueueErrorName ?? string.Empty);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ProductMessageJob Error : {ex}");
            _logger.LogError(ex, "ProductMessageJob Error");
            throw new Exception(ex.Message, ex);
        }
    }
}