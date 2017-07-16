using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPT_OnlineService.Helper
{
    public class StaffRole
    {
        private string roleName;

        private string staffName;

        private string staffEmail;

        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        public string StaffName
        {
            get { return staffName; }
            set { staffName = value; }
        }

        public string StaffEmail
        {
            get { return staffEmail; }
            set { staffEmail = value; }
        }
    }
}