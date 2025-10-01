using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Persistence.Context;
using ProductApp.Persistence.Repositories;

namespace ProductApp.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("memoryDb"));
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlDb")));
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserOperationClaimRepository, UserOperationClaimRepository>();
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();

    }
}
