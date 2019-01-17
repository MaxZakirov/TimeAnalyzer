using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using TimeAnalyzer.Core.Activities;
using TimeAnalyzer.Core.Interfaces;
using TimeAnalyzer.Core.TimeReports;
using TimeAnalyzer.Core.Users;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Models;
using TimeAnalyzer.Persistence;
using TimeAnalyzer.Persistence.DapperRepositories;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddCors();
            services.AddMvc();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            ConfigureJWTAuthentication(services, key);

            RegisterAppDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseExceptionHandler("/Home/Error");
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        private void RegisterAppDependencies(IServiceCollection services)
        {
            services.AddTransient(typeof(IDapperQueryExecuter<>), typeof(DapperQueryExecuter<>));
            services.AddTransient<ITransactableQueryExecuterFactory, TransactableQueryExecuterFactory>();
            services.AddTransient<IDapperRepositoriesFactory, DapperRepositoriesFactory>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITimeReportRepository, TimeReportRepository>();
            services.AddTransient<ITimeReportService, TimeReportService>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<ITimeReportService, TimeReportService>();
            services.AddTransient<ITimeReportServiceFactory, TimeReportServiceFactory>();
            services.AddTransient<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddTransient<ISuggestionsService, SuggestionsService>();
            services.AddTransient<IReferenceLoader, ReferenceLoader>();
        }

        private void ConfigureJWTAuthentication(IServiceCollection services, byte[] key)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
