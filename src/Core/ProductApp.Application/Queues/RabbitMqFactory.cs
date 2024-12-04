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

    public async Task PublishAsync(string queue, string message)
    {
        Console.WriteLine($"PublishAsync Started ...");

        IChannel channel = await ConnectionAsync("localhost");

        // Kuyruk oluşturuluyor
        await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

        // Gönderilecek mesaj
        byte[] body = Encoding.UTF8.GetBytes(message);

        BasicProperties basicProperties = new BasicProperties();

        // Asenkron mesaj gönderme işlemi
        await channel.BasicPublishAsync(exchange: "", routingKey: queue, mandatory: true, basicProperties: basicProperties, body: body);

        Console.WriteLine($"Publish Message : {message}");

        Console.WriteLine($"PublishAsync Finished ...");

    }

    public async Task ConsumeAsync(string queue)
    {
        Console.WriteLine($"ConsumeAsync Started ...");

        IChannel channel = await ConnectionAsync("localhost");

        var consumer = new AsyncMessageConsumer(channel);

        // Kuyruktan mesaj alıyoruz
        await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);

        Console.WriteLine($"ConsumeAsync Finished ...");
    }

    public class AsyncMessageConsumer : IAsyncBasicConsumer
    {
        private readonly IChannel channel;

        public IChannel Channel { get; set; }

        public AsyncMessageConsumer(IChannel channel)
        {
            this.channel = channel;
        }

        // Mesaj alındığında çalışacak metot
        public async Task HandleBasicDeliverAsync(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IReadOnlyBasicProperties properties, ReadOnlyMemory<byte> body, CancellationToken cancellationToken = default)
        {
            var message = Encoding.UTF8.GetString(body.Span);
            Console.WriteLine($"Received Message: {message}");

            // Burada mesajı işleyebilirsiniz.
            // Örnek: Asenkron bir işlem başlatabilirsiniz (veritabanı işlemleri vs.)

            // Mesaj işlendikten sonra onay (acknowledge) göndermek
            await channel.BasicAckAsync(deliveryTag, false);
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

