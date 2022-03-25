using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01542751_Assignment3.Models;
using System.Diagnostics;

namespace n01542751_Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        //// GET: Teacher
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Teacher/List
        //showing a page of all teacher information
        [Route("/Teacher/List/{SearchKey}")]
        public ActionResult List(string SearchKey)
        {
            Debug.WriteLine(SearchKey);
            //connect a data access layer
            TeacherDataController controller = new TeacherDataController();

            List<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        //GET: Teacher/Show/id
        [Route("Teacher/Show/{id}")]

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            TeacherCourse TC = controller.FindTeacher(id);

            //routes the single teacher information to Show.cshtml
            return View(TC);
        }
    }
}