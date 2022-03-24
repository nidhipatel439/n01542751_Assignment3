using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01542751_Assignment3.Models;

namespace n01542751_Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        //showing a page of all teacher information
        public ActionResult List()
        {
            //connect a data access layer
            TeacherDataController controller = new TeacherDataController();

            List<Teacher> Teachers = controller.ListTeachers();

            return View(Teachers);
        }

        //GET: Teacher/Show/id
        [Route("Teacher/Show/{id}")]

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            Teacher SelectedTeacher = controller.FindTeacher(id);

            //routes the single teacher information to Show.cshtml
            return View(SelectedTeacher);
        }
    }
}