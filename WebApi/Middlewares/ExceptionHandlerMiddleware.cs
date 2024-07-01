using Exceptions.Infrastructure;
using Exceptions.Services;
using FluentValidation;
using WebApi.Models;

namespace WebApi.Middlewares;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
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

            var response = new CommonResponse<Empty>
            {
                Data = null,
                Error = new Error
                {
                    Title = e.Title,
                    Message = e.Message,
                    StatusCode = e.StatusCode
                }
            };
            
            context.Response.Clear();
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (ServiceException e)
        {
            var response = new CommonResponse<Empty>
            {
                Data = null,
                Error = new Error
                {
                    Title = e.Title,
                    Message = e.Message,
                    StatusCode = e.StatusCode
                }
            };
            
            context.Response.Clear();
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception e)
        {
            logger.LogWarning(e, e.Message);
            
            var response = new CommonResponse<Empty>
            {
                Data = null,
                Error = new Error
                {
                    Title = "Unknown server error",
                    Message = "Please retry query",
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };
            
            context.Response.Clear();
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

public record Empty();