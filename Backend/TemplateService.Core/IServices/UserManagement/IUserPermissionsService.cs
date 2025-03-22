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
    public interface IUserPermissionsService : IBaseService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ValidateModelAsync(UserPermissionViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        Task ValidateModelAsync(IEnumerable<UserPermissionViewModel> modelCollection);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<UserPermissionViewModel>>> GetAsync(BaseFilter baseFilter = null);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserPermissionViewModel> GetAsync(long  id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserPermissionViewModel> AddAsync(UserPermissionViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserPermissionViewModel> UpdateAsync(UserPermissionViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long  id);

        #endregion
    }
}
