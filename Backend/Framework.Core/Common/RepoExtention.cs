#region Using ...
using System;
using System.Linq;
using System.Linq.Expressions;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class RepoExtention
	{
		/// <summary>
		/// An extention method that extends IQueryable 
		/// object and will attach a predicate to this 
		/// object if the condition evaluates to true.
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entities"></param>
		/// <param name="condition">
		/// A delegate to a function that will execute condition that evalute to bool value, true or false.
		/// </param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> entities, Func<bool> condition, Expression<Func<TEntity, bool>> predicate)
		{
			if (condition() == true && entities != null)
			{
				entities = entities.Where(predicate);
			}

			return entities;
		}

		/// <summary>
		/// An extention method that extends IQueryable 
		/// object and will attach a predicate to this 
		/// object if the condition evaluates to true.
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entities"></param>
		/// <param name="condition">
		/// A condition that evalute to bool value, true or false.
		/// </param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> entities, bool condition, Expression<Func<TEntity, bool>> predicate)
		{
			if (condition == true && entities != null)
			{
				entities = entities.Where(predicate);
			}

			return entities;
		}
	}
}
