using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.RequestModels
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Debes ingresar tu usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Debes ingresar tu contraseña")]
        public string Password { get; set; }
    }
}
