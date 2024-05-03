using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControl.Server.Data;
using StockControl.Shared.Constants;
using StockControl.Shared.Models;
using StockControl.Shared.Models.Identity;
using StockControl.Shared.ModelsDto;

namespace StockControl.Server.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [AllowAnonymous]
    public class OrdenesController : Controller
    {
        public OrdenesController(ApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
            : base(context, configuration, httpContextAccessor, userManager) { }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdenesTotalesDto ordenDto)
        {
            try
            {
                if (ordenDto == null)
                {
                    return BadRequest();
                }
                var equipo = await DbContext.Equipos.Where(x => x.Id_Equip == ordenDto.EquipoId).FirstOrDefaultAsync();
                ApplicationUser user = await UserManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    return BadRequest("-1");
                }
                if (equipo != null)
                {
                    OrdenesTotales ordenTotal = new()
                    {
                        Active = true,
                        nOrden = ordenDto.nOrden,
                        Escuela = ordenDto.Escuela,
                        UserName = user,
                        Date = DateTime.UtcNow.ToShortDateString(),
                        Hour = DateTime.UtcNow.Hour.ToString(),
                        TotalDate = DateTime.UtcNow.ToString(),
                        State = OrderStates.New,
                        Equipo = equipo
                    };
                    await DbContext.OrdenesTotales.AddAsync(ordenTotal);
                }
                else
                {
                    return BadRequest("-2");
                }
                return Ok(await DbContext.SaveChangesAsync());
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int ordenTotalId)
        {
            try
            {
                OrdenDto? sparePart = await DbContext.OrdenesTotales
                    .Where(x => x.Id == ordenTotalId)
                    .Select(x => new OrdenDto()
                    {
                        Active = x.Active,
                        //UserName = x.UserName.UserName,
                        Escuela = x.Escuela,
                        nOrden = x.nOrden,
                        Estado = x.State,
                        Equipos = new EquiposDto
                        {
                            Id_Equip = x.Id,
                            NameEquip = x.Equipo.NameEquip
                        }
                    }).FirstOrDefaultAsync();

                return Ok(sparePart);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(OrdenesTotalesDto ordenDto)
        {
            try
            {
                if (ordenDto == null)
                {
                    return BadRequest();
                }
                OrdenesTotales? ordenTotal = await DbContext.OrdenesTotales
                    .Where(x => x.Id == ordenDto.Id).FirstOrDefaultAsync();
                if (ordenTotal == null)
                {
                    return NotFound();
                }
                else
                {
                    ordenTotal.Escuela = ordenDto.Escuela;
                    ordenTotal.nOrden = ordenTotal.nOrden;
                    return Ok(await DbContext.SaveChangesAsync());
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{sparePartId}")]
        public async Task<ActionResult> Delete(int ordenTotalId)
        {
            try
            {
                OrdenesTotales? orden = await DbContext.OrdenesTotales.Where(x => x.Id == ordenTotalId && x.Active).FirstOrDefaultAsync();
                if (orden == null)
                {
                    return NotFound();
                }
                else
                {
                    orden.Active = false;
                }
                DbContext.Entry(orden).State = EntityState.Modified;
                return Ok(await DbContext.SaveChangesAsync());
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ordenesTotales")]
        public async Task<IActionResult> GetSpareParts(int? page = null, string queryString = null)
        {
            try
            {
                List<OrdenesTotalesDto> ordenesTotalesDtos = new();
                PaginatedResponse<OrdenesTotalesDto> paginatedResponse = new();
                queryString = queryString?.ToLower();
                if (page != null)
                {
                    int skip = (int)page * PageSize;
                    int totalCount = await DbContext.OrdenesTotales.Where(x => x.Active).CountAsync();
                    ordenesTotalesDtos = await DbContext.OrdenesTotales
                        .Where(x => x.Active
                            && (queryString == null || x.nOrden.ToLower().Contains(queryString)))
                        .OrderBy(x => x.Id)
                        .Skip(skip)
                        .Take(PageSize)
                        .Select(x => new OrdenesTotalesDto()
                        {
                            Id = x.Id,
                            nOrden = x.nOrden,
                            Escuela = x.Escuela,
                            //UserName = x.UserName.UserName
                            Estado = x.State
                        }).ToListAsync();
                    paginatedResponse = new(ordenesTotalesDtos, totalCount, (int)page, PageSize);
                }
                return Ok(paginatedResponse);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
