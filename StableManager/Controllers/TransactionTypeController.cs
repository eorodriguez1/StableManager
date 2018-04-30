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
    public class TransactionTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransactionType
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionType.ToListAsync());
        }

        // GET: TransactionType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionType = await _context.TransactionType
                .SingleOrDefaultAsync(m => m.TransactionTypeID == id);
            if (transactionType == null)
            {
                return NotFound();
            }

            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString() };
            ViewData["Types"] = new SelectList(EnumList);

            return View(transactionType);
        }

        // GET: TransactionType/Create
        public IActionResult Create()
        {
            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString()};
            ViewData["Types"] = new SelectList(EnumList);
            return View();
        }

        // POST: TransactionType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionTypeID,TransactionTypeName,TransactionDescription,Type,ModifiedOn,ModifierUserID")] TransactionType transactionType)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                transactionType.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                transactionType.ModifiedOn = DateTime.Now;
                _context.Add(transactionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionType);
        }

        // GET: TransactionType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionType = await _context.TransactionType.SingleOrDefaultAsync(m => m.TransactionTypeID == id);
            if (transactionType == null)
            {
                return NotFound();
            }
            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString() };
            ViewData["Types"] = new SelectList(EnumList);
            return View(transactionType);
        }

        // POST: TransactionType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TransactionTypeID,TransactionTypeName,TransactionDescription,Type,ModifiedOn,ModifierUserID")] TransactionType transactionType)
        {
            if (id != transactionType.TransactionTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    transactionType.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    transactionType.ModifiedOn = DateTime.Now;
                    _context.Update(transactionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionTypeExists(transactionType.TransactionTypeID))
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
            return View(transactionType);
        }

        // GET: TransactionType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionType = await _context.TransactionType
                .SingleOrDefaultAsync(m => m.TransactionTypeID == id);
            if (transactionType == null)
            {
                return NotFound();
            }

            return View(transactionType);
        }

        // POST: TransactionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transactionType = await _context.TransactionType.SingleOrDefaultAsync(m => m.TransactionTypeID == id);
            _context.TransactionType.Remove(transactionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionTypeExists(string id)
        {
            return _context.TransactionType.Any(e => e.TransactionTypeID == id);
        }
    }
}
