using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Collections;
using FPT_OnlineService.Models;

namespace FPT_OnlineService.DAL
{
    public class Users : ApplicationUser
    {
        private static string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        
        public static string UserId
        {
            get { return HttpContext.Current.User.Identity.GetUserId(); }
        }

        public static void GetStudent()
        {
            //get User from DB
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "Select Name,Username,Email from Users where Id ='" + UserId + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                UserInfo.ID = UserId;
                UserInfo.Name = dr.GetString(0).Trim();
                UserInfo.Username = dr.GetString(1).Trim();
                UserInfo.Email = dr.GetString(2).Trim();
                UserInfo.Role = "Student";
            }
            conn.Close();
        }

        public static string GetStudent(string StudentRollNo)
        {
            //get User from DB
            string Email = "";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "Select Email from Students where StudentRollNo ='" + StudentRollNo + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Email = dr.GetString(0).Trim();
            }
            conn.Close();
            return Email;
        }

        public static void GetStaff()
        {
            //get User from DB
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "SELECT U.Name,U.Username,U.Email,R.Name FROM users u INNER JOIN "
            +"UserRoles ur ON ur.userid = u.id LEFT OUTER JOIN Roles r ON r.id = ur.roleid WHERE u.id = '" + UserId + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                UserInfo.ID = UserId;
                UserInfo.Name = dr.GetString(0).Trim();
                UserInfo.Username = dr.GetString(1).Trim();
                UserInfo.Email = dr.GetString(2).Trim();
                UserInfo.Role = dr.GetString(3).Trim();
            }
            conn.Close();
        }


        public static ArrayList GetUser()
        {
            //get User from DB
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "SELECT U.Name,U.Username,U.Email,R.Name FROM users u INNER JOIN "
            + "UserRoles ur ON ur.userid = u.id LEFT OUTER JOIN Roles r ON r.id = ur.roleid WHERE u.id = '" + UserId + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            ArrayList UserDetails = new ArrayList();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                UserDetails.Add(dr.GetString(0).Trim());
                UserDetails.Add(dr.GetString(1).Trim());
                UserDetails.Add(dr.GetString(2).Trim());
                UserDetails.Add(dr.GetString(3).Trim());
            }
            conn.Close();
            return UserDetails;
        }

        public static string GetStudentRollNo(string Name,string Username)
        {
            string RollNo="";
            //string[] studentStudentRollNo = Name.Split(' ');
            //foreach (string StudentRollNo in studentStudentRollNo)
            //{
            //    int i = StudentRollNo.Length;
            //    string s = Username.Substring(0, i).ToLower();
            //    if (StudentRollNo.ToLower().Equals(s))
            //    {
            //        RollNo = Username.Substring(Username.Length - i - 1);
            //    }
            //}
            RollNo = Username.Substring(Username.Length - 7);
            return RollNo;
        }


        public static string GetUserRole()
        {
            //get User from DB
            string userRole = "";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "select roles.Name from roles, userroles "
                            +" where roles.Id = userroles.RoleId "
                            +" and userroles.UserId = '" + UserId + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                userRole = dr.GetString(0).Trim();
            }
            conn.Close();
            return userRole;
        }


        public static string GetUserRole2()
        {
            //get User from DB
            string userRole = "";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "select roles.Name from roles, userroles "
                            + " where roles.Id = userroles.RoleId "
                            + " and userroles.UserId = '" + UserId + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                userRole = dr.GetString(0).Trim();
            }
            conn.Close();

            switch (userRole)
            {
                case "Student":
                    return "Student";
                case "Admin":
                    return "Admin";
                case "HeadOfAcademicDepartment-Staff":
                    return "Head Of Academic Department";
                case "AcademicHead-Staff":
                    return "Academic Head";
                case "CampusDirector-Staff":
                    return "Campus Director";
                case "Academic-Staff":
                    return "Academic Staff";
                default:
                    return "Unknown";
            }

            return userRole;
        }

        public static void SetUserNull()
        {
            UserInfo.ID = null;
            UserInfo.Name = null;
            UserInfo.Username = null;
            UserInfo.Email = null;
            UserInfo.Role = null;
        }
    }
}