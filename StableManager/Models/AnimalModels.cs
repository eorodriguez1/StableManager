using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StableManager.Models
{
    public class AnimalModels
    {
    }

    public class Animal
    {
        //IDs
        public string AnimalID { get; set; }
        [Display(Name = "Animal Number")]
        public string AnimalNumber { get; set; }


        //Animal Details
        [Display(Name = "Name")]
        public string AnimalName { get; set; }
        [Display(Name = "Breed")]
        public string Breed { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        [Display(Name = "Type")]
        public string AnimalType { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        //special information
        [Display(Name = "Health Concerns")]
        public string HealthConcerns { get; set; }
        [Display(Name = "Diet Required")]
        public Boolean SpecialDiet { get; set; }
        [Display(Name = "Diet Details")]
        public string DietDetails { get; set; }

        //Owner
        [Display(Name = "Owned By")]
        public virtual ApplicationUser AnimalOwner { get; set; }
        [Display(Name = "Owned By")]
        public string AnimalOwnerID { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }


    public class AnimalHealthUpdates
    {
        //ID
        public string AnimalHealthUpdatesID { get; set; }

        //details
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Date")]
        public DateTime DateOccured { get; set; }

        
        public virtual Animal Animal { get; set; }
        [Display(Name = "Animal")]
        public string AnimalID { get; set; }
        [Display(Name = "By")]
        public string UserBy { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }

    public class AnimalToOwner
    {
        //ID
        public string AnimalToOwnerID { get; set; }

        //Owner
        [Display(Name = "Owned By")]
        public virtual ApplicationUser Owner { get; set; }
        public string OwnerID { get; set; }
        
        //Animal
        [Display(Name = "Animal")]
        public virtual Animal Animal { get; set; }
        [Display(Name = "Animal")]
        public string AnimalID { get; set; }

        //security things
        [Display(Name = "From")]
        public DateTime OwnershipStartedOn { get; set; }
        [Display(Name = "To")]
        public DateTime OwnershipEndedOn { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }
}
