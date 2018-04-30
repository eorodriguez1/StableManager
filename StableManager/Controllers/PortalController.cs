using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StableManager.Data;
using StableManager.Models;
using StableManager.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StableManager.Services;
using Microsoft.Extensions.Logging;

namespace StableManager.Controllers
{
    /// <summary>
    /// This controller will route all basic portal funtionality
    /// </summary>
    [Authorize]
    public class PortalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public PortalController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        /// <summary>
        /// This action will route the user to the correct portal index depending on their authority/authorization
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            //Find the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewData["Name"] = AppUser.FirstName;

            //if the current user is an admin, go to admin page
            if (AppUser.IsAdmin)
            {
                return View();
            }
            else
            {
                return View();
            }

        }

    }
}
