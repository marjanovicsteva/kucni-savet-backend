using System.Net;
using System.Text.Json;
using KucniSavetBackend.Exceptions;

namespace KucniSavetBackend.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            OperationalException => HttpStatusCode.InternalServerError,
            ArgumentException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            error = exception.Message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

}