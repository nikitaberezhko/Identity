using Exceptions.Infrastructure;
using Exceptions.Services;
using FluentValidation;

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
            InterceptResponse(e.StatusCode);
        }
        catch (ServiceException e)
        {
            logger.LogWarning(e, e.Message);
            InterceptResponse(e.StatusCode);
        }
        catch (Exception e)
        {
            logger.LogWarning(e, e.Message);
            
            logger.LogWarning(e, e.Message);
            InterceptResponse(StatusCodes.Status500InternalServerError);
        }

        void InterceptResponse(int statusCode)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
        }
    }
}