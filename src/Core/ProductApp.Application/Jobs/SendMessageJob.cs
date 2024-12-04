using ProductApp.Application.Queues;
using Quartz;

namespace ProductApp.Application.Jobs;

public class SendMessageJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        //await new RabbitMqFactory().PublishAsync("hello", "Hello RabbitMQ Async! Test");

        //await new RabbitMqFactory().ConsumeAsync("hello");

        await new RabbitMqFactory().ConsumeAsync("product_queue_error");
    }
}