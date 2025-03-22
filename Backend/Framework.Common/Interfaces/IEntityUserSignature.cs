#region Using ...
#endregion

/*
 
 
 */
namespace Framework.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityUserSignature : IEntityCreatedUserSignature
	{
		#region Properties
		/// <summary>
		/// Gets or sets entity FirstModifiedByUserId
		/// </summary>
		long? FirstModifiedByUserId { get; set; }
		/// <summary>
		/// Gets or sets entity LastModifiedByUserId.
		/// </summary>
		long? LastModifiedByUserId { get; set; }
		#endregion
	}
}
