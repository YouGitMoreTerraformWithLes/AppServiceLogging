using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace AppServiceLogging
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Use this query in the Azure Application Insights associated with a deployed instance
        // traces
        // | order by timestamp desc
        // | extend cd = parse_json(customDimensions)
        // | where cd.CategoryName == "AppServiceLogging.Controllers.HealthController"
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options.AddConsole();
                options.SetMinimumLevel(LogLevel.Trace);

                options.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
                options.AddApplicationInsights(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
            });

            services.AddControllers();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("healthy");
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
