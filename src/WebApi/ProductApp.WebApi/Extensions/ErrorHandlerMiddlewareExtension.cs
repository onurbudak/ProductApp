using ProductApp.WebApi.Middlewares;

namespace ProductApp.WebApi.Extensions;

public static class ErrorHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}