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
    public class UserRolesRepositoryAsync : Base.BaseServiceRepositoryAsync<UserRole, long>, IUserRolesRepositoryAsync
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserRolesRepositoryAsync.
        /// </summary>
        /// <param name="context"></param>
        public UserRolesRepositoryAsync(TemplateServiceContext context, ICurrentUserService currentUserService)
            : base(context, currentUserService)
        {

        }
        #endregion
    }
}
