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
    public interface IPermissionsService : IBaseService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ValidateModelAsync(PermissionViewModel model);
        Task<List<PermissionGroupLookupViewModel>> GetAllPermissions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        Task ValidateModelAsync(IEnumerable<PermissionViewModel> modelCollection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<GenericResult<IList<PermissionLightViewModel>>> Search(PermissionSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<byte[]> ExportExcel(PermissionSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<PermissionViewModel>>> GetAsync(BaseFilter baseFilter = null);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PermissionViewModel> GetAsync(long id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PermissionViewModel> AddAsync(PermissionViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PermissionViewModel> UpdateAsync(PermissionViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long id);



        IList<PermissionLightViewModel> GetAllUnSelectedPermissions(long roleId);
        IList<PermissionLightViewModel> GetAllUnSelectedPermissionsForUser(long userId);
        Task<IList<PermissionGroupLookupViewModel>> GetAllPermissionsGroups();

        Task<IList<NameValueNumericViewModel>> GetPermissionByGroupId(long Id);
        #endregion
    }
}
