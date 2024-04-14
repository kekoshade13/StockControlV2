using System.ComponentModel.DataAnnotations;

namespace StockControl.Shared.ModelsDto.RequestModels
{
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Debes ingresar tu usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debes ingresar tu contraseña")]
        public string Password { get; set; }
    }
}
