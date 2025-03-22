namespace Framework.Common.Exceptions
{
    public class InvalidOperationException : Base.BaseException
    {
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		/// <param name="message"></param>
		public InvalidOperationException(string message)
			: base(message)
		{

		}
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		public InvalidOperationException()
			: base("InvalidOperationException")
		{

		}

		public InvalidOperationException(int errorCode)
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
