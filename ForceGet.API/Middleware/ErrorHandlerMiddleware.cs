using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ForceGet.API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetStatusCode(exception);

        var response = new
        {
            error = exception.Message,
            stackTrace = exception.StackTrace
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            ArgumentException => 400,
            UnauthorizedAccessException => 401,
            Exception => 500
        };
    }
}
