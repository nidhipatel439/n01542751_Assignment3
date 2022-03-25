﻿using MySql.Data.MySqlClient;
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
            cmd.CommandText = "SELECT * FROM teachers";

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
        /// return a teacher information (including id, firstname, lastname, employeenumber, hiredate, salary) as weel as teacher's course list 
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{teacherid}")]

        public TeacherCourse FindTeacher(int teacherid)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //establish a new command
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            // cmd.CommandText = "select teachers.*, classes.classname from teachers left join classes on teachers.teacherid = classes.teacherid where teachers.teacherid = " + teacherid;

            // find teacher 
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = @teacherid";
            cmd.Parameters.AddWithValue("@teacherid", teacherid);
            cmd.Prepare();

            //gather result of query into a variable
            MySqlDataReader TeacherResult= cmd.ExecuteReader();

            //create a teacher instance
            Teacher SelectedTeacher= new Teacher();


            //loop for the result
            while(TeacherResult.Read())
            {

                SelectedTeacher.TeacherId = Convert.ToInt32(TeacherResult["teacherid"]);
                SelectedTeacher.TeacherFName = TeacherResult["teacherfname"].ToString();
                SelectedTeacher.TeacherLName = TeacherResult["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = TeacherResult["employeenumber"].ToString();
                SelectedTeacher.HireDate = TeacherResult["hiredate"].ToString();
                SelectedTeacher.Salary = Convert.ToDouble(TeacherResult["salary"]);

            }

            // close sql reader
            TeacherResult.Close();

            // establish a new command
            // MySqlCommand cmd2 = Conn.CreateCommand();

            // find course for the teacher
            cmd.CommandText = "SELECT * FROM classes WHERE teacherid = @teacherid";
            cmd.Parameters.AddWithValue("@teacherid", "teacherid");
            cmd.Prepare();

            //gather result of query into a variable
            MySqlDataReader ClassResult = cmd.ExecuteReader();

            // create a list of classes
            List<Course> ClassList = new List<Course> { };

            //loop for the result
            while (ClassResult.Read())
            {
                Course NewCourse = new Course();
                NewCourse.ClassName = ClassResult["classname"].ToString();
                ClassList.Add(NewCourse);
            }

            // close sql reader
            ClassResult.Close();

            //close the connection
            Conn.Close();

            //combine a teacher and a couse information
            TeacherCourse TC = new TeacherCourse();
            TC.Teacher = SelectedTeacher;
            TC.Courses = ClassList;

            //return the final list of teacher information
            return TC;
        }

    }
}
