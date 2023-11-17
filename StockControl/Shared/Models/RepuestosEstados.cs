using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    [PrimaryKey(nameof(SparePartId), nameof(StockTypeId))]
    public class RepuestosEstados
    {
        public int SparePartId { get; set; }
        [ForeignKey(nameof(SparePartId))]
        public SpareParts? SpareParts { get; set; }
        public int StockTypeId { get; set; }
        [ForeignKey(nameof(StockTypeId))]
        public TipoStock? TipoStock { get; set; }
        public int Amount { get; set; }
    }
}
