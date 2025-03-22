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
    public interface IDateTimeSignature : ICreationTimeSignature
	{
		#region Properties
		DateTime? FirstModificationDate { get; set; }
		DateTime? LastModificationDate { get; set; }
		#endregion
	}
}
