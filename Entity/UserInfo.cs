using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserInfo
    {
        //private static string id;

        /*public static string ID
        {
            get { return id; }
            set { id = value; }
        }*/

        private static string name;
        private static string email;
        private static string role;

        public static string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static string Email
        {
            get { return email; }
            set { email = value; }
        }

        public static string Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}
