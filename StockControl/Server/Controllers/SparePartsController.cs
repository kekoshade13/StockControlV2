using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControl.Server.Data;
using StockControl.Shared.Models;
using StockControl.Shared.Models.Identity;
using StockControl.Shared.ModelsDto;

namespace StockControl.Server.Controllers
{
    [Route("api/spareparts")]
    [ApiController]
    [AllowAnonymous]
    public class SparePartsController : Controller
    {
        public SparePartsController(ApplicationDbContext context, IConfiguration configuration)
            : base(context, configuration) { }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SparePartsDto sparePartsDto)
        {
            try
            {
                if (sparePartsDto == null)
                {
                    return BadRequest();
                }
                SpareParts? sparePartExists = await DbContext.SpareParts.Where(x => x.Code == sparePartsDto.Code).FirstOrDefaultAsync();

                if (sparePartExists == null)
                {
                    SpareParts sparePart = new()
                    {
                        Active = true,
                        Code = sparePartsDto.Code,
                        Name = sparePartsDto.Name,
                        Equipos = await DbContext.Equipos.Where(x => x.Id_Equip == sparePartsDto.EquipoId).ToListAsync(),
                    };
                    
                    await DbContext.SpareParts.AddAsync(sparePart);
                    await DbContext.SaveChangesAsync();

                    return Ok(sparePart.Id_Code);
                }
                else
                {
                    return BadRequest("-1");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int sparePartId)
        {
            try
            {
                SparePartsDto? sparePart = await DbContext.SpareParts
                    .Where(x => x.Id_Code == sparePartId)
                    .Select(x => new SparePartsDto()
                    {
                        Id_Code = x.Id_Code,
                        Name = x.Name,
                        Code = x.Code,
                        Equipos = x.Equipos.Select(eq => new EquiposDto()
                        {
                            Id_Equip = eq.Id_Equip,
                            NameEquip = eq.NameEquip,
                        }).ToList()
                    }).FirstOrDefaultAsync();

                return Ok(sparePart);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(SparePartsDto sparePartDto)
        {
            try
            {
                if (sparePartDto == null)
                {
                    return BadRequest();
                }
                SpareParts? SpareParts = await DbContext.SpareParts
                    .Where(x => x.Id_Code == sparePartDto.Id_Code && x.Active).FirstOrDefaultAsync();
                if (SpareParts == null)
                {
                    return NotFound();
                }
                else
                {
                    SpareParts.Name = sparePartDto.Name;
                    SpareParts.Code = sparePartDto.Code;
                    var equipos = await DbContext.Equipos.Where(x => sparePartDto.EquipoId == x.Id_Equip).ToListAsync();
                    SpareParts.Equipos = equipos;
                    return Ok(await DbContext.SaveChangesAsync());
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{sparePartId}")]
        public async Task<ActionResult> Delete(int sparePartId)
        {
            try
            {
                SpareParts? sparePart = await DbContext.SpareParts.Where(x => x.Id_Code == sparePartId && x.Active).FirstOrDefaultAsync();
                if (sparePart == null)
                {
                    return NotFound();
                }
                else
                {
                    sparePart.Active = false;
                }
                DbContext.Entry(sparePart).State = EntityState.Modified;
                return Ok(await DbContext.SaveChangesAsync());
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("spareParts")]
        public async Task<IActionResult> GetSpareParts(int? page = null, string queryString = null)
        {
            try
            {
                List<SparePartsDto> spareParts = new();
                PaginatedResponse<SparePartsDto> paginatedSpareParts = new();
                queryString = queryString?.ToLower();
                if (page != null)
                {
                    int skip = (int)page * PageSize;
                    int totalCount = await DbContext.SpareParts.Where(x => x.Active).CountAsync();
                    spareParts = await DbContext.SpareParts
                        .Where(x => x.Active
                            && (queryString == null || x.Name.ToLower().Contains(queryString)))
                        .OrderBy(x => x.Code)
                        .Skip(skip)
                        .Take(PageSize)
                        .Select(x => new SparePartsDto()
                        {
                            Name = x.Name,
                            Code = x.Code
                        }).ToListAsync();
                    paginatedSpareParts = new(spareParts, totalCount, (int)page, PageSize);
                }
                return Ok(paginatedSpareParts);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
