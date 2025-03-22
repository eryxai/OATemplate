#region Using ...
#endregion

/*
 
 
 */
namespace Framework.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemAlreadyExistException : Base.BaseException
	{
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		/// <param name="message"></param>
		public ItemAlreadyExistException(string message)
			: base(message)
		{

		}
		/// <summary>
		/// Initializes a new instance of 
		/// type ItemAlreadyExistException.
		/// </summary>
		public ItemAlreadyExistException()
			: base("ItemAlreadyExistException")
		{

		}

		public ItemAlreadyExistException(int errorCode)
			: base(errorCode)
		{

		}
        public ItemAlreadyExistException(int errorCode, string message)
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
