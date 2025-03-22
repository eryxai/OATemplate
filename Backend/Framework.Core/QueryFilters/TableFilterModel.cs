using System.Collections.Generic;

namespace Framework.Core.QueryFilters
{
    public interface ITableFilterModel
    {
        public Dictionary<string, object> FilterMetadata { get; set; }

    }
}
