using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductApp.Application.Queues;

public class RabbitMqFactory
{
    private async Task<IChannel> ConnectionAsync(string hostName)
    {
        Console.WriteLine($"ConnectionAsync Started ...");

        ConnectionFactory factory = new ConnectionFactory() { HostName = hostName };
        IConnection connection = await factory.CreateConnectionAsync();
        IChannel channel = await connection.CreateChannelAsync();

        Console.WriteLine($"ConnectionAsync Finished ...");

        return channel;
    }

    public async Task PublishAsync(string hostName, string queue, string message)
    {
        Console.WriteLine($"PublishAsync Started ...");

        IChannel channel = await ConnectionAsync(hostName);
        await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        byte[] body = Encoding.UTF8.GetBytes(message);
        BasicProperties basicProperties = new BasicProperties();
        await channel.BasicPublishAsync(exchange: "", routingKey: queue, mandatory: true, basicProperties: basicProperties, body: body);

        Console.WriteLine($"Publish Message : {message}");
        Console.WriteLine($"PublishAsync Finished ...");

    }

    public async Task ConsumeAsync(string hostName, string queue)
    {
        Console.WriteLine($"ConsumeAsync Started ...");

        IChannel channel = await ConnectionAsync(hostName);
        var consumer = new AsyncMessageConsumer(channel);
        await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);

        Console.WriteLine($"ConsumeAsync Finished ...");
    }

    private class AsyncMessageConsumer : IAsyncBasicConsumer
    {
        public IChannel Channel { get; set; }

        public AsyncMessageConsumer(IChannel channel)
        {
            Channel = channel;
        }

        public async Task HandleBasicDeliverAsync(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IReadOnlyBasicProperties properties, ReadOnlyMemory<byte> body, CancellationToken cancellationToken = default)
        {
            var message = Encoding.UTF8.GetString(body.Span);

            Console.WriteLine($"Received Message: {message}");

            //await new RabbitMqFactory().PublishAsync("localhost", "product_queue", message);

            await Channel.BasicAckAsync(deliveryTag, false);
        }

        public Task HandleBasicConsumeOkAsync(string consumerTag, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Consumer started with tag: {consumerTag}");
            return Task.CompletedTask;
        }

        public Task HandleBasicCancelAsync(string consumerTag, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Consumer canceled with tag: {consumerTag}");
            return Task.CompletedTask;
        }

        public Task HandleBasicCancelOkAsync(string consumerTag, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Consumer cancel acknowledged with tag: {consumerTag}");
            return Task.CompletedTask;
        }

        public Task HandleChannelShutdownAsync(object channel, ShutdownEventArgs reason)
        {
            Console.WriteLine($"Consumer Shutdown ...");
            return Task.CompletedTask;
        }
    }
}

