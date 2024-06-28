using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Services.Services.Contracts.User;

namespace WebApi.Models.User.Requests.Validators;

public static class UserValidatorService
{
    public static IServiceCollection ConfigureUserValidators(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<AuthenticateUserDto>, AuthenticateValidator>();
        services.AddScoped<IValidator<CreateUserDto>, CreateValidator>();
        services.AddScoped<IValidator<DeleteUserDto>, DeleteValidator>();
        
        return services;
    }
}