using System.Net;
using System.Text.Json;
using Serilog.AWS.Logging.Errors;

namespace Serilog.AWS.Logging.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> _logger,
        IHostEnvironment env)
{
public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new CustomApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new CustomApiException(context.Response.StatusCode, ex.Message, "Internal server error");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            _logger.LogError("An error occurred: {message}", json);

            await context.Response.WriteAsync(json);
        }
    }
}
