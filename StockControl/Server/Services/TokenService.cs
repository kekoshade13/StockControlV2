using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StockControl.Server.Services
{
    public class TokenService : ITokenService
    {
        protected readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var expiry = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpireInMinutes"]!));

            var tokenOptions = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiry,
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredToken)
        {
            try
            {
                TokenValidationParameters tokenParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]!))
                };
                JwtSecurityTokenHandler handler = new();
                ClaimsPrincipal claimsPrincipal = handler.ValidateToken(expiredToken, tokenParameters, out SecurityToken securityToken);
                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("Invalid token.");
                }
                return claimsPrincipal;
            }
            catch
            {
                throw new SecurityTokenException("Invalid token.");
            }
        }

        public SigningCredentials GetSigningCredentials()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]!));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return credentials;
        }
    }
}
