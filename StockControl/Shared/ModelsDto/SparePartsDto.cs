using StockControl.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.ModelsDto
{
    public class SparePartsDto
    {
        public int Id_Code { get; set; }
        public bool Active { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int EquipoId { get; set; }
        public List<EquiposDto>? Equipos { get; set; }
        public int StockTypeId { get; set; }
        public List<TipoStockDto>? TypeStockDto { get; set; }
    }
}
