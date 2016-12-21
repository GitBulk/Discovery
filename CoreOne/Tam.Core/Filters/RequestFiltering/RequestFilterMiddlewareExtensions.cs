using Microsoft.AspNetCore.Builder;
using Tam.Core.Utilities;

namespace Tam.Core.Filters.RequestFiltering
{
    public static class RequestFilterMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestFilter(this IApplicationBuilder app, RequestFilterOptions options)
        {
            Guard.ThrowIfNull(app);
            Guard.ThrowIfNull(options);
            return app.UseMiddleware<RequestFilterMiddleware>(options);
        }
    }
}
