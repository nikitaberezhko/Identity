using Infrastructure.EntityFramework;
using Infrastructure.JwtProvider.Implementations;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services.JwtProvider.Abstractions;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Implementations;
using Services.Services.Implementations.Mapping;
using WebApi.Mapping;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        
        // Options
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("JwtSettings"));
        
        // JwtProvider and auth
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.ConfigureAuthServices(builder.Configuration);
        
        // DataContext
        builder.Services.ConfigureContext(
            builder.Configuration.GetConnectionString("DefaultConnectionString")!);
        builder.Services.AddScoped<DbContext, DataContext>();

        // AutoMapper
        builder.Services.AddAutoMapper(typeof(UserMappingProfile), 
            typeof(UserModelMappingProfile));
        
        // Репозитории
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        
        // Сервисы
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}