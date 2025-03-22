#region Using ...
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
    public interface IUserLoginsService : IBaseService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ValidateModelAsync(UserLoginViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        Task ValidateModelAsync(IEnumerable<UserLoginViewModel> modelCollection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<GenericResult<IList<UserLoginLightViewModel>>> Search(UserLoginSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<byte[]> ExportExcel(UserLoginSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<UserLoginViewModel>>> GetAsync(BaseFilter baseFilter = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserLoginViewModel> GetAsync(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserLoginViewModel> AddAsync(UserLoginViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserLoginViewModel> UpdateAsync(UserLoginViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);


        #endregion
    }
}
