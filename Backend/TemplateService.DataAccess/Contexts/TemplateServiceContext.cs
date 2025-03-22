#region Using ...
using Framework.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TemplateService.DataAccess.Mappings;
using TemplateService.Entity.Entities;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using TemplateService.DataAccess.Mappings;
using TemplateService.Entity.Entities;
using TemplateService.DataAccess.Helper;

#endregion

/*
 
 
 */
namespace TemplateService.DataAccess.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateServiceContext : DbContext
    {

        private readonly AuditSaveChangesInterceptor _auditInterceptor;

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type 
        /// TemplateServiceContext.
        /// </summary>
        /// <param name="options"></param>
        public TemplateServiceContext(DbContextOptions options
            , AuditSaveChangesInterceptor auditInterceptor)
            : base(options)
        {
            _auditInterceptor = auditInterceptor;
        }

        public TemplateServiceContext()

        {

        }


        #endregion

        #region Methods
        /// <summary>
        /// Override this method to further configure the model that was discovered by convention
        /// from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties
        /// on your derived context. The resulting model may be cached and re-used for subsequent
        /// instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context.Databases(and
        /// other extensions) typically define extension methods on this object that allow
        /// you to configure aspects of the model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RolePermissionMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new PermissionMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserPermissionMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new PermissionGroupMap());
            modelBuilder.ApplyConfiguration(new ExperienceMap());


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);
            optionsBuilder.AddInterceptors(_auditInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region Properties
        public DbSet<User> Users { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroup { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Experience> Experience { get; set; }



        public DbSet<UserAuditLog> UserAuditLogs { get; set; }

        #endregion
    }
}
