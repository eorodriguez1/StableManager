using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models.UserViewModels
{
    public class EditUserViewModel
    {

        [HiddenInput(DisplayValue = false)]
        public string UserID { get; set; }
        [Display(Name = "Active")]
        public bool ActiveUser { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


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

        //security things
        [Display(Name = "Last Logged On")]
        public DateTime LastLoggedOn { get; set; }
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }



        public EditUserViewModel(ApplicationUser appUser)
        {
            appUser.Id = this.UserID;
            appUser.ActiveUser = this.ActiveUser;
            appUser.Address = this.Address;
            appUser.City = this.City;
            appUser.Country = this.Country;
            appUser.Email = this.Email;
            appUser.FirstName = this.FirstName;
            appUser.IsAdmin = this.IsAdmin;
            appUser.IsClient = this.IsClient;
            appUser.IsEmployee = this.IsEmployee;
            appUser.IsInstructor = this.IsInstructor;
            appUser.IsManager = this.IsManager;
            appUser.IsTrainer = this.IsTrainer;
            appUser.LastName = this.LastName;
            appUser.PhoneNumber = this.PhoneNumber;
            appUser.PostalCode = this.PostalCode;
            appUser.ProvState = this.ProvState;


        }

        public EditUserViewModel()
        {

        }


    }
}
