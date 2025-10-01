using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Queues;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductApp.Application.Queues;

public class RabbitMqFactory : IRabbitMqFactory
{
    private readonly ILogger<RabbitMqFactory> _logger;
    private readonly AppSettings _settings;

    public RabbitMqFactory(ILogger<RabbitMqFactory> logger, IOptions<AppSettings> options)
    {
        _logger = logger;
        _settings = options.Value;
    }

    private async Task<IChannel> ConnectionAsync(string hostName)
    {
        Console.WriteLine("ConnectionAsync Started");
        _logger.LogInformation("ConnectionAsync Started");

        ConnectionFactory factory = new ConnectionFactory() { HostName = hostName };
        IConnection connection = await factory.CreateConnectionAsync();
        IChannel channel = await connection.CreateChannelAsync();

        Console.WriteLine("ConnectionAsync Finished");
        _logger.LogInformation("ConnectionAsync Finished");

        return channel;
    }

    public async Task PublishAsync(string hostName, string queue, string message)
    {
        Console.WriteLine($"PublishAsync Started ...");
        _logger.LogInformation("PublishAsync Started ...");

        IChannel channel = await ConnectionAsync(hostName);
        await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        byte[] body = Encoding.UTF8.GetBytes(message);
        BasicProperties basicProperties = new BasicProperties();
        await channel.BasicPublishAsync(exchange: "", routingKey: queue, mandatory: true, basicProperties: basicProperties, body: body);

        Console.WriteLine($"Publish Message : {message}");
        _logger.LogInformation("Publish Message : {Message}", message);
        Console.WriteLine("PublishAsync Finished");
        _logger.LogInformation("PublishAsync Finished");

    }

    public async Task ConsumeAsync(string hostName, string queue)
    {
        Console.WriteLine("ConsumeAsync Started");
        _logger.LogInformation("ConsumeAsync Started");

        IChannel channel = await ConnectionAsync(hostName);
        var consumer = new AsyncMessageConsumer(channel, _logger, _settings);
        await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);

        Console.WriteLine("ConsumeAsync Finished");
        _logger.LogInformation("ConsumeAsync Finished");
    }

    private class AsyncMessageConsumer : IAsyncBasicConsumer
    {
        public IChannel Channel { get; set; }
        private readonly ILogger<RabbitMqFactory> _logger;
        private readonly AppSettings _settings;

        public AsyncMessageConsumer(IChannel channel, ILogger<RabbitMqFactory> logger, AppSettings appSettings)
        {
            Channel = channel;
            _logger = logger;
            _settings = appSettings;
        }

        public async Task HandleBasicDeliverAsync(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IReadOnlyBasicProperties properties, ReadOnlyMemory<byte> body, CancellationToken cancellationToken = default)
        {
            var message = Encoding.UTF8.GetString(body.Span);

            Console.WriteLine("Received Message: {message}");
            _logger.LogInformation("Received Message: {Message}", message);

            BasicProperties basicProperties = new BasicProperties();
            await Channel.BasicPublishAsync(exchange: "", routingKey: _settings?.RabbitMq?.ProductQueueName ?? string.Empty, mandatory: true, basicProperties: basicProperties, body: body, cancellationToken);

            await Channel.BasicAckAsync(deliveryTag, false, cancellationToken);
        }

        public Task HandleBasicConsumeOkAsync(string consumerTag, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Consumer started with tag: {consumerTag}");
            _logger.LogInformation("Consumer started with tag: {ConsumerTag}", consumerTag);

            return Task.CompletedTask;
        }

        public Task HandleBasicCancelAsync(string consumerTag, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Consumer canceled with tag: {consumerTag}");
            _logger.LogInformation("Consumer canceled with tag: {ConsumerTag}", consumerTag);

            return Task.CompletedTask;
        }

        public Task HandleBasicCancelOkAsync(string consumerTag, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Consumer cancel acknowledged with tag: {consumerTag}");
            _logger.LogInformation("Consumer cancel acknowledged with tag: {ConsumerTag}", consumerTag);

            return Task.CompletedTask;
        }

        public Task HandleChannelShutdownAsync(object channel, ShutdownEventArgs reason)
        {
            Console.WriteLine("Consumer Shutdown");
            _logger.LogInformation("Consumer Shutdown");

            return Task.CompletedTask;
        }
    }
}

