using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StableManager.Data;
using StableManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace StableManager.Controllers
{
    [Authorize]
    [Authorize(Policy = "RequireAdministratorRole")]
   // [Authorize(Policy = "AdminOnly")]
    public class StableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StableController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Index for stable actions
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Redirect to "Details" as only 1 object will be found in DB
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Displays the stable information
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Details()
        {
            //get the first and only stable entry in db
            var id =  _context.StableDetails.FirstOrDefault().StableDetailsID;

            //try and find the stable
            var stableDetails = await _context.StableDetails
                .SingleOrDefaultAsync(m => m.StableDetailsID == id);

            //if not found, return not found
            if (stableDetails == null)
            {
                return NotFound();
            }

            return View(stableDetails);
        }


        /// <summary>
        /// Edit Stable details
        /// </summary>
        /// <param name="id">ID for stable</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            //if id is not specified, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try and find the stable
            var stableDetails = await _context.StableDetails.SingleOrDefaultAsync(m => m.StableDetailsID == id);

            //if not found, return not found
            if (stableDetails == null)
            {
                return NotFound();
            }
            return View(stableDetails);
        }

        /// <summary>
        /// Edit stable details
        /// </summary>
        /// <param name="id">ID for stable</param>
        /// <param name="stableDetails">Stable detail object to save</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StableDetailsID,StableName,StableCountry,StableProvState,StableCity,StablePostalCode,StableAddress,ContactName,StableEmail,StablePhone,TaxNumber,ModifiedOn,ModifierUserID")] StableDetails stableDetails)
        {
            //if id and stableDetail object's id is not the same, return not found
            if (id != stableDetails.StableDetailsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    stableDetails.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    stableDetails.ModifiedOn = DateTime.Now;
                    _context.Update(stableDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StableDetailsExists(stableDetails.StableDetailsID))
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
            return View(stableDetails);
        }


        private bool StableDetailsExists(string id)
        {
            return _context.StableDetails.Any(e => e.StableDetailsID == id);
        }




    }
}
