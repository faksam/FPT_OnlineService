using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPT_OnlineService.Models
{
    public class MailModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string From { get; set; }

        [EmailAddress]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}