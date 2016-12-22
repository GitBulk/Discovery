using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

        //public Task Invoke(HttpContext context)
        //{
        //    return this.next(context);
        //}

        public override Task Invoke(HttpContext context)
        {
            this.logger.LogInformation("Test base middleware");
            return base.Invoke(context);
        }
    }
}
