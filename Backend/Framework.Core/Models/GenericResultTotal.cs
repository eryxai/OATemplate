using Framework.Core.Common;

namespace Framework.Core.Models
{
    public class GenericResultTotal<TCollection, Statistics>
	{
		#region Properties
		public TCollection Collection { get; set; }
		public Pagination Pagination { get; set; }
		public Statistics StatisticsData { get; set; }
		#endregion
	}
}
