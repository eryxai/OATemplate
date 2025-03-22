#region Using ...
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Framework.Core.Common;
using TemplateService.Core.IRepositories;
using TemplateService.DataAccess.Contexts;
using TemplateService.Entity.Entities;
#endregion

/*


*/
namespace TemplateService.DataAccess.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionGroupRepositoryAsync : Base.BaseServiceRepositoryAsync<PermissionGroup, long>, IPermissionGroupRepositoryAsync
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// PermissionsRepositoryAsync.
        /// </summary>
        /// <param name="context"></param>
        public PermissionGroupRepositoryAsync(TemplateServiceContext context, ICurrentUserService currentUserService)
            : base(context, currentUserService)
        {

        }
        #endregion
    }
}
