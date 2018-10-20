using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models.AnimalViewModels
{
    public class MyAnimalsViewModel
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

        //special information
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "Diet Details")]
        public string DietDetails { get; set; }


        //Updates
        [Display(Name = "Updates To Date")]
        public int UpdatesToDate { get; set; }
        [Display(Name = "Latest Update")]
        public string LatestUpdate { get; set; }
        [Display(Name = "Updated On")]
        public DateTime UpdatedOccuredOn { get; set; }

    }
}



