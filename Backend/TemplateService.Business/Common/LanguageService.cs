#region Using ...
using Framework.Common.Enums;
using Framework.Core.Common;
using Microsoft.AspNetCore.Http;
#endregion

/*
 
 
 */
namespace TemplateService.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class LanguageService : ILanguageService
	{
		#region Data Members
		private readonly IHttpContextAccessor _httpContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// LanguageService.
		/// </summary>
		/// <param name="httpContext"></param>
		public LanguageService(IHttpContextAccessor httpContext)
		{
			this._httpContext = httpContext;
		}
		#endregion

		#region ILanguageService
		/// <summary>
		/// Gets the current language that
		/// sent in request header.
		/// </summary>
		public Language CurrentLanguage
		{
			get
			{
				var defaultLanguage = this.DefaultLanguage;
				var result = defaultLanguage;
				var currentLanguageCode = this.GetCurrentLanguageCode();

				switch (currentLanguageCode)
				{
					case "ar":
						result = Language.Arabic;
						break;
					case "en":
						result = Language.English;
						break;
					default:
						result = defaultLanguage;
						break;
				}

				return result;
			}
		}

		/// <summary>
		/// Gets the default language that 
		/// provided for this request.
		/// </summary>
		public Language DefaultLanguage
		{
			get
			{
				var result = Language.Arabic;
				return result;
			}
		}

		/// <summary>
		/// Gets the current language code 
		/// that sent in request header.
		/// </summary>
		/// <returns></returns>
		public string GetCurrentLanguageCode()
		{
			string result = this.GetDefaultLanguageCode();

			Microsoft.Extensions.Primitives.StringValues vs = new Microsoft.Extensions.Primitives.StringValues();
			this._httpContext.HttpContext?.Request.Headers.TryGetValue("language", out vs);

			if (vs.Count > 0)
			{
				var langCode = vs[0];

				if (string.IsNullOrEmpty(langCode) == false &&
					langCode.Length >= 2)
				{
					result = langCode.Substring(0, 2).ToLower();
				}
			}

			return result;
		}
		/// <summary>
		/// Gets the default language code 
		/// that provided for this request.
		/// </summary>
		/// <returns></returns>
		public string GetDefaultLanguageCode()
		{
			string result = "ar";
			return result;
		}

		#endregion
	}
}
