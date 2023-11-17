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
using StockControl.Shared.RequestModels;

namespace StockControl.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ITokenService _tokenService;

        public LoginController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
            : base(context, configuration, userManager) { }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                if (userLogin == null)
                {
                    return BadRequest();
                }
                ApplicationUser user = await UserManager.FindByNameAsync(userLogin.UserName);

                if (user == null || !await UserManager.CheckPasswordAsync(user, userLogin.Password))
                {
                    return BadRequest();
                }
                else
                {
                    var tokenString = GenerateToken(userLogin);

                    SigningCredentials signingCredentials = _tokenService.GetSigningCredentials();
                    List<Claim> claims = await _tokenService.GetClaims(user);
                    JwtSecurityToken tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
                    string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    user.RefreshToken = _tokenService.GenerateRefreshToken();
                    int refreshTokenExpiry = 2;
                    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(refreshTokenExpiry);

                    await UserManager.UpdateAsync(user);
                    DateTime expiration = DateTime.UtcNow.AddMinutes(120);
                    return Ok(new AuthenticationResponse{ IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken, Expiration = expiration });
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private string GenerateToken(UserLogin userLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"]!));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Configuration["JwtSettings:Issuer"], Configuration["JwtSettings:Issuer"], null,
                expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: signInCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(string templateId)
        //{
        //    try
        //    {
        //        LogInformation("{0} {1}", StringResource.ViewTemplate + " " + templateId, "Read");
        //        // Obtener para ver que no es null y que el tenant es el del user
        //        TemplateDto templateDto = await DbContext.Template
        //            .Where(x => x.TenantId == TenantId && x.Id == templateId && x.Active) // CHeck bundle?
        //            .Select(template => new TemplateDto()
        //            {

        //            }).FirstOrDefaultAsync();
        //        if (templateDto == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return Ok(templateDto);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        LogError(exception);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpPut]
        //public async Task<ActionResult> Put([FromBody] TemplateDto templateDto)
        //{
        //    try
        //    {
        //        if (templateDto == null)
        //        {
        //            return BadRequest();
        //        }
        //        LogInformation("{0} {1}", StringResource.UpdateTemplate + " " + templateDto.Id, "Update");
        //        // Get template from DB using id, similar Get
        //        if (template == null)
        //        {
        //            return NotFound();
        //        }
        //        DbContext.Entry(template).State = EntityState.Modified;
        //        return Ok(await DbContext.SaveChangesAsync());
        //    }
        //    catch (Exception exception)
        //    {
        //        LogError(exception);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete(string templateId)
        //{
        //    try
        //    {
        //        LogInformation("{0} {1}", StringResource.DeleteTemplate + " " + templateId, "Delete");
        //        // Buscarlo con check tenant
        //        Template template;// = await 
        //        if (template == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            template.Active = false;
        //            DbContext.Entry(template).State = EntityState.Modified;
        //            return Ok(await DbContext.SaveChangesAsync());
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        LogError(exception);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpGet("equipos")]
        public async Task<IActionResult> GetEquipos(int? page = null, string queryString = null)
        {
            try
            {
                List<EquiposDto> equipos;
                queryString = queryString?.ToLower();
                if (page != null)
                {
                    int skip = (int)page * PageSize;
                    int totalCount = await DbContext.Equipos.CountAsync();
                    equipos = await DbContext.Equipos
                        .Where(x => (queryString == null || x.NameEquip.ToLower().Contains(queryString)))
                        .OrderBy(x => x.Id_Equip)
                        .Skip(skip)
                        .Take(PageSize)
                        .Select(x => new EquiposDto()
                        {
                            Id_Equip = x.Id_Equip,
                            NameEquip = x.NameEquip
                        }).ToListAsync();
                    PaginatedResponse<EquiposDto> templatePages = new(equipos, totalCount, (int)page, PageSize);
                    return Ok(templatePages);
                }
                else
                {
                    equipos = await DbContext.Equipos
                        .Where(x => (queryString == null || x.NameEquip.ToLower().Contains(queryString)))
                        .OrderBy(x => x.Id_Equip)
                        .Select(x => new EquiposDto()
                        {
                            Id_Equip = x.Id_Equip,
                            NameEquip = x.NameEquip
                        }).ToListAsync();
                    return Ok(equipos);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpGet("export")]
        //public async Task<IActionResult> ExportTemplates(string exportType, string queryString = null)
        //{
        //    try
        //    {
        //        LogInformation("{0} {1}", StringResource.ExportTemplates, "Read");
        //        queryString = queryString?.ToLower();
        //        List<TemplateDto> templates = await DbContext.Template
        //                .Where(x => x.TenantId == TenantId && x.Active
        //                    && (queryString == null || x.Name.ToLower().Contains(queryString)))
        //                .OrderBy(x => x.Id)
        //                .Select(template => new TemplateDto()
        //                {

        //                }).ToListAsync();
        //        switch (exportType)
        //        {
        //            case (
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        LogError(exception);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpGet("export")]
        //public async Task<IActionResult> ExportTemplates(string exportType, string dates = null, string queryString = null)
        //{
        //    try
        //    {
        //        LogInformation("{0} {1}", StringResource.ExportTemplates + " " + dates + " - " + queryString, "Read");
        //        string[] datesStr = dates?.Split(",");
        //        DateTime? startDate = datesStr == null || string.IsNullOrEmpty(datesStr[0]) ? null : DateTime.Parse(datesStr[0]);
        //        DateTime? endDate = datesStr == null || string.IsNullOrEmpty(datesStr[1]) ? null : DateTime.Parse(datesStr[1]);
        //        if ((startDate != null) && (endDate != null) && (startDate > endDate))
        //        {
        //            return BadRequest(-1);
        //        }

        //        List<TemplateDto> templates = USE THE query of the else in ExportTemplates
        //        List<string> header = GenerateHeader();
        //        switch (exportType)
        //        {
        //            case ExportationType.Excel:
        //                {
        //                    var content = DocumentService.GenerateExcel(ToStringListList(templates), StringResource.Templates, header);
        //                    var contentType = "application/octet-stream";
        //                    return File(content, contentType, StringResource.Templates + ".xlsx");
        //                }
        //            case ExportationType.PDF:
        //                {
        //                    var content = DocumentService.GeneratePDF(ToStringListList(templates), StringResource.Templates, header);
        //                    var contentType = "application/pdf";
        //                    return File(content, contentType, StringResource.Templates + ".pdf");
        //                }
        //            case ExportationType.Text:
        //                {
        //                    var content = await DocumentService.GenerateCsv(ToStringListList(templates), header);
        //                    var contentType = "text/csv";
        //                    return File(content, contentType, StringResource.Templates + ".csv");
        //                }
        //            case ExportationType.Json:
        //                {
        //                    var content = JsonSerializer.Serialize(templates);
        //                    var contentType = "text/json";
        //                    return File(Encoding.ASCII.GetBytes(content), contentType, StringResource.Templates + ".json");
        //                }
        //            default:
        //                return BadRequest();
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        LogError(exception);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //private static List<List<string>> ToStringListList(List<TemplateDto> templates)
        //{
        //    List<List<string>> data = new();
        //    foreach (TemplateDto template in templates)
        //    {
        //        // Check each property before add, must match GenerateHeader
        //        List<string> line = new()
        //        {
        //            template.,
        //        };
        //        data.Add(line);
        //    }
        //    return data;
        //}

        //private static List<string> GenerateHeader()
        //{
        //    List<string> properties = new()
        //    {
        //        StringResource.Id,
        //        StringResource.Name,
        //        StringResource.Bundles
        //    };
        //    return properties;
        //}
    }
}
