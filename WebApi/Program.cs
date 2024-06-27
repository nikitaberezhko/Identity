using Infrastructure.EntityFramework;
using Infrastructure.JwtProvider.Implementations;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Services.JwtProvider.Abstractions;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Implementations;
using Services.Services.Implementations.Mapping;
using WebApi.Mapping;
using WebApi.Models.User.Requests.Validators;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;
        
        services.AddControllers();
        services.AddProblemDetails();
        
        // Options
        services.Configure<JwtSettings>(
            builder.Configuration.GetSection("JwtSettings"));
        
        // JwtProvider and auth
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.ConfigureAuthServices(builder.Configuration);
        
        // FluentValidation
        services.ConfigureUserValidators();
        
        // DataContext
        services.ConfigureContext(
            builder.Configuration.GetConnectionString("DefaultConnectionString")!);
        services.AddScoped<DbContext, DataContext>();

        // AutoMapper
        services.AddAutoMapper(typeof(UserMappingProfile), 
            typeof(UserModelMappingProfile));
        
        // Репозитории
        services.AddScoped<IUserRepository, UserRepository>();
        
        // Сервисы
        services.AddScoped<IUserService, UserService>();

        services.AddAuthentication();
        services.AddAuthorization();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var app = builder.Build();

        app.UseExceptionHandler();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}