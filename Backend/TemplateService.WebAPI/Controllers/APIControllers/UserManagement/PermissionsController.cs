#region Using ...
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Core.Common;
using Framework.Core.Models;
using Microsoft.AspNetCore.Mvc;
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
    /// type PermissionsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        #region Data Members
        private readonly IPermissionsService _PermissionsService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// PermissionsController.
        /// </summary>
        /// <param name="PermissionsService"></param>
        public PermissionsController(
            IPermissionsService PermissionsService
            )
        {
            this._PermissionsService = PermissionsService;
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
       // [JwtAuthentication(Permissions.EnableDisablePermissions)]
        public async Task<GenericResult<IList<PermissionLightViewModel>>> Search(PermissionSearchModel searchModel)
        {
            var result = await this._PermissionsService.Search(searchModel);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [Route("export-excel")]
        [HttpPost]
       // [JwtAuthentication(Permissions.EnableDisablePermissions)]
        public async Task<IActionResult> ExportExcel(PermissionSearchModel searchModel)
        {
            var exportedData = await this._PermissionsService.ExportExcel(searchModel);

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
        //[JwtAuthentication(Permissions.EnableDisablePermissions)]
        public async Task<IActionResult> ExportPDF(PermissionSearchModel searchModel)
        {
            var exportedData = await this._PermissionsService.ExportExcel(searchModel);

            return File(exportedData, "application/pdf", "EmployeeSalarys.pdf");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        public async Task<GenericResult<IList<PermissionViewModel>>> GetAsync()
        {
            var result = await this._PermissionsService.GetAsync();
            return result;
        }


        [Route("GetAllPermissions")]
        [HttpGet]
        public async Task<IList<PermissionGroupLookupViewModel>> GetAllPermissions()
        {
            var result = await this._PermissionsService.GetAllPermissions();
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
        public async Task<GenericResult<IList<PermissionViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._PermissionsService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Route("get-light-collection")]
        //[HttpGet]
        //public async Task<GenericResult<IList<PermissionLightViewModel>>> GetLightAsync()
        //{
        //    var result = await this._PermissionsService.GetLightAsync();
        //    return result;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //[Route("get-light-collection/{pageIndex}/{pageSize}/{sorting}")]
        //[HttpGet]
        //public async Task<GenericResult<IList<PermissionLightViewModel>>> GetLightAsync(int? pageIndex, int? pageSize, string sorting)
        //{
        //    var result = await this._PermissionsService.GetLightAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
        //    return result;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Route("get-lookup-collection")]
        //[HttpGet]
        //public async Task<GenericResult<IList<PermissionLookupViewModel>>> GetLookupAsync()
        //{
        //    var result = await this._PermissionsService.GetLookupAsync();
        //    return result;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //[Route("get-lookup-collection/{pageIndex}/{pageSize}/{sorting}")]
        //[HttpGet]
        //public async Task<GenericResult<IList<PermissionLookupViewModel>>> GetLookupAsync(int? pageIndex, int? pageSize, string sorting)
        //{
        //    var result = await this._PermissionsService.GetLookupAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
        //    return result;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        public async Task<PermissionViewModel> GetAsync(long  id)
        {
            var result = await this._PermissionsService.GetAsync(id);
            return result;
        }

        /// <summary>
        /// Adds a new Permission to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public async Task<PermissionViewModel> AddAsync(PermissionViewModel model)
        {
            var result = await this._PermissionsService.AddAsync(model);
            return result;
        }


        /// <summary>
        /// updates Permission to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        public async Task<PermissionViewModel> UpdateAsync(PermissionViewModel model)
        {
            var result = await this._PermissionsService.UpdateAsync(model);
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
            await this._PermissionsService.DeleteAsync(id);
        }


        [Route("get-all-un-selected-permissions/{roleId}")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<IList<PermissionLightViewModel>> GetAllUnSelectedPermissionsAsync(long roleId)
        {
            var result =   this._PermissionsService.GetAllUnSelectedPermissions(roleId);
            return result;
        }

        [Route("get-all-un-selected-permissions-for-user/{userId}")]
        [HttpGet]
        //[JwtAuthentication(Permissions.ChangeUserPermisstions)]
        public async Task<IList<PermissionLightViewModel>> GetAllUnSelectedPermissionsForUserAsync(long userId)
        {
            var result =   this._PermissionsService.GetAllUnSelectedPermissionsForUser(userId);
            return result;
        }


        [Route("get-all-permissions-Groups")]
        [HttpGet]
        public async Task<IList<PermissionGroupLookupViewModel>> GetAllPermissionsGroups()
        {
            var result = await this._PermissionsService.GetAllPermissionsGroups();
            return result;
        }


        [Route("get-all-permissions-By-Group/{groupId}")]
        [HttpGet]
        public async Task<IList<NameValueNumericViewModel>> GetPermissionByGroupId(long groupId)
        {
            var result = await this._PermissionsService.GetPermissionByGroupId(groupId);
            return result;
        }


        #endregion
    }
}
