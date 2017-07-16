using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Requests
    {
        private static int id { get; set; }

        private static string type { get; set; }

        private static string date { get; set; }

        private static string currentDesk { get; set; }

        private static string flow { get; set; }

        private static string status { get; set; }

        private static string studentRollNo { get; set; }

        public static int ID
        {
            get { return id; }
            set { id = value; }
        }

        public static string Type
        {
            get { return type; }
            set { type = value; }
        }

        public static string Date
        {
            get { return date; }
            set { date = value; }
        }

        public static string CurrentDesk
        {
            get { return currentDesk; }
            set { currentDesk = value; }
        }

        public static string Flow
        {
            get { return flow; }
            set { flow = value; }
        }

        public static string Status
        {
            get { return status; }
            set { status = value; }
        }

        public static string StudentRollNo
        {
            get { return studentRollNo; }
            set { studentRollNo = value; }
        }

        /*public static string ID
        {
            get { return id; }
            set { id = value; }
        }*/
    }
}
