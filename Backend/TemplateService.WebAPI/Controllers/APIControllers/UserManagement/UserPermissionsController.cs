#region Using ...
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Core.Common;
using Framework.Core.Models;
using Microsoft.AspNetCore.Mvc;
using TemplateService.Core.IServices;
using TemplateService.Core.Models.ViewModels;
#endregion

/*


*/
namespace TemplateService.WebAPI.Controllers.APIControllers
{
    /// <summary>
    /// Providing an API controller that holds 
    /// end points to manage operations over 
    /// type UserPermissionsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController : ControllerBase
    {
        #region Data Members
        private readonly IUserPermissionsService _UserPermissionsService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserPermissionsController.
        /// </summary>
        /// <param name="UserPermissionsService"></param>
        public UserPermissionsController(
            IUserPermissionsService UserPermissionsService
            )
        {
            this._UserPermissionsService = UserPermissionsService;
        }
        #endregion

        #region Actions


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        public async Task<GenericResult<IList<UserPermissionViewModel>>> GetAsync()
        {
            var result = await this._UserPermissionsService.GetAsync();
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
        public async Task<GenericResult<IList<UserPermissionViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._UserPermissionsService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        public async Task<UserPermissionViewModel> GetAsync(long  id)
        {
            var result = await this._UserPermissionsService.GetAsync(id);
            return result;
        }

        /// <summary>
        /// Adds a new UserPermission to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<UserPermissionViewModel> AddAsync(UserPermissionViewModel model)
        {
            var result = await this._UserPermissionsService.AddAsync(model);
            return result;
        }



        /// <summary>
        /// updates UserPermission to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        public async Task<UserPermissionViewModel> UpdateAsync(UserPermissionViewModel model)
        {
            var result = await this._UserPermissionsService.UpdateAsync(model);
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}")]
        [HttpPost]
        public async Task DeleteAsync(long  id)
        {
            await this._UserPermissionsService.DeleteAsync(id);
        }


        #endregion
    }
}
