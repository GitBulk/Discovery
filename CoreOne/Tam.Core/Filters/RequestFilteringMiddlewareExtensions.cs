using Microsoft.AspNetCore.Builder;
using Tam.Core.Utilities;

namespace Tam.Core.Filters
{
    public static class RequestFilteringMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestFiltering(this IApplicationBuilder app, RequestFilteringOptions options)
        {
            Guard.ThrowIfNull(app);
            Guard.ThrowIfNull(options);
            return app.UseMiddleware<RequestFilteringMiddleware>(options);
        }
    }
}
