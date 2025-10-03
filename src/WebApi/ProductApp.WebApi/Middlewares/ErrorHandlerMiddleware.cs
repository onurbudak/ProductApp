using System.Net;
using System.Text;
using System.Text.Json;
using ProductApp.Application.Wrappers;

namespace ProductApp.WebApi.Middlewares;

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
            string method = httpContext.Request.Method;
            PathString path = httpContext.Request.Path;
            string requestBody = await ReadRequestBody(httpContext.Request);

            _logger.LogError(ex,
                "{Method} {Path} \nBody: {Body} \nMessage: {Message}",
                method, path, requestBody, ex.Message);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();
        request.Body.Position = 0;

        using var reader = new StreamReader(
            request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true);

        string body = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return body;
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new ErrorResponse
        {
            IsSuccess = false,
            StatusCode = httpContext.Response.StatusCode,
            Message = "Internal Server Error. Please try again later."
        };

        string json = JsonSerializer.Serialize(errorResponse);

        return httpContext.Response.WriteAsync(json);
    }
}
