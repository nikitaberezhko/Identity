using Exceptions.Infrastructure;
using Exceptions.Services;
using FluentValidation;
using WebApi.Models;

namespace WebApi.Middlewares;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) 
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DomainException e)
        {
            logger.LogWarning(e, e.Message);

            await InterceptResponseAsync(e.Title,
                e.Message,
                e.StatusCode);
        }
        catch (ServiceException e)
        {
            logger.LogWarning(e, e.Message);
            
            await InterceptResponseAsync(e.Title,
                e.Message,
                e.StatusCode);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            
            await InterceptResponseAsync("Unknown server error", 
                "Please retry query", 
                StatusCodes.Status500InternalServerError);
        }

        async Task InterceptResponseAsync(string title, string message, int statusCode)
        {
            var response = new CommonResponse<Empty>
            {
                Data = null,
                Error = new Error
                {
                    Title = title,
                    Message = message,
                    StatusCode = statusCode
                }
            };
            
            context.Response.Clear();
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

public record Empty();