using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n01542751_Assignment3.Models;
using MySql.Data.MySqlClient;

namespace n01542751_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<string> ListTeacher()
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<String> TeacherNames = new List<string> { };

            while (ResultSet.Read())
            {
                string teacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                TeacherNames.Add(teacherName);
            }

            Conn.Close();

            return TeacherNames;

        }
    }
}
