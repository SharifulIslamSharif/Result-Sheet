using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModels
{
    public class StudentVM
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DoB { get; set; }
        public string Picture { get; set; }
        public HttpPostedFileBase imgFile { get; set; }
    }
}