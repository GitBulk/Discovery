using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;
using Tam.Core.Compression;
using Tam.Core.Utilities;
using System.Text;

namespace Tam.Core.Middlewares
{
    public class MaintenanceMiddleware : BaseMiddleware
    {
        private readonly ILogger logger;
        private readonly MaintenanceWindow window;

        public MaintenanceMiddleware(RequestDelegate next, MaintenanceWindow window,
            ILogger<MaintenanceMiddleware> logger) : base(next)
        {
            this.logger = logger;
            this.window = window;
        }

        public override async Task Invoke(HttpContext context)
        {
            this.logger.LogInformation("Test MaintenanceMiddleware");
            if (this.window.Enabled)
            {
                // set the code to 503 for SEO reasons
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                context.AddHttpResponseHeader("Retry-After", this.window.RetryAfterInSeconds.ToString());
                context.Response.ContentType = window.ContentType;
                await context.Response.WriteAsync(Encoding.UTF8.GetString(window.Response), Encoding.UTF8);
            }
            await this.next.Invoke(context);
        }
    }
}
