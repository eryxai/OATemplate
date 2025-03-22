#region Using ...
#endregion

/*
 
 
 */
namespace Framework.Core.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork
	{
		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		int Commit();
		#endregion
	}
}
