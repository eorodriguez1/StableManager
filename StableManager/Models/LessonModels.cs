using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StableManager.Models
{
    public class LessonModels
    {
    }

    public class Lesson
    {
        //IDs
        public string LessonID { get; set; }
        public string LessonNumber { get; set; }

        //details
        public DateTime LessonTime { get; set; }
        public double LessonLength { get; set; }
        public string LessonDescription { get; set; }
        public double LessonCost { get; set; }

        //security things
        public DateTime ModifiedOn { get; set; }
        public string ModifierUserID { get; set; }
    }

    public class LessonToUsers
    {
        //IDs
        public string LessonToUsersID { get; set; }
       
        public virtual ApplicationUser ClientUser { get; set; }
        public string ClientUserID { get; set; }
        public virtual ApplicationUser InstructorUser { get; set; } 
        public string InstructorUserID { get; set; }

        //security things
        public DateTime ModifiedOn { get; set; }
        public string ModifierUserID { get; set; }
    }
}
