#region Using ...
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Core.Common;
using Framework.Core.Models;
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
    /// type RolesController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        #region Data Members
        private readonly IRolesService _RolesService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// RolesController.
        /// </summary>
        /// <param name="RolesService"></param>
        public RolesController(
            IRolesService RolesService
            )
        {
            this._RolesService = RolesService;
        }
        #endregion

        #region Actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [Route("search")]
        [HttpPost]
        [JwtAuthentication(Permissions.RoleList)]
        public async Task<GenericResult<IList<RoleLightViewModel>>> Search(RoleSearchModel searchModel)
        {
            var result = await this._RolesService.Search(searchModel);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [Route("export-excel")]
        [HttpPost]
        [JwtAuthentication(Permissions.RoleList)]
        public async Task<IActionResult> ExportExcel(RoleSearchModel searchModel)
        {
            var exportedData = await this._RolesService.ExportExcel(searchModel);

            #region mime type
            //  "application/pdf" // for pdf 
            //  "application/vnd.openxmlformats-officedocument.wordprocessingml.document" // for Word doc
            //  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" // for excel
            #endregion

            return File(exportedData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeSalarys.xlsx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [Route("export-pdf")]
        [HttpPost]
        [JwtAuthentication(Permissions.RoleList)]
        public async Task<IActionResult> ExportPDF(RoleSearchModel searchModel)
        {
            var exportedData = await this._RolesService.ExportExcel(searchModel);

            return File(exportedData, "application/pdf", "EmployeeSalarys.pdf");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        [JwtAuthentication(Permissions.RoleList)]
        public async Task<GenericResult<IList<RoleViewModel>>> GetAsync()
        {
            var result = await this._RolesService.GetAsync();
            return result;
        }

        [Route("GetLookup")]
        [HttpGet]
        [JwtAuthentication(Permissions.RoleList)]
        public async Task<IList<RoleLookupViewModel>> GetLookup()
        {
            var result = await this._RolesService.GetLookup();
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
        [JwtAuthentication(Permissions.RoleList)]
        public async Task<GenericResult<IList<RoleViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._RolesService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        [JwtAuthentication(Permissions.RoleEdit)]
        public async Task<RoleViewModel> GetAsync(long id)
        {
            var result = await this._RolesService.GetAsync(id);
            return result;
        }


        /// <summary>
        /// Adds a new Role to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        [JwtAuthentication(Permissions.RoleAdd)]
        public async Task<RoleViewModel> AddAsync(RoleViewModel model)
        {
            var result = await this._RolesService.AddAsync(model);
            return result;
        }

        /// <summary>
        /// updates Role to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        [JwtAuthentication(Permissions.RoleEdit)]
        public async Task<RoleViewModel> UpdateAsync(RoleViewModel model)
        {
            var result = await this._RolesService.UpdateAsync(model);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}")]
        [HttpPost]
        [JwtAuthentication(Permissions.RoleDelete)]
        public async Task DeleteAsync(long id)
        {
            await this._RolesService.DeleteAsync(id);
        }


        [Route("get-all-un-selected-roles-for-user/{userId}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<IList<RoleLightViewModel>> GetAllUnSelectedRolesForUserAsync(long userId)
        {
            var result =   this._RolesService.GetAllUnSelectedRolesForUser(userId);
            return result;
        }

        [Route("get-role-permissions/{roleId}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<RolePermissionListViewModel> GetRolePermission(long roleId)
        {
            var result = await this._RolesService.GetRolePermission(roleId);
            return result;
        }

        [Route("update-role-permissions")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<RolePermissionListViewModel> UpdateRolePermissionAsync([FromBody] RolePermissionListViewModel model)
        {
            var result = await this._RolesService.UpdateRolePermissionAsync(model);
            return result;
        }


        [Route("searchLookup")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<GenericResult<IList<RoleLookupViewModel>>> SearchLookup(RoleLookupSearchModel searchModel)
        {
            var result = await this._RolesService.SearchLookup(searchModel);
            return result;
        }

        [Route("details/{id}")]
        [HttpGet]
        [JwtAuthentication(Permissions.RoleList)]
        public async Task<RoleDetailViewModel> GetDetails(long id)
        {
            var result = await this._RolesService.GetDetailsAsync(id);
            return result;
        }

        #endregion
    }
}
