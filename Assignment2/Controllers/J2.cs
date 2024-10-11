using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J2Controller : ControllerBase
        /// calculates the pepper values based on the peppers given.
        /// comment summary.
        
    {
        [HttpGet(template: "ChiliPeppers")]
        public int SHU(string Ingredients)
        {
            List<string> spice = new List<string>();
            spice = Ingredients.Split(",").ToList();
            int value = 0;
            for (int i = 0; i < spice.Count; i++)
            {
                if (spice[i] == "Poblano")
                {
                    value += 1500;
                }
                else if (spice[i] == "Mirasol")
                {
                    value += 6000;
                }
                else if (spice[i] == "Serrano")
                {
                    value += 15500;
                }
                else if (spice[i] == "Cayenne")
                {
                    value += 40000;
                }
                else if (spice[i] == "Thai")
                {
                    value += 75000;
                }
                else if (spice[i] == "Habanero")
                {
                    value += 125000;
                }
            }

            return value;

        }

    }
}

