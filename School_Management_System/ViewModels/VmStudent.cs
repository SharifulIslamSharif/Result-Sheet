using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModels
{
        public class VmStudent : Student
        {
            public HttpPostedFileBase file { get; set; }
        }   
}