namespace ProductApp.Application.Common;

public class AppSettings
{
    public RabbitMq? RabbitMq { get; set; }
}

public class RabbitMq
{
    public string? Host { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ProductQueueName { get; set; }
    public string? ProductQueueErrorName { get; set; }
}
