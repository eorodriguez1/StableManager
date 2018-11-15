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
    [Authorize()]
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Redirect to correct menu
        /// </summary>
        /// <returns></returns>
        [Authorize()]
        public async Task<IActionResult> Index()
        {
            //Send user to "Manage Transactions by User" section
            
            var AppUser = await _context.Users.FirstAsync(u => u.UserName == User.Identity.Name);
            if (AppUser.IsAdmin)
            {
                return RedirectToAction(nameof(ManageByUser));
            }
            else
            {
                return RedirectToAction(nameof(MyTransactions));
            }
        }

        /// <summary>
        /// Section that allows a user to manage current transactions (For the current month)
        /// </summary>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageCurrent()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.TransactionType).Include(t => t.UserCharged).Include(t => t.Animal).Where(u => u.TransactionMadeOn.Value.Year == DateTime.Now.Year && u.TransactionMadeOn.Value.Month == DateTime.Now.Month).OrderByDescending(t => t.TransactionMadeOn);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        ///Section that allows users to manage all transactions
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageTransactions()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.TransactionType).Include(t => t.UserCharged).Include(t => t.Animal).OrderByDescending(t => t.TransactionMadeOn);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        ///Section that list all of a current users transactions
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyTransactions()
        {
            var CurrentUser = await _context.ApplicationUser.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var applicationDbContext = _context.Transactions.Include(t => t.TransactionType).Include(t => t.UserCharged).Include(t => t.Animal).Where(t => t.UserChargedID == CurrentUser.Id).OrderByDescending(t => t.TransactionMadeOn);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Section that provides the user with an overview/summary of all transactions per user (1 line per user)
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageByUser()
        {

            //get a list of all users in the system
            var TransactionUsers = _context.Users.Select(u => u.FullName).Distinct().ToListAsync();

            //uses a ViewModel to capture key information of all users
            var TransactionSummaries = new List<UserTransactionsViewModel>();

            //used to capture key information for individual users
            var TransactionSummary = new UserTransactionsViewModel(await _context.ApplicationUser.FirstOrDefaultAsync());

            //for each user in the system
            foreach (string name in await TransactionUsers)
            {
                //try to generate summary information for a user including transaction totals
                try
                {
                    TransactionSummary = new UserTransactionsViewModel(name, _context.Transactions.Include(t => t.UserCharged).Where(u => u.UserCharged.FullName.Equals(name)).OrderByDescending(t => t.TransactionMadeOn).ToList());

                }
                //if the user does not have any transactions recorded, then produce a summary with 0 totals
                catch
                {
                    TransactionSummary = new UserTransactionsViewModel(await _context.ApplicationUser.FirstOrDefaultAsync(u => u.FullName.Equals(name)));
                }
                //add the transaction summary to the list of summaries
                TransactionSummaries.Add(TransactionSummary);

            }
            //return all summaries
            return View(TransactionSummaries);
        }

        /// <summary>
        /// List of all transactions by a specific user
        /// </summary>
        /// <param name="id">id for user to look up</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> TransactionsByUser(string id)
        {
            //If user is not specified, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try and find user
            var CurrentUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            
            //if user is not found, return not found
            if (CurrentUser == null)
            {
                return NotFound();
            }

            //return a list of all transactions
            var applicationDbContext = _context.Transactions.Include(t => t.TransactionType).Include(t => t.UserCharged).Include(t=> t.Animal).Where(u => u.UserChargedID.Equals(id)).OrderByDescending(t => t.TransactionMadeOn);

            //specific fields to return (for URLs or HTML)
            ViewData["BillToID"] = id;
            ViewData["BillToName"] = CurrentUser.FullName;
           
            return View(await applicationDbContext.ToListAsync());

        }



        /// <summary>
        /// Generate monthly transactions
        /// </summary>
        /// <param name="id">id used to specify the owner of the transactions (USER ID)</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult GenerateTransactions(string id)
        {

            var GenerateTrans = new GenerateTransactionsViewModel();
            GenerateTrans.BilledToID = id;
            GenerateTrans.BillFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            GenerateTrans.BillTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            
            ViewData["BilledToID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            return View(GenerateTrans);
        }

        //TODO -- OPTIMIZE! - refactor to seperate logic to get boarding fees, services, etc.
        //                  - Possibility of adding transaction period (From & To fields) to better protect againts duplicate items
        /// <summary>
        /// Generate monthly transactions
        /// </summary>
        /// <param name="id">id used to specify the owner of the transactions (USER ID)</param>
        /// <param name="model"> transaction list view model to save</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> GenerateTransactions(string id, [Bind("BilledToID,BillFrom,BillTo")] GenerateTransactionsViewModel model)
        {

            //if user we are trying bill does not exist return not found 
            if (_context.ApplicationUser.Where(u => u.Id == model.BilledToID) == null)
            {
                return NotFound();
            }

            //get the current user for logs
            var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            //returns a list of boardings for the selected user
            var BoardingList = await _context.Boardings.Where(u => u.BillToUserID.Equals(model.BilledToID)).ToListAsync();
            //get the user we are trying to bill
            var BilledUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.BilledToID);

            //for every active boarding the user has
            foreach (Boarding Board in BoardingList)
            {
                //TODO Change so that transaction only occurs for billing period
                var test = _context.Transactions.Where(a => a.AnimalID == Board.AnimalID && a.TransactionType.TransactionTypeName.Equals("Boarding Fees") && a.TransactionMadeOn > model.BillFrom).ToList(); 
                //if we find an active boarding
                if (test.Count == 0  && (Board.EndedBoard == null || Board.EndedBoard > model.BillTo))
                {
                    //generate a new transaction for the boarding
                    var NewTransaction = new Transaction();

                    //logs
                    NewTransaction.ModifiedOn = DateTime.Now;
                    NewTransaction.ModifierUserID = CurrentUser.FullName;
                    NewTransaction.TransactionMadeOn = DateTime.Now;


                    //boarding information
                    NewTransaction.TransactionAdditionalDescription = "Boarding fees from:" + model.BillFrom.ToString("MMMM dd yyyy") + " to " +model.BillTo.ToString("MMMM dd yyyy");
                    //generate a new transaction number based on the current count of transaction and format it into a string
                    NewTransaction.TransactionNumber = "TN" + (_context.Transactions.Count() + 1).ToString("D8");
                    //save animal
                    NewTransaction.AnimalID = Board.AnimalID;

                    //get transaction data
                    NewTransaction.TransactionTypeID = (await _context.TransactionType.FirstOrDefaultAsync(t => t.TransactionTypeName.Equals("Boarding Fees"))).TransactionTypeID;
                    NewTransaction.TransactionValue = (await _context.BoardingType.FirstOrDefaultAsync(b => b.BoardingTypeID == Board.BoardingTypeID)).BoardingPrice *-1;
                    NewTransaction.UserChargedID = model.BilledToID;
                    _context.Add(NewTransaction);
                    
                    //update balance
                    BilledUser.UserBalance += NewTransaction.TransactionValue;

                }
                _context.Update(BilledUser);
                await _context.SaveChangesAsync();


            }

            return RedirectToAction("TransactionsByUser", new { id = model.BilledToID });
        }


        /// <summary>
        /// View the details of a transaction
        /// </summary>
        /// <param name="id">ID for the transaction to loopup</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Details(string id)
        {
            //if id is not specfied, return not found
            if (id == null)
            {
                return NotFound();
            }

            //try to find transaction
            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.TransactionID == id);
            //If transaction is not found, return not found
            if (transaction == null)
            {
                return NotFound();
            }

            //specific data to use for UI/URLs
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", transaction.UserChargedID);
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName");
            return View(transaction);
        }



        /// <summary>
        /// Create a new transaction
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Create()
        {
            //specific data to use for UI/URLs
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName");
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName");
            return View();
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        /// <param name="transaction">transaction object to save</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([Bind("TransactionID,TransactionNumber,TransactionValue,TransactionMadeOn,TransactionAdditionalDescription,TransactionTypeID,UserChargedID,AnimalID,ModifiedOn,ModifierUserID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                //get the current user for logs
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                //transaction logs
                transaction.ModifierUserID = CurrentUser.FullName;
                transaction.ModifiedOn = DateTime.Now;

                //get the user that is being billed
                var BilledUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == transaction.UserChargedID);
                //get the transaction type
                var TransType = await _context.TransactionType.FirstOrDefaultAsync(t => t.TransactionTypeID == transaction.TransactionTypeID);
                //generate a new transaction number based on the current count of transaction and format it into a string
                transaction.TransactionNumber = "TN" + (_context.Transactions.Count() + 1).ToString("D8");
                //if transaction date is not specified, use todays date
                if (transaction.TransactionMadeOn == null)
                {
                    transaction.TransactionMadeOn = DateTime.Now;
                }
                
                //update the balanced based on transaction type if required(Receivable = negative, else positive)
                if (TransType.Type == DebitCredit.Receiveable)
                {
                    transaction.TransactionValue = transaction.TransactionValue * -1;
                }

                //update global user balance
                BilledUser.UserBalance += transaction.TransactionValue;

                //save changes
                _context.Add(transaction);
                _context.Update(BilledUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", transaction.UserChargedID);
            return View(transaction);
        }



        /// <summary>
        /// Edit a transaction
        /// </summary>
        /// <param name="id">id of transaction to edit</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id)
        {
            //if id is not specified, return not found
            if (id == null)
            {
                return NotFound();
            }
            
            //look up transaction
            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.TransactionID == id);
            //if not found, return not found
            if (transaction == null)
            {
                return NotFound();
            }

            ViewData["TransactionTypeID"] = new SelectList(_context.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            ViewData["UserChargedID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", transaction.UserChargedID);
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName");
            return View(transaction);
        }

        /// <summary>
        /// Edit Transaction
        /// </summary>
        /// <param name="id">id of transaction to edit</param>
        /// <param name="transaction">transaction object to save</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id, [Bind("TransactionID,TransactionNumber,TransactionValue,TransactionMadeOn,TransactionAdditionalDescription,TransactionTypeID,UserChargedID,AnimalID,ModifiedOn,ModifierUserID")] Transaction transaction)
        {
            //if id and transaction object's id do not match, return not found
            if (id != transaction.TransactionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //get the current user for 
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    //update logs
                    transaction.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    transaction.ModifiedOn = DateTime.Now;

                    //get the user being billed
                    var BilledUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == transaction.UserChargedID);
                    //get the transaction type and correct balance if required.
                    var TransType = await _context.TransactionType.FirstOrDefaultAsync(t => t.TransactionTypeID == transaction.TransactionTypeID);
                    if (TransType.Type == DebitCredit.Receiveable)
                    {
                        transaction.TransactionValue = Math.Abs(transaction.TransactionValue) * -1;
                    }
                    //update balance and do not track temporary instance of old transaction.
                    var PreviousTransactionBalance = (await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.TransactionID == transaction.TransactionID)).TransactionValue;
                    var Balance = transaction.TransactionValue - PreviousTransactionBalance;
                    //update transaction date if required
                    if (transaction.TransactionMadeOn == null)
                    {
                        transaction.TransactionMadeOn = DateTime.Now;
                    }
                    //Update global user balance
                    BilledUser.UserBalance += Balance;

                    _context.Update(transaction);
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
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName");
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        [Authorize(Policy = "RequireAdministratorRole")]
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
        [Authorize(Policy = "RequireAdministratorRole")]
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
