using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPT_OnlineService.Models;

namespace FPT_OnlineService.Controllers
{
    public class NotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /*
        // GET: Notifications
        public ActionResult Index()
        {
            var notifications = db.Notifications.Include(n => n.Form).Include(n => n.Staff).Include(n => n.Student);
            return View(notifications.ToList());
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type");
            ViewBag.StaffUsername = new SelectList(db.Staffs, "UserName", "UserId");
            ViewBag.StudentStudentRollNo = new SelectList(db.Students, "StudentRollNo", "UserId");
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StudentStudentRollNo,FormID,StaffUsername,Message")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", notification.FormID);
            ViewBag.StaffUsername = new SelectList(db.Staffs, "UserName", "UserId", notification.StaffUsername);
            ViewBag.StudentStudentRollNo = new SelectList(db.Students, "StudentRollNo", "UserId", notification.StudentStudentRollNo);
            return View(notification);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", notification.FormID);
            ViewBag.StaffUsername = new SelectList(db.Staffs, "UserName", "UserId", notification.StaffUsername);
            ViewBag.StudentStudentRollNo = new SelectList(db.Students, "StudentRollNo", "UserId", notification.StudentStudentRollNo);
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentStudentRollNo,FormID,StaffUsername,Message")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormID = new SelectList(db.Forms, "ID", "Type", notification.FormID);
            ViewBag.StaffUsername = new SelectList(db.Staffs, "UserName", "UserId", notification.StaffUsername);
            ViewBag.StudentStudentRollNo = new SelectList(db.Students, "StudentRollNo", "UserId", notification.StudentStudentRollNo);
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = db.Notifications.Find(id);
            db.Notifications.Remove(notification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
