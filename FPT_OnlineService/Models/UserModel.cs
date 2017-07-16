using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPT_OnlineService.Models
{
    public class User
    {
        public string Name { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]", ErrorMessage = "Must be a valid mail.")]
        public string Email { get; set; }
        
        public static string role { get; set; }

        public static string UserRole
        {
            get { return role; }
            set { role = value; }
        }
    }

    public class Student : User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentRollNo { get; set; }

        [ForeignKey("ApplicationUser"), Column(Order = 2)]
        public string UserId { get; set; }

        public virtual ICollection<Form> Form { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class Staff : User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserName { get; set; }

        [ForeignKey("ApplicationUser"), Column(Order = 2)]
        public string UserId { get; set; }

        public string StaffRole { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }

    public class Admin : User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserName { get; set; }

        [ForeignKey("ApplicationUser"), Column(Order = 2)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }

}