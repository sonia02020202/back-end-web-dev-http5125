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

        /// <summary>
        /// Lists all teachers.
        /// </summary>
        /// <returns>A view displaying the list of teachers.</returns>
        /// <example>
        /// GET: TeacherPage/List
        /// </example>
        // List all teachers (GET: TeacherPage/List)
        public IActionResult List()
        {
           //Fetches The list of teachers using the API
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

        //Fetches the teachers current details using the API
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
        // GET: TeacherPage/Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }

        // PUT: TeacherPage/Update{id}
/// <summary>
/// Updates a teacher's details and redirects to the Show page for the teacher.
/// </summary>
/// <param name="id">The ID of the teacher to update.</param>
/// <param name="TeacherFName">The updated first name.</param>
/// <param name="TeacherLName">The updated last name.</param>
/// <param name="EmployeeNumber">The updated employee number.</param>
/// <param name="salary">The updated salary.</param>
/// <param name="hireDate">The updated hire date.</param>
/// <returns>A redirection to the Show page for the updated teacher.</returns>
/// <example>
/// POST: TeacherPage/Update/1
/// Body: { "TeacherFName": "Jane", "TeacherLName": "Smith", "EmployeeNumber": "E54321", "salary": 55000, "hireDate": "2024-01-01" }
/// </example>
        
        [HttpPost]
        public IActionResult Update(int id, string TeacherFName, string TeacherLName, string EmployeeNumber, double salary, DateTime hireDate)
        {
            // Creates a new Teacher object with updated details

            Teacher updatedTeacher = new Teacher
            {
                TeacherFName = TeacherFName,
                TeacherLName = TeacherLName,
                EmployeeNumber = EmployeeNumber,
                salary = salary,
                hiredate = hireDate
            };


            _api.UpdateTeacher(id, updatedTeacher);

            // Redirected to show the teacher details

            return RedirectToAction("Show", new { id = id });
        }
    }
}
