using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StableManager.Models.TransactionViewModels
{


    public class UserTransactionsViewModel
    {
 
        [Display(Name = "User Charged")]
        public string UserCharged { get; set; }
        [Display(Name = "UserID Charged")]
        public string UserChargedID { get; set; }

        [Display(Name = "Balance")]
        public double Balance { get; set; }
        [Display(Name = "Transaction To Date")]
        public double TransactionCount { get; set; }


        /// <summary>
        /// Used to generate a summary of all transactions of a single user
        /// </summary>
        /// <param name="userName">name of user</param>
        /// <param name="transactions">list of transactions</param>
        public UserTransactionsViewModel(string userName, List<Transaction> transactions)
        {
            UserCharged = userName;
            UserChargedID = transactions.FirstOrDefault().UserChargedID;
            Balance = transactions.Sum(t => t.TransactionValue);
            TransactionCount = transactions.Count();


        }


        /// <summary>
        /// Used to generate a sumary of a user with no transactions
        /// </summary>
        /// <param name="user"></param>
        public UserTransactionsViewModel(ApplicationUser user)
        {
            UserCharged = user.FullName;
            UserChargedID = user.Id;
            Balance = 0;
            TransactionCount = 0;
        }
    }
}
