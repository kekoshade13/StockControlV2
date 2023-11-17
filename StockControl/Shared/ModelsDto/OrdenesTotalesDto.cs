using StockControl.Shared.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.ModelsDto
{
    public class OrdenesTotalesDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string nOrden { get; set; }
        public string UserName { get; set; }
        public int Escuela { get; set; }
        public string Estado { get; set; }
        public string TakenBy { get; set; }
        public int EquipoId { get; set; }
        public EquiposDto? Equipos { get; set; }
    }
}
