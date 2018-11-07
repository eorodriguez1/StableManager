using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StableManager.Models.BillingViewModels
{
    public class ViewBillViewModel
    {

        public Bill Bill { get; set; }
        public List<Transaction> Transactions { get; set; }
        public StableDetails Stable { get; set; }

    }
}
