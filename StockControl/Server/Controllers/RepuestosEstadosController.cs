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
    [Route("api/repuestosEstados")]
    [ApiController]
    [AllowAnonymous]
    public class RepuestosEstadosController : Controller
    {
        public RepuestosEstadosController(ApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
            : base(context, configuration, httpContextAccessor, userManager) { }

        [HttpPost]
        public async Task<IActionResult> Post(RepuestosEstadosDto repuestoEstadoDto)
        {
            try
            {
                if (repuestoEstadoDto == null)
                {
                    return BadRequest();
                }
                RepuestosEstados? repuestoEstado = await DbContext.RepuestosEstados
                    .Where(x => x.SparePartId == repuestoEstadoDto.SparePartDtoId
                    && x.StockTypeId == repuestoEstadoDto.TipoStockId).FirstOrDefaultAsync();

                if (repuestoEstado == null)
                {
                    RepuestosEstados repuestoEstadoNew = new()
                    {
                        SparePartId = repuestoEstadoDto.SparePartDtoId,
                        StockTypeId = repuestoEstadoDto.TipoStockId,
                        Amount = repuestoEstadoDto.Amount
                    };

                    await DbContext.RepuestosEstados.AddAsync(repuestoEstadoNew);
                }
                else
                {
                    return BadRequest("-1");
                }
                return Ok(await DbContext.SaveChangesAsync());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int repuestoEstadoId, int tipoStock)
        {
            try
            {
                RepuestosEstadosDto? repuestoEstado = await DbContext.RepuestosEstados
                    .Where(x => x.SpareParts.Id_Code == repuestoEstadoId && x.TipoStock.Id_Stock == tipoStock)
                    .Select(x => new RepuestosEstadosDto()
                    {
                        SparePartsDto = new()
                        {
                            Name = x.SpareParts.Name,
                            Code = x.SpareParts.Code,
                            Equipos = x.SpareParts.Equipos.Select(eq => new EquiposDto()
                            {
                                NameEquip = eq.NameEquip
                            }).ToList(),
                        },
                        TipoStockDto = new() { NameStock = x.TipoStock.NameStock },
                        Amount = x.Amount
                    }).FirstOrDefaultAsync();

                return Ok(repuestoEstado);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(RepuestosEstadosDto repuestosEstadosDto)
        {
            try
            {
                if (repuestosEstadosDto == null)
                {
                    return BadRequest();
                }
                RepuestosEstados? repuestoEstado = await DbContext.RepuestosEstados
                    .Where(x => x.SpareParts.Id_Code == repuestosEstadosDto.SparePartsDto.Id_Code && x.TipoStock.Id_Stock == repuestosEstadosDto.TipoStockDto.Id_Stock).FirstOrDefaultAsync();
                if (repuestoEstado == null)
                {
                    return NotFound();
                }
                else
                {
                    // To Do
                    //repuestoEstado. = sparePartDto.Name;
                    //repuestoEstado.Code = sparePartDto.Code;
                    //var equipos = await DbContext.Equipos.Where(x => sparePartDto.EquiposIds.Contains(x.Id_Equip)).ToListAsync();
                    //repuestoEstado.Equipos = equipos;
                    return Ok(await DbContext.SaveChangesAsync());
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int sparePartId, int tipoStockId)
        {
            try
            {
                RepuestosEstados? repuestoEstados = await DbContext.RepuestosEstados.Where(x => x.SpareParts.Id_Code == sparePartId && x.TipoStock.Id_Stock == tipoStockId).FirstOrDefaultAsync();
                if (repuestoEstados == null)
                {
                    return NotFound();
                }
                DbContext.Entry(repuestoEstados).State = EntityState.Deleted;
                return Ok(await DbContext.SaveChangesAsync());
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("repuestosEstados")]
        public async Task<IActionResult> GetSpareParts(int? page = null, string queryString = null)
        {
            try
            {
                List<RepuestosEstadosDto> repuestosEstados = new();
                PaginatedResponse<RepuestosEstadosDto> paginatedSpareParts = new();
                queryString = queryString?.ToLower();
                if (page != null)
                {
                    int skip = (int)page * PageSize;
                    int totalCount = await DbContext.SpareParts.Where(x => x.Active).CountAsync();
                    repuestosEstados = await DbContext.RepuestosEstados
                        .Where(x => x.SpareParts.Active
                            && (queryString == null || x.SpareParts.Name.ToLower().Contains(queryString)))
                        .OrderBy(x => x.SpareParts.Code)
                        .Skip(skip)
                        .Take(PageSize)
                        .Select(x => new RepuestosEstadosDto()
                        {
                            SparePartsDto = new SparePartsDto()
                            {
                                Active = x.SpareParts.Active,
                                Name = x.SpareParts.Name,
                                Code = x.SpareParts.Code,
                            },
                            TipoStockDto = new TipoStockDto()
                            {
                                NameStock = x.TipoStock.NameStock
                            },
                            Amount = x.Amount
                        }).ToListAsync();
                    paginatedSpareParts = new(repuestosEstados, totalCount, (int)page, PageSize);
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
