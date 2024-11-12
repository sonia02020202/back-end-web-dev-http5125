using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q5Controller : ControllerBase
    {
        [HttpPost(template:"secret/{secret}")]
        public string secret(int secret) {
            return "Shh.. the secret is "+ secret ; 
        }
                
        

    }
}
