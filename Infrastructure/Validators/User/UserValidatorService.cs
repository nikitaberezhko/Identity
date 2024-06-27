using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Models.User.Requests.Validators;

public static class UserValidatorService
{
    public static IServiceCollection ConfigureUserValidators(
        this IServiceCollection services)
    {
        services.AddScoped<AuthenticateValidator>();
        services.AddScoped<CreateValidator>();
        services.AddScoped<DeleteValidator>();
        
        return services;
    }
}