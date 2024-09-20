using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q3Controller : ControllerBase
    {
        [HttpGet(template:"cube/{cube}")]
        public double cube(int cube) {
            return Math.Pow(cube, 3);
        
        }

    }
}
