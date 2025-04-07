using Microsoft.AspNetCore.Mvc;

namespace ApiServiceREgistrySample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : Controller
    {

        [HttpGet]
        [Route("check")]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}
