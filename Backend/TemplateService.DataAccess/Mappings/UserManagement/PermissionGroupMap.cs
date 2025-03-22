#region Using ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateService.Entity.Entities;
using TemplateService.Core.Common;

#endregion

namespace TemplateService.DataAccess.Mappings
{
    public class PermissionGroupMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<PermissionGroup>
    {
        //public static List<PermissionGroup> permissionGroups;

        #region Methods

        /// <summary>
        /// Configures the entity of type TEntity.
        /// </summary>
        /// <param name="builder">
        /// The builder to be used to configure the entity type.
        /// </param>
        public void Configure(EntityTypeBuilder<PermissionGroup> builder)
        {
            builder.ToTable($"{typeof(PermissionGroup).Name}s", ApplicationGlobalConfig.Schema.UserManagementSchema);

            #region Configure Fields
            builder.Property(prop => prop.NameAr).HasMaxLength(200);
            builder.Property(prop => prop.NameEn).HasMaxLength(200);
            //builder.Property(prop => prop.Code).HasMaxLength(50);


            builder.HasIndex(prop => prop.NameEn);
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
        private void Seed(EntityTypeBuilder<PermissionGroup> builder)
        {
            if (ApplicationGlobalConfig.EnableSeedOnMigration)
            {
                List<PermissionGroup> entityList = new List<PermissionGroup>();
                #region UserManagements
                PermissionGroup permissionGroupUserManagements = new PermissionGroup
                {
                    Id = 1,
                    CreationDate = new DateTime(2023, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    NameEn = "Users Managements",
                    NameAr = "ادارة المستخدمين",
                    IsActive = true,
                    ParentId = null
                };
                entityList.Add(permissionGroupUserManagements);

                PermissionGroup permissionGroupRole = new PermissionGroup
                {
                    Id = 2,
                    CreationDate = new DateTime(2023, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    NameEn = "Roles",
                    NameAr = "الادوار",
                    IsActive = true,
                    ParentId = 1
                };
                entityList.Add(permissionGroupRole);


                PermissionGroup permissionGroupUsers = new PermissionGroup
                {
                    Id = 3,
                    CreationDate = new DateTime(2023, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    NameEn = "Users  ",
                    NameAr = "المستخدمين",
                    IsActive = true,
                    ParentId = 1

                };
                entityList.Add(permissionGroupUsers); 
                #endregion

                #region lookups
                PermissionGroup permissionGroupLookups = new PermissionGroup
                {
                    Id = 4,
                    CreationDate = new DateTime(2023, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    NameEn = "Lookups",
                    NameAr = "القوائم",
                    IsActive = true,
                    ParentId = null
                };
                entityList.Add(permissionGroupLookups); 
                
           
                #region Experience
                PermissionGroup permissionGroupExperience = new PermissionGroup
                {
                    Id = 5,
                    CreationDate = new DateTime(2023, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    NameEn = "Experience",
                    NameAr = "خبرة",
                    IsActive = true,
                    ParentId = 4
                };
                entityList.Add(permissionGroupExperience);

                #endregion

                #endregion




                //permissionGroups = entityList;
                builder.HasData(entityList);
            }
        }
        #endregion
    }
}
