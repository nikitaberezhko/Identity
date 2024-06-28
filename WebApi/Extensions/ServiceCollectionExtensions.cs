using Infrastructure.EntityFramework;
using Infrastructure.JwtProvider.Implementations;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.User.Requests.Validators;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureApiVersioning();
        services.ConfigureContext(configuration.GetConnectionString("DefaultConnectionString")!);
        services.AddScoped<DbContext, DataContext>();
        services.ConfigureAuthServices(configuration);
        services.ConfigureUserValidators();

        return services;
    }
}