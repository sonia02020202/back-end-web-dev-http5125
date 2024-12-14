using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;
using cumulative1.Models;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        
        // Returns a list of Teacher(s) in the system
        //A list of Teachers
       
        [HttpGet("TeacherList")]
        public List<Teacher> ListTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM teachers";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Teacher teacher = new Teacher()
                        {
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            TeacherFName = reader["teacherfname"].ToString(),
                            TeacherLName = reader["teacherlname"].ToString(),
                            EmployeeNumber = reader["employeenumber"].ToString(),
                            hiredate = Convert.ToDateTime(reader["hiredate"]),
                            salary = Convert.ToDouble(reader["salary"])
                        };
                        teachers.Add(teacher);
                    }
                }
            }

            return teachers;
        }

        /// <summary>
        /// Returns a Teacher from the Database by their ID
        /// </summary>
        /// <param name="id">The ID of the teacher to find</param>
        /// <returns>A Teacher object</returns>
        
      [HttpGet("FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher selectedTeacher = new Teacher();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM teachers WHERE teacherid=@id";
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        selectedTeacher.TeacherId = Convert.ToInt32(reader["teacherid"]);
                        selectedTeacher.TeacherFName = reader["teacherfname"].ToString();
                        selectedTeacher.TeacherLName = reader["teacherlname"].ToString();
                        selectedTeacher.EmployeeNumber = reader["employeenumber"].ToString();
                        selectedTeacher.hiredate = Convert.ToDateTime(reader["hiredate"]);
                        selectedTeacher.salary = Convert.ToDouble(reader["salary"]);
                    }
                }
            }

            return selectedTeacher;
        }

        //<summary>
        /// Add new teacher to the database
        /// </summary>
        /// <param name="teacher">The teacher data to be added</param>
        /// <returns>The ID of the newly added teacher</returns>
        [HttpPost("AddTeacher")]
        public int AddTeacher([FromBody] Teacher teacher)
        {
            

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                                      "VALUES (@teacherfname, @teacherlname, @employeenumber, CURRENT_DATE(), @salary)";
                Command.Parameters.AddWithValue("@teacherfname", teacher.TeacherFName);
                Command.Parameters.AddWithValue("@teacherlname", teacher.TeacherLName);
                Command.Parameters.AddWithValue("@employeenumber", teacher.EmployeeNumber);
                Command.Parameters.AddWithValue("@salary", teacher.salary);

                Command.ExecuteNonQuery();
               
                    // Return the ID of the newly added teacher
                     
                    return Convert.ToInt32(Command.LastInsertedId);

              }
            
        }

        /// <summary>
        /// Deletes a teacher by their ID
        /// </summary>
        /// <param name="id">The ID of the teacher to be deleted</param>
        /// <returns>Status of the deletion</returns>
        
        [HttpDelete(template: "DeleteTeacher/{TeacherId}")]
        public int DeleteTeacher(int TeacherId)
        {
            // 'using' will close the connection after the code executes
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                
                //A new command for our database

                MySqlCommand Command = Connection.CreateCommand();


                Command.CommandText = "delete from teachers where teacherid=@id";
                Command.Parameters.AddWithValue("@id", TeacherId);
                return Command.ExecuteNonQuery();

            }
            // if failure
            return 0;
        }
         /// <summary>
 /// Updates an existing teacher's details in the database.
 /// </summary>
 /// <param name="TeacherId">The ID of the teacher to update.</param>
 /// <param name="TeacherData">The updated teacher data.</param>
 /// <returns>The updated Teacher object.</returns>
 /// <example>
 /// PUT: api/TeacherAPI/TeacherUpdate/1
 /// Body: { "TeacherFName": "Jane", "TeacherLName": "Doe", "EmployeeNumber": "E54321", "hiredate": "2024-01-01", "salary": 55000 }
 /// </example>
 
        [HttpPut("TeacherUpdate/{TeacherId}")]
        public IActionResult UpdateTeacher(int TeacherId, [FromBody] Teacher TeacherData)
        {

            // 'using' will close the connection after the code is executed

            using (MySqlConnection Connection = _context.AccessDatabase())

            {
                Connection.Open();
                //Establish a new command (query) for our database
               
                MySqlCommand Command = Connection.CreateCommand();

                //  query
                Command.CommandText = "UPDATE teachers SET teacherfname=@teacherfname, teacherlname=@teacherlname, employeenumber=@employeenumber, hiredate=@hiredate, salary=@salary WHERE teacherid=@id";
                Command.Parameters.AddWithValue("@teacherfname", TeacherData.TeacherFName);
                Command.Parameters.AddWithValue("@teacherlname", TeacherData.TeacherLName);
                Command.Parameters.AddWithValue("@employeenumber", TeacherData.EmployeeNumber);
                Command.Parameters.AddWithValue("@hiredate", TeacherData.hiredate);
                Command.Parameters.AddWithValue("@salary", TeacherData.salary);
                Command.Parameters.AddWithValue("@id", TeacherId);

                Command.ExecuteNonQuery();

                return Ok(FindTeacher(TeacherId)); 
                // Find Teacher returns the updated teacher
            }

        }

    }
}
