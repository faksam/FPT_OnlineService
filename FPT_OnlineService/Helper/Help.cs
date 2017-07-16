using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FPT_OnlineService.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace FPT_OnlineService.Helper
{
    public class Help
    {
        public static string GetDate(DateTime item)
        {
            DateTime today = DateTime.Now;
            string date = "";
            if (item.Date.Year.Equals(today.Date.Year))
            {
                if (item.Date.Month.Equals(today.Month))
                {
                    if (item.Date.Day.Equals(today.Day))
                    {
                        date = item.Date.ToString("hh:mm tt");
                    }
                    else
                    {
                        date = item.Date.ToString("M");
                    }
                }
                else
                {
                    date = item.Date.ToString("M");
                }
            }
            else
            {
                date = item.Date.ToString("M") + " " + item.Date.Year;
            }
            return date;
        }

        public static string GetCurrentDesk(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Student";
                case 4:
                    return "HeadOfAcademicDepartment-Staff";
                case 5:
                    return "AcademicHead-Staff";
                case 6:
                    return "CampusDirector-Staff";
                case 7:
                    return "Academic-Staff";
                default:
                    return "Academic-Staff";
            }
        }


        public static string GetRoleId(string CurrentDesk)
        {
            switch (CurrentDesk)
            {
                case "Student":
                    return "1";
                case "HeadOfAcademicDepartment-Staff":
                    return "4";
                case "AcademicHead-Staff":
                    return "5";
                case "CampusDirector-Staff":
                    return "6";
                case "Academic-Staff":
                    return "7";
                default:
                    return "7";
            }
        }

        public static bool CheckIsWeekBefore(DateTime SubmissionDate, DateTime SemesterStart)
        {
            bool isWeekBefore = false;
            DateTime today = DateTime.Now;
            double diff = SemesterStart.Subtract(SubmissionDate).TotalDays;
            if (diff > 7)
                isWeekBefore = true;

            return isWeekBefore;
        }

        public static string GetStaffRole(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Student";
                case 3:
                    return "Admin";
                case 4:
                    return "Head Of Academic Department";
                case 5:
                    return "Academic Head";
                case 6:
                    return "Campus Director";
                case 7:
                    return "Academic Staff";
                default:
                    return "Unknown";
            }
        }

        public static int GetFlowCount(string flow, string role)
        {
            string[] splitdata = flow.Split(',');

            var results = from p in splitdata
                          where p.Contains(role)
                          select p;

            return results.Count();
        }

        
        public static string GetCurrentSemester()
        {
            
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //get User from DB
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "Select Name from Semesters";
            SqlCommand cmd = new SqlCommand(sql, conn);
            string name = "";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                name = dr.GetString(0).Trim();
            }
            conn.Close();
            return name;
        }
    }
}