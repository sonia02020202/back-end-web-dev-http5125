using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    public class Q1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("welcome")]
        public IActionResult Welcome()
        {

            return Ok("welcome 5125");
        }



    }
}
