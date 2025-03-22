#region Using ...
#endregion

/*
 
 
 */
namespace Framework.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DataValidationException : Base.BaseException
	{
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		/// <param name="message"></param>
		public DataValidationException(string message)
			: base(message)
		{

		}
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		public DataValidationException()
			: base("DataValidationException")
		{

		}

		public DataValidationException(int errorCode)
			: base(errorCode)
		{

		}
		public DataValidationException(int errorCode, string message)
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
