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

        /// <summary>
        /// Index for all transaction type actions
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionType.ToListAsync());
        }

        /// <summary>
        /// Get the details of a specific transaction type
        /// </summary>
        /// <param name="id">id for transaction type</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            //if id is not specified, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try and find the transaction type
            var transactionType = await _context.TransactionType
                .SingleOrDefaultAsync(m => m.TransactionTypeID == id);
            //if transaction type is not found, return not found
            if (transactionType == null)
            {
                return NotFound();
            }

            //enum of possible types of transaction methods
            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString() };
            ViewData["Types"] = new SelectList(EnumList);

            return View(transactionType);
        }

        /// <summary>
        /// Create a new transaction type
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            //enum of possible types of transaction methods
            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString()};
            ViewData["Types"] = new SelectList(EnumList);
            return View();
        }

        /// <summary>
        /// Create a new transaction type
        /// </summary>
        /// <param name="transactionType">Transaction type object to create</param>
        /// <returns></returns>
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
            //enum of possible types of transaction methods
            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString() };
            ViewData["Types"] = new SelectList(EnumList);
            return View(transactionType);
        }

        /// <summary>
        /// Edit a specific transaction type
        /// </summary>
        /// <param name="id">id of transaction to edit</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            //if id is not specfied, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try and find transaction type
            var transactionType = await _context.TransactionType.SingleOrDefaultAsync(m => m.TransactionTypeID == id);
            //if tranaction is not found, return not found
            if (transactionType == null)
            {
                return NotFound();
            }
            //enum of possible types of transaction methods
            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString() };
            ViewData["Types"] = new SelectList(EnumList);
            return View(transactionType);
        }

        /// <summary>
        /// Edit a specific Transaction Type
        /// </summary>
        /// <param name="id">id of transaction to edit</param>
        /// <param name="transactionType">Transaction type object to edit</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TransactionTypeID,TransactionTypeName,TransactionDescription,Type,ModifiedOn,ModifierUserID")] TransactionType transactionType)
        {
            //if id and transaction type object's id do not match, return not found
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

            //enum of possible types of transaction methods
            var EnumList = new List<String>() { DebitCredit.Receiveable.ToString(), DebitCredit.Payment.ToString() };
            ViewData["Types"] = new SelectList(EnumList);
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
