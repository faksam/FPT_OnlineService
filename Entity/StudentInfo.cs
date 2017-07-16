using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class StudentInfo : UserInfo
    {
        private static string rollNo;
        private static string major;

        public static string RollNo
        {
            get { return rollNo; }
            set { rollNo = value; }
        }

        public static string Major
        {
            get { return major; }
            set { major = value; }
        }
    }
}
