using System.ComponentModel.DataAnnotations;

namespace StockControl.Shared.ModelsDto.RequestModels
{
    public class RefreshTokenModel
    {
        [Required(ErrorMessage = "El token es requerido")]
        public string Token { get; set; }
        [Required(ErrorMessage = "El refresh token es requerido")]
        public string RefreshToken { get; set; }
    }
}
