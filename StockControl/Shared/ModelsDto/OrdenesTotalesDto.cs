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
        [Required(ErrorMessage = "Es necesario que se ingrese el número de serie del equipo")]
        public string nOrden { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Escuela { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string TakenBy { get; set; } = string.Empty;
        [Required(ErrorMessage = "Es necesario que se seleccione un equipo para esta orden")]
        public int EquipoId { get; set; }
        public EquiposDto? Equipos { get; set; }
    }
}
