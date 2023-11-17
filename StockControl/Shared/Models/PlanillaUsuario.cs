using StockControl.Shared.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models
{
    public class PlanillaUsuario
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser UserName { get; set; }
        public int EscuelaNum { get; set; }
        public string Serie { get; set; }
        public SpareParts SpareParts { get; set; }
        public bool isFlash { get; set; }
        public bool isFlashCap { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public DateTime TotalDate { get; set; }
    }
}
