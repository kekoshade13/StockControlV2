using StockControl.Shared.ModelsDto;

namespace StockControl.Server.Services
{
    public interface IUserService
    {
        Task<(bool IsUserRegistered, string Message)>RegisterNewUser(UserRegistrationDto userRegistration);
    }
}
