#region Using ...
using Framework.Core.Common;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
#endregion

/*
 
 
 */
namespace TemplateService.BusinessLogic.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentUserService : ICurrentUserService
	{
		#region Data Members
		private readonly IHttpContextAccessor _httpContext;
		#endregion

		#region Constructors
		public CurrentUserService(IHttpContextAccessor httpContext)
		{
			this._httpContext = httpContext;
		}
		#endregion

		#region ICurrentUserService
		public long? CurrentUserId
		{
            get
            {
                if (this._httpContext != null && this._httpContext.HttpContext != null)
                {
                    Microsoft.Extensions.Primitives.StringValues authorization;

                    if (this._httpContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authorization))
                    {
                        var token = authorization.ToString().Split(" ")[1];
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                        if (jwtToken == null)
                            return null;

                        string id = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name").Value;

                        return long.Parse(id);
                    }
                }
                return null;
            }
            //	get
            //	{
            //		Microsoft.Extensions.Primitives.StringValues userId;

            //		if (string.IsNullOrEmpty(userId) == false)
            //		{
            //			return new Guid(userId);
            //		}
            //		return null;
            //	}
            }

            //public User CurrentUser => throw new NotImplementedException();
            #endregion
        }
}
