using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RequestLogger
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            using var newRequestStream = new MemoryStream();

            using (StreamReader reader = new StreamReader(context.Request.Body))
            {
                //COMMENT 1: Read body stream as text
                var bodyText = await reader.ReadToEndAsync();

                //COMMENT 2: Log request as information
                _logger.LogInformation($"REQUEST Requester: {context.Connection.RemoteIpAddress}, Method: {context.Request.Method}, Path: {context.Request.Path}, Body: {bodyText}");

                var bodyBytes = Encoding.UTF8.GetBytes(bodyText);

                newRequestStream.Write(bodyBytes, 0, bodyBytes.Length);

                newRequestStream.Seek(0, SeekOrigin.Begin);

                //COMMENT 3: Write body stream back to the request body
                context.Request.Body = newRequestStream;
            }

            await _next.Invoke(context);
        }
    }
}
