using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class Repuestos
    {
        [Key]
        public int Id_User { get; set; }
        public string Nombre_u { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CI { get; set; }
        public string Password { get; set; }
        public string Class { get; set; }
        public string Genero { get; set; }
        public string TargetProd { get; set; }
    }
}
