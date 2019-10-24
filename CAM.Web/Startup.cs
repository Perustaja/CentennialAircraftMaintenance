using System.ComponentModel.Design;
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
using CAM.Core.Interfaces;
using CAM.Web.Jobs;
using CAM.Core.Services.TimesScraper;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity;
using CAM.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ApplicationConnection")));

            // Identity Db Context and DefaultIdentity
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<IdentityContext>();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Hangfire
            // NOTE: The Hangfire connection string ends with a ; in the json file. 
            // This is because of the SQLite extension checking for one. 
            services.AddHangfire(config => config.UseSQLiteStorage(Configuration.GetConnectionString("HangfireConnection")));

            // TimesScraperJob
            services.AddScoped<ITimesScraper, FspTimesScraper>();

            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Password settings
                options.Password.RequireNonAlphanumeric = false;

                // Default signin settings
                options.SignIn.RequireConfirmedEmail = true;
            });

            // Cookie config
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "CentennialAircraftMaintenance";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1000);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
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
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
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