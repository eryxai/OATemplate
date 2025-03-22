#region Using ...
using Framework.Core.Common;
using Framework.Core.QueryFilters;
using System.Collections.Generic;
#endregion

/*
 
 
 */
namespace Framework.Core.Models
{
    public class BaseFilter: ITableFilterModel
    {
		public string Sorting { get; set; }
		public Pagination Pagination { get; set; }
        public Dictionary<string, object> FilterMetadata { get; set; }

    }
}
