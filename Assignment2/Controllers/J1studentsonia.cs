using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace why.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class j1_student : ControllerBase
        ///comment summary: gets the value of and then calucates value times 5
        ///- 400 to sea level
    {
        [HttpGet(template: "BoilingWater")]
        public string boiling(int value)
        {
            int calculation = (value * 5) - 400;
            if (calculation > 100)
            {
                return $"{calculation} 1";
            }
            else
            {
                return $"{calculation} -1";
            }
        }
    }
}
