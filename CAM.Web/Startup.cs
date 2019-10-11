using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using CAM.Infrastructure.Data;
using AutoMapper;
using Newtonsoft.Json;
using Hangfire;
using Hangfire.SQLite;
using CAM.Infrastructure.Jobs;
using CAM.Core.Interfaces;

namespace CAM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Main Db Context
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(
                Configuration.GetConnectionString("ApplicationContext"),
                assembly => assembly.MigrationsAssembly("CAM.Web")
                ));
            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Hangfire
            // NOTE: The Hangfire connection string ends with a ;. This is because of the SQLite extension checking for one. 
            services.AddHangfire(config => config.UseSQLiteStorage(Configuration.GetConnectionString("HangfireConnection")));
            // TimesScraperJob
            services.AddScoped<ITimesScraperJob, TimesScraperJob>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }
            // Hangfire JobStorage.Current initialize
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1
            });

            // app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                // Add Area routing convention
                routes.MapRoute(
                name: "MyArea",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Hangfire job scheduling
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
            HangfireScheduler.ScheduleRecurringJobs();
        }
    }
}