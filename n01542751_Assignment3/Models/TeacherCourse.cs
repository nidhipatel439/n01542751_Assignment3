using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01542751_Assignment3.Models
{
    public class TeacherCourse
    {
        public Teacher Teacher { get; set; }

        public List<Course> Courses { get; set; }

    }
}