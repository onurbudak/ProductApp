using System.Text;
using RabbitMQ.Client;

namespace ProductApp.Application.Queues;

public class RabbitMqFactory
{
    public static async Task ConnectionAsync()
    {
        ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();

        await CreateQueuePublishAsync(channel, "Hello RabbitMQ Async!");
    }

    public static async Task CreateQueuePublishAsync(IChannel channel, string message)
    {
        // Kuyruk oluşturuluyor
        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: true, arguments: null);

        // Gönderilecek mesaj
        byte[] body = Encoding.UTF8.GetBytes(message);

        BasicProperties basicProperties = new BasicProperties();

        // Asenkron mesaj gönderme işlemi
        await channel.BasicPublishAsync(exchange: "", routingKey: "hello", mandatory: true, basicProperties: basicProperties, body: body);

        Console.WriteLine($" [x] Sent {message}");
    }
}

