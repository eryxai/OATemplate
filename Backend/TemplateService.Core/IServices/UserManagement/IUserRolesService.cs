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
    public interface IUserRolesService : IBaseService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ValidateModelAsync(UserRoleViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        Task ValidateModelAsync(IEnumerable<UserRoleViewModel> modelCollection);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<UserRoleViewModel>>> GetAsync(BaseFilter baseFilter = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserRoleViewModel> GetAsync(long  id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserRoleViewModel> AddAsync(UserRoleViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserRoleViewModel> UpdateAsync(UserRoleViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long  id);

        #endregion
    }
}
