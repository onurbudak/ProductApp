using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Filtering;
using ProductApp.Application.Interfaces.Filtering;
using ProductApp.Application.Interfaces.Repository;
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
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFilterService<Product>, DynamicLinqFilterService<Product>>();
    }
}
