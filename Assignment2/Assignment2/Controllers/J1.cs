using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J1Controller : ControllerBase
    {
        /// <summary>
        /// I tested different numbers takes values for deliveries
        /// takes the values of collisions and calucates the
        /// value based on that!!
        /// </summary>
        /// <param name="Collisions"></param>
        /// <param name="Deliveries"></param>
        /// <returns></returns>
        [HttpPost(template: "Delivedroid")]
        [Consumes("application/x-www-form-urlencoded")]
        public int points([FromForm] int Collisions, [FromForm] int Deliveries)
        {

            int total = Deliveries * 50 - Collisions * 10;
            if (Deliveries > Collisions)
            {
                return total + 500;
            }
            return total;
        }
    }
}