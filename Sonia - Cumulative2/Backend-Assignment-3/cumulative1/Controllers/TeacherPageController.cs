using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using System.Collections.Generic;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;

        // Dependency injection of the API controller
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        // List all teachers (GET: TeacherPage/List)
        public IActionResult List()
        {
            List<Teacher> teachers = _api.ListTeachers();
            return View(teachers);
        }

        // Show details of a specific teacher (GET: TeacherPage/Show/{id})
        public IActionResult Show(int id)
        {
            Teacher selectedTeacher = _api.FindTeacher(id);
            return View(selectedTeacher);
        }

        // Add a new teacher (GET: TeacherPage/Add)
        [HttpGet]
        public IActionResult New(int id)
        {
            return View();
        }

        // Add a new teacher (POST: TeacherPage/Add)
        [HttpPost]
        public IActionResult Create(Teacher NewTeacher)
        {        
                // Calling AddTeacher method from API
                  // AddTeacher returns ActionResult<int>

            // Check if the result is an OkObjectResult

            // Get the teacher ID from the result
            int TeacherId = (_api.AddTeacher(NewTeacher));

            return RedirectToAction("Show", new { id = TeacherId });
            
            
        }

        // Confirm deletion of teacher (GET: TeacherPage/ConfirmDelete/{id})
        public IActionResult ConfirmDelete(int id)
        {
            Teacher selectedTeacher = _api.FindTeacher(id);
            return View(selectedTeacher);
        }

        // Delete a teacher (POST: TeacherPage/Delete/{id})
        [HttpPost]
        public IActionResult Delete(int id)
        {
            int TeacherId = _api.DeleteTeacher(id);
            // redirects to list action
            return RedirectToAction("List");
        }
    }
}
