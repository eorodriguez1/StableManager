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
using StableManager.Models.TransactionViewModels;

namespace StableManager.Controllers
{
    [Authorize]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ManageByUser));
        }

        // GET: Transaction
        public async Task<IActionResult> ManageCurrent()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.TransactionType).Include(t => t.UserCharged).Where(u => u.TransactionMadeOn.Value.Year == DateTime.Now.Year && u.TransactionMadeOn.Value.Month == DateTime.Now.Month).OrderByDescending(t => t.TransactionMadeOn);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transaction
        public async Task<IActionResult> ManageTransactions()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.TransactionType).Include(t => t.UserCharged).OrderByDescending(t => t.TransactionMadeOn);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transaction
        public async Task<IActionResult> ManageByUser()
        {
            var TransactionUsers = _context.Users.Select(u => u.FullName).Distinct().ToListAsync();
            var TransactionSummaries = new List<UserTransactionsViewModel>();
            var TransactionSummary = new UserTransactionsViewModel(await _context.ApplicationUser.FirstOrDefaultAsync());

            foreach (string name in await TransactionUsers)
            {
                try
                {
                    TransactionSummary = new UserTransactionsViewModel(name, _context.Transactions.Include(t => t.UserCharged).Where(u => u.UserCharged.FullName.Equals(name)).OrderByDescending(t => t.TransactionMadeOn).ToList());

                }
                catch
                {
                    TransactionSummary = new UserTransactionsViewModel(await _context.ApplicationUser.FirstOrDefaultAsync(u => u.FullName.Equals(name)));
                }
                TransactionSummaries.Add(TransactionSummary);

            }
            return View(TransactionSummaries);
        }

        // GET: Transaction
        public async Task<IActionResult> TransactionsByUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id) == null)
            {
                return NotFound();
            }

            var applicationDbContext = _context.Transactions.Include(t => t.TransactionType).Include(t => t.UserCharged).Where(u => u.UserChargedID.Equals(id)).OrderByDescending(t => t.TransactionMadeOn);
            ViewData["BillToID"] = id;
            return View(await applicationDbContext.ToListAsync());

        }






        // GET: Transaction/Create
        public IActionResult GenerateTransactions(string id)
        {

            var GenerateTrans = new GenerateTransactionsViewModel();
            GenerateTrans.BilledToID = id;
            GenerateTrans.BillFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            GenerateTrans.BillTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);

            ViewData["blah"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);
            ViewData["BilledToID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View(GenerateTrans);
        }

        // GET: Transaction/Create
        [HttpPost]
        public async Task<IActionResult> GenerateTransactions(string id, [Bind("BilledToID,BillFrom,BillTo")] GenerateTransactionsViewModel model)
        {
            if (_context.ApplicationUser.Where(u => u.Id == model.BilledToID) == null)
            {
                return NotFound();
            }

            var BoardingList = await _context.Boardings.Where(u => u.BillToUserID.Equals(model.BilledToID)).ToListAsync();
            var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var BilledUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.BilledToID);

            foreach (Boarding Board in BoardingList)
            {
                if (Board.EndedBoard > model.BillTo)
                {
                    var NewTransaction = new Transaction();
                    NewTransaction.ModifiedOn = DateTime.Now;
                    NewTransaction.ModifierUserID = CurrentUser.Id;
                    NewTransaction.TransactionMadeOn = DateTime.Now;
                    NewTransaction.TransactionAdditionalDescription = "Boarding fees for " + (await _context.Animals.FirstOrDefaultAsync(a => a.AnimalID == Board.AnimalID)).AnimalName;
                    NewTransaction.TransactionNumber = "T" + (_context.Transactions.Count() + 1).ToString();
                   
                    NewTransaction.TransactionTypeID = (await _context.TransactionType.FirstOrDefaultAsync(t => t.TransactionTypeName.Equals("Boarding Fees"))).TransactionTypeID;
                    NewTransaction.TransactionValue = (await _context.BoardingType.FirstOrDefaultAsync(b => b.BoardingTypeID == Board.BoardingTypeID)).BoardingPrice *-1;
                    NewTransaction.UserChargedID = model.BilledToID;
                    _context.Add(NewTransaction);
                    await _context.SaveChangesAsync();


                    BilledUser.UserBalance += NewTransaction.TransactionValue;

                }
                _context.Update(BilledUser);
                await _context.SaveChangesAsync();


            }

            return RedirectToAction("TransactionsByUser", new { id = model.BilledToID });
        }


        // GET: Animal/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", transaction.UserChargedID);
            return View(transaction);
        }



        // GET: Transaction/Create
        public IActionResult Create()
        {
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName");
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionID,TransactionNumber,TransactionValue,TransactionMadeOn,TransactionAdditionalDescription,TransactionTypeID,UserChargedID,ModifiedOn,ModifierUserID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                CurrentUser.UserBalance += transaction.TransactionValue;
                transaction.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                transaction.ModifiedOn = DateTime.Now;
                if (transaction.TransactionMadeOn == null)
                {
                    transaction.TransactionMadeOn = DateTime.Now;
                }
                _context.Add(transaction);
                await _context.SaveChangesAsync();

                _context.Update(CurrentUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", transaction.UserChargedID);
            return View(transaction);
        }



        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", transaction.UserChargedID);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TransactionID,TransactionNumber,TransactionValue,TransactionMadeOn,TransactionAdditionalDescription,TransactionTypeID,UserChargedID,ModifiedOn,ModifierUserID")] Transaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    var BilledUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == transaction.UserChargedID);
                    var PreviousTransactionBalance = (await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionID == transaction.TransactionID)).TransactionValue;
                    var Balance = transaction.TransactionValue - PreviousTransactionBalance;

                    transaction.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    transaction.ModifiedOn = DateTime.Now;
                    if (transaction.TransactionMadeOn == null)
                    {
                        transaction.TransactionMadeOn = DateTime.Now;
                    }
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();

                    BilledUser.UserBalance += Balance;
                    _context.Update(BilledUser);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionID))
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
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", transaction.UserChargedID);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.TransactionType)
                .Include(t => t.UserCharged)
                .SingleOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.TransactionID == id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(string id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }
    }
}
