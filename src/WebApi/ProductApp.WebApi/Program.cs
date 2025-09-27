using MassTransit;
using ProductApp.Application;
using ProductApp.Application.Common;
using ProductApp.Persistence;
using ProductApp.WebApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
AppSettings? appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

string host = appSettings?.RabbitMq?.Host ?? string.Empty;
string username = appSettings?.RabbitMq?.Username ?? string.Empty;
string password = appSettings?.RabbitMq?.Password ?? string.Empty;

builder.Host.UseSerilog((_, loggerConfiguration) => loggerConfiguration.WriteTo.Console(formatProvider: null).ReadFrom.Configuration(builder.Configuration));

builder.Services.AddMassTransit(x =>
{
    // RabbitMQ ile bağlantıyı kuruyoruz
    x.UsingRabbitMq((context, cfg) =>
    {
        // RabbitMQ sunucusuna bağlantı bilgilerini ekliyoruz
        cfg.Host(host, h =>
        {
            h.Username(username);
            h.Password(password);
        });
    });
});

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.SetMinimumLevel(LogLevel.Trace);
});

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceRegistration(builder.Configuration);

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
app.UseLoggingHandler();
app.UseErrorHandler();

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
