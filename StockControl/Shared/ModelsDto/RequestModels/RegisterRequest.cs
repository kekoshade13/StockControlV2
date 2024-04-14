using System.ComponentModel.DataAnnotations;

namespace StockControl.Shared.ModelsDto.RequestModels
{
    public class RegisterRequest
    {
        [Required]
        public int CI { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
