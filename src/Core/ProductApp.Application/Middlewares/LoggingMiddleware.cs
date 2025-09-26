using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ProductApp.Application.Middlewares
{
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
            Stream originalBodyStream = context.Response.Body;
            using MemoryStream responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            string requestBody = string.Empty;

            try
            {
                MemoryStream buffer = new MemoryStream();
                await context.Request.Body.CopyToAsync(buffer);
                buffer.Seek(0, SeekOrigin.Begin);
                context.Request.Body = buffer;
                requestBody = await ReadRequestBody(context.Request);

                _logger.LogInformation("Request started: {Method} {Path} \nHeaders: {Headers} \nBody: {Body}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.Headers,
                    requestBody);

                await _next(context);
            }
            finally
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation("Request finished: {Method} {Path} \nStatusCode: {StatusCode} \nResponse Body: {ResponseBody}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    responseText);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }
}
