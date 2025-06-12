using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log incoming request
        _logger.LogInformation("Request started : {Method} ", context.Request.Path);

        // Call the next middleware in the pipeline
        await _next(context);

        // Log outgoing response
        _logger.LogInformation("Request finished : {Method} {Url} {StatusCode} ", context.Request.Method, context.Request.Path, context.Response.StatusCode);
    }
}
