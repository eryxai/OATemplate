#region Using ...
using TemplateService.Entity.Entities;
#endregion

/*


*/
namespace TemplateService.Core.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUsersRepositoryAsync : Base.IBaseServiceRepositoryAsync<User, long>
    {
        User Login(string userName);
    }
}
