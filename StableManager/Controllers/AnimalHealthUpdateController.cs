﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StableManager.Data;
using StableManager.Models;

namespace StableManager.Controllers
{
    /// <summary>
    /// A controller to manage all actions to the Animal Health Update Context
    /// </summary>
    /// 
    [Authorize]
    public class AnimalHealthUpdateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnimalHealthUpdateController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Get the Index page. This will return a result based on the authentication of the user
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> Index()
        {
            //get the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //if user is an admin, go to manage page, else go to personal list of animals
            if (AppUser.IsAdmin)
            {
                return RedirectToAction("ManageUpdates");
            }
            else
            {
                return RedirectToAction("Index", "Portal");
            }

        }

        /// <summary>
        /// This is an admin page to manage all of the updates in the system
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageUpdates()
        {
            var applicationDbContext = _context.AnimalHealthUpdates.Include(a => a.Animal);
            return View(await applicationDbContext.ToListAsync());
        }


        /// <summary>
        /// Gets the view only data for an individual update to an animal
        /// </summary>
        /// <param name="id">database id for the update</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {

            //if id is null, return not found
            if (id == null)
            {
                return NotFound();
            }

            //find the update based on the id given, if it doesnt match up to an entry, return not found
            var animalHealthUpdates = await _context.AnimalHealthUpdates
                .Include(a => a.Animal)
                .SingleOrDefaultAsync(m => m.AnimalHealthUpdatesID == id);
            if (animalHealthUpdates == null)
            {
                return NotFound();
            }

            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            //return the health update
            return View(animalHealthUpdates);
        }



        /// <summary>
        /// An action required to create a new update. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> NewUpdate(string id)
        {
            //get the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //if the id is null, return not found
            if (id == null)
            {
                return NotFound();
            }
            // find the animal based on the id given,
            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.AnimalID == id);

            //if animal does not belong to the current owner, return not found
            // if it doesnt match up to an entry, return not found
            if (animal == null || animal.AnimalOwnerID != AppUser.Id)
            {
                return NotFound();
            }

            //create a new update and sync it with the specified animal
            var AnimalUpdate = new AnimalHealthUpdates();
            AnimalUpdate.AnimalID = id;


            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", AnimalUpdate.AnimalID);
            return View(AnimalUpdate);


        }

        /// <summary>
        /// The posting action of the new Update. 
        /// </summary>
        /// <param name="id">animal id</param>
        /// <param name="animalHealthUpdates">animal update created in previous page</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUpdate(string id, [Bind("AnimalHealthUpdatesID,Name,Description,DateOccured,AnimalID,UserBy,ModifiedOn,ModifierUserID")] AnimalHealthUpdates animalHealthUpdates)
        {
            //if the id does not match the animal id passed in
            if (id != animalHealthUpdates.AnimalID)
            {
                return NotFound();
            }

            //as long as the model is valid
            if (ModelState.IsValid)
            {

                //update the modified by and modified on fields
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                animalHealthUpdates.ModifierUserID = CurrentUser.FullName;
                animalHealthUpdates.ModifiedOn = DateTime.Now;

                //save the object
                _context.Add(animalHealthUpdates);
                await _context.SaveChangesAsync();

                //return to my animal
                return RedirectToAction("MyAnimalDetails", "Animal", new { id });
            }

            //return
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            return View(animalHealthUpdates);
        }


        // GET: AnimalHealthUpdate/Create
        public IActionResult Create()
        {

            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName");
            return View();
        }

        // POST: AnimalHealthUpdate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalHealthUpdatesID,Name,Description,DateOccured,AnimalID,UserBy,ModifiedOn,ModifierUserID")] AnimalHealthUpdates animalHealthUpdates)
        {
            if (ModelState.IsValid)
            {
                //update the modified by and modified on fields
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                animalHealthUpdates.ModifierUserID = CurrentUser.FullName;
                animalHealthUpdates.ModifiedOn = DateTime.Now;

                _context.Add(animalHealthUpdates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            return View(animalHealthUpdates);
        }


        // GET: AnimalHealthUpdate/Edit/5
        public async Task<IActionResult> Update(string id)
        {
            //if the id is null, return not found
            if (id == null)
            {
                return NotFound();
            }

            //fetch the update from the context, if not found return not found
            var animalHealthUpdates = await _context.AnimalHealthUpdates.SingleOrDefaultAsync(m => m.AnimalHealthUpdatesID == id);
            if (animalHealthUpdates == null)
            {
                return NotFound();
            }

            //find the animal based on the id given, if it doesnt match up to an entry, return not found
            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.AnimalID == animalHealthUpdates.AnimalID);
            //get the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //if animal does not belong to the current owner, return not found
            if ((animal == null) || (AppUser.IsAdmin==false  && animal.AnimalOwnerID != AppUser.Id))
            {
                return NotFound();
            }

            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            return View(animalHealthUpdates);
        }

        // POST: AnimalHealthUpdate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, [Bind("AnimalHealthUpdatesID,Name,Description,DateOccured,AnimalID,UserBy,ModifiedOn,ModifierUserID")] AnimalHealthUpdates animalHealthUpdates)
        {
            //if the id passed in and the model id do not match, return not found
            if (id != animalHealthUpdates.AnimalHealthUpdatesID)
            {
                return NotFound();
            }

            //find the animal based on the id given, if it doesnt match up to an entry, return not found
            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.AnimalID == animalHealthUpdates.AnimalID);
            //get the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //if animal does not belong to the current owner, return not found
            if ((animal == null) || (AppUser.IsAdmin == false && animal.AnimalOwnerID != AppUser.Id))
            {
                return NotFound();
            }

            //if model is valid
            if (ModelState.IsValid)
            {
                try
                {

                    animalHealthUpdates.ModifierUserID = AppUser.FullName;
                    animalHealthUpdates.ModifiedOn = DateTime.Now;

                    //save the update
                    _context.Update(animalHealthUpdates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalHealthUpdatesExists(animalHealthUpdates.AnimalHealthUpdatesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return to animal view
                return RedirectToAction("MyAnimalDetails", "Animal", new { id = animalHealthUpdates.AnimalID });
            }

            //return if for edits if model is incorrect
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            return View(animalHealthUpdates);
        }


        // GET: AnimalHealthUpdate/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            //if the id is null, return not found
            if (id == null)
            {
                return NotFound();
            }

            //fetch the update from the context, if not found return not found
            var animalHealthUpdates = await _context.AnimalHealthUpdates.SingleOrDefaultAsync(m => m.AnimalHealthUpdatesID == id);
            if (animalHealthUpdates == null)
            {
                return NotFound();
            }

            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            return View(animalHealthUpdates);
        }


        // POST: AnimalHealthUpdate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AnimalHealthUpdatesID,Name,Description,DateOccured,AnimalID,UserBy,ModifiedOn,ModifierUserID")] AnimalHealthUpdates animalHealthUpdates)
        {
            //if the id passed in and the model id do not match, return not found
            if (id != animalHealthUpdates.AnimalHealthUpdatesID)
            {
                return NotFound();
            }

            //if model is valid
            if (ModelState.IsValid)
            {
                try
                {

                    //update the modified by and modified on fields
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    animalHealthUpdates.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    animalHealthUpdates.ModifiedOn = DateTime.Now;

                    //save the update
                    _context.Update(animalHealthUpdates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalHealthUpdatesExists(animalHealthUpdates.AnimalHealthUpdatesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return to animal view
                return RedirectToAction(nameof(Index));
            }

            //return if for edits if model is incorrect
            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            return View(animalHealthUpdates);
        }


        // GET: AnimalHealthUpdate/Delete/5
        public async Task<IActionResult> DeleteUpdate(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalHealthUpdates = await _context.AnimalHealthUpdates
                .Include(a => a.Animal)
                .SingleOrDefaultAsync(m => m.AnimalHealthUpdatesID == id);
            if (animalHealthUpdates == null)
            {
                return NotFound();
            }

            //find the animal based on the id given, if it doesnt match up to an entry, return not found
            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.AnimalID == animalHealthUpdates.AnimalID);
            //get the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //if animal does not belong to the current owner, return not found
            if ((animal == null) || (AppUser.IsAdmin == false && animal.AnimalOwnerID != AppUser.Id))
            {
                return NotFound();
            }

            ViewData["AnimalID"] = new SelectList(_context.Animals, "AnimalID", "AnimalName", animalHealthUpdates.AnimalID);
            return View(animalHealthUpdates);
        }

        // POST: AnimalHealthUpdate/Delete/5
        [HttpPost, ActionName("DeleteUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var animalHealthUpdates = await _context.AnimalHealthUpdates.SingleOrDefaultAsync(m => m.AnimalHealthUpdatesID == id);
            _context.AnimalHealthUpdates.Remove(animalHealthUpdates);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyAnimalDetails", "Animal", new { id = animalHealthUpdates.AnimalID });
        }

        private bool AnimalHealthUpdatesExists(string id)
        {
            return _context.AnimalHealthUpdates.Any(e => e.AnimalHealthUpdatesID == id);
        }

    }
}
