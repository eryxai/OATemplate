#region Using ...
using Framework.Common.Enums;
using System;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    /// <summary>
    /// Specify a functionality to 
    /// log any thing in a log.
    /// </summary>
    public interface ILoggerService
	{
		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="content"></param>
		/// <param name="type"></param>
		/// <param name="customFileName"></param>
		void Log(string content, LogType type, string customFileName = null);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="content"></param>
		void LogError(string content);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ex"></param>
		void LogError(Exception ex);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="content"></param>
		/// <param name="customFileName"></param>
		void LogInfo(string content, string customFileName = null);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="content"></param>
		/// <param name="customFileName"></param>
		void LogText(string content, string customFileName = null);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="content"></param>
		/// <param name="customFileName"></param>
		void LogWarning(string content, string customFileName = null);
		#endregion
	}
}
