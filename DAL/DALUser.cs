using Entity;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUser
    {
        private static string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static void AddStudent(string id)
        {
            SqlConnection conn = new SqlConnection(connString);

            String query = "insert into Students (UserId,RollNo,Name,Major,Email) " +
                                " values(@UserId,@RollNo,@Name,@Major,@Email)";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@UserId", id);
            cmd.Parameters.AddWithValue("@RollNo", StudentInfo.RollNo);
            cmd.Parameters.AddWithValue("@Name", StudentInfo.Name);
            cmd.Parameters.AddWithValue("@Major", StudentInfo.Major);
            cmd.Parameters.AddWithValue("@Email", StudentInfo.Email);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void AddStaff(string id)
        {
            SqlConnection conn = new SqlConnection(connString);

            String query = "insert into Staffs (UserId,Username,Name,StaffRole,Email) " +
                                " values(@UserId,@Username,@Name,@StaffRole,@Email)";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@UserId", id);
            cmd.Parameters.AddWithValue("@Username", StaffInfo.Username);
            cmd.Parameters.AddWithValue("@Name", StaffInfo.Name);
            cmd.Parameters.AddWithValue("@StaffRole", StaffInfo.StaffRole);
            cmd.Parameters.AddWithValue("@Email", StaffInfo.Email);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void GetStudent(string id, string email)
        {
           //get User from DB
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string sql = "Select RollNo,Name,Major from Students where UserId ='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                StudentInfo.RollNo = dr.GetString(0).Trim();
                StudentInfo.Name = dr.GetString(1).Trim();
                StudentInfo.Major = dr.GetString(2).Trim();
                StudentInfo.Email = email;
            }
            conn.Close();
        }

        public static void GetStudentForms(string rollNo)
        {
            //get User from DB
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            string sql = "SELECT type,date,currentdesk,flow,status FROM Forms INNER JOIN StudentForms ON Forms.ID=StudentForms.FormID where studentrollNo = '" + rollNo + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Requests.Type = dr.GetString(0).Trim();
                Requests.Date = dr.GetString(1).Trim();
                Requests.CurrentDesk = dr.GetString(2).Trim();
                Requests.Flow = dr.GetString(3).Trim();
                Requests.Status = dr.GetString(4).Trim();
                Requests.StudentRollNo = dr.GetString(5).Trim();
                //StudentInfo.Email = email;
            }
            conn.Close();
        }
        /*
        public async void GetStaff(string id, string email)
        {
            Staff staff = null;
            string query = "Select * from Student where UserId =@p0";
            //staff = await db.Students.SqlQuery(query, id).SingleOrDefaultAsync();

            StaffInfo.Name = staff.Name;
            StaffInfo.Email = email;
            StaffInfo.StaffRole = staff.StaffRole;
            StaffInfo.Username = staff.Username;
        }

        /*
        public User GetUser(string login)
        {

            //get User from DB
            SqlConnection con = new SqlConnection("server=faksam;Database=FUOnlineService; Integrated Security=true;");
            con.Open();
            string sql = "SELECT Password FROM Users WHERE UserName='" + login + "'";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader dr = cmd.ExecuteReader();
            User u = null;
            if (dr.Read())
            {
                u = new User();
                u.UserName = login;
                u.Password = dr.GetString(0).Trim();
            }
            con.Close();
            return u;
        }

        public string GetUserRole()
        {
            return Roles.GetRolesForUser();
        }
        */
    }
}
