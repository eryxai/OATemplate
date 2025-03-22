using Framework.Common.Enums;
using Framework.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Core.QueryFilters
{
    public class ConditionFilter<TEntity, T>
       where TEntity : class, IEntityIdentity<T>
    {
        #region Constructors
        public ConditionFilter()
        {
            this.NavigationPropertyCollection = new List<string>();
            this.Order = Order.Ascending;

            //this.OrderByItemCollection = new List<OrderByItem<TEntity, T>>();
        }
        #endregion

        #region Methods
        public static ConditionFilter<TEntity, T> Initialize(Expression<Func<TEntity, bool>> query)
        {
            return new ConditionFilter<TEntity, T>
            {
                Query = query
            };
        }
        #endregion

        #region Properties
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public Expression<Func<TEntity, bool>> Query { get; set; }
        public bool IsMustGetTotalCount { get; set; }
        public long TotalCount { get; set; }
        public IList<string> NavigationPropertyCollection { get; set; }
        public Order Order { get; set; }

        //public IList<OrderByItem<TEntity, T>> OrderByItemCollection { get; set; }
        #endregion
    }
}
