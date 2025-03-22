#region Using ...
using Framework.Core.Common;
using Framework.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateService.Business.Common;
using TemplateService.Business.Services;
using TemplateService.BusinessLogic.Common;
using TemplateService.Core.IRepositories;
using TemplateService.Core.IServices;
using TemplateService.Core.Common;
using TemplateService.DataAccess.Contexts;
using TemplateService.DataAccess.Repositories;
using TemplateService.DataAccess.Helper;

#endregion

/*
 
 
 */
namespace TemplateService.DI
{
    /// <summary>
    /// 
    /// </summary>
    public class ContainerConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            #region Register AutoMapper
            services.AddAutoMapper(typeof(Core.Profile).Assembly);
            #endregion
            services.AddScoped<AuditSaveChangesInterceptor>();
            services.AddHttpContextAccessor();

            services.AddScoped<ILoggerService, LoggerService>();

            #region Add Db Context
            services.AddDbContext<TemplateServiceContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString:TemplateServiceConnection"], b => b.MigrationsAssembly("TemplateService.DataAccess"))
              
;
            });
            #endregion

            #region Register TemplateServiceContext
            services.AddScoped<TemplateServiceContext, TemplateServiceContext>();
            #endregion


            #region Register Repositories

            services.AddScoped<IUsersRepositoryAsync, UsersRepositoryAsync>();
            services.AddScoped<IRolePermissionsRepositoryAsync, RolePermissionsRepositoryAsync>();
            services.AddScoped<IRolesRepositoryAsync, RolesRepositoryAsync>();
            services.AddScoped<IPermissionsRepositoryAsync, PermissionsRepositoryAsync>();
            services.AddScoped<IUserLoginsRepositoryAsync, UserLoginsRepositoryAsync>();
            services.AddScoped<IUserPermissionsRepositoryAsync, UserPermissionsRepositoryAsync>();
            services.AddScoped<IUserRolesRepositoryAsync, UserRolesRepositoryAsync>();
            services.AddScoped<IPermissionGroupRepositoryAsync, PermissionGroupRepositoryAsync>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();

            services.AddScoped<IExperienceRepositoryAsync, ExperienceRepositoryAsync>();




            #endregion

            #region Register Services


            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolePermissionsService, RolePermissionsService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IUserLoginsService, UserLoginsService>();
            services.AddScoped<ILocalizationService, LocalizationService>();

            services.AddScoped<IUserPermissionsService, UserPermissionsService>();
            services.AddScoped<IUserRolesService, UserRolesService>();

            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<IMailNotification, MailNotificationService>();

            services.AddScoped<IExperienceService, ExperienceService>();


            #endregion
        }
        public static void ConfigurePublic(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            #region Register AutoMapper
            services.AddAutoMapper(typeof(Core.Profile).Assembly);
            #endregion

            services.AddHttpContextAccessor();

            services.AddScoped<ILoggerService, LoggerService>();

            #region Add Db Context
            services.AddDbContext<TemplateServiceContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString:TemplateServiceConnection"], b => b.MigrationsAssembly("TemplateService.DataAccess"));
            });
            #endregion

            #region Register TemplateServiceContext
            services.AddScoped<TemplateServiceContext, TemplateServiceContext>();
            #endregion

            #region Register Services


            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<IMailNotification, MailNotificationService>();
            #endregion
        }
    }
}
