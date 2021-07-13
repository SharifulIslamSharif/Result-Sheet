using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.ViewModels
{
    public class VmResultSheet
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Grade { get; set; }
        public decimal GPA { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }

    }
}