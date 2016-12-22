﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tam.Core.Filters.RequestFiltering;
using Tam.Core.Utilities;

namespace Tam.Core.Middlewares
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMaintenanceMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>();
        }

        public static IApplicationBuilder UseRequestFilter(this IApplicationBuilder app, RequestFiltMaintenanceWindowerOptions options)
        {
            Guard.ThrowIfNull(app);
            Guard.ThrowIfNull(options);
            return app.UseMiddleware<RequestFilterMiddleware>(options);
        }
    }
}
