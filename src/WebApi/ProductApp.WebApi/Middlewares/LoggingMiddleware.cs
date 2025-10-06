using System.Text;

namespace ProductApp.WebApi.Middlewares
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
            var originalBodyStream = context.Response.Body;
            var responseBody = new MemoryStream();        
            try
            {
                context.Response.Body = responseBody;
                string requestBody = await ReadRequestBody(context.Request);
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
                context.Response.Body = originalBodyStream;
                responseBody.Dispose(); 
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
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }
}
