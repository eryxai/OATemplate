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
    /// type UserRolesController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        #region Data Members
        private readonly IUserRolesService _UserRolesService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserRolesController.
        /// </summary>
        /// <param name="UserRolesService"></param>
        public UserRolesController(
            IUserRolesService UserRolesService
            )
        {
            this._UserRolesService = UserRolesService;
        }
        #endregion

        #region Actions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        public async Task<GenericResult<IList<UserRoleViewModel>>> GetAsync()
        {
            var result = await this._UserRolesService.GetAsync();
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
        public async Task<GenericResult<IList<UserRoleViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._UserRolesService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        public async Task<UserRoleViewModel> GetAsync(long  id)
        {
            var result = await this._UserRolesService.GetAsync(id);
            return result;
        }

        /// <summary>
        /// Adds a new UserRole to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<UserRoleViewModel> AddAsync(UserRoleViewModel model)
        {
            var result = await this._UserRolesService.AddAsync(model);
            return result;
        }

        /// <summary>
        /// updates UserRole to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        public async Task<UserRoleViewModel> UpdateAsync(UserRoleViewModel model)
        {
            var result = await this._UserRolesService.UpdateAsync(model);
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
            await this._UserRolesService.DeleteAsync(id);
        }


        #endregion
    }
}
