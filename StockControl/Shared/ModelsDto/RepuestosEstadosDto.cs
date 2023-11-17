using StockControl.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.ModelsDto
{
    public class RepuestosEstadosDto
    {
        public SparePartsDto? SparePartsDto { get; set; }
        public int SparePartDtoId { get; set; }
        public TipoStockDto? TipoStockDto { get; set; }
        public int TipoStockId { get; set; }
        public int Amount { get; set; }
    }
}
