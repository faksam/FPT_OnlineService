using FPT_OnlineService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace FPT_OnlineService.Controllers
{
    [Authorize(Roles = "FPT-Staff,HeadOfAcademicDepartment-Staff,AcademicHead-Staff,CampusDirector-Staff,Academic-Staff")]
    public class StaffController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        List<Form> fforms = null;
        public IQueryable<Form> GetForms()
        {
            return db.Forms.AsQueryable();
        }

        public async Task<List<Form>> GetFormsFlow()
        {
            string role = DAL.UserInfo.Role;
            return (await GetForms().Where(f => f.Flow.Contains(role)).ToListAsync());
        }
        //GET All Forms
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            string id = User.Identity.GetUserId();
            //DAL.DALUser.GetStudent(User.Identity.GetUserId(), User.Identity.GetUserName());
            //samuelse03917@fpt.edu.vn
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            string role = DAL.Users.GetUserRole();
            var forms = from f in db.Forms
                        select f;

            int count = forms.Count<Form>();
            forms = forms.Where(f => f.Flow.Contains(role));
            //var forms = (IQueryable<Form>)formsFlow;
            count = forms.Count<Form>();
            //await forms.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(f => f.StudentRollNo.Contains(searchString.Trim()));
            }

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

        public ViewResult New(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            string id = User.Identity.GetUserId();
            //DAL.DALUser.GetStudent(User.Identity.GetUserId(), User.Identity.GetUserName());
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ArrayList UserDetails = DAL.Users.GetUser();
            string role = UserDetails[3].ToString();
            var forms = from f in db.Forms
                        where f.Flow.Contains(role)
                        select f;
            forms = forms.Where(f => f.Status.ToLower().Contains("new"));
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(f => f.StudentRollNo.Contains(searchString.Trim()));
            }
            forms = forms.OrderBy(f => f.Date);
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

        public ViewResult Approved(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            string id = User.Identity.GetUserId();
            //DAL.DALUser.GetStudent(User.Identity.GetUserId(), User.Identity.GetUserName());
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ArrayList UserDetails = DAL.Users.GetUser();
            string role = UserDetails[3].ToString();
            var forms = from f in db.Forms
                        where f.Flow.Contains(role)
                        select f;
            forms = forms.Where(f => f.Status.ToLower().Contains("approved"));
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(f => f.StudentRollNo.Contains(searchString.Trim()));
            }
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

        public ViewResult Rejected(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            string id = User.Identity.GetUserId();
            //DAL.DALUser.GetStudent(User.Identity.GetUserId(), User.Identity.GetUserName());
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ArrayList UserDetails = DAL.Users.GetUser();
            string role = UserDetails[3].ToString();
            var forms = from f in db.Forms
                        where f.Flow.Contains(role)
                        select f;

            forms = forms.Where(f => f.Status.ToLower().Contains("rejected"));
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(f => f.StudentRollNo.Contains(searchString.Trim()));
            }
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

        public ViewResult InProgress(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            string id = User.Identity.GetUserId();
            //DAL.DALUser.GetStudent(User.Identity.GetUserId(), User.Identity.GetUserName());
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ArrayList UserDetails = DAL.Users.GetUser();
            string role = UserDetails[3].ToString();
            var forms = from f in db.Forms
                        where f.Flow.Contains(role)
                        select f;

            forms = forms.Where(f => f.Status.ToLower().Contains("inprogress"));
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(f => f.StudentRollNo.Contains(searchString.Trim()));
            }
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

        public ActionResult StaffEndorsement(int? id)
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
                    return RedirectToAction("CourseRegEndorsement", new { id = id });
                case ("Suspend Subject"):
                    return RedirectToAction("SuspendSubjectEndorsement", new { id = id });
                case ("Suspend Semester"):
                    return RedirectToAction("SuspendSemesterEndorsement", new { id = id });
                case ("Drop Out"):
                    return RedirectToAction("DropOutEndorsement", new { id = id });
                case ("Request"):
                    return RedirectToAction("RequestEndorsement", new { id = id });
                case ("General Request"):
                    return RedirectToAction("RequestEndorsement", new { id = id });
                /*
            case (""):
                break;*/
                default:
                    break;
            }
            return View();
        }

        public ActionResult SuspendSemesterEndorsement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSemesterForm suspendSemesterForm = db.SuspendSemesterForms.Find(id);

            AllFormModel allFormModels = new AllFormModel()
            {
                Form = db.Forms.Find(id),
                SuspendSubjectForm = db.SuspendSubjectForms.Find(id)
            };

            if (suspendSemesterForm == null)
            {
                return HttpNotFound();
            }

            ViewBag.Comment = new List<FormComment>(db.FormComments.Where(fc=>fc.FormID==id));
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type,PreviousSemester,TuitionFee,TwoPrevSemester,IsWeekBefore", suspendSemesterForm.FormID);
            return View(suspendSemesterForm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SuspendSemesterEndorsement([Bind(Include = "FormID,PreviousSemester,TwoPrevSemester")] 
            SuspendSemesterForm suspendSemesterForm, FormCollection collection)
        {
            string ForwardTo = collection["ForwardTo"]+"";
            string IsWeekBefore = collection["IsWeekBefore"] + "";
            string FormStatus = collection["FormStatus"] + "";
            string Life = collection["Life"] + "";
            string TuitionFee = collection["TuitionFee"] + "";
            string formComment = collection["formComment"] + "";
            SuspendSemesterForm ssf = db.SuspendSemesterForms.Find(suspendSemesterForm.FormID);
            Form f = db.Forms.Find(suspendSemesterForm.FormID);
            
            FormComment fC = new FormComment();
            if (!formComment.Equals(""))
            {
                fC.FormID = suspendSemesterForm.FormID;
                fC.Comment = formComment;
                fC.RoleID = Helper.Help.GetRoleId(DAL.UserInfo.Role);
                db.FormComments.Add(fC);
            }
            if (FormStatus.Equals(""))
                f.Status = "Inprogress";
            else
                f.Status = FormStatus;
            string getCurrentDesk = "";
            if (ForwardTo != "")
                getCurrentDesk = Helper.Help.GetCurrentDesk(Int32.Parse(ForwardTo));
            if(getCurrentDesk.Equals("Student"))
            {
                //send mail to notify student
                string notifyMessage = "<p>Please provide more information like: </p>"
                                        +"<p>====================================</p>"+ fC.Comment;
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
                //Set status To new
                f.Status = "New";

                f.CurrentDesk = "Student";
                f.Flow = f.Flow + "Student,";
            }
            else if (getCurrentDesk.Length > 4)
            {
                f.CurrentDesk = getCurrentDesk;
                f.Flow = f.Flow + f.CurrentDesk + ",";
            }


            if (IsWeekBefore != null)
            {
                if (IsWeekBefore.Equals("on"))
                    ssf.IsWeekBefore = true;
                else
                    ssf.IsWeekBefore = false;
            }

            if (TuitionFee != null)
            {
                if (TuitionFee.Equals("on"))
                    ssf.TuitionFee = true;
                else
                    ssf.TuitionFee = false;
            }
            ssf.PreviousSemester = suspendSemesterForm.PreviousSemester;
            ssf.TwoPrevSemester = suspendSemesterForm.TwoPrevSemester;
            db.Entry(ssf).State = EntityState.Modified;
            db.SaveChanges();
            if (f.Status.Equals("Approved") || f.Status.Equals("Rejected"))
            {
                f.ApprovalDate = DateTime.Now.ToString();
                f.ApprovalBy = DAL.UserInfo.Role;
                Helper.Notify notify = new Helper.Notify();

                string notifyMessage = "<p>Your form:        " + f.Type + "</p> "
                                      + "<p>Student Name:    " + f.StudentName + "</p>"
                                      + "<p>Student RollNo:  " + f.StudentRollNo + "</p>"
                                      + "<p>Student Email:   " + f.StudentEmail + "</p>"
                                      + "<p>Student Phone:   " + f.StudentPhone + "</p>"
                                      + "<p>Semester:        "+f.SemesterName+"</p>"
                                      + "<p>Reason:          " + ssf.Reason + "</p>"
                                      +" has been <b>" + f.Status+"</b>";
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
            }
            return RedirectToAction("Index", "Staff");
        }

        
        public ActionResult SuspendSubjectEndorsement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendSubjectForm suspendSubjectForm = db.SuspendSubjectForms.Find(id);

            AllFormModel allFormModels = new AllFormModel()
            {
                Form = db.Forms.Find(id),
                SuspendSubjectForm = db.SuspendSubjectForms.Find(id)
            };


            if (suspendSubjectForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.Comment = new List<FormComment>(db.FormComments.Where(fc => fc.FormID == id));
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", suspendSubjectForm.FormID);
            return View(suspendSubjectForm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SuspendSubjectEndorsement([Bind(Include = "FormID,NotAllSubject,IsWeekBefore")] 
            SuspendSubjectForm suspendSubjectForm, FormCollection collection)
        {
            string ForwardTo = collection["ForwardTo"]+"";            
            string FormStatus = collection["FormStatus"]+"";
            string IsWeekBefore = collection["IsWeekBefore"]+"";
            string NotAllSubject = collection["NotAllSubject"]+"";
            string formComment = collection["formComment"]+"";
            SuspendSubjectForm ssf = db.SuspendSubjectForms.Find(suspendSubjectForm.FormID);
            Form f = db.Forms.Find(suspendSubjectForm.FormID);
                FormComment fC = new FormComment();
            if (!formComment.Equals(""))
            {
                fC.FormID = suspendSubjectForm.FormID;
                fC.Comment = formComment;
                fC.RoleID = Helper.Help.GetRoleId(DAL.UserInfo.Role);
                db.FormComments.Add(fC);
            }
            if (FormStatus.Equals(""))
                f.Status = "Inprogress";
            else
                f.Status = FormStatus;
            string getCurrentDesk = "";
            if (ForwardTo != "")
                getCurrentDesk = Helper.Help.GetCurrentDesk(Int32.Parse(ForwardTo));
            if (getCurrentDesk.Equals("Student"))
            {
                //send mail to notify student
                string notifyMessage = "<p>Please provide more information like: </p>"
                                        + "<p>====================================</p>" + fC.Comment;
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
                //Set status To new
                f.Status = "New";
                f.CurrentDesk = "Student";
                f.Flow = f.Flow + "Student,";
            }
            else if (getCurrentDesk.Length > 4)
            {
                f.CurrentDesk = getCurrentDesk;
                f.Flow = f.Flow + f.CurrentDesk + ",";
            }

            if (IsWeekBefore != null)
            {
                if (IsWeekBefore.Equals("on"))
                    ssf.IsWeekBefore = true;
                else
                    ssf.IsWeekBefore = false;
            }

            if (NotAllSubject != null)
            {
                if (NotAllSubject.Equals("on"))
                    ssf.NotAllSubject = true;
                else
                    ssf.NotAllSubject = false;
            }
            db.Entry(ssf).State = EntityState.Modified;
            db.SaveChanges();
            if(f.Status.Equals("Approved") || f.Status.Equals("Rejected"))
            {
                f.ApprovalDate = DateTime.Now.ToString();
                f.ApprovalBy = DAL.UserInfo.Role;
                Helper.Notify notify = new Helper.Notify();

                string notifyMessage = "<p>Your form:       " + f.Type + "</p> "
                                      + "<p>Student Name:   " + f.StudentName + "</p>"
                                      + "<p>Student RollNo: " + f.StudentRollNo + "</p>"
                                      + "<p>Student Email:  " + f.StudentEmail + "</p>"
                                      + "<p>Student Phone:  " + f.StudentPhone + "</p>"
                                      + "<p>Subject Name:   " + ssf.SubjectName+" "+ ssf.SubjectCode + "</p>"
                                      + "<p>Reason:         "   + ssf.Reason + "</p>"
                                       + " has been <b>" + f.Status + "</b>";
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
            }
            return RedirectToAction("Index", "Staff");
        }

        public ActionResult CourseRegEndorsement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseRegForm courseRegForm = db.CourseRegForms.Find(id);

            AllFormModel allFormModels = new AllFormModel()
            {
                Form = db.Forms.Find(id),
                SuspendSubjectForm = db.SuspendSubjectForms.Find(id)
            };

            if (courseRegForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.Comment = new List<FormComment>(db.FormComments.Where(fc => fc.FormID == id));
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", courseRegForm.FormID);
            return View(courseRegForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CourseRegEndorsement([Bind(Include = "FormID,TuitionFee")] 
            CourseRegForm courseRegForm, FormCollection collection)
        {
            string ForwardTo = collection["ForwardTo"]+"";
            string FormStatus = collection["FormStatus"]+"";
            string IsWeekBefore = collection["IsWeekBefore"]+"";
            string NotAllSubject = collection["NotAllSubject"]+"";
            string formComment = collection["formComment"]+"";
            string TuitionFee = collection["TuitionFee"]+"";
            string CoursesAvailable = collection["CoursesAvailable"]+"";

            CourseRegForm crf = db.CourseRegForms.Find(courseRegForm.FormID);
            Form f = db.Forms.Find(courseRegForm.FormID);
                FormComment fC = new FormComment();
            if (!formComment.Equals(""))
            {
                fC.FormID = courseRegForm.FormID;
                fC.Comment = formComment;
                fC.RoleID = Helper.Help.GetRoleId(DAL.UserInfo.Role);
                db.FormComments.Add(fC);
            }
            if (FormStatus.Equals(""))
                f.Status = "Inprogress";
            else
                f.Status = FormStatus;
            string getCurrentDesk = "";
            if(ForwardTo!= "")
                getCurrentDesk = Helper.Help.GetCurrentDesk(Int32.Parse(ForwardTo));
            if (getCurrentDesk.Equals("Student"))
            {
                //send mail to notify student
                string notifyMessage = "<p>Please provide more information like: </p>"
                                        + "<p>====================================</p>" + fC.Comment;
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
                //Set status To new
                f.Status = "New";
                f.CurrentDesk = "Student";
                f.Flow = f.Flow + "Student,";
            }
            else if (getCurrentDesk.Length > 4)
            {
                f.CurrentDesk = getCurrentDesk;
                f.Flow = f.Flow + f.CurrentDesk + ",";
            }
            if(IsWeekBefore!=null)
            {
                if (IsWeekBefore.Equals("on"))
                    crf.IsWeekBefore = true;
                else
                    crf.IsWeekBefore = false;
            }
            
            if(TuitionFee!=null)
            {
                if (TuitionFee.Equals("on"))
                    crf.TuitionFee = true;
                else
                    crf.TuitionFee = false;
            }
            crf.CoursesAvailable = CoursesAvailable;
            db.Entry(crf).State = EntityState.Modified;
            db.SaveChanges();
            if (f.Status.Equals("Approved") || f.Status.Equals("Rejected"))
            {
                f.ApprovalDate = DateTime.Now.ToString();
                f.ApprovalBy = DAL.UserInfo.Role;
                Helper.Notify notify = new Helper.Notify();

                string notifyMessage = "<p>Your form: " + f.Type + "</p> "
                                      + "<p>Student Name:     " + f.StudentName + "</p>"
                                      + "<p>Student RollNo:   " + f.StudentRollNo + "</p>"
                                      + "<p>Student Email:    " + f.StudentEmail + "</p>"
                                      + "<p>Student Phone:    " + f.StudentPhone + "</p>"
                                      + "<p>Semester:         " + f.SemesterName + "</p>"
                                      + "<p>Subject:   " + crf.Subject + " "+crf.SubjectCode +" </p>"
                                      + " has been <b>" + f.Status + "</b>";
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
            }
            return RedirectToAction("Index", "Staff");
        }

        public ActionResult DropOutEndorsement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropOutForm dropOutForm = db.DropOutForms.Find(id);

            AllFormModel allFormModels = new AllFormModel()
            {
                Form = db.Forms.Find(id),
                SuspendSubjectForm = db.SuspendSubjectForms.Find(id)
            };

            if (dropOutForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.Comment = new List<FormComment>(db.FormComments.Where(fc => fc.FormID == id));
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", dropOutForm.FormID);
            return View(dropOutForm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DropOutEndorsement([Bind(Include = "FormID")] 
            DropOutForm dropOutForm, FormCollection collection)
        {
            string ForwardTo = collection["ForwardTo"]+"";
            string FormStatus = collection["FormStatus"]+"";
            string IsWeekBefore = collection["IsWeekBefore"]+"";
            string AcademicHeadEndorse = collection["AcademicHeadEndorse"]+"";
            string formComment = collection["formComment"]+"";
            string TuitionFee = collection["TuitionFee"]+"";
            string AccountStatus = collection["AccountStatus"]+"";
            string LibraryStatus = collection["LibraryStatus"]+"";
            DropOutForm dof = db.DropOutForms.Find(dropOutForm.FormID);
            Form f = db.Forms.Find(dropOutForm.FormID);
                FormComment fC = new FormComment();
            if (!formComment.Equals(""))
            {
                fC.FormID = dropOutForm.FormID;
                fC.Comment = formComment;
                fC.RoleID = Helper.Help.GetRoleId(DAL.UserInfo.Role);
                db.FormComments.Add(fC);
            }
            if (FormStatus.Equals(""))
                f.Status = "Inprogress";
            else
                f.Status = FormStatus;
            string getCurrentDesk = "";
            if (ForwardTo != "")
                getCurrentDesk = Helper.Help.GetCurrentDesk(Int32.Parse(ForwardTo));
            if (getCurrentDesk.Equals("Student"))
            {
                //send mail to notify student
                string notifyMessage = "<p>Please provide more information like: </p>"
                                        + "<p>====================================</p>" + fC.Comment;
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
                //Set status To new
                f.Status = "New";
                f.CurrentDesk = "Student";
                f.Flow = f.Flow + "Student,";
            }
            else if (getCurrentDesk.Length > 4)
            {
                f.CurrentDesk = getCurrentDesk;
                f.Flow = f.Flow + f.CurrentDesk + ",";
            }
            
            dof.AcademicHeadEndorse = AcademicHeadEndorse;
            dof.AccountStatus = AccountStatus;
            dof.LibraryStatus = LibraryStatus;

            if (f.Status.Equals("Approved"))
                f.ApprovalDate = DateTime.Now.ToString();

            db.Entry(dof).State = EntityState.Modified;
            db.SaveChanges();
            if (f.Status.Equals("Approved") || f.Status.Equals("Rejected"))
            {
                f.ApprovalDate = DateTime.Now.ToString();
                f.ApprovalBy = DAL.UserInfo.Role;
                Helper.Notify notify = new Helper.Notify();

                string notifyMessage = "<p>Your form:       " + f.Type + "</p> "
                                      + "<p>Student Name:   " + f.StudentName + "</p>"
                                      + "<p>Student RollNo: " + f.StudentRollNo + "</p>"
                                      + "<p>Student Email:  " + f.StudentEmail + "</p>"
                                      + "<p>Student Phone:  " + f.StudentPhone + "</p>"
                                      + "<p>Semester:       " + f.SemesterName + "</p>"
                                      + "<p>Class:          " + dof.Class + "</p>"
                                      + "<p>Drop out date:  " + dof.DropOutDate + "</p>"
                                      + "<p>Payment Method: " + dof.MethodPayment + "</p>"
                                      + "<p>Drop out Reason:" + dof.Reason + "</p>"
                                       + " has been <b>" + f.Status + "</b>";
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
            }
            return RedirectToAction("Index", "Staff");
        }

        public ActionResult Drafts()
        {
            return View();
        }
        
        public ActionResult Trash()
        {
            return View();
        }

        public ActionResult RequestEndorsement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestForm requestForm = db.RequestForms.Find(id);

            AllFormModel allFormModels = new AllFormModel()
            {
                Form = db.Forms.Find(id),
                SuspendSubjectForm = db.SuspendSubjectForms.Find(id)
            };

            if (requestForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.Comment = new List<FormComment>(db.FormComments.Where(fc => fc.FormID == id));
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", requestForm.FormID);
            return View(requestForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestEndorsement([Bind(Include = "FormID")] 
            RequestForm requestForm, FormCollection collection)
        {
            string ForwardTo = collection["ForwardTo"]+"";
            string FormStatus = collection["FormStatus"]+"";
            string ReceivedBy = collection["ReceivedBy"]+"";
            string formComment = collection["formComment"]+"";
            string ToDepartment = collection["ToDepartment"]+"";
            string ProcessedBy = collection["ProcessedBy"]+"";
            string ApprovalReason = collection["ApprovalReason"]+"";
            RequestForm rf = db.RequestForms.Find(requestForm.FormID);
            Form f = db.Forms.Find(requestForm.FormID);
                FormComment fC = new FormComment();
            if (!formComment.Equals(""))
            {
                fC.FormID = requestForm.FormID;
                fC.Comment = formComment;
                fC.RoleID = Helper.Help.GetRoleId(DAL.UserInfo.Role);
                db.FormComments.Add(fC);
            }
            rf.ReceivedBy = ReceivedBy;
            rf.ProcessedBy = ProcessedBy;
            rf.ToDepartment = ToDepartment;
            if (FormStatus.Equals(""))
                f.Status = "Inprogress";
            else
                f.Status = FormStatus;
            string getCurrentDesk = "";
            if (ForwardTo != "")
                getCurrentDesk = Helper.Help.GetCurrentDesk(Int32.Parse(ForwardTo));
            if (getCurrentDesk.Equals("Student"))
            {
                //send mail to notify student
                string notifyMessage = "<p>Please provide more information like: </p>"
                                        + "<p>====================================</p>" + fC.Comment;
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
                //Set status To new
                f.Status = "New";
                f.CurrentDesk = "Student";
                f.Flow = f.Flow + "Student,";
            }
            else if (getCurrentDesk.Length > 4)
            {
                f.CurrentDesk = getCurrentDesk;
                f.Flow = f.Flow + f.CurrentDesk + ",";
            }
            
            rf.ApprovalReason = ApprovalReason;

            db.Entry(rf).State = EntityState.Modified;
            db.SaveChanges();
            if (f.Status.Equals("Approved") || f.Status.Equals("Rejected"))
            {
                f.ApprovalDate = DateTime.Now.ToString();
                f.ApprovalBy = DAL.UserInfo.Role;
                Helper.Notify notify = new Helper.Notify();

                string notifyMessage = "<p>Your form:            " + f.Type + "</p> "
                                      + "<p>Request Title:       " + rf.RequestTitle + "</p>"
                                      + "<p>Semester:            " + f.SemesterName + "</p>"
                                      + "<p>Student Name:        " + f.StudentName + "</p>"
                                      + "<p>Student RollNo:      " + f.StudentRollNo + "</p>"
                                      + "<p>Student Email:       " + f.StudentEmail + "</p>"
                                      + "<p>Student Phone:       " + f.StudentPhone + "</p>"
                                      + "<p>Class:               " + rf.Class + "</p>"
                                      + "<p>Request Description: " + rf.Description + "</p>"
                                       + " for has been <b>" + f.Status + "</b>";
                await SendNotification(notifyMessage, DAL.UserInfo.Email, f.StudentEmail);
            }
            return RedirectToAction("Index", "Staff");
        }


        // GET: Semesters
        public ActionResult Semester()
        {
            return View(db.Semesters.ToList());
        }

        // GET: Semesters/Details/5
        public ActionResult SemesterDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        // GET: Semesters/Create
        public ActionResult CreateSemester()
        {
            return View();
        }

        // POST: Semesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSemester([Bind(Include = "StartDate,EndDate,Year,NoOfWeeks,NoOfMonths,Season")] Semester semester)
        {
            if (ModelState.IsValid)
            {
                semester.Name =semester.Season + " " + semester.Year;
                db.Semesters.Add(semester);
                db.SaveChanges();
                return RedirectToAction("Semester");
            }

            return View(semester);
        }

        // GET: Semesters/Edit/5
        public ActionResult EditSemester(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            var forms = from f in db.Forms
                        select f;
            forms = forms.Where(f => f.SemesterName.Equals(semester.Name));
            if(forms.Count() > 0)
            {
                ViewBag.ContainsForms = "true";
            }
            
            return View(semester);
        }

        // POST: Semesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSemester([Bind(Include = "StartDate,EndDate,Year,NoOfWeeks,NoOfMonths,Season")] Semester semester)
        {
            if (ModelState.IsValid)
            {
                semester.Name = semester.Season + " " + semester.Year;
                db.Entry(semester).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Semester");
            }
            return View(semester);
        }

        // GET: Semesters/Delete/5
        public ActionResult DeleteSemester(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            var forms = from f in db.Forms
                        select f;
            forms = forms.Where(f => f.SemesterName.Equals(semester.Name));
            if (forms.Count() > 0)
            {
                ViewBag.ContainsForms = "true";
            }
            return View(semester);
        }

        // POST: Semesters/Delete/5
        [HttpPost, ActionName("DeleteSemester")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Semester semester = db.Semesters.Find(id);
            db.Semesters.Remove(semester);
            db.SaveChanges();
            return RedirectToAction("Semester");
        }

        public async Task<ActionResult> SendNotification(string NotificationMessage, string NotifyFrom, string NotifyTo)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            string ToEmail = NotifyTo.Trim();
            message.To.Add(new MailAddress(ToEmail));  // replace with valid value 
            //message.Bcc.Add(new MailAddress(NotifyFrom + "@fpt.edu.vn"));
            message.From = new MailAddress("fuonlineservice2016@gmail.com");  // replace with valid value
            message.Subject = "FU - Notification";
            message.Body = string.Format(body, "FU - Online Service", "fuonlineservice2016@gmail.com", NotificationMessage);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "fuonlineservice2016@gmail.com",  // replace with valid value
                    Password = "ATS3...."  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            return RedirectToAction("Index", "Staff");
        }

        public ActionResult FormsReport(string id)
        {
            var reportModel = new FormsReport();
            reportModel.Semesters = db.Semesters;

            if (id != null)
            {
                ViewBag.SemesterName = id;
                reportModel.Forms = db.Forms.Where(
                    f => f.SemesterName == id);
                reportModel.CourseRegForms = db.CourseRegForms.Where(
                    f => f.Form.SemesterName == id);
                reportModel.SuspendSemesterForms = db.SuspendSemesterForms.Where(
                    f => f.Form.SemesterName == id);
                reportModel.SuspendSubjectForms = db.SuspendSubjectForms.Where(
                    f => f.Form.SemesterName == id);
                reportModel.RequestForms = db.RequestForms.Where(
                    f => f.Form.SemesterName == id);
                reportModel.DropOutForms = db.DropOutForms.Where(
                    f => f.Form.SemesterName == id);
            }

            //ViewBag.Form = new SelectList(db.Forms, "ID", "Type");
            return View(reportModel);
        }
    }
}