using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StableManager.Data;
using StableManager.Models;

namespace StableManager.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public ClientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        /// <summary>
        /// ROuting based on user authority.
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<IActionResult> Index()
        {
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (AppUser.IsAdmin)
            {
                return RedirectToAction("ManageClients");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Routing for Admin to manage clients
        /// </summary>
        /// <returns>a complete listing of all clients</returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageClients()
        {
            var applicationDbContext = _context.Clients.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Gets details of a client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Details(string id)
        {
            //if id is null, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try to get the client
            var client = await _context.Clients
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.ClientID == id);
            //if client is not found, return not found
            if (client == null)
            {
                return NotFound();
            }

            //return client data
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", client.UserID);
            return View(client);
        }

        /// <summary>
        /// Get Method ofr creating new client
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// Post Method for creating a new client
        /// Adds in timestamps and userID
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([Bind("ClientID,ClientNumber,UserID,PreferredVet,PreferredVetDetails,AlternativeVet,AlternativeVetDetails,ModifiedOn,ModifierUserID")] Client client)
        {
            if (ModelState.IsValid)
            {
                //set the modified by and modified on fields
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                client.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                client.ModifiedOn = DateTime.Now;

                //add visible client number to the dataset
                client.ClientNumber = (_context.Clients.Count() + 1).ToString();

                //save client
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", client.UserID);
            return View(client);
        }

        /// <summary>
        /// Get client data
        /// </summary>
        /// <param name="id">Client iD</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id)
        {
            //if id is null, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try to get client
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ClientID == id);
            //if client is not found, return not found
            if (client == null)
            {
                return NotFound();
            }

            //return client for edit
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", client.UserID);
            return View(client);
        }

        /// <summary>
        /// Post changes
        /// </summary>
        /// <param name="id">Client ID</param>
        /// <param name="client">Client object that has been edited</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id, [Bind("ClientID,ClientNumber,UserID,PreferredVet,PreferredVetDetails,AlternativeVet,AlternativeVetDetails,ModifiedOn,ModifierUserID")] Client client)
        {
            //if id and client id do not match return not found (tamperred)
            if (id != client.ClientID)
            {
                return NotFound();
            }

            //if model is valid, try to update object in db
            if (ModelState.IsValid)
            {
                try
                {
                    //update the modified by and modified on fields
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    client.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    client.ModifiedOn = DateTime.Now;

                    //save changes
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", client.UserID);
            return View(client);
        }

        // GET: Client/Delete/5
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ClientID == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(string id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
    }
}
