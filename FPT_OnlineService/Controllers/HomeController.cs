using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace FPT_OnlineService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string role = "";
            if (!string.IsNullOrEmpty(FPT_OnlineService.Models.User.UserRole))
                role = FPT_OnlineService.Models.User.UserRole;
            if (User.IsInRole("FPT-Staff") || role.Contains("Staff")
                || User.IsInRole("HeadOfAcademicDepartment-Staff") || User.IsInRole("AcademicHead-Staff")
                || User.IsInRole("CampusDirector-Staff") || User.IsInRole("Academic-Staff"))
                return RedirectToAction("Index", "Staff");
            else if (User.IsInRole("Student") || role.Equals("Student"))
                return RedirectToAction("Index", "Student");
            else if (User.IsInRole("Admin") || role.Equals("Admin"))
                return RedirectToAction("Index", "Admins");
            else
                return View();
        }
        [HttpPost]
        public ViewResult Index(Models.MailModel _objModelMail)
       {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("fuonlineservice2016@gmail.com"));
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject +" from: "+ _objModelMail.FullName;
                string Body = "<p>"+_objModelMail.Subject + " from: " + _objModelMail.FullName +"</p> "+_objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("fuonlineservice2016@gmail.com", "ATS3....");// Enter senders User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Success", _objModelMail);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Success(Models.MailModel _objModelMail)
        {
            return View(_objModelMail);
        }

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}