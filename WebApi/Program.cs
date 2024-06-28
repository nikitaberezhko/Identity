using Infrastructure.JwtProvider.Implementations;
using Infrastructure.Repositories.Implementations;
using Services.JwtProvider.Abstractions;
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

        // Extensions
        services.AddExtensions(builder.Configuration);
        
        // JwtProvider
        services.AddScoped<IJwtProvider, JwtProvider>();
        
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