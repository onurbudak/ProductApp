using MassTransit;
using ProductApp.Application;
using ProductApp.Application.Extensions;
using ProductApp.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var rabbitMqConfig = configuration.GetSection("RabbitMq");
string host = rabbitMqConfig["host"] ?? string.Empty;
string username = rabbitMqConfig["username"] ?? string.Empty;
string password = rabbitMqConfig["password"] ?? string.Empty;
string productQueueName = rabbitMqConfig["productQueueName"] ?? string.Empty;

builder.Services.AddMassTransit(x =>
{
    //Consumer'ı ekliyoruz
    //x.AddConsumer<ProductMessageConsumer>();

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
        //cfg.ReceiveEndpoint(productQueueName, e =>
        //{
        //    e.ConfigureConsumer<ProductMessageConsumer>(context);
        //    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(20)));
        //    e.UseInMemoryOutbox(context);
        //});
    });
});

builder.Host.UseSerilog((_, loggerConfiguration) => loggerConfiguration.WriteTo.Console(formatProvider: null).ReadFrom.Configuration(configuration));

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.SetMinimumLevel(LogLevel.Trace);
});

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceRegistration(configuration);

// Add services to the container.

builder.Services
    .AddControllers();
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    //});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseErrorHandler();
app.UseLoggingHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
