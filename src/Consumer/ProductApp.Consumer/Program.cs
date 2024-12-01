using MassTransit;
using ProductApp.Persistence;
using ProductApp.Application;
using ProductApp.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);

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
        cfg.Host("localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        //Consumer'ı dinlemek için endpoint ekliyoruz
        cfg.ReceiveEndpoint("product_queue", e =>
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
