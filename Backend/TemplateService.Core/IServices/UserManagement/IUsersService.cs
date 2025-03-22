#region Using ...
using Framework.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateService.Core.IServices.Base;
using TemplateService.Core.Models.ViewModels;
using TemplateService.Common.Enums;
#endregion

/*


*/
namespace TemplateService.Core.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUsersService : IBaseService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ValidateModelAsync(UserViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        Task ValidateModelAsync(IEnumerable<UserViewModel> modelCollection);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<UserViewModel>>> GetAsync(BaseFilter baseFilter = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<GenericResult<IList<UserLightViewModel>>> Search(UserSearchModel searchModel);

        /// <summary>
        /// Gets a lookup from entity.
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<UserLookupViewModel>>> GetLookupAsync(BaseFilter baseFilter = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserViewModel> GetAsync(long id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserViewModel> AddAsync(UserViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserViewModel> UpdateAsync(UserViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        bool GetHasPermissionToChangePostedAccounts();

        bool IsUserHassPermission(long? userId, Permissions permissionItem);

        bool IsCurrentUserHassPermission(Permissions permissionItem);

        Task<UserPermissionListViewModel> GetUserPermissionAsync(long userId);

        Task<UserPermissionListViewModel> UpdateUserPermissionAsync(UserPermissionListViewModel model);

        Task<UserRoleListViewModel> GetUserRole(long userId);
        Task<UserRoleListViewModel> UpdateUserRole(UserRoleListViewModel model);

        UserLoggedInViewModel Login(LoginViewModel model);

        Task ChangePasswordAsync(ChangePasswordViewModel model);

        Task ResetPasswordAsync(long userId);

        MobileUserLoggedInViewModel MobileLogin(MobileLoginViewModel model);
        Task<CurrentUserViewModel> GetCurrentUser();

        Task<bool> ForgotPassword(ForgotPasswordModel forgotPasswordModel);
        Task<UserDetailViewModel> GetDetailsAsync(long id);
        Task<bool> CheckPermissionForList(long id);

        Task<bool> IsHidenPassword();
        #endregion
    }
}
