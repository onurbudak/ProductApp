using MassTransit;
using ProductApp.Persistence;
using ProductApp.Application;
using ProductApp.Application.Consumers;
using ProductApp.Application.Jobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var rabbitMqConfig = configuration.GetSection("RabbitMq");
string host = rabbitMqConfig["host"] ?? string.Empty;
string username = rabbitMqConfig["username"] ?? string.Empty;
string password = rabbitMqConfig["password"] ?? string.Empty;
string productQueueName = rabbitMqConfig["productQueueName"] ?? string.Empty;
string productQueueErrorName = rabbitMqConfig["productQueueErrorName"] ?? string.Empty;

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceRegistration(configuration);

builder.Services.AddMassTransit(x =>
{
    //Consumer'ı ekliyoruz
    x.AddConsumer<ProductMessageConsumer>();

    // RabbitMQ ile bağlantıyı kuruyoruz
    x.UsingRabbitMq((context, cfg) =>
    {
        // RabbitMQ sunucusuna bağlantı bilgilerini ekliyoruz
        cfg.Host(host, h =>
        {
            h.Username(username);
            h.Password(password);
        });

        //Consumer'ı dinlemek için endpoint ekliyoruz
        cfg.ReceiveEndpoint(productQueueName, e =>
        {
            e.ConfigureConsumer<ProductMessageConsumer>(context);
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(20)));
            e.UseInMemoryOutbox(context);
        });

        //cfg.ReceiveEndpoint(productQueueErrorName , e =>
        //{
        //    e.ConfigureConsumer<ProductMessageConsumer>(context);
        //    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(20)));
        //    e.UseInMemoryOutbox(context);
        //});

    });
});

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.SetMinimumLevel(LogLevel.Trace);
});

// Add Quartz and configure the job and scheduler
builder.Services.AddQuartz(q =>
{
    // Quartz logging configuration
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
    q.UseDefaultThreadPool(tp => tp.MaxConcurrency = 10);
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

IScheduler scheduler = await QuartzJobFactory<ProductMessageJob>.CreateJobAsync("ProductMessageJob", "Group1", "ProductMessageTrigger", "Group1", 30);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
