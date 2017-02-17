using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace EmptyCore
{
    public class Startup
    {
        private static void HandleMap1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map test 1");
            });
        }

        private static void HandleMap2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map test 2");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.Map("/map1", HandleMap1);
            app.Map("/app2", HandleMap2);

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);

            app.Use((context, next) =>
            {
                var cultureQuery = context.Request.Query["culture"];
                if (!string.IsNullOrWhiteSpace(cultureQuery))
                {
                    var culture = new CultureInfo(cultureQuery);
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }
                return next();
            });

            app.UseRequestCulture();

            app.UsePayloadProcessing();

            // non-Map delegate, default map
            app.Run(async (context) =>
            {
                //await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync($"Hello {CultureInfo.CurrentCulture.DisplayName}");
            });
        }
    }

    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            // Call the next delegate/middleware in the pipeline
            return this.next(context);
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }

    public class Payload
    {
        public string JobId { get; set; }
        public string ContainerId { get; set; }
    }

    public class PayloadProcessingMiddleware
    {
        private readonly RequestDelegate next;
        public PayloadProcessingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/payload"))
            {
                if (string.Equals(context.Request.Method, "post", StringComparison.OrdinalIgnoreCase))
                {
                    using (var stream = new StreamReader(context.Request.Body))
                    {
                        var str = await stream.ReadToEndAsync();
                        Payload res = JsonConvert.DeserializeObject<Payload>(str);
                    }
                }
            }
            await this.next(context);
        }
    }

    public static class UsePayloadProcessingMiddlewareExtensions
    {
        public static IApplicationBuilder UsePayloadProcessing(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PayloadProcessingMiddleware>();
        }
    }
}
