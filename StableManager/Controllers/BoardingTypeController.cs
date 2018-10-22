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

        // GET: BoardingType
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoardingType.ToListAsync());
            
        }

        // GET: BoardingType/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: BoardingType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BoardingType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: BoardingType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardingType = await _context.BoardingType.SingleOrDefaultAsync(m => m.BoardingTypeID == id);
            if (boardingType == null)
            {
                return NotFound();
            }
            return View(boardingType);
        }

        // POST: BoardingType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BoardingTypeID,BoardingTypeName,BoardingTypeDescription,BoardingPrice,UserDefined1,UserDefined2")] BoardingType boardingType)
        {
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
