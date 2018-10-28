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
    public class BoardingTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardingTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the index/manage page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoardingType.ToListAsync());
            
        }

        /// <summary>
        /// Details for a specific Boarding Type
        /// </summary>
        /// <param name="id">ID for boarding type</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            //if id is not provided, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try to get boarding type
            var boardingType = await _context.BoardingType
                .SingleOrDefaultAsync(m => m.BoardingTypeID == id);

            //if not found, return not found
            if (boardingType == null)
            {
                return NotFound();
            }

            return View(boardingType);
        }

        /// <summary>
        /// Create a new boarding  type
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a new boarding type
        /// </summary>
        /// <param name="boardingType">Boarding type to create</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoardingTypeID,BoardingTypeName,BoardingTypeDescription,BoardingPrice,UserDefined1,UserDefined2")] BoardingType boardingType)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                boardingType.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                boardingType.ModifiedOn = DateTime.Now;
                _context.Add(boardingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boardingType);
        }

        /// <summary>
        /// Edit existing boarding type
        /// </summary>
        /// <param name="id">ID of boarding type to edit</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            //if id is not specified, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try to find boarding type
            var boardingType = await _context.BoardingType.SingleOrDefaultAsync(m => m.BoardingTypeID == id);
            //if boarding type is not found, return not found
            if (boardingType == null)
            {
                return NotFound();
            }
            return View(boardingType);
        }

        /// <summary>
        /// Edit an existing boarding type
        /// </summary>
        /// <param name="id">ID of boarding type to edit</param>
        /// <param name="boardingType">Boarding Type Object to edit</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BoardingTypeID,BoardingTypeName,BoardingTypeDescription,BoardingPrice,UserDefined1,UserDefined2")] BoardingType boardingType)
        {
            //if id does not match boarding type object's id, return not found
            if (id != boardingType.BoardingTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    boardingType.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    boardingType.ModifiedOn = DateTime.Now;
                    _context.Update(boardingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardingTypeExists(boardingType.BoardingTypeID))
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
            return View(boardingType);
        }

        // GET: BoardingType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardingType = await _context.BoardingType
                .SingleOrDefaultAsync(m => m.BoardingTypeID == id);
            if (boardingType == null)
            {
                return NotFound();
            }

            return View(boardingType);
        }

        // POST: BoardingType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var boardingType = await _context.BoardingType.SingleOrDefaultAsync(m => m.BoardingTypeID == id);
            _context.BoardingType.Remove(boardingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardingTypeExists(string id)
        {
            return _context.BoardingType.Any(e => e.BoardingTypeID == id);
        }
    }
}
