using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPT_OnlineService.Helper;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using FPT_OnlineService.Models;

namespace FPT_OnlineService.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

         //GET: Staffs n Role
        public ActionResult Index()
        {
            List<StaffRole> listStaffRole = new List<StaffRole>();
            StaffRole sr = null;
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //get User from DB
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = " SELECT U.Name,U.Username,U.Email,R.id FROM users u  "
                           + " INNER JOIN UserRoles ur ON ur.userid = u.id LEFT OUTER JOIN Roles r ON r.id = ur.roleid where ur.roleid = 4 "
                           + " or ur.roleid = 5 or ur.roleid = 6  or ur.roleid = 7";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                sr = new StaffRole();
                sr.StaffName = dr.GetString(0).Trim();
                sr.StaffEmail = dr.GetString(2).Trim();
                sr.RoleName = Help.GetStaffRole(Int16.Parse(dr.GetString(3)));
                listStaffRole.Add(sr);
            }
            conn.Close();

            ViewBag.ListOfStaffRole = listStaffRole;

            return View();
        }
        public ActionResult StaffToRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StaffToRole(FormCollection collection)
        {
            string sqlUpdate = "";
            bool ready = false;
            string campusDirector = collection["campusDirector"];
            string acadHead = collection["acadHead"];
            string headAcadDept = collection["headAcadDept"];
            string acadStaff = collection["acadStaff"];
            if (!campusDirector.Equals(""))
            {
                sqlUpdate += " update r set r.roleId = 6 from userroles r inner join users u on r.userId = u.Id where u.email = '" + campusDirector + "' ";
                ready = true;
            }
            if (!acadHead.Equals(""))
            {
                sqlUpdate += " update r set r.roleId = 5 from userroles r inner join users u on r.userId = u.Id where u.email = '" + acadHead + "' ";
                ready = true;
            }
            if (!headAcadDept.Equals(""))
            {
                sqlUpdate += " update r set r.roleId = 4 from userroles r inner join users u on r.userId = u.Id where u.email = '" + headAcadDept + "' ";
                ready = true;
            }
            if (!acadStaff.Equals(""))
            {
                sqlUpdate += " update r set r.roleId = 7 from userroles r inner join users u on r.userId = u.Id where u.email = '" + acadStaff + "' ";
                ready = true;
            }

            if(ready)
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                //get User from DB
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            

            return Index();
        }


    }
}
