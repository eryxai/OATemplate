#region Using ...
#endregion

/*
 
 
 */
namespace Framework.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimeryKey"></typeparam>
    public interface IEntityIdentity<TPrimeryKey>
	{
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public TPrimeryKey Id { get; set; }
		#endregion
	}
}
