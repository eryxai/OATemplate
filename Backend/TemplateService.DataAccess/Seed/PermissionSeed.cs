using TemplateService.Common.Enums;
using TemplateService.Core.Common;
using TemplateService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateService.DataAccess.Seed
{
    public class PermissionSeed
    {
        static PermissionSeed()
        {
            Seed();
        }

        private static void Seed()
        {
            SeedList = new List<Permission>();

            if (ApplicationGlobalConfig.EnableSeedOnMigration)
            {
                // Seed Permissions
                List<Permission> entityList = new List<Permission>();

                #region roles
                Permission permissionRoleList = new Permission
                {
                    Id = (int)Permissions.RoleList,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.RoleList,
                    NameEn = "View Role List",
                    NameAr = "قائمة الادوار",
                    IsActive = true,
                    PermissionGroupId = 2
                };
                entityList.Add(permissionRoleList);

                Permission permissionRoleAdd = new Permission
                {
                    Id = (int)Permissions.RoleAdd,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.RoleAdd,
                    NameEn = "Add Role",
                    NameAr = "إضافة دور",
                    IsActive = true,
                    PermissionGroupId = 2
                };
                entityList.Add(permissionRoleAdd);

                Permission permissionpermissionRoleEdit = new Permission
                {
                    Id = (int)Permissions.RoleEdit,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.RoleEdit,
                    NameEn = "Update Role",
                    NameAr = "تعديل دور",
                    IsActive = true,
                    PermissionGroupId = 2

                };
                entityList.Add(permissionpermissionRoleEdit);

                Permission permissionRoleView = new Permission
                {
                    Id = (int)Permissions.RoleView,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.RoleView,
                    NameEn = "View Role",
                    NameAr = "تفاصيل الدور",
                    IsActive = true,
                    PermissionGroupId = 2

                };
                entityList.Add(permissionRoleView);

                Permission permissionRoleDelete = new Permission
                {
                    Id = (int)Permissions.RoleDelete,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.RoleDelete,
                    NameEn = "Delete Role",
                    NameAr = "حذف دور",
                    IsActive = true,
                    PermissionGroupId = 2

                };
                entityList.Add(permissionRoleDelete);

                #endregion

                #region User
                Permission permissionAdduser = new Permission
                {
                    Id = (int)Permissions.UserAdd,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.UserAdd,
                    NameEn = "Add User",
                    NameAr = "إضافة مستخدم",
                    IsActive = true,
                    PermissionGroupId = 3
                };
                entityList.Add(permissionAdduser);

                Permission permissionupdateuser = new Permission
                {
                    Id = (int)Permissions.UserEdit,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.UserEdit,
                    NameEn = "Edit User",
                    NameAr = "تعديل مستخدم",
                    IsActive = true,
                    PermissionGroupId = 3
                };
                entityList.Add(permissionupdateuser);


                Permission permissionviewuser = new Permission
                {
                    Id = (int)Permissions.UserView,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.UserView,
                    NameEn = "View User",
                    NameAr = "عرض تفاصيل مستخدم",
                    IsActive = true,
                    PermissionGroupId = 3
                };
                entityList.Add(permissionviewuser);

                Permission permissionDeleteuser = new Permission
                {
                    Id = (int)Permissions.UserDelete,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.UserDelete,
                    NameEn = "Delete User",
                    NameAr = " حذف مستخدم",
                    IsActive = true,
                    PermissionGroupId = 3
                };
                entityList.Add(permissionDeleteuser);


                Permission permissionListuser = new Permission
                {
                    Id = (int)Permissions.UserList,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.UserList,
                    NameEn = "View Users List",
                    NameAr = "قائمة المستخدمين",
                    IsActive = true,
                    PermissionGroupId = 3
                };
                entityList.Add(permissionListuser);
                #endregion

        
                #region Experience
                Permission permissionAddExperience = new Permission
                {
                    Id = (int)Permissions.ExperienceAdd,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.ExperienceAdd,
                    NameEn = "Add Experience",
                    NameAr = "إضافة خبرة",
                    IsActive = true,
                    PermissionGroupId = 5
                };
                entityList.Add(permissionAddExperience);
                Permission permissionEditExperience = new Permission
                {
                    Id = (int)Permissions.ExperienceEdit,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.ExperienceEdit,
                    NameEn = "Edit Experience",
                    NameAr = "تعديل خبرة",
                    IsActive = true,
                    PermissionGroupId = 5
                };
                entityList.Add(permissionEditExperience);
                Permission permissionViewExperience = new Permission
                {
                    Id = (int)Permissions.ExperienceView,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.ExperienceView,
                    NameEn = "View Experience",
                    NameAr = "عرض خبرة",
                    IsActive = true,
                    PermissionGroupId = 5
                };
                entityList.Add(permissionViewExperience);
                Permission permissionDeleteExperience = new Permission
                {
                    Id = (int)Permissions.ExperienceDelete,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.ExperienceDelete,
                    NameEn = "Delete Experience",
                    NameAr = "حذف خبرة",
                    IsActive = true,
                    PermissionGroupId = 5
                };
                entityList.Add(permissionDeleteExperience);
                Permission permissionListExperience = new Permission
                {
                    Id = (int)Permissions.ExperienceList,
                    CreationDate = new DateTime(2024, 9, 8, 13, 15, 24, 581, DateTimeKind.Local),
                    Code = (int)Permissions.ExperienceList,
                    NameEn = "View Experience List",
                    NameAr = "قائمة خبرة",
                    IsActive = true,
                    PermissionGroupId = 5
                };
                entityList.Add(permissionListExperience);
                #endregion

                SeedList = entityList;
            }
        }

        public static List<Permission> SeedList { get; set; }
    }
}
