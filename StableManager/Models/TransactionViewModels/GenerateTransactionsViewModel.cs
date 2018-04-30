using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StableManager.Models.TransactionViewModels
{
    public class GenerateTransactionsViewModel
    {
        [Display(Name = "Bill To")]
        public virtual ApplicationUser BilledTo { get; set; }
        [Display(Name = "Bill To")]
        public string BilledToID { get; set; }

        //Billing Period
        [Display(Name = "From")]
        public DateTime BillFrom { get; set; }
        [Display(Name = "To")]
        public DateTime BillTo { get; set; }


    }
}
