namespace ProductApp.Application.Interfaces.Queues;

public interface IRabbitMqConsumer
{
    Task ConsumeAsync(string queueName, string queueErrorName);
}
