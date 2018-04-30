using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models
{
    public class StableModels
    {
    }

    public class StableDetails
    {
        //IDs
        public string StableDetailsID { get; set; }

        //stable Details
        [Display(Name = "Stable Name")]
        public string StableName { get; set; }

        //Address
        [Display(Name = "Country")]
        public string StableCountry { get; set; }
        [Display(Name = "Province/State")]
        public string StableProvState { get; set; }
        [Display(Name = "City")]
        public string StableCity { get; set; }
        [Display(Name = "Postal Code")]
        public string StablePostalCode { get; set; }
        [Display(Name = "Address")]
        public string StableAddress { get; set; }

        //Contact Info
        [Display(Name = "General Contact")]
        public string ContactName { get; set; }
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string StableEmail { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string StablePhone { get; set; }

        //Other
        [Display(Name = "Tax Number")]
        public string TaxNumber { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }
}
