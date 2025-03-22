#region Using ...
using System;
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
    /// type ExperiencesController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        #region Data Members
        private readonly IExperienceService _ExperiencesService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// ExperiencesController.
        /// </summary>
        /// <param name="ExperiencesService"></param>
        public ExperienceController(
            IExperienceService ExperiencesService
            )
        {
            this._ExperiencesService = ExperiencesService;
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
        [JwtAuthentication(Permissions.ExperienceList)]
        public async Task<GenericResult<IList<ExperienceLightViewModel>>> Search(ExperienceSearchModel searchModel)
        {
            var result = await this._ExperiencesService.Search(searchModel);
            return result;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [Route("export-excel")]
        [HttpPost]
        [JwtAuthentication(Permissions.ExperienceList)]
        public async Task<IActionResult> ExportExcel(ExperienceSearchModel searchModel)
        {
            var exportedData = await this._ExperiencesService.ExportExcel(searchModel);

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
        [JwtAuthentication(Permissions.ExperienceList)]
        public async Task<IActionResult> ExportPDF(ExperienceSearchModel searchModel)
        {
            var exportedData = await this._ExperiencesService.ExportExcel(searchModel);

            return File(exportedData, "application/pdf", "EmployeeSalarys.pdf");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("get-collection")]
        [HttpGet]
        [JwtAuthentication(Permissions.ExperienceList)]
        public async Task<GenericResult<IList<ExperienceViewModel>>> GetAsync()
        {
            var result = await this._ExperiencesService.GetAsync();
            return result;
        }
        
        [Route("GetLookup")]
        [HttpGet]
        [JwtAuthentication]
        public async Task<IList<ExperienceLookupViewModel>> GetLookup()
        {
            var result = await this._ExperiencesService.GetLookup();
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
        [JwtAuthentication(Permissions.ExperienceList)]
        public async Task<GenericResult<IList<ExperienceViewModel>>> GetAsync(int? pageIndex, int? pageSize, string sorting)
        {
            var result = await this._ExperiencesService.GetAsync(new BaseFilter { Sorting = sorting, Pagination = new Pagination { PageIndex = pageIndex, PageSize = pageSize } });
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get/{id}")]
        [HttpGet]
        [JwtAuthentication(Permissions.ExperienceView)]
        public async Task<ExperienceViewModel> GetAsync(long id)
        {
            var result = await this._ExperiencesService.GetAsync(id);
            return result;
        }


        /// <summary>
        /// Adds a new Experience to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        [JwtAuthentication(Permissions.ExperienceAdd)]
        public async Task<ExperienceViewModel> AddAsync(ExperienceViewModel model)
        {
            var result = await this._ExperiencesService.AddAsync(model);
            return result;
        }

        /// <summary>
        /// updates Experience to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        [JwtAuthentication(Permissions.ExperienceEdit)]
        public async Task<ExperienceViewModel> UpdateAsync(ExperienceViewModel model)
        {
            var result = await this._ExperiencesService.UpdateAsync(model);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}")]
        [HttpPost]
        [JwtAuthentication(Permissions.ExperienceDelete)]
        public async Task DeleteAsync(long id)
        {
            await this._ExperiencesService.DeleteAsync(id);
        }

        #endregion
    }
}
