using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AzureOneCore.Filter;
using Microsoft.Net.Http.Headers;
using AzureOneCore.Midleware;
using AzureOneCore.Services;

namespace AzureOneCore
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }
        private ILoggerFactory loggerFactory;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            this.loggerFactory = new LoggerFactory();

        }

        private IMvcBuilder ConfigureMvc(IServiceCollection services)
        {
            var builder = services.AddMvc();
            builder.AddMvcCamelCasePropertyNames();
            var exception = new GlobalExceptionFilter(this.loggerFactory);
            builder.AddMvcOptions(o =>
            {
                o.Filters.Add(exception);
                o.Filters.Add(new WhitespaceFilterAttribute());
            });
            return builder;
        }

        private void SetCustomConfiguration(IServiceCollection services)
        {
            //http://andrewlock.net/how-to-use-the-ioptions-pattern-for-configuration-in-asp-net-core-rc2/
            services.Configure<SystemSettings>(option => this.Configuration.GetSection("SystemSettings").Bind(option));
            //services.Configure<SystemSettings>(op =>
            //{
            //    ConfigurationBinder.Bind(Configuration, Configuration.GetSection(nameof(SystemSettings)));
            //});
        }

        private void SetupDI(IServiceCollection services)
        {
            services.AddSingleton<CountryService>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMemoryCache();
            // https://wildermuth.com/2016/04/14/Using-Cache-in-ASP-NET-Core-1-0-RC1
            // https://www.tutorialspoint.com/asp.net_core/asp.net_core_middleware.htm
            // https://channel9.msdn.com/Series/aspnetmonsters/Episode-32-In-Memory-Caching-with-ASPNET-Core
            var builder = ConfigureMvc(services);
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            SetCustomConfiguration(services);
            SetupDI(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Handle unhandled errors
                app.UseExceptionHandler("/Error");
                // Display friendly error pages for any non-success case
                // This will handle any situation where a status code is >= 400
                // and < 600, so long as no response body has already been
                // generated.
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //app.Run(async (context) =>
            //{
            //    var message = this.Configuration["message"];
            //    await context.Response.WriteAsync(message);
            //});

            //app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    context.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
                }
            });

            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                }
            );
        }
    }
}
