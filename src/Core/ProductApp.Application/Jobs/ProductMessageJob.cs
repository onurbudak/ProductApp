using Microsoft.Extensions.Logging;
using ProductApp.Application.Queues;
using Quartz;

namespace ProductApp.Application.Jobs;

public class ProductMessageJob : IJob
{
    private readonly IRabbitMqFactory _rabbitMqFactory;
    private readonly ILogger<ProductMessageJob> _logger;

    public ProductMessageJob(IRabbitMqFactory rabbitMqFactory, ILogger<ProductMessageJob> logger)
    {
        _logger = logger;
        _rabbitMqFactory = rabbitMqFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation("ProductMessageJob started");
            await _rabbitMqFactory.ConsumeAsync("localhost", "product_queue_error");
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
            _logger.LogError(ex, "Error ProductMessageJob");
            //throw new Exception("An error occurred during ProductMessageJob execution.");
        }
    }
}