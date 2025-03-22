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
    /// type UserLoginsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        #region Data Members
        private readonly IUserLoginsService _UserLoginsService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserLoginsController.
        /// </summary>
        /// <param name="UserLoginsService"></param>
        public UserLoginsController(
            IUserLoginsService UserLoginsService
            )
        {
            this._UserLoginsService = UserLoginsService;
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
        [JwtAuthentication()]
        public async Task<GenericResult<IList<UserLoginLightViewModel>>> Search(UserLoginSearchModel searchModel)
        {
            var result = await this._UserLoginsService.Search(searchModel);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [Route("export-excel")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<IActionResult> ExportExcel(UserLoginSearchModel searchModel)
        {
            var exportedData = await this._UserLoginsService.ExportExcel(searchModel);

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
        [JwtAuthentication()]
        public async Task<IActionResult> ExportPDF(UserLoginSearchModel searchModel)
        {
            var exportedData = await this._UserLoginsService.ExportExcel(searchModel);

            return File(exportedData, "application/pdf", "EmployeeSalarys.pdf");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        [JwtAuthentication()]
        public async Task<GenericResult<IList<UserLoginViewModel>>> GetAsync()
        {
            var result = await this._UserLoginsService.GetAsync();
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
        [JwtAuthentication()]
        public async Task<GenericResult<IList<UserLoginViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._UserLoginsService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        //[JwtAuthentication(Permissions.UserLoginView)]
        public async Task<UserLoginViewModel> GetAsync(long id)
        {
            var result = await this._UserLoginsService.GetAsync(id);
            return result;
        }

        /// <summary>
        /// Adds a new UserLogin to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        //[JwtAuthentication(Permissions.UserLoginCreate)]
        public async Task<UserLoginViewModel> AddAsync(UserLoginViewModel model)
        {
            var result = await this._UserLoginsService.AddAsync(model);
            return result;
        }



        /// <summary>
        /// updates UserLogin to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        [JwtAuthentication()]
        public async Task<UserLoginViewModel> UpdateAsync(UserLoginViewModel model)
        {
            var result = await this._UserLoginsService.UpdateAsync(model);
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}")]
        [HttpPost]
        //[JwtAuthentication(Permissions.UserLoginDelete)]
        public async Task DeleteAsync(long id)
        {
            await this._UserLoginsService.DeleteAsync(id);
        }



        #endregion
    }
}
