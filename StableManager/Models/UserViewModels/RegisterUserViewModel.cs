using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models.UserViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(15, ErrorMessage = "Invalid Phone lenght")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }


        //Name
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name is too long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Name is too long")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }


        //Billing address (if required)
        [StringLength(50, ErrorMessage = "County name is too long")]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [StringLength(50, ErrorMessage = "Province/State name is too long")]
        [Display(Name = "Province/State")]
        public string ProvState { get; set; }
        [StringLength(50, ErrorMessage = "City name is too long")]
        [Display(Name = "City")]
        public string City { get; set; }
        [StringLength(10, ErrorMessage = "Invalid postal code lenght")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [StringLength(100, ErrorMessage = "Address is too long")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        //Authority --Is in role
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Client")]
        public bool IsClient { get; set; }
        [Display(Name = "Trainer")]
        public bool IsTrainer { get; set; }
        [Display(Name = "Instructor")]
        public bool IsInstructor { get; set; }
        [Display(Name = "Manager")]
        public bool IsManager { get; set; }
        [Display(Name = "Employee")]
        public bool IsEmployee { get; set; }

        //These will get auto populated
        /*
        public string UserNumber { get; set; }      //Easy Number to lookup
        //User Activity
        public DateTime LastLoggedOn { get; set; }
        public bool ActiveUser { get; set; }

        //security things
        public DateTime ModifiedOn { get; set; }
        public string ModifierUserID { get; set; }

        //Authority -- Can Edit All
        public bool CanEditBilling { get; set; }
        public bool CanEditAnimals { get; set; }
        public bool CanEditUsers { get; set; }
        public bool CanEditBoardings { get; set; }
        public bool CanEditLessons { get; set; }

        //Authority -- Can View All
        public bool CanViewAllBilling { get; set; }
        public bool CanViewAllAnimals { get; set; }
        public bool CanViewAllUsers { get; set; }
        public bool CanViewAllBoardings { get; set; }
        public bool CanViewAllLessons { get; set; }

    */

    }
}
