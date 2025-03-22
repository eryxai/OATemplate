#region Using ...
using Framework.Common.Enums;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILanguageService
	{
		#region Properties
		/// <summary>
		/// Gets the current language that
		/// sent in request header.
		/// </summary>
		Language CurrentLanguage { get; }
		/// <summary>
		/// Gets the default language that 
		/// provided for this request.
		/// </summary>
		Language DefaultLanguage { get; }
		/// <summary>
		/// Gets the current language code 
		/// that sent in request header.
		/// </summary>
		/// <returns></returns>
		string GetCurrentLanguageCode();
		/// <summary>
		/// Gets the default language code 
		/// that provided for this request.
		/// </summary>
		/// <returns></returns>
		string GetDefaultLanguageCode();
		#endregion
	}
}
