namespace ProductApp.Application.Interfaces.Queues;

public interface IRabbitMqFactory
{
    Task PublishAsync(string hostName, string queue, string message);

    Task ConsumeAsync(string hostName, string queue);
}