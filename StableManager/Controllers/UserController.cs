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
    ///A controller to handle user/ ASPNETUSERS
    ///Authorization is required
    /// </summary>
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
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
        /// ROuting based on user authority.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (AppUser.IsAdmin)
            {
                return RedirectToAction("ManageUsers");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }


        /// <summary>
        /// Action to view all users in the system
        /// Only Admins can view this
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageUsers()
        {
            //Get the list of current users
            var UserList = await _context.ApplicationUser.ToListAsync();

            //the users are sorted and returned
            var SortedList = UserList.OrderByDescending(u => u.ActiveUser);
            return View(SortedList);
        }

        /// <summary>
        /// An action that returns an individual user's details.
        /// No param needed as it will determine the current users id and use it to fetch the data
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyDetails()
        {
            //Get the current user and return the user information
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(AppUser);
        }

        /// <summary>
        /// Register a new user. Only admins can create new users
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult RegisterUser()
        {
            return View();
           
        }


        /// <summary>
        /// Posting action for registering new users.
        /// </summary>
        /// <param name="newUser">new user model</param>
        /// <param name="returnUrl">null</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel newUser, string returnUrl = null)
        {
            //if passed in model is valid
            if (ModelState.IsValid)
            {
                //Create user object and set a user number
                var user = new ApplicationUser { UserName = newUser.Email, Email = newUser.Email, ActiveUser = true, Address = newUser.Address, City = newUser.City, Country = newUser.Country, FirstName = newUser.FirstName, IsAdmin = newUser.IsAdmin, IsClient = newUser.IsClient, IsEmployee = newUser.IsEmployee, IsInstructor = newUser.IsInstructor, IsManager = newUser.IsManager, IsTrainer = newUser.IsTrainer, LastName = newUser.LastName, PhoneNumber = newUser.PhoneNumber, PostalCode = newUser.PostalCode, ProvState = newUser.ProvState };
                user.UserNumber = (_context.Users.Count() + 1).ToString();

                //Set updated on and updated by details
                user.ModifiedOn = DateTime.Now;
                var ModifierUser = await _userManager.FindByNameAsync(User.Identity.Name);
                user.ModifierUserID = ModifierUser.FullName;
                

                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(newUser.Email, callbackUrl);

                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(newUser);
        }



        /// <summary>
        /// View a user through admin actions
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ViewUser(string id)
        {
            //if id is not entered, return not found
            if (id == null)
            {
                return NotFound();
            }

            //if the id does not match a user id, return not found
            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            //return user
            return View(applicationUser);
        }


        /// <summary>
        /// Return a user to Edit 
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns></returns>
        public async Task<IActionResult> EditMyDetails()
        {
            //get the current user's identity
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (AppUser == null)
            {
                return NotFound();
            }

            //setup userVM
            var editableUser = new EditUserViewModel();
            editableUser.UserID = AppUser.Id;
            editableUser.FirstName = AppUser.FirstName;
            editableUser.LastName = AppUser.LastName;

            editableUser.Email = AppUser.Email;
            editableUser.PhoneNumber = AppUser.PhoneNumber;

            editableUser.Address = AppUser.Address;
            editableUser.City = AppUser.City;
            editableUser.ProvState = AppUser.ProvState;
            editableUser.Country = AppUser.Country;
            editableUser.PostalCode = AppUser.PostalCode;

            editableUser.IsAdmin = AppUser.IsAdmin;
            editableUser.IsClient = AppUser.IsClient;
            editableUser.IsEmployee = AppUser.IsEmployee;
            editableUser.IsInstructor = AppUser.IsInstructor;
            editableUser.IsTrainer = AppUser.IsTrainer;
            editableUser.ActiveUser = AppUser.ActiveUser;

            editableUser.LastLoggedOn = AppUser.LastLoggedOn;
            editableUser.ModifiedOn = AppUser.ModifiedOn;
            editableUser.ModifierUserID = AppUser.ModifierUserID;


            //return userVM
            return View(editableUser);
        }

        /// <summary>
        /// Edit a user with admin priviledges
        /// </summary>
        /// <param name="id">USERID</param>
        /// <param name="updatedUser">updated user VM</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMyDetails(EditUserViewModel updatedUser)
        {
            //get the current user's identity
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (AppUser == null)
            {
                return NotFound();
            }

        
            AppUser.Address = updatedUser.Address;
            AppUser.City = updatedUser.City;
            AppUser.ProvState = updatedUser.ProvState;
            AppUser.PostalCode = updatedUser.PostalCode;
            AppUser.Country = updatedUser.Country;
            AppUser.Email = updatedUser.Email;
            AppUser.FirstName = updatedUser.FirstName;
            AppUser.LastName = updatedUser.LastName;
            AppUser.UserName = updatedUser.Email;
            AppUser.PhoneNumber = updatedUser.PhoneNumber;  

            //set up modified by/on fields
            AppUser.ModifiedOn = DateTime.Now;
            var ModifierUser = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser.ModifierUserID = ModifierUser.FirstName + " " + ModifierUser.LastName;

            //save the changes
            await _userManager.UpdateAsync(AppUser);
            await _context.SaveChangesAsync();

            //Update the password if it has been updated in the VM
            if (updatedUser.Password != null && updatedUser.Password.Trim() != "")
            {
                string ResetToken = await _userManager.GeneratePasswordResetTokenAsync(AppUser);
                await _userManager.ResetPasswordAsync(AppUser, ResetToken, updatedUser.Password);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyDetails");
        }


        /// <summary>
        /// Return a user to Edit 
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> EditUser(string id)
        {
            //if id is not entered, return not found
            if (id == null)
            {
                return NotFound();
            }

            //find a user with the requested ID, if not found return not found
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            //setup userVM
            var editableUser = new EditUserViewModel();
            editableUser.UserID = applicationUser.Id;
            editableUser.FirstName = applicationUser.FirstName;
            editableUser.LastName = applicationUser.LastName;

            editableUser.Email = applicationUser.Email;
            editableUser.PhoneNumber = applicationUser.PhoneNumber;

            editableUser.Address = applicationUser.Address;
            editableUser.City = applicationUser.City;
            editableUser.ProvState = applicationUser.ProvState;
            editableUser.Country = applicationUser.Country;
            editableUser.PostalCode = applicationUser.PostalCode;

            editableUser.IsAdmin = applicationUser.IsAdmin;
            editableUser.IsClient = applicationUser.IsClient;
            editableUser.IsEmployee = applicationUser.IsEmployee;
            editableUser.IsInstructor = applicationUser.IsInstructor;
            editableUser.IsTrainer = applicationUser.IsTrainer;
            editableUser.ActiveUser = applicationUser.ActiveUser;

            editableUser.LastLoggedOn = applicationUser.LastLoggedOn;
            editableUser.ModifiedOn = applicationUser.ModifiedOn;
            editableUser.ModifierUserID = applicationUser.ModifierUserID;
            

            //return userVM
            return View(editableUser);
        }

        /// <summary>
        /// Edit a user with admin priviledges
        /// </summary>
        /// <param name="id">USERID</param>
        /// <param name="updatedUser">updated user VM</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel updatedUser)
        {
            //if user vm id does not match parameter id, return not found
            if (id != updatedUser.UserID)
            {
                return NotFound();
            }

            //get the application user and updated it
            var AppUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            AppUser.ActiveUser = updatedUser.ActiveUser;
            AppUser.Address = updatedUser.Address;
            AppUser.City = updatedUser.City;
            AppUser.ProvState = updatedUser.ProvState;
            AppUser.PostalCode = updatedUser.PostalCode;
            AppUser.Country = updatedUser.Country;
            AppUser.Email = updatedUser.Email;

            AppUser.IsAdmin = updatedUser.IsAdmin;
            AppUser.IsClient = updatedUser.IsClient;
            AppUser.IsEmployee = updatedUser.IsEmployee;
            AppUser.IsInstructor = updatedUser.IsInstructor;
            AppUser.IsManager = updatedUser.IsManager;
            AppUser.IsTrainer = updatedUser.IsTrainer;

            AppUser.FirstName = updatedUser.FirstName;
            AppUser.LastName = updatedUser.LastName;
            AppUser.UserName = updatedUser.Email;
            AppUser.PhoneNumber = updatedUser.PhoneNumber;

            //set up modified by/on fields
            AppUser.ModifiedOn = DateTime.Now;
            var ModifierUser = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser.ModifierUserID = ModifierUser.FirstName + " " + ModifierUser.LastName;

            //save the changes
            await _userManager.UpdateAsync(AppUser);
            await _context.SaveChangesAsync();

            //Update the password if it has been updated in the VM
            if (updatedUser.Password != null && updatedUser.Password.Trim() != "")
            {
                string ResetToken = await _userManager.GeneratePasswordResetTokenAsync(AppUser);
                await _userManager.ResetPasswordAsync(AppUser, ResetToken, updatedUser.Password);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }



        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(UserController.Index), "Index");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
