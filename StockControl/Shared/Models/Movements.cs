using StockControl.Shared.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class Movements
    {
        [Key]
        public int Id_Movement { get; set; }
        public ApplicationUser UserName { get; set; }
        public SpareParts SpareParts { get; set; }
        public int Qty { get; set; }
        public TipoStock TipoStock { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public DateTime TotalDate { get; set;}
    }
}
