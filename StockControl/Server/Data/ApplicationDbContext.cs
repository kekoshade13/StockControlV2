using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockControl.Shared.Models;
using StockControl.Shared.Models.Identity;

namespace StockControl.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movements> Movements { get; set; }
        public DbSet<Equipos> Equipos { get; set; }
        public DbSet<SpareParts> SpareParts { get; set; }
        public DbSet<TipoStock> TipoStock { get; set; }
        public DbSet<EquiposRepuestos> EquiposRepuestos { get; set; }
        public DbSet<RepuestosEstados> RepuestosEstados { get; set; }
        //public DbSet<TipoStockSalida> TipoStockSalida { get; set; }
        //public DbSet<PlanillaUsuario> PlanillaUsuario { get; set; }
        public DbSet<OrdenesTotales> OrdenesTotales { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
