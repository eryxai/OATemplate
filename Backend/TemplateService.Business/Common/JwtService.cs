using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using TemplateService.Core.Common;

namespace TemplateService.Business.Common
{
    public class JwtService : IJwtService
    {
        public const string SecretKey = "JIOBLi6eVjBpvGtWBgJzjWd2QH0sOn5tI8rIFXSHKijXWEt/3J2jFYL79DQ1vKu+EtTYgYkwTluFRDdtF41yAQ==";

        public string GenerateJWTToken(string id, string permissions, int expire_in_Minutes = (60 * 6 ))
        {
            var symmetric_Key = Convert.FromBase64String(SecretKey);
            var token_Handler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var securitytokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, id),
                            new Claim("permissions", permissions)
                        
                        }),
                
                Expires = now.AddMinutes(Convert.ToInt32(expire_in_Minutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetric_Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = token_Handler.CreateToken(securitytokenDescriptor);
            var token = token_Handler.WriteToken(stoken);

            return token;
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                if (jwtToken.ValidTo < DateTime.Now) return null;

                var symmetricKey = Convert.FromBase64String(SecretKey);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<int> GetPermissions(string token)
        {
            List<int> permissionsCode = new List<int>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var p = jwtToken.Claims.FirstOrDefault(x => x.Type == "permissions");
                var ps = p.Value.Split(",");
                foreach (var item in ps)
                {
                    permissionsCode.Add(int.Parse(item));
                }

            }
            return permissionsCode;
        }
    }
}
