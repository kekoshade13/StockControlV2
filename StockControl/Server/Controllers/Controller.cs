using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using StockControl.Server.Data;
using StockControl.Shared.Models.Identity;

namespace StockControl.Server.Controllers
{
    /// <summary>
    /// Base controller
    /// Implement here common functionalities required in all controllers
    /// </summary>
    [ApiController]
    public class Controller : ControllerBase
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly UserManager<ApplicationUser> UserManager;
        protected readonly IConfiguration Configuration;

        private string _userName;
        protected int PageSize = 10;

        public Controller(
            ApplicationDbContext applicationDbContext, IConfiguration configuration, UserManager<ApplicationUser> userManager = null)
        {
            DbContext = applicationDbContext;
            UserManager = userManager;
            Configuration = configuration;
        }
    }
}