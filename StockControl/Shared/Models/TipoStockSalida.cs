using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class TipoStockSalida
    {
        [Key]
        public int Id { get; set; }
        public SpareParts SpareParts { get; set; }
        public string tipoStockSalida { get; set; }
        public int Qty { get; set; }
    }
}
