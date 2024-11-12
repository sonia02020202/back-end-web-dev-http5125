using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;
using cumulative1.Models;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : Controller
    {
        // dependency injection of database context
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns a list of Teacher(s) in the system
        /// </summary>
        /// <example>
        /// retrieve Teacher list GET api/Teacher/Listteacher -> 
        /// </example>
        /// <returns>
        /// A list of Teacher(s)
        /// </returns>


        [HttpGet]
        [Route(template: "TeacherList")]

        public List<Teacher> ListTeachers()
        {
            // Creating an empty list of Teacher
            List<Teacher> Teachers = new List<Teacher>();
            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //create a new query for our DB
                MySqlCommand Command = Connection.CreateCommand();

                //SQL QUERY
                Command.CommandText = "select * from teachers";
                // Gather results of queries into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop through each row from the results
                    while (ResultSet.Read())
                    {
                        //retrieve column information by the DB column name as an index
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                        double salary = Convert.ToDouble(ResultSet["salary"]);

                        //short cut  to set all properties while creating an object
                        Teacher CurrentTeacher = new Teacher()
                        {
                            TeacherId = TeacherId,
                            TeacherFName = TeacherFName,
                            TeacherLName = TeacherLName,
                            EmployeeNumber = EmployeeNumber,
                            hiredate = hiredate,
                            salary = salary
                        };

                        Teachers.Add(CurrentTeacher);

                    }
                }
            }


            //Return the final list of teachers
            return Teachers;


        }
        /// <summary>
        /// Returns a Teacher from the DB by their ID
        /// </summary>
        /// <example>
        /// GET api/Teacher/FindTeacher/7 -> 
        /// </example>
        /// <returns>
        /// A matching Teacher  by their ID. Empty results if teacher not found
        /// </returns>

        [HttpGet]
        [Route(template: "FindTeacher/{id}")]
        //Create empty Teacher
        public Teacher FindTeacher(int id)
        {

            Teacher SelectedTeacher = new Teacher();
            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();
                // @id is replaced with a id
                Command.CommandText = "select * from teachers where teacherid=@id";
                Command.Parameters.AddWithValue("@id", id);
                // Gather results of the query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the result
                    while (ResultSet.Read())
                    {
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                        double salary = Convert.ToDouble(ResultSet["salary"]);

                        SelectedTeacher.TeacherId = TeacherId;
                        SelectedTeacher.TeacherFName = TeacherFName;
                        SelectedTeacher.TeacherLName = TeacherLName;
                        SelectedTeacher.EmployeeNumber = EmployeeNumber;
                        SelectedTeacher.hiredate = hiredate;
                        SelectedTeacher.salary = salary;

                    }
                }
            }


            //Return SelectedTeacher
            return SelectedTeacher;
        }

    }
}

