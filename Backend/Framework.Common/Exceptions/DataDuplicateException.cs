#region Using ...
#endregion

/*
 
 
 */
namespace Framework.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DataDuplicateException : Base.BaseException
	{
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		/// <param name="message"></param>
		public DataDuplicateException(string message)
			: base(message)
		{

		}
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		public DataDuplicateException()
			: base("DataDuplicateException")
		{

		}

		public DataDuplicateException(int errorCode)
			: base(errorCode)
		{

		}
		#endregion

		#region Methods

		#endregion

		#region Properties

		#endregion
	}
}
