#region Using ...
using Framework.Common.Interfaces;
using Framework.Core.Common;
using Framework.Core.IRepositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
    public class BaseFrameworkRepositoryAsync<TEntity, TPrimeryKey> :
		IDisposable,
		IAsyncDisposable,
		IBaseFrameworkRepositoryAsync<TEntity, TPrimeryKey>
		where TEntity : class, IEntityIdentity<TPrimeryKey>
	{
		#region Data Members
		protected DbContext _context;
		
		private readonly ICurrentUserService _currentUserService;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// BaseFrameworkRepositoryAsync.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="currentUserService"></param>
		public BaseFrameworkRepositoryAsync(
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

		public ValueTask DisposeAsync()
		{
			return this._context.DisposeAsync();
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
		public virtual async Task<long> GetCountAsync()
		{
			var result = await this.Entities.LongCountAsync();
			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public virtual async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
		{
			var result = await this.Entities.LongCountAsync(predicate);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		public async Task<IQueryable<TEntity>> SetIncludedNavigationsListAsync(IQueryable<TEntity> source, IEnumerable<string> list)
		{
			return await Task.Run(() =>
			{
				if (source != null && list != null)
				{
					foreach (var item in list)
					{
						source = source.Include(item);
					}
				}

				return source;
			});
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="sortOrder"></param>
		/// <returns></returns>
		public async Task<IQueryable<TEntity>> SetSortOrderAsync(IQueryable<TEntity> source, string sortOrder)
		{
			return await Task.Run(() =>
			{
				if (source != null &&
					string.IsNullOrEmpty(sortOrder) == false &&
					sortOrder != "null")
				{
					source = source.OrderBy(sortOrder);
				}

				return source;
			});
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pagination"></param>
		/// <returns></returns>
		public async Task<Pagination> SetPaginationCountAsync(IQueryable<TEntity> source, Pagination pagination)
		{
			if (pagination != null &&
				pagination.GetTotalCount)
			{
				pagination.TotalCount = await source.LongCountAsync();
			}

			return pagination;
		}
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<IQueryable<TEntity>> SetPaginationAsync(IQueryable<TEntity> source, Pagination pagination)
		{
			return await Task.Run(() =>
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
			});
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="conditionFilter"></param>
		/// <returns></returns>
		public virtual async Task<IQueryable<TEntity>> GetAsync(RepositoryRequestConditionFilter<TEntity, TPrimeryKey> conditionFilter = null)
		{
			return await Task.Run(async () =>
		   {
			   var result = this.Entities.AsQueryable();

			   if (conditionFilter != null)
			   {
				   #region Set IncludedNavigationsList
				   result = await SetIncludedNavigationsListAsync(result, conditionFilter.IncludedNavigationsList);
				   #endregion

				   #region Set Where Clause
				   if (conditionFilter.Query != null)
				   {
					   result = result.Where(conditionFilter.Query);
				   }
				   #endregion

				   #region Set Count
				   conditionFilter.Pagination = await SetPaginationCountAsync(result, conditionFilter.Pagination);
				   #endregion

				   #region Set Order
				   if (string.IsNullOrEmpty(conditionFilter.Sorting) == false &&
					   conditionFilter.Sorting != "null")
				   {
					   result = await SetSortOrderAsync(result, conditionFilter.Sorting);
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
				   result = await SetPaginationAsync(result, conditionFilter.Pagination);
				   #endregion
			   }

			   return result;

		   });
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="repositoryRequest"></param>
		/// <returns></returns>
		public virtual async Task<IQueryable<TEntity>> GetAsync(RepositoryRequest repositoryRequest = null)
		{
			RepositoryRequestConditionFilter<TEntity, TPrimeryKey> conditionFilter = null;

			if (repositoryRequest != null)
			{
				conditionFilter = new RepositoryRequestConditionFilter<TEntity, TPrimeryKey>(repositoryRequest);
			}

			return await this.GetAsync(conditionFilter);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="includedNavigationsList"></param>
		/// <returns></returns>
		public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] includedNavigationsList = null)
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
				result = await repo.FirstOrDefaultAsync(predicate);
			}
			#endregion

			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual async Task<TEntity> GetAsync(TPrimeryKey id)
		{
			var result = await this.Entities.FindAsync(id);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		public virtual async Task<IList<TEntity>> AddAsync(IEnumerable<TEntity> entityCollection)
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
				if (entity is IDateTimeSignature)
				{
					var dateTimeSignature = (IDateTimeSignature)entity;
					dateTimeSignature.FirstModificationDate = now;
					dateTimeSignature.LastModificationDate = now;
				}

				if (entity is IEntityUserSignature)
				{
					var entityUserSignature = (IEntityUserSignature)entity;
					entityUserSignature.FirstModifiedByUserId = this._currentUserService.CurrentUserId;
					entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
				}

				result.Add(entity);
			}

			await this.Entities.AddRangeAsync(result);
			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task<TEntity> AddAsync(TEntity entity)
		{
			DateTime now = DateTime.Now;
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
			if (entity is IDateTimeSignature)
			{
				var dateTimeSignature = (IDateTimeSignature)entity;
				dateTimeSignature.FirstModificationDate = now;
				dateTimeSignature.LastModificationDate = now;
			}

			if (entity is IEntityUserSignature)
			{
				var entityUserSignature = (IEntityUserSignature)entity;
				entityUserSignature.FirstModifiedByUserId = this._currentUserService.CurrentUserId;
				entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
			}


			await this.Entities.AddAsync(entity);
			return entity;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		public virtual async Task<IQueryable<TEntity>> UpdateAsync(IEnumerable<TEntity> entityCollection)
		{
			return await Task.Run(() =>
			{
				DateTime now = DateTime.Now;

				foreach (var entity in entityCollection)
				{
					if (entity is IDateTimeSignature)
					{
						var dateTimeSignature = (IDateTimeSignature)entity;

						if (dateTimeSignature.FirstModificationDate.HasValue == false)
                        {
							dateTimeSignature.FirstModificationDate = now;
							dateTimeSignature.LastModificationDate = now;
						}
						else
							dateTimeSignature.LastModificationDate = now;
					}

					if (entity is IEntityUserSignature)
					{
						var entityUserSignature = (IEntityUserSignature)entity;

						if (entityUserSignature.FirstModifiedByUserId.HasValue == false)
                        {
							entityUserSignature.FirstModifiedByUserId = this._currentUserService.CurrentUserId;
							entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
						}
						else
							entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
					}
				}

				this.Entities.UpdateRange(entityCollection);
				return entityCollection.AsQueryable();
			});
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task<TEntity> UpdateAsync(TEntity entity)
		{
			return await Task.Run(() =>
			{
				if (entity is IDateTimeSignature)
				{
					DateTime now = DateTime.Now;
					var dateTimeSignature = (IDateTimeSignature)entity;

					if (dateTimeSignature.FirstModificationDate.HasValue == false)
                    {
						dateTimeSignature.FirstModificationDate = now;
						dateTimeSignature.LastModificationDate = now;
					}					
					else
						dateTimeSignature.LastModificationDate = now;
				}

				if (entity is IEntityUserSignature)
				{
					var entityUserSignature = (IEntityUserSignature)entity;

					if (entityUserSignature.FirstModifiedByUserId.HasValue == false)
                    {
						entityUserSignature.FirstModifiedByUserId = this._currentUserService.CurrentUserId;
						entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
					}
					else
						entityUserSignature.LastModifiedByUserId = this._currentUserService.CurrentUserId;
				}

				this.Entities.Update(entity);
				return entity;
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual async Task DeleteAsync(TPrimeryKey id)
		{
			await Task.Run(() =>
			{
				var entity = this.Entities.Find(id);

				this.DeleteEntity(entity);
			});
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idCollection"></param>
		/// <returns></returns>
		public virtual async Task DeleteAsync(IEnumerable<TPrimeryKey> idCollection)
		{
			await Task.Run(() =>
			{
				foreach (var id in idCollection)
				{
					var entity = this.Entities.Find(id);

					this.DeleteEntity(entity);
				}
			});
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task DeleteAsync(TEntity entity)
		{
			await Task.Run(() =>
			{
				this.DeleteEntity(entity);
			});
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityCollection"></param>
		/// <returns></returns>
		public virtual async Task DeleteAsync(IEnumerable<TEntity> entityCollection)
		{
			await Task.Run(() =>
			{
				foreach (var entity in entityCollection)
				{
					this.DeleteEntity(entity);
				}
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		private void DeleteEntity(TEntity entity)
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

        public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, string[] includedNavigationsList = null)
        {
			var repo = this.Entities.AsQueryable();
			IList<TEntity> result = null;

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
				result = await repo.Where(predicate).ToListAsync();
			}
			#endregion

			return result;
		}
        #endregion
    }
}
