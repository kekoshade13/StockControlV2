using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.ModelsDto
{
    public class EquiposDto
    {
        public int Id_Equip { get; set; }
        [Required(ErrorMessage = "Es necesario asignar un nombre para el equipo")]
        public string NameEquip { get; set; }
    }
}
