using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;
using cumulative1.Models;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : Controller
    {
      
    private readonly SchoolDbContext _context;

    public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        
        //Returns a list of Students 
        // locates Student list 
        
       
        /// A list of Students
        


        [HttpGet]
        [Route(template: "StudentList")]

        public List<Student> ListStudents()
        {
            // Creating an empty list of Students
            List<Student> Students = new List<Student>();
            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
            
             Connection.Open();

             //create new query for Database

             MySqlCommand Command = Connection.CreateCommand();


                //SQL QUERY

                Command.CommandText = "select * from students";


                // Gather results of queries into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                  
                    
                    while (ResultSet.Read())
                    {
                        //get column information by the DB column name as an index

                        int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                        string StudentFName = ResultSet["studentfname"].ToString();
                        string StudentLName = ResultSet["studentlname"].ToString();
                        string StudentNumber = ResultSet["studentnumber"].ToString();
                        DateTime enroldate = Convert.ToDateTime(ResultSet["enroldate"]);
                       

                      
                        Student CurrentStudent = new Student()
                        {
                            StudentId = StudentId,
                            StudentFName = StudentFName,
                            StudentLName = StudentLName,
                            StudentNumber = StudentNumber,
                            enroldate = enroldate
                            
                        };

                        Students.Add(CurrentStudent);

                    }
                }
            }


            //Return the list of Students

            return Students;


        }
       
        // Returns a Student from the Database by their ID
        
        /// student matching by their ID. results will say Student not found

        [HttpGet]

        [Route(template: "FindStudent/{id}")]

        
        public Student FindStudent(int id)
        {

            Student SelectedStudent = new Student();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //create new command

                MySqlCommand Command = Connection.CreateCommand();

                
                Command.CommandText = "select * from students where studentid=@id";
                Command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop each result
                    
                    while (ResultSet.Read())

                    {
                        int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                        string StudentFName = ResultSet["studentfname"].ToString();
                        string StudentLName = ResultSet["studentlname"].ToString();
                        string StudentNumber = ResultSet["studentnumber"].ToString();
                        DateTime enroldate = Convert.ToDateTime(ResultSet["enroldate"]);


                        SelectedStudent.StudentId = StudentId;

                        SelectedStudent.StudentFName = StudentFName;

                        SelectedStudent.StudentLName = StudentLName;

                        SelectedStudent.StudentNumber = StudentNumber;

                        SelectedStudent.enroldate = enroldate;
                        

                    }
                }
            }


           
            return SelectedStudent;
        }

    }
}

