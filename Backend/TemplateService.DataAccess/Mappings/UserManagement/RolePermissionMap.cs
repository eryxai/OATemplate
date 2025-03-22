#region Using ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateService.Core.Common;
using TemplateService.Entity.Entities;
using TemplateService.DataAccess.Seed;
using TemplateService.Common.Enums;
#endregion

/*


*/
namespace TemplateService.DataAccess.Mappings
{
    /// <summary>
    /// Provides a configuration for entity 
    /// RolePermission
    /// that will called when model created.
    /// </summary>
    public class RolePermissionMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<RolePermission>
    {
        #region Methods

        /// <summary>
        /// Configures the entity of type TEntity.
        /// </summary>
        /// <param name="builder">
        /// The builder to be used to configure the entity type.
        /// </param>
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable($"{typeof(RolePermission).Name}s", ApplicationGlobalConfig.Schema.UserManagementSchema);

            #region Configure Fields

            #endregion

            #region Configure Relations
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
        private void Seed(EntityTypeBuilder<RolePermission> builder)
        {
            if (ApplicationGlobalConfig.EnableSeedOnMigration)
            {
                DateTime now = DateTime.Now;
                int idCounter = 0;
                List<RolePermission> entityList = new List<RolePermission>();

                foreach (var item in PermissionSeed.SeedList.OrderBy(x => x.Id))
                {
                    entityList.Add(new RolePermission
                    {
                        Id = ++idCounter,
                        RoleId = 1,
                        PermissionId = item.Id,
                        CreatedByUserId = 1,
                        CreationDate = now,

                    });
                }

                builder.HasData(entityList);
            }
        }
        #endregion
    }
}
