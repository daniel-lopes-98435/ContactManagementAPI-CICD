using ContactManagement.Application.Interfaces;
using ContactManagement.Application.Services;
using ContactManagement.Domain.Interfaces;
using ContactManagement.InfraStructure.Respositories;
using System.Data.SqlClient;
using System.Data;

namespace ContactManagement.Api.Configurations;
public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddTransient<IDbConnection>(db => new SqlConnection(connectionString));
        services.AddScoped<IContactServices, ContactServices>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

