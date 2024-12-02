﻿using MassTransit;
using ProductApp.Application;
using ProductApp.Application.Extensions;
using ProductApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var rabbitMqConfig = configuration.GetSection("RabbitMq");
string host = rabbitMqConfig["host"] ?? string.Empty;
string username = rabbitMqConfig["username"] ?? string.Empty;
string password = rabbitMqConfig["password"] ?? string.Empty;

//await RabbitMqFactory.ConnectionAsync();

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

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceRegistration(configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

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
