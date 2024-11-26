using Microsoft.AspNetCore.Builder;

namespace ProductApp.Application.Extensions;

public static class LoggingMiddlewareExtension
{
    public static IApplicationBuilder UseLoggingHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
