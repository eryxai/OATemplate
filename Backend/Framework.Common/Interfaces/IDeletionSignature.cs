#region Using ...
using System;
#endregion

/*
 
 
 */
namespace Framework.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeletionSignature
	{
		#region Properties
		public bool IsDeleted { get; set; }
		public DateTime? DeletionDate { get; set; }
		//public DateTime? FirstDeletionDate { get; set; }
		//public DateTime? LastDeletionDate { get; set; }
		public long? DeletedByUserId { get; set; }
		public bool? MustDeletedPhysical { get; set; }
		#endregion
	}
}
