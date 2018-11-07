using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StableManager.Models
{
    public class BillingModels
    {
    }

    public class Bill
    {
        //IDs
        public string BillID { get; set; }
        [Display(Name = "Bill Number")]
        public string BillNumber { get; set; }

        //details
        [Display(Name = "Created On")]
        public DateTime BillCreatedOn { get; set; }
        [Display(Name = "Due On")]
        public DateTime BillDueOn { get; set; }
        [Display(Name = "Billing From")]
        public DateTime BillFrom { get; set; }
        [Display(Name = "Billing To")]
        public DateTime BillTo { get; set; }
        [Display(Name = "Net Total")]
        public double BillNetTotal { get; set; }
        [Display(Name = "Tax")]
        public double BillTaxTotal { get; set; }
        [Display(Name = "Total Due (Current)")]
        public double BillCurrentAmountDue { get; set; }
        [Display(Name = "Total Due (Past Due)")]
        public double BillPastDueAmountDue { get; set; }
        [Display(Name = "Total Due")]
        public double BillTotalAmountDue { get; set; }



        //owner of the bill
        [Display(Name = "Bill To")]
        public virtual ApplicationUser User { get; set; }
        public string UserID { get; set; }

        //who made the bill (user id)
        [Display(Name = "Created By")]
        public string BillCreatorID { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }
}
