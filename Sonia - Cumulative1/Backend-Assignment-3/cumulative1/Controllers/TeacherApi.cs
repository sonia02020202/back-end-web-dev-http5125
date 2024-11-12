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
        // database context
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// It returns a list of Teachers in the system
        /// an example
        /// retrieves Teacher list GET api/Teacher/Listteacher -> 
        /// 
        /// <returns>
        /// A list of the Teacher/Teachers
        /// </returns>


        [HttpGet]
        [Route(template: "TeacherList")]

        public List<Teacher> ListTeachers()
        {
            // Create an empty list of Teacher
            List<Teacher> Teachers = new List<Teacher>();
            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //create new query for our DB
                MySqlCommand Command = Connection.CreateCommand();

                //SQL QUERY
                Command.CommandText = "select * from teachers";
                // Gather results of queries into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop through each row from the results
                    while (ResultSet.Read())
                    {
                        //retrieve the column info by the DataBase column name as an index
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                        double salary = Convert.ToDouble(ResultSet["salary"]);

                        //short cut to set all properties while creating an object
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


            //Returning the final list of teachers
            return Teachers;


        }
        /// <summary>
        /// Returns a Teacher from the DB by their ID
        /// </summary>
        /// an example
        /// GET api/Teacher/FindTeacher/7 -> 
        ///Returns
        /// Matching a Teacher by their ID. Empty results if teacher is not found
        

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
                //Established a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();
                // @id is replaced with a id
                Command.CommandText = "select * from teachers where teacherid=@id";
                Command.Parameters.AddWithValue("@id", id);
                // Collect results of the query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Each Row of the result Loop though
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

