#region Using ...
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Core.Common;
using Framework.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateService.Common.Enums;
using TemplateService.Core.IServices;
using TemplateService.Core.Models.ViewModels;
using TemplateService.WebAPI.Auth;
#endregion

/*


*/
namespace TemplateService.WebAPI.Controllers.APIControllers
{
    /// <summary>
    /// Providing an API controller that holds 
    /// end points to manage operations over 
    /// type UsersController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Data Members
        private readonly IUsersService _UsersService;
        #endregion

        #region Constructors
        public UsersController(
            IUsersService UsersService
            )
        {
            this._UsersService = UsersService;
        }
        #endregion

        #region Actions

        #region Basic Function

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<GenericResult<IList<UserViewModel>>> GetAsync()
        {
            var result = await this._UsersService.GetAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("get-collection/{pageIndex}/{pageSize}/{sorting}")]
        [HttpGet]
        public async Task<GenericResult<IList<UserViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._UsersService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetLookup")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<GenericResult<IList<UserLookupViewModel>>> GetLookup()
        {
            var result = await this._UsersService.GetLookupAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("get-lookup-collection/{pageIndex}/{pageSize}/{sorting}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<GenericResult<IList<UserLookupViewModel>>> GetLookupAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._UsersService.GetLookupAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<UserViewModel> GetAsync(long id)
        {
            var result = await this._UsersService.GetAsync(id);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("CheckPermissionForList/{id}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<bool> CheckPermissionForList(long id)
        {
            var result = await this._UsersService.CheckPermissionForList(id);
            return result;
        }

        /// <summary>
        /// Adds a new User to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<UserViewModel> AddAsync(UserViewModel model)
        {
            var result = await this._UsersService.AddAsync(model);
            return result;
        }

        /// <summary>
        /// updates User to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<UserViewModel> UpdateAsync(UserViewModel model)
        {
            var result = await this._UsersService.UpdateAsync(model);
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}")]
        [HttpPost]
        [JwtAuthentication(Permissions.UserDelete)]
        public async Task DeleteAsync(long id)
        {
            await this._UsersService.DeleteAsync(id);
        }

        #endregion

        #region User Permission

        [Route("has-permission-to-change-posted-accounts")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<bool> GetHasPermissionToChangePostedAccountsAsync()
        {
            var result =   this._UsersService.GetHasPermissionToChangePostedAccounts();
            return result;
        }

        [Route("is-user-has-permission/{userId}/{permissionItem}")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<bool> IsUserHassPermissionAsync(long? userId, Permissions permissionItem)
        {
            var result =   this._UsersService.IsUserHassPermission(userId, permissionItem);
            return result;
        }


        [Route("is-current-user-has-permission/{permissionItem}")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<bool> IsCurrentUserHassPermissionAsync(Permissions permissionItem)
        {
            var result =   this._UsersService.IsCurrentUserHassPermission(permissionItem);
            return result;
        }

        [Route("get-user-permissions/{userId}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<UserPermissionListViewModel> GetUserPermissionAsync(long userId)
        {
            var result = await this._UsersService.GetUserPermissionAsync(userId);
            return result;
        }

        [Route("update-user-permissions")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<UserPermissionListViewModel> UpdateUserPermissionAsync([FromBody] UserPermissionListViewModel model)
        {
            var result = await this._UsersService.UpdateUserPermissionAsync(model);
            return result;
        }

        #endregion

        #region User Role

        [Route("get-user-roles/{userId}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<UserRoleListViewModel> GetUserRole(long userId)
        {
            var result = await this._UsersService.GetUserRole(userId);
            return result;
        }


        [Route("update-user-roles")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<UserRoleListViewModel> UpdateUserRole([FromBody] UserRoleListViewModel model)
        {
            var result = await this._UsersService.UpdateUserRole(model);
            return result;
        }

        #endregion

        #region Auth
        [AllowAnonymous]
        [Route("user-login")]
        [HttpPost]
        public IActionResult LoginInternal(LoginViewModel model)
        {
            UserLoggedInViewModel user = this._UsersService.Login(model);
            if (user != null)
            {

                return Ok(user);
            }
            else
            {
                return BadRequest("0001");
            }
        }


        [Route("change-password")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            await this._UsersService.ChangePasswordAsync(model);
            return Ok();
        }

        [Route("reset-password/{userId}")]
        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(long userId)
        {
            await this._UsersService.ResetPasswordAsync(userId);
            return Ok();
        }

        [Route("GetCurrentUser")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<CurrentUserViewModel> GetCurrentUser()
        {
            var result = await _UsersService.GetCurrentUser();

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [Route("search")]
        [HttpPost]
        [JwtAuthentication(Permissions.UserList)]
        public async Task<GenericResult<IList<UserLightViewModel>>> Search(UserSearchModel searchModel)
        {
            var result = await this._UsersService.Search(searchModel);
            return result;
        }

        [AllowAnonymous]
        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<bool> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            return await _UsersService.ForgotPassword(forgotPasswordModel);
        }


        //[AllowAnonymous]
        //[Route("login")]
        //[HttpPost]
        //public IActionResult Login(MobileLoginViewModel model)
        //{
        //    MobileUserLoggedInViewModel user = this._UsersService.MobileLogin(model);
        //    if (user != null)
        //    {

        //        return Ok(user);
        //    }
        //    else
        //    {
        //        return BadRequest("0001");
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("details/{id}")]
        [HttpGet]
        [JwtAuthentication(Permissions.UserView)]
        public async Task<UserDetailViewModel> GetDetails(long id)
        {
            var result = await this._UsersService.GetDetailsAsync(id);
            return result;
        }
        [Route("IsHidenPassword")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<bool> IsHidenPassword()
        {
            return(await _UsersService.IsHidenPassword());
        }

        #endregion

        #endregion
    }
}
