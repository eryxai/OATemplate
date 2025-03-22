using Framework.Core.Core;
using Framework.Core.QueryFilters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Models
{
    public static class TableFilterExtension
    {
        public static IQueryable<T> PrimengTableFilter<T>(this IQueryable<T> dataSet,
          ITableFilterModel tableFilterPayload, out int totalRecord)
        {
            ITableFilterManager<T> tableFilterManager = new TableFilterManager<T>(dataSet);


            if (tableFilterPayload!=null&&tableFilterPayload.FilterMetadata != null && tableFilterPayload.FilterMetadata.Any())
            {
                foreach (var filterContext in tableFilterPayload.FilterMetadata)
                {
                    var filterPayload = filterContext.Value.ToString();
                    var filterToken = JToken.Parse(filterPayload);
                    switch (filterToken)
                    {
                        case JArray _:
                            {
                                var filters = filterToken.ToObject<List<TableFilterContext>>();
                                tableFilterManager.FiltersDataSet(filterContext.Key, filters);
                                break;
                            }
                        case JObject _:
                            {
                                var filter = filterToken.ToObject<TableFilterContext>();
                                tableFilterManager.FilterDataSet(filterContext.Key, filter);
                                break;
                            }
                    }
                }
                tableFilterManager.ExecuteFilter();
            }

            //if (!string.IsNullOrEmpty(tableFilterPayload.SortField))
            //{
            //    tableFilterManager.SingleOrderDataSet(tableFilterPayload);
            //}

            //if (tableFilterPayload.MultiSortMeta != null && tableFilterPayload.MultiSortMeta.Any())
            //{
            //    tableFilterManager.MultipleOrderDataSet(tableFilterPayload);
            //}

            dataSet = tableFilterManager.GetResult();
            totalRecord = dataSet.Count();
            //dataSet = dataSet.Skip(tableFilterPayload.First).Take(tableFilterPayload.Rows);
            return dataSet;
        }
    }
}
