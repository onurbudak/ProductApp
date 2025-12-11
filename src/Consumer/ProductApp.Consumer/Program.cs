using MassTransit;
using ProductApp.Application;
using ProductApp.Application.Common;
using ProductApp.Application.Consumers;
using ProductApp.Application.Jobs;
using ProductApp.Persistence;
using Quartz;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
AppSettings? appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

string hostName = appSettings?.RabbitMq?.HostName ?? string.Empty;
string userName = appSettings?.RabbitMq?.UserName ?? string.Empty;
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
int quartzRepeatCount = appSettings?.Quartz?.RepeatCount ?? 0;

builder.Host.UseSerilog((_, loggerConfiguration) => loggerConfiguration.WriteTo.Console(formatProvider: null).ReadFrom.Configuration(builder.Configuration));

builder.Services
    .AddApplicationRegistration()
    .AddPersistenceRegistration(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    //Consumer'ı ekliyoruz
    x.AddConsumer<UpdateProductConsumer>();
    // RabbitMQ ile bağlantıyı kuruyoruz
    x.UsingRabbitMq((context, cfg) =>
    {
        // RabbitMQ sunucusuna bağlantı bilgilerini ekliyoruz
        cfg.Host(hostName, h =>
        {
            h.Username(userName);
            h.Password(password);
        });
        //Consumer'ı dinlemek için endpoint ekliyoruz
        cfg.ReceiveEndpoint(productQueueName, e =>
        {
            e.ConfigureConsumer<UpdateProductConsumer>(context);
            e.UseMessageRetry(r => r.Interval(massTransitRetryCount, TimeSpan.FromSeconds(massTransitInterval)));
            e.UseInMemoryOutbox(context);
        });
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
    q.AddJob<UpdateProductJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity(quartzTriggerName)
        .StartAt(DateTimeOffset.Now.AddSeconds(quartzStartTime))
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(quartzInterval)
            .WithRepeatCount(quartzRepeatCount)));

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

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // HTTPS yanıtlarında da sıkıştırmayı aç
});

var app = builder.Build();

app.UseResponseCompression();
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
