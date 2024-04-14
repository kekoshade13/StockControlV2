using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StockControl.Server.Services
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredToken);
        SigningCredentials GetSigningCredentials();
    }
}
