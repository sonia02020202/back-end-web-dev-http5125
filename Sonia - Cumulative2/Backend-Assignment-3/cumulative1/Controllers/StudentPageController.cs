using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cumulative1.Controllers
{
  public class StudentPageController : Controller
    {
      private readonly StudentAPIController _api;

        //Using api to get Student information

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }
        // get students by their name in list format GET : Student Page

      public IActionResult List()
        {
            
            
            List<Student> Students = _api.ListStudents();
            return View(Students);
        }

        // students id GET :StudentPage/Show/{id} 
        public IActionResult Show(int id)
        {
         Student SelectedStudent = _api.FindStudent(id);
         return View(SelectedStudent);
        }

    }
}