#region Using ...
using Framework.Common.Interfaces;
using Framework.Core.Common;
using Framework.Core.IRepositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
#endregion

/*
 
 
 */
namespace Framework.DataAccess.Repositories.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimeryKey"></typeparam>
    public class BaseFrameworkRepository<TEntity, TPrimeryKey> :
		IDisposable,
		IBaseFrameworkRepository<TEntity, TPrimeryKey>
		where TEntity : class, IEntityIdentity<TPrimeryKey>
	{
		#region Data Members
		private DbContext _context;
		private readonly ICurrentUserService _currentUserService;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// BaseFrameworkRepository.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="currentUserService"></param>
		public BaseFrameworkRepository(
			DbContext context,
			ICurrentUserService currentUserService
			)
		{
			this.Context = context;
			this._currentUserService = currentUserService;
		}
		#endregion

		public void Dispose()
		{
			this._context.Dispose();
		}

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		protected DbContext Context
		{
			get { return this._context; }
			set
			{
				this._context = value;
				this.Entities = this._context.Set<TEntity>();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected DbSet<TEntity> Entities { get; set; }
		#endregion

		#region IBaseRepository<TEntity, TPrimeryKey>
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual long GetCount()
		{
			var result = this.Entities.LongCount();
			return result;
		}
		/// <summary>
		/// Returns an System.Int64 that represents the number of elements in a sequence
		/// that satisfy a condition.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual long GetCount(Expression<Func<TEntity, bool>> predicate)
		{
			var result = this.Entities.LongCount(predicate);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		public IQueryable<TEntity> SetIncludedNavigationsList(IQueryable<TEntity> source, IEnumerable<string> list)
		{
			if (source != null && list != null)
			{
				foreach (var item in list)
				{
					source = source.Include(item);
				}
			}

			return source;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="sortOrder"></param>
		/// <returns></returns>
		public IQueryable<TEntity> SetSortOrder(IQueryable<TEntity> source, string sortOrder)
		{
			if (source != null && 
				string.IsNullOrEmpty(sortOrder) == false && 
				sortOrder != "null")
			{
				source = source.OrderBy(sortOrder);
			}

			return source;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pagination"></param>
		/// <returns></returns>
		public Pagination SetPaginationCount(IQueryable<TEntity> source, Pagination pagination)
		{
			if (pagination != null &&
				pagination.GetTotalCount)
			{
				pagination.TotalCount = source.LongCount();
			}

			return pagination;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pagination"></param>
		/// <returns></returns>
		public IQueryable<TEntity> SetPagination(IQueryable<TEntity> source, Pagination pagination)
		{
			if (source != null &&
				pagination != null &&
				pagination.PageIndex.HasValue &&
				pagination.PageSize.HasValue)
			{
				source = source.Skip(pagination.PageIndex.Value * pagination.PageSize.Value)
							   .Take(pagination.PageSize.Value);
			}

			return source;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="conditionFilter"></param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> Get(RepositoryRequestConditionFilter<TEntity, TPrimeryKey> conditionFilter = null)
		{
			var result = this.Entities.AsQueryable();

			if (conditionFilter != null)
			{
				#region Set IncludedNavigationsList
				result = SetIncludedNavigationsList(result, conditionFilter.IncludedNavigationsList);
				#endregion

				#region Set Where Clause
				if (conditionFilter.Query != null)
				{
					result = result.Where(conditionFilter.Query);
				}
				#endregion

				#region Set Count
				conditionFilter.Pagination = SetPaginationCount(result, conditionFilter.Pagination);
				#endregion

				#region Set Order
				if (string.IsNullOrEmpty(conditionFilter.Sorting) == false &&
					conditionFilter.Sorting != "null")
				{
					result = SetSortOrder(result, conditionFilter.Sorting);
				}
				else if (conditionFilter.Order.HasValue)
				{
					if (conditionFilter.Order == Common.Enums.Order.Ascending)
					{
						result = result.OrderBy(entity => entity.Id);
					}
					else
					{
						result = result.OrderByDescending(entity => entity.Id);
					}
				}
				#endregion

				#region Set Pagination
				result = SetPagination(result, conditionFilter.Pagination);
				#endregion
			}

			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="repositoryRequest"></param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> Get(RepositoryRequest repositoryRequest = null)
		{
			RepositoryRequestConditionFilter<TEntity, TPrimeryKey> conditionFilter = null;

			if (repositoryRequest != null)
			{
				conditionFilter = new RepositoryRequestConditionFilter<TEntity, TPrimeryKey>(repositoryRequest);
			}

			return this.Get(conditionFilter);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="includedNavigationsList"></param>
		/// <returns></returns>
		public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, string[] includedNavigationsList = null)
		{
			var repo = this.Entities.AsQueryable();
			TEntity result = null;

			#region Set IncludedNavigationsList
			if (includedNavigationsList != null)
			{
				for (int i = 0; i < includedNavigationsList.Length; i++)
				{
					repo = repo.Include(includedNavigationsList[i]);
				}
			}
			#endregion

			#region Set Where Clause
			if (predicate != null)
			{
				result = repo.FirstOrDefault(predicate);
			}
			#endregion

			return result;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual TEntity Get(TPrimeryKey id)
		{
			var result = this.Entities.Find(id);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		public virtual IList<TEntity> Add(IEnumerable<TEntity> entityCollection)
		{
			DateTime now = DateTime.Now;
			List<TEntity> result = new List<TEntity>();

			foreach (var entity in entityCollection)
			{
				if (entity is ICreationTimeSignature)
				{
					var dateTimeSignature = (ICreationTimeSignature)entity;
					dateTimeSignature.CreationDate = now;
				}

				if (entity is IEntityCreatedUserSignature)
				{
					var entityUserSignature = (IEntityCreatedUserSignature)entity;
					entityUserSignature.CreatedByUserId = this._currentUserService.CurrentUserId;
				}

				result.Add(entity);
			}

			this.Entities.AddRange(result);
			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual TEntity Add(TEntity entity)
		{
			if (entity is ICreationTimeSignature)
			{
				DateTime now = DateTime.Now;
				var dateTimeSignature = (ICreationTimeSignature)entity;
				dateTimeSignature.CreationDate = now;
			}

			if (entity is IEntityCreatedUserSignature)
			{
				var entityUserSignature = (IEntityCreatedUserSignature)entity;
				entityUserSignature.CreatedByUserId = this._currentUserService.CurrentUserId;
			}

			this.Entities.Add(entity);
			return entity;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> Update(IEnumerable<TEntity> entityCollection)
		{
			DateTime now = DateTime.Now;

			foreach (var entity in entityCollection)
			{
				if (entity is IDateTimeSignature)
				{
					var dateTimeSignature = (IDateTimeSignature)entity;

					if (dateTimeSignature.FirstModificationDate.HasValue == false)
						dateTimeSignature.FirstModificationDate = now;
					else
						dateTimeSignature.LastModificationDate = now;
				}

				if (entity is IEntityUserSignature)
				{
					var entityUserSignature = (IEntityUserSignature)entity;

					if (entityUserSignature.FirstModifiedByUserId.HasValue == false)
						entityUserSignature.FirstModifiedByUserId = this._currentUserService.CurrentUserId;
					else
						entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
				}
			}

			this.Entities.UpdateRange(entityCollection);
			return entityCollection.AsQueryable();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual TEntity Update(TEntity entity)
		{
			if (entity is IDateTimeSignature)
			{
				DateTime now = DateTime.Now;
				var dateTimeSignature = (IDateTimeSignature)entity;

				if (dateTimeSignature.FirstModificationDate.HasValue == false)
					dateTimeSignature.FirstModificationDate = now;
				else
					dateTimeSignature.LastModificationDate = now;
			}

			if (entity is IEntityUserSignature)
			{
				var entityUserSignature = (IEntityUserSignature)entity;

				if (entityUserSignature.FirstModifiedByUserId.HasValue == false)
					entityUserSignature.FirstModifiedByUserId = this._currentUserService.CurrentUserId;
				else
					entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
			}

			this.Entities.Update(entity);
			return entity;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		public virtual void Delete(TPrimeryKey id)
		{
			var entity = this.Entities.Find(id);
			this.Delete(entity);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idCollection"></param>
		public virtual void Delete(IEnumerable<TPrimeryKey> idCollection)
		{
			foreach (var id in idCollection)
			{
				this.Delete(id);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Delete(TEntity entity)
		{
			if (entity is IDeletionSignature)
			{
				DateTime now = DateTime.Now;
				var virtualDeleteEntity = (IDeletionSignature)entity;

				if (virtualDeleteEntity.MustDeletedPhysical == true)
				{
					this.Entities.Remove(entity);
				}
				else
				{
					virtualDeleteEntity.IsDeleted = true;
					virtualDeleteEntity.DeletionDate = now;
					virtualDeleteEntity.DeletedByUserId = this._currentUserService.CurrentUserId;

					//if (virtualDeleteEntity.FirstDeletionDate.HasValue == false)
					//	virtualDeleteEntity.FirstDeletionDate = now;
					//else
					//	virtualDeleteEntity.LastDeletionDate = now;

					this.Entities.Update(entity);
				}
			}
			else
			{
				this.Entities.Remove(entity);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		public virtual void Delete(IEnumerable<TEntity> entityCollection)
		{
			foreach (var entity in entityCollection)
			{
				this.Delete(entity);
			}
		}
		#endregion
	}
}
