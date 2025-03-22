#region Using ...
using System;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="TEntity"></typeparam>
    ///// <typeparam name="TPrimeryKey"></typeparam>
    //public interface ICurrentUserService<TEntity, TPrimeryKey>
    //	where TEntity : class, IEntityIdentity<TPrimeryKey>
    //	where TPrimeryKey : struct
    //{
    //	#region Properties
    //	/// <summary>
    //	/// 
    //	/// </summary>
    //	Nullable<TPrimeryKey> CurrentUserId { get; }
    //	/// <summary>
    //	/// 
    //	/// </summary>
    //	TEntity CurrentUser { get; }
    //	#endregion
    //}

    /// <summary>
    /// 
    /// </summary>
    public interface ICurrentUserService
	{
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		Nullable<long> CurrentUserId { get; }
		///// <summary>
		///// 
		///// </summary>
		//User CurrentUser { get; }
		#endregion
	}
}
