#region Using ...
#endregion

/*
 
 
 */
namespace Framework.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemNotFoundException : Base.BaseException
	{
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemNotFoundException.
		/// </summary>
		/// <param name="message"></param>
		public ItemNotFoundException(string message)
			: base(message)
		{
			
		}
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemNotFoundException.
		/// </summary>
		public ItemNotFoundException()
			: base("ItemNotFoundException")
		{

		}
		public ItemNotFoundException(int errorCode)
			: base(errorCode)
		{

		}
		public ItemNotFoundException(int errorCode , string message)
			: base(errorCode, message)
		{

		}
		#endregion

		#region Methods

		#endregion

		#region Properties

		#endregion
	}
}
