using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class StaffInfo : UserInfo
    {
        private static string staffRole;
        private static string username;

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        public static string StaffRole
        {
            get { return staffRole; }
            set { staffRole = value; }
        }

    }
}
