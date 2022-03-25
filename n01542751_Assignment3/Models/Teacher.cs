using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01542751_Assignment3.Models
{
    public class Teacher
    {
        public int TeacherId  { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public string EmployeeNumber { get; set; }
        public string HireDate { get; set; }
        public double Salary { get; set; }

        public string ClassName { get; set; }

    }
}