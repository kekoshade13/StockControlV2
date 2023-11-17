using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.ModelsDto
{
    public class OrdenDto
    {
        public int OrderId { get; set; }
        public bool Active { get; set; }
        public string nOrden { get; set; }
        public string UserName { get; set; }
        public int Escuela { get; set; }
        public string Estado { get; set; }
        public string TakenBy { get; set; }
        public int EquipoId { get; set; }
        public EquiposDto Equipos { get; set; }
    }
}
