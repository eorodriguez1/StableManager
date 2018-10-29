using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StableManager.Data;
using StableManager.Models;

namespace StableManager.Controllers
{
    [Authorize]
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// send user to the correct billing index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var AppUser = await _context.Users.FirstAsync(u => u.UserName == User.Identity.Name);
            if (AppUser.IsAdmin)
            {
                return RedirectToAction("ManageBilling");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult GenerateBill()
        {
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View();
        }


        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageBilling()
        {
            var applicationDbContext = _context.Bills.Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([Bind("BillID,BillNumber,BillCreatedOn,BillDueOn,BillFrom,BillTo,BillNetTotal,BillTaxTotal,BillCurrentAmountDue,BillPastDueAmountDue,BillTotalAmountDue,UserID,BillCreatorID,ModifiedOn,ModifierUserID")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", bill.UserID);
            return View(bill);
        }

        // GET: Bill/Edit/5
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.SingleOrDefaultAsync(m => m.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", bill.UserID);
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id, [Bind("BillID,BillNumber,BillCreatedOn,BillDueOn,BillFrom,BillTo,BillNetTotal,BillTaxTotal,BillCurrentAmountDue,BillPastDueAmountDue,BillTotalAmountDue,UserID,BillCreatorID,ModifiedOn,ModifierUserID")] Bill bill)
        {
            if (id != bill.BillID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.BillID))
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
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", bill.UserID);
            return View(bill);
        }

        // GET: Bill/Delete/5
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bill = await _context.Bills.SingleOrDefaultAsync(m => m.BillID == id);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(string id)
        {
            return _context.Bills.Any(e => e.BillID == id);
        }
    }
}
