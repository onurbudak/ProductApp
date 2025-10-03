using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Filters;
using ProductApp.Application.Interfaces.Filters;
using ProductApp.Application.Interfaces.Queues;
using ProductApp.Application.Queues;
using ProductApp.Application.Services;
using ProductApp.Domain.Entities;

namespace ProductApp.Application;

public static class ServiceRegistration
{
    public static void AddApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(assembly);
        services.AddSingleton<IRabbitMqFactory, RabbitMqFactory>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFilterService<Product>, DynamicLinqFilterService<Product>>();
        services.AddScoped<IFilterService<User>, DynamicLinqFilterService<User>>();
        services.AddScoped<IFilterService<UserOperationClaim>, DynamicLinqFilterService<UserOperationClaim>>();
        services.AddScoped<IFilterService<OperationClaim>, DynamicLinqFilterService<OperationClaim>>();
        services.AddScoped<IFilterService<RefreshToken>, DynamicLinqFilterService<RefreshToken>>();
    }
}
