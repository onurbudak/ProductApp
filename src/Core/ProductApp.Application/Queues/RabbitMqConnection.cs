using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Queues;
using RabbitMQ.Client;

namespace ProductApp.Application.Queues;

public class RabbitMqConnection : IRabbitMqConnection
{
    private readonly ILogger<RabbitMqConnection> _logger;
    private readonly AppSettings _settings;
    private IConnection? _connection;

    public RabbitMqConnection(ILogger<RabbitMqConnection> logger, IOptions<AppSettings> options)
    {
        _logger = logger;
        _settings = options.Value;
    }

    public async Task<IConnection> CreateConnectionAsync()
    {
        if (_connection is { IsOpen: true })
            return _connection;

        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings?.RabbitMq?.HostName ?? string.Empty,
                UserName = _settings?.RabbitMq?.UserName ?? string.Empty,
                Password = _settings?.RabbitMq?.Password ?? string.Empty
            };

            _connection = await factory.CreateConnectionAsync();
            _logger.LogInformation("RabbitMQ connection established to {HostName}", _settings?.RabbitMq?.HostName ?? string.Empty);

            return _connection;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RabbitMQ connection failed.");
            throw;
        }
    }

    public async Task<IChannel> CreateChannelAsync()
    {
        var connection = await CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        _logger.LogInformation("RabbitMQ channel created.");
        return channel;
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
