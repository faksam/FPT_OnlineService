using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using FPT_OnlineService.Models;

namespace FPT_OnlineService.DAL
{
    public class Forms
    {
        //private static string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        //static List<Notification> ListOfNotifications = GetNotifications();
        //public static List<Notification> GetNotifications()
        //{
        //    //get User from DB
        //    SqlConnection conn = new SqlConnection(connString);
        //    conn.Open();
        //    string sql = "Select * from Notifications where StudentRollNo like '%" + UserInfo.Username + "%' or  "
        //                    + "StaffUsername like  '%" + UserInfo.Username + "%' ";
        //    SqlCommand cmd = new SqlCommand(sql, conn);
        //    SqlDataReader dr = cmd.ExecuteReader();

        //    List<Notification> listOfNotifications = new List<Notification>();
        //    if(dr.HasRows)
        //    {
        //        Notification Notification = new Notification();
        //        while(dr.Read())
        //        {
        //            Notification.ID = dr.GetInt32(0);
        //            Notification.StudentRollNo = dr.GetString(1).Trim();
        //            Notification.FormID = dr.GetInt32(2);
        //            Notification.StaffUsername = dr.GetString(3).Trim();
        //            Notification.Message = dr.GetString(4).Trim();
        //            listOfNotifications.Add(Notification);
        //        }
        //    }
        //    conn.Close();
        //    return listOfNotifications;
        //}
    }
}