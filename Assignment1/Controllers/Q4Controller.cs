using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q4Controller : ControllerBase
    {
        [HttpPost(template: "KnockKnock")]
        public string knockknock(){
            return "Whos is There";


        }
    }
}
