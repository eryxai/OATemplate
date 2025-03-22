using Framework.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Core
{
    public interface ITableFilterManager<out TEntity>
    {
        void MultipleOrderDataSet(ITableFilterModel tableFilterPayload);
        void SingleOrderDataSet(ITableFilterModel tableFilterPayload);
        void FilterDataSet(string key, TableFilterContext value);
        void FiltersDataSet(string key, IEnumerable<TableFilterContext> values);
        void ExecuteFilter();
        IQueryable<TEntity> GetResult();
    }
}
