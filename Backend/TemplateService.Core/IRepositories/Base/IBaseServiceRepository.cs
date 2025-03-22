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
    public interface IBaseServiceRepository<TEntity, TPrimeryKey> : IBaseFrameworkRepository<TEntity, TPrimeryKey>
		where TEntity : class, IEntityIdentity<TPrimeryKey>
	{

	}
}
