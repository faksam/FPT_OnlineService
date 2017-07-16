using FPT_OnlineService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using FPT_OnlineService.DAL;
using System.Collections;

namespace FPT_OnlineService.Controllers
{
    [Authorize(Roles="Student")]
    public class FormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Forms
        public ActionResult Index()
        {
            string role = "";
            if (User.IsInRole("FPT-Staff") || role.Equals("FPT-Staff") || role.Contains("Staff"))
                return RedirectToAction("Index", "Staff");
            else if (User.IsInRole("Student") || role.Equals("Student"))
                return RedirectToAction("Index", "Student");
            else
                return RedirectToAction("Index", "Home");
        }

        // GET: Forms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //string formType = CheckFormType(id);
            Form form = db.Forms.Find(id);
            string formType = form.Type;

            switch (formType)
            {
                case ("Course Registration"):
                    return RedirectToAction("CourseRegDetails", new { id = id });
                case ("Suspend Subject"):
                    return RedirectToAction("SuspendSubjectDetails", new { id = id });
                case ("Suspend Semester"):
                    return RedirectToAction("SuspendSemesterDetails", new { id = id });
                case ("Drop Out"):
                    return RedirectToAction("DropOutDetails", new { id = id });
                case ("Request"):
                    return RedirectToAction("RequestDetails", new { id = id });
                case ("General Request"):
                    return RedirectToAction("RequestDetails", new { id = id });
                /*
            case (""):
                break;*/
                default:
                    break;
            }
            return View();
        }

        //Edit Form
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //string formType = CheckFormType(id);
            Form form = db.Forms.Find(id);
            string formType = form.Type;

            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });

            switch (formType)
            {
                case ("Course Registration"):
                    return RedirectToAction("CourseRegEdit", new { id = id });
                case ("Suspend Subject"):
                    return RedirectToAction("SuspendSubjectEdit", new { id = id });
                case ("Suspend Semester"):
                    return RedirectToAction("SuspendSemesterEdit", new { id = id });
                case ("Drop Out"):
                    return RedirectToAction("DropOutEdit", new { id = id });
                case ("Request"):
                    return RedirectToAction("RequestEdit", new { id = id });
                case ("General Request"):
                    return RedirectToAction("RequestEdit", new { id = id });
                default:
                    break;
            }
            return View();
        }

        //Edit Form
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //string formType = CheckFormType(id);
            Form form = db.Forms.Find(id);
            string formType = form.Type;
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });

            switch (formType)
            {
                case ("Course Registration"):
                    return RedirectToAction("CourseRegDelete", new { id = id });
                case ("Suspend Subject"):
                    return RedirectToAction("SuspendSubjectDelete", new { id = id });
                case ("Suspend Semester"):
                    return RedirectToAction("SuspendSemesterDelete", new { id = id });
                case ("Drop Out"):
                    return RedirectToAction("DropOutDelete", new { id = id });
                case ("Request"):
                    return RedirectToAction("RequestDelete", new { id = id });
                case ("General Request"):
                    return RedirectToAction("RequestDelete", new { id = id });
                /*
            case (""):
                break;*/
                default:
                    break;
            }
            return View();
        }

        // GET: CourseRegForms/Create
        public ActionResult CourseRegCreate()
        {
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type");
            return View();
        }

        // POST: CourseRegForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseRegCreate([Bind(Include = "StudentPhone,SubjectCode,Subject,Status,Class,SemesterName")] CourseRegForm courseRegForm, Form form)
        {
            if (ModelState.IsValid)
            {
                //Form form = new Form();
                form.Type = "Course Registration";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")); 
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.FormDetails = "Course Registration: "+courseRegForm.Subject+" ("+courseRegForm.SubjectCode+")";
                ArrayList UserDetails = DAL.Users.GetUser();
                form.StudentRollNo = Users.GetStudentRollNo(UserDetails[0].ToString(), UserDetails[1].ToString());
                form.StudentEmail = UserDetails[2].ToString();
                form.StudentName = UserDetails[0].ToString();
                form.Status = "New";
                db.Forms.Add(form);
                db.SaveChanges();
                courseRegForm.FormID = form.ID;
                db.CourseRegForms.Add(courseRegForm);
                db.SaveChanges();
                return RedirectToAction("Index", "Student");
            }
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", courseRegForm.FormID);
            return View(courseRegForm);
        }

        // GET: CourseRegForms/Details/5
        public ActionResult CourseRegDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseRegForm courseRegForm = db.CourseRegForms.Find(id);
            if (courseRegForm == null)
            {
                return HttpNotFound();
            }
            return View(courseRegForm);
        }

        // GET: CourseRegForms/Edit/5
        public ActionResult CourseRegEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseRegForm courseRegForm = db.CourseRegForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (courseRegForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", courseRegForm.FormID); 
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            return View(courseRegForm);
        }

        // POST: CourseRegForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseRegEdit([Bind(Include = "FormID,StudentPhone,SubjectCode,Subject,SemesterName")] CourseRegForm courseRegForm, Form form)
        {
            if (ModelState.IsValid)
            {
                CourseRegForm crf = db.CourseRegForms.Find(courseRegForm.FormID);
                Form f = db.Forms.Find(courseRegForm.FormID);
                f.FormDetails = "Course Registration: " + courseRegForm.Subject + " (" + courseRegForm.SubjectCode + ")";
                f.StudentPhone = form.StudentPhone;
                //crf.StudentPhone = courseRegForm.StudentPhone;
                crf.Subject = courseRegForm.Subject;
                crf.SubjectCode = courseRegForm.SubjectCode;
                f.SemesterName = form.SemesterName;
                db.Entry(crf).State = EntityState.Modified;
                db.SaveChanges();
                if (DAL.UserInfo.Role.Equals("Student"))
                    return RedirectToAction("Index", "Student");
                else if (DAL.UserInfo.Role.Contains("Staff"))
                    return RedirectToAction("Index", "Staff");
                else
                    return RedirectToAction("Index", "Home");
            }
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", courseRegForm.FormID);
            return View(courseRegForm);
        }


        // GET: CourseRegForms/Delete/5
        public ActionResult CourseRegDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseRegForm courseRegForm = db.CourseRegForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (courseRegForm == null)
            {
                return HttpNotFound();
            }
            return View(courseRegForm);
        }

        // POST: CourseRegForms/Delete/5
        [HttpPost, ActionName("CourseRegDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseRegForm courseRegForm = db.CourseRegForms.Find(id);
            db.CourseRegForms.Remove(courseRegForm);
            Form form = db.Forms.Find(id);
            db.Forms.Remove(form);
            db.SaveChanges();
            if (DAL.UserInfo.Role.Equals("Student"))
                return RedirectToAction("Index", "Student");
            else if (DAL.UserInfo.Role.Contains("Staff"))
                return RedirectToAction("Index", "Staff");
            else
                return RedirectToAction("Index", "Home");
        }

        // GET: SuspendSubjectForms/Details/5
        public ActionResult SuspendSubjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSubjectForm suspendSubjectForm = db.SuspendSubjectForms.Find(id);
            if (suspendSubjectForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSubjectForm.FormID);
            return View(suspendSubjectForm);
        }

        // GET: SuspendSubjectForms/Create
        public ActionResult SuspendSubjectCreate()
        {
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type");
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            return View();
        }

        // POST: SuspendSubjectForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendSubjectCreate([Bind(Include = "FormID,StudentPhone,SubjectName,SubjectCode,SemesterName,Reason")] SuspendSubjectForm suspendSubjectForm, Form form,
             FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                //Form form = new Form();
                form.Type = "Suspend Subject";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.FormDetails = "Suspend Subject: "+suspendSubjectForm.SubjectName+" ("+suspendSubjectForm.SubjectCode+")";

                ArrayList UserDetails = DAL.Users.GetUser();
                form.StudentRollNo = Users.GetStudentRollNo(UserDetails[0].ToString(), UserDetails[1].ToString());
                form.StudentEmail = UserDetails[2].ToString();
                form.StudentName = UserDetails[0].ToString();
                form.Status = "New";
                db.Forms.Add(form);
                db.SaveChanges();
                suspendSubjectForm.FormID = form.ID;
                if (suspendSubjectForm.Reason.Equals("Other"))
                {
                    suspendSubjectForm.Reason = "Other: " + collection["displayFor"];
                }
                db.SuspendSubjectForms.Add(suspendSubjectForm);
                db.SaveChanges();
                return RedirectToAction("Index", "Student");
            }

            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSubjectForm.FormID);
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            return View(suspendSubjectForm);
        }


        // GET: SuspendSubjectForms/Edit/5
        public ActionResult SuspendSubjectEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSubjectForm suspendSubjectForm = db.SuspendSubjectForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (suspendSubjectForm == null)
            {
                return HttpNotFound();
            }
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSubjectForm.FormID);
            return View(suspendSubjectForm);
        }

        // POST: SuspendSubjectForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendSubjectEdit([Bind(Include = "FormID,StudentPhone,SubjectName,SubjectCode,SemesterName,Reason")] SuspendSubjectForm suspendSubjectForm, Form form,
             FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                form = db.Forms.Find(suspendSubjectForm.FormID);
                form.Type = "Suspend Subject";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.FormDetails = "Suspend Subject: " + suspendSubjectForm.SubjectName + " (" + suspendSubjectForm.SubjectCode + ")";

                if (suspendSubjectForm.Reason.Equals("Other"))
                {
                    suspendSubjectForm.Reason = "Other: " + collection["displayFor"];
                }
                db.Entry(suspendSubjectForm).State = EntityState.Modified;
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                if (DAL.UserInfo.Role.Equals("Student"))
                    return RedirectToAction("Index", "Student");
                else if (DAL.UserInfo.Role.Contains("Staff"))
                    return RedirectToAction("Index", "Staff");
                else
                    return RedirectToAction("Index", "Home");
            }
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSubjectForm.FormID);
            return View(suspendSubjectForm);
        }

        // GET: SuspendSubjectForms/Delete/5
        public ActionResult SuspendSubjectDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSubjectForm suspendSubjectForm = db.SuspendSubjectForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (suspendSubjectForm == null)
            {
                return HttpNotFound();
            }
            return View(suspendSubjectForm);
        }

        // POST: SuspendSubjectForms/Delete/5
        [HttpPost, ActionName("SuspendSubjectDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendSubjectDeleteConfirmed(int id)
        {
            SuspendSubjectForm suspendSubjectForm = db.SuspendSubjectForms.Find(id);
            db.SuspendSubjectForms.Remove(suspendSubjectForm);
            Form form = db.Forms.Find(id);
            db.Forms.Remove(form);
            db.SaveChanges();
            if (DAL.UserInfo.Role.Equals("Student"))
                return RedirectToAction("Index", "Student");
            else if (DAL.UserInfo.Role.Contains("Staff"))
                return RedirectToAction("Index", "Staff");
            else
                return RedirectToAction("Index", "Home");
        }


        // GET: SuspendSemesterForms/Details/5
        public ActionResult SuspendSemesterDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSemesterForm suspendSemesterForm = db.SuspendSemesterForms.Find(id);
            Form form = db.Forms.Find(id);
            if (suspendSemesterForm == null)
            {
                return HttpNotFound();
            }
            return View(suspendSemesterForm);
        }

        // GET: SuspendSemesterForms/Create
        public ActionResult SuspendSemesterCreate()
        {
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type");
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            //var semesters = db.Semesters.Select(s => new SelectListItem
            //    {
            //        Value = s.Name,
            //        Text = s.Name
            //    });
            //ViewBag.Semesters = semesters.AsEnumerable();
            return View();
        }

        // POST: SuspendSemesterForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendSemesterCreate([Bind(Include = "StudentPhone,SemesterName,Reason")] SuspendSemesterForm suspendSemesterForm, Form form,
             FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                //Form form = new Form();
                form.Type = "Suspend Semester";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.FormDetails = "Suspend Semester: " + form.SemesterName;

                ArrayList UserDetails = DAL.Users.GetUser();
                form.StudentRollNo = Users.GetStudentRollNo(UserDetails[0].ToString(), UserDetails[1].ToString());
                form.StudentEmail = UserDetails[2].ToString();
                form.StudentName = UserDetails[0].ToString();
                form.Status = "New";
                db.Forms.Add(form);
                db.SaveChanges();
                suspendSemesterForm.FormID = form.ID;
                if (suspendSemesterForm.Reason.Equals("Other"))
                {
                    suspendSemesterForm.Reason = "Other: " + collection["displayFor"];
                }
                db.SuspendSemesterForms.Add(suspendSemesterForm);
                db.SaveChanges();
                return RedirectToAction("Index", "Student");
            }

            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSemesterForm.FormID);
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            return View(suspendSemesterForm);
        }


        // GET: SuspendSemesterForms/Edit/5
        public ActionResult SuspendSemesterEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSemesterForm suspendSemesterForm = db.SuspendSemesterForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (suspendSemesterForm == null)
            {
                return HttpNotFound();
            }
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSemesterForm.FormID);
            return View(suspendSemesterForm);
        }

        // POST: SuspendSemesterForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendSemesterEdit([Bind(Include = "FormID,StudentPhone,SemesterName,Reason")] SuspendSemesterForm suspendSemesterForm, Form form,
             FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                form = db.Forms.Find(suspendSemesterForm.FormID);
                form.Type = "Suspend Semester";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.FormDetails = "Suspend Semester: " + form.SemesterName;
                if (suspendSemesterForm.Reason.Equals("Other"))
                {
                    suspendSemesterForm.Reason = "Other: " + collection["displayFor"];
                }
                db.Entry(suspendSemesterForm).State = EntityState.Modified;
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                if (DAL.UserInfo.Role.Equals("Student"))
                    return RedirectToAction("Index", "Student");
                else if (DAL.UserInfo.Role.Contains("Staff"))
                    return RedirectToAction("Index", "Staff");
                else
                    return RedirectToAction("Index", "Home");
            }
            DateTime today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            ViewBag.SemesterName = new SelectList(db.Semesters.Where(s => s.EndDate > today), "Name", "Name");
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSemesterForm.FormID);
            return View(suspendSemesterForm);
        }

        // GET: SuspendSemesterForms/Delete/5
        public ActionResult SuspendSemesterDelete(int? id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSemesterForm suspendSemesterForm = db.SuspendSemesterForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (suspendSemesterForm == null)
            {
                return HttpNotFound();
            }
            return View(suspendSemesterForm);
        }

        // POST: SuspendSemesterForms/Delete/5
        [HttpPost, ActionName("SuspendSemesterDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendSemesterDeleteConfirmed(int id)
        {
            SuspendSemesterForm suspendSemesterForm = db.SuspendSemesterForms.Find(id);
            db.SuspendSemesterForms.Remove(suspendSemesterForm);
            Form form = db.Forms.Find(id);
            db.Forms.Remove(form);
            db.SaveChanges();
            if(DAL.UserInfo.Role.Equals("Student"))
                return RedirectToAction("Index","Student");
            else if(DAL.UserInfo.Role.Contains("Staff"))
                return RedirectToAction("Index","Staff");
            else
                return RedirectToAction("Index", "Home");
        }


        // GET: RequestForms/Details/5
        public ActionResult RequestDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestForm requestForm = db.RequestForms.Find(id);
            Form form = db.Forms.Find(id);
            if (requestForm == null)
            {
                return HttpNotFound();
            }
            return View(requestForm);
        }

        // GET: RequestForms/Create
        public ActionResult RequestCreate()
        {
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type");
            return View();
        }

        // POST: RequestForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestCreate([Bind(Include = "RequestTitle,StudentPhone,Class,Description")] RequestForm requestForm, Form form)
        {
            if (ModelState.IsValid)
            {
                //Form form = new Form();
                form.Type = "General Request";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.FormDetails = "General Request: " + requestForm.RequestTitle;
                form.SemesterName = Helper.Help.GetCurrentSemester();

                ArrayList UserDetails = DAL.Users.GetUser();
                form.StudentRollNo = Users.GetStudentRollNo(UserDetails[0].ToString(), UserDetails[1].ToString());
                form.StudentEmail = UserDetails[2].ToString();
                form.StudentName = UserDetails[0].ToString();
                form.Status = "New";
                db.Forms.Add(form);
                db.SaveChanges();
                requestForm.FormID = form.ID;
                db.RequestForms.Add(requestForm);
                db.SaveChanges();
                return RedirectToAction("Index", "Student");
            }

            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", requestForm.FormID);
            return View(requestForm);
        }


        // GET: RequestForms/Edit/5
        public ActionResult RequestEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestForm requestForm = db.RequestForms.Find(id);
            if (requestForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", requestForm.FormID);
            return View(requestForm);
        }

        // POST: RequestForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestEdit([Bind(Include = "FormID,RequestTitle,StudentPhone,StudentEmail,Class,Batch,Description")] RequestForm requestForm, Form form)
        {
            if (ModelState.IsValid)
            {
                form = db.Forms.Find(requestForm.FormID);
                form.Type = "General Request";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.FormDetails = "General Request: " + requestForm.RequestTitle;
                form.SemesterName = Helper.Help.GetCurrentSemester();
                db.Entry(requestForm).State = EntityState.Modified;
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                if (DAL.UserInfo.Role.Equals("Student"))
                    return RedirectToAction("Index", "Student");
                else if (DAL.UserInfo.Role.Contains("Staff"))
                    return RedirectToAction("Index", "Staff");
                else
                    return RedirectToAction("Index", "Home");
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", requestForm.FormID);
            return View(requestForm);
        }

        // GET: RequestForms/Delete/5
        public ActionResult RequestDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestForm requestForm = db.RequestForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (requestForm == null)
            {
                return HttpNotFound();
            }
            return View(requestForm);
        }

        // POST: RequestForms/Delete/5
        [HttpPost, ActionName("RequestDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult RequestDeleteConfirmed(int id)
        {
            RequestForm requestForm = db.RequestForms.Find(id);
            db.RequestForms.Remove(requestForm);
            Form form = db.Forms.Find(id);
            db.Forms.Remove(form);
            db.SaveChanges();
            if (DAL.UserInfo.Role.Equals("Student"))
                return RedirectToAction("Index", "Student");
            else if (DAL.UserInfo.Role.Contains("Staff"))
                return RedirectToAction("Index", "Staff");
            else
                return RedirectToAction("Index", "Home");
        }

        // GET: DropOutForms/Details/5
        public ActionResult DropOutDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropOutForm dropOutForm = db.DropOutForms.Find(id);
            Form form = db.Forms.Find(id);
            if (dropOutForm == null)
            {
                return HttpNotFound();
            }
            return View(dropOutForm);
        }

        // GET: DropOutForms/Create
        public ActionResult DropOutCreate()
        {
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type");
            return View();
        }

        // POST: DropOutForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DropOutCreate([Bind(Include = "Class,MethodPayment,DropOutDate,Reason")] DropOutForm dropOutForm, Form form,
             FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                //Form form = new Form();
                form.Type = "Drop Out";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.SemesterName = Helper.Help.GetCurrentSemester();
                if(dropOutForm.Reason.Equals("Other"))
                {
                    dropOutForm.Reason = "Other: " + collection["displayFor"];
                }
                ArrayList UserDetails = DAL.Users.GetUser();
                form.StudentRollNo = Users.GetStudentRollNo(UserDetails[0].ToString(), UserDetails[1].ToString());
                form.StudentEmail = UserDetails[2].ToString();
                form.StudentName = UserDetails[0].ToString();
                form.FormDetails = "Drop Out: "+dropOutForm.Reason;
                form.Status = "New";
                db.Forms.Add(form);
                db.SaveChanges();
                dropOutForm.FormID = form.ID;
                db.DropOutForms.Add(dropOutForm);
                db.SaveChanges();
                return RedirectToAction("Index", "Student");
            }

            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", dropOutForm.FormID);
            return View(dropOutForm);
        }


        // GET: DropOutForms/Edit/5
        public ActionResult DropOutEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropOutForm dropOutForm = db.DropOutForms.Find(id);
            if (dropOutForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", dropOutForm.FormID);
            return View(dropOutForm);
        }

        // POST: DropOutForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DropOutEdit([Bind(Include = "FormID,Class,MethodPayment,DropOutDate,Reason")] DropOutForm dropOutForm, Form form,
            FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                form = db.Forms.Find(dropOutForm.FormID);
                form.FormDetails = "Drop Out: " + dropOutForm.Reason;
                form.Type = "Drop Out";
                form.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                form.CurrentDesk = "Academic-Staff";
                form.Flow = "Academic-Staff,";
                form.SemesterName = Helper.Help.GetCurrentSemester();
                if (dropOutForm.Reason.Equals("Other"))
                {
                    dropOutForm.Reason = "Other: " + collection["displayFor"];
                }
                db.Entry(dropOutForm).State = EntityState.Modified;
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                if (DAL.UserInfo.Role.Equals("Student"))
                    return RedirectToAction("Index", "Student");
                else if (DAL.UserInfo.Role.Contains("Staff"))
                    return RedirectToAction("Index", "Staff");
                else
                    return RedirectToAction("Index", "Home");
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", dropOutForm.FormID);
            return View(dropOutForm);
        }

        // GET: DropOutForms/Delete/5
        public ActionResult DropOutDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropOutForm dropOutForm = db.DropOutForms.Find(id);
            Form form = db.Forms.Find(id);
            if (!form.Status.Equals("New"))
                return RedirectToAction("Details", new { id = id });
            if (dropOutForm == null)
            {
                return HttpNotFound();
            }
            return View(dropOutForm);
        }

        // POST: DropOutForms/Delete/5
        [HttpPost, ActionName("DropOutDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DropOutDeleteConfirmed(int id)
        {
            DropOutForm dropOutForm = db.DropOutForms.Find(id);
            db.DropOutForms.Remove(dropOutForm);
            Form form = db.Forms.Find(id);
            db.Forms.Remove(form);
            db.SaveChanges();
            if (DAL.UserInfo.Role.Equals("Student"))
                return RedirectToAction("Index", "Student");
            else if (DAL.UserInfo.Role.Contains("Staff"))
                return RedirectToAction("Index", "Staff");
            else
                return RedirectToAction("Index", "Home");
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}