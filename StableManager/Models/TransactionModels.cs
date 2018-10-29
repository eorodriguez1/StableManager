using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StableManager.Models
{
    public class TransactionModels
    {

    }
    public class Transaction
    {
        //IDs
        public string TransactionID { get; set; }
        [Display(Name = "Transaction #")]
        public string TransactionNumber { get; set; }

        //transaction details
        [Display(Name = "Value")]
        public double TransactionValue { get; set; }
        [Display(Name = "Transaction Date")]
        public DateTime? TransactionMadeOn { get; set; }
        [Display(Name = "Description")]
        public string TransactionAdditionalDescription { get; set; }

        //foreign key
        [Display(Name = "Type")]
        public virtual TransactionType TransactionType { get; set; }
        [Display(Name = "Type")]
        public string TransactionTypeID { get; set; }

        //who made the transaction (UserID)
        [Display(Name = "Charged To")]
        public virtual ApplicationUser UserCharged { get; set; }
        [Display(Name = "Charged To")]
        public string UserChargedID { get; set; }

        //transaction is for what animal? 
        [Display(Name = "For")]
        public virtual Animal Animal { get; set; }
        [Display(Name = "Charged For")]
        public string AnimalID { get; set; }


        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }

    public class TransactionType
    {
        //IDs
        public string TransactionTypeID { get; set; }

        //Details
        [Display(Name = "Name")]
        public string TransactionTypeName { get; set; }
        [Display(Name = "Description")]
        public string TransactionDescription { get; set; }

        //Type
        [Display(Name = "Type")]
        public DebitCredit Type { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }

    public enum DebitCredit
    {
        [Display(Name = "Payment")]
        Payment,
        [Display(Name = "Receiveable")]
        Receiveable,
        [Display(Name = "Adjustment")]
        Adjustment
    }
}
