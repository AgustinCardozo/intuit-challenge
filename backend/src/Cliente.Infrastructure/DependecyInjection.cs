using Cliente.Domain.Contracts.Repositories;
using Cliente.Infrastructure.Data;
using Cliente.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cliente.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ClienteDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IClienteDapperContext, ClienteDapperContext>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddSingleton<ClienteDapperContext>();

        return services;
    }
}
