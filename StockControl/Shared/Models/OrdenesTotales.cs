using StockControl.Shared.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class OrdenesTotales
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public string nOrden { get; set; }
        public int Escuela { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string TotalDate { get; set; }
        public bool isFlash { get; set; }
        public bool isFlashCap { get; set; }
        public int isFinished { get; set; }
        public string State { get; set; }
        public ApplicationUser UserName { get; set; }
        public Equipos Equipo { get; set; }
        public List<OrderSpareParts> SpareParts { get; set; }
    }
}
