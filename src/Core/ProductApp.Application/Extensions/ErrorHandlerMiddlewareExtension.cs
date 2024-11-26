using Microsoft.AspNetCore.Builder;
using ProductApp.Application.Middlewares;

namespace ProductApp.Application.Extensions;

public static class ErrorHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}