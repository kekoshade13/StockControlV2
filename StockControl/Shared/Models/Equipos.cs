using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class Equipos
    {
        [Key]
        public int Id_Equip { get; set; }
        public string NameEquip { get; set; }
        public List<SpareParts> SpareParts { get; set; }
    }
}
