using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPT_OnlineService.Models
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Student")]
        public string StudentStudentRollNo { get; set; }

        [ForeignKey("Form")]
        public int FormID { get; set; }

        [ForeignKey("Staff")]
        public string StaffUsername { get; set; }

        public string Message { get; set; }

        public virtual Student Student { get; set; }
        public virtual Form Form { get; set; }
        public virtual Staff Staff { get; set; }
    }
}