#region Using ...
using Framework.Common.Interfaces;
using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public interface IBaseFrameworkRepository<TEntity, TPrimeryKey>
		where TEntity : class, IEntityIdentity<TPrimeryKey>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		long GetCount();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		long GetCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		IQueryable<TEntity> SetIncludedNavigationsList(IQueryable<TEntity> source, IEnumerable<string> list);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="sortOrder"></param>
		/// <returns></returns>
		IQueryable<TEntity> SetSortOrder(IQueryable<TEntity> source, string sortOrder);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pagination"></param>
		/// <returns></returns>
		IQueryable<TEntity> SetPagination(IQueryable<TEntity> source, Pagination pagination);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pagination"></param>
		/// <returns></returns>
		Pagination SetPaginationCount(IQueryable<TEntity> source, Pagination pagination);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="conditionFilter"></param>
		/// <returns></returns>
		IQueryable<TEntity> Get(RepositoryRequestConditionFilter<TEntity, TPrimeryKey> conditionFilter = null);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="repositoryRequest"></param>
		/// <returns></returns>
		IQueryable<TEntity> Get(RepositoryRequest repositoryRequest = null);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="includedNavigationsList"></param>
		/// <returns></returns>
		TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, string[] includedNavigationsList = null);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		TEntity Get(TPrimeryKey id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		IList<TEntity> Add(IEnumerable<TEntity> entityCollection);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		TEntity Add(TEntity entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		IQueryable<TEntity> Update(IEnumerable<TEntity> entityCollection);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		TEntity Update(TEntity entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		void Delete(TPrimeryKey id);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idCollection"></param>
		void Delete(IEnumerable<TPrimeryKey> idCollection);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		void Delete(TEntity entity);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		void Delete(IEnumerable<TEntity> entityCollection);
	}
}
