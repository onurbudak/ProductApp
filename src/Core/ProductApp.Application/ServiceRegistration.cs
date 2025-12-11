using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Filters;
using ProductApp.Application.Interfaces.Filters;
using ProductApp.Application.Interfaces.Queues;
using ProductApp.Application.Interfaces.Services;
using ProductApp.Application.Queues;
using ProductApp.Application.Services;
using ProductApp.Domain.Entities;

namespace ProductApp.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
        services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFilterService<Product>, DynamicLinqFilterService<Product>>();
        services.AddScoped<IFilterService<User>, DynamicLinqFilterService<User>>();
        services.AddScoped<IFilterService<UserOperationClaim>, DynamicLinqFilterService<UserOperationClaim>>();
        services.AddScoped<IFilterService<OperationClaim>, DynamicLinqFilterService<OperationClaim>>();
        services.AddScoped<IFilterService<RefreshToken>, DynamicLinqFilterService<RefreshToken>>();

        return services;
    }
}
