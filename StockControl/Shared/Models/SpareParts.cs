using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class SpareParts
    {
        [Key]
        public int Id_Code { get; set; }
        public bool Active { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public List<Equipos> Equipos { get; set; }
    }
}
