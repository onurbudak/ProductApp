using MassTransit;
using ProductApp.Application;
using ProductApp.Application.Common;
using ProductApp.Application.Consumers;
using ProductApp.Application.Interfaces.Queues;
using ProductApp.Application.Jobs;
using ProductApp.Application.Queues;
using ProductApp.Persistence;
using Quartz;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
AppSettings? appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

string host = appSettings?.RabbitMq?.Host ?? string.Empty;
string username = appSettings?.RabbitMq?.Username ?? string.Empty;
string password = appSettings?.RabbitMq?.Password ?? string.Empty;
string productQueueName = appSettings?.RabbitMq?.ProductQueueName ?? string.Empty;
string productQueueErrorName = appSettings?.RabbitMq?.ProductQueueErrorName ?? string.Empty;
int massTransitRetryCount = appSettings?.MassTransit?.RetryCount ?? 0;
long massTransitInterval = appSettings?.MassTransit?.Interval ?? 0;
double quartzStartTime = appSettings?.Quartz?.StartTime ?? 0;
int quartzInterval = appSettings?.Quartz?.Interval ?? 0;
int quartzMaxConcurrency = appSettings?.Quartz?.MaxConcurrency ?? 0;
string quartzJobName = appSettings?.Quartz?.JobName ?? string.Empty;
string quartzTriggerName = appSettings?.Quartz?.TriggerName ?? string.Empty;

builder.Host.UseSerilog((_, loggerConfiguration) => loggerConfiguration.WriteTo.Console(formatProvider: null).ReadFrom.Configuration(builder.Configuration));

builder.Services.AddSingleton<IRabbitMqFactory, RabbitMqFactory>();
//builder.Services.AddSingleton<IJob, ProductMessageJob>();

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceRegistration(builder.Configuration);

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
            e.UseMessageRetry(r => r.Interval(massTransitRetryCount, TimeSpan.FromSeconds(massTransitInterval)));
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
    var jobKey = new JobKey(quartzJobName);

    q.AddJob<ProductMessageJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity(quartzTriggerName)
        .StartAt(DateTimeOffset.Now.AddSeconds(quartzStartTime))
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(quartzInterval)
            .RepeatForever()));

    // Quartz logging configuration
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
    q.UseDefaultThreadPool(tp => tp.MaxConcurrency = quartzMaxConcurrency);
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//IScheduler scheduler = await QuartzJobFactory<ProductMessageJob>.CreateJobAsync("ProductMessageJob", "ProductMessageJobGroup", "ProductMessageTrigger", "ProductMessageTriggerGroup", 300, 120);

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
