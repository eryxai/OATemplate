#region Using ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateService.Common.Enums;
using TemplateService.Core.Common;
using TemplateService.Entity.Entities;
using TemplateService.DataAccess.Seed;
#endregion

/*


*/
namespace TemplateService.DataAccess.Mappings
{
    /// <summary>
    /// Provides a configuration for entity 
    /// Permission
    /// that will called when model created.
    /// </summary>
    public class PermissionMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Permission>
    {
        public static List<Permission> permissions;

        #region Methods

        /// <summary>
        /// Configures the entity of type TEntity.
        /// </summary>
        /// <param name="builder">
        /// The builder to be used to configure the entity type.
        /// </param>
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable($"{typeof(Permission).Name}s", ApplicationGlobalConfig.Schema.UserManagementSchema);

            #region Configure Fields
            builder.Property(prop => prop.NameAr).HasMaxLength(200);
            builder.Property(prop => prop.NameEn).HasMaxLength(200);
            builder.Property(prop => prop.DescriptionAr).HasMaxLength(200);
            builder.Property(prop => prop.DescriptionEn).HasMaxLength(200);


            builder.HasIndex(prop => prop.Code);
            builder.HasIndex(prop => prop.NameEn);
            #endregion

            #region Configure Relations
            builder.HasMany(many => many.RolePermissions)
                .WithOne(one => one.Permission)
                .HasForeignKey(key => key.PermissionId);


            builder.HasMany(many => many.UserPermissions)
                .WithOne(one => one.Permission)
                .HasForeignKey(key => key.PermissionId);

            #endregion

            #region Ignore
            /* 
             * If do you want to initialize some data in 
             * this entity use this method: 
             * builder.Ignore(entity => entity.<PropertyName>);
             */
            #endregion

            #region Set Initial Data
            /* 
             * If do you want to initialize some data in
             * this entity use this method: 
             * builder.HasData(); 
             */
            this.Seed(builder);
            #endregion
        }

        /// <summary>
        /// Initialize databas entity with some data.
        /// </summary>
        /// <param name="builder"></param>
        private void Seed(EntityTypeBuilder<Permission> builder)
        {
            if (ApplicationGlobalConfig.EnableSeedOnMigration)
            {

                //int idCounter = 0;
               
                builder.HasData(PermissionSeed.SeedList);
            }
        }

        #endregion
    }
}
