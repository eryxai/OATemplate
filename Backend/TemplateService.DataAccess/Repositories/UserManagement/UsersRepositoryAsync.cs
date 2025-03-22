#region Using ...
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Common;
using TemplateService.Core.IRepositories;
using TemplateService.DataAccess.Contexts;
using TemplateService.Entity.Entities;
using Microsoft.EntityFrameworkCore;
#endregion

/*


*/
namespace TemplateService.DataAccess.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersRepositoryAsync : Base.BaseServiceRepositoryAsync<User, long>, IUsersRepositoryAsync
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UsersRepositoryAsync.
        /// </summary>
        /// <param name="context"></param>
        public UsersRepositoryAsync(TemplateServiceContext context, ICurrentUserService currentUserService)
            : base(context, currentUserService)
        {

        }
        #endregion


        public User Login(string userName)
        {
            return this.Entities.AsQueryable().Include(x => x.UserRoles).ThenInclude(x=>x.Role).FirstOrDefault(x => x.Username == userName &&x.IsDeleted!=true&&x.IsActive==true);
        }

    }
}
