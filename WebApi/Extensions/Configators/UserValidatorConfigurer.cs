using FluentValidation;
using Infrastructure.Validators.User;
using Services.Services.Contracts.User;

namespace WebApi.Models.User.Requests.Validators;

public static class UserValidatorConfigurer
{
    public static IServiceCollection ConfigureUserValidators(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<AuthenticateUserDto>, AuthenticateValidator>();
        services.AddScoped<IValidator<AuthorizationUserDto>, AuthorizationValidator>();
        services.AddScoped<IValidator<CreateUserDto>, CreateValidator>();
        services.AddScoped<IValidator<DeleteUserDto>, DeleteValidator>();
        
        return services;
    }
}