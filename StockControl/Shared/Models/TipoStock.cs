using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class TipoStock
    {
        [Key]
        public int Id_Stock { get; set; }
        public bool Active { get; set; }
        public string NameStock { get; set; }
    }
}
