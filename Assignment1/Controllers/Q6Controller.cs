using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q6Controller : ControllerBase
    {
        [HttpGet("hexagon/{side}")]
        public ActionResult<double> GetHexagonArea(double side)
        {
           

            double area = (3 * Math.Sqrt(3) / 2) * Math.Pow(side, 2);
            return (area);
        }
    }
}
