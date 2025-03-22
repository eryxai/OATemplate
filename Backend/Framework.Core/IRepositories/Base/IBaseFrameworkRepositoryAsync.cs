#region Using ...
using Framework.Common.Interfaces;
using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
#endregion

/*
 
 
 */
namespace Framework.Core.IRepositories.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimeryKey"></typeparam>
    public interface IBaseFrameworkRepositoryAsync<TEntity, TPrimeryKey> : IAsyncDisposable
		where TEntity : class, IEntityIdentity<TPrimeryKey>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		Task<long> GetCountAsync();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		Task<IQueryable<TEntity>> SetIncludedNavigationsListAsync(IQueryable<TEntity> source, IEnumerable<string> list);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="sortOrder"></param>
		/// <returns></returns>
		Task<IQueryable<TEntity>> SetSortOrderAsync(IQueryable<TEntity> source, string sortOrder);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pagination"></param>
		/// <returns></returns>
		Task<IQueryable<TEntity>> SetPaginationAsync(IQueryable<TEntity> source, Pagination pagination);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pagination"></param>
		/// <returns></returns>
		Task<Pagination> SetPaginationCountAsync(IQueryable<TEntity> source, Pagination pagination);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="conditionFilter"></param>
		/// <returns></returns>
		Task<IQueryable<TEntity>> GetAsync(RepositoryRequestConditionFilter<TEntity, TPrimeryKey> conditionFilter = null);
		Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, string[] includedNavigationsList = null);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="repositoryRequest"></param>
		/// <returns></returns>
		Task<IQueryable<TEntity>> GetAsync(RepositoryRequest repositoryRequest = null);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="includedNavigationsList"></param>
		/// <returns></returns>
		Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] includedNavigationsList = null);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<TEntity> GetAsync(TPrimeryKey id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		Task<IList<TEntity>> AddAsync(IEnumerable<TEntity> entityCollection);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<TEntity> AddAsync(TEntity entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		Task<IQueryable<TEntity>> UpdateAsync(IEnumerable<TEntity> entityCollection);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<TEntity> UpdateAsync(TEntity entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task DeleteAsync(TPrimeryKey id);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idCollection"></param>
		/// <returns></returns>
		Task DeleteAsync(IEnumerable<TPrimeryKey> idCollection);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task DeleteAsync(TEntity entity);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		Task DeleteAsync(IEnumerable<TEntity> entityCollection);
	}
}
