using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using CAM.Infrastructure.Data;
using AutoMapper;
using Hangfire;
using Hangfire.SQLite;
using CAM.Core.Interfaces;
using CAM.Web.Jobs;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity;
using CAM.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using CAM.Infrastructure.Services;
using CAM.Infrastructure.Services.TimesScraper;
using CAM.Core.Options;
using CAM.Web.Interfaces;
using CAM.Web.Services;
using Microsoft.Extensions.Hosting;
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
            // Main DbContext
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ApplicationConnection")));

            // Identity DbContext and options below
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Hangfire
            // NOTE: The Hangfire connection string ends with a ; in the json file. 
            // This is because of the SQLite extension checking for one. 
            services.AddHangfire(config => config.UseSQLiteStorage(Configuration.GetConnectionString("HangfireConnection")));

            // Email confirmation/password reset with options
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration); //SendGrid keys stored in usersecrets

            // RazorViewToStringRenderer(for email templates)
            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            // IConfirmationEmailGenerator and Sender(for clean confirmation emails across controllers)
            services.AddScoped<IConfirmationEmailGenerator, ConfirmationEmailGenerator>();
            services.AddScoped<IConfirmationEmailSender, ConfirmationEmailSender>();

            // Fsp login options
            services.Configure<FspScraperOptions>(Configuration);

            // TimesScraperJob
            services.AddSingleton<ITimesScraper, FspTimesScraper>();

            // Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Password settings
                options.Password.RequireNonAlphanumeric = false;
            });

            // MVC + Razor
            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorPagesOptions(options => 
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage/");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            // Cookie config
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "CentennialAircraftMaintenance";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1000);
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Hangfire JobStorage.Current initialize
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseEndpoints(routes =>
                {
                    routes.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action}/{id?}",
                        defaults: new {controller = "home", Action="index" }
                    );
                    routes.MapRazorPages();
                });
            
            // Auth
            app.UseAuthentication();
            app.UseAuthorization();

            // Hangfire job scheduling
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
            HangfireScheduler.ScheduleRecurringJobs();


        }
    }
}