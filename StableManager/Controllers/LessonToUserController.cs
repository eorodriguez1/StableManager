using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StableManager.Data;
using StableManager.Models;

namespace StableManager.Controllers
{
    public class LessonToUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonToUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LessonToUser
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LessonToUsers.Include(l => l.ClientUser).Include(l => l.InstructorUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LessonToUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonToUsers = await _context.LessonToUsers
                .Include(l => l.ClientUser)
                .Include(l => l.InstructorUser)
                .SingleOrDefaultAsync(m => m.LessonToUsersID == id);
            if (lessonToUsers == null)
            {
                return NotFound();
            }

            return View(lessonToUsers);
        }

        // GET: LessonToUser/Create
        public IActionResult Create()
        {
            ViewData["ClientUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName");
            ViewData["InstructorUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName");
            return View();
        }

        // POST: LessonToUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonToUsersID,ClientUserID,InstructorUserID,ModifiedOn,ModifierUserID")] LessonToUsers lessonToUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessonToUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName", lessonToUsers.ClientUserID);
            ViewData["InstructorUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName", lessonToUsers.InstructorUserID);
            return View(lessonToUsers);
        }

        // GET: LessonToUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonToUsers = await _context.LessonToUsers.SingleOrDefaultAsync(m => m.LessonToUsersID == id);
            if (lessonToUsers == null)
            {
                return NotFound();
            }
            ViewData["ClientUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName", lessonToUsers.ClientUserID);
            ViewData["InstructorUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName", lessonToUsers.InstructorUserID);
            return View(lessonToUsers);
        }

        // POST: LessonToUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LessonToUsersID,ClientUserID,InstructorUserID,ModifiedOn,ModifierUserID")] LessonToUsers lessonToUsers)
        {
            if (id != lessonToUsers.LessonToUsersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessonToUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonToUsersExists(lessonToUsers.LessonToUsersID))
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
            ViewData["ClientUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName", lessonToUsers.ClientUserID);
            ViewData["InstructorUserID"] = new SelectList(_context.ApplicationUser, "Id", "FirstName", lessonToUsers.InstructorUserID);
            return View(lessonToUsers);
        }

        // GET: LessonToUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonToUsers = await _context.LessonToUsers
                .Include(l => l.ClientUser)
                .Include(l => l.InstructorUser)
                .SingleOrDefaultAsync(m => m.LessonToUsersID == id);
            if (lessonToUsers == null)
            {
                return NotFound();
            }

            return View(lessonToUsers);
        }

        // POST: LessonToUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lessonToUsers = await _context.LessonToUsers.SingleOrDefaultAsync(m => m.LessonToUsersID == id);
            _context.LessonToUsers.Remove(lessonToUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonToUsersExists(string id)
        {
            return _context.LessonToUsers.Any(e => e.LessonToUsersID == id);
        }
    }
}
