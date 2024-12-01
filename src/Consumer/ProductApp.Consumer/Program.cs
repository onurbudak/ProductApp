using MassTransit;
using ProductApp.Persistence;
using ProductApp.Application;
using ProductApp.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var rabbitMqConfig = configuration.GetSection("RabbitMq");
string host = rabbitMqConfig["host"] ?? string.Empty;
string username = rabbitMqConfig["username"] ?? string.Empty;
string password = rabbitMqConfig["password"] ?? string.Empty;
string productQueueName = rabbitMqConfig["productQueueName"] ?? string.Empty;

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
        });
    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
