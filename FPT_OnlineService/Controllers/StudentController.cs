using FPT_OnlineService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Net;
using PagedList;

namespace FPT_OnlineService.Controllers
{
    [Authorize(Roles="Student")]
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET All Forms
        public ViewResult Index(string sortOrder, string currentFilter, int? page)
        {
            string id = User.Identity.GetUserId();
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            DAL.Users.GetStudent();
            string StudentRollNo = DAL.Users.GetStudentRollNo(DAL.UserInfo.Name, DAL.UserInfo.Username);
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.StudentRollNo.Equals(StudentRollNo));
            int d = count;
            switch (sortOrder)
            {
                case "type_desc":
                    forms = forms.OrderByDescending(f => f.Type);
                    break;
                case "Date":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                case "date_desc":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                default:
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(forms.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult Drafts(string sortOrder, string currentFilter, int? page)
        {
            string id = User.Identity.GetUserId();
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            string StudentRollNo = DAL.Users.GetStudentRollNo(DAL.UserInfo.Name, DAL.UserInfo.Username);
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.StudentRollNo.Equals(StudentRollNo));
            forms = forms.Where(f => f.Status.ToLower().Equals("drafts"));
            int d = count;

            switch (sortOrder)
            {
                case "type_desc":
                    forms = forms.OrderByDescending(f => f.Type);
                    break;
                case "Date":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                case "date_desc":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                default:
                    forms = forms.OrderBy(f => f.Date);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(forms.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult Trash(string sortOrder, string currentFilter, int? page)
        {
            string id = User.Identity.GetUserId();
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            string StudentRollNo = DAL.Users.GetStudentRollNo(DAL.UserInfo.Name, DAL.UserInfo.Username);
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.StudentRollNo.Equals(StudentRollNo));
            forms = forms.Where(f => f.Status.ToLower().Equals("trash"));
            int d = count;

            switch (sortOrder)
            {
                case "type_desc":
                    forms = forms.OrderByDescending(f => f.Type);
                    break;
                case "Date":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                case "date_desc":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                default:
                    forms = forms.OrderBy(f => f.Date);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(forms.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult Approved(string sortOrder, string currentFilter, int? page)
        {
            string id = User.Identity.GetUserId();
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            string StudentRollNo = DAL.Users.GetStudentRollNo(DAL.UserInfo.Name, DAL.UserInfo.Username);
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.StudentRollNo.Equals(StudentRollNo));
            forms = forms.Where(f => f.Status.ToLower().Equals("approved"));
            int d = count;

            switch (sortOrder)
            {
                case "type_desc":
                    forms = forms.OrderByDescending(f => f.Type);
                    break;
                case "Date":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                case "date_desc":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                default:
                    forms = forms.OrderBy(f => f.Date);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(forms.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult Rejected(string sortOrder, string currentFilter, int? page)
        {
            string id = User.Identity.GetUserId();
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            string StudentRollNo = DAL.Users.GetStudentRollNo(DAL.UserInfo.Name, DAL.UserInfo.Username);
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.StudentRollNo.Equals(StudentRollNo));
            forms = forms.Where(f => f.Status.ToLower().Equals("rejected"));
            int d = count;

            switch (sortOrder)
            {
                case "type_desc":
                    forms = forms.OrderByDescending(f => f.Type);
                    break;
                case "Date":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                case "date_desc":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                default:
                    forms = forms.OrderBy(f => f.Date);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(forms.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult InProgress(string sortOrder, string currentFilter, int? page)
        {
            string id = User.Identity.GetUserId();
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            string StudentRollNo = DAL.Users.GetStudentRollNo(DAL.UserInfo.Name, DAL.UserInfo.Username);
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.StudentRollNo.Equals(StudentRollNo));
            forms = forms.Where(f => f.Status.ToLower().Equals("inprogress"));
            int d = count;

            switch (sortOrder)
            {
                case "type_desc":
                    forms = forms.OrderByDescending(f => f.Type);
                    break;
                case "Date":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                case "date_desc":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                default:
                    forms = forms.OrderBy(f => f.Date);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(forms.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult New(string sortOrder, string currentFilter, int? page)
        {
            string id = User.Identity.GetUserId();
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            string StudentRollNo = DAL.Users.GetStudentRollNo(DAL.UserInfo.Name, DAL.UserInfo.Username);
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.StudentRollNo.Equals(StudentRollNo));
            forms = forms.Where(f => f.Status.ToLower().Equals("new"));
            int d = count;

            switch (sortOrder)
            {
                case "type_desc":
                    forms = forms.OrderByDescending(f => f.Type);
                    break;
                case "Date":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                case "date_desc":
                    forms = forms.OrderByDescending(f => f.Date);
                    break;
                default:
                    forms = forms.OrderBy(f => f.Date);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(forms.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult Notifications()
        //{
        //   // var notifications = db.Notifications.Include(n => n.Form).Include(n => n.Staff).Include(n => n.Student);
        //    return View(notifications.ToList());
        //}
    }
}