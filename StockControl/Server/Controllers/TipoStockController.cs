using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControl.Server.Data;
using StockControl.Shared.Models.Identity;
using StockControl.Shared.Models;
using StockControl.Shared.ModelsDto;

namespace StockControl.Server.Controllers
{
    [Route("api/tipostock")]
    [ApiController]
    [AllowAnonymous]
    public class TipoStockController : Controller
    {
        //private readonly XXX YYY;

        public TipoStockController(ApplicationDbContext context, IConfiguration configuration)
            : base(context, configuration) { }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TipoStockDto tipoStockDto)
        {
            try
            {
                if (tipoStockDto == null)
                {
                    return BadRequest();
                }

                TipoStock tipoStock = new()
                {
                    Active = true,
                    NameStock = tipoStockDto.NameStock,
                };
                await DbContext.TipoStock.AddAsync(tipoStock);
                return Ok(await DbContext.SaveChangesAsync());
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int tipoStockId)
        {
            try
            {
                TipoStockDto tipoStockDto = await DbContext.TipoStock
                    .Where(x => x.Id_Stock == tipoStockId)
                    .Select(x => new TipoStockDto
                    {
                        Id_Stock = x.Id_Stock,
                        NameStock = x.NameStock
                    }).FirstOrDefaultAsync();
                if (tipoStockDto == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(tipoStockDto);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] TipoStockDto tipoStockDto)
        {
            try
            {
                if (tipoStockDto == null)
                {
                    return NotFound();
                }
                TipoStock tipoStock = await DbContext.TipoStock.Where(x => x.Id_Stock == tipoStockDto.Id_Stock).FirstOrDefaultAsync();
                if (tipoStock != null)
                {
                    tipoStock.NameStock = tipoStockDto.NameStock;
                }
                DbContext.Entry(tipoStock).State = EntityState.Modified;
                return Ok(await DbContext.SaveChangesAsync());
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int tipoStockId)
        {
            try
            {
                // Buscarlo con check tenant
                TipoStock tipoStock = await DbContext.TipoStock.Where(x => x.Id_Stock == tipoStockId).FirstOrDefaultAsync();
                if (tipoStock == null)
                {
                    return NotFound();
                }
                else
                {
                    tipoStock.Active = false;
                    DbContext.Entry(tipoStock).State = EntityState.Modified;
                    return Ok(await DbContext.SaveChangesAsync());
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("typeStocks")]
        public async Task<IActionResult> GetAllTypeStock(int? page = null, string queryString = null)
        {
            try
            {
                List<TipoStockDto> equipos;
                queryString = queryString?.ToLower();
                if (page != null)
                {
                    int skip = (int)page * PageSize;
                    int totalCount = await DbContext.TipoStock.CountAsync();
                    equipos = await DbContext.TipoStock
                        .Where(x => x.Active && (queryString == null || x.NameStock.ToLower().Contains(queryString)))
                        .OrderBy(x => x.Id_Stock)
                        .Skip(skip)
                        .Take(PageSize)
                        .Select(x => new TipoStockDto()
                        {
                            Id_Stock = x.Id_Stock,
                            NameStock = x.NameStock
                        }).ToListAsync();
                    PaginatedResponse<TipoStockDto> templatePages = new(equipos, totalCount, (int)page, PageSize);
                    return Ok(templatePages);
                }
                else
                {
                    equipos = await DbContext.TipoStock
                        .Where(x => x.Active && (queryString == null || x.NameStock.ToLower().Contains(queryString)))
                        .OrderBy(x => x.Id_Stock)
                        .Select(x => new TipoStockDto()
                        {
                            Id_Stock = x.Id_Stock,
                            NameStock = x.NameStock
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
