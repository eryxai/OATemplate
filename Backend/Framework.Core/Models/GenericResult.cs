#region Using ...
using Framework.Core.Common;
#endregion

/*
 
 
 */
namespace Framework.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCollection"></typeparam>
    public class GenericResult<TCollection>
	{
		#region Properties
		public TCollection Collection { get; set; }
		public Pagination Pagination { get; set; }
		#endregion
	}
}
