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
    /// type RolePermissionsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionsController : ControllerBase
    {
        #region Data Members
        private readonly IRolePermissionsService _RolePermissionsService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// RolePermissionsController.
        /// </summary>
        /// <param name="RolePermissionsService"></param>
        public RolePermissionsController(
            IRolePermissionsService RolePermissionsService
            )
        {
            this._RolePermissionsService = RolePermissionsService;
        }
        #endregion

        #region Actions


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        public async Task<GenericResult<IList<RolePermissionViewModel>>> GetAsync()
        {
            var result = await this._RolePermissionsService.GetAsync();
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
        public async Task<GenericResult<IList<RolePermissionViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._RolePermissionsService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        public async Task<RolePermissionViewModel> GetAsync(long  id)
        {
            var result = await this._RolePermissionsService.GetAsync(id);
            return result;
        }

        /// <summary>
        /// Adds a new RolePermission to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<RolePermissionViewModel> AddAsync(RolePermissionViewModel model)
        {
            var result = await this._RolePermissionsService.AddAsync(model);
            return result;
        }

        /// <summary>
        /// updates RolePermission to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        public async Task<RolePermissionViewModel> UpdateAsync(RolePermissionViewModel model)
        {
            var result = await this._RolePermissionsService.UpdateAsync(model);
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
            await this._RolePermissionsService.DeleteAsync(id);
        }

        #endregion
    }
}
