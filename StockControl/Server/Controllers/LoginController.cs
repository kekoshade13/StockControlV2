using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControl.Server.Data;
using StockControl.Shared.Models.Identity;
using StockControl.Shared.Models;
using StockControl.Shared.ModelsDto;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using StockControl.Server.Services;
using System.Security.Claims;
using StockControl.Shared.ModelsDto.RequestModels;
using System.Text.RegularExpressions;

namespace StockControl.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginController(IConfiguration configuration, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest login)
        {
            try
            {
                // var por usings ambiguos
                var result = await _signInManager.PasswordSignInAsync(login.UserName!, login.Password!, false, false);
                if (!result.Succeeded)
                {
                    return BadRequest(new AuthenticationResponse { IsAuthSuccessful = false, ErrorMessage = "Usuario o contraseña incorrecto" });
                }
                ApplicationUser user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    List<Claim> claims = new()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                    };
                    SigningCredentials credentials = _tokenService.GetSigningCredentials();
                    JwtSecurityToken token = _tokenService.GenerateTokenOptions(credentials, claims);
                    string refreshToken = _tokenService.GenerateRefreshToken();

                    _ = int.TryParse(_configuration["Jwt:ExpireInMinutes"], out int refreshTokenExpireInMinutes);
                    user!.RefreshToken = refreshToken;
                    user!.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenExpireInMinutes);

                    await _userManager.UpdateAsync(user);

                    return Ok(new AuthenticationResponse { IsAuthSuccessful = true, Token = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = refreshToken, Expiration = token.ValidTo });
                }
                else
                {
                    return BadRequest("Usuario no encontrado");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            try
            {
                if (refreshTokenModel is null)
                {
                    return Unauthorized(new AuthenticationResponse { IsAuthSuccessful = false, ErrorMessage = "Refresh token no existente" });
                }
                string accessToken = refreshTokenModel.Token;
                string refreshToken = refreshTokenModel.RefreshToken;
                ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

                if (principal == null)
                {
                    return BadRequest("Token inválido o refresh token inválido");
                }
                SigningCredentials credentials = _tokenService.GetSigningCredentials();
                ApplicationUser user = await _userManager.FindByEmailAsync(principal.FindFirstValue(ClaimTypes.Email));
                if (user == null)
                {
                    return BadRequest("No se encontró usuario");
                }
                JwtSecurityToken token = _tokenService.GenerateTokenOptions(credentials, principal.Claims.ToList());
                string newToken = new JwtSecurityTokenHandler().WriteToken(token);
                user.RefreshToken = _tokenService.GenerateRefreshToken();
                DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration.GetSection("Jwt").GetSection("ExpireInMinutes").Value));

                await _userManager.UpdateAsync(user);
                return Ok(new AuthenticationResponse { IsAuthSuccessful = true, Token = newToken, RefreshToken = user.RefreshToken, Expiration = expiration });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] RegisterRequest register)
        {
            try
            {
                if (!Regex.IsMatch(register.UserName, @"^[a-zA-Z0-9]+$"))
                {
                    return Ok(new RegisterResponse { Successful = false, Errors = new List<string> { "Username is invalid, can only contain letters or digits." } });
                }

                ApplicationUser newUser = new ApplicationUser
                {
                    Name = register.Name,
                    LastName = register.LastName,
                    UserName = register.UserName,
                    NormalizedUserName = register.UserName,
                    CI = register.CI,
                    RefreshToken = string.Empty
                };
                IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);

                if (!result.Succeeded)
                {
                    IEnumerable<string> errors = result.Errors.Select(x => x.Description);
                    return Ok(new RegisterResponse { Successful = false, Errors = errors });
                }
                else
                {
                    return Ok(new RegisterResponse { Successful = true });
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
