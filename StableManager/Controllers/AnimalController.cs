using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StableManager.Data;
using StableManager.Models;
using StableManager.Models.AnimalViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace StableManager.Controllers
{
    /// <summary>
    /// This controls all Animal related back-end interactions.
    /// Should only be accessiable to logged in users
    /// </summary>
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnimalController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        /// <summary>
        /// This is the general result of the animal control
        /// Will re-direct based on authority
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            //get the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //if user is an admin, go to manage page, else go to personal list of animals
            if (AppUser.IsAdmin)
            {
                return RedirectToAction("ManageAnimals");
            }
            else
            {
                return RedirectToAction("MyAnimals");
            }

        }

        /// <summary>
        /// this will list the personal list of animals
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyAnimals()
        {
            //get the current user
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //A list of My Animals using the View Model
            var MyAnimalList = new List <MyAnimalsViewModel>();
            
            //this will narrow down results to include animals owned by current user
            var Animals = await _context.Animals.Include(a => a.AnimalOwner).Where(u => u.AnimalOwnerID.Equals(AppUser.Id)).ToListAsync();

            //for each animal owned, setup an animalVM
            foreach (Animal animal in Animals)
            {
                var MyAnimaslVM = new MyAnimalsViewModel();
                MyAnimaslVM.AnimalID = animal.AnimalID;
                MyAnimaslVM.AnimalName = animal.AnimalName;
                MyAnimaslVM.Breed = animal.Breed;
                MyAnimaslVM.DietDetails = animal.DietDetails;
                MyAnimaslVM.Gender = animal.Gender;
                MyAnimaslVM.HealthConcerns = animal.HealthConcerns;

                //get the list of health updates for each animal
                var HealthUpdates = await _context.AnimalHealthUpdates.Where(hu => hu.AnimalID == animal.AnimalID).OrderByDescending(hu => hu.DateOccured).ToListAsync();

                //if updates are found, update the VM
                if (HealthUpdates.Count > 0)
                {
                    MyAnimaslVM.UpdatesToDate = HealthUpdates.Count();
                    MyAnimaslVM.UpdatedOccuredOn = HealthUpdates.FirstOrDefault().DateOccured;
                    MyAnimaslVM.LatestUpdate = HealthUpdates.FirstOrDefault().Description;
                }

                //Add animalVM to the list of owned animals
                MyAnimalList.Add(MyAnimaslVM);
            }

            //return list of owned animals
            return View(MyAnimalList);

        }

        /// <summary>
        /// This will list all animals in the system
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> ManageAnimals()
        {

            //get all animals from the dataset
            var applicationDbContext = _context.Animals.Include(a => a.AnimalOwner);
            return View(await applicationDbContext.ToListAsync());

        }

        /// <summary>
        /// This will get the Details of any user in the system
        /// </summary>
        /// <param name="id">ID of the animal</param>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Details(string id)
        {
            //if parameter id is null, return not found
            if (id == null)
            {
                return NotFound();
            }

            //if animal with parameter id is nout found, return null
            var animal = await _context.Animals
                .SingleOrDefaultAsync(m => m.AnimalID == id);

            //if animal is null, return not found
            if (animal == null)
            {
                return NotFound();
            }

            //return a list of owners ID as the key and the full name as visible data
            ViewData["OwnerID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", animal.AnimalOwnerID);
            return View(animal);
        }

        /// <summary>
        /// Action that returns the details of an individual's animal
        /// </summary>
        /// <param name="id">id of animal</param>
        /// <returns></returns>
        public async Task<IActionResult> MyAnimalDetails(string id)
        {
            //if id is null, no animal was queried and return not found
            if (id == null)
            {
                return NotFound();
            }

            //if animal with parameter id is nout found, return null
            var animal = await _context.Animals
                .SingleOrDefaultAsync(m => m.AnimalID == id);
            //return not found if animal is null
            if (animal == null)
            {
                return NotFound();
            }

            //returns a list of health updates for an specific animal. If empty, returns nothing
            var Updates = await _context.AnimalHealthUpdates.Where(a => a.AnimalID.Equals(id)).OrderByDescending(u => u.DateOccured).ToListAsync();

            //this sets up the Animal View Model that can be seen by any user.
            var MyAnimalVM = new MyAnimalDetailsViewModel();
            MyAnimalVM.AnimalID = animal.AnimalID;
            MyAnimalVM.AnimalName = animal.AnimalName;
            MyAnimalVM.DOB = animal.DOB;
            MyAnimalVM.Breed = animal.Breed;
            MyAnimalVM.Gender = animal.Gender;
            MyAnimalVM.SpecialDiet = animal.SpecialDiet;
            MyAnimalVM.DietDetails = animal.DietDetails;
            MyAnimalVM.ModifiedOn = animal.ModifiedOn;
            MyAnimalVM.ModifierUserID = animal.ModifierUserID;
            MyAnimalVM.HealthConcerns = animal.HealthConcerns;
            MyAnimalVM.AnimalUpdates = Updates;

            //We get the boarding for the animal
            var AnimalBoarding = await _context.Boardings.Where(b => b.AnimalID.Equals(id)).OrderByDescending(b => b.StartedBoard).FirstOrDefaultAsync();

            //if boarding is null, return VM
            if (AnimalBoarding == null)
            {
                return View(MyAnimalVM);
            }

            //a boarding type is initialized. This will hold the boarding details if found.
            var AnimalBoardingType = new BoardingType();
            AnimalBoardingType = await _context.BoardingType.SingleOrDefaultAsync(b => b.BoardingTypeID == AnimalBoarding.BoardingTypeID);
            //if boarding type is null, return VM
            if (AnimalBoardingType == null)
            {
                return View(MyAnimalVM);
            }

            //set the Boarding VM info from return dataset information
            MyAnimalVM.BoardingTypeName = AnimalBoardingType.BoardingTypeName;
            MyAnimalVM.BoardingTypeDescription = AnimalBoardingType.BoardingTypeDescription;

            //return VM
            return View(MyAnimalVM);
        }


        /// <summary>
        /// Action that returns the details of an individual's animal
        /// </summary>
        /// <param name="id">id of animal</param>
        /// <returns></returns>
        public async Task<IActionResult> EditMyAnimal(string id)
        {
            //if id is null, no animal was queried and return not found
            if (id == null)
            {
                return NotFound();
            }

            //if animal with parameter id is nout found, return null
            var animal = await _context.Animals
                .SingleOrDefaultAsync(m => m.AnimalID == id);
            //return not found if animal is null
            if (animal == null)
            {
                return NotFound();
            }

            //returns a list of health updates for an specific animal. If empty, returns nothing
            var Updates = await _context.AnimalHealthUpdates.Where(a => a.AnimalID.Equals(id)).OrderByDescending(u => u.DateOccured).ToListAsync();

            //this sets up the Animal View Model that can be seen by any user.
            var MyAnimalVM = new MyAnimalDetailsViewModel();
            MyAnimalVM.AnimalID = animal.AnimalID;
            MyAnimalVM.AnimalName = animal.AnimalName;
            MyAnimalVM.DOB = animal.DOB;
            MyAnimalVM.Breed = animal.Breed;
            MyAnimalVM.Gender = animal.Gender;
            MyAnimalVM.SpecialDiet = animal.SpecialDiet;
            MyAnimalVM.DietDetails = animal.DietDetails;
            MyAnimalVM.ModifiedOn = animal.ModifiedOn;
            MyAnimalVM.ModifierUserID = animal.ModifierUserID;
            MyAnimalVM.HealthConcerns = animal.HealthConcerns;
            MyAnimalVM.AnimalUpdates = Updates;

            //We get the boarding for the animal
            var AnimalBoarding = await _context.Boardings.Where(b => b.AnimalID.Equals(id)).OrderByDescending(b => b.StartedBoard).FirstOrDefaultAsync();

            //if boarding is null, return VM
            if (AnimalBoarding == null)
            {
                return View(MyAnimalVM);
            }

            //a boarding type is initialized. This will hold the boarding details if found.
            var AnimalBoardingType = new BoardingType();
            AnimalBoardingType = await _context.BoardingType.SingleOrDefaultAsync(b => b.BoardingTypeID == AnimalBoarding.BoardingTypeID);
            //if boarding type is null, return VM
            if (AnimalBoardingType == null)
            {
                return View(MyAnimalVM);
            }

            //set the Boarding VM info from return dataset information
            MyAnimalVM.BoardingTypeName = AnimalBoardingType.BoardingTypeName;
            MyAnimalVM.BoardingTypeDescription = AnimalBoardingType.BoardingTypeDescription;

            //return VM
            return View(MyAnimalVM);
        }


        /// <summary>
        /// Saves the updated animal VM based on a normal user's changes.
        /// </summary>
        /// <param name="id">id of animal</param>
        /// <param name="animalVM">Animal VM currently being updated</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMyAnimal(string id, MyAnimalDetailsViewModel animalVM)
        {
            //if id does not match the animalVM id, return not found
            if (id != animalVM.AnimalID)
            {
                return NotFound();
            }

            var animal = await _context.Animals.SingleOrDefaultAsync(a => a.AnimalID == animalVM.AnimalID);

            //if the model is valid, try to update the animal
            if (ModelState.IsValid)
            {
                try
                {
                    //update animal field with user's changes
                    animal.DietDetails = animalVM.DietDetails;
                    animal.SpecialDiet = animalVM.SpecialDiet;
                    animal.HealthConcerns = animalVM.HealthConcerns;

                    //update the modified by and modified on fields
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    animal.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    animal.ModifiedOn = DateTime.Now;

                    //update the animal and save it
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return to index
                return RedirectToAction(nameof(MyAnimalDetails), new { id} );
            }

            //return VM
            return View(animalVM);
        }




        /// <summary>
        /// Create an animal in the system
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Create()
        {
            //return a select list of IDs (key) and the full names of the users (visible text)
            ViewData["OwnerID"] = new SelectList(_context.ApplicationUser, "Id", "FullName");

            return View();
        }

        /// <summary>
        /// Posting action for creating a new animal
        /// </summary>
        /// <param name="animal">animal model that is being passed in</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([Bind("AnimalID,AnimalNumber,AnimalName,Breed,Gender,Age,AnimalType,HealthConcerns,SpecialDiet,DietDetails,AnimalOwnerID,ModifiedOn,ModifierUserID")] Animal animal)
        {
            //if animal model is valid, try to create the animal in the system
            if (ModelState.IsValid)
            {
                //set the animal as active
                animal.IsActive = true;

                //set the modified by and modified on fields
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                animal.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                animal.ModifiedOn = DateTime.Now;

                //add visible animal number to the dataset
                animal.AnimalNumber = (_context.Animals.Count() + 1).ToString();

                //add the animal and save it
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //return a select list of IDs (key) and the full names of the users (visible text)
            ViewData["OwnerID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", animal.AnimalOwnerID);
            return View(animal);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.AnimalID == id);
            if (animal == null)
            {
                return NotFound();
            }
            //return a select list of IDs (key) and the full names of the users (visible text)
            ViewData["OwnerID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", animal.AnimalOwnerID);
            return View(animal);
        }

        /// <summary>
        /// Posting action for an edit/update to an animal.
        /// </summary>
        /// <param name="id">id of the animal</param>
        /// <param name="animal">model data of the animal</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Edit(string id, [Bind("AnimalID,AnimalNumber,AnimalName,Breed,Gender,Age,AnimalType,HealthConcerns,SpecialDiet,DietDetails,AnimalOwnerID,ModifiedOn,ModifierUserID,IsActive")] Animal animal)
        {
            //if the model id and the passed in id do not match, return not found
            if (id != animal.AnimalID)
            {
                return NotFound();
            }

            //if the model is valid, try to update the animal
            if (ModelState.IsValid)
            {
                try
                {
                    //update the modified by and modified on fields
                    var CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                    animal.ModifierUserID = CurrentUser.FirstName + " " + CurrentUser.LastName;
                    animal.ModifiedOn = DateTime.Now;

                    //update the animal and save it
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return to index
                return RedirectToAction(nameof(Index));
            }

            //return a select list of IDs (key) and the full names of the users (visible text)
            ViewData["OwnerID"] = new SelectList(_context.ApplicationUser, "Id", "FullName", animal.AnimalOwnerID);
            return View(animal);
        }

        // GET: Animal/Delete/5
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .SingleOrDefaultAsync(m => m.AnimalID == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var animal = await _context.Animals.SingleOrDefaultAsync(m => m.AnimalID == id);
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(string id)
        {
            return _context.Animals.Any(e => e.AnimalID == id);
        }
    }
}
