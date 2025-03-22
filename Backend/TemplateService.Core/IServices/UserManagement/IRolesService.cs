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
    public interface IRolesService : IBaseService
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ValidateModelAsync(RoleViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        Task ValidateModelAsync(IEnumerable<RoleViewModel> modelCollection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<GenericResult<IList<RoleLightViewModel>>> Search(RoleSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<byte[]> ExportExcel(RoleSearchModel searchModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<GenericResult<IList<RoleViewModel>>> GetAsync(BaseFilter baseFilter = null);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RoleViewModel> GetAsync(long  id);


        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <returns></returns>
        Task<List<RoleLookupViewModel>> GetLookup();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<RoleViewModel> AddAsync(RoleViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<RoleViewModel> UpdateAsync(RoleViewModel model);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(long  id);

        IList<RoleLightViewModel> GetAllUnSelectedRolesForUser(long userId);
        Task<RolePermissionListViewModel> GetRolePermission(long roleId);
        Task<RolePermissionListViewModel> UpdateRolePermissionAsync(RolePermissionListViewModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<string> GetRoleName(long roleId);
        Task<GenericResult<IList<RoleLookupViewModel>>> SearchLookup(RoleLookupSearchModel searchModel);
        Task<RoleDetailViewModel> GetDetailsAsync(long id);

        #endregion
    }
}
