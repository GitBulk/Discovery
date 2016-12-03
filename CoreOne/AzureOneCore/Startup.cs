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

namespace AzureOneCore
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }
        private ILoggerFactory loggerFactory;
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            this.loggerFactory = loggerFactory;
        }

        private void ConfigureFilter(IMvcBuilder builder)
        {
            var exception = new GlobalExceptionFilter(this.loggerFactory);
            builder.AddMvcOptions(o => o.Filters.Add(exception));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            // https://wildermuth.com/2016/04/14/Using-Cache-in-ASP-NET-Core-1-0-RC1
            // https://www.tutorialspoint.com/asp.net_core/asp.net_core_middleware.htm
            // https://channel9.msdn.com/Series/aspnetmonsters/Episode-32-In-Memory-Caching-with-ASPNET-Core
            var builder = services.AddMvc();
            builder.AddMvcCamelCasePropertyNames();
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            ConfigureFilter(builder);

            //http://andrewlock.net/how-to-use-the-ioptions-pattern-for-configuration-in-asp-net-core-rc2/
            services.Configure<SystemSettings>(option => this.Configuration.GetSection("SystemSettings").Bind(option));
            //services.Configure<SystemSettings>(op =>
            //{
            //    ConfigurationBinder.Bind(Configuration, Configuration.GetSection(nameof(SystemSettings)));
            //});
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
                app.UseExceptionHandler("/Home/Error");
            }

            //app.Run(async (context) =>
            //{
            //    var message = this.Configuration["message"];
            //    await context.Response.WriteAsync(message);
            //});

            app.UseStaticFiles();

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
