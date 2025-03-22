#region Using ...
using Framework.Common.Interfaces;
using Framework.Core.IRepositories.Base;
#endregion

/*
 
 
 */
namespace TemplateService.Core.IRepositories.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimeryKey"></typeparam>
    public interface IBaseServiceRepositoryAsync<TEntity, TPrimeryKey> : IBaseFrameworkRepositoryAsync<TEntity, TPrimeryKey>
		where TEntity : class, IEntityIdentity<TPrimeryKey>
	{

	}
}
