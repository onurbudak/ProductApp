namespace ProductApp.Application.Interfaces.Queues;

public interface IRabbitMqPublisher
{
    Task PublishAsync(string queueName, string message);
}
