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
using StableManager.Models.BillingViewModels;

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
                return RedirectToAction("MyInvoices");
            }
        }


        /// <summary>
        /// A list of all bills available in the system
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageBilling()
        {
            var applicationDbContext = _context.Bills.Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// A list of all bills available in the system
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyInvoices()
        {
            var CurrentUser = await _context.ApplicationUser.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var applicationDbContext = _context.Bills.Include(b => b.User).Where( u => u.UserID == CurrentUser.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// A list of all current bills available in the system
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageCurrent()
        {
            var applicationDbContext = _context.Bills.Include(b => b.User).Where(b => b.BillFrom.Month == DateTime.Now.Month && b.BillFrom.Year ==DateTime.Now.Year);
            return View(await applicationDbContext.ToListAsync());
        }


        /// <summary>
        /// A list of all current bills available in the system
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageHistory()
        {
            var applicationDbContext = _context.Bills.Include(b => b.User).Where(b => b.BillFrom < DateTime.Now.AddDays(-DateTime.Now.Day));
            return View(await applicationDbContext.ToListAsync());
        }




        /// <summary>
        /// Generates a bill with transaction data for the selected period and client.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");

            //presets a billing period
            var Bill = new Bill();
            //first of the month
            Bill.BillFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //last of the month
            Bill.BillTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            return View(Bill);
        }

        /// <summary>
        /// Creates a new bill from a selected billing period for the selected user.
        /// Initializes all other fields
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([Bind("BillID,BillNumber,BillCreatedOn,BillDueOn,BillFrom,BillTo,BillNetTotal,BillTaxTotal,BillCurrentAmountDue,BillPastDueAmountDue,BillTotalAmountDue,UserID,BillCreatorID,ModifiedOn,ModifierUserID")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                //get the current user for logs
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                //transaction logs
                bill.ModifierUserID = CurrentUser.FullName;
                bill.ModifiedOn = DateTime.Now;

                //Basic bill details
                bill.BillCreatorID = CurrentUser.Id;
                bill.BillCreatedOn = DateTime.Now;
                bill.BillDueOn = bill.BillTo.AddDays(7);  //1 week to pay
                //generate a new transaction number based on the current count of transaction and format it into a string
                bill.BillNumber = "IN" + (_context.Bills.Count() + 1).ToString("D8");

                //get all of the transactions for this bill and get sums
                //Done virtually but will need to migrate to "Invoice Transactions" table to keep everything transparent
                var Transactions =  _context.Transactions.Where(t => t.UserChargedID == bill.UserID && t.TransactionMadeOn >= bill.BillFrom && t.TransactionMadeOn <= bill.BillTo).ToList();
                var CurrentTotal = (Transactions.Sum(t => t.TransactionValue) * -1);
                bill.BillTaxTotal = Math.Abs(CurrentTotal * 0.05);

                bill.BillPastDueAmountDue = _context.Transactions.Where(t => t.UserChargedID == bill.UserID && t.TransactionMadeOn < bill.BillFrom).ToList().Sum(t => t.TransactionValue) * -1.05;
                bill.BillNetTotal =  + bill.BillPastDueAmountDue + CurrentTotal;

                bill.BillCurrentAmountDue = 0;
                bill.BillTotalAmountDue = bill.BillCurrentAmountDue + bill.BillPastDueAmountDue ;

               
                //Save context
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", bill.UserID);
            return View(bill);
        }


        /// <summary>
        /// View a bill in a formated area
        /// </summary>
        /// <param name="id">bill ID</param>
        /// <returns></returns>
        public async Task<IActionResult> ViewBill(string id)
        {
            //if id is not specified, return not found
            if (id == null)
            {
                return NotFound();
            }

            //get the current bill object
            var bill = await _context.Bills
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }

            //create a view bill VM
            var ViewBill = new ViewBillViewModel();

            //Init VM with bill object
            ViewBill.Bill = bill;

            //Get all transactions for the VM
            ViewBill.Transactions = _context.Transactions.Include(a => a.Animal).Include(t => t.TransactionType).Where(t => t.UserChargedID == bill.UserID && t.TransactionMadeOn >= bill.BillFrom && t.TransactionMadeOn <= bill.BillTo).ToList();
            //if there is a past due amount, add as a seperate transaction
            if (bill.BillPastDueAmountDue > 0)
            {
                var PastDueTrans = new Transaction();
                PastDueTrans.TransactionValue = bill.BillPastDueAmountDue * -1 ;
                PastDueTrans.TransactionAdditionalDescription = "Past Due Amount From Previous Invoice";

                ViewBill.Transactions.Add(PastDueTrans);
                ViewBill.Bill.BillNetTotal += bill.BillPastDueAmountDue;
            }

            

            ViewBill.Stable = _context.StableDetails.FirstOrDefault();

            return View(ViewBill);
        }




        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult GenerateBill()
        {
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View();
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
