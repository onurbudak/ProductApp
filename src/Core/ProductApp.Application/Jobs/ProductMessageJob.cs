using Microsoft.Extensions.Logging;
using ProductApp.Application.Queues;
using Quartz;

namespace ProductApp.Application.Jobs;

public class ProductMessageJob : IJob
{
    //private readonly ILogger<ProductMessageJob> _logger;

    //public ProductMessageJob(ILogger<ProductMessageJob> logger)
    //{
    //    _logger = logger;
    //}

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            //_logger.LogInformation("Job started");
            // Your job logic goes here

            await new RabbitMqFactory().ConsumeAsync("localhost", "product_queue_error");

            //throw new Exception("An error occurred during job execution.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            //_logger.LogError(ex, "Error in MyJob");
        }
    }
}