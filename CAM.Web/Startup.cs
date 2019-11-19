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
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Hangfire
            // NOTE: The Hangfire connection string ends with a ; in the json file. 
            // This is because of the SQLite extension checking for one. 
            services.AddHangfire(config => config.UseSQLiteStorage(Configuration.GetConnectionString("HangfireConnection")));

            // Email confirmation/password reset etc with options
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration); //SendGrid keys stored in usersecrets

            // Fsp login options
            services.Configure<FspScraperOptions>(Configuration);

            // TimesScraperJob
            services.AddSingleton<ITimesScraper, FspTimesScraper>();

            // Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // SignIn settings
                options.SignIn.RequireConfirmedEmail = false;
            });

            // MVC + Razor
            services.AddControllersWithViews(config => 
            {
                // Default to requiring authorization
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter());
            })
            .AddRazorRuntimeCompilation();

            services.AddRazorPages()
                    .AddRazorPagesOptions(options => 
                    {
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage/");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    });
            // Cookie policy
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Cookie config
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "CentennialAircraftMaintenance";
                options.Cookie.HttpOnly = true;
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
                };
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
                app.UseExceptionHandler("/Home/Error");
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

            // Auth BEFORE ENDPOINTS
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action}/{id?}",
                        defaults: new {controller = "home", Action="index" }
                    );
                    endpoints.MapRazorPages();
                });


            // Hangfire job scheduling
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
            HangfireScheduler.ScheduleRecurringJobs();


        }
    }
}