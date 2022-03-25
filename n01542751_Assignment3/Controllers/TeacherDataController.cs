using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n01542751_Assignment3.Models;

namespace n01542751_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        //allows to access our database
        private SchoolDbContext School = new SchoolDbContext();

        ///<summary>
        /// Return the list of Teachers
        ///</summary>
        ///<example>Get api/TeacherData/ListTeachers</example>
        ///<return>
        ///A list of Teacher object (including id, firstname, lastname, employeenumber, hiredate, salary)
        ///</return>
        
        [HttpGet]
        public List<Teacher> ListTeachers()
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish a new command
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql query
            cmd.CommandText = "select * from teachers";

            //gather result of query into a variable
            MySqlDataReader SetResult = cmd.ExecuteReader();

            //create an empty list of teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //loop for the result
            while (SetResult.Read())
            {
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(SetResult["teacherid"]);
                NewTeacher.TeacherFName = SetResult["teacherfname"].ToString();
                NewTeacher.TeacherLName = SetResult["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = SetResult["employeenumber"].ToString();
                NewTeacher.HireDate = SetResult["hiredate"].ToString();
                NewTeacher.Salary = Convert.ToDouble(SetResult["salary"]);

                Teachers.Add(NewTeacher);
            }

            //close connection
            Conn.Close();

            return Teachers;
        }

        /// <summary>
        /// return a teacher information which match the teacher id
        /// </summary>
        /// <param name="teacherid">teacher's id number</param>
        /// <returns>
        /// return a list of teacher object (including id, firstname, lastname, employeenumber, hiredate, salary,course name)
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{teacherid}")]

        public List<Teacher> FindTeacher(int teacherid)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish a new command
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "select teachers.*, classes.classname from teachers left join classes on teachers.teacherid = classes.teacherid where teachers.teacherid = " + teacherid;

            //gather result of query into a variable
            MySqlDataReader SetResult = cmd.ExecuteReader();

            //create an empty list of teacher
            List<Teacher> Teachers = new List<Teacher> { };

            //loop for the result
            while(SetResult.Read())
            {
                Teacher SelectedTeacher = new Teacher();

                SelectedTeacher.TeacherId = Convert.ToInt32(SetResult["teacherid"]);
                SelectedTeacher.TeacherFName = SetResult["teacherfname"].ToString();
                SelectedTeacher.TeacherLName = SetResult["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = SetResult["employeenumber"].ToString();
                SelectedTeacher.HireDate = SetResult["hiredate"].ToString();
                SelectedTeacher.Salary = Convert.ToDouble(SetResult["salary"]);
                SelectedTeacher.ClassName = SetResult["classname"].ToString();

                Teachers.Add(SelectedTeacher);
            }

            //close the connection
            Conn.Close();

            //return the final list of teacher information
            return Teachers;
        }

    }
}
