using Microsoft.EntityFrameworkCore;

namespace StockControl.Shared.Models
{
    [PrimaryKey(nameof(SparePartId), nameof(OrderTotalId))]
    public class OrderSpareParts
    {
        public int SparePartId { get; set; }
        public SpareParts SpareParts { get; set; }
        public int OrderTotalId { get; set; }
        public OrdenesTotales OrdersTotals { get; set; }
        public TipoStock TipoStock { get; set; }
    }
}
