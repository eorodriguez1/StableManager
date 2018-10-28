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
    public class BoardingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardingController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all boardings
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Boardings.Include(b => b.Animal).Include(b => b.BoardingType).Include(b => b.BillToUser);
            return View(await applicationDbContext.ToListAsync());
        }


        /// <summary>
        /// Returns a specific boarding detail
        /// </summary>
        /// <param name="id">id of boarding</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //attach required properties to each boarding
            var boarding = await _context.Boardings
                .Include(b => b.Animal)
                .Include(b => b.BoardingType)
                .Include(b => b.BillToUser)
                .SingleOrDefaultAsync(m => m.BoardingID == id);

            if (boarding == null)
            {
                return NotFound();
            }

            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName");
            ViewData["BoardingTypeID"] = new SelectList(_context.BoardingType, "BoardingTypeID", "BoardingTypeName");
            ViewData["BillToUserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");

            return View(boarding);
        }

        /// <summary>
        /// Create a new boarding
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName");
            ViewData["BoardingTypeID"] = new SelectList(_context.BoardingType, "BoardingTypeID", "BoardingTypeName");
            ViewData["BillToUserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// Create a new boarding
        /// </summary>
        /// <param name="boarding">Boarding oject to create</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoardingID,AnimalID,BillToUserID,BoardingTypeID,StartedBoard,EndedBoard")] Boarding boarding)
        {
            
            if (ModelState.IsValid)
            {
                //add the require modified when/by stamps
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                boarding.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                boarding.ModifiedOn = DateTime.Now;
                _context.Add(boarding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", boarding.AnimalID);
            ViewData["BoardingTypeID"] = new SelectList(_context.BoardingType, "BoardingTypeID", "BoardingTypeName", boarding.BoardingTypeID);
            ViewData["BillToUserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", boarding.BillToUserID);
            return View(boarding);
        }

        /// <summary>
        /// get boarding for edit
        /// </summary>
        /// <param name="id">id of boarding to modify</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boarding = await _context.Boardings.SingleOrDefaultAsync(m => m.BoardingID == id);
            if (boarding == null)
            {
                return NotFound();
            }
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", boarding.AnimalID);
            ViewData["BoardingTypeID"] = new SelectList(_context.BoardingType, "BoardingTypeID", "BoardingTypeName", boarding.BoardingTypeID);
            ViewData["BillToUserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", boarding.BillToUserID);
            return View(boarding);
        }

        // POST: Boarding/Edit/5
        /// <summary>
        /// Saving an editted boarding
        /// </summary>
        /// <param name="id">boarding id</param>
        /// <param name="boarding">boarding object to save</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BoardingID,AnimalID,BillToUserID,BoardingTypeID,StartedBoard,EndedBoard")] Boarding boarding)
        {
            if (id != boarding.BoardingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //add modified by/when stamps
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    boarding.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    boarding.ModifiedOn = DateTime.Now;
                    _context.Update(boarding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardingExists(boarding.BoardingID))
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
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", boarding.AnimalID);
            ViewData["BoardingTypeID"] = new SelectList(_context.BoardingType, "BoardingTypeID", "BoardingTypeName", boarding.BoardingTypeID);
            ViewData["BillToUserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", boarding.BillToUserID);
            return View(boarding);
        }

        // GET: Boarding/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boarding = await _context.Boardings
                .Include(b => b.Animal)
                .Include(b => b.BoardingType)
                .Include(b => b.BillToUser)
                .SingleOrDefaultAsync(m => m.BoardingID == id);
            if (boarding == null)
            {
                return NotFound();
            }

            return View(boarding);
        }

        // POST: Boarding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var boarding = await _context.Boardings.SingleOrDefaultAsync(m => m.BoardingID == id);
            _context.Boardings.Remove(boarding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardingExists(string id)
        {
            return _context.Boardings.Any(e => e.BoardingID == id);
        }
    }
}
