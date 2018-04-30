using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models
{
    public class BoardingModels
    {
    }
    
    /// <summary>
    /// A Class that defines the type of boarding in a stable
    /// </summary>
    public class BoardingType
    {   //IDs
        public string BoardingTypeID { get; set; }

        //Boarding information
        [Display(Name = "Boarding Type")]
        public string BoardingTypeName { get; set; }
        [Display(Name = "Short Name")]
        public string BoardingTypeNameShort { get; set; }
        [Display(Name = "Description")]
        public string BoardingTypeDescription { get; set; }

        //Cost information
        [Display(Name = "Cost")]
        public double BoardingPrice { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }


    /// <summary>
    /// Individual Boarding for each animal in the stable
    /// </summary>
    public class Boarding
    {
        //IDs
        public string BoardingID { get; set; }

        //foreign keys - Link to Boarding Animal and to Owner
        public virtual Animal Animal { get; set; }
        [Display(Name = "Animal")]
        public string AnimalID { get; set; }
        public virtual ApplicationUser BillToUser { get; set; }
        [Display(Name = "Bill To")]
        public string BillToUserID { get; set; }

        //boarding times for bill
        [Display(Name = "Started On")]
        public DateTime StartedBoard { get; set; }
        [Display(Name = "Ended On")]
        public DateTime EndedBoard { get; set; }
        

        //get boarding type
        public virtual BoardingType BoardingType { get; set; }
        [Display(Name = "Boarding Type")]
        public string BoardingTypeID { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }
}
