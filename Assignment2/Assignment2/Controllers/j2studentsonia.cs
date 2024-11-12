using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace why.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class j2_student : ControllerBase 
        /// Comment Summary Calculates the person with the highest value as in
        /// Sonia.
    {
        [HttpGet(template: "")]

        public string boiling(string value)
        {
            List<string> list = new List<string>();
            list = value.Split(",").ToList();
            int temp = 0;
            int person = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (int.Parse(list[i + 1]) > temp)
                {
                    temp = int.Parse(list[i + 1]);
                    person = i;
                }
                i++;
            }
            return $"{list[person]}";
        }
    }
}