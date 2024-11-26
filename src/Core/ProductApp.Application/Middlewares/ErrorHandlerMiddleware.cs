using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Wrappers;
using System.Net;

namespace ProductApp.Application.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return httpContext.Response.WriteAsync(new ErrorResponse
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = "Internal Server Error. Please try again later."
        }.ToString());
    }
}