using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models.AnimalViewModels
{
    public class MyAnimalDetailsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string AnimalID { get; set; }

        //Animal Details
        [Display(Name = "Name")]
        public string AnimalName { get; set; }
        [Display(Name = "Breed")]
        public string Breed { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
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

        //board details
        [Display(Name = "Boarding Type")]
        public string BoardingTypeName { get; set; }
        [Display(Name = "Description")]
        public string BoardingTypeDescription { get; set; }

        //Updates
        [Display(Name = "Updates")]
        public List<AnimalHealthUpdates> AnimalUpdates { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }

    }
}



