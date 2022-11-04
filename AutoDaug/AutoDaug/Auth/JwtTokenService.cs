using AutoDaug.DataContext;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoDaug.Auth
{
    public class JwtTokenService
    {
        private readonly ApiDbContext _context;

        public JwtTokenService(ApiDbContext context)
        {
            _context = context;
        }

        private string secureKey = "'wfekjbkjbckjbdwcjkwb51561vwdsc15c3s5dv151v";
        public string Generate(int id, bool isAdmin)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var claims = new[] {
                    new Claim("Id", id.ToString()),
                    new Claim("Role", isAdmin ? "admin" : "user"),
            };

            var payload = new JwtPayload("autoDaug", null, claims, null, DateTime.Today.AddDays(7));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenhandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,

            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }

        public UserAuth ParseUser(string? jwt, bool requireAdmin)
        {
            try
            {
                if (jwt == null)
                    return new UserAuth { Error = "Authentication token not found, please login first!" };

                var token = Verify(jwt);

                var tokenIdClaim = token.Claims.FirstOrDefault(c => c.Type == "Id");
                var tokenRoleClaim = token.Claims.FirstOrDefault(c => c.Type == "Role");

                if (tokenIdClaim == null || tokenRoleClaim == null)
                    return new UserAuth { Error = "Invalid authentication token!" };

                var tokenId = int.Parse(tokenIdClaim.Value);
                var tokenRole = tokenRoleClaim.Value;

                if (!_context.Users.Any(x => x.Id == tokenId))
                    return new UserAuth { Error = "User does not exist!" };

                if (requireAdmin && tokenRole != "admin")
                {
                    return new UserAuth { Error = "You do not have permissions to access this!" };
                }

                return new UserAuth { UserId = tokenId, Role = tokenRole };
            }
            catch
            {
                return new UserAuth { Error = "Authentication error has occured!" };
            }
        }
    }
}
