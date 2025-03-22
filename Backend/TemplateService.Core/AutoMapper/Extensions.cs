#region Using ...
using TemplateService.Core.Models.ViewModels;
using TemplateService.Entity.Entities;


#endregion

/*
 
 
 */
namespace TemplateService.Core
{
    /// <summary>
    /// Provides an extention methods that used for mapping.
    /// </summary>
    public static class Extensions
    {
        #region User
        public static User ToEntity(this UserViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserViewModel, User>(model);
            return result;
        }
        public static UserViewModel ToModel(this User entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<User, UserViewModel>(entity);
            return result;
        }
        public static UserDetailViewModel ToDetailModel(this User entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<User, UserDetailViewModel>(entity);
            return result;
        }
        public static UserViewViewModel ToViewViewModel(this User entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<User, UserViewViewModel>(entity);
            return result;
        }
        public static UserLightViewModel ToLightModel(this User entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<User, UserLightViewModel>(entity);
            return result;
        }
        public static UserLookupViewModel ToLookupModel(this User entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<User, UserLookupViewModel>(entity);
            return result;
        }

        #endregion

        #region RolePermission
        public static RolePermission ToEntity(this RolePermissionViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<RolePermissionViewModel, RolePermission>(model);
            return result;
        }
        public static RolePermissionViewModel ToModel(this RolePermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<RolePermission, RolePermissionViewModel>(entity);
            return result;
        }
        public static RolePermissionLightViewModel ToLightModel(this RolePermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<RolePermission, RolePermissionLightViewModel>(entity);
            return result;
        }
        public static RolePermissionLookupViewModel ToLookupModel(this RolePermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<RolePermission, RolePermissionLookupViewModel>(entity);
            return result;
        }
        public static RolePermissionViewViewModel ToViewViewModel(this RolePermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<RolePermission, RolePermissionViewViewModel>(entity);
            return result;
        }
        #endregion

        #region Role
        public static Role ToEntity(this RoleViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<RoleViewModel, Role>(model);
            return result;
        }
        public static RoleDetailViewModel ToDetailModel(this Role entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Role, RoleDetailViewModel>(entity);
            return result;
        }
        public static RoleViewModel ToModel(this Role entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Role, RoleViewModel>(entity);
            return result;
        }
        public static RoleLightViewModel ToLightModel(this Role entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Role, RoleLightViewModel>(entity);
            return result;
        }
        //public static UserLightViewModel ToLightModel(this User entity, AutoMapper.IMapper mapper)
        //{
        //    var result = mapper.Map<Role, UserLightViewModel>(entity);
        //    return result;
        //}
        public static RoleLookupViewModel ToLookupModel(this Role entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Role, RoleLookupViewModel>(entity);
            return result;
        }
        public static RoleViewViewModel ToViewViewModel(this Role entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Role, RoleViewViewModel>(entity);
            return result;
        }
        #endregion

        #region Permission
        public static Permission ToEntity(this PermissionViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<PermissionViewModel, Permission>(model);
            return result;
        }
        public static PermissionViewModel ToModel(this Permission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Permission, PermissionViewModel>(entity);
            return result;
        }
        public static PermissionLightViewModel ToLightModel(this Permission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Permission, PermissionLightViewModel>(entity);
            return result;
        }
        public static PermissionLookupViewModel ToLookupModel(this Permission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Permission, PermissionLookupViewModel>(entity);
            return result;
        }
        public static PermissionViewViewModel ToViewViewModel(this Permission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Permission, PermissionViewViewModel>(entity);
            return result;
        }
        #endregion

        #region UserLogin
        public static UserLogin ToEntity(this UserLoginViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserLoginViewModel, UserLogin>(model);
            return result;
        }
        public static UserLoginViewModel ToModel(this UserLogin entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserLogin, UserLoginViewModel>(entity);
            return result;
        }
        public static UserLoginLightViewModel ToLightModel(this UserLogin entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserLogin, UserLoginLightViewModel>(entity);
            return result;
        }
        public static UserLoginLookupViewModel ToLookupModel(this UserLogin entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserLogin, UserLoginLookupViewModel>(entity);
            return result;
        }
        public static UserLoginViewViewModel ToViewViewModel(this UserLogin entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserLogin, UserLoginViewViewModel>(entity);
            return result;
        }
        #endregion

        #region UserPermission
        public static UserPermission ToEntity(this UserPermissionViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserPermissionViewModel, UserPermission>(model);
            return result;
        }
        public static UserPermissionViewModel ToModel(this UserPermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserPermission, UserPermissionViewModel>(entity);
            return result;
        }
        public static UserPermissionLightViewModel ToLightModel(this UserPermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserPermission, UserPermissionLightViewModel>(entity);
            return result;
        }
        public static UserPermissionLookupViewModel ToLookupModel(this UserPermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserPermission, UserPermissionLookupViewModel>(entity);
            return result;
        }
        public static UserPermissionViewViewModel ToViewViewModel(this UserPermission entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserPermission, UserPermissionViewViewModel>(entity);
            return result;
        }

        #endregion
    
        #region UserRole
        public static UserRole ToEntity(this UserRoleViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserRoleViewModel, UserRole>(model);
            return result;
        }
        public static UserRoleViewModel ToModel(this UserRole entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserRole, UserRoleViewModel>(entity);
            return result;
        }
        public static UserRoleLightViewModel ToLightModel(this UserRole entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserRole, UserRoleLightViewModel>(entity);
            return result;
        }
        public static UserRoleLookupViewModel ToLookupModel(this UserRole entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserRole, UserRoleLookupViewModel>(entity);
            return result;
        }
        public static UserRoleViewViewModel ToViewViewModel(this UserRole entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<UserRole, UserRoleViewViewModel>(entity);
            return result;
        }
        #endregion
  
  
        #region Experience
        public static Experience ToEntity(this ExperienceViewModel model, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<ExperienceViewModel, Experience>(model);
            return result;
        }
        public static ExperienceViewModel ToModel(this Experience entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Experience, ExperienceViewModel>(entity);
            return result;
        }
        public static ExperienceLightViewModel ToLightModel(this Experience entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Experience, ExperienceLightViewModel>(entity);
            return result;
        }
        public static ExperienceViewViewModel ToViewViewModel(this Experience entity, AutoMapper.IMapper mapper)
        {
            var result = mapper.Map<Experience, ExperienceViewViewModel>(entity);
            return result;
        }
        #endregion
    }
}


