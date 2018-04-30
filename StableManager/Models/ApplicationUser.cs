using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StableManager.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "User Number")]
        public string UserNumber { get; set; }      //Easy Number to lookup

        //Name
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        //Billing address (if required)
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Province/State")]
        public string ProvState { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
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

        //User Activity
        [Display(Name = "Last Logged On")]
        public DateTime LastLoggedOn { get; set; }
        [Display(Name = "Active")]
        public bool ActiveUser { get; set; }

        //Balance
        [Display(Name = "Balance")]
        public double UserBalance { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }
}
