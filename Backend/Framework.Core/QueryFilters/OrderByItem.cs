using Framework.Common.Enums;
using Framework.Common.Interfaces;

namespace Framework.Core.QueryFilters
{
    public class OrderByItem<TEntity, T>//, TKey>
          where TEntity : class, IEntityIdentity<T>
    {
        #region Properties
        public Order Order { get; set; }
        //public T id { get; set; }
        //public Expression<Func<TEntity, TKey>> Selector { get; set; }
        #endregion
    }
}
