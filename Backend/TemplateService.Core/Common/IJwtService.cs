using System.Collections.Generic;
using System.Security.Claims;

namespace TemplateService.Core.Common
{
    public interface IJwtService
    {
        string GenerateJWTToken(string id, string permissions, int expire_in_Minutes = (60 * 24));

        ClaimsPrincipal GetPrincipal(string token);

        List<int> GetPermissions(string token);
    }
}
