using Domain.Repository;
using Domain.Service;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureExtension
{
    public static void AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepository();
        services.AddService();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");

        services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(connectionString));
    }

    private static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IDDDRepository, DDDRepository>();
    }

    private static void AddService(this IServiceCollection services)
    {
        //TODO: remover
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();
    }
}
