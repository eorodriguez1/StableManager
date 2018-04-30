using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StableManager.Models
{
    public class UserModels
    {
    }

    public class Client
    {
        //IDs
        public string ClientID { get; set; }
        [Display(Name = "Client Number")]
        public string ClientNumber { get; set; }

        //foreign key to user
        [Display(Name = "Name")]
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Name")]
        public string UserID { get; set; }

        //Vet info
        [Display(Name = "Preferred Vet")]
        public string PreferredVet { get; set; }
        [Display(Name = "Vet Information")]
        public string PreferredVetDetails { get; set; }
        [Display(Name = "Alternative Vet")]
        public string AlternativeVet { get; set; }
        [Display(Name = "Vet Information")]
        public string AlternativeVetDetails { get; set; }

        //security things
        [Display(Name = "Modified On")]
        public DateTime ModifiedOn { get; set; }
        [Display(Name = "Modified By")]
        public string ModifierUserID { get; set; }
    }




    //TO IMPLEMENT
    public class Trainer
    {

    }

    public class Instructor
    {

    }
}
