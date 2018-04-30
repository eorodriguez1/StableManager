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

        // GET: Stable
        public IActionResult Index()
        {
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> Details()
        {
            var id =  _context.StableDetails.FirstOrDefault().StableDetailsID;

            var stableDetails = await _context.StableDetails
                .SingleOrDefaultAsync(m => m.StableDetailsID == id);
            if (stableDetails == null)
            {
                return NotFound();
            }

            return View(stableDetails);
        }


        // GET: Stable/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stableDetails = await _context.StableDetails.SingleOrDefaultAsync(m => m.StableDetailsID == id);
            if (stableDetails == null)
            {
                return NotFound();
            }
            return View(stableDetails);
        }

        // POST: Stable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StableDetailsID,StableName,StableCountry,StableProvState,StableCity,StablePostalCode,StableAddress,ContactName,StableEmail,StablePhone,TaxNumber,ModifiedOn,ModifierUserID")] StableDetails stableDetails)
        {
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
