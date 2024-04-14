using StockControl.Shared.ModelsDto;
using StockControl.Shared.ModelsDto.RequestModels;

namespace StockControl.Client.Services
{
    public interface IAuthService
    {
        Task<RegisterResponse> Register(RegisterRequest registerModel);
        Task<AuthenticationResponse> Login(AuthenticationRequest loginModel);
        Task Logout();
    }
}
