using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpGet]
        public ActionResult GetTests()
        {
            return Ok(new
            {
                value = "Test"
            });
        }
    }
}
