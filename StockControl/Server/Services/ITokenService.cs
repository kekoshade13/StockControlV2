using Microsoft.IdentityModel.Tokens;
using StockControl.Shared.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StockControl.Server.Services
{
    public interface ITokenService
    {
        SigningCredentials GetSigningCredentials();
        Task<List<Claim>> GetClaims(ApplicationUser user);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
