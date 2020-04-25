using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CAM.Infrastructure.Data;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.SQLite;
using CAM.Core.Interfaces;
using CAM.Web.Jobs;
using Microsoft.AspNetCore.Identity;
using CAM.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using CAM.Infrastructure.Services;
using CAM.Infrastructure.Services.TimesScraper;
using CAM.Core.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using CAM.Core.Interfaces.Repositories;
using CAM.Infrastructure.Data.Repositories;
using CAM.Web.Mapping;
using CAM.Core.Interfaces.Services;
using CAM.Core.Services;
using Serilog;

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
            services.AddAutoMapper(config =>
            {
                config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            },
            AppDomain.CurrentDomain.GetAssemblies());

            // Hangfire
            // NOTE: The Hangfire connection string ends with a ; in the json file. 
            // This is because of the SQLite extension checking for one. 
            services.AddHangfire(config => config.UseSQLiteStorage(Configuration.GetConnectionString("HangfireConnection")));
            // Repositories
            services.AddScoped<IAircraftRepository, AircraftRepository>();
            services.AddScoped<IPartRepository, PartRepository>();
            services.AddScoped<IPartCategoryRepository, PartCategoryRepository>();
            services.AddScoped<IDiscrepancyRepository, DiscrepancyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // Services
            services.AddScoped<IPaginatedListMapper, PaginatedListMapper>();
            services.AddScoped<IPartsService, PartsService>();
            services.AddScoped<IDiscrepancyService, DiscrepancyService>();
            // Email confirmation/password reset etc with options
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration); //SendGrid keys stored in usersecrets
            // Fsp login options
            services.Configure<FspScraperOptions>(Configuration);
            // TimesScraperJob
            services.AddSingleton<ITimesScraper, FspTimesScraper>();
            // DocumentGenerator
            services.AddScoped<IDocumentGenerator, DocumentGenerator>();
            // FileHandler
            services.AddScoped<IFileHandler, FileHandler>();

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
            services.AddSession();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            // Authorization policies - temporarily disabled. Add policies when ready
            // services.AddAuthorization();

            services.AddRazorPages()
                    .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Identity/Account/Manage/");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Identity/Account/Logout");
                    });

            // OpenAPI with docs
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CAM-API", Version = "v1" });
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
                options.AccessDeniedPath = $"/Account/AccessDenied";
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
            app.UseSession();
            app.UseRouting();
            app.UseSerilogRequestLogging();

            // Auth BEFORE ENDPOINTS
            app.UseAuthentication();
            app.UseAuthorization();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // OpenAPI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CAM API V1");
            });

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action}/{id?}",
                        defaults: new { controller = "home", Action = "index" }
                    );
                    endpoints.MapControllerRoute(
                        name: "maintenance",
                        pattern: "maintenance/{controller}/{action}/{id?}",
                        defaults: new { controller = "workorders", Action = "index" }
                    );
                    endpoints.MapRazorPages();
                });
            // Hangfire job scheduling
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
            HangfireScheduler.ScheduleRecurringJobs();
        }
    }
}