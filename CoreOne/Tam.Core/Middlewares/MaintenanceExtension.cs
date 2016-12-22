using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tam.Core.Compression;
using Tam.Core.Filters.RequestFiltering;
using Tam.Core.Utilities;

namespace Tam.Core.Middlewares
{
    public static class MaintenanceExtension
    {
        public static IApplicationBuilder UseMaintenanceMode(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>();
        }

        public static IServiceCollection AddMaintenaceMode(this IServiceCollection service,
            MaintenanceWindow window)
        {
            Guard.ThrowIfNull(window);
            service.AddSingleton(window);
            return service;
        }

        public static IServiceCollection AddMaintenaceMode(this IServiceCollection service,
            Func<bool> enabler, byte[] response, string contentType = "text/html", int retryAfterInSeconds = 3600)
        {
            service.AddSingleton(new MaintenanceWindow(enabler, response)
            {
                ContentType = contentType,
                RetryAfterInSeconds = retryAfterInSeconds
            });
            return service;
        }
    }
}
