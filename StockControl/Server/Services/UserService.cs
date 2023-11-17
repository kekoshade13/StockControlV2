using StockControl.Server.Data;
using StockControl.Shared.Models.Identity;
using StockControl.Shared.ModelsDto;
using System.Security.Cryptography;

namespace StockControl.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ApplicationUser FromUserRegistrationModelToUserModel(UserRegistrationDto userRegistration)
        {
            return new ApplicationUser
            {
                Email = userRegistration.Email,
                UserName = userRegistration.UserName,
                Name = userRegistration.FirstName,
                LastName = userRegistration.LastName,
                PasswordHash = userRegistration.Password
            };
        }

        private string HashPassword(string plainPassword)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var rfcPassword = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);

            byte[] rfcPasswordHash = rfcPassword.GetBytes(20);
            byte[] passwordHash = new byte[36];
            Array.Copy(salt, 0, passwordHash, 0, 16);
            Array.Copy(rfcPasswordHash, 0, passwordHash, 16, 20);

            return Convert.ToBase64String(passwordHash);
        }

        public async Task<(bool IsUserRegistered, string Message)> RegisterNewUser(UserRegistrationDto userRegistration)
        {
            var userExists = _dbContext.Users.Any(x => x.Email.ToLower().Equals(userRegistration.Email.ToLower()));
            if (userExists)
            {
                return (false, "El usuario ya existe");
            }

            var newUser = FromUserRegistrationModelToUserModel(userRegistration);
            newUser.PasswordHash = HashPassword(newUser.PasswordHash);

            _dbContext.Users.Add(newUser);

            await _dbContext.SaveChangesAsync();
        }
    }
}
