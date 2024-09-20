using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    public class Q2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("greeting")]
        public string Greeting(string name)
        {
            return HtmlEncoder.Default.Encode($"greeting {name}");
        }



    }
}
