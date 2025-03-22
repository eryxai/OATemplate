#region Using ...
using System;
using System.Linq;
using Framework.Core.Common;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TemplateService.DataAccess.Contexts;
using TemplateService.DI;
using TemplateService.WebAPI.Middlewares;
using TemplateService.Business.Helper.Socket;
using Microsoft.OpenApi.Models;
#endregion

/*
 
 
 */
// for JWT see: https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
namespace TemplateService.WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type 
        /// Startup.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        #endregion

        #region Methods
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #region AddHangfire
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(Configuration["ConnectionString:TemplateServiceConnection"]);
            });

            services.AddHangfireServer();

            #endregion

            #region AddSignalR
            services.AddSignalR();
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("allowall", builder =>
                {
                    builder.WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            Configuration["CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o)
                                .ToArray()
                        )
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            services.AddCors(setup => setup.AddPolicy("allowall", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Templates.API", Version = "v1" });
                //   c.InferSecuritySchemes();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer",
                            }
                        },
                        new string[]{}
                    }
                });
            });


            ContainerConfiguration.Configure(services, this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerService logger,
                IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Use Exception Middleware
            app.ConfigureExceptionHandler(logger);
            //app.ConfigureCustomExceptionMiddleware(); 
            #endregion
            app.UseStaticFiles();

            //Hangfire dashboard &server(Enable to use Hangfire instead of default job manager)
            app.UseHangfireDashboard("/hangfire", new DashboardOptions());
            TemplateService.Core.Profile.ApplicationBuilder = app;
            //  app.UseHangfireDashboard();
            var isEnableHangfireMigration = Configuration.GetSection("Hangfire").GetSection("isEnableHangfireMigration").Value;
            if (isEnableHangfireMigration == "True")
            {
                var SendGracePeriodNotification = Configuration.GetSection("Hangfire").GetSection("SendGracePeriodNotification").Value;
                //RecurringJob.AddOrUpdate<IAdvertisingService>("SendGraceRenewNotification", x => x.SendGraceRenewNotification(), SendGracePeriodNotification);
            }
            app.UseCors("allowall");


            app.UseStaticFiles();
            app.UseHttpsRedirection();

            var isEnableSwaagger = Configuration.GetSection("CommonSettings").GetSection("isEnableSwagger").Value;
            if (isEnableSwaagger == "True")
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotifyKCCHub>("/notifyTemplateHub");
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            try
            {
                UpdateDatabase(app);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TemplateServiceContext>())
                {
                    //context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
            }
        }
        #endregion
    }
}
