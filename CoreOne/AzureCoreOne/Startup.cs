using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AzureCoreOne.Helpers;
using AzureCoreOne.Filter;
using AzureCoreOne.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using AzureCoreOne.Configurations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using AzureCoreOne.AppContexts;
using Microsoft.EntityFrameworkCore;
using AzureCoreOne.Models.Indentities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tam.Core.Utilities;
using Tam.Core.Middlewares;
using System.Text;
using AzureCoreOne.Policies;
using Microsoft.AspNetCore.Authorization;
using AzureCoreOne.Models.ProBook;

namespace AzureCoreOne
{
    public class Startup
    {
        private ILoggerFactory loggerFactory;
        private IHostingEnvironment environment;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.loggerFactory = new LoggerFactory();
            this.environment = env;
            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        private IMvcBuilder SetupMvc(IServiceCollection services)
        {
            var builder = services.AddMvc();
            builder.AddMvcCamelCasePropertyNames();
            var exception = new GlobalExceptionFilter(this.loggerFactory);
            builder.AddMvcOptions(o =>
            {
                o.Filters.Add(exception);
                if (!this.environment.IsDevelopment())
                {
                    o.Filters.Add(new WhitespaceFilterAttribute());
                }
            });
            return builder;
        }

        private void SetupExternalViewComponent(IServiceCollection services)
        {
            //var assembly = typeof(ViewComponentLibrary.SimpleViewComponent).GetTypeInfo().Assembly;
            //var embeddedFileProvier = new EmbeddedFileProvider(assembly, "ViewComponentLibrary");
            //services.Configure<RazorViewEngineOptions>(o =>
            //{
            //    o.FileProviders.Add(embeddedFileProvier);
            //});

            // or
            services.AddExternalViewComponent<ViewComponentLibrary.SimpleViewComponent>("ViewComponentLibrary");
        }

        private void SetupCustomConfiguration(IServiceCollection services)
        {
            //http://andrewlock.net/how-to-use-the-ioptions-pattern-for-configuration-in-asp-net-core-rc2/
            //services.Configure<SystemSettings>(option => this.Configuration.GetSection("SystemSettings").Bind(option));

            // or
            //services.ConfigPOCO<SystemSettings>(this.Configuration.GetSection(typeof(SystemSettings).Name));

            // or
            //var setting = new SystemSettings();
            //services.ConfigPOCO(this.Configuration.GetSection(typeof(SystemSettings).Name), setting);

            // or
            services.ConfigPOCO(this.Configuration.GetSection(typeof(SystemSettings).Name), () =>
                new SystemSettings());

            // or
            //services.Configure<SystemSettings>(op =>
            //{
            //    ConfigurationBinder.Bind(Configuration, Configuration.GetSection(nameof(SystemSettings)));
            //});
        }

        private void SetupDI(IServiceCollection services)
        {
            //services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddSingleton<CountryService>();
            services.AddSingleton<IBookService, BookService>();
            // Add application services.
            services.AddSingleton<IEmailSender, AuthMessageSender>();
            services.AddSingleton<ISmsSender, AuthMessageSender>();
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddSingleton<IAuthorizationHandler, EditSkiCardAuthorizationHandler>();
            services.AddTransient<IProductRepository, EFProductRepository>();
        }

        private void SetupComponents(IServiceCollection services)
        {
            string connectionString = string.Empty;
            if (this.environment.IsDevelopment())
            {
                connectionString = Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                connectionString = Configuration.GetConnectionString("AzureCoreOneConnection");
            }

            services.AddEntityFrameworkInMemoryDatabase();
            //services.AddEntityFramework().AddDbContext<ApplicationDbContext>
            services.AddMemoryCache();
            services.AddDistributedRedisCache(options =>
            {
                //options.InstanceName = "AzureCoreOne";
                //options.Configuration = "127.0.0.1:6379";
                options.InstanceName = this.Configuration["RedisInstanceName"];
                options.Configuration = this.Configuration["RedisConnectionString"];
            });


            services.AddEntityFramework();
            //services.AddDbContext<AzureCoreOneDbContext>(option => option.UseSqlServer(connectionString));
            
            services.AddSqlServerDbContext<TamContext>(connectionString);
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AzureCoreOneDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddIdentity<ApplicationUser, IdentityRole, TamContext>();

            services.AddAuthorization(o =>
            {
                o.AddPolicy("Over17", policy =>
                policy.Requirements.Add(new MinimumAgeRequirement(17)));
            });
            

            services.AddDistributedMemoryCache();
            services.AddSession(o =>
            {
                o.CookieName = ".AzureCoreOne.QuizApp";
                string textTimeout = this.Configuration["SessionIdleTimeout"];
                o.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt32(textTimeout));
            });
            //services.AddMaintenaceMode(new MaintenanceWindow());
            services.AddMaintenaceMode(() => true, Encoding.UTF8.GetBytes("<div>I am in maintenance mode.</div>"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            SetupCustomConfiguration(services);
            SetupComponents(services);
            SetupExternalViewComponent(services);
            SetupMvc(services);
            SetupDI(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifeTime, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            lifeTime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("Server started");
            });

            lifeTime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("Server stopping");
            });

            lifeTime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("Server stopped");
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseMaintenanceMode();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                // Handle unhandled errors
                app.UseExceptionHandler("/Error");
                // Display friendly error pages for any non-success case
                // This will handle any situation where a status code is >= 400
                // and < 600, so long as no response body has already been
                // generated.

                // Error Action in Home Controller
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseSession();

            app.UseIdentity();

            app.UseMvc(routes =>
            {

                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Probook}/{action=List}/{id?}");

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}", // Shows the specified page (in this case, page 2) of items from the specified category (Ex: Soccer)
                    defaults: new { controller = "Probook", action = "List" }
                );
                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}", // Lists the specified page (Ex: showing items from all categories)
                    defaults: new { controller = "Probook", action = "List", page = 1 }
                );
                routes.MapRoute(
                    name: null,
                    template: "{category}", // Shows the first page of items from a specific category (Ex: the Soccer category)
                    defaults: new { controller = "Probook", action = "List", page = 1 }
                );
                routes.MapRoute(
                    name: null,
                    template: "", // Lists the first page of products from all categories
                    defaults: new { controller = "Probook", action = "List", page = 1 });

                routes.MapRoute(name: null, template: "{ controller}/{action}/{id?}");
            });

            app.ImportQuizData(this.environment.WebRootPath);
            app.ImportData();
            SeedData.EnsurePopulated(app);
        }
    }
}
