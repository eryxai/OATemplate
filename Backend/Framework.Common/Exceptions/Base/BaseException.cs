#region Using ...
using System;
using System.Runtime.Serialization;
#endregion

/*
 
 
 */
namespace Framework.Common.Exceptions.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseException : ApplicationException
	{
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of 
		/// type 
		/// System.ApplicationException.
		/// </summary>		
		public BaseException()
		{

		}
		/// <summary>
		/// Initializes a new instance of the System.ApplicationException class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>      
		public BaseException(string message)
			: base(message)
		{

		}
		/// <summary>
		/// Initializes a new instance of the System.ApplicationException class with a specified 
		/// error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception. If the innerException
		/// parameter is not a null reference, the current exception is raised in a catch
		/// block that handles the inner exception.
		/// </param>        
		public BaseException(string message, Exception innerException)
			: base(message, innerException)
		{

		}
		/// <summary>
		///  Initializes a new instance of the System.ApplicationException class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>       
		protected BaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}
		public BaseException(int errorCode)
			: base(errorCode.ToString())
		{
			this.ErrorCode = errorCode;
		}
		public BaseException(int errorCode , string message)
		: base(message)
		{
			this.ErrorCode = errorCode;
		}

		#endregion

		#region Methods

		#endregion

		#region Properties
		public int ErrorCode { get; set; }
		#endregion
	}
}
