namespace ProductApp.Application.Common;

public class AppSettings
{
    public RabbitMq? RabbitMq { get; set; }
    public Quartz? Quartz { get; set; }
    public MassTransit? MassTransit { get; set; }
}

public class RabbitMq
{
    public string? Host { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ProductQueueName { get; set; }
    public string? ProductQueueErrorName { get; set; }
}

public class Quartz
{
    public double StartTime { get; set; }
    public int Interval { get; set; }
    public int MaxConcurrency { get; set; }
    public string? JobName { get; set; }
    public string? TriggerName { get; set; }

}

public class MassTransit
{
    public int RetryCount { get; set; }
    public long Interval { get; set; }
}