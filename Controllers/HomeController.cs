using Microsoft.AspNetCore.Mvc;
namespace SistemaBiblioteca.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}