namespace Cumulative1.Models
{
    public class Student
    {

        //Student Identification
       public int StudentId { get; set; }

        //Students First Name.
        public string? StudentFName { get; set; }
        //Student's Last Name
        public string? StudentLName { get; set; }
        // Students Number
        public string? StudentNumber { get; set; }

        //  Student's date of enrollment
        public DateTime enroldate { get; set; }
        
    }
}