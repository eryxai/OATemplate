#region Using ...
using System;
using Framework.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemplateService.Core.IServices.Base;
using TemplateService.Core.Models.ViewModels;
#endregion

/*


*/
namespace TemplateService.Core.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExperienceService : IBaseService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ValidateModelAsync(ExperienceViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<GenericResult<IList<ExperienceLightViewModel>>> Search(ExperienceSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<byte[]> ExportExcel(ExperienceSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<ExperienceViewModel>>> GetAsync(BaseFilter baseFilter = null);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExperienceViewModel> GetAsync(long  id);


        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        Task<IList<ExperienceLookupViewModel>> GetLookup();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExperienceViewModel> AddAsync(ExperienceViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExperienceViewModel> UpdateAsync(ExperienceViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long  id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExperienceId"></param>
        /// <returns></returns>
       // Task<string> GetExperienceTitle(long ExperienceId);
     //   Task<GenericResult<IList<ExperienceLookupViewModel>>> SearchLookup(ExperienceLookupSearchModel searchModel);
       // Task<ExperienceDetailViewModel> GetDetailsAsync(long id);

        #endregion
    }
}
