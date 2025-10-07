using RabbitMQ.Client;

namespace ProductApp.Application.Interfaces.Queues;

public interface IRabbitMqConnection : IDisposable
{
    Task<IConnection> CreateConnectionAsync();
    Task<IChannel> CreateChannelAsync();
}