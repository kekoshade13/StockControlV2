using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int CI { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
