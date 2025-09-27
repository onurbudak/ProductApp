using ProductApp.WebApi.Middlewares;

namespace ProductApp.WebApi.Extensions;

public static class LoggingMiddlewareExtension
{
    public static IApplicationBuilder UseLoggingHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
