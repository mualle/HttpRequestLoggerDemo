using Microsoft.AspNetCore.Builder;

namespace RequestLogger
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
