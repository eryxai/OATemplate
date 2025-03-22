#region Using ...
using TemplateService.Core.Models.ViewModels;
using TemplateService.Entity.Entities;
using Microsoft.AspNetCore.Builder;

#endregion


namespace TemplateService.Core
{
    /* 
	 * See: https://code-maze.com/automapper-net-core/ 
	 */

    /// <summary>
    /// Provides a named configuration for maps. 
    /// Naming conventions become scoped per 
    /// profileStockItem, StockItemListViewModel
    /// </summary>
    public class Profile : AutoMapper.Profile
    {
        #region Properties
        public static IApplicationBuilder ApplicationBuilder { get; set; }
        #endregion


        #region Constructors
        /// <summary>
        /// Initializes a new instance from type 
        /// Profile.
        /// </summary>
        public Profile()
        {
            #region User
            CreateMap<User, UserViewModel>()

               .ReverseMap();

            CreateMap<User, UserLightViewModel>().ReverseMap();
            CreateMap<User, UserLookupViewModel>().ReverseMap();
            CreateMap<User, UserViewViewModel>().ReverseMap();
            CreateMap<User, UserDetailViewModel>().ReverseMap();
            #endregion

            #region RolePermission
            CreateMap<RolePermission, RolePermissionViewModel>().ReverseMap();
            CreateMap<RolePermission, RolePermissionLightViewModel>().ReverseMap();
            CreateMap<RolePermission, RolePermissionLookupViewModel>().ReverseMap();
            CreateMap<RolePermission, RolePermissionViewViewModel>().ReverseMap();
            #endregion

            #region Role
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Role, RoleDetailViewModel>().ReverseMap();
            CreateMap<Role, RoleLightViewModel>().ReverseMap();
            CreateMap<Role, RoleLookupViewModel>().ReverseMap();
            #endregion

            #region Role







            CreateMap<Role, RoleViewViewModel>()
                  .ForMember(member => member.Id, opt => opt.MapFrom(src => (src.Id)))
                  .ForMember(member => member.NameArabic, opt => opt.MapFrom(src => src.NameAr))
                  .ForMember(member => member.NameEnglish, opt => opt.MapFrom(src => src.NameEn))
                  //.ForMember(member => member.Code, opt => opt.MapFrom(src => (src.Code)))
                  //.ForMember(member => member.Date, opt => opt.MapFrom(src => (src.Date)))
                  .ForMember(member => member.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
                  .ForMember(member => member.DescriptionAr, opt => opt.MapFrom(src => src.DescriptionAr))
                  .ReverseMap();
            CreateMap<Role, RoleViewViewModel>()
                   .ForMember(member => member.Id, opt => opt.MapFrom(src => (src.Id)))
                   .ForMember(member => member.NameArabic, opt => opt.MapFrom(src => src.NameAr))
                   .ForMember(member => member.NameEnglish, opt => opt.MapFrom(src => src.NameEn))
                   //.ForMember(member => member.Code, opt => opt.MapFrom(src => (src.Code)))
                   //.ForMember(member => member.Date, opt => opt.MapFrom(src => (src.Date)))
                   .ForMember(member => member.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
                   .ForMember(member => member.DescriptionAr, opt => opt.MapFrom(src => src.DescriptionAr))
                   .ReverseMap();
            #endregion

            #region Permission
            CreateMap<Permission, PermissionViewModel>()
             .ForMember(member => member.Id, opt => opt.MapFrom(src => (src.Id)))
             .ForMember(member => member.Code, opt => opt.MapFrom(src => (src.Code)))
             .ForMember(member => member.IsActive, opt => opt.MapFrom(src => (src.IsActive)))
             .ReverseMap();

            CreateMap<Permission, PermissionLightViewModel>()
            .ForMember(member => member.Id, opt => opt.MapFrom(src => (src.Id)))
            .ForMember(member => member.Code, opt => opt.MapFrom(src => (src.Code)))
            .ForMember(member => member.IsActive, opt => opt.MapFrom(src => (src.IsActive)))
            .ReverseMap();

            CreateMap<Permission, PermissionLookupViewModel>()
            .ForMember(member => member.Id, opt => opt.MapFrom(src => (src.Id)))
            .ReverseMap();

            CreateMap<Permission, PermissionViewViewModel>()
            .ForMember(member => member.Id, opt => opt.MapFrom(src => (src.Id)))
            .ForMember(member => member.NameArabic, opt => opt.MapFrom(src => src.NameAr))
            .ForMember(member => member.NameEnglish, opt => opt.MapFrom(src => src.NameEn))
            .ForMember(member => member.Code, opt => opt.MapFrom(src => (src.Code)))
            .ForMember(member => member.IsActive, opt => opt.MapFrom(src => (src.IsActive)))
            .ForMember(member => member.DescriptionArabic, opt => opt.MapFrom(src => src.DescriptionAr))
            .ForMember(member => member.DescriptionEnglish, opt => opt.MapFrom(src => src.DescriptionEn))
            .ReverseMap();

            #endregion

            #region UserLogin
            CreateMap<UserLogin, UserLoginViewModel>()
             .ReverseMap();

            CreateMap<UserLogin, UserLoginLightViewModel>()
            .ForMember(member => member.UserUsername, opt => opt.MapFrom(src => (src.User.Username)))
            .ReverseMap();

            CreateMap<UserLogin, UserLoginLookupViewModel>()
            .ReverseMap();

            CreateMap<UserLogin, UserLoginViewViewModel>()
            .ForMember(member => member.UserUsername, opt => opt.MapFrom(src => (src.User.Username)))
            .ReverseMap();
            #endregion

            #region UserPermission
            CreateMap<UserPermission, UserPermissionViewModel>().ReverseMap();

            CreateMap<UserPermission, UserPermissionLightViewModel>()
            .ForMember(member => member.UserUsername, opt => opt.MapFrom(src => (src.User.Username)))
            .ReverseMap();

            CreateMap<UserPermission, UserPermissionLookupViewModel>().ReverseMap();

            CreateMap<UserPermission, UserPermissionViewViewModel>()
            .ForMember(member => member.UserUsername, opt => opt.MapFrom(src => (src.User.Username)))
            .ReverseMap();
            #endregion

            #region UserRole
            CreateMap<UserRole, UserRoleViewModel>().ReverseMap();

            CreateMap<UserRole, UserRoleLightViewModel>()
            .ForMember(member => member.UserUsername, opt => opt.MapFrom(src => (src.User.Username)))
            .ReverseMap();

            CreateMap<UserRole, UserRoleLookupViewModel>().ReverseMap();

            CreateMap<UserRole, UserRoleViewViewModel>()
            .ForMember(member => member.UserUsername, opt => opt.MapFrom(src => (src.User.Username)))
            .ReverseMap();
            #endregion

         

            #region Experience
            CreateMap<Experience, ExperienceViewModel>().ReverseMap();

            CreateMap<Experience, ExperienceLightViewModel>()
            .ReverseMap();

            CreateMap<Experience, ExperienceLookupViewModel>().ReverseMap();

            CreateMap<Experience, ExperienceViewViewModel>()
            .ReverseMap();
            #endregion
        }

        #endregion

    }
}

