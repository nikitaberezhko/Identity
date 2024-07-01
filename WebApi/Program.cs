using Infrastructure.JwtProvider.Implementations;
using Infrastructure.PasswordHasher;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework;
using Services.Auth.Abstractions;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Implementations;
using Services.Services.Implementations.Mapping;
using WebApi.Extensions;
using WebApi.Mapping;
using WebApi.Middlewares;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;
        
        services.AddControllers();
        
        services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        
        // Extensions
        services.ConfigureApiVersioning();
        services.ConfigureContext(builder.Configuration.GetConnectionString("DefaultConnectionString")!);
        services.AddScoped<DbContext, DataContext>();
        services.ConfigureAuthServices(builder.Configuration);
        services.ConfigureUserValidators();
        
        // Auth services
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        // ExceptionHandlerMiddleware
        services.AddTransient<ExceptionHandlerMiddleware>();

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

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}