namespace StockControl.Shared.ModelsDto.RequestModels
{
    public class RegisterResponse
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
