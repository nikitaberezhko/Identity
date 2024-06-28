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
        catch (Exception e)
        {
            var statusCode = e switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            
            logger.LogWarning(e, e.Message);
            
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
        }
    }
}